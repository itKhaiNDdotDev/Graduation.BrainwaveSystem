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
using Microsoft.Data.SqlClient;
using Microsoft.ML.Data;
using Microsoft.ML;
using Microsoft.ML.Transforms.TimeSeries;
using Graduation.BrainwaveSystem.Models.Entities;
using Numpy.Models;
using MathNet.Numerics.Distributions;
using Microsoft.VisualBasic;
using System.Buffers.Text;
using System.Reflection.Metadata;
using System.Diagnostics.Metrics;

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

        public static (TimeSeriesPredictMetricsModel Evaluation, ModelOutput Prediction) TrainSSAWithSplitTrainTestDataSet(List<int> rawDatas, int predictSize, bool isBoundCanculation = true)
        {

            //DEV======================
            //string rootDir = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../../"));
            //string coreProjectDir = Path.Combine(rootDir, "Graduation.BrainwaveSystem.Cores");
            //string modelPath = Path.Combine(coreProjectDir, "TrainedModels", "MLModel.zip");
            //Product===================
            string modelPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot", "trainedmodels", "MLModel.zip");
            //string dbFilePath = Path.Combine(rootDir, "Data", "DailyDemand.mdf");
            //var connectionString = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={dbFilePath};Integrated Security=True;Connect Timeout=30;";
            //var connectionString = "Server=(localdb)\\mssqllocaldb;Database=Graduation.BrainwaveSystem;Trusted_Connection=True;MultipleActiveResultSets=true";

            MLContext mlContext = new MLContext();

            //DatabaseLoader loader = mlContext.Data.CreateDatabaseLoader<ModelInput>();
            //string query = "SELECT RentalDate, CAST(Year as REAL) as Year, CAST(TotalRentals as REAL) as TotalRentals FROM Rentals";
            //DatabaseSource dbSource = new DatabaseSource(SqlClientFactory.Instance,
            //                                connectionString,
            //                                query);
            //IDataView dataView = loader.Load(dbSource);
            //DatabaseLoader loader = mlContext.Data.CreateDatabaseLoader<ModelInput>();
            //string query = "SELECT TOP 30720 CAST(Value AS REAL) AS Value, CreatedTime FROM DataRawEEGs ORDER BY CreatedTime DESC";
            //DatabaseSource dbSource = new DatabaseSource(SqlClientFactory.Instance,
            //                                connectionString,
            //                                query);
            //IDataView dataView = loader.Load(dbSource);
            //TODO: Check lại xem - lỗi xảy ra ử đây do ML.NET không load được datatype Guid => hãy dùng DTO========================================
            //rawDatas là trainData từ data đang có trong Db
            var dataView = mlContext.Data.LoadFromEnumerable(rawDatas.Select(value => new ModelInput() { Value = (float)value }));

            //IDataView firstYearData = mlContext.Data.FilterRowsByColumn(dataView, "Year", upperBound: 1);
            //IDataView secondYearData = mlContext.Data.FilterRowsByColumn(dataView, "Year", lowerBound: 1);
            var trainTestSplit = mlContext.Data.TrainTestSplit(dataView, testFraction: 0.2);

            SsaForecastingEstimator forecastingPipeline;
            //if (isBoundCanculation)
            //{
                forecastingPipeline = mlContext.Forecasting.ForecastBySsa(
                    outputColumnName: "ForecastedValues",
                    inputColumnName: "Value",
                    windowSize: rawDatas.Count / 3, // Cửa sổ trượt để capture features.
                    seriesLength: rawDatas.Count / 2, // The length of series that is kept in buffer for modeling (parameter N).
                    trainSize: rawDatas.Count, // Kích thước data train => Nên lấy size của dta đầu vào.
                    horizon: predictSize, // số giá trị dự đoán đầu ra.
                    confidenceLevel: 0.95f,//Mức độ tin cậy của dự đoán.
                                       //isAdaptive: Boolean - The flag determining whether the model is adaptive.
                                       //discountFactor: Single - The discount factor in [0, 1] used for online updates.
                                       //rankSelectionMethod: RankSelectionMethod - The rank selection method.
                                       //rank: Nullable<Int32> - The desired rank of the subspace used for SSA projection (parameter r). This parameter should be in the range in [1, windowSize]. If set to null, the rank is automatically determined based on prediction error minimization.
                                       //maxRank: Nullable<Int32> - The maximum rank considered during the rank selection process. If not provided(i.e.set to null), it is set to windowSize - 1.
                                       //shouldStabilize: Boolean - The flag determining whether the model should be stabilized.
                                       //shouldMaintainInfo: Boolean - The flag determining whether the meta information for the model needs to be maintained.
                                       //maxGrowth: Nullable <GrowthRatio> - The maximum growth on the exponential trend.
                    confidenceLowerBoundColumn: "LowerBoundValues",//The name of the confidence interval lower bound column. If not specified then confidence intervals will not be calculated.
                    confidenceUpperBoundColumn: "UpperBoundValues"
                    //variableHorizon: Boolean - Set this to true if horizon will change after training(at prediction time).
                );//The name of the confidence interval upper bound column. If not specified then confidence intervals will not be calculated.
            //}
            //else
            //{
            //    forecastingPipeline = mlContext.Forecasting.ForecastBySsa(
            //        outputColumnName: "ForecastedValues",
            //        inputColumnName: "Value",
            //        windowSize: rawDatas.Count / 3,
            //        seriesLength: rawDatas.Count / 2,
            //        trainSize: rawDatas.Count,
            //        horizon: predictSize,
            //        confidenceLevel: 0.95f
            //    );
            //}
                
            SsaForecastingTransformer forecaster = forecastingPipeline.Fit(trainTestSplit.TrainSet);

            var evalResult = Evaluate(trainTestSplit.TestSet, forecaster, mlContext);

            var forecastEngine = forecaster.CreateTimeSeriesEngine<ModelInput, ModelOutput>(mlContext);
            forecastEngine.CheckPoint(mlContext, modelPath);

            //var predictResult = Forecast(trainTestSplit.TestSet, predictSize, forecastEngine, mlContext);
            ModelOutput predictResult = forecastEngine.Predict();

            return (evalResult, predictResult);
        }

        static TimeSeriesPredictMetricsModel Evaluate(IDataView testData, ITransformer model, MLContext mlContext)
        {
            // Make predictions
            IDataView predictions = model.Transform(testData);

            // Actual values
            IEnumerable<float> actual =
                mlContext.Data.CreateEnumerable<ModelInput>(testData, true)
                    .Select(observed => (float)observed.Value); // Cân nhắc dự đoán với giá tị int (0-255)

            // Predicted values
            IEnumerable<float> forecast =
                mlContext.Data.CreateEnumerable<ModelOutput>(predictions, true)
                    .Select(prediction => prediction.ForecastedValues[0]);

            // Calculate error (actual - forecast)
            var metrics = actual.Zip(forecast, (actualValue, forecastValue) => actualValue - forecastValue);

            // Get metric averages
            var result = new TimeSeriesPredictMetricsModel()
            {
                MAE = metrics.Average(error => Math.Abs(error)), // Mean Absolute Error
                RMSE = Math.Sqrt(metrics.Average(error => Math.Pow(error, 2))) // Root Mean Squared Error
            };
            //var result = new TimeSeriesPredictMetricsModel()
            //{
            //    MAE = 0, // Mean Absolute Error
            //    RMSE = 0 // Root Mean Squared Error
            //};


            // Output metrics
            //Console.WriteLine("Evaluation Metrics");
            //Console.WriteLine("---------------------");
            //Console.WriteLine($"Mean Absolute Error: {MAE:F3}");
            //Console.WriteLine($"Root Mean Squared Error: {RMSE:F3}\n");
            return result;
        }

        public class TimeSeriesPredictMetricsModel
        {
            public float MAE { get; set; }
            public double RMSE { get; set; }
        }

        static ModelOutput Forecast(IDataView testData, int horizon, TimeSeriesPredictionEngine<ModelInput, ModelOutput> forecaster, MLContext mlContext)
        {
            ModelOutput forecast = forecaster.Predict();

            //IEnumerable<string> forecastOutput =
            //    mlContext.Data.CreateEnumerable<DataRawEEG>(testData, reuseRowObject: false)
            //        .Take(horizon)
            //        .Select((DataRawEEG input, int index) =>
            //        {
            //            //string createdDateTime = input.CreatedTime.ToShortDateString();
            //            float actualValues = input.Value;
            //            float lowerEstimate = Math.Max(0, forecast.LowerBoundValues[index]);
            //            float estimate = forecast.ForecastedValues[index];
            //            float upperEstimate = forecast.UpperBoundValues[index];
            //            return //$"Date: {createdDateTime}\n" +
            //            $"Actual Rentals: {actualValues}\n" +
            //            $"Lower Estimate: {lowerEstimate}\n" +
            //            $"Forecast: {estimate}\n" +
            //            $"Upper Estimate: {upperEstimate}\n";
            //        });

            // Output predictions
            //Console.WriteLine("Rental Forecast");
            //Console.WriteLine("---------------------");
            //foreach (var prediction in forecastOutput)
            //{
            //    Console.WriteLine(prediction);
            //}
            return forecast;
        }

        public class ModelInput
        {
            //public DateTime CreatedTime { get; set; }

            public float Value { get; set; } // Dự đoán TimeSeries thường là float => Đã đổi hết qua int
        }

        public class ModelOutput
        {
            public float[] ForecastedValues { get; set; }

            public float[] LowerBoundValues { get; set; }

            public float[] UpperBoundValues { get; set; }   // Cân nhắc dùng kiểu int thay vì float
        }

        public class DataRawEEGPrediction
        {
            public float ForecastedValue { get; set; }
            public float LowerBoundValue { get; set; }
            public float UpperBoundValues { get; set; }
            public DateTime CreatedTime { get; set; }
        }
    }
}