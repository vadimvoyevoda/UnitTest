using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculatorKata
{
    public class StringCalcClass
    {
        public int Add(string numbers)
        {
            if(string.IsNullOrEmpty(numbers))
                return 0;
            
            var delimiters = new List<string> { ",", "\n" };
            if (numbers.StartsWith("//"))
            {
                var thisStringDelimiters = numbers.Substring(2, numbers.IndexOf("\n") - 2)
                    .Split(new string[]{"[", "]"}, StringSplitOptions.RemoveEmptyEntries);

                delimiters.AddRange(thisStringDelimiters);
                numbers = numbers.Substring(numbers.IndexOf("\n")+1);                
            }

            var num = numbers.Split(delimiters.ToArray(),StringSplitOptions.RemoveEmptyEntries)
                .Select(x => Convert.ToInt32(x));

            var negatives = num.Where(n => n < 0);
            if (negatives.Any())
            {
                throw new ArgumentException("negatives not allowed: " + 
                String.Join(",",negatives));
            }
            return num.Where(n => n < 1000).Sum();
        }

        //int number;
        //if (Int32.TryParse(numbers, out number))
        //{
        //    return number;
        //}

        //int one = (int)Char.GetNumericValue(numbers[0]);
        //int two = (int)Char.GetNumericValue(numbers[2]);

    }
}
