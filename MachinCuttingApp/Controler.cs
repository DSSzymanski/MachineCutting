using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachinCuttingApp
{
    public class Controler
    {
        //constants
        public static readonly string DimensionString = "SetMaterialBlockDimensions";
        public static readonly string LocationString = "SetCutLocation";
        public static readonly string CutNorth = "CutMoveNorth";
        public static readonly string CutSouth = "CutMoveSouth";
        public static readonly string CutEast = "CutMoveEast";
        public static readonly string CutWest = "CutMoveWest";

        public static readonly int FAILED_NOT_AN_INT = -1;
        public static readonly int FAILED_BOUNDS_CHECK = -2;
        public static readonly int FAILED_PARAM_LENGTH = -3;
        public static readonly int FAILED_INSTRUCTION_NOT_FOUND = -4;
        public static readonly int PASS = 1;
        //rest of vars
        private int[] materialSize { get; set; }
        private int[] currPos { get; set; }
        private List<string> instructionList = new List<string>();

        public Controler()
        {
            materialSize = new int[2] { 0, 0 };
            currPos = new int[2] { 0, 0 };
        }


        //testing string, not currently used
        public string MaterialString() { return $"({materialSize[0]},{materialSize[1]})"; }
        //current location string for main app page
        public string CurrPosString() { return $"({currPos[0]},{currPos[1]})"; }
        //used for unit testing
        public int InstructionLength() { return instructionList.Count; }
        public List<string> GetInstructions() { return instructionList; }
        //testing for input validity. Parses input, checks if instruction exists,
        //checks for error in parameters, and adds to instruction list if it passes.
        public int TestInstruction(string input)
        {
            //base
            int result = FAILED_INSTRUCTION_NOT_FOUND;
            string[] parsedInput = Validator.Parser(input);
            if (parsedInput[0] == DimensionString)
            {
                if (parsedInput.Length != 3) { return FAILED_PARAM_LENGTH; }
                result = TestSetMaterialBlockDimensions(parsedInput[1], parsedInput[2]);
                if (result == PASS) { SetMaterialBlockDimensions(parsedInput[1], parsedInput[2]); instructionList.Add(input); }
            }
            else if (parsedInput[0] == LocationString)
            {
                if (parsedInput.Length != 3) { return FAILED_PARAM_LENGTH; }
                result = TestSetCutLocation(parsedInput[1], parsedInput[2]);
                if (result == PASS) { SetCutLocation(parsedInput[1], parsedInput[2]); instructionList.Add(input); }
            }
            else if (parsedInput[0] == CutNorth)
            {
                if (parsedInput.Length != 2) { return FAILED_PARAM_LENGTH; }
                result = TestCutNorth(parsedInput[1]);
                if (result == PASS) {
                    currPos[1] += Validator.ValidateInput(parsedInput[1]);
                    instructionList.Add(input);
                }
            }
            else if (parsedInput[0] == CutSouth)
            {
                if (parsedInput.Length != 2) { return FAILED_PARAM_LENGTH; }
                result = TestCutSouth(parsedInput[1]);
                if (result == PASS) {
                    currPos[1] -= Validator.ValidateInput(parsedInput[1]);
                    instructionList.Add(input);
                }
            }
            else if (parsedInput[0] == CutEast)
            {
                if (parsedInput.Length != 2) { return FAILED_PARAM_LENGTH; }
                result = TestCutEast(parsedInput[1]);
                if (result == PASS) {
                    currPos[0] += Validator.ValidateInput(parsedInput[1]);
                    instructionList.Add(input);
                }
            }
            else if (parsedInput[0] == CutWest)
            {
                if (parsedInput.Length != 2) { return FAILED_PARAM_LENGTH; }
                result = TestCutWest(parsedInput[1]);
                if (result == PASS) {
                    currPos[0] -= Validator.ValidateInput(parsedInput[1]);
                    instructionList.Add(input);
                }
            }
            return result;
        }

        //Removes instruction at current index. Retests all inputs to ensure they
        //are still valid
        public void RemoveInstruction(int index)
        {
            List<string> instructions = new List<string>(instructionList);
            instructions.RemoveAt(index);
            instructionList.Clear();
            currPos[0] = 0;
            currPos[1] = 0;
            materialSize[0] = 0;
            materialSize[1] = 0;
            for (int i = 0; i < instructions.Count; i++){ TestInstruction(instructions[i]); }
        }

        //Below are the tests, written to ensure that the parameters for a given instruction are valid
        
        private int TestSetMaterialBlockDimensions(string x, string y)
        {
            int validatedX = Validator.ValidateInput(x);
            int validatedY = Validator.ValidateInput(y);
            if (validatedX == -1 || validatedY == -1)
            {
                return FAILED_NOT_AN_INT;
            }
            else if (!Validator.LowerBoundCheck(lowerBound: 0, validatedX) ||
                !Validator.LowerBoundCheck(lowerBound: 0, validatedY))
            {
                return FAILED_BOUNDS_CHECK;
            }
            return PASS;
        }

        private int TestSetCutLocation(string x, string y)
        {
            int validatedX = Validator.ValidateInput(x);
            int validatedY = Validator.ValidateInput(y);
            if (validatedX == -1 || validatedY == -1)
            {
                return FAILED_NOT_AN_INT;
            }
            else if (Validator.BoundsCheck(lowerBound: 0, upperBound: materialSize[0], checkVal: validatedX) == false ||
                Validator.BoundsCheck(lowerBound: 0, upperBound: materialSize[1], checkVal: validatedY) == false)
            {
                return FAILED_BOUNDS_CHECK;
            }
            return PASS;
        }
        private int TestCutNorth(string len)
        {
            int validatedLen = Validator.ValidateInput(len);
            if (validatedLen == -1) { return FAILED_NOT_AN_INT; }
            if (!Validator.BoundsCheck(0, materialSize[1], currPos[1] + validatedLen)) { return FAILED_BOUNDS_CHECK; }
            return PASS;
        }

        private int TestCutSouth(string len)
        {
            int validatedLen = Validator.ValidateInput(len);
            if (validatedLen == -1) { return FAILED_NOT_AN_INT; }
            if (!Validator.BoundsCheck(0, materialSize[1], currPos[1] - validatedLen)) { return FAILED_BOUNDS_CHECK; }
            return PASS;
        }
        private int TestCutEast(string len)
        {
            int validatedLen = Validator.ValidateInput(len);
            if (validatedLen == -1) { return FAILED_NOT_AN_INT; }
            if (!Validator.BoundsCheck(0, materialSize[0], currPos[0] + validatedLen)) { return FAILED_BOUNDS_CHECK; }
            return PASS;
        }
        private int TestCutWest(string len)
        {
            int validatedLen = Validator.ValidateInput(len);
            if (validatedLen == -1) { return FAILED_NOT_AN_INT; }
            if (!Validator.BoundsCheck(0, materialSize[0], currPos[0] - validatedLen)) { return FAILED_BOUNDS_CHECK; }
            return PASS;
        }

        private int TestSetMaterialBlockDimensionsTest(string x, string y)
        {
            int validatedX = Validator.ValidateInput(x);
            int validatedY = Validator.ValidateInput(y);
            if (validatedX == -1 || validatedY == -1)
            {
                return FAILED_NOT_AN_INT;
            }
            else if (!Validator.LowerBoundCheck(lowerBound: 0, validatedX) ||
                !Validator.LowerBoundCheck(lowerBound: 0, validatedY))
            {
                return FAILED_BOUNDS_CHECK;
            }
            return PASS;
        }
       
        public void SetMaterialBlockDimensions(string x, string y)
        {
            int validatedX = Validator.ValidateInput(x);
            int validatedY = Validator.ValidateInput(y);
            materialSize[0] = validatedX;
            materialSize[1] = validatedY;
        }
        public void SetCutLocation(string x, string y)
        {
            int validatedX = Validator.ValidateInput(x);
            int validatedY = Validator.ValidateInput(y);
            currPos[0] = validatedX;
            currPos[1] = validatedY;
        }
    }
}
