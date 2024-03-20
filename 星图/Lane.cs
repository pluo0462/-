using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace 星图
{
    [DataContract]
    public class Lane : INotifyPropertyChanged, IExtensibleDataObject
    {
        #region Fields
        private LaneType _type;
        private int _difficulty;
        private string _endpoint1_name = string.Empty;
        private string _endpoint2_name = string.Empty;
        private Star? _endpoint1_star = null;
        private Star? _endpoint2_star = null;
        private bool _explored;
        #endregion

        [DataMember]
        public LaneType Type
        {
            get
            {
                return _type;
            }
            set
            {
                if (_type != value)
                {
                    _type = value;
                    OnPropertyChanged(nameof(Type));
                }
            }
        }

        [DataMember]
        public int Difficulty
        {
            get
            {
                return _difficulty;
            }
            set
            {
                if (_difficulty != value)
                {
                    _difficulty = value;
                    OnPropertyChanged(nameof(Difficulty));
                }
            }
        }

        [DataMember]
        public bool Explored
        {
            get
            {
                return _explored;
            }
            set
            {
                if (_explored != value)
                {
                    _explored = value;
                    OnPropertyChanged(nameof(Explored));
                }
            }
        }

        [DataMember]
        public string Endpoint1_Name
        {
            get
            {
                return _endpoint1_name;
            }
            set
            {
                if (_endpoint1_name != value)
                {
                    _endpoint1_name = value;
                    OnPropertyChanged(nameof(Endpoint1_Name));
                }
            }
        }
        [DataMember]
        public string Endpoint2_Name
        {
            get
            {
                return _endpoint2_name;
            }
            set
            {
                if (_endpoint2_name != value)
                {
                    _endpoint2_name = value;
                    OnPropertyChanged(nameof(Endpoint2_Name));
                }
            }
        }

        public Star? Endpoint1_Star
        {
            get
            {
                return _endpoint1_star;
            }
            set
            {
                if (value == null) { }
                else if (_endpoint1_star == null)
                {
                    _endpoint1_star = value;
                    _endpoint1_star.Lanes.Add(this);

                    OnPropertyChanged(nameof(Endpoint1_Star));
                }
                else if (_endpoint1_star != value)
                {
                    Star previousEnd = _endpoint1_star;
                    Star newEnd = value;

                    previousEnd.Lanes.Remove(this);
                    newEnd.Lanes.Add(this);

                    _endpoint1_star = value;
                    OnPropertyChanged(nameof(Endpoint1_Star));
                }
            }
        }

        public Star? Endpoint2_Star
        {
            get
            {
                return _endpoint2_star;
            }
            set
            {
                if (value == null) { }
                else if (_endpoint2_star == null)
                {
                    _endpoint2_star = value;
                    _endpoint2_star.Lanes.Add(this);

                    OnPropertyChanged(nameof(Endpoint2_Star));
                }
                else if (_endpoint2_star != value /*&& Endpoint1_Star != value*/)
                {
                    Star previousEnd = _endpoint2_star;
                    Star newEnd = value;

                    previousEnd.Lanes.Remove(this);
                    newEnd.Lanes.Add(this);

                    _endpoint2_star = value;
                    OnPropertyChanged(nameof(Endpoint2_Star));
                }
            }
        }

        public Lane(Star originStar)
        {
            RandomLane();
            _endpoint1_star = originStar;
            _endpoint1_star.Lanes.Add(this);
        }

        public Lane(Star endpoint1_star, Star endpoint2_star)
        {
            if (endpoint1_star == endpoint2_star)
            {
                throw new Exception("Self-loop Lane is not allowed");
            }

            RandomLane();
            _endpoint1_star = endpoint1_star;
            _endpoint2_star = endpoint2_star;

            endpoint1_star.Lanes.Add(this);
            endpoint2_star.Lanes.Add(this);
        }

        public Lane(Star endpoint1_star, Star endpoint2_star, LaneType laneType)
        {
            if (endpoint1_star == endpoint2_star)
            {
                throw new Exception("Self-loop Lane is not allowed");
            }

            RandomLane();

            switch (laneType)
            {
                case LaneType.JumpGate:
                    Type = LaneType.JumpGate;
                    break;
                case LaneType.HyperLane:
                    Type = LaneType.HyperLane;
                    Difficulty = 0;
                    break;
                case LaneType.PsychicSpace:
                    Type = LaneType.PsychicSpace;
                    Difficulty = 0;
                    break;
            }

            _endpoint1_star = endpoint1_star;
            _endpoint2_star = endpoint2_star;

            endpoint1_star.Lanes.Add(this);
            endpoint2_star.Lanes.Add(this);
        }

        public void OrganizeEndpoint(Star originStar)
        {
            if (_endpoint1_star != originStar)
            {
                _endpoint2_star = _endpoint1_star;
                _endpoint1_star = originStar;
            }
        }

        #region 用于随机生成的Function

        private void RandomLane()
        {
            Random random = new();
            double p = random.NextDouble();
            if (p < 0.36)
            {
                Type = LaneType.HyperLane;
                Difficulty = 1;
            }
            else if (p < 0.63)
            {
                Type = LaneType.HyperLane;
                Difficulty = 2;
            }
            else if (p < 0.81)
            {
                Type = LaneType.HyperLane;
                Difficulty = 3;
            }
            else if (p < 0.9)
            {
                Type = LaneType.HyperLane;
                Difficulty = 4;
            }
            else
            {
                Type = LaneType.JumpGate;
            }
        }
#endregion

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        #region IExtensibleDataObject Implementation
        private ExtensionDataObject? extensionDataObjectValue;
        public ExtensionDataObject? ExtensionData
        {
            get
            {
                return extensionDataObjectValue;
            }
            set
            {
                extensionDataObjectValue = value;
            }
        }
        #endregion
    }
}
