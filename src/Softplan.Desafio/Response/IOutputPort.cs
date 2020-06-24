namespace Softplan.Desafio.Response
{
    public interface IOutputPort<in TUseCaseResponse>
    {
        void Handler(TUseCaseResponse response);        
    }
}