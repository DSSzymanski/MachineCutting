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
            if (InputText.Text == "") { Draw(); return; }
            string input = InputText.Text;
            InputText.Text = "";
            mainControl.testInstruction(input);
            SetCurrentLocation();
            Draw();
        }

        private void SetCurrentLocation()
        {
            currentPosition.Text = mainControl.locationString() + mainControl.materialString();
        }
        private void Draw()
        {
            //clear children from draw area
            drawArea.Children.Clear();

            //init vars
            double windowSizeX = drawArea.Height;
            double windowSizeY = drawArea.Width;

            Rectangle materialDimension = new Rectangle();
            materialDimension.Stroke = Brushes.Black;
            materialDimension.StrokeThickness = 0.1;

            drawArea.Children.Add(materialDimension);

            double scaleX = windowSizeX / materialDimension.Width;
            double scaleY = windowSizeY / materialDimension.Height;

            double[] currPos = { 0, 0 };

            //get instructions from controler
            List<string> instructions = mainControl.getInstructions();
            
            //iter through instructions and draw on canvas
            for (int i = 0 ; i < instructions.Count; i++)
            {
                string[] parsed = Validator.Parser(instructions[i]);
                if (parsed[0] == Controler.DimensionString)
                {
                    materialDimension.Width = Validator.validateInput(parsed[1]);
                    materialDimension.Height = Validator.validateInput(parsed[2]);
                    scaleX = windowSizeX / materialDimension.Width;
                    scaleY = windowSizeY / materialDimension.Height;
                }
                else if (parsed[0] == Controler.LocationString)
                {
                    currPos[0] = Validator.validateInput(parsed[1]);
                    currPos[1] = Validator.validateInput(parsed[2]);
                }
                else if (parsed[0] == Controler.CutNorth)
                {
                    Line l = new Line
                    {
                        X1 = currPos[0],
                        Y1 = currPos[1],
                        Stroke = Brushes.Black,
                        StrokeThickness = 0.1
                    };
                    l.X2 = l.X1;
                    l.Y2 = l.Y1 + Validator.validateInput(parsed[1]);
                    drawArea.Children.Add(l);
                    currPos[0] = l.X2;
                    currPos[1] = l.Y2;
                }
                else if (parsed[0] == Controler.CutSouth)
                {
                    Line l = new Line
                    {
                        X1 = currPos[0],
                        Y1 = currPos[1],
                        Stroke = Brushes.Black,
                        StrokeThickness = 0.1
                    };
                    l.X2 = l.X1;
                    l.Y2 = l.Y1 - Validator.validateInput(parsed[1]);
                    drawArea.Children.Add(l);
                    currPos[0] = l.X2;
                    currPos[1] = l.Y2;
                }
                else if (parsed[0] == Controler.CutEast)
                {
                    Line l = new Line
                    {
                        X1 = currPos[0],
                        Y1 = currPos[1],
                        Stroke = Brushes.Black,
                        StrokeThickness = 0.1
                    };
                    l.X2 = l.X1 + Validator.validateInput(parsed[1]);
                    l.Y2 = l.Y1;
                    drawArea.Children.Add(l);
                    currPos[0] = l.X2;
                    currPos[1] = l.Y2;
                }
                else if (parsed[0] == Controler.CutWest)
                {
                    Line l = new Line
                    {
                        X1 = currPos[0],
                        Y1 = currPos[1],
                        Stroke = Brushes.Black,
                        StrokeThickness = 0.1
                    };
                    l.X2 = l.X1 - Validator.validateInput(parsed[1]);
                    l.Y2 = l.Y1;
                    drawArea.Children.Add(l);
                    currPos[0] = l.X2;
                    currPos[1] = l.Y2;
                }
            }

            //Crosshairs for current location
            Line l1 = new Line();
            l1.X1 = currPos[0] - 2;
            l1.Y1 = currPos[1] - 2;
            l1.X2 = currPos[0] + 2;
            l1.Y2 = currPos[1] + 2;
            l1.Stroke = Brushes.Red;
            l1.StrokeThickness = 0.25;

            Line l2 = new Line();
            l2.X1 = currPos[0] - 2;
            l2.Y1 = currPos[1] + 2;
            l2.X2 = currPos[0] + 2;
            l2.Y2 = currPos[1] - 2;
            l2.Stroke = Brushes.Red;
            l2.StrokeThickness = 0.25;

            drawArea.Children.Add(l1);
            drawArea.Children.Add(l2);

            //scale to fit canvas
            ScaleTransform scale = new ScaleTransform(scaleX: Math.Min(scaleX, scaleY), scaleY: Math.Min(scaleX, scaleY));
            materialDimension.RenderTransform = scale;
            foreach (Line l in drawArea.Children.OfType<Line>())
            {
                l.RenderTransform = scale;
            }
        }
        //sets button click to fill text box and focus
        private void SetDimClick(object sender, RoutedEventArgs e)
        {
            InputText.Text = Controler.DimensionString + " ";
            Keyboard.Focus(InputText);
            InputText.SelectionStart = InputText.Text.Length;
        }
        //sets button click to fill text box and focus
        private void SetLocClick(object sender, RoutedEventArgs e)
        {
            InputText.Text = Controler.LocationString + " ";
            Keyboard.Focus(InputText);
            InputText.SelectionStart = InputText.Text.Length;
        }
        //sets button click to fill text box and focus
        private void CutNClick(object sender, RoutedEventArgs e)
        {
            InputText.Text = Controler.CutNorth + " ";
            Keyboard.Focus(InputText);
            InputText.SelectionStart = InputText.Text.Length;
        }
        //sets button click to fill text box and focus
        private void CutSClick(object sender, RoutedEventArgs e)
        {
            InputText.Text = Controler.CutSouth + " ";
            Keyboard.Focus(InputText);
            InputText.SelectionStart = InputText.Text.Length;
        }
        //sets button click to fill text box and focus
        private void CutEClick(object sender, RoutedEventArgs e)
        {
            InputText.Text = Controler.CutEast + " ";
            Keyboard.Focus(InputText);
            InputText.SelectionStart = InputText.Text.Length;
        }
        //sets button click to fill text box and focus
        private void CutWClick(object sender, RoutedEventArgs e)
        {
            InputText.Text = Controler.CutWest + " ";
            Keyboard.Focus(InputText);
            InputText.SelectionStart = InputText.Text.Length;
        }
    }
}
