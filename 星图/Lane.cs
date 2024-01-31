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
        private Map.LaneType _type;
        private int _difficulty;
        private string _endpoint1 = string.Empty;
        private string _endpoint2 = string.Empty;
        private bool _explored;
        #endregion
        [DataMember]
        public Map.LaneType Type
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

        public Lane()
        {
            var random = new Random();
            double p = random.NextDouble();

            if (p < 0.36)
            {
                Type = Map.LaneType.HyperLane;
                Difficulty = 1;
            }
            else if (p < 0.63)
            {
                Type = Map.LaneType.HyperLane;
                Difficulty = 2;
            }
            else if (p < 0.81)
            {
                Type = Map.LaneType.HyperLane;
                Difficulty = 3;
            }
            else if (p < 0.9)
            {
                Type = Map.LaneType.HyperLane;
                Difficulty = 4;
            }
            else
            {
                Type = Map.LaneType.JumpGate;
                Difficulty = 0;
            }
        }

        public Lane(Map.LaneType pathType)
        {
            switch (pathType)
            {
                case Map.LaneType.HyperLane:
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

                case Map.LaneType.JumpGate:
                    Type = pathType;
                    Difficulty = 0;
                    break;
                case Map.LaneType.PsychicSpace:
                    Type = pathType;
                    Difficulty = 0;
                    break;
                default:
                    throw new NotImplementedException("Invalid path type.");
            }
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
