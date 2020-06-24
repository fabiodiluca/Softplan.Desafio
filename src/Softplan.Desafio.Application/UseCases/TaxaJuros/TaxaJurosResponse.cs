namespace Softplan.Desafio.Application.UseCases.TaxaJuros
{
    public class TaxaJurosResponse: UseCaseResponseMessageBase
    {
        public decimal TaxaJuros { get; set; }

        public TaxaJurosResponse() { }
        public TaxaJurosResponse(decimal taxaJuros)
        {
            this.TaxaJuros = taxaJuros;
        }
    }
}
