using Autofac;
using Microsoft.Extensions.Configuration;
using Softplan.Desafio.Application;
using Softplan.Desafio.Application.UseCases.TaxaJuros;
using Softplan.Desafio.Response;
using Softplan.Desafio.UseCase;

namespace Softplan.Desafio.Test.IntegrationTests
{
    public class StartupApi1 : StartupBase
    {
        public StartupApi1(IConfiguration configuration) : base(configuration)
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

            builder
                .RegisterType(typeof(Api1Client))
                .As<IApi1Client>()
                .InstancePerLifetimeScope();

            builder.RegisterType<Presenter>();
        }
    }
}
