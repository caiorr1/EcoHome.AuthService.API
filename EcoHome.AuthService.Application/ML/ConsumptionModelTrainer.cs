using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.IO;

namespace EcoHome.AuthService.Application.ML
{
    public class ConsumptionModelTrainer
    {
        private readonly MLContext _mlContext;

        public ConsumptionModelTrainer()
        {
            _mlContext = new MLContext();
        }

        public void TrainAndSaveModel(string dataPath, string modelPath)
        {
            if (!File.Exists(dataPath))
                throw new FileNotFoundException($"Arquivo de dados não encontrado: {dataPath}");

            var dataView = _mlContext.Data.LoadFromTextFile<ConsumptionLogData>(
                dataPath, hasHeader: true, separatorChar: ',');

            var pipeline = _mlContext.Transforms.Concatenate("Features", nameof(ConsumptionLogData.Timestamp), nameof(ConsumptionLogData.DeviceId))
                .Append(_mlContext.Regression.Trainers.Sdca(labelColumnName: nameof(ConsumptionLogData.Consumption)));

            Console.WriteLine("Treinando o modelo...");
            var model = pipeline.Fit(dataView);

            _mlContext.Model.Save(model, dataView.Schema, modelPath);
            Console.WriteLine($"Modelo salvo em: {modelPath}");
        }
    }
}
