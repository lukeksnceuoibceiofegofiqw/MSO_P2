using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P2;

internal class Statistics
{
    internal int depth;
    internal int count;

    internal Statistics(int depth, int count)
    {
        this.depth = depth;
        this.count = count;
    }

    public static Statistics operator +(Statistics s1, Statistics s2) => new Statistics(Math.Max(s1.depth, s2.depth), s1.count + s2.count);


}
