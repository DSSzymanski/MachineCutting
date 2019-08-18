using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachinCuttingApp
{
    class Controler
    {
        private int[] materialSize { get; }
        private int[] currPos { get; }
        private List<Cut> cutList = new List<Cut>();

        public Controler()
        {
            materialSize = new int[2] { 0, 0 };
            currPos = new int[2] { 0, 0 };
        }
        
        public void SetCutLocation(string x, string y)
        {
            int validatedX = Validator.validateInput(x);
            int validatedY = Validator.validateInput(y);
            if (validatedX == -1 || validatedY == -1)
            {
                //invalid input error msg
            }
            else if (!Validator.boundsCheck(lowerBound: 0, upperBound: materialSize[0], validatedX) ||
                !Validator.boundsCheck(lowerBound: 0, upperBound: materialSize[1], validatedY))
            {
                //out of bounds error msg
            }
            else
            {
                currPos[0] = validatedX;
                currPos[1] = validatedY;
            }
        }

        public void SetMaterialBlockDimensions(string x, string y)
        {
            int validatedX = Validator.validateInput(x);
            int validatedY = Validator.validateInput(y);
            if (validatedX == -1 || validatedY == -1)
            {
                //invalid input error msg
            }
            else if (!Validator.lowerBoundCheck(lowerBound: 0, validatedX) ||
                !Validator.lowerBoundCheck(lowerBound: 0, validatedY))
            {
                //out of bounds error msg; must be greater than 0
            }
            else
            {
                materialSize[0] = validatedX;
                materialSize[1] = validatedY;
            }
        }
    }
}
