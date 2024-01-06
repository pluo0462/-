using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 星图
{
    internal class Star : INotifyPropertyChanged
    {
        private string _name = string.Empty;
        private string _description = string.Empty;
        public string Name 
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        
        }
        public string Description 
        {
            get
            {
                return _description;
            }
            set {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }
        } 
        public HashSet<Star> Neighbors { get; } = [];

        // The Coordinate property should be used in map drawing only!
        public System.Windows.Point Coordinate { get; set; }

        public bool Visited { get; set; } = false;

        public Star() {}
        public Star(string name, string description)
        {
            Name = name;
            Description = description;

            Map.Stars.Add(this);
        }

        public static void ConnectStar(Star A, Star B)
        {
            A.Neighbors.Add(B);
            B.Neighbors.Add(A);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
