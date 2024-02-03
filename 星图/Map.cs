using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace 星图
{
    /// <summary>
    /// Global variables for map display. 
    /// </summary>
    [DataContract]
    [KnownType(typeof(Star))]
    [KnownType(typeof(Lane))]
    [KnownType(typeof(List<Star>))]
    [KnownType(typeof(List<Lane>))]
    internal class Map
    {
        #region 全局属性
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

        public enum LaneType
        { 
            HyperLane,
            JumpGate,
            PsychicSpace,
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
        /// <summary>
        /// Should interpreted as:
        ///     Index: LaneType
        ///     Value: Chance to be that LaneType if generated in random
        /// </summary>
        public static List<double> LaneTypeChance { get; } = [0, 1, 0];
        /// <summary>
        /// Should interpreted as:
        ///     Index: Difficulty Level
        ///     Value: Chance for hyperlane to have that difficulty level if generated in random
        /// </summary>
        public static List<double> LaneDifficultyChance { get; } = [1, 0, 0, 0];
        #endregion

        [DataMember]
        internal HashSet<Star> Stars { get; } = [];
        [DataMember]
        internal HashSet<Lane> Lanes { get; } = [];

        internal Dictionary<string, Star> StarDict { get; } = [];

        internal void AddStar(Star star)
        {
            Stars.Add(star);
            StarDict[star.Name] = star;
        }

        /// <summary>
        /// If a Star is deleted, then all Lane it has will be deleted, too.
        /// </summary>
        /// <param name="star"></param>
        internal void RemoveStar(Star star)
        {
            Stars.Remove(star);
            StarDict.Remove(star.Name);

            foreach(Lane l in star.Neighbors)
            {
                Star neighborStar = l.Endpoint1 == star.Name ? StarDict[l.Endpoint2] : StarDict[l.Endpoint1];
                neighborStar.Neighbors.Remove(l);
                Lanes.Remove(l);
            }
        }

        internal void RemoveStar(string starName)
        {
            Star star = StarDict[starName];
            Stars.Remove(star);
            StarDict.Remove(starName);

            foreach (Lane l in star.Neighbors)
            {
                Star neighborStar = l.Endpoint1 == star.Name ? StarDict[l.Endpoint2] : StarDict[l.Endpoint1];
                neighborStar.Neighbors.Remove(l);
                Lanes.Remove(l);
            }
        }

        internal void RemoveLane(Lane lane)
        {
            Star? endStar1 = lane.EndStar1;
            Star? endStar2 = lane.EndStar2;

            if (endStar1 == null || endStar2 == null)
            {
                throw new Exception("Invalid Operation: Try to remove a uninitialized lane");
            }
            
            endStar1.Neighbors.Remove(lane);
            endStar2.Neighbors.Remove(lane);
            Lanes.Remove(l);
        }

        internal void AddLane(Lane lane)
        {

        }

        public void Clean()
        {
            foreach (Star s in Stars)
            {
                s.Visited = false;
            }
        }

        public void WriteMap()
        {
            DataContractSerializer starSerializers = new(typeof(Star), new Type[] { typeof(List<Star>) });
            using (FileStream fs = new(@"save\stars.xml", FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (XmlDictionaryWriter xdw = XmlDictionaryWriter.CreateTextWriter(fs, Encoding.UTF8))
                {
                    starSerializers.WriteObject(xdw, Stars);
                }
            }
            DataContractSerializer laneSerializers = new(typeof(Lane), new Type[] {typeof(List<Lane>) });
            using (FileStream fs = new(@"save\lanes.xml", FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (XmlDictionaryWriter xdw = XmlDictionaryWriter.CreateTextWriter(fs, Encoding.UTF8))
                {
                    laneSerializers.WriteObject(xdw, Lanes);
                }
            }
        }
    }
}
