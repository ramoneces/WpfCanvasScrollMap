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
    public partial class SolutionMap : UserControl
    {

        public Solution Solution
        {
            get { return (Solution)GetValue(SolutionProperty); }
            set { SetValue(SolutionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Solution.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SolutionProperty =
            DependencyProperty.Register("Solution", typeof(Solution), typeof(SolutionMap), new PropertyMetadata(null, OnSolutionChanged));

        private static void OnSolutionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var instance = d as SolutionMap;
            if(instance.Solution != null)
            {
                RenderSolution(instance);
            }

        }

        private static void RenderSolution(SolutionMap? instance)
        {
            instance.MapCanvas.Children.Clear();
            new SolutionRenderer(instance.Solution, instance.MapCanvas).Render();
        }



        public SolutionMap()
        {
            InitializeComponent();
        }
    }
}
