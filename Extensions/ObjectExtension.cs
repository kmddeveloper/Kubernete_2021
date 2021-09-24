using System;
using System.Collections.Generic;
using System.Text;

namespace Extensions
{
    public static class ObjectExtension
    {
        public static int ToInt32(this Object o)
        {
            int val = 0;
            if (o != null && Int32.TryParse(o.ToString(), out val))
                return val;
            return 0;

        }
    }
}
