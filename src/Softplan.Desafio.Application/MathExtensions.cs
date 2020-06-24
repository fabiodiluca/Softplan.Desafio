using System;

namespace Softplan.Desafio.Application
{
    public static class MathExtensions
    {
        public static decimal Truncate2DecimalPlaces(decimal value)
        {
            return Math.Truncate(value * 100m) / 100m;
        }
    }
}
