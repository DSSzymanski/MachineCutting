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
        private Controler mainControl = new Controler();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddInstructionClick(object sender, RoutedEventArgs e)
        {
            //Console.WriteLine(mainControl.getCurrPos());
            if (InputText.Text == "") { return; }
            string input = InputText.Text;
            InputText.Text = "";
            mainControl.runInstruction(input);
        }

        private void SetDimClick(object sender, RoutedEventArgs e)
        {
            InputText.Text = Controler.DimensionString;
        }

        private void SetLocClick(object sender, RoutedEventArgs e)
        {
            InputText.Text = Controler.LocationString;
        }

        private void CutNClick(object sender, RoutedEventArgs e)
        {
            InputText.Text = Controler.CutNorth;
        }

        private void CutSClick(object sender, RoutedEventArgs e)
        {
            InputText.Text = Controler.CutSouth;
        }
        private void CutEClick(object sender, RoutedEventArgs e)
        {
            InputText.Text = Controler.CutEast;
        }
        private void CutWClick(object sender, RoutedEventArgs e)
        {
            InputText.Text = Controler.CutWest;
        }
    }
}
