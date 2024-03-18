using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// EditMapPreview.xaml 的交互逻辑
    /// </summary>
    public partial class EditMapPreview : Window
    {
        public EditMapPreview() 
        {
            InitializeComponent();

            //Star? selectedStar = StarBoxs.SelectedItem as Star;
            //if (selectedStar != null)
            //{
            //    StarType selectedStarType = selectedStar.StarType;
            //    foreach (StarType st in StarType_ComboBox.ItemsSource)
            //    {
            //        if (selectedStarType == st)
            //        {
            //            StarType_ComboBox.SelectedItem = st;
            //        }
            //    }
            //}
        }

        private void CollectionViewSource_Filter(object sender, FilterEventArgs e)
        {
            Star selectedStar = (StarBoxs as ListBox).SelectedItem as Star;
            e.Accepted = e.Item != selectedStar;
        }

        private void StarBoxs_AddStar_Click(object sender, RoutedEventArgs e)
        {
            Star newStar = new Star("New Star", "Add Description Here");
            (DataContext as Map).AddStar(newStar);

        }

        private void StarBoxs_RemoveStar_Click(object sender, RoutedEventArgs e)
        {
            Star selectedStar = (StarBoxs as ListBox).SelectedItem as Star;
            (DataContext as Map).RemoveStar(selectedStar);
            //MessageBox.Show(selectedStar.Name);
        }

        private void AddLane_Click(object sender, RoutedEventArgs e)
        {
            EditLane editLaneWindow = new EditLane() { DataContext =  this.DataContext };
            editLaneWindow.Show();
        }

        //private void StarBoxs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    Star? selectedStar = StarBoxs.SelectedItem as Star;

        //    if (selectedStar != null && StarType_ComboBox != null)
        //    {
        //        StarType_ComboBox.SelectedItem = selectedStar.StarType;
        //        //StarType selectedStarType = selectedStar.StarType;
        //        //foreach (StarType st in StarType_ComboBox.ItemsSource )
        //        //{
        //        //    if ( selectedStarType == st )
        //        //    {
        //        //        StarType_ComboBox.SelectedItem = st;
        //        //    }
        //        //}
        //    }
        //}

        //private void StarType_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    Star? selectedStar = StarBoxs.SelectedItem as Star;
        //    StarType selectedStarType = (StarType) StarType_ComboBox.SelectedItem;
        //    if (selectedStar != null)
        //    {
        //        selectedStar.StarType = selectedStarType;
        //    }
        //}

    }

    public class StarCheck_Converter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 3)
            {
                throw new ArgumentException("Invalid Argument: Must be three argument");
            }

            if (values[0] == values[1])
            {
                return values[2];
            }
            else if (values[0] == values[2])
            {
                return values[1];
            }
            else
            {
                throw new ArgumentException("Invalid Argument: Must be equivalent stars");
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    public class StarCheck2_Converter : IValueConverter
    {
        public object Convert(object values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == parameter)
            {
                return DependencyProperty.UnsetValue;
            }
            else
            {
                return values;
            }
        }

        public object ConvertBack(object value, Type targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
