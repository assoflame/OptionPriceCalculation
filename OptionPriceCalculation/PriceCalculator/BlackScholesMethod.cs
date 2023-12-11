using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionPriceCalculation.PriceCalculator
{
    internal class BlackScholesMethod : OptionPriceCalculationMethod
    {
        public override double Calculate(OptionParams optParams)
        {
            var distribution = new Normal();

            double d1 = (Math.Log(optParams.BasePrice / optParams.StrikePrice)
                    + (optParams.RiskFreeRate + Math.Pow(optParams.Volatility, 2) / 2) * optParams.Time)
                / optParams.Volatility / Math.Sqrt(optParams.Time);

            double d2 = d1 - optParams.Volatility * Math.Sqrt(optParams.Time);

            return optParams.BasePrice * distribution.CumulativeDistribution(d1)
                - optParams.StrikePrice * Math.Exp(-optParams.RiskFreeRate * optParams.Time)
                    * distribution.CumulativeDistribution(d2);
        }
    }
}
