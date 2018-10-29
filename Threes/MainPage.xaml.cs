using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
using Threes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Threes
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public GameState game;
        public GameView view;
        public MainPage()
        {
            this.InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Start.Content = "Restart";
            MyCanvas.Visibility = Visibility.Visible;
            while (MyCanvas.Children.Count != 0)
                MyCanvas.Children.RemoveAt(0);
            //try // if game object have not been created
            //{
            //    if (game.Equals(null))
            //        game = new GameState();
            //    if (view.Equals(null))
            //        view = new GameView(ref MyCanvas, game);
            //}
            
                game = new GameState();
                view = new GameView(ref MyCanvas, game);
            GC.Collect(); // remove old game objects if we restarted
        }

        public int Score() => game.Score;

        private void Button_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            //label1.Text = "Button pressed";


            if (game != null && !game.GameOver)
            {
                int dir = 0;
                
                switch (e.Key)
                {
                    case VirtualKey.Up:     dir = (int)Constants.DIRS.UP;       break;
                    case VirtualKey.Down:   dir = (int)Constants.DIRS.DOWN;     break;
                    case VirtualKey.Left:   dir = (int)Constants.DIRS.LEFT;     break;
                    case VirtualKey.Right:  dir = (int)Constants.DIRS.RIGHT;    break;
                }
                
                // move tiles based on button pressed. If tiles merged during move, spawn a new tile on empty edge caused by move 
                if (game.MoveTiles(dir))
                {
                    game.SpawnTile(dir);
                    if (!game.MoveAvailable())
                    {
                        view.GameOver();
                    }
                }
                    
            }
            view.UpdateView();
        }

        private void Button_PointerEntered(object sender, PointerRoutedEventArgs e)
        {

        }
    }
}
