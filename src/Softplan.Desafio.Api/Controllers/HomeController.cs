using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Softplan.Desafio.Api1.Response;
using Softplan.Desafio.Application.UseCases.TaxaJuros;

namespace Softplan.Desafio.Api1.Controllers
{
    /// <summary/>
    [AllowAnonymous]
    public class HomeController : ApiController
    {
        protected readonly ITaxaJurosUseCase _taxaJurosUseCase;
        public HomeController(ITaxaJurosUseCase taxaJurosUseCase, Presenter presenter) : base(presenter)
        {
            _taxaJurosUseCase = taxaJurosUseCase;
        }


        /// <summary/>
        public ActionResult Index() => new RedirectResult("~/swagger");


        [Route("taxaJuros")]
        [HttpGet]
        public ActionResult taxaJuros()
        {
            _presenter.Handler(_taxaJurosUseCase.GetTaxaJuros());
            return _presenter.ContentResult;
        }
    }
}