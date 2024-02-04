using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gotchi.Core.Repository
{
    public class CoreIdAttributeNotFoundException : Exception
    {
        public CoreIdAttributeNotFoundException(object obj)
            : base(
                 $"Object of type : {obj.GetType()}. Has no CoreId Attribute"
                 ) { }
    }
}
