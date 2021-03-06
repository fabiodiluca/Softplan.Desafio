using Newtonsoft.Json;
using Softplan.Desafio.Api1.Controllers;
using Softplan.Desafio.Application.UseCases.TaxaJuros;
using Softplan.Desafio.Response;
using Xunit;

namespace Softplan.Desafio.Test
{
    public class Api1UnitTests
    {
        [Fact]
        public void taxaJurosMustBe1Percent()
        {
            var controller = new HomeController(new TaxaJurosUseCase(null), new Presenter());
            var result = (JsonContentResult)controller.taxaJuros();
            var reponse = JsonConvert.DeserializeObject<TaxaJurosResponse>(result.Content);

            Assert.Equal(200, result.StatusCode);
            Assert.Equal(0.01m, reponse.TaxaJuros);
        }
    }
}
