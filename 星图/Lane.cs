using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace 星图
{
    [DataContract]
    internal class Lane : INotifyPropertyChanged, IExtensibleDataObject
    {
        #region Fields
        private LaneType _type;
        private int _difficulty;
        private string _endpoint1 = string.Empty;
        private string _endpoint2 = string.Empty;
        private Star? _endStar1 = null;
        private Star? _endStar2 = null;
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
        public string Endpoint1
        {
            get
            {
                return _endpoint1;
            }
            set
            {
                if (_endpoint1 != value)
                {
                    _endpoint1 = value;
                    OnPropertyChanged(nameof(Endpoint1));
                }
            }
        }
        [DataMember]
        public string Endpoint2
        {
            get
            {
                return _endpoint2;
            }
            set
            {
                if (_endpoint2 != value)
                {
                    _endpoint2 = value;
                    OnPropertyChanged(nameof(Endpoint2));
                }
            }
        }

        internal Star? EndStar1
        {
            get
            {
                return _endStar1;
            }
            set
            {
                if (value == null) { }
                else if (_endStar1 == null)
                {
                    _endStar1 = value;
                    OnPropertyChanged(nameof(EndStar1));
                }
                else if (_endStar1 != value && EndStar2 != value)
                {
                    Star previousEnd = _endStar1;
                    Star newEnd = value;

                    previousEnd.Neighbors.Remove(this);
                    newEnd.Neighbors.Add(this);

                    _endStar1 = value;
                    OnPropertyChanged(nameof(EndStar1));
                }
            }
        }

        internal Star? EndStar2
        {
            get
            {
                return _endStar2;
            }
            set
            {
                if (value == null) { }
                else if (_endStar2 == null)
                {
                    _endStar2 = value;
                    OnPropertyChanged(nameof(EndStar2));
                }
                else if (_endStar2 != value && EndStar1 != value)
                {
                    Star previousEnd = _endStar2;
                    Star newEnd = value;

                    previousEnd.Neighbors.Remove(this);
                    newEnd.Neighbors.Add(this);

                    _endStar2 = value;
                    OnPropertyChanged(nameof(EndStar2));
                }
            }
        }

        public Lane()
        {
            var random = new Random();
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
                Difficulty = 0;
            }
        }

        public Lane(LaneType pathType)
        {
            switch (pathType)
            {
                case LaneType.HyperLane:
                    Type = pathType;
                    var random = new Random();
                    double p = random.NextDouble();
                    if (p < 0.4)
                    {
                        Difficulty = 1;
                    }
                    else if (p < 0.7)
                    {
                        Difficulty = 2;
                    }
                    else if (p < 0.9)
                    {
                        Difficulty = 3;
                    }
                    else 
                    {
                        Difficulty = 4;
                    }
                    break;

                case LaneType.JumpGate:
                    Type = pathType;
                    Difficulty = 0;
                    break;
                case LaneType.PsychicSpace:
                    Type = pathType;
                    Difficulty = 0;
                    break;
                default:
                    throw new NotImplementedException("Invalid path type.");
            }
        }

        internal void SwitchEndPoint()
        {

        }

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
