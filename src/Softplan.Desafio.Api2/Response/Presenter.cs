using Softplan.Desafio.Api2.Serialization;
using Softplan.Desafio.Application;
using Softplan.Desafio.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Softplan.Desafio.Api2.Response
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
