using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P2;

internal class Turn : Leaf
{
    int value;
    internal Turn(Executor executor, int turn) : base(executor)
    {
        value = turn;
    }

    internal override void Execute()
    {
        executor.Execute((Field f) =>
        {
            f.guy.direction = ((f.guy.direction + value) % 4 + 4) %4;

            if (f.log.Length > 0 && f.log.Last() != '\n')
            {
                f.log += ", ";
            }
            switch (value)
            {
                case (-1):
                    f.log += "Turn left";
                    break;
                case (1):
                    f.log += "Turn right";
                    break;
            }


        });
        base.Execute();
    }
}
