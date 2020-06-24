using Microsoft.AspNetCore.Mvc;
using Softplan.Desafio.Api2.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Softplan.Desafio.Api2.Controllers
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
