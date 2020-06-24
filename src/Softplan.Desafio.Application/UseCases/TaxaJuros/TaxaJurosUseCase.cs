using Softplan.Desafio.UseCase;
using System;
using System.Threading.Tasks;

namespace Softplan.Desafio.Application.UseCases.TaxaJuros
{
    public class TaxaJurosUseCase : UseCaseBase, ITaxaJurosUseCase
    {
        protected IApi1Client _apiClient;
        public TaxaJurosUseCase(IApi1Client apiClient) : base()
        {
            _apiClient = apiClient;
        }

        public TaxaJurosResponse GetTaxaJuros()
        {
            return new TaxaJurosResponse(0.01m);
        }

        public async Task<CalcularTaxaJurosResponse> CalcularJurosCompostos(decimal valorInicial, int tempo)
        {
            TaxaJurosResponse taxaJurosResponse = await _apiClient.GetTaxaJuros();

            var resultado = Math.Truncate(valorInicial * (decimal)
                (Math.Pow((1d + (double)taxaJurosResponse.TaxaJuros), tempo) * 100)) 
                / 100m;

            return new CalcularTaxaJurosResponse(resultado);
        }
    }
}
