using Softplan.Desafio.UseCase;

namespace Softplan.Desafio.Application.UseCases.TaxaJuros
{
    public class CalcularTaxaJurosResponse : UseCaseResponseMessageBase
    {
        public decimal Resultado { get; set; }

        public CalcularTaxaJurosResponse() { }
        public CalcularTaxaJurosResponse(decimal resultado)
        {
            this.Resultado = resultado;
        }
    }
}
