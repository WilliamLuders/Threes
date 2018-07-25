using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Threes
{
    public class GameState : INotifyPropertyChanged
    {
        //Fields
        private int score;
        public event PropertyChangedEventHandler PropertyChanged;

        //public int size; // IDEA: dynamic array size depending on progression through single game/achievements
        //private Board gameBoard;
        //Fields
        private const int boardSize = Constants.boardSize;
        private int[,] boardTiles = new int[boardSize, boardSize];

        public int[,] BoardTiles {
            get => boardTiles;
        }

        public GameState()
        {
            score = 0;
            boardTiles = InitializeBoard();
            
        }
        public int Score { get => score; }
        //public int[,] GetBoard() => boardTiles;

        public bool MoveTiles(int dir)
        {
            //iterate over rows/cols, checking if any have valid moves
            //if any have valid moves, specify transition behaviour
            //perform those moves
            //check if deadlock occurs after move, end game if so

            int[][] vectors = new int[boardSize][]; // array of arrays makes board merge operations invariant on merge direction
            for(int i = 0; i<boardSize; i++)
            {
                vectors[i] = new int[boardSize];
            }
            bool rowcol;
            bool posneg;
            bool moveOccurred = false;

            switch (dir)
            {
                case (int)Constants.DIRS.UP:
                    rowcol = Constants.COL;
                    posneg = Constants.NEGDIR;
                    break;
                case (int)Constants.DIRS.DOWN:
                    rowcol = Constants.COL;
                    posneg = Constants.POSDIR;
                    break;
                case (int)Constants.DIRS.RIGHT:
                    rowcol = Constants.ROW;
                    posneg = Constants.POSDIR;
                    break;
                case (int)Constants.DIRS.LEFT:
                    rowcol = Constants.ROW;
                    posneg = Constants.NEGDIR;
                    break;
                default: // default should never occur, just need one to make compiler happy
                    rowcol = Constants.ROW;
                    posneg = Constants.NEGDIR;
                    break; 
            }
            for (int i = 0; i < boardSize; i++)
            {
                ReadVector(i, rowcol, posneg, vectors[i]);
                if (Merge(vectors[i])) moveOccurred = true ;
                WriteVector(i, rowcol, posneg, vectors[i]);
            }
            //call merge on these vectors, merge returns transition vector - not bool (all zeros except for position of merge)
            for (int i = 0; i < boardSize; i++)
            {
                //Merge
            }

            //inform view that new board is available
            OnPropertyChanged("BoardTiles");
            return moveOccurred;
        }
        public void SpawnTile(int dir)
        {
            int i;
            Random rndGen = new Random();

            switch (dir)
            {
                case (int)Constants.DIRS.UP:
                    // keep generating coordinates until we find a blank square along specific edge
                    do
                    {
                        i = rndGen.Next(0, boardSize);
                    } while (boardTiles[boardSize - 1, i] != 0); // pick empty square along bottom
                    boardTiles[boardSize - 1, i] = rndGen.Next(1, 4);
                    break;
                case (int)Constants.DIRS.DOWN:
                    do
                    {
                        i = rndGen.Next(0, boardSize);
                    } while (boardTiles[0, i] != 0); // pick empty square along top
                    boardTiles[0, i] = rndGen.Next(1, 4);
                    break;
                case (int)Constants.DIRS.LEFT:
                    do
                    {
                        i = rndGen.Next(0, boardSize);
                    } while (boardTiles[i, boardSize - 1] != 0); // pick empty square along right
                    boardTiles[i, boardSize - 1] = rndGen.Next(1, 4);
                    break;
                case (int)Constants.DIRS.RIGHT:
                    do
                    {
                        i = rndGen.Next(0, boardSize);
                    } while (boardTiles[i, 0] != 0); // pick empty square along left
                    boardTiles[i, 0] = rndGen.Next(1, 4);
                    break;
            }
        }
        private void ReadVector(int index, bool rowcol, bool posneg, int[] vector)
        {
            if (rowcol == Constants.ROW)
            {
                if (posneg == Constants.POSDIR) // RIGHT
                {
                    for (int i = 0; i < boardSize; i++)
                    {
                        vector[boardSize-i-1] = boardTiles[index, i];
                    }
                }
                else if (posneg == Constants.NEGDIR) // LEFT
                {
                    for (int i = boardSize-1; i >= 0; i--)
                    {
                        vector[i] = boardTiles[index, i];
                    }
                }
            }
            else if (rowcol == Constants.COL)
            {
                if (posneg == Constants.POSDIR) // DOWN
                    for (int i = 0; i < boardSize; i++)
                        vector[boardSize-1-i] = boardTiles[i, index];
                else if (posneg == Constants.NEGDIR) // UP
                    for (int i = boardSize-1; i >= 0; i--)
                        vector[i] = boardTiles[i, index];
            }
        }
        private void WriteVector(int index, bool rowcol, bool posneg, int[] vector)
        {
            if (rowcol == Constants.ROW)
            {
                if (posneg == Constants.POSDIR) // RIGHT
                    for (int i = 0; i < boardSize; i++)
                        boardTiles[index, i] = vector[boardSize-1-i];
                else if (posneg == Constants.NEGDIR) // LEFT
                    for (int i = boardSize-1; i >= 0; i--)
                        boardTiles[index, i] = vector[i];
            }
            else if (rowcol == Constants.COL)
            {
                if (posneg == Constants.POSDIR) // DOWN
                    for (int i = 0; i < boardSize; i++)
                        boardTiles[i, index] = vector[boardSize-1-i];
                else if (posneg == Constants.NEGDIR) // UP
                    for (int i = boardSize-1; i >= 0; i--)
                        boardTiles[i, index] = vector[i];
            }
        }
        private bool Merge(int[] vector)
        {
            //todo
            //bool[] transition = new bool[boardSize - 1]; // 0th index specifies transition between 0th and 1st tiles etc.
            bool moveMade = false;
            for (int i = 0; i < boardSize - 1; i++)
            {
                if (!moveMade)
                {
                    if (CanMerge(vector[i], vector[i + 1]))
                    {
                        //transition[i] = true; // merge made between ith and (i+1)th tiles
                        vector[i] += vector[i + 1];
                        vector[i + 1] = 0;
                        moveMade = true;
                    }
                    else if (vector[i] == 0)
                    {
                        vector[i] = vector[i + 1];
                        if (vector[i + 1] != 0) moveMade = true; // if block is moved along, move was made
                        vector[i + 1] = 0;
                    }
                }
                else // merge has been made, move all remaining blocks along
                    vector[i] = vector[i + 1];

            }
            if (moveMade)
                vector[boardSize - 1] = 0; // set last block in row/col to 0
            return moveMade;
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
        private int[,] InitializeBoard()
        {
            // populate and return this board
            int[,] tempBoard = new int[boardSize, boardSize];
            //coords for placing tiles
            int x, y;
            // RNG for quantity, placement and value of tiles
            Random rndGen = new Random();

            //fill up to 1/3 of the board worth of tiles
            int numTiles = rndGen.Next(1, (boardSize * boardSize) / 3+1);
            for (int i = 0; i < numTiles; i++)
            {
                // keep generating coordinates until we find a blank square
                do
                {
                    x = rndGen.Next(0, boardSize);
                    y = rndGen.Next(0, boardSize);
                } while (tempBoard[x, y] != 0);
                //place random value at this empty square
                tempBoard[x, y] = rndGen.Next(1, 3); // need to update to allow spawning 6s, 12, 24s etc.
            }

            return tempBoard;
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

    }
}
