using System;

namespace Softplan.Desafio.Application.Exceptions
{
    public class Api1GetTaxaJurosException : Exception
    {
        public Api1GetTaxaJurosException(Exception innerException): 
            base("Não foi possível consultar a taxa de juros na API 1.", innerException)
        {
        }
    }
}
