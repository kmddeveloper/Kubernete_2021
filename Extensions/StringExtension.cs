using System;
using System.Collections.Generic;
using System.Text;

namespace Extensions
{
    public static class StringExtension
    {
        public static int ToInt32(this string s)
        {
            int value = 0;

            if (Int32.TryParse(s, out value))
                return value;

            return 0;
        }
    }
}
