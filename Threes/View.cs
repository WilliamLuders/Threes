using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
using System.ComponentModel;
using System.Collections.Generic;
using Windows.UI;
using System;

namespace Threes
{
    public class GameView : INotifyPropertyChanged
    {


        //private Canvas canvas;
        private GameState myGame;
        private Canvas myCanvas;
        public event PropertyChangedEventHandler PropertyChanged;

        private const int boardSize = Constants.boardSize;
        Button[,] boardTiles; // array of buttons
                              //private string viewString;

        Dictionary<int, SolidColorBrush> colorScheme = new Dictionary<int, SolidColorBrush>(); // different color for each tile value
        
        

        public GameView(ref Canvas canvas, GameState game)
        {
            myGame = game;
            myCanvas = canvas;
            boardTiles = new Button[boardSize, boardSize];
            colorScheme.Add(0, new SolidColorBrush(Windows.UI.Colors.White));
            colorScheme.Add(1, new SolidColorBrush(Windows.UI.Colors.LightBlue));
            colorScheme.Add(2, new SolidColorBrush(Windows.UI.Colors.LightSalmon));
            colorScheme.Add(3, new SolidColorBrush(Windows.UI.Colors.LightGreen));
            colorScheme.Add(6, new SolidColorBrush(Windows.UI.Colors.LightGreen));

            var testButton = new Button();

            for (int i = 0; i<boardSize; i++)
            {
                for (int j = 0; j<boardSize; j++)
                {
                    boardTiles[i, j] = new Button
                    {
                        Foreground = new SolidColorBrush(Windows.UI.Colors.White),
                        Height = 45,
                        Width = 45,
                    };
                    myCanvas.Children.Add(boardTiles[i, j]);
                    Canvas.SetLeft(boardTiles[i, j], 50 * j);
                    Canvas.SetTop(boardTiles[i, j], 50 * i);
                }
            }
            UpdateView(); // update tiles with values and corresponding colours
        }

        public void UpdateView()
        {
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    var tile = (Button) myCanvas.Children[myCanvas.Children.IndexOf(boardTiles[i, j])]; //find tile on Canvas

                    tile.Content = myGame.BoardTiles[i, j];
                    if (tile.Content.Equals(0))
                        tile.Content = ""; // don't write 0 on tiles, just make them blank
                    
                        int colorGen = myGame.BoardTiles[i, j]; // use tile value to decide colour
                        byte B = (byte)(200 * (1 - Math.Exp(-colorGen/2)));
                        byte G = (byte)(255 * (Math.Exp(-colorGen/2)));
                        tile.Background = new SolidColorBrush(Color.FromArgb((byte)220, 0, G, B)); // still experimenting with colour generation
                }
            }
        }

        public void GameOver()
        {
            //To do
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
