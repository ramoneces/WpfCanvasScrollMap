using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CanvasScrollMap
{
    public class Solution
    {
        public Caster[] Casters { get; set; }
        public long DurationSeconds { get; set; }
    }

    public class Caster
    {
        public Sequence[] Sequences { get; set; }

    }

    public class Sequence
    {
        public Tundish[] Tundishes { get; set; }
        public long StartSecond => Tundishes.First().StartSecond;
        public long DurationSeconds => EndSecond - StartSecond;
        public long EndSecond => Tundishes.Last().EndSecond;
    }

    public class Tundish
    {
        public Ladle[] Ladles { get; set; }
        public long StartSecond => Ladles.First().StartSecond;
        public long DurationSeconds => EndSecond - StartSecond;
        public long EndSecond => Ladles.Last().EndSecond;
    }

    public class Ladle
    {
        public Brush Color { get; set; }
        public long DurationSeconds { get; set; }
        public long StartSecond { get; set; }
        public long EndSecond { get; set; }
    }
}
