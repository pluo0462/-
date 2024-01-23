using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 星图
{
    /// <summary>
    /// Global variables for map display. 
    /// </summary>
    internal static class Map
    {

        private const double _basicStarHabitableChance = 0.05;
        public enum StarType
        {
            ClassA,
            ClassB,
            ClassF,
            ClassG,
            ClassK,
            ClassM,
            ClassMRG,
            ClassTBD,
            BlackHole,
            Neutron,
            Pulsar,
        }

        public static List<double> StarTypeChance { get; } = [10, 10, 15, 30, 20, 20, 10, 0.8, 0.4, 0.4];


        public static List<double> StarHabitableChance { get; } = 
            [
                0.6 * _basicStarHabitableChance, 
                0.6 * _basicStarHabitableChance, 
                _basicStarHabitableChance, 
                _basicStarHabitableChance, 
                _basicStarHabitableChance, 
                0.4 * _basicStarHabitableChance,
                0.1 * _basicStarHabitableChance,
                0.4 * _basicStarHabitableChance,
                0,
                0,
                0
            ];


        public static List<Star> Stars { get; } = [];

        public static void Clean()
        {
            foreach (Star s in Stars)
            {
                s.Visited = false;
            }
        }
    }
}
