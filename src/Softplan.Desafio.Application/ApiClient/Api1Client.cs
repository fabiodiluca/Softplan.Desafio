using DiLuca.RestApiClient;
using Microsoft.Extensions.Configuration;
using Softplan.Desafio.Application.Exceptions;
using Softplan.Desafio.Application.UseCases.TaxaJuros;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Softplan.Desafio.Application
{
    public class Api1Client : RestApiClient, IApi1Client
    {
        public Api1Client(IConfigurationRoot configRoot) : base(configRoot["api1Uri"]?? "http://localhost")
        {
        }

        public async Task<TaxaJurosResponse> GetTaxaJuros()
        {
            TaxaJurosResponse taxaJurosResponse = null;

            (await this._GetAsync("taxaJuros"))
            .HttpStatus(HttpStatusCode.OK, ref taxaJurosResponse)
            .OtherHttpStatus((resp) =>
                throw new Api1GetTaxaJurosException(null)
            );

            return taxaJurosResponse;
        }
    }
}
