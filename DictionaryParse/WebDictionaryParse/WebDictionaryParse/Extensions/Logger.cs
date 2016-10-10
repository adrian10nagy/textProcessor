using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDictionaryParse.Extensions
{
	public class Logger
	{
		private static string _folder;

		/// <summary>
		/// writes to text file, as loggers
		/// </summary>
		/// <param name="folder">Ex: @"c:\log.txt"</param>
		/// <param name="message"> Any text</param>
		public void Log(string folder, string message)
		{
			_folder = folder;
			Log(message);
		}

		public void Log(string message)
		{
			System.IO.File.AppendAllText(_folder, message);
		}

		public void Log2(string folder, string lines)
		{
			
		  System.IO.StreamWriter file = new System.IO.StreamWriter(folder, true);
		  file.WriteLine(lines);

		  file.Close();

		}
	}
}
