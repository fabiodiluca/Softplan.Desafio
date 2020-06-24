using System.Globalization;

namespace Softplan.Desafio
{
    public static class Culture
    {
        public static CultureInfo CultureBr = new CultureInfo("pt-BR");
        public static CultureInfo CultureEn = new CultureInfo("en-US");

        public static string CultureBrFormat(decimal value)
        {
            return string.Format(CultureBr, "{0}", value);
        }
    }
}
