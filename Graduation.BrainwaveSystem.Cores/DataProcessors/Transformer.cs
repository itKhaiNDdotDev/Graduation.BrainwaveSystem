using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics;
using MathNet.Numerics.IntegralTransforms;

namespace Graduation.BrainwaveSystem.Cores.DataProcessors
{
    /// <summary>
    /// Các bộ biến đổi tín hiệu số
    /// </summary>
    /// (KhaiND - 05/2023)
    public class Transformer
    {
        public static (List<double> frequencyAxis, List<double> amplitudeSpectrum) SolveFFT(List<int> listByteTimeDomainSignal, List<DateTime> listTime)
        {
            //// Chuỗi tín hiệu điện theo thời gian
            //double[]  = { 1.0, 2.0, 3.0, 4.0 }; // Ví dụ: tín hiệu gồm các giá trị 1, 2, 3, 4

            //// Thời gian tương ứng với mỗi mẫu trong chuỗi tín hiệu
            //double[] time = { 0.0, 1.0, 2.0, 3.0 }; // Ví dụ: tín hiệu được lấy mẫu cách nhau 1 giây

            // Số mẫu trong cửa sổ FFT
            int windowSize = 512 * 15;

            // Số lượng mẫu và tần số lấy mẫu
            int sampleCount = listByteTimeDomainSignal.Count;
            double sampleRate = 1.0 / (listTime[1] - listTime[0]).TotalSeconds;

            // Tính số lượng cửa sổ FFT
            int windowCount = sampleCount / windowSize;

            // Chuyển đổi dữ liệu đầu vào thành mảng Complex32
            Complex32[] timeDomainSignal = listByteTimeDomainSignal.Select(x => new Complex32(x, 0)).ToArray();

            // Chuẩn bị dữ liệu đầu ra
            List<double> frequencyAxis = new List<double>();
            List<double> amplitudeSpectrum = new List<double>();

            // Tính toán FFT cho từng cửa sổ
            for (int i = 0; i < windowCount; i++)
            {
                //// Tạo cửa sổ FFT từ chuỗi tín hiệu
                //double[] timeDomainSignal = new double[windowSize];
                //for (int j = 0; j < windowSize; j++)
                //{
                //    timeDomainSignal[j] = listByteTimeDomainSignal[i * windowSize + j];
                //}

                // Tạo cửa sổ FFT từ chuỗi tín hiệu
                Complex32[] window = timeDomainSignal.Skip(i * windowSize).Take(windowSize).ToArray();

                // Thực hiện FFT
                Fourier.Forward(window, FourierOptions.Default);

                //// Thực hiện FFT
                //Complex[] frequencyDomainSignal = new Complex[windowSize];
                //Fourier.Forward(timeDomainSignal, FourierOptions.Default);

                //// Chuẩn bị dữ liệu đầu ra biên độ
                //double[] amplitudeSpectrumWindow = new double[windowSize];

                //// Tính toán biên độ của phổ tần số
                //for (int j = 0; j < windowSize; j++)
                //{
                //    amplitudeSpectrumWindow[j] = frequencyDomainSignal[j].Magnitude;
                //}

                //// Tính toán trục tần số
                //double[] frequencyAxisWindow = Fourier.FrequencyScale(windowSize, sampleRate);

                //// Thêm dữ liệu của cửa sổ hiện tại vào đầu ra chính
                //frequencyAxis.AddRange(frequencyAxisWindow);
                //amplitudeSpectrum.AddRange(amplitudeSpectrumWindow);

                // Chuẩn bị dữ liệu đầu ra biên độ
                double[] amplitudeSpectrumWindow = new double[windowSize];

                // Tính toán biên độ của phổ tần số
                for (int j = 0; j < windowSize; j++)
                {
                    amplitudeSpectrumWindow[j] = window[j].Magnitude;
                }

                // Tính toán trục tần số
                double[] frequencyAxisWindow = Fourier.FrequencyScale(windowSize, sampleRate);

                // Thêm dữ liệu của cửa sổ hiện tại vào đầu ra chính
                frequencyAxis.AddRange(frequencyAxisWindow);
                amplitudeSpectrum.AddRange(amplitudeSpectrumWindow);
            }

            return (frequencyAxis, amplitudeSpectrum);


            //// Chuyển đổi mảng tín hiệu thành mảng số phức
            //Complex32[] complexSignal = new Complex32[timeDomainSignal.Length];
            //for (int i = 0; i < timeDomainSignal.Length; i++)
            //{
            //    complexSignal[i] = new Complex32((float)timeDomainSignal[i], 0);
            //}

            //// Thực hiện FFT
            //Complex[] frequencyDomainSignal = new Complex[timeDomainSignal.Length];
            //Fourier.Forward(complexSignal, FourierOptions.Default);

            //// Chuẩn bị dữ liệu đầu ra biên độ
            //double[] amplitudeSpectrum = new double[frequencyDomainSignal.Length];

            //// Tính toán biên độ của phổ tần số
            //for (int i = 0; i < frequencyDomainSignal.Length; i++)
            //{
            //    amplitudeSpectrum[i] = frequencyDomainSignal[i].Magnitude;
            //}

            //// Tính toán trục tần số
            //double[] frequencyAxis = Fourier.FrequencyScale(timeDomainSignal.Length, 1.0); // Số lượng mẫu, tỷ lệ lấy mẫu

            // Hiển thị kết quả
            //Console.WriteLine("FFT Result:");
            //for (int i = 0; i < frequencyDomainSignal.Length; i++)
            //{
            //    Console.WriteLine("Frequency: " + frequencyAxis[i] + " Hz, Amplitude: " + amplitudeSpectrum[i]);
            //}
            //return (frequencyAxis.ToList(), amplitudeSpectrum.ToList());
        }
    }
}
