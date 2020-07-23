using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Attributes
{
    public class EntityFieldAttribute : Attribute
    {
        public EntityFieldAttribute(bool ignore)
        {
            Ignore = ignore;
        }

        public bool Ignore { get;  set; }
    }
}
