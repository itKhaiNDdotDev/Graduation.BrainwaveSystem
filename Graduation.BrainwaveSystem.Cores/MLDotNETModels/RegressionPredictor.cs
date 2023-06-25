using Keras;
using Keras.Layers;
using Keras.Models;
using Newtonsoft.Json.Linq;
using Python.Runtime;
using Numpy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Graduation.BrainwaveSystem.Cores.MLDotNETModels
{
    public class RegressionPredictor
    {
        //static void LSTM()
        //{
        //    // Chuỗi dữ liệu đã có
        //    int[] inputData = { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 };

        //    // Chuẩn bị dữ liệu huấn luyện
        //    int sequenceLength = 5; // Độ dài chuỗi
        //    int numFeatures = 1; // Số lượng đặc trưng

        //    int numSamples = inputData.Length - sequenceLength;
        //    //NDarray xTrain = new NDarray(new Shape(numSamples, sequenceLength, numFeatures));
        //    //NDarray yTrain = new NDarray(new Shape(numSamples, numFeatures));
        //    var xTrain = new NDarray(np.zeros(new Shape(numSamples, sequenceLength, numFeatures)).astype(np.float32));
        //    var yTrain = new NDarray(np.zeros(new Shape(numSamples, numFeatures)).astype(np.float32));


        //    for (int i = 0; i < numSamples; i++)
        //    {
        //        for (int j = 0; j < sequenceLength; j++)
        //        {
        //            xTrain[i, j, 0] = inputData[i + j];
        //        }
        //        yTrain[i, 0] = inputData[i + sequenceLength];
        //    }

        //    // Xây dựng mô hình LSTM
        //    var model = new Sequential();
        //    model.Add(new LSTM(128, input_shape: new Shape(sequenceLength, numFeatures)));
        //    model.Add(new Dense(1));

        //    // Biên dịch mô hình
        //    model.Compile(optimizer: "adam", loss: "mse");

        //    // Huấn luyện mô hình
        //    model.Fit(xTrain, yTrain, epochs: 100, batch_size: 1);

        //    // Dự đoán giá trị tiếp theo
        //    int[] inputSequence = { 60, 70, 80, 90, 100 };
        //    NDarray xTest = new NDarray(new Shape(1, sequenceLength, numFeatures));

        //    for (int i = 0; i < sequenceLength; i++)
        //    {
        //        xTest[0, i, 0] = inputSequence[i];
        //    }

        //    NDarray predictedValue = model.Predict(xTest);
        //    int nextValue = (int)predictedValue[0, 0];

        //    Console.WriteLine("Giá trị tiếp theo được dự đoán là: " + nextValue);
        //}
    }
}