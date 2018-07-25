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
            //Start.Visibility = Visibility.Collapsed;
            MyCanvas.Visibility = Visibility.Visible;
            game = new GameState();
            view = new GameView(MyCanvas, game);

            label1.Text = view.UpdateView();

        }

        public int Score() => game.Score;

        private void Button_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            //label1.Text = "Button pressed";
            if (game != null)
            {
                switch (e.Key)
                {
                    case VirtualKey.Up: if(game.MoveTiles((int)Constants.DIRS.UP)) game.SpawnTile((int)Constants.DIRS.UP); break;
                    case VirtualKey.Down: if (game.MoveTiles((int)Constants.DIRS.DOWN)) game.SpawnTile((int)Constants.DIRS.DOWN); break;
                    case VirtualKey.Left: if (game.MoveTiles((int)Constants.DIRS.LEFT)) game.SpawnTile((int)Constants.DIRS.LEFT); break;
                    case VirtualKey.Right: if (game.MoveTiles((int)Constants.DIRS.RIGHT)) game.SpawnTile((int)Constants.DIRS.RIGHT); break;
                }
            }
            label1.Text = view.UpdateView();
        }
    }
}
