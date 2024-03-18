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
    /// EditLane.xaml 的交互逻辑
    /// </summary>
    public partial class EditLane : Window
    {
        public EditLane()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Endpoint1_ComboBox.SelectedItem == Endpoint2_ComboBox.SelectedItem)
            {
                AnnouceError("Self-Loop Lane is not supported yet");
                return;
            }

            (DataContext as Map).ConnectStars(Endpoint1_ComboBox.SelectedItem as Star, Endpoint2_ComboBox.SelectedItem as Star, (LaneType)LaneType_ComboBox.SelectedItem);
        }

        private void AnnouceError(string message)
        {
            MessageBox.Show(message);
        }
    }
}
