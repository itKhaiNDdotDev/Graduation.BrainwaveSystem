using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation.BrainwaveSystem.Models.DTOs.AIModels
{
    public class RawEEGPredictResponse
    {
        public float[] ForecastedValues { get; set; }

        public float[] LowerBoundValues { get; set; }

        public float[] UpperBoundValues { get; set; }

        public List<DateTime> PredictTimes { get; set; }

        public float MAE { get; set; }
        public double RMSE { get; set; }
    }
}
