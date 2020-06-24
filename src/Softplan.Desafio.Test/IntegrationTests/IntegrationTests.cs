using Newtonsoft.Json;
using Softplan.Desafio.Application.UseCases.TaxaJuros;
using System.Threading.Tasks;
using Xunit;

namespace Softplan.Desafio.Test.IntegrationTests
{
    public class IntegrationTests :
        IClassFixture<Api1ApplicationFactory<Api1.Startup>>
    {
        private readonly Api1ApplicationFactory<Api1.Startup>
            _factory;

        public IntegrationTests(
            Api1ApplicationFactory<Api1.Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Api1TaxaJurosMustBe1Percent()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("taxaJuros");
            var taxaJuros = JsonConvert.DeserializeObject<TaxaJurosResponse>(await response.Content.ReadAsStringAsync());
            Assert.Equal(0.01m, taxaJuros.TaxaJuros);
        }

    }
}

