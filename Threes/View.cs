using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace Threes
{
    public class GameView
    {
        Canvas myCanvas;
        Rectangle rectangle1;
        public GameView(Canvas canvas)
        {
            myCanvas = canvas;
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
        public void UpdateView()
        {
            rectangle1.RadiusX = 10;
        }
    }
}
