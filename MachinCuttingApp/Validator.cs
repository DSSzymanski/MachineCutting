using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachinCuttingApp
{
    static class Validator
    {
        /*
         * Takes a string of input and validates
         * to ensure input is integer value
         * and value is positive. Returns
         * valid parsed number or -1.
         */
        public static int validateInput(string input)
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
        public static bool lowerBoundCheck(int lowerBound, int checkVal)
        {
            if (checkVal <= lowerBound) { return false; }
            return true;
        }

        public static bool boundsCheck(int lowerBound, int upperBound, int checkVal)
        {
            if (checkVal >= upperBound || checkVal <= lowerBound) { return false; }
            return true;
        }
    }
}
