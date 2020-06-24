using Newtonsoft.Json;
using Softplan.Desafio.Application.UseCases.TaxaJuros;
using System.Threading.Tasks;
using Xunit;

namespace Softplan.Desafio.Test.IntegrationTests
{
    public class IntegrationTests :
        IClassFixture<Api1ApplicationFactory<Api1.Startup>>,
        IClassFixture<Api2ApplicationFactory<Api2.Startup>>
    {
        private readonly Api1ApplicationFactory<Api1.Startup> _api1Factory;
        private readonly Api2ApplicationFactory<Api2.Startup> _api2Factory;

        public IntegrationTests(
              Api1ApplicationFactory<Api1.Startup> api1Factory
            , Api2ApplicationFactory<Api2.Startup> api2Factory
        )
        {
            _api1Factory = api1Factory;
            _api2Factory = api2Factory;
        }

        [Fact]
        public async Task Api1TaxaJurosMustBe1Percent()
        {
            var client = _api1Factory.CreateClient();
            var response = await client.GetAsync("taxaJuros");
            var taxaJuros = JsonConvert.DeserializeObject<TaxaJurosResponse>(await response.Content.ReadAsStringAsync());
            Assert.Equal(0.01m, taxaJuros.TaxaJuros);
        }

        //[Fact]
        //public async Task Api2TaxaJurosMustBe1Percent()
        //{
        //    var client = _api2Factory.CreateClient();
        //    var response = await client.GetAsync("taxaJuros");
        //    var taxaJuros = JsonConvert.DeserializeObject<TaxaJurosResponse>(await response.Content.ReadAsStringAsync());
        //    Assert.Equal(0.01m, taxaJuros.TaxaJuros);
        //}

    }
}

