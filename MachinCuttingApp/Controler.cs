using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachinCuttingApp
{
    public class Controler
    {
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
        private int[] materialSize { get; }
        private int[] currPos { get; }
        private List<string> instructionList = new List<string>();

        public Controler()
        {
            materialSize = new int[2] { 0, 0 };
            currPos = new int[2] { 0, 0 };
        }


        public string materialString() { return $"({materialSize[0]},{materialSize[1]})"; }
        public string locationString() { return $"({currPos[0]},{currPos[1]})"; }
        public int instructionLength() { return instructionList.Count; }
        public List<string> getInstructions() { return instructionList; }
        public int testInstruction(string input)
        {
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
                    currPos[1] += Validator.validateInput(parsedInput[1]);
                    instructionList.Add(input);
                }
            }
            else if (parsedInput[0] == CutSouth)
            {
                if (parsedInput.Length != 2) { return FAILED_PARAM_LENGTH; }
                result = TestCutSouth(parsedInput[1]);
                if (result == PASS) {
                    currPos[1] -= Validator.validateInput(parsedInput[1]);
                    instructionList.Add(input);
                }
            }
            else if (parsedInput[0] == CutEast)
            {
                if (parsedInput.Length != 2) { return FAILED_PARAM_LENGTH; }
                result = TestCutEast(parsedInput[1]);
                if (result == PASS) {
                    currPos[0] += Validator.validateInput(parsedInput[1]);
                    instructionList.Add(input);
                }
            }
            else if (parsedInput[0] == CutWest)
            {
                if (parsedInput.Length != 2) { return FAILED_PARAM_LENGTH; }
                result = TestCutWest(parsedInput[1]);
                if (result == PASS) {
                    currPos[0] -= Validator.validateInput(parsedInput[1]);
                    instructionList.Add(input);
                }
            }
            return result;
        }
        
        private int TestSetMaterialBlockDimensions(string x, string y)
        {
            int validatedX = Validator.validateInput(x);
            int validatedY = Validator.validateInput(y);
            if (validatedX == -1 || validatedY == -1)
            {
                return FAILED_NOT_AN_INT;
            }
            else if (!Validator.lowerBoundCheck(lowerBound: 0, validatedX) ||
                !Validator.lowerBoundCheck(lowerBound: 0, validatedY))
            {
                return FAILED_BOUNDS_CHECK;
            }
            return PASS;
        }

        private int TestSetCutLocation(string x, string y)
        {
            int validatedX = Validator.validateInput(x);
            int validatedY = Validator.validateInput(y);
            if (validatedX == -1 || validatedY == -1)
            {
                return FAILED_NOT_AN_INT;
            }
            else if (Validator.boundsCheck(lowerBound: 0, upperBound: materialSize[0], checkVal: validatedX) == false ||
                Validator.boundsCheck(lowerBound: 0, upperBound: materialSize[1], checkVal: validatedY) == false)
            {
                return FAILED_BOUNDS_CHECK;
            }
            return PASS;
        }
        private int TestCutNorth(string len)
        {
            int validatedLen = Validator.validateInput(len);
            if (validatedLen == -1) { return FAILED_NOT_AN_INT; }
            if (!Validator.boundsCheck(0, materialSize[1], currPos[1] + validatedLen)) { return FAILED_BOUNDS_CHECK; }
            return PASS;
        }

        private int TestCutSouth(string len)
        {
            int validatedLen = Validator.validateInput(len);
            if (validatedLen == -1) { return FAILED_NOT_AN_INT; }
            if (!Validator.boundsCheck(0, materialSize[1], currPos[1] - validatedLen)) { return FAILED_BOUNDS_CHECK; }
            return PASS;
        }
        private int TestCutEast(string len)
        {
            int validatedLen = Validator.validateInput(len);
            if (validatedLen == -1) { return FAILED_NOT_AN_INT; }
            if (!Validator.boundsCheck(0, materialSize[0], currPos[0] + validatedLen)) { return FAILED_BOUNDS_CHECK; }
            return PASS;
        }
        private int TestCutWest(string len)
        {
            int validatedLen = Validator.validateInput(len);
            if (validatedLen == -1) { return FAILED_NOT_AN_INT; }
            if (!Validator.boundsCheck(0, materialSize[0], currPos[0] - validatedLen)) { return FAILED_BOUNDS_CHECK; }
            return PASS;
        }

        private int TestSetMaterialBlockDimensionsTest(string x, string y)
        {
            int validatedX = Validator.validateInput(x);
            int validatedY = Validator.validateInput(y);
            if (validatedX == -1 || validatedY == -1)
            {
                return FAILED_NOT_AN_INT;
            }
            else if (!Validator.lowerBoundCheck(lowerBound: 0, validatedX) ||
                !Validator.lowerBoundCheck(lowerBound: 0, validatedY))
            {
                return FAILED_BOUNDS_CHECK;
            }
            return PASS;
        }
        /*
        public int runInstruction(string input)
        {
            string[] parsedInput = Validator.Parser(input);
            if (parsedInput[0] == DimensionString)
            {
                return SetMaterialBlockDimensions(parsedInput[1], parsedInput[2]);
            }
            else if (parsedInput[0] == LocationString)
            {
                if (parsedInput.Length != 3) { return FAILED_PARAM_LENGTH; }
                return SetCutLocation(parsedInput[1], parsedInput[2]);
            }

            else if (parsedInput[0] == CutNorth)
            {
                if (parsedInput.Length != 2) { return FAILED_PARAM_LENGTH; }
                return CutMoveNorth(parsedInput[1]);
            }
            else { return FAILED_INSTRUCTION_NOT_FOUND; }
        }
        */
        public void SetMaterialBlockDimensions(string x, string y)
        {
            int validatedX = Validator.validateInput(x);
            int validatedY = Validator.validateInput(y);
            materialSize[0] = validatedX;
            materialSize[1] = validatedY;
        }
        public void SetCutLocation(string x, string y)
        {
            int validatedX = Validator.validateInput(x);
            int validatedY = Validator.validateInput(y);
            currPos[0] = validatedX;
            currPos[1] = validatedY;
        }
    }
}
