using Microsoft.ML.Data;
using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public static string SVMTrain()
        {
            // Khởi tạo MLContext
            var mlContext = new MLContext();

            // Đọc dữ liệu từ các tệp CSV
            var file1 = mlContext.Data.LoadFromTextFile<DataPoint>("K:\\2022_2_DATN\\Graduation.BrainwaveSystem\\Graduation.BrainwaveSystem.Cores\\DataSets\\Awake.csv", separatorChar: ',');
            var file2 = mlContext.Data.LoadFromTextFile<DataPoint>("K:\\2022_2_DATN\\Graduation.BrainwaveSystem\\Graduation.BrainwaveSystem.Cores\\DataSets\\drowsiness1.csv", separatorChar: ',');
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

        public static string FastTreeTrain()
        {
            // Khởi tạo MLContext
            var mlContext = new MLContext();

            // STEP 1: Common data loading configuration
            // Đọc dữ liệu từ các tệp CSV
            var file1 = mlContext.Data.LoadFromTextFile<DataPoint>("K:\\2022_2_DATN\\Graduation.BrainwaveSystem\\Graduation.BrainwaveSystem.Cores\\DataSets\\Awake.csv", separatorChar: ',');
            var file2 = mlContext.Data.LoadFromTextFile<DataPoint>("K:\\2022_2_DATN\\Graduation.BrainwaveSystem\\Graduation.BrainwaveSystem.Cores\\DataSets\\drowsiness1.csv", separatorChar: ',');
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

            return $"Train Accuracy: {trainMetrics.Accuracy:P2} - Test Accuracy: {testMetrics.Accuracy:P2}";
        }
    }
}
