using System;
using System.Collections.Generic;
using System.Text;

namespace EFModel
{
    public class Field<KTYPE, VTYPE>
    {
        public KTYPE Key { get; set; }
        public VTYPE Value { get; set; }
    }
}
