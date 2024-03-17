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

        internal Star? Endpoint1_Star
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
                    OnPropertyChanged(nameof(Endpoint1_Star));
                }
                else if (_endpoint1_star != value && Endpoint2_Star != value)
                {
                    Star previousEnd = _endpoint1_star;
                    Star newEnd = value;

                    previousEnd.Neighbors.Remove(this);
                    newEnd.Neighbors.Add(this);

                    _endpoint1_star = value;
                    OnPropertyChanged(nameof(Endpoint1_Star));
                }
            }
        }

        internal Star? Endpoint2_Star
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
                    OnPropertyChanged(nameof(Endpoint2_Star));
                }
                else if (_endpoint2_star != value && Endpoint1_Star != value)
                {
                    Star previousEnd = _endpoint2_star;
                    Star newEnd = value;

                    previousEnd.Neighbors.Remove(this);
                    newEnd.Neighbors.Add(this);

                    _endpoint2_star = value;
                    OnPropertyChanged(nameof(Endpoint2_Star));
                }
            }
        }

        public Lane()
        {
            RandomHyperLane();

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

        public Lane(Star endpoint1_star, Star endpoint2_star)
        {
            RandomHyperLane();
            Endpoint1_Star = endpoint1_star;
            Endpoint2_Star = endpoint2_star;
        }

        internal void RandomLane()
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

        internal void RandomHyperLane()
        {
            Type = LaneType.HyperLane;
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
