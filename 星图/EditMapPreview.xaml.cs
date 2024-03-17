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

    public class StarCheck_Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value + (parameter as string);

            //if (parameter == value)
            //{
            //    return DependencyProperty.UnsetValue;
            //}
            //else
            //{
            //    return value;
            //}
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
