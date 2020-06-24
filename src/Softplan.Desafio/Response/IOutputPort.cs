namespace Softplan.Desafio.Application.Interfaces
{
    public interface IOutputPort<in TUseCaseResponse>
    {
        void Handler(TUseCaseResponse response);        
    }
}