using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachinCuttingApp
{
    public class Cut
    {
        public static readonly int NORTH = 0;
        public static readonly int SOUTH = 1;
        public static readonly int EAST = 2;
        public static readonly int WEST = 3;

        private int startXPos { get; }
        private int startYPos { get; }
        private int endXPos { get; set; }
        private int endYPos { get; set; }

        public Cut(int x1, int y1, int len, int dir)
        {
            this.startXPos = x1;
            this.startYPos = y1;
            this.setEndPos(len: len, dir: dir);
        }

        private void setEndPos(int len, int dir)
        {
            if (dir == NORTH)
            {
                this.endXPos = this.startXPos;
                this.endYPos = this.startYPos - len;
            }
            else if (dir == SOUTH)
            {
                this.endXPos = this.startXPos;
                this.endYPos = this.startYPos + len;
            }
            else if (dir == EAST)
            {
                this.endXPos = this.startXPos - len;
                this.endYPos = this.startYPos;
            }
            else if (dir == WEST)
            {
                this.endXPos = this.startXPos + len;
                this.endYPos = this.startYPos;
            }

        }
    }
}
