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
            //Write current cut location
            SetCurrentLocation();
            //initialize values on detail page
            SetupDetailWindow();
        }

        //opens error popup for detail instruction page
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

        //opens error popup for main page
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

        /*Detail click below will create a string based on which button is clicked 
         * on the detail page, then run the instruction, reset the page, and raise errors if any.
         */
        private void SMBDDetailClick(object sender, RoutedEventArgs e)
        {
            string instruction = $"{Controler.DimensionString} {SMBDXInput.Text} {SMBDYInput.Text}";
            int error = mainControl.TestInstruction(instruction);
            ResetInputs();
            DetailErrorHandler(error);
        }
        private void SCLDetailClick(object sender, RoutedEventArgs e)
        {
            string instruction = $"{Controler.LocationString} {SCLXInput.Text} {SCLYInput.Text}";
            int error = mainControl.TestInstruction(instruction);
            ResetInputs();
            DetailErrorHandler(error);
        }
        private void CMNDetailClick(object sender, RoutedEventArgs e)
        {
            string instruction = $"{Controler.CutNorth} {CMNLInput.Text}";
            int error = mainControl.TestInstruction(instruction);
            ResetInputs();
            DetailErrorHandler(error);
        }
        private void CMSDetailClick(object sender, RoutedEventArgs e)
        {
            string instruction = $"{Controler.CutSouth} {CMSLInput.Text}";
            int error = mainControl.TestInstruction(instruction);
            ResetInputs();
            DetailErrorHandler(error);
        }
        private void CMEDetailClick(object sender, RoutedEventArgs e)
        {
            string instruction = $"{Controler.CutEast} {CMELInput.Text}";
            int error = mainControl.TestInstruction(instruction);
            ResetInputs();
            DetailErrorHandler(error);
        }
        private void CMWDetailClick(object sender, RoutedEventArgs e)
        {
            string instruction = $"{Controler.CutWest} {CMWLInput.Text}";
            int error = mainControl.TestInstruction(instruction);
            ResetInputs();
            DetailErrorHandler(error);
        }
        //Method to draw current instructions when swapping detail page -> main app
        private void TabUpdate(object sender, RoutedEventArgs e) { Draw(); }

        //initialized detail page
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
        
        //resets defaults for detail page
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

        /*Takes input from main app page when add button is pressed.
         * Tests for validity, updates, and throws error if any.
         */
        private void AddInstructionClick(object sender, RoutedEventArgs e)
        {
            if (InputText.Text == "") { Draw(); return; }
            string input = InputText.Text;
            InputText.Text = "";
            //test
            int error = mainControl.TestInstruction(input);
            //update
            SetCurrentLocation();
            Draw();
            FillInstrutionListbox(mainControl.GetInstructions());
            ErrorHandler(error);
        }

        //removes instruction from listbox and re-runs all commands to verify
        //they are still valid.
        private void RemoveInstructionClick(object sender, RoutedEventArgs e)
        {
            if (instructionListBox.SelectedIndex == -1) { return; } //base case if nothing selected
            mainControl.RemoveInstruction(instructionListBox.SelectedIndex);
            //updates
            Draw();
            SetCurrentLocation();
            FillInstrutionListbox(mainControl.GetInstructions());
        }

        private void FillInstrutionListbox(List<string> input)
        {
            instructionListBox.Items.Clear();
            foreach (string instruction in input)
            {
                instructionListBox.Items.Add(instruction);
            }
        }

        //updates textblock on main app
        private void SetCurrentLocation() { currentPosition.Text = $"Location: {mainControl.CurrPosString()}";}
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
            List<string> instructions = mainControl.GetInstructions();
            
            //iter through instructions and draw on canvas
            for (int i = 0 ; i < instructions.Count; i++)
            {
                string[] parsed = Validator.Parser(instructions[i]);
                if (parsed[0] == Controler.DimensionString)
                {
                    materialDimension.Width = Validator.ValidateInput(parsed[1]);
                    materialDimension.Height = Validator.ValidateInput(parsed[2]);
                    scaleX = windowSizeX / materialDimension.Width;
                    scaleY = windowSizeY / materialDimension.Height;
                }
                else if (parsed[0] == Controler.LocationString)
                {
                    currPos[0] = Validator.ValidateInput(parsed[1]);
                    currPos[1] = Validator.ValidateInput(parsed[2]);
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
                    l.Y2 = l.Y1 + Validator.ValidateInput(parsed[1]);
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
                    l.Y2 = l.Y1 - Validator.ValidateInput(parsed[1]);
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
                    l.X2 = l.X1 + Validator.ValidateInput(parsed[1]);
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
                    l.X2 = l.X1 - Validator.ValidateInput(parsed[1]);
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

            DrawCrosshairs(scaleX: scaleX, scaleY: scaleY, currX: currPos[0], currY: currPos[1]);

            
        }

        //draw current location as crosshairs on main app visuals
        private void DrawCrosshairs(double scaleX, double scaleY, double currX, double currY)
        {
            int lineLength = 4;
            //Crosshairs for current location
            Line l1 = new Line();
            l1.X1 = currX + lineLength;
            l1.Y1 = currY + lineLength;
            l1.X2 = currX - lineLength;
            l1.Y2 = currY - lineLength;
            l1.Stroke = Brushes.Red;
            l1.StrokeThickness = 1.0;

            Line l2 = new Line();
            l2.X1 = currX - lineLength;
            l2.Y1 = currY + lineLength;
            l2.X2 = currX + lineLength;
            l2.Y2 = currY - lineLength;
            l2.Stroke = Brushes.Red;
            l2.StrokeThickness = 1.0;

            //get new location
            double xloc = currX * Math.Min(scaleX, scaleY);
            double yloc = currY * Math.Min(scaleX, scaleY);

            //translate object to new location by subtracting current location
            TranslateTransform crosshairScale = new TranslateTransform(xloc - currX, yloc - currY);

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
