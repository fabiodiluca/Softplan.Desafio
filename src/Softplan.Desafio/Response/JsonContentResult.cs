using Microsoft.AspNetCore.Mvc;

namespace Softplan.Desafio.Response
{
    public sealed class JsonContentResult : ContentResult
    {
        public JsonContentResult()
        {
            ContentType = "application/json";
        }
    }
}
