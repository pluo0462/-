using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 星图
{
    internal class PointComp : Comparer<System.Windows.Point>
    {
        public override int Compare(System.Windows.Point p1, System.Windows.Point p2)
        {
            if (p1.X < p2.X)
            {
                return -1;
            }

            if (p1.X > p2.X)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
