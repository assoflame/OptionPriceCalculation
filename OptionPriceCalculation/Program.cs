using ScottPlot;
using Statistics;
using System.Diagnostics;
using System.Drawing;

namespace OptionPriceCalculation
{
    class Program
    {

        static void Main(string[] args)
        {
            var pricesPlot = new Plot();

            var optionParams = new OptionParams
            {
                StrikePrice = 100,
                Time = 1,
                BasePrice = 100,
                Volatility = 0.3,
                RiskFreeRate = 0.06
            };
            int calculationsCount = 250;

            var dataByMonteCarlo = GetDataByMonteCarlo(optionParams, calculationsCount);
            var priceByBlackScholes = GetPriceByBlackScholes(optionParams);

            pricesPlot.AddScatter(dataByMonteCarlo.iterationCounts,
                        dataByMonteCarlo.optionPrices,
                        label: "Monte-Carlo method");

            pricesPlot.AddScatter(
                        new double[] { dataByMonteCarlo.iterationCounts[0],
                                dataByMonteCarlo.iterationCounts[calculationsCount - 1]},
                        Enumerable.Repeat(priceByBlackScholes, 2).ToArray(),
                        label: "Black-Scholes",
                        color: Color.Red);

            pricesPlot.XLabel("Iterations count");
            pricesPlot.YLabel("Option price");
            pricesPlot.SaveFig("Price.png");



            var errorsPlot = new Plot();
            var errors = dataByMonteCarlo.optionPrices
                .Select(price => Math.Abs(price - priceByBlackScholes))
                .ToArray();

            errorsPlot.AddScatter(dataByMonteCarlo.iterationCounts, errors);

            errorsPlot.XLabel("Iterations count");
            errorsPlot.YLabel("Error");
            errorsPlot.SaveFig("Error.png");



            OpenImage("Error.png");
            OpenImage("Price.png");
        }

        private static (double[] iterationCounts, double[] optionPrices) GetDataByMonteCarlo
            (OptionParams optParams, int calcCount)
        {
            var iterationCounts = new double[calcCount];
            for(int i = 0; i < calcCount; ++i)
                iterationCounts[i] = 1000 * (i + 1);

            var optionPrices = new double[calcCount];

            var calculator = new OptionPriceCalculator(
                    new MonteCarloMethod(1), optParams
                    );

            for (int i = 0; i < calcCount; ++i)
            {
                calculator.SetMethod(new MonteCarloMethod((int)iterationCounts[i]));
                optionPrices[i] = calculator.Calculate();
            }

            return (iterationCounts, optionPrices);
        }

        private static double GetPriceByBlackScholes(OptionParams optParams)
        {
            var calculator = new OptionPriceCalculator(new BlackScholesMethod(), optParams);

            return calculator.Calculate();
        }

        static void OpenImage(string path)
        {
            Process.Start("explorer.exe", path);
        }
    }
}
