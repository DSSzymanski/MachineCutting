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
            SetCurrentLocation();
            SetupDetailWindow();
        }

        private void DetailErrorHandler(int error)
        {
            if (error == Controler.FAILED_NOT_AN_INT)
            {
                DetailErrors.IsOpen = true;
                DetailErrorText.Text = "Input must be an integer(s) greater than zero.";
            }
            else if (error == Controler.FAILED_PARAM_LENGTH)
            {
                DetailErrors.IsOpen = true;
                DetailErrorText.Text = "Incorrect parameter length. See details tab for instruction parameters.";
            }
            else if (error == Controler.FAILED_INSTRUCTION_NOT_FOUND)
            {
                DetailErrors.IsOpen = true;
                DetailErrorText.Text = "Invalid instruction. See details tab for instruction list.";
            }
            else if (error == Controler.FAILED_BOUNDS_CHECK)
            {
                DetailErrors.IsOpen = true;
                DetailErrorText.Text = "Input failed bounds check. Please enter values that fall within the material size.";
            }
        }

        private void ErrorHandler(int error)
        {
            if (error == Controler.FAILED_NOT_AN_INT)
            {
                Errors.IsOpen = true;
                ErrorText.Text = "Input must be an integer(s) greater than zero.";
            }
            else if(error == Controler.FAILED_PARAM_LENGTH)
            {
                Errors.IsOpen = true;
                ErrorText.Text = "Incorrect parameter length. See details tab for instruction parameters.";
            }
            else if(error == Controler.FAILED_INSTRUCTION_NOT_FOUND)
            {
                Errors.IsOpen = true;
                ErrorText.Text = "Invalid instruction. See details tab for instruction list.";
            }
            else if(error == Controler.FAILED_BOUNDS_CHECK)
            {
                Errors.IsOpen = true;
                ErrorText.Text = "Input failed bounds check. Please enter values that fall within the material size.";
            }
        }
        private void ClosePopupClick(object sender, RoutedEventArgs e) { Errors.IsOpen = false; }
        private void CloseDetailPopupClick(object sender, RoutedEventArgs e) { DetailErrors.IsOpen = false; }

        private void SMBDDetailClick(object sender, RoutedEventArgs e)
        {
            string instruction = $"{Controler.DimensionString} {SMBDXInput.Text} {SMBDYInput.Text}";
            int error = mainControl.testInstruction(instruction);
            ResetInputs();
            DetailErrorHandler(error);
        }
        private void SCLDetailClick(object sender, RoutedEventArgs e)
        {
            string instruction = $"{Controler.LocationString} {SCLXInput.Text} {SCLYInput.Text}";
            int error = mainControl.testInstruction(instruction);
            ResetInputs();
            DetailErrorHandler(error);
        }
        private void CMNDetailClick(object sender, RoutedEventArgs e)
        {
            string instruction = $"{Controler.CutNorth} {CMNLInput.Text}";
            int error = mainControl.testInstruction(instruction);
            ResetInputs();
            DetailErrorHandler(error);
        }
        private void CMSDetailClick(object sender, RoutedEventArgs e)
        {
            string instruction = $"{Controler.CutSouth} {CMSLInput.Text}";
            int error = mainControl.testInstruction(instruction);
            ResetInputs();
            DetailErrorHandler(error);
        }
        private void CMEDetailClick(object sender, RoutedEventArgs e)
        {
            string instruction = $"{Controler.CutEast} {CMELInput.Text}";
            int error = mainControl.testInstruction(instruction);
            ResetInputs();
            DetailErrorHandler(error);
        }
        private void CMWDetailClick(object sender, RoutedEventArgs e)
        {
            string instruction = $"{Controler.CutWest} {CMWLInput.Text}";
            int error = mainControl.testInstruction(instruction);
            ResetInputs();
            DetailErrorHandler(error);
        }
        private void TabUpdate(object sender, RoutedEventArgs e) { Draw(); }
        private void SetupDetailWindow()
        {
            SMBDTextBlock.Text = Controler.DimensionString;
            SMBDDescriptionBlock.Text = "Sets size of material block on screen (screen will scale). " +
                $"Parameters of length and width. Called as: {Controler.DimensionString} Length Width";
            SCLTextBlock.Text = Controler.LocationString;
            SCLDescriptionBlock.Text = "Sets the current position to one given. Parameters of X and Y." +
                $"Called as: {Controler.LocationString} X Y";
            CMNTextBlock.Text = Controler.CutNorth;
            CMNDescriptionBlock.Text = "Adds a cut North a certain amount given by the parameter entered. " +
                $"Parameter of length. Called as: {Controler.CutNorth} Length";
            CMSTextBlock.Text = Controler.CutSouth;
            CMSDescriptionBlock.Text = "Adds a cut South a certain amount given by the parameter entered. " +
                $"Parameter of length. Called as: {Controler.CutSouth} Length";
            CMETextBlock.Text = Controler.CutEast;
            CMEDescriptionBlock.Text = "Adds a cut East a certain amount given by the parameter entered. " +
                $"Parameter of length. Called as: {Controler.CutEast} Length";
            CMWTextBlock.Text = Controler.CutWest;
            CMWDescriptionBlock.Text = "Adds a cut West a certain amount given by the parameter entered. " +
                $"Parameter of length. Called as: {Controler.CutWest} Length";
            ResetInputs();
        }
        private void ResetInputs()
        {
            SMBDXInput.Text = "0"; //SetMaterialBlockDimensions
            SMBDYInput.Text = "0"; //SetMaterialBlockDimensions
            SCLXInput.Text = "0"; //SetCutLength
            SCLYInput.Text = "0"; //SetCutLength
            CMNLInput.Text = "0"; //CutMoveNorth
            CMSLInput.Text = "0"; //CutMoveSouth
            CMELInput.Text = "0"; //CutMoveEast
            CMWLInput.Text = "0"; //CutMoveWest
        }
        private void AddInstructionClick(object sender, RoutedEventArgs e)
        {
            if (InputText.Text == "") { Draw(); return; }
            string input = InputText.Text;
            InputText.Text = "";
            int error = mainControl.testInstruction(input);
            SetCurrentLocation();
            Draw();
            fillInstrutionListbox(mainControl.getInstructions());
            ErrorHandler(error);
        }

        private void removeInstructionClick(object sender, RoutedEventArgs e)
        {
            if (instructionListBox.SelectedIndex == -1) { return; }
            int index = instructionListBox.SelectedIndex;
            mainControl.RemoveInstruction(index);
            Draw();
            SetCurrentLocation();
            fillInstrutionListbox(mainControl.getInstructions());
        }

        private void fillInstrutionListbox(List<string> input)
        {
            instructionListBox.Items.Clear();
            foreach (string instruction in input)
            {
                instructionListBox.Items.Add(instruction);
            }
        }

        private void SetCurrentLocation()
        {
            currentPosition.Text = $"Location: {mainControl.locationString()}";
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
        
            //transform canvas
            drawArea.RenderTransformOrigin = new Point(0.5, 0.5);
            drawArea.RenderTransform = new ScaleTransform(scaleX: 1, scaleY:-1);

            //scale objects to fit canvas
            ScaleTransform scale = new ScaleTransform(scaleX: Math.Min(scaleX, scaleY), 
                scaleY: Math.Min(scaleX, scaleY));
            materialDimension.RenderTransform = scale;

            foreach (Line l in drawArea.Children.OfType<Line>()) { l.RenderTransform = scale; }

            //Crosshairs for current location
            Line l1 = new Line();
            l1.X1 = currPos[0]+4;
            l1.Y1 = currPos[1]+4;
            l1.X2 = currPos[0]-4;
            l1.Y2 = currPos[1]-4;
            l1.Stroke = Brushes.Red;
            l1.StrokeThickness = 1.0;

            Line l2 = new Line();
            l2.X1 = currPos[0]-4;
            l2.Y1 = currPos[1]+4;
            l2.X2 = currPos[0]+4;
            l2.Y2 = currPos[1]-4;
            l2.Stroke = Brushes.Red;
            l2.StrokeThickness = 1.0;

            double xloc = currPos[0] * scaleX;
            double yloc = currPos[1] * scaleY;
            TranslateTransform crosshairScale = new TranslateTransform(xloc-currPos[0], yloc-currPos[1] );

            l1.RenderTransform = crosshairScale;
            l2.RenderTransform = crosshairScale;

            drawArea.Children.Add(l1);
            drawArea.Children.Add(l2);
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
