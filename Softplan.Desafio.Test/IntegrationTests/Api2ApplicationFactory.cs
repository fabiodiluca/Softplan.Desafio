using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using Softplan.Desafio.Application;

namespace Softplan.Desafio.Test.IntegrationTests
{
    public class Api2ApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected HttpClient httpClientApi1;

        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return WebHost.CreateDefaultBuilder();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(
                services => {
                    services.AddAutofac();
                    Api1Client apiClient = new Api1Client(null);
                    apiClient.HttpClient = this.httpClientApi1;
                    services.AddSingleton<IApi1Client>(apiClient);
                }
            );
            builder.UseStartup<StartupApi2>();

            base.ConfigureWebHost(builder);
        }

        public void SetApi1HttpClient(HttpClient client)
        {
            httpClientApi1 = client;
        }
    }
}
