using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Threes
{
    static class Constants
    {
        public const int boardSize = 4;
        public enum DIRS { UP = 1, DOWN, RIGHT, LEFT };
        public const bool ROW = false;
        public const bool COL = true;
        public const bool POSDIR = true;
        public const bool NEGDIR = false;


        // probably will use Tuples

        //internal enum UP    { X = 0,  Y = 1  };
        //internal enum DOWN  { X = 0,  Y = -1 };
        //internal enum RIGHT { X = 1,  Y = 0  };
        //internal enum LEFT  { X = -1, Y = 0  };

        //internal enum MOVES { LEFT = 0b00, RIGHT = 0b01,  DOWN = 0b10, UP = 0b11 };
    }
}
