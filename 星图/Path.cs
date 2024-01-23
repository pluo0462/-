using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 星图
{
    internal class Path : INotifyPropertyChanged
    {
        public enum PathTypes
        {
            HyperLane = 0,
            JumpGate = 1,
            PsychicSpace = 2,
        }
        private PathTypes _type;
        private int _difficulty;
        private bool _explored;

        public PathTypes Type
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

        public Path()
        {
            var random = new Random();
            double p = random.NextDouble();

            if (p < 0.36)
            {
                Type = PathTypes.HyperLane;
                Difficulty = 1;
            }
            else if (p < 0.63)
            {
                Type = PathTypes.HyperLane;
                Difficulty = 2;
            }
            else if (p < 0.81)
            {
                Type = PathTypes.HyperLane;
                Difficulty = 3;
            }
            else if (p < 0.9)
            {
                Type = PathTypes.HyperLane;
                Difficulty = 4;
            }
            else
            {
                Type = PathTypes.JumpGate;
                Difficulty = 0;
            }
        }

        public Path(PathTypes pathType)
        {
            switch (pathType)
            {
                case PathTypes.HyperLane:
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

                case PathTypes.JumpGate:
                    Type = pathType;
                    Difficulty = 0;
                    break;
                case PathTypes.PsychicSpace:
                    Type = pathType;
                    Difficulty = 0;
                    break;
                default:
                    throw new NotImplementedException("Invalid path type.");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
