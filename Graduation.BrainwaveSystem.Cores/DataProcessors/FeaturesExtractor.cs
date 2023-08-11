using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation.BrainwaveSystem.Cores.DataProcessors
{
    public class FeaturesExtractor
    {
        public static AwakeState10Feature ExtractAwakeState(List<int> rawData)
        {
            var fftRes = Transformer.TransformFFT(rawData);
            int L = fftRes.spectrums.Count;
            int fs = 512;
            for (int i = 0; i < L; i++)
            {
                fftRes.freqs[i] = i * fs / L;
            }

            var result = new AwakeState10Feature();
            List<int> indices = fftRes.freqs.Select((value, index) => new { Value = value, Index = index })
                                    .Where(pair => pair.Value >= 0.5 && pair.Value <= 4)
                                    .Select(pair => pair.Index).ToList();
            double delta = indices.Select(index => fftRes.spectrums[index]).Sum();
            result.Delta = delta;

            indices = fftRes.freqs.Select((value, index) => new { Value = value, Index = index })
                          .Where(pair => pair.Value >= 4 && pair.Value <= 8)
                          .Select(pair => pair.Index).ToList();
            double theta = indices.Select(index => fftRes.spectrums[index]).Sum();
            result.Theta = theta;

            indices = fftRes.freqs.Select((value, index) => new { Value = value, Index = index })
                          .Where(pair => pair.Value >= 8 && pair.Value <= 13)
                          .Select(pair => pair.Index).ToList();
            double alpha = indices.Select(index => fftRes.spectrums[index]).Sum();
            result.Alpha= alpha;

            indices = fftRes.freqs.Select((value, index) => new { Value = value, Index = index })
                          .Where(pair => pair.Value >= 13 && pair.Value <= 30)
                          .Select(pair => pair.Index).ToList();
            double beta = indices.Select(index => fftRes.spectrums[index]).Sum();
            result.Beta = beta;

            double abr = alpha / beta;
            result.Abr = abr;

            double tbr = theta / beta;
            result.Tbr = tbr;

            double dbr = delta / beta;
            result.Dbr = dbr;

            double tar = theta / alpha;
            result.Tar = tar;

            double dar = delta / alpha;
            result.Dar = dar;

            double dtabr = (alpha + beta) / (delta + theta);
            result.Dtabr = dtabr;

            return result;
        }
    }

    public class AwakeState10Feature
    {
        public double Delta { get; set; }
        public double Theta { get; set; }
        public double Alpha { get; set; }
        public double Beta { get; set; }
        public double Abr { get; set; }
        public double Tbr { get; set; }
        public double Dbr { get; set; }
        public double Tar { get; set; }
        public double Dar { get; set; }
        public double Dtabr { get; set; }
    }
}
