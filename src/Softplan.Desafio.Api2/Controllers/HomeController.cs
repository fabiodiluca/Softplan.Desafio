using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Softplan.Desafio.Api2.Response;
using Softplan.Desafio.Application.UseCases.TaxaJuros;
using System.Threading.Tasks;

namespace Softplan.Desafio.Api2.Controllers
{
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


        [Route("calculajuros")]
        [HttpGet]
        public async Task<ActionResult> calculajuros(decimal valorinicial, int meses)
        {
            _presenter.Handler(
                await _taxaJurosUseCase.CalcularJurosCompostos(valorinicial, meses)
            );
            return _presenter.ContentResult;
        }
    }
}