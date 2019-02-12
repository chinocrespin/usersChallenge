using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Common.Extensions
{
    public static class StringExtensions
    {
        public static bool Contains(this string s, string[] elements)
        {
            foreach (var element in elements)
            {
                if (s.Contains(element)) return true;
            }
            return false;
        }
    }
}
