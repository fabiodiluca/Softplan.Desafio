using Microsoft.AspNetCore.Mvc;
using Softplan.Desafio.Response;

namespace Softplan.Desafio.Controllers
{
    public abstract class ApiController : ControllerBase
    {
        protected Presenter _presenter;

        public ApiController(Presenter presenter)
        {
            _presenter = presenter;
        }
    }
}
