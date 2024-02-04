using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gotchi.Core.Repository;

[AttributeUsage (AttributeTargets.Property,
    AllowMultiple = false,
    Inherited = true)]
public class CoreId : Attribute
{
}


[AttributeUsage(AttributeTargets.Field)]
public class CoreIdOverride : Attribute
{
}
