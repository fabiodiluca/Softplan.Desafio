using DiLuca.RestApiClient;
using Softplan.Desafio.Application;
using Softplan.Desafio.Application.UseCases.TaxaJuros;
using System.Threading.Tasks;

namespace Softplan.Desafio.Test.Mocks
{
    public class Api1ClientMock : RestApiClient, IApi1Client
    {
        public Api1ClientMock() : base("http://anything")
        {
        }

        public async Task<TaxaJurosResponse> GetTaxaJuros()
        {
            return await Task.FromResult(new TaxaJurosResponse(0.01m));
        }
    }
}
