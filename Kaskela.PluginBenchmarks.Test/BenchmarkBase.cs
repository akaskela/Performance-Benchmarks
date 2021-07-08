using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaskela.PluginBenchmarks.Test
{
    public class BenchmarkBase
    {
        protected decimal CalculateStandardDeviation(List<decimal> input)
        {
            decimal meanToUse = input.Average(i => (decimal)i);

            if (input.Count == 1)
            {
                return 0;
            }
            else
            {
                decimal sumOfDifferences = 0;
                foreach (decimal value in input)
                {
                    decimal difference = (value - meanToUse);
                    sumOfDifferences += difference * difference;
                }
                decimal variance = sumOfDifferences / (input.Count - 1);
                return (decimal)Math.Sqrt((double)variance);
            }
        }
        protected decimal CalculateMedian(List<decimal> input)
        {
            List<decimal> values = input.OrderBy(n => n).ToList();
            decimal returnValue = 0;
            int numberCount = values.Count();
            int halfIndex = values.Count() / 2;
            if ((numberCount % 2) == 0)
            {
                decimal element1 = values.ElementAt(halfIndex);
                decimal element2 = values.ElementAt(halfIndex - 1);
                returnValue = (element1 + element2) / 2;
            }
            else
            {
                returnValue = values.ElementAt(halfIndex);
            }
            return returnValue;
        }
        protected decimal CalculateMedian(List<int> input)
        {
            return this.CalculateMedian(input.Select(i => (decimal)i).ToList());
        }
    }
}
