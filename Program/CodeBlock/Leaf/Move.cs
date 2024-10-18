using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P2;

internal class Move : Leaf
{
    int value;
    internal Move(Executor executor, int distance) : base(executor)
    {
        value = distance;
    }

    internal override void Execute()
    {
        executor.Execute((Field f) => 
        { 
            switch (f.guy.direction)
            {
                case (0):
                    f.guy.p = new Point(f.guy.p.X + value, f.guy.p.Y);
                    break;
                case (1):
                    f.guy.p = new Point(f.guy.p.X, f.guy.p.Y + value);
                    break;
                case (2):
                    f.guy.p = new Point(f.guy.p.X - value, f.guy.p.Y);
                    break;
                case (3):
                    f.guy.p = new Point(f.guy.p.X, f.guy.p.Y - value);
                    break;

            }

            if(f.log.Length > 0 && f.log.Last() != '\n')
            {
                f.log += ", ";
            }
            f.log += "Move " + value;

        });
        base.Execute();
    }

}
