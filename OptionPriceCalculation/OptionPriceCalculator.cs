namespace Statistics
{
    internal class OptionPriceCalculator
    {
        private OptionPriceCalculationMethod calcMethod { get; set; }
        public OptionParams OptionParams { get; set; }

        public OptionPriceCalculator(OptionPriceCalculationMethod calcMethod,
            OptionParams optionParams)
        {
            this.calcMethod = calcMethod;
            OptionParams = optionParams;
        }

        public double Calculate() => calcMethod.Calculate(OptionParams);

        public void SetMethod(OptionPriceCalculationMethod calcMethod)
            => this.calcMethod = calcMethod;
    }
}
