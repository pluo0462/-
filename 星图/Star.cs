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
    internal class Star : INotifyPropertyChanged, IExtensibleDataObject
    {
        #region Fields
        private string _name = string.Empty;
        private string _description = string.Empty;
        private Map.StarType _starType;
        private bool _explored = false;
        private int _population = 0;
        private int _capital = 0;
        private double _tfp = 0;
        private double _populationGrowthRate = 0;
        private double _saveRate = 0;
        private double _outputElasticity = 0;
        private double _captialDepreciateRate = 0;
        private double _taxRate = 0;
        private bool _habitable = false;
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
        public Map.StarType StarType
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

        //#region Solow Model 相关Properties
        //public int Population
        //{
        //    get
        //    {
        //        return _population;
        //    }
        //    set
        //    {
        //        if (_population != value)
        //        {
        //            _population = value;
        //            OnPropertyChanged(nameof(Population));
        //        }
        //    }
        //}

        //public int Capital
        //{
        //    get { return _capital; }
        //    set
        //    {
        //        if (_capital != value)
        //        {
        //            _capital = value;
        //            OnPropertyChanged(nameof(Capital));
        //        }
        //    }
        //}
        //public double TFP
        //{
        //    get
        //    {
        //        return _tfp;
        //    }
        //    set
        //    {
        //        if (_tfp != value)
        //        {
        //            _tfp = value;
        //            OnPropertyChanged(nameof(TFP));
        //        }
        //    }
        //}
        //public int CapitalPC
        //{
        //    get { 
        //        if (Population == 0)
        //        {
        //            return 0;
        //        }
        //        else
        //        {
        //            return Capital / Population;
        //        }
             
        //        }
        //    set
        //    {
        //        if (CapitalPC != value)
        //        {
        //            Capital = value * Population;
        //            OnPropertyChanged(nameof(CapitalPC));
        //        }
        //    }
        //}
        //public int OutputPC
        //{
        //    get
        //    {
        //        return (int)(CapitalPC * TFP * OutputElasticity);
        //    }
        //}

        //public double PopulationGrowthRate
        //{
        //    get
        //    {
        //        return _populationGrowthRate;
        //    }
        //    set
        //    {
        //        if (value < 0)
        //        {
        //            MessageBox.Show("Invalid Population Growth Rate");
        //        }
        //        else
        //        {
        //            if (_populationGrowthRate != value)
        //            {
        //                _populationGrowthRate = value;
        //                OnPropertyChanged(nameof(PopulationGrowthRate));
        //            }
        //        }
        //    }
        //}
        //public double SaveRate
        //{
        //    get
        //    {
        //        return _saveRate;
        //    }
        //    set
        //    {
        //        if (value < 0)
        //        {
        //            MessageBox.Show("Invalid Save Rate");
        //        }
        //        else
        //        {
        //            if (_saveRate != value)
        //            {
        //                _saveRate = value;
        //                OnPropertyChanged(nameof(SaveRate));
        //            }
        //        }
        //    }
        //}
        //public double OutputElasticity
        //{
        //    get
        //    {
        //        return _outputElasticity;
        //    }
        //    set
        //    {
        //        if (value < 0)
        //        {
        //            MessageBox.Show("Invalid Output's Elasticity");
        //        }
        //        else
        //        {
        //            if (_outputElasticity != value)
        //            {
        //                _outputElasticity = value;
        //                OnPropertyChanged(nameof(OutputElasticity));
        //            }
        //        }
        //    }
        //}
        //public double CaptialDepreciateRate
        //{
        //    get
        //    {
        //        return _captialDepreciateRate;
        //    }
        //    set
        //    {
        //        if (value < 0)
        //        {
        //            MessageBox.Show("Invalid Captial Depreciate Rate");
        //        }
        //        else
        //        {
        //            if (_captialDepreciateRate != value)
        //            {
        //                _captialDepreciateRate = value;
        //                OnPropertyChanged(nameof(CaptialDepreciateRate));
        //            }
        //        }
        //    }
        //}

        //public double TaxRate
        //{
        //    get
        //    {
        //        return _taxRate;
        //    }
        //    set
        //    {
        //        if (value < 0)
        //        {
        //            MessageBox.Show("Invalid Tax Rate");
        //        }
        //        else
        //        {
        //            if (_taxRate != value)
        //            {
        //                _taxRate = value;
        //                OnPropertyChanged(nameof(TaxRate));
        //            }
        //        }
        //    }
        //}
        //public bool Habitable
        //{
        //    get
        //    {
        //        return _habitable;
        //    }
        //    set
        //    {
        //        if (_habitable != value)
        //        {
        //            _habitable = value;
        //            OnPropertyChanged(nameof(Habitable));
        //        }
        //    }
        //}
        //#endregion

        //public Dictionary<Star, Lane> Neighbors { get; } = [];
        public ObservableCollection<Lane> Neighbors { get; } = [];
        
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
            Explored = true;
        }
        public Star(string name, string description, Map.StarType starType)
        {
            Name = name;
            Description = description;
            StarType = starType;
            Explored = false;
        }

        //public void Explore()
        //{
        //    // Decide the star type.
        //    Random p = new Random();
        //    double roll = p.NextDouble() * Map.StarTypeChance.Sum();
        //    double basic = 0;
        //    for (int i = 0; i < Map.StarTypeChance.Count; i++)
        //    {
        //        basic += Map.StarTypeChance[i];
        //        if (roll <= basic)
        //        {
        //            this.StarType = (Map.StarType) i;
        //            break;
        //        }
        //    }

        //    // Decide whether it is habitable
        //    roll = p.NextDouble();
        //    if (roll <= Map.StarHabitableChance[(int) this.StarType])
        //    {
        //        this.Habitable = true;
        //    }

        //    // Decide the number of neighbor
        //    roll = p.NextDouble() * 100;
        //    int neighborCount = 1;
        //    if (roll <= 40)
        //    {
        //        neighborCount = 2;
        //    }
        //    else if (roll <= 70)
        //    {
        //        neighborCount = 3;
        //    }
        //    else if (roll <= 90)
        //    {
        //        neighborCount = 4;
        //    }
        //    for (int i = 1; i < neighborCount; i++)
        //    {
        //        Star star = new Star();
        //        Star.ConnectStar(this, star);
        //    }

        //}

        //public static void ConnectStar(Star A, Star B)
        //{
        //    Lane lane = new Lane();
        //    Map.Lanes.Add(lane);
        //    A.Neighbors.TryAdd(B, lane);
        //    B.Neighbors.TryAdd(A, lane);
        //}

        //public static void ConnectStar(Star A, Star B, Map.LaneType laneType)
        //{
        //    Lane lane = new Lane(laneType);
        //    Map.Lanes.Add(lane);
        //    A.Neighbors.TryAdd(B, lane);
        //    B.Neighbors.TryAdd(A, lane);
        //}

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
