using System;
using System.Collections.Generic;
using System.Text;

namespace EFModel
{
    public class DropdownField<KType, VType>
    {
        public KType Key { get; set; }
        public VType Value { get; set; }
        public bool Selected { get; set; }
    }
}
