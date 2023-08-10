using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation.BrainwaveSystem.Models.DTOs.AIModels
{
    public class AwakeStateFastTreeResponse
    {
        public bool? PredictionValue { get; set; }
        public string StateLabel { get; set; }
        public float? Probability { get; set; }
    }
}
