using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace 星图
{
    /// <summary>
    /// Interaction logic for EditMap.xaml
    /// </summary>
    public partial class EditMap : Window
    {
        public EditMap()
        {
            InitializeComponent();
        }
        private void CollectionViewSource_Filter(object sender, FilterEventArgs e)
        {
            Star selectedStar = StarBoxs.SelectedItem as Star;
            e.Accepted = e.Item != selectedStar;
        }

        private void StarBoxs_AddStar_Click(object sender, RoutedEventArgs e)
        {
            Star newStar = new Star("New Star", "Add Description Here");
            (DataContext as Map).AddStar(newStar);

        }

        private void StarBoxs_RemoveStar_Click(object sender, RoutedEventArgs e)
        {
            Star selectedStar = StarBoxs.SelectedItem as Star;
            (DataContext as Map).RemoveStar(selectedStar);
            //MessageBox.Show(selectedStar.Name);
        }

        private void AddLane_Click(object sender, RoutedEventArgs e)
        {
            //EditLane editLaneWindow = new EditLane() { DataContext =  this.DataContext };
            //editLaneWindow.Show();
            Star selectedStar = StarBoxs.SelectedItem as Star;
            Lane l = new Lane(selectedStar);
            (DataContext as Map).Lanes.Add(l);
        }

        private void StarBoxs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Star? selectedStar = StarBoxs.SelectedItem as Star;

            if (selectedStar != null)
            {
                selectedStar.OrganizeLanes();
            }
        }
        private void ComboBox_Initialized(object sender, EventArgs e)
        {
            ComboBox CB = sender as ComboBox;
            CollectionViewSource CVS = new() { Source = (DataContext as Map).Stars };
            CVS.Filter += ShowOnlySelectableStarsFilter;

            Binding newBinding = new();
            newBinding.Source = CVS;
            CB.SetBinding(ComboBox.ItemsSourceProperty, newBinding);
        }

        private void ShowOnlySelectableStarsFilter(object sender, FilterEventArgs e)
        {
            Star selectedStar = StarBoxs.SelectedItem as Star;
            e.Accepted = e.Item != selectedStar;
        }

        private void RemoveLane_Click(object sender, RoutedEventArgs e)
        {
            Lane lane = LaneView.SelectedItem as Lane;
            (DataContext as Map).RemoveLane(lane);
        }

        private void DisplayMapBtn_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(DataContext.ToString());
            //MessageBox.Show((StarBoxs.SelectedItem as Star).Name);
            DisplayedMap displayedMap = new DisplayedMap() { DataContext = this.DataContext, rootStar = StarBoxs.SelectedItem as Star };
            displayedMap.Show();
        }
    }
}
