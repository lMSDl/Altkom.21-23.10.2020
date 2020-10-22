using ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Extensions
{
    public static class StringExtensions
    {
        //public static Nullable<Commands> ToCommand(string input)
        public static Commands? ToCommand(this string input)
        {
            if (Enum.TryParse(input, true, out Commands command))
                return command;
            return null;
        }

        public static bool EqualsCaseIgnore(this string input, string value)
        {
            return string.Compare(input, value, ignoreCase: true) == 0;
        }
    }
}
