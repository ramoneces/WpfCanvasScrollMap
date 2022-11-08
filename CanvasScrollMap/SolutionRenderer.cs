using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CanvasScrollMap
{
    public class SolutionRenderer
    {
        private readonly Solution solution;

        private readonly Canvas canvas;

        private const double levelPaddingTop = 10;

        private double casterHeight;

        public SolutionRenderer(Solution solution, Canvas canvas)
        {
            this.solution = solution;
            this.canvas = canvas;
        }

        public void Render()
        {
            casterHeight = canvas.ActualHeight / solution.Casters.Length;

            double casterTop = 0;
            foreach (var caster in solution.Casters)
            {
                RenderCaster(casterTop, caster);
                casterTop += casterHeight;
            }
        }

        private void RenderCaster(double casterTop, Caster caster)
        {
            var sequenceTop = casterTop + levelPaddingTop;
            double sequenceHeight = casterHeight - levelPaddingTop;
            foreach (var sequence in caster.Sequences)
            {
                RenderSequence(sequenceTop, sequenceHeight, sequence);
            }
        }

        private void RenderSequence(double sequenceTop, double sequenceHeight, Sequence sequence)
        {
            var sequenceView = new Rectangle
            {
                Fill = Brushes.Peru,
                Opacity = 0.5,
                Height = sequenceHeight,
                Width = TimeToX(sequence.DurationSeconds),
            };
            canvas.Children.Add(sequenceView);
            Canvas.SetTop(sequenceView, sequenceTop);
            Canvas.SetLeft(sequenceView, TimeToX(sequence.StartSecond));

            var tundishTop = sequenceTop + levelPaddingTop;
            double tundishHeight = sequenceHeight - levelPaddingTop;
            foreach (var tundish in sequence.Tundishes)
            {
                RenderTundish(tundishTop, tundishHeight, tundish);
            }
        }

        private void RenderTundish(double tundishTop, double tundishHeight, Tundish tundish)
        {
            var tundishView = new Rectangle
            {
                Fill = Brushes.Peru,
                Opacity = 0.5,
                Height = tundishHeight,
                Width = TimeToX(tundish.DurationSeconds),
            };
            canvas.Children.Add(tundishView);
            Canvas.SetTop(tundishView, tundishTop);
            Canvas.SetLeft(tundishView, TimeToX(tundish.StartSecond));

            var ladleTop = tundishTop + levelPaddingTop;
            double ladleHeight = tundishHeight - levelPaddingTop;
            foreach (var ladle in tundish.Ladles)
            {
                RenderLadle(ladleTop, ladleHeight, ladle);
            }
        }

        private void RenderLadle(double ladleTop, double ladleHeight, Ladle ladle)
        {
            var ladleView = new Rectangle
            {
                Fill = ladle.Color,
                Height = ladleHeight,
                Width = TimeToX(ladle.DurationSeconds),
            };
            canvas.Children.Add(ladleView);
            Canvas.SetTop(ladleView, ladleTop);
            Canvas.SetLeft(ladleView, TimeToX(ladle.StartSecond));
        }

        private double TimeToX(long seconds)
        {
            return seconds * canvas.ActualWidth / solution.DurationSeconds;
        }
    }
}
