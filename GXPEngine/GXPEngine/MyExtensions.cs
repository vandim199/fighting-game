using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace ExtensionMethods
{
    public static class MyExtensions
    {
        public static string Between(this string value, string startingWith, string endingWith)
        {
            int start = value.IndexOf(startingWith);
            int end = value.IndexOf(endingWith, start + startingWith.Length);
            if (start < 0 || end < 0) return "";
            string s = value.Substring(start + startingWith.Length, end - start - startingWith.Length);
            return s;
        }
    }
}