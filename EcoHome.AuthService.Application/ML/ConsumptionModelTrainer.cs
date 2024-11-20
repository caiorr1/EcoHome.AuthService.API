using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.IO;
using System.Linq;

namespace EcoHome.AuthService.Application.ML
{
    public class ConsumptionModelTrainer
    {
        private readonly MLContext _mlContext;

        public ConsumptionModelTrainer()
        {
            _mlContext = new MLContext();
        }

        public void TrainAndSaveModelUsingFastTree(string dataPath, string modelPath)
        {
            if (!File.Exists(dataPath))
                throw new FileNotFoundException($"Arquivo de dados não encontrado: {dataPath}");

            var lines = File.ReadAllLines(dataPath);
            Console.WriteLine($"Número de linhas no arquivo de dados: {lines.Length}");
            if (lines.Length <= 1)
            {
                throw new InvalidOperationException("O arquivo de dados contém apenas o cabeçalho ou está vazio. Verifique o arquivo de entrada.");
            }

            var dataView = _mlContext.Data.LoadFromTextFile<ConsumptionLogData>(
                dataPath, hasHeader: true, separatorChar: ',',
                allowQuoting: true, allowSparse: false);

            var preview = dataView.Preview();
            if (preview.RowView.Length == 0)
            {
                throw new InvalidOperationException("O conjunto de dados de treinamento está vazio após o carregamento. Verifique o arquivo de entrada.");
            }

            Console.WriteLine("Amostras dos dados de entrada:");
            foreach (var row in preview.RowView.Take(5))
            {
                Console.WriteLine(string.Join(", ", row.Values.Select(v => $"{v.Key}: {v.Value}")));
            }

            var filteredData = _mlContext.Data.FilterRowsByMissingValues(dataView, nameof(ConsumptionLogData.Consumption));

            var pipeline = _mlContext.Transforms.Conversion.MapValueToKey("DeviceId", nameof(ConsumptionLogData.DeviceId))
                .Append(_mlContext.Transforms.NormalizeMinMax(nameof(ConsumptionLogData.Timestamp)))
                .Append(_mlContext.Transforms.NormalizeMinMax(nameof(ConsumptionLogData.Consumption)))
                .Append(_mlContext.Transforms.Categorical.OneHotEncoding("DeviceIdEncoded", "DeviceId"))
                .Append(_mlContext.Transforms.Concatenate("Features", nameof(ConsumptionLogData.Timestamp), "DeviceIdEncoded"))
                .Append(_mlContext.Regression.Trainers.FastTree(labelColumnName: nameof(ConsumptionLogData.Consumption)));

            Console.WriteLine("Treinando o modelo...");
            var model = pipeline.Fit(filteredData);

            _mlContext.Model.Save(model, dataView.Schema, modelPath);
            Console.WriteLine($"Modelo salvo em: {modelPath}");
        }
    }
}
