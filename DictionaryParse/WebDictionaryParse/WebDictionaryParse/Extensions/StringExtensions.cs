using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDictionaryParse.Extensions
{
	public static class StringExtensions
	{
		public static string KeepOnlyAlphabetical(this String str)
		{
			StringBuilder sb = new StringBuilder();
			foreach (char c in str.ToCharArray())
			{
				if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') ||
					c == 194 || c == 226 || // â
					c == 206 || c == 238 || // î 
					c == 258 || c == 259 || // ă
					c == 354 || c == 355 || // ț
					c == 538 || c == 539 || // ț
					c == 218 || c == 219 || // ș
					c == 350 || c == 351 || // ș
					c == 537 || c == 536
					)
				{
					sb.Append(c);
				}
				else
				{
					sb.Append(" ");
				}
			}

			return sb.ToString();
		}
	}
}
