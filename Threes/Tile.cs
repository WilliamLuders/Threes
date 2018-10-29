using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threes
{
    class Tile
    {
        private int x, y;
        private int topCoord, leftCoord;
        private int value;


        public Tile(int xIndex, int yIndex, int xPos, int yPos, int val)
        {
            x = xIndex;
            y = yIndex;
            topCoord = yPos;
            leftCoord = xPos;
            value = val;
        }
    }
}
