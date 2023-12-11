using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics
{
    internal class OptionParams
    {
        public double BasePrice { get; set; }
        public double StrikePrice { get; set; }
        public double RiskFreeRate { get; set; }
        public double Volatility { get; set; }
        public double Time { get; set; }
    }
}
