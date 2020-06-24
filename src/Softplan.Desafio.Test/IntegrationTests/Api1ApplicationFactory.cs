using Microsoft.AspNetCore.Mvc.Testing;

namespace Softplan.Desafio.Test.IntegrationTests
{
    public class Api1ApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup : class
    {

    }
}
