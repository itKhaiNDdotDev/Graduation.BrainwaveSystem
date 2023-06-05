using MathNet.Numerics.LinearAlgebra;
//using MathNet.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation.BrainwaveSystem.Cores.DataProcessors
{
    public class Filter
    {
        //public static (List<double> predictedSignal, List<double> filteredSignal) SolveKalman(List<int> listByteTimeDomainSignal, List<DateTime> listTime)
        //{
        //    // Tính số mẫu tương ứng với 15 giây  
        //    int samplesPer15Seconds = 15 * 512;

        //    // Định nghĩa ma trận trạng thái (state matrix)
        //    Matrix<double> stateMatrix = Matrix<double>.Build.DenseOfArray(new double[,]
        //    {
        //    { 1, 1 },
        //    { 0, 1 }
        //    });

        //    // Định nghĩa ma trận đo (measurement matrix)
        //    Matrix<double> measurementMatrix = Matrix<double>.Build.DenseOfArray(new double[,]
        //    {
        //    { 1, 0 }
        //    });

        //    // Định nghĩa ma trận sai số quá trình (process noise covariance matrix)
        //    Matrix<double> processNoiseCovarianceMatrix = Matrix<double>.Build.DenseOfArray(new double[,]
        //    {
        //    { 0.01, 0 },
        //    { 0, 0.01 }
        //    });

        //    // Định nghĩa ma trận sai số đo (measurement noise covariance matrix)
        //    Matrix<double> measurementNoiseCovarianceMatrix = Matrix<double>.Build.DenseOfArray(new double[,]
        //    {
        //    { 1 }
        //    });

        //    // Khởi tạo ma trận trạng thái ban đầu (initial state matrix)
        //    Vector<double> initialStateVector = Vector<double>.Build.Dense(new double[] { listByteTimeDomainSignal[0], 0 });

        //    // Khởi tạo ma trận sai số trạng thái ban đầu (initial state covariance matrix)
        //    Matrix<double> initialCovarianceMatrix = Matrix<double>.Build.DenseIdentity(2);

        //    // Khởi tạo bộ lọc Kalman
        //    IKalmanFilter kalmanFilter = new KalmanFilter(stateMatrix, measurementMatrix, processNoiseCovarianceMatrix,
        //        measurementNoiseCovarianceMatrix, initialStateVector, initialCovarianceMatrix);

        //    // List lưu trữ kết quả dự đoán và lọc
        //    List<double> predictedSignal = new List<double>();
        //    List<double> filteredSignal = new List<double>();

        //    // Dự đoán và cập nhật các giá trị tiếp theo
        //    for (int i = 0; i < numSamples; i++)
        //    {
        //        // Tính thời gian chênh lệch giữa các mẫu
        //        double deltaTime = (time[i] - time[i - 1]).TotalSeconds;

        //        // Dự đoán trạng thái tiếp
        //        kalmanFilter.Predict(deltaTime);

        //        // Lấy giá trị đo từ chuỗi tín hiệu
        //        double measurement = timeDomainSignal[i];

        //        // Cập nhật trạng thái dựa trên giá trị đo
        //        kalmanFilter.Update(measurement);

        //        // Lấy trạng thái dự đoán và lọc
        //        Vector<double> predictedState = kalmanFilter.State;
        //        double predictedValue = predictedState[0];
        //        double filteredValue = predictedState[1];

        //        // Thêm giá trị dự đoán và lọc vào danh sách kết quả
        //        predictedSignal.Add(predictedValue);
        //        filteredSignal.Add(filteredValue);

        //        // Kiểm tra xem đã đủ 15 giây mẫu chưa
        //        if (i % samplesPer15Seconds == 0)
        //        {
        //            // Thực hiện xử lý hoặc lưu trữ kết quả 15 giây tại đây
        //            // ...
        //        }
        //    }

        //    return (predictedSignal, filteredSignal);
        //}
    }
}
