// This file was auto-generated by ML.NET Model Builder. 

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.ML;
using PracaInzynierskaML.Model;

namespace PracaInzynierskaML.ConsoleApp
{
    class Program
    {
        //Dataset to use for predictions 
        //private const string DATA_FILEPATH = @"C:\Users\rogow\AppData\Local\Temp\73d511e9-ddda-4a28-9c1c-c0f165c557b2.tsv";
        public static IEnumerable<ModelInput> LoadImagesFromDirectory(string folder, bool useFolderNameAsLabel = true)
        {
            var files = Directory.GetFiles(folder, "*",searchOption: SearchOption.AllDirectories);
            foreach (var file in files)
            {
                if ((Path.GetExtension(file) != ".jpg") && (Path.GetExtension(file) != ".png"))
                    continue;
                var label = Path.GetFileName(file);

                if (useFolderNameAsLabel)
                    label = Directory.GetParent(file).Name;
                else
                {
                    for (int index = 0; index < label.Length; index++)
                    {
                        if (!char.IsLetter(label[index]))
                        {
                            label = label.Substring(0, index);
                            break;
                        }
                    }
                }
                yield return new ModelInput()
                {
                    ImageSource = file,
                    Label = label
                };
            }

        }
        static void Main(string[] args)
        {
            // Create single instance of sample data from first line of dataset for model input
            //ModelInput sampleData = CreateSingleDataSample(DATA_FILEPATH);
            MLContext mlContext = new MLContext();

            var dataPath = @"E:\INZYNIERKA\test\";
            //ModelInput sampleData = CreateSingleDataSample(dataPath);
            IEnumerable<ModelInput> images = LoadImagesFromDirectory(folder: dataPath, useFolderNameAsLabel: true);

            IDataView imageData = mlContext.Data.LoadFromEnumerable(images);

            IDataView shuffledData = mlContext.Data.ShuffleRows(imageData);

            var preprocessingPipeline = mlContext.Transforms.Conversion.MapValueToKey(
                inputColumnName: "Label",
                outputColumnName: "LabelAsKey")
                .Append(mlContext.Transforms.LoadRawImageBytes(
                outputColumnName: "Image",
                imageFolder: dataPath,
                inputColumnName: "ImageSource"));

            IDataView preProcessedData = preprocessingPipeline
                    .Fit(shuffledData)
                    .Transform(shuffledData);


            DataViewSchema modelSchema;
            //ITransformer trainedModel = mlContext.Model.Load(@"E:\INZYNIERKA\PracaInzynierska\PracaInzynierskaML.Model\MLModel.zip", out modelSchema);
            //ITransformer trainedModel = mlContext.Model.Load(@"E:\INZYNIERKA\modelInceptionV3\MLModelInception.zip", out modelSchema);
            ITransformer trainedModel = mlContext.Model.Load(@"E:\INZYNIERKA\modelInceptionV3_2\MLModelInceptionV3.zip", out modelSchema);
            // Make a single prediction on the sample data and print results
            //var predictionResult = ConsumeModel.Predict(images);

            var evaluate = trainedModel.Transform(preProcessedData);

            var metrics = mlContext.MulticlassClassification.Evaluate(evaluate);
            Console.WriteLine("Using model to make single prediction -- Comparing actual Label with predicted Label from sample data...\n\n");
            //Console.WriteLine($"ImageSource: {sampleData.ImageSource}");
            //Console.WriteLine($"\n\nActual Label: {sampleData.Label} \nPredicted Label value {predictionResult.Prediction} \nPredicted Label scores: [{String.Join(",", predictionResult.Score)}]\n\n");
            Console.WriteLine(metrics.ConfusionMatrix.GetFormattedConfusionTable());
            Console.WriteLine(metrics.MicroAccuracy);
            Console.WriteLine(metrics.MacroAccuracy);
            //Console.WriteLine("=============== Train new model ===============");
            //Console.ReadKey();
            //ModelBuilder.CreateModel();
            Console.WriteLine("=============== End of process, hit any key to finish ===============");
            Console.ReadKey();
        }

        // Change this code to create your own sample data
        #region CreateSingleDataSample
        // Method to load single row of dataset to try a single prediction
        private static ModelInput CreateSingleDataSample(string dataFilePath)
        {
            // Create MLContext
            MLContext mlContext = new MLContext();

            // Load dataset
            IDataView dataView = mlContext.Data.LoadFromTextFile<ModelInput>(
                                            path: dataFilePath,
                                            hasHeader: true,
                                            separatorChar: '\t',
                                            allowQuoting: true,
                                            allowSparse: false);

            // Use first line of dataset as model input
            // You can replace this with new test data (hardcoded or from end-user application)
            ModelInput sampleForPrediction = mlContext.Data.CreateEnumerable<ModelInput>(dataView, false)
                                                                        .First();
            return sampleForPrediction;
        }
        #endregion
    }
}
