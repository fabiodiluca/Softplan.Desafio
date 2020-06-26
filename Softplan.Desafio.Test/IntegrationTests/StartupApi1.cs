using Autofac;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Softplan.Desafio.Application;
using Softplan.Desafio.Application.UseCases.TaxaJuros;
using Softplan.Desafio.Response;
using Softplan.Desafio.UseCase;

namespace Softplan.Desafio.Test.IntegrationTests
{
    public class StartupApi1 : StartupBase
    {
        protected override string Version { get { return "1"; } }
        protected override string ProjectTitleName { get { return "Softplan.Desafio.Api1"; } }

        public StartupApi1(IConfiguration configuration, IHostingEnvironment env) : base(configuration, env)
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

            builder
                .RegisterType(typeof(Api1Client))
                .As<IApi1Client>()
                .InstancePerLifetimeScope();

            builder.RegisterType<Presenter>();
        }
    }
}
