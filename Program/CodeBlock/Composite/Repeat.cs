using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P2;

internal class Repeat : Composite
{
    int value;
    CodeBlock? repeatedBlock;


    internal Repeat(Executor executor, int amount, CodeBlock? repeatedBlock) : base(executor)
    {
        value = amount;
        this.repeatedBlock = repeatedBlock;
    }

    internal override void Execute()
    {
        executor.Execute((Field f) =>
        {
            for (int i = 0; i < value; i++)
            {
                repeatedBlock.Execute();
            }
        });
        base.Execute();
    }

    internal override Statistics GetStatistics()
    {
        if (repeatedBlock == null)
            return base.GetStatistics();

        Statistics rbStatistics = repeatedBlock.GetStatistics();
        rbStatistics = new Statistics(rbStatistics.depth+1, rbStatistics.count);

        return rbStatistics + base.GetStatistics();
    }
}
