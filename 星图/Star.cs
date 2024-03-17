using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace 星图
{
    [DataContract] 
    public class Star : INotifyPropertyChanged, IExtensibleDataObject
    {
        #region Fields
        private string _name = string.Empty;
        private string _description = string.Empty;
        private StarType _starType;
        #endregion
        [DataMember]
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

        [DataMember]
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

        [DataMember]
        public StarType StarType
        {
            get 
            { 
                return _starType; 
            }
            set
            {
                if (_starType != value)
                {
                    _starType = value;
                    OnPropertyChanged(nameof(StarType));
                }
            }
        }

        public ObservableCollection<Lane> Lanes { get; } = [];
        
        // The Coordinate property should be used in map drawing only!
        public System.Windows.Point Coordinate { get; set; }

        /// <summary>
        /// 用于图论算法的property。禁止用于其他目的
        /// </summary>
        public bool Visited { get; set; } = false;

        /// <summary>
        /// 默认constructor。
        /// 用于构建尚未探索的星球
        /// </summary>
        public Star() {}
        /// <summary>
        /// 用于构建开局已知的星球
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        public Star(string name, string description)
        {
            Name = name;
            Description = description;
        }
        public Star(string name, string description, StarType starType)
        {
            Name = name;
            Description = description;
            StarType = starType;
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
