using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class Logger
    {
        public  StringBuilder _stringBuilder = new StringBuilder();
        public  void Log(string input)
        {
            _stringBuilder.Append(input);
        }

        public  void LogLine(string input)
        {
            _stringBuilder.AppendLine(input);
        }

        public string GetLogs()
        {
            return _stringBuilder.ToString();
        }
    }
}
