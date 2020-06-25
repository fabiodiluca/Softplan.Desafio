using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Softplan.Desafio.Application.UseCases.TaxaJuros;
using System.Threading.Tasks;
using Xunit;

namespace Softplan.Desafio.Test.IntegrationTests
{
    public class IntegrationTests :
        IClassFixture<Api1ApplicationFactory<StartupApi1>>,
        IClassFixture<Api2ApplicationFactory<StartupApi1>>
    {
        private readonly Api1ApplicationFactory<StartupApi1> _api1Factory;
        private readonly Api2ApplicationFactory<StartupApi1> _api2Factory;

        public IntegrationTests(
              Api1ApplicationFactory<StartupApi1> api1Factory
            , Api2ApplicationFactory<StartupApi1> api2Factory
        )
        {
            _api1Factory = api1Factory;
            _api2Factory = api2Factory;
        }

        [Fact]
        public async Task Api1TaxaJurosMustBe1Percent()
        {
            var client = _api1Factory
                .CreateClient();
            var response = await client.GetAsync("taxaJuros");
            var taxaJuros = JsonConvert.DeserializeObject<TaxaJurosResponse>(await response.Content.ReadAsStringAsync());
            Assert.Equal(0.01m, taxaJuros.TaxaJuros);
        }

        [Fact]
        public async Task calculaJurosValorInicial100Meses5()
        {
            _api2Factory.SetApi1HttpClient(_api1Factory.CreateClient());
            var client = _api2Factory.CreateClient();
            var response = await client.GetAsync("calculajuros?valorinicial=100&meses=5");
            var calculado = JsonConvert.DeserializeObject<CalcularTaxaJurosResponse>(await response.Content.ReadAsStringAsync());
            Assert.Equal(105.1m, calculado.Resultado);
        }

    }
}

