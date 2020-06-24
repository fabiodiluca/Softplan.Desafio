using Softplan.Desafio.Serialization;
using Softplan.Desafio.UseCase;
using System.Net;

namespace Softplan.Desafio.Response
{
    public class Presenter : IOutputPort<UseCaseResponseMessageBase>
    {
        public JsonContentResult ContentResult { get; }

        public Presenter()
        {
            ContentResult = new JsonContentResult();
        }

        public void Handler(UseCaseResponseMessageBase response)
        {
            ContentResult.StatusCode = (int)HttpStatusCode.OK;
            ContentResult.Content = JsonSerializer.SerializeObject(response);
        }
    }
}
