using Newtonsoft.Json;
using Softplan.Desafio.Api1.Controllers;
using Softplan.Desafio.Api1.Response;
using Softplan.Desafio.Application.UseCases.TaxaJuros;
using System;
using Xunit;

namespace Softplan.Desafio.Test
{
    public class Api1UnitTest
    {
        [Fact]
        public void taxaJurosMustBe1Percent()
        {
            HomeController controller = new HomeController(new TaxaJurosUseCase(null, null), new Api1.Response.Presenter());
            var result = (JsonContentResult) controller.taxaJuros();
            var reponse = JsonConvert.DeserializeObject<TaxaJurosResponse>(result.Content);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(0.01m, reponse.TaxaJuros);
        }
    }
}
