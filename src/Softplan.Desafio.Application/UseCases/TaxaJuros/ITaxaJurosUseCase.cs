using System.Threading.Tasks;

namespace Softplan.Desafio.Application.UseCases.TaxaJuros
{
    public interface ITaxaJurosUseCase
    {
        TaxaJurosResponse GetTaxaJuros();
        Task<CalcularTaxaJurosResponse> CalcularJurosCompostos(decimal valorInicial, int tempo);
    }
}