using Autofac;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Softplan.Desafio.Application.UseCases.TaxaJuros;
using Softplan.Desafio.Response;
using Softplan.Desafio.UseCase;

namespace Softplan.Desafio.Test.IntegrationTests
{
    public class StartupApi2 : StartupBase
    {
        protected override string Version { get { return "1"; } }
        protected override string ProjectTitleName { get { return "Softplan.Desafio.Api2"; } }

        public StartupApi2(IConfiguration configuration, IHostingEnvironment env) : base(configuration, env)
        {

        }

        public override void ConfigureContainer(ContainerBuilder builder)
        {
            builder
               .RegisterAssemblyTypes(typeof(IUseCaseBase).Assembly)
               .AsImplementedInterfaces().InstancePerLifetimeScope();

            builder
                .RegisterType(typeof(TaxaJurosUseCase))
                .As<ITaxaJurosUseCase>()
                .InstancePerLifetimeScope();

            builder.RegisterType<Presenter>();
        }
    }
}
