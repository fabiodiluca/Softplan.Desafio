using Microsoft.AspNetCore.Mvc;
using Softplan.Desafio.Api1.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Softplan.Desafio.Api1.Controllers
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
