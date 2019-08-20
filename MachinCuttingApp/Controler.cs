using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachinCuttingApp
{
    public class Controler
    {
        public static string DimensionString = "SetMaterialBlockDimensions";
        public static string LocationString = "SetCutLocation";
        public static string CutNorth = "CutMoveNorth";
        public static string CutSouth = "CutMoveSouth";
        public static string CutEast = "CutMoveEast";
        public static string CutWest = "CutMoveWest";

        public static int FAILED_NOT_AN_INT = -1;
        public static int FAILED_BOUNDS_CHECK = -2;
        public static int FAILED_PARAM_LENGTH = -3;
        public static int FAILED_INSTRUCTION_NOT_FOUND = -4;
        public static int PASS = 1;
        private int[] materialSize { get; }
        private int[] currPos { get; }
        private List<Cut> cutList = new List<Cut>();

        public Controler()
        {
            materialSize = new int[2] { 0, 0 };
            currPos = new int[2] { 0, 0 };
        }

        public string materialString() { return $"({materialSize[0]},{materialSize[1]})"; }
        public string locationString() { return $"({currPos[0]},{currPos[1]})"; }
        public int runInstruction(string input)
        {
            string[] parsedInput = Validator.Parser(input);
            if (parsedInput[0] == DimensionString)
            {
                if (parsedInput.Length != 3) { return FAILED_PARAM_LENGTH; }
                return SetMaterialBlockDimensions(parsedInput[1], parsedInput[2]);
            }
            else if (parsedInput[0] == LocationString)
            {
                if (parsedInput.Length != 3) { return FAILED_PARAM_LENGTH; }
                return SetCutLocation(parsedInput[1], parsedInput[2]);
            }
            /*
            else if (parsedInput[0] == CutNorth)
            {
                if (parsedInput.Length != 2) { return; }
                CutMoveNorth(parsedInput[1]);
            }
            else if (parsedInput[0] == CutSouth)
            {
                if (parsedInput.Length != 2) { return; }
                CutMoveSouth(parsedInput[1]);
            }
            else if (parsedInput[0] == CutEast)
            {
                if (parsedInput.Length != 2) { return; }
                CutMoveEast(parsedInput[1], parsedInput[2]);
            }
            else if (parsedInput[0] == CutWest)
            {
                if (parsedInput.Length != 2) { return; }
                CutMoveWest(parsedInput[1], parsedInput[2]);
            }
            */
            else { return FAILED_INSTRUCTION_NOT_FOUND; }
        }

        private int SetCutLocation(string x, string y)
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
            currPos[0] = validatedX;
            currPos[1] = validatedY;
            return PASS;
        }

        private int SetMaterialBlockDimensions(string x, string y)
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
            materialSize[0] = validatedX;
            materialSize[1] = validatedY;
            return PASS;
        }
    }
}
