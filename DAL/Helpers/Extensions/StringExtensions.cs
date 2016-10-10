using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Helpers.Extensions
{
    public static class StringExtensions
    {
        public static Dictionary<int, int> AllIndexesOf(this string str, string value)
        {
            List<string> neighbors = new List<string>()
            {
                string.Empty,
                " ",
                ".",
                ",",
                "?",
                "!",
                @"\",
                "'",
                "\r",
                "\n",
            };

            if (String.IsNullOrEmpty(value))
            {
                throw new ArgumentException("the string to find may not be empty", "value");
            }

            Dictionary<int, int> indexes = new Dictionary<int, int>();
            for (int index = 0; ; index += value.Length)
            {
                index = str.IndexOf(value, index);
                if (index == -1)
                    return indexes;
                
                var leftNeighbor = (index > 0) ? str.Substring(index - 1, 1) : string.Empty;
                
                var rightNeighbor = (index+ value.Length -1 < str.Length -1) ? str.Substring(index + value.Length, 1): String.Empty;

                if (neighbors.Contains(leftNeighbor) && neighbors.Contains(rightNeighbor))
                {
                    indexes.Add(index, index + value.Length);
                }

            }
        }
    }
}
