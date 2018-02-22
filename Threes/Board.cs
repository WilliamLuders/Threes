using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Threes;

namespace Threes
{
    class Board
    {
        //Fields
        private const int boardSize = Constants.boardSize;
        private int[,] boardTiles = new int[boardSize, boardSize];

        public Board()
        {
            boardTiles = InitializeBoard();
        }

        public void MoveTiles(int dir)
        {
            //iterate over rows/cols, checking if any have valid moves
            //if any have valid moves, specify transition behaviour
            //perform those moves
            //check if deadlock occurs after move, end game if so

            //int[][] vectors = new int[boardSize][]; // array of arrays makes board merge operations invariant on merge direction
            bool rowcol;
            bool posneg;

            switch (dir)
            {
                case 1: // UP
                    rowcol = Constants.COL;
                    posneg = Constants.NEGDIR;
                    break;
                case 2: // DOWN
                    rowcol = Constants.COL;
                    posneg = Constants.POSDIR;
                    break;
                case 3: // RIGHT
                    rowcol = Constants.ROW;
                    posneg = Constants.POSDIR;
                    break;
                case 4: // LEFT
                    rowcol = Constants.ROW;
                    posneg = Constants.NEGDIR;
                    break;
                default:
                    return;
            }
            for (int i = 0; i < boardSize; i++) {
                //vectors[i] = ReadVector(i, rowcol, posneg);
                WriteVector(i, rowcol, posneg, Merge(ReadVector(i,rowcol,posneg)));
            }
            //call merge on these vectors, merge returns transition vector - not bool (all zeros except for position of merge)
            for (int i = 0; i<boardSize; i++)
            {
                //Merge
            } 
        }
        private int[,] InitializeBoard()
        {
            // populate and return this board
            int[,] tempBoard = new int[boardSize, boardSize];
            //coords for placing tiles
            int x, y;
            // RNG for quantity, placement and value of tiles
            Random rndGen = new Random();

            //fill up to 1/3 of the board worth of tiles
            int numTiles = rndGen.Next(1, boardSize * boardSize / 3);
            for (int i = 0; i<numTiles; i++)
            {
                // keep generating coordinates until we find a blank square
                do
                {
                    x = rndGen.Next(1, boardSize);
                    y = rndGen.Next(1, boardSize);
                } while (tempBoard[x, y] != 0);
                //place random value at this empty square
                tempBoard[x, y] = rndGen.Next(1, 3); // need to update to allow spawning 6s, 12, 24s etc.
            }

            return tempBoard;
        }
        private int[] ReadVector(int index, bool rowcol, bool posneg)
        {
            int[] vector = new int[boardSize];

            if (rowcol == Constants.ROW)
            {
                if (posneg == Constants.POSDIR) // RIGHT
                    for (int i = 0; i < boardSize; i++)
                        vector[i] = boardTiles[i, index];
                else if (posneg == Constants.NEGDIR) // LEFT
                    for (int i = boardSize; i > 0; i++)
                        vector[i] = boardTiles[i, index];
            }
            else if (rowcol == Constants.COL)
            {
                if (posneg == Constants.POSDIR) // DOWN
                    for (int i = 0; i < boardSize; i++)
                        vector[i] = boardTiles[index, i];
                else if (posneg == Constants.NEGDIR) // UP
                    for (int i = boardSize; i > 0; i++)
                        vector[i] = boardTiles[index, i];
            }
            return vector;
        }
        private void WriteVector(int index, bool rowcol, bool posneg, int[] vector)
        {
            if (rowcol == Constants.ROW)
            {
                if (posneg == Constants.POSDIR) // RIGHT
                    for (int i = 0; i < boardSize; i++)
                        boardTiles[index, i] = vector[i];
                else if (posneg == Constants.NEGDIR) // LEFT
                    for (int i = boardSize; i > 0; i++)
                        boardTiles[index, i] = vector[i];
            }
            else if (rowcol == Constants.COL)
            {
                if (posneg == Constants.POSDIR) // DOWN
                    for (int i = 0; i < boardSize; i++)
                        boardTiles[index, i] = vector[i];
                else if (posneg == Constants.NEGDIR) // UP
                    for (int i = boardSize; i > 0; i++)
                        boardTiles[index, i] = vector[i];
            }
        }

        private int[] Merge(int[] vector)
        {
            //todo
            //bool[] transition = new bool[boardSize - 1]; // 0th index specifies transition between 0th and 1st tiles etc.
            bool mergeMade = false;
            for (int i = 0; i < boardSize - 1; i++)
            {
                if (!mergeMade)
                {
                    if (CanMerge(vector[i], vector[i + 1]))
                    {
                        //transition[i] = true; // merge made between ith and (i+1)th tiles
                        vector[i] += vector[i + 1];
                        mergeMade = true;
                    }
                }
                else // merge has been made, move all remaining blocks along
                    vector[i] = vector[i + 1];

            }
            if (mergeMade)
                vector[boardSize - 1] = 0; // set last block in row/col to 0
            return vector;
        }
        private bool CanMerge(int i, int j)
        {
            if ((i == 1 && j == 2) || (i == 2) && (j == 1)) // conditions for merging 1 + 2
                return true;
            else if (i == j && i > 2) // merging anything else
                return true;
            else
                return false;
        }
    }
}