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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Solution Solution
        {
            get { return (Solution)GetValue(SolutionProperty); }
            set { SetValue(SolutionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Solution.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SolutionProperty =
            DependencyProperty.Register("Solution", typeof(Solution), typeof(MainWindow), new PropertyMetadata(null));

        public MainWindow()
        {
            InitializeComponent();

        }

        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            Solution = new SolutionGenerator((long)TimeSpan.FromDays(3).TotalSeconds, new Random()).Generate();
        }

        private void Scroll_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            var scrollStartPercent = e.HorizontalOffset / e.ExtentWidth;
            var scrollWidthPercent = e.ViewportWidth / e.ExtentWidth;
            MapScroll.SetSelectionSize(scrollStartPercent, scrollWidthPercent);
        }

        private void MapScroll_SelectionXChanged(double newXPercent)
        {
            Scroll.ScrollToHorizontalOffset(newXPercent * Scroll.ExtentWidth);
        }
    }
}
