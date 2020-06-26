using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using Serilog.Sinks.RollingFileAlternate;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Globalization;
using System.IO;

namespace Softplan.Desafio
{
    public abstract class StartupBase
    {
        protected abstract string Version { get; }
        protected abstract string ProjectTitleName { get; }

        protected IConfigurationRoot _configurationRoot;
        protected ILogger _serilog;

        public StartupBase(IConfiguration configuration, IHostingEnvironment env)
        {
            Try(() => {
                _serilog = CreateLogger(env);
                _configurationRoot = BuildConfigurationRoot(configuration);
            });
        }

        public IConfigurationRoot BuildConfigurationRoot(IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", true, true);

            return builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            Try(() => {
                services.AddSingleton(ctx => _serilog);
                services.AddSingleton(_configurationRoot);
                AddMvc(services);
                AddSwagger(services);
            });
        }

        public void AddMvc(IServiceCollection services)
        {
            services.AddMvc()
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            .AddJsonOptions(opt =>
            {
                opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                opt.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                opt.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });
        }

        private void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.DescribeAllEnumsAsStrings();
                s.DescribeStringEnumsInCamelCase();
                s.DescribeAllParametersInCamelCase();
                s.SwaggerDoc($"v{Version}", new Info
                {
                    Version = $"v{Version}",
                    Title = ProjectTitleName,
                    Description = "API Swagger Desafio Softplan API 1",

                    TermsOfService = "TermsOfService",
                    Contact = new Contact
                    {
                        Name = "Contact_Name",
                        Email = "Contact_Email",
                        Url = "Url"
                    },
                });

                var filePath = Path.Combine(AppContext.BaseDirectory, $"{ProjectTitleName}.xml");
                s.IncludeXmlComments(filePath);
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            Try(() => {
                if (env.IsDevelopment())
                    app.UseDeveloperExceptionPage();
                else
                    app.UseHsts();

                app.UseHttpsRedirection();

                app.UseSwagger();
                app.UseSwaggerUI(s =>
                {
                    s.SwaggerEndpoint($"/swagger/v{Version}/swagger.json", "API 1");
                });

                app.UseAuthentication();
                app.UseStaticFiles();
                app.UseMvcWithDefaultRoute();
            });
        }

        public abstract void ConfigureContainer(ContainerBuilder builder);

        private ILogger CreateLogger(IHostingEnvironment env)
        {
            var config = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Async(s =>
                    s.RollingFileAlternate(
                        @"Logs/",
                        outputTemplate:
                        "[{ProcessId}] {Timestamp} [{ThreadId}] [{Level}] [{SourceContext}] [{Category}] {Message}{NewLine}{Exception}",
                        fileSizeLimitBytes: 10 * 1024 * 1024,
                        retainedFileCountLimit: 100,
                        formatProvider: new CultureInfo("pt-BR")
                    )
                );
            ;

            if (env.IsDevelopment())
                config.MinimumLevel.Verbose();
            else
                config.MinimumLevel.Error();

            return config.CreateLogger();
        }

        protected void Try(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                _serilog?.Error("Erro no Startup: " + ex);
                throw ex;
            }
        }
    }
}
