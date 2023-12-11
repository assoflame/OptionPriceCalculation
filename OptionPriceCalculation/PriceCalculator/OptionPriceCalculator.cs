namespace OptionPriceCalculation.PriceCalculator
{
    internal class OptionPriceCalculator
    {
        private OptionPriceCalculationMethod calcMethod { get; set; }
        private OptionParams optionParams { get; set; }

        public OptionPriceCalculator(OptionPriceCalculationMethod calcMethod,
            OptionParams optionParams)
        {
            this.calcMethod = calcMethod;
            this.optionParams = optionParams;
        }

        public double Calculate() => calcMethod.Calculate(optionParams);

        public void SetMethod(OptionPriceCalculationMethod calcMethod)
            => this.calcMethod = calcMethod;

        public void SetOptionParams(OptionParams optionParams)
            => this.optionParams = optionParams;
    }
}
