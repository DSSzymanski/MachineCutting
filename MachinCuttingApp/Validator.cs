using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachinCuttingApp
{
    public static class Validator
    {
        public static readonly int VALID = 0;
        public static readonly int INVALID_PARAMS = 1;
        public static string[] Parser(string input)
        {
            string[] separator = { " ", "\'", "\"", "/", "\\", ".", "\r", "\n", ";" };
            string[] parsed = input.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            
            return parsed;
        }
        /*
         * Takes a string of input and validates
         * to ensure input is integer value
         * and value is positive. Returns
         * valid parsed number or -1.
         */
        public static int ValidateInput(string input)
        {
            try
            {
                int len = Int32.Parse(input);
                return len;
            }
            catch (FormatException)
            {
                return -1;
            }
        }
        public static bool LowerBoundCheck(int lowerBound, int checkVal)
        {
            if (checkVal < lowerBound) { return false; }
            return true;
        }

        public static bool BoundsCheck(int lowerBound, int upperBound, int checkVal)
        {
            if (checkVal <= upperBound && checkVal >= lowerBound) { return true; }
            return false;
        }
    }
}
