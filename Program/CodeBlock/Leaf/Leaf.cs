using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P2;

internal abstract class Leaf : CodeBlock
{
    internal Leaf(Executor executor) : base(executor) { }
}
