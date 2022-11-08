using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace CanvasScrollMap
{
    public class SolutionGenerator
    {
        private readonly long durationSeconds;

        private readonly Random random;

        private readonly Brush[] palette = new[]
        {
            Brushes.PaleGreen,
            Brushes.PaleTurquoise,
            Brushes.PaleVioletRed,
            Brushes.PaleGoldenrod,
            Brushes.SkyBlue,
            Brushes.PeachPuff
        };

        private long elapsedSeconds;

        public SolutionGenerator(long durationSeconds, Random random)
        {
            this.durationSeconds = durationSeconds;
            this.random = random;
        }

        public Solution Generate()
        {
            return new Solution
            {
                Casters = GenerateCasters().ToArray(),
                DurationSeconds = durationSeconds,
            };
        }
        private IEnumerable<Caster> GenerateCasters()
        {
            for (int i = 0; i < 2; i++)
            {
                yield return GenerateCaster();
            }
        }

        private Caster GenerateCaster()
        {
            elapsedSeconds = 0;
            return new Caster
            {
                Sequences = GenerateSequences().Where(s => s.Tundishes.Length > 0).ToArray()
            };
        }

        private IEnumerable<Sequence> GenerateSequences()
        {

            while (elapsedSeconds < durationSeconds)
            {
                // Pre sequence time
                elapsedSeconds += 10 * 60;
                yield return new Sequence
                {
                    Tundishes = GenerateTundishes().Where(s => s.Ladles.Length > 0).ToArray()
                };
            }
        }

        private IEnumerable<Tundish> GenerateTundishes()
        {
            var maxTundishCount = random.NextInt64(1, 4);
            int tundishCount = 0;
            while (tundishCount < maxTundishCount && elapsedSeconds < durationSeconds)
            {
                // Pre tundish time
                elapsedSeconds += 5 * 60;
                yield return new Tundish
                {
                    Ladles = GenerateLadles().ToArray()
                };
                tundishCount++;
            }
        }

        private IEnumerable<Ladle> GenerateLadles()
        {
            var maxLadleCount = random.NextInt64(1, 8);
            int ladleCount = 0;

            while (ladleCount < maxLadleCount && elapsedSeconds < durationSeconds)
            {
                var ladleDuration = random.NextInt64(15 * 60, 45 * 60);
                long remainingSeconds = durationSeconds - elapsedSeconds;

                ladleDuration = Math.Min(ladleDuration, remainingSeconds);
                yield return new Ladle
                {
                    StartSecond = elapsedSeconds,
                    DurationSeconds = ladleDuration,
                    EndSecond = elapsedSeconds + ladleDuration,
                    Color = palette[random.Next(palette.Length)]
                };

                elapsedSeconds += ladleDuration;
                ladleCount++;
            }
        }
    }
}
