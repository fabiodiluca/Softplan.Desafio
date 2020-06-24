﻿using Autofac;
using AutoMapper;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using Serilog.Sinks.RollingFileAlternate;
using Softplan.Desafio.Api1;
using Softplan.Desafio.Response;
using Softplan.Desafio.Infra.CrossCutting.AutoMapper;
using Softplan.Desafio.Infra.CrossCutting.IoC;
using Softplan.Desafio.Response;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace Softplan.Desafio.Api1
{
    public class Startup
    {
        private const string Version = "1";
        private const string ProjectTitleName = "Softplan.Desafio.Api1";
        private readonly IConfigurationRoot _configurationRoot;
        private readonly ILogger _serilog;

        public Startup(IConfiguration configuration)
        {
            try
            {
                _serilog = CreateLogger();

                var builder = new ConfigurationBuilder()
                       .SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile("appsettings.json", true, true);

                _configurationRoot = builder.Build();

                Configuration = configuration;
            }
            catch (Exception ex)
            {
                _serilog?.Error("Erro no Startup: " + ex);
            }
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(ctx => _serilog);

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(opt =>
                {
                    opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    opt.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    opt.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                    opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });

            services.AddResponseCompression(options =>
            {
                options.Providers.Add<BrotliCompressionProvider>();
                options.EnableForHttps = true;
            });

            services.AddSingleton<IConfigurationRoot>(_configurationRoot);

            ConfigureSwagger(services);
        }

        private void ConfigureSwagger(IServiceCollection services)
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
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new ApplicationModule());
            builder.RegisterType<Presenter>();
        }

        private ILogger CreateLogger()
        {
            return new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .Enrich.FromLogContext()
                .WriteTo.Async(s =>
                    s.RollingFileAlternate(
                        @"Logs/",
                        outputTemplate:
                        "[{ProcessId}] {Timestamp} [{ThreadId}] [{Level}] [{SourceContext}] [{Category}] {Message}{NewLine}{Exception}",
                        fileSizeLimitBytes: 10 * 1024 * 1024,
                        retainedFileCountLimit: 100,
                        formatProvider: CreateLoggingCulture()
                    ).MinimumLevel.Debug())
                .CreateLogger();
        }

        private static CultureInfo CreateLoggingCulture()
        {
            var loggingCulture = new CultureInfo("");
            loggingCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            loggingCulture.DateTimeFormat.LongTimePattern = "HH:mm:ss.fffzz";

            return loggingCulture;
        }

    }
}