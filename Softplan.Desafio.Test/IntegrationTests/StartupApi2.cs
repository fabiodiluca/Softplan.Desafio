using Autofac;
using Microsoft.Extensions.Configuration;
using Softplan.Desafio.Application.UseCases.TaxaJuros;
using Softplan.Desafio.Response;
using Softplan.Desafio.UseCase;

namespace Softplan.Desafio.Test.IntegrationTests
{
    public class StartupApi2 : StartupBase
    {
        public StartupApi2(IConfiguration configuration) : base(configuration)
        {
            this.ProjectTitleName = "Softplan.Desafio.Api2";
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
