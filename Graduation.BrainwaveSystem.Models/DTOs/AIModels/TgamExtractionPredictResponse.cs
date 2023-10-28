using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation.BrainwaveSystem.Models.DTOs.AIModels
{
    public class TgamExtractionPredictResponse
    {
        public float[] PoorQuality { get; set; }

        public float[] Attention { get; set; }

        public float[] Meditation { get; set; }

        public float[] Delta { get; set; }
        public float[] Theta { get; set; }
        public float[] Alpha { get; set; }
        public float[] LowBeta { get; set; }
        public float[] MidBeta { get; set; }
        public float[] HighBeta { get; set; }
        public float[] Gamma { get; set; }
        public float[] UHFGamma { get; set; }

        public List<DateTime> PredictTimes { get; set; }

        public float MAEGeneral { get; set; }
        public double RMSEGeneral { get; set; }

        public float MAEFor8Band { get; set; }
        public double RMSEFor8Band { get; set; }
    }
}
