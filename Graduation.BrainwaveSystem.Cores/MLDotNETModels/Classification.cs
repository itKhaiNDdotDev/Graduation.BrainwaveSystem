using Microsoft.ML.Data;
using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Diagnostics;
using System.Drawing;
using Graduation.BrainwaveSystem.Models;
using Graduation.BrainwaveSystem.Cores.DataProcessors;
using Graduation.BrainwaveSystem.Models.DTOs.AIModels;

namespace Graduation.BrainwaveSystem.Cores.MLDotNETModels
{
    public class DataPoint
    {
        [LoadColumn(10)]
        public bool Label { get; set; }

        [LoadColumn(0)]
        public float Delta { get; set; }

        [LoadColumn(1)]
        public float Theta { get; set; }

        [LoadColumn(2)]
        public float Alpha { get; set; }

        [LoadColumn(3)]
        public float Beta { get; set; }

        [LoadColumn(4)]
        public float Abr { get; set; }

        [LoadColumn(5)]
        public float Tbr { get; set; }

        [LoadColumn(6)]
        public float Dbr { get; set; }

        [LoadColumn(7)]
        public float Tar { get; set; }

        [LoadColumn(8)]
        public float Dar { get; set; }

        [LoadColumn(9)]
        public float Dtabr { get; set; }
    }

    public class Prediction
    {
        // ColumnName attribute is used to change the column name from
        // its default value, which is the name of the field.
        [ColumnName("PredictedLabel")]
        public bool PredictedLabel;

        // No need to specify ColumnName attribute, because the field
        // name "Probability" is the column name we want.
        public float Probability;

        public float Score;
    }

    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        // Khởi tạo MLContext
    //        var mlContext = new MLContext();

    //        // Đọc dữ liệu từ tệp CSV
    //        var data = mlContext.Data.LoadFromTextFile<DataPoint>("path/to/your/trainfile.csv", separatorChar: ',');

    //        // Tạo pipeline
    //        var pipeline = mlContext.Transforms.Conversion.MapValueToKey("Label")
    //            .Append(mlContext.Transforms.Concatenate("Features", "Feature1"))
    //            .Append(mlContext.Transforms.NormalizeMinMax("Features"))
    //            .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"))
    //            .Append(mlContext.Transforms.Conversion.MapKeyToValue("Label"))
    //            .Append(mlContext.Transforms.Conversion.MapValueToKey("Label"));

    //        // Huấn luyện mô hình trên toàn bộ dữ liệu
    //        var model = pipeline.Fit(data);

    //        // Dự đoán nhãn cho giá trị mới
    //        var predictionEngine = mlContext.Model.CreatePredictionEngine<DataPoint, Prediction>(model);

    //        var newData = new DataPoint
    //        {
    //            Feature1 = 0.5f, // Giá trị của thuộc tính Feature1 trong mẫu mới
    //                             // Thêm các giá trị của các thuộc tính khác tương ứng trong mẫu mới
    //        };

    //        var prediction = predictionEngine.Predict(newData);

    //        Console.WriteLine($"Predicted Label: {prediction.PredictedLabel}");
    //    }
    //}

    public class Classification
    {
        private static string BaseDatasetsRelativePath = @"../../../DataSets";
        private static string AwakeDataRelativePath = $"{BaseDatasetsRelativePath}/Awake.csv";
        private static string DrowsinessDataRelativePath = $"{BaseDatasetsRelativePath}/Drowsiness.csv";
        private static string AwakeDataPath = GetAbsolutePath(AwakeDataRelativePath);
        private static string DrowsinessDataPath = GetAbsolutePath(DrowsinessDataPath);

        private static string BaseModelsRelativePath = @"../../../TrainedModels";
        private static string ModelRelativePath = $"{BaseModelsRelativePath}/AwakefulStateFastTreeMLNETBC.zip";
        private static string ModelPath = GetAbsolutePath(ModelRelativePath);

        public static string SVMTrain()
        {
            // Khởi tạo MLContext
            var mlContext = new MLContext();

            // Đọc dữ liệu từ các tệp CSV
            var file1 = mlContext.Data.LoadFromTextFile<DataPoint>("K:\\2022_2_DATN\\Graduation.BrainwaveSystem\\Graduation.BrainwaveSystem.Cores\\DataSets\\Awake.csv", separatorChar: ',');
            var file2 = mlContext.Data.LoadFromTextFile<DataPoint>("K:\\2022_2_DATN\\Graduation.BrainwaveSystem\\Graduation.BrainwaveSystem.Cores\\DataSets\\Drowsiness.csv", separatorChar: ',');
            //string dataFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "DataSets");
            //var file1 = mlContext.Data.LoadFromTextFile<DataPoint>(Path.Combine(dataFolderPath, "Awake.csv"), separatorChar: ',');
            //var file2 = mlContext.Data.LoadFromTextFile<DataPoint>(Path.Combine(dataFolderPath, "drowsiness1.csv"), separatorChar: ',');

            // Thêm cột nhãn cho các tệp dữ liệu
            //var labeledFile1 = mlContext.Data.WithColumn(file1, nameof(DataPoint.Label), mlContext.Data.Const(1));
            //var labeledFile2 = mlContext.Data.WithColumn(file2, nameof(DataPoint.Label), mlContext.Data.Const(0));
            var labeledFile1 = mlContext.Data.CreateEnumerable<DataPoint>(file1, reuseRowObject: false)
                .Select(x => new DataPoint
                {
                    Label = true,
                    Delta = x.Delta,
                    Theta = x.Theta,
                    Alpha = x.Alpha,
                    Beta = x.Beta,
                    Abr = x.Abr,
                    Dbr = x.Dbr,
                    Tar = x.Tar,
                    Tbr = x.Tbr,
                    Dar = x.Dar,
                    Dtabr = x.Dtabr
                }).ToList();
            var labeledFile2 = mlContext.Data.CreateEnumerable<DataPoint>(file2, reuseRowObject: false)
                .Select(x => new DataPoint
                {
                    Label = false,
                    Delta = x.Delta,
                    Theta = x.Theta,
                    Alpha = x.Alpha,
                    Beta = x.Beta,
                    Abr = x.Abr,
                    Dbr = x.Dbr,
                    Tar = x.Tar,
                    Tbr = x.Tbr,
                    Dar = x.Dar,
                    Dtabr = x.Dtabr
                }).ToList();

            // Kết hợp các tệp dữ liệu
            //var data = mlContext.Data.LoadFromEnumerable(
            //mlContext.Data.CreateEnumerable<DataPoint>(file1, reuseRowObject: false)
            //.Concat(mlContext.Data.CreateEnumerable<DataPoint>(file2, reuseRowObject: false)));
            var data = mlContext.Data.LoadFromEnumerable(labeledFile1.Concat(labeledFile2));

            // Tạo pipeline
            var pipeline = mlContext.Transforms.Concatenate("Features", new[] { "Delta", "Theta", "Alpha", "Beta", "Abr", "Dbr", "Tbr", "Dar", "Tar", "Dtabr" })
                //.Append(mlContext.Transforms.NormalizeMinMax("Features"))
                //.Append(mlContext.Transforms.Conversion.MapValueToKey("Label"));//, "LabelKey"))
                //.Append(mlContext.Transforms.Conversion.MapKeyToValue("LabelKey", "PredictedLabel"));
                .Append(mlContext.BinaryClassification.Trainers.LinearSvm());

            // Chia thành tập huấn luyện và tập kiểm tra
            var trainTestSplit = mlContext.Data.TrainTestSplit(data, testFraction: 0.2);

            // Huấn luyện mô hình
            var model = //pipeline.Append(mlContext.BinaryClassification.Trainers.LinearSvm())
                pipeline
                //.Append(mlContext.Transforms.Conversion.MapValueToKey("PredictedLabel"))
                .Fit(trainTestSplit.TrainSet);

            var predictions = model.Transform(trainTestSplit.TestSet);
            var metrics = mlContext.BinaryClassification.Evaluate(data: predictions, labelColumnName: "Label", scoreColumnName: "Score");

            // Đánh giá kết quả trên tập huấn luyện và tập kiểm tra
            var trainMetrics = mlContext.BinaryClassification.Evaluate(model.Transform(trainTestSplit.TrainSet));
            var testMetrics = mlContext.BinaryClassification.Evaluate(model.Transform(trainTestSplit.TestSet));

            //// Tạo tập dữ liệu và nhãn 
            //var X = mlContext.Data.LoadFromEnumerable(
            //    mlContext.Data.CreateEnumerable<DataPoint>(file1, reuseRowObject: false)
            //    .Concat(mlContext.Data.CreateEnumerable<DataPoint>(file2, reuseRowObject: false))
            //);

            //// Lấy số dòng trong 2 file
            //int lineCountAwake = 0;
            //using (var stream = new FileStream("K:\\2022_2_DATN\\Graduation.BrainwaveSystem\\Graduation.BrainwaveSystem.Cores\\DataSets\\Awake.csv", FileMode.Open, FileAccess.Read, FileShare.Read))
            //using (var reader = new StreamReader(stream, Encoding.Default))
            //{
            //    while (reader.Peek() >= 0)
            //    {
            //        if (reader.ReadLine() != null)
            //        {
            //            lineCountAwake++;
            //        }
            //    }
            //}
            //int lineCountDrowsiness = 0;
            //using (var stream = new FileStream("K:\\2022_2_DATN\\Graduation.BrainwaveSystem\\Graduation.BrainwaveSystem.Cores\\DataSets\\drowsiness1.csv", FileMode.Open, FileAccess.Read, FileShare.Read))
            //using (var reader = new StreamReader(stream, Encoding.Default))
            //{
            //    while (reader.Peek() >= 0)
            //    {
            //        if (reader.ReadLine() != null)
            //        {
            //            lineCountDrowsiness++;
            //        }
            //    }
            //}

            //var y = mlContext.Data.LoadFromEnumerable(
            //    new[] { 1f }.Concat(new float[file1.GetColumn<float>("Feature1").Length]).Concat(new float[file2.GetColumn<float>("Feature1").Length])
            //);

            //// Chia thành tập huấn luyện và tập kiểm tra
            //var data = mlContext.Data.ZipColumns(X, y);
            //var trainTestSplit = mlContext.Data.TrainTestSplit(data, testFraction: 0.2);

            //// Tạo pipeline
            //var pipeline = mlContext.Transforms.NormalizeMinMax("Features")
            //    .Append(mlContext.Transforms.Conversion.MapValueToKey("Label"))
            //    .Append(mlContext.Transforms.Conversion.MapValueToKey("Label"));

            //// Huấn luyện mô hình
            //var model = pipeline.Append(mlContext.BinaryClassification.Trainers.LinearSvm())
            //    .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"))
            //    .Fit(trainTestSplit.TrainSet);

            //// Đánh giá kết quả trên tập huấn luyện và tập kiểm tra
            //var trainMetrics = mlContext.BinaryClassification.Evaluate(model.Transform(trainTestSplit.TrainSet));
            //var testMetrics = mlContext.BinaryClassification.Evaluate(model.Transform(trainTestSplit.TestSet));


            //Console.WriteLine($"Train score: {trainMetrics.Accuracy}");
            //Console.WriteLine($"Test score: {testMetrics.Accuracy}");
            return ($"Train score: {trainMetrics.Accuracy} - Test score: {testMetrics.Accuracy}");
        }

        public static List<string> FastTreeTrain()
        {
            string rootDir = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../../"));
            string coreProjectDir = Path.Combine(rootDir, "Graduation.BrainwaveSystem.Cores");
            string modelPath = Path.Combine(coreProjectDir, "TrainedModels", "AwakefulStateFastTreeMLNETBC.zip");

            // Khởi tạo MLContext
            var mlContext = new MLContext();

            // STEP 1: Common data loading configuration
            // Đọc dữ liệu từ các tệp CSV
            //var file1 = mlContext.Data.LoadFromTextFile<DataPoint>(AwakeDataPath, hasHeader: true, separatorChar: ',');
            //var file2 = mlContext.Data.LoadFromTextFile<DataPoint>(DrowsinessDataPath, hasHeader: true, separatorChar: ',');
            var file1 = mlContext.Data.LoadFromTextFile<DataPoint>(Path.Combine(coreProjectDir, "DataSets", "Awake.csv"), separatorChar: ',');
            var file2 = mlContext.Data.LoadFromTextFile<DataPoint>(Path.Combine(coreProjectDir, "DataSets", "Drowsiness.csv"), separatorChar: ',');
            var labeledFile1 = mlContext.Data.CreateEnumerable<DataPoint>(file1, reuseRowObject: false)
                .Select(x => new DataPoint
                {
                    Label = true,
                    Delta = x.Delta,
                    Theta = x.Theta,
                    Alpha = x.Alpha,
                    Beta = x.Beta,
                    Abr = x.Abr,
                    Dbr = x.Dbr,
                    Tar = x.Tar,
                    Tbr = x.Tbr,
                    Dar = x.Dar,
                    Dtabr = x.Dtabr
                }).ToList();
            var labeledFile2 = mlContext.Data.CreateEnumerable<DataPoint>(file2, reuseRowObject: false)
                .Select(x => new DataPoint
                {
                    Label = false,
                    Delta = x.Delta,
                    Theta = x.Theta,
                    Alpha = x.Alpha,
                    Beta = x.Beta,
                    Abr = x.Abr,
                    Dbr = x.Dbr,
                    Tar = x.Tar,
                    Tbr = x.Tbr,
                    Dar = x.Dar,
                    Dtabr = x.Dtabr
                }).ToList();
            var data = mlContext.Data.LoadFromEnumerable(labeledFile1.Concat(labeledFile2));

            // STEP 2: Concatenate the features and set the training algorithm
            var pipeline = mlContext.Transforms.Concatenate("Features", "Delta", "Theta", "Alpha", "Beta", "Abr", "Dbr", "Tbr", "Dar", "Tar", "Dtabr")
                .Append(mlContext.BinaryClassification.Trainers.FastTree(labelColumnName: "Label", featureColumnName: "Features"));

            // Chia thành tập huấn luyện và tập kiểm tra
            var trainTestSplit = mlContext.Data.TrainTestSplit(mlContext.Data.ShuffleRows(data), testFraction: 0.2);

            ITransformer trainedModel = pipeline.Fit(trainTestSplit.TrainSet);

            var trainPredictions = trainedModel.Transform(trainTestSplit.TrainSet);
            var trainMetrics = mlContext.BinaryClassification.Evaluate(data: trainPredictions, labelColumnName: "Label", scoreColumnName: "Score");
            var testPredictions = trainedModel.Transform(trainTestSplit.TestSet);
            var testMetrics = mlContext.BinaryClassification.Evaluate(data: testPredictions, labelColumnName: "Label", scoreColumnName: "Score");

            var listMsg = new List<string>();
            listMsg.Add($"Train Accuracy: {trainMetrics.Accuracy:P2} - Test Accuracy: {testMetrics.Accuracy:P2}");
            listMsg.Add("");
            listMsg.Add($"************************ TRAIN RESULTS: *************");
            listMsg.Add($"* Area Under Roc Curve:      {trainMetrics.AreaUnderRocCurve:P2}");
            listMsg.Add($"* Area Under PrecisionRecall Curve:  {trainMetrics.AreaUnderPrecisionRecallCurve:P2}");
            listMsg.Add($"* F1Score:  {trainMetrics.F1Score:P2}");
            listMsg.Add($"* LogLoss:  {trainMetrics.LogLoss:#.##}");
            listMsg.Add($"* LogLossReduction:  {trainMetrics.LogLossReduction:#.##}");
            listMsg.Add($"* PositivePrecision:  {trainMetrics.PositivePrecision:#.##}");
            listMsg.Add($"* PositiveRecall:  {trainMetrics.PositiveRecall:#.##}");
            listMsg.Add($"* NegativePrecision:  {trainMetrics.NegativePrecision:#.##}");
            listMsg.Add($"* NegativeRecall:  {trainMetrics.NegativeRecall:P2}");
            listMsg.Add($"******************************************************");
            listMsg.Add("");
            listMsg.Add($"************************ TEST RESULTS: *************");
            listMsg.Add($"* Area Under Roc Curve:      {testMetrics.AreaUnderRocCurve:P2}");
            listMsg.Add($"* Area Under PrecisionRecall Curve:  {testMetrics.AreaUnderPrecisionRecallCurve:P2}");
            listMsg.Add($"* F1Score:  {testMetrics.F1Score:P2}");
            listMsg.Add($"* LogLoss:  {testMetrics.LogLoss:#.##}");
            listMsg.Add($"* LogLossReduction:  {testMetrics.LogLossReduction:#.##}");
            listMsg.Add($"* PositivePrecision:  {testMetrics.PositivePrecision:#.##}");
            listMsg.Add($"* PositiveRecall:  {testMetrics.PositiveRecall:#.##}");
            listMsg.Add($"* NegativePrecision:  {testMetrics.NegativePrecision:#.##}");
            listMsg.Add($"* NegativeRecall:  {testMetrics.NegativeRecall:P2}");
            listMsg.Add($"******************************************************");
            listMsg.Add("");
            listMsg.Add("============= Saving the model to a file =============");
            mlContext.Model.Save(trainedModel, trainTestSplit.TrainSet.Schema, modelPath);
            listMsg.Add("");
            listMsg.Add("=================== Model Saved ===================== ");

            return listMsg;
        }

        public static AwakeStateFastTreeResponse FastTreeTest(AwakeState10Feature inputFeaturesData)
        {
            //string rootDir = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../../"));
            //string coreProjectDir = Path.Combine(rootDir, "Graduation.BrainwaveSystem.Cores");
            //string modelPath = Path.Combine(coreProjectDir, "TrainedModels", "AwakefulStateFastTreeMLNETBC.zip");
            string modelPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot", "trainedmodels", "AwakefulStateFastTreeMLNETBC.zip");
            var mlContext = new MLContext();
            ITransformer trainedModel = mlContext.Model.Load(modelPath, out var modelInputSchema);

            // Create prediction engine related to the loaded trained model
            var predictionEngine = mlContext.Model.CreatePredictionEngine<DataPoint, Prediction>(trainedModel);

            var dataPoint = new DataPoint()
            {
                Delta = (float)inputFeaturesData.Delta,
                Theta = (float)inputFeaturesData.Theta,
                Alpha = (float)inputFeaturesData.Alpha,
                Beta = (float)inputFeaturesData.Beta,
                Abr = (float)inputFeaturesData.Abr,
                Dbr = (float)inputFeaturesData.Dbr,
                Tbr = (float)inputFeaturesData.Tbr,
                Tar = (float)inputFeaturesData.Tar,
                Dar = (float)inputFeaturesData.Dar,
                Dtabr = (float)inputFeaturesData.Dtabr,
            };
            var prediction = predictionEngine.Predict(dataPoint);
            return new AwakeStateFastTreeResponse()
            {
                PredictionValue = prediction.PredictedLabel,
                StateLabel = prediction.PredictedLabel ? "Awake" : "Drowsiness",
                Probability = prediction.Probability
            };
        }

        ///// <summary>
        ///// Dự đoán các giá trị tiếp theo
        ///// </summary>
        ///// <returns></returns>
        ///// (Jun 16)
        //public static List<Point<int, DateTime>> TrainPredict()
        //{
        //    // Tạo một đối tượng MLContext
        //    var context = new MLContext();

        //    // Đọc dữ liệu
        //    var dataContext = new DataContext();
        //    var data = dataContext.DataRawEEGs.ToList();
        //    var dataView = context.Data.LoadFromEnumerable(data);

        //    // Chia dữ liệu thành tập huấn luyện và tập kiểm tra
        //    var trainTestSplit = context.Data.TrainTestSplit(dataView, testFraction: 0.2);

        //    // Tạo pipeline xử lý dữ liệu
        //    var pipeline = context.Transforms.Conversion.MapValueToKey("Label")
        //        .Append(context.Transforms.Concatenate("Features", "Value"))
        //        .Append(context.Transforms.NormalizeMinMax("Features"))
        //        .Append(context.Transforms.SlidingWindow("WindowedFeatures", "Features", windowSize: 60))
        //        .Append(context.Transforms.DropColumns("Features"))
        //        .Append(context.Transforms.Concatenate("Features"))
        //        .Append(context.Transforms.Concatenate("Features"))
        //        .Append(context.Transforms.SelectFeatures("Features"))
        //        .Append(context.Transforms.CopyColumns("Label", "Value"))
        //        .Append(context.Transforms.DropColumns("Value"));

        //    // Huấn luyện mô hình với pipeline đã xây dựng
        //    var model = pipeline.Fit(trainTestSplit.TrainSet);

        //    // Đánh giá mô hình trên tập kiểm tra
        //    var predictions = model.Transform(trainTestSplit.TestSet);
        //    var metrics = context.Regression.Evaluate(predictions);
        //    Console.WriteLine($"Mean Absolute Error: {metrics.MeanAbsoluteError}");
        //    Console.WriteLine($"Root Mean Squared Error: {metrics.RootMeanSquaredError}");

        //    // Dự đoán các giá trị tiếp theo
        //    var predictionEngine = context.Model.CreatePredictionEngine<TimeData, TimePrediction>(model);
        //    var testData = new TimeData { Value = 100 };
        //    var prediction = predictionEngine.Predict(testData);

        //    Console.WriteLine($"Next value prediction: {prediction.Prediction}");
        //}

        //// Lớp chứa dữ liệu chuỗi thời gian
        //public class TimeData
        //{
        //    public float Value { get; set; }
        //}

        //// Lớp chứa kết quả dự đoán chuỗi thời gian
        //public class TimePrediction
        //{
        //    [ColumnName("Score")]
        //    public float Prediction { get; set; }
        //}

        /// <summary>
        /// Nên bỏ vào Utility
        /// </summary>
        /// <param name="relativePath"></param>
        /// <returns></returns> ĐANG SAI - vào bin/Debug
        /// (Jun 15)
        public static string GetAbsolutePath(string relativePath)
        {
            FileInfo _dataRoot = new FileInfo(typeof(Classification).Assembly.Location);
            string assemblyFolderPath = _dataRoot.Directory.FullName;

            string fullPath = Path.Combine(assemblyFolderPath, relativePath);

            return fullPath;

        }
    }
}
