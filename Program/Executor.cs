using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P2;

internal class Executor
{
    Field field;

    internal Executor(Field field)
    {
        this.field = field;
    }

    internal void Execute(Action<Field> action)
    {
        action(field);
    }

    


}
