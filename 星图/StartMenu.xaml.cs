using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
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
using System.Xml;

namespace 星图
{
    /// <summary>
    /// Interaction logic for StartMenu.xaml
    /// </summary>
    public partial class StartMenu : Window
    {
        public StartMenu()
        {
            InitializeComponent();
        }

        internal Map? map;

        private void EditMap_btn_Click(object sender, RoutedEventArgs e)
        {
            var editMapWindow = new EditMap()
            {
                DataContext = map,
            };
            editMapWindow.ShowDialog();
        }

        private void CreateMap_btn_Click(object sender, RoutedEventArgs e)
        {
            map = new Map();
            MapName_textb.Text = "New Map (Not Saved Yet)";

            EditMap_btn.IsEnabled = true;
        }

        private void CreateInteractiveMap_btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LoadMap_btn_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.DefaultExt = ".xml";
            dialog.Filter = "Map XML (.xml)|*.xml";
            dialog.DefaultDirectory = @"save\";
            dialog.Multiselect = false;

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                DataContractSerializer dcs = new(typeof(Map));
                using(var fs = dialog.OpenFile())
                {
                    using (XmlDictionaryReader xdr = XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas()))
                    {
                        map = dcs.ReadObject(xdr) as Map;
                        MapName_textb.Text = dialog.FileName;
                    }       
                }

                EditMap_btn.IsEnabled = true;
                DisplayMap_btn.IsEnabled = true;
                CreateInteractiveMap_btn.IsEnabled = true;
            }
        }

        private void DisplayMap_btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveMap_btn_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.DefaultExt = ".xml";
            dialog.Filter = "Map XML (.xml)|*.xml";
            dialog.AddExtension = true;
            dialog.DefaultDirectory = @"save\";
            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                DataContractSerializer dcs = new(typeof(Map));
                using (var fs = dialog.OpenFile())
                {
                    using (XmlDictionaryWriter xdw = XmlDictionaryWriter.CreateTextWriter(fs, Encoding.UTF8))
                    {
                        dcs.WriteObject(xdw, map);
                        MapName_textb.Text = dialog.FileName;
                    }
                }
            }
        }
    }
}
