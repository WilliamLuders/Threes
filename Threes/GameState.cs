using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threes
{
    public class GameState
    {
        //Fields
        public int score;
        public int size; // IDEA: dynamic array size depending on progression through single game/achievements
        private Board gameBoard;

        public GameState()
        {
            score = 0;
            gameBoard = new Board();
            
        }

    }
}
