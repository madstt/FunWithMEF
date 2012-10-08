using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Globalization;
using System.Linq;

namespace FunWithMEF
{
    [Export(typeof(ICalculator))]
    class Calculator : ICalculator
    {
        [ImportMany]
        IEnumerable<Lazy<IOperation, IOperationData>> operations; 

        public string Calculate(string input)
        {
            int left;
            int right;

            var fn = FindFirstNonDigit(input);
            if (fn < 0)
            {
                return "Could not parse command.";
            }

            try
            {
                left = int.Parse(input.Substring(0, fn));
                right = int.Parse(input.Substring(fn + 1));
            }
            catch (Exception)
            {
                return "Could not parse command.";
            }

            var operation = input[fn];

            foreach (var i in operations.Where(i => i.Metadata.Symbol.Equals(operation)))
            {
                return i.Value.Operate(left, right).ToString(CultureInfo.InvariantCulture);
            }

            return "Operation Not Found!";
        }

        private int FindFirstNonDigit(String s)
        {

            for (int i = 0; i < s.Length; i++)
            {
                if (!(Char.IsDigit(s[i]))) return i;
            }
            return -1;
        }
    }
}