using Newtonsoft.Json;
using Softplan.Desafio.Api2.Controllers;
using Softplan.Desafio.Application.UseCases.TaxaJuros;
using Softplan.Desafio.Response;
using Softplan.Desafio.Test.Mocks;
using System.Threading.Tasks;
using Xunit;

namespace Softplan.Desafio.Test
{
    public class Api2UnitTest
    {
        [Fact]
        public async Task calculaJurosValorInicial100Meses5()
        {
            var api1 = new Api1ClientMock();

            HomeController controller = new HomeController(new TaxaJurosUseCase(api1), new Presenter());
            var result = (JsonContentResult) (await controller.calculajuros(100, 5));
            var reponse = JsonConvert.DeserializeObject<CalcularTaxaJurosResponse>(result.Content);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(105.1m, reponse.Resultado);
        }
    }
}
