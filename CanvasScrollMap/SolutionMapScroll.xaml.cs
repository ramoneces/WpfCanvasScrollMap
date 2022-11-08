using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CanvasScrollMap
{
    /// <summary>
    /// Lógica de interacción para SolutionScrollMap.xaml
    /// </summary>
    public partial class SolutionMapScroll : UserControl
    {
        public Solution Solution
        {
            get { return (Solution)GetValue(SolutionProperty); }
            set { SetValue(SolutionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Solution.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SolutionProperty =
            DependencyProperty.Register("Solution", typeof(Solution), typeof(SolutionMapScroll), new PropertyMetadata(null));

        public SolutionMapScroll()
        {
            InitializeComponent();

            Point? dragStart = null;

            MouseButtonEventHandler mouseDown = (sender, args) =>
            {
                var element = (UIElement)sender;
                dragStart = args.GetPosition(element);
                element.CaptureMouse();
            };

            MouseButtonEventHandler mouseUp = (sender, args) =>
            {
                var element = (UIElement)sender;
                dragStart = null;
                element.ReleaseMouseCapture();
            };

            MouseEventHandler mouseMove = (sender, args) =>
            {
                if (dragStart != null && args.LeftButton == MouseButtonState.Pressed)
                {
                    var element = (Rectangle)sender;
                    var p2 = args.GetPosition(OverlayCanvas);
                    double newX = Math.Min(Math.Max(p2.X - dragStart.Value.X, 0), OverlayCanvas.ActualWidth- element.ActualWidth);
                    Canvas.SetLeft(element, newX);
                    SelectionXChanged?.Invoke(newX / OverlayCanvas.ActualWidth);
                }
            };

            SelectionRectangle.MouseDown += mouseDown;
            SelectionRectangle.MouseMove += mouseMove;
            SelectionRectangle.MouseUp += mouseUp;
        }

        public void SetSelectionSize(double startPercent, double widthPercent)
        {
            SelectionRectangle.Width = widthPercent * OverlayCanvas.ActualWidth;
            SelectionRectangle.Height = OverlayCanvas.ActualHeight;
            Canvas.SetLeft(SelectionRectangle, startPercent * OverlayCanvas.ActualWidth);
        }

        public event Action<double> SelectionXChanged;

        private void SolutionMap_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var point = e.GetPosition(OverlayCanvas);
            double newX = Math.Min(Math.Max(point.X - SelectionRectangle.ActualWidth / 2, 0), OverlayCanvas.ActualWidth - SelectionRectangle.ActualWidth) ;
            Canvas.SetLeft(SelectionRectangle, newX);
            SelectionXChanged?.Invoke(newX / OverlayCanvas.ActualWidth);
        }
    }
}
