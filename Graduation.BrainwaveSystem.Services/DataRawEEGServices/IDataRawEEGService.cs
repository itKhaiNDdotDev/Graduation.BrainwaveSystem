﻿using Graduation.BrainwaveSystem.Models.DTOs;
using Graduation.BrainwaveSystem.Models.Entities;
using Graduation.BrainwaveSystem.Services.BaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation.BrainwaveSystem.Services.DataRawEEGServices
{
    public interface IDataRawEEGService : IBaseService<DataRawEEG, DataRawEEGRequest>
    {
        public Task<DataRawEEGResponse[]> GetLastNDataRecords(Guid deviceId, int n = 1);

        public (List<double> frequencyAxis, List<double> amplitudeSpectrum) GetFFTData(Guid deviceId);
    }
}
