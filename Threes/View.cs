using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
using System.ComponentModel;

namespace Threes
{
    public class GameView : INotifyPropertyChanged
    {


        private Canvas myCanvas;
        private GameState myGame;
        private Rectangle rectangle1;
        public event PropertyChangedEventHandler PropertyChanged;

        private const int boardSize = Constants.boardSize;
        int[,] board;
        private string viewString;

        public GameView(Canvas canvas, GameState game)
        {
            myCanvas = canvas;
            myGame = game;
            board = new int[boardSize, boardSize];

            //testing
            rectangle1 = new Rectangle
            {
                Fill = new SolidColorBrush(Windows.UI.Colors.Blue),
                Width = 200,
                Height = 100,
                Stroke = new SolidColorBrush(Windows.UI.Colors.Black),
                StrokeThickness = 3,
                RadiusX = 50,
                RadiusY = 10
            };

            myCanvas.Children.Add(rectangle1);
        }

        /*public string ViewString {
            get
            {
                string viewStringBuilder = "";
                board = myGame.BoardTiles; // I think = is overridden in this case...?
                for (int i = 0; i < boardSize; i++)
                {
                    for (int j = 0; j < boardSize; j++)
                    {
                        viewStringBuilder += board[i, j] + " ";
                    }
                    viewStringBuilder += "\n";
                }

                viewString = viewStringBuilder;
                return viewString;
            }
            set
            {
                viewString = value;
                OnPropertyChanged("ViewString");
            }
        }*/

        public string UpdateView()
        {
            string viewStringBuilder = "";
            board = myGame.BoardTiles; // I think = is overridden in this case...?
            for (int i=0; i<boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    viewStringBuilder += board[i, j] + "   ";
                }
                viewStringBuilder += "\n\n";
            }
            
            //testing
            rectangle1.RadiusX = 10;

            return viewStringBuilder;
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
