using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P2;

internal abstract class CodeBlock
{
    internal CodeBlock? nextCommand = null;
    protected Executor executor;



    internal CodeBlock(Executor e)
    {
        executor = e;
    }

    virtual internal void Execute()
    {
        if (nextCommand != null)
        {
            nextCommand.Execute();
        }
    }

    internal virtual Statistics GetStatistics()
    {
        if (nextCommand == null)
            return new Statistics(0, 1);

        return nextCommand.GetStatistics()+new Statistics(0,1);
    }


}
