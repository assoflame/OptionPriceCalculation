using MathNet.Numerics.Distributions;

namespace OptionPriceCalculation.PriceCalculator
{
    internal class MonteCarloMethod : OptionPriceCalculationMethod
    {
        public int IterationsCount { get; set; }

        public MonteCarloMethod(int iterationsCount)
        {
            if (iterationsCount <= 0)
                throw new ArgumentException("Negative iterations count");

            IterationsCount = iterationsCount;
        }

        public override double Calculate(OptionParams optParams)
        {
            var distribution = new Normal();
            var samples = new double[IterationsCount];
            distribution.Samples(samples);

            double sum = 0;

            for (int i = 0; i < samples.Length; ++i)
                sum += GetOptionPrice(samples[i], optParams);

            return Math.Exp(-optParams.RiskFreeRate * optParams.Time) * (sum / IterationsCount);
        }

        private static double GetActivePrice(double sample, OptionParams optParams)
        {
            return optParams.BasePrice
                * Math.Exp((optParams.RiskFreeRate - Math.Pow(optParams.Volatility, 2) / 2) * optParams.Time
                            + optParams.Volatility * Math.Sqrt(optParams.Time) * sample);
        }

        private static double GetOptionPrice(double sample, OptionParams optParams)
            => Math.Max(GetActivePrice(sample, optParams) - optParams.StrikePrice, 0);
    }
}
