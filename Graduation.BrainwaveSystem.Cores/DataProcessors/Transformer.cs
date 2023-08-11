using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using AForge.Math;
using Graduation.BrainwaveSystem.Cores.MLDotNETModels;
using Keras.Layers;
using MathNet.Numerics;
using MathNet.Numerics.IntegralTransforms;
using Complex = AForge.Math.Complex;

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
            //double sampleRate = 1.0 / (listTime[1] - listTime[0]).TotalSeconds;  /// Fs = 512
            double sampleRate = 512;

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


        public static (List<double> freqs, List<double> spectrums) TransformFFT(List<int> rawData)
        {
            //int Flm = 512;
            //var complexInput = rawData.Select(x => new Complex(x, 0)).ToArray();
            //Fourier.Forward(complexInput);

            //int length = rawData.Count;
            //return complexInput.Select(x =>  x.Real).Take(length / 2 + 1).ToList();

            ////======= AForge ============
            //int flm = 512;
            //int L = rawData.Count;
            //var complexInput = rawData.Select(y => new Complex(y, 0)).ToArray();
            //FourierTransform.FFT(complexInput, FourierTransform.Direction.Forward);

            //complexInput[0] = new Complex(0, 0);
            //List<double> P2 = complexInput.Select(val => Math.Abs(val.Re/L)).ToList();
            //List<double> P1 = P2.Take(L / 2 + 1).ToList();
            //for (int i = 1; i < P1.Count - 1; i++)
            //{
            //    P1[i] *= 2;
            //}

            //// Calculate frequency values
            //List<double> f1 = Enumerable.Range(0, P1.Count).Select(val => val * flm / ((double)P1.Count)).ToList();
            //return(f1, P1);

            //================FIX MathNetNumerics
            int fs = 512;
            int dataSize = rawData.Count;

            Complex32[] complexData = new Complex32[dataSize];

            for (int i = 0; i < dataSize; i++)
            {
                complexData[i] = new Complex32((float)rawData[i], 0);
            }

            Fourier.Forward(complexData, FourierOptions.Default);
            complexData[0] = 0;

            double[] P2 = new double[dataSize];
            for (int i = 0; i < dataSize; i++)
            {
                P2[i] = (double)complexData[i].Magnitude / dataSize;
            }

            double[] P1 = new double[dataSize/2 + 1];
            Array.Copy(P2, P1, dataSize/2 + 1);

            for (int i = 1; i < P1.Length - 1; i++)
            {
                P1[i] = 2 * P1[i];
            }

            double[] f1 = new double[P1.Length];
            for (int i = 0; i < f1.Length; i++)
            {
                f1[i] = (double)(i * fs/P1.Length)/2;
            }

            //PlotModel model = new PlotModel();
            //LineSeries series = new LineSeries();
            //for (int i = 0; i < f1.Length; i++)
            //{
            //    series.Points.Add(new DataPoint(f1[i], P1[i]));
            //}
            //model.Series.Add(series);

            return (f1.ToList(), P1.ToList());
        }

    }
}
