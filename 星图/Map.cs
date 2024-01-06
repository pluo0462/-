using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 星图
{
    internal static class Map
    {
        public static List<int> ButtonSize { get; } = [40, 30, 20];
        public static List<int> LineLength { get; } = [180, 90, 30];

        public static List<Star> Stars { get; } = [];

        public static void ConnectStar(Star A, Star B)
        {
            A.Neighbors.Add(B);
            B.Neighbors.Add(A);
        }

        public static void Clean()
        {
            foreach (Star s  in Stars)
            {
                s.Visited = false;
            }
        }
    }
}
