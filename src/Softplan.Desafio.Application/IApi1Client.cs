using Softplan.Desafio.Application.UseCases.TaxaJuros;
using System.Threading.Tasks;

namespace Softplan.Desafio.Application
{
    public interface IApi1Client
    {
        Task<TaxaJurosResponse> GetTaxaJuros();
    }
}