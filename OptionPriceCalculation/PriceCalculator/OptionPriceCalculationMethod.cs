using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionPriceCalculation.PriceCalculator
{
    internal abstract class OptionPriceCalculationMethod
    {
        public abstract double Calculate(OptionParams optionParams);
    }
}
