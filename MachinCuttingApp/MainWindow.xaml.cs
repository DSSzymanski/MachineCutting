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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MachinCuttingApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Controler mainControl;
        public MainWindow()
        {
            InitializeComponent();
            mainControl = new Controler();
        }

        private void SetDimClick(object sender, RoutedEventArgs e)
        {
            InputPrompt.Text = "Set Material Dimensions:";
            inputText.Content = "X:";
            InputPopup.IsOpen = true; 
        }

        private void CloseClick(object sender, RoutedEventArgs e)
        {
            InputPopup.IsOpen = false;
        }

        private void SetLocClick(object sender, RoutedEventArgs e)
        {

        }

        private void CutNClick(object sender, RoutedEventArgs e)
        {

        }


    }
}
