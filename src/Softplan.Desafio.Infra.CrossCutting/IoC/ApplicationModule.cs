using Autofac;
using Softplan.Desafio.Application.UseCases.TaxaJuros;
using Softplan.Desafio.UseCase;

namespace Softplan.Desafio.Infra.CrossCutting.IoC
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
               .RegisterAssemblyTypes(typeof(IUseCaseBase).Assembly)
               .AsImplementedInterfaces().InstancePerLifetimeScope();

            builder
               .RegisterAssemblyTypes(typeof(ITaxaJurosUseCase).Assembly)
               .AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}