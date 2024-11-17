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

        public void TrainAndSaveModel(string dataPath, string modelPath)
        {
            if (!File.Exists(dataPath))
                throw new FileNotFoundException($"Arquivo de dados não encontrado: {dataPath}");

            // Ler e contar o número de linhas no arquivo CSV
            var lines = File.ReadAllLines(dataPath);
            Console.WriteLine($"Número de linhas no arquivo de dados: {lines.Length}");
            if (lines.Length <= 1)
            {
                throw new InvalidOperationException("O arquivo de dados contém apenas o cabeçalho ou está vazio. Verifique o arquivo de entrada.");
            }

            var dataView = _mlContext.Data.LoadFromTextFile<ConsumptionLogData>(
                dataPath, hasHeader: true, separatorChar: ',');

            // Verificar se o conjunto de dados possui instâncias
            var preview = dataView.Preview();
            if (preview.RowView.Length == 0)
            {
                throw new InvalidOperationException("O conjunto de dados de treinamento está vazio após o carregamento. Verifique o arquivo de entrada.");
            }

            // Exibir algumas amostras dos dados para debug
            Console.WriteLine("Amostras dos dados de entrada:");
            foreach (var row in preview.RowView.Take(5))
            {
                Console.WriteLine(string.Join(", ", row.Values.Select(v => $"{v.Key}: {v.Value}")));
            }

            // Remover linhas que contenham valores NaN no campo Consumption
            var filteredData = _mlContext.Data.FilterRowsByMissingValues(dataView, nameof(ConsumptionLogData.Consumption));

            var pipeline = _mlContext.Transforms.Conversion.ConvertType("DeviceId", outputKind: DataKind.Single)
                .Append(_mlContext.Transforms.Concatenate("Features", nameof(ConsumptionLogData.Timestamp), "DeviceId"))
                .Append(_mlContext.Regression.Trainers.Sdca(labelColumnName: nameof(ConsumptionLogData.Consumption)));

            Console.WriteLine("Treinando o modelo...");
            var model = pipeline.Fit(filteredData);

            _mlContext.Model.Save(model, dataView.Schema, modelPath);
            Console.WriteLine($"Modelo salvo em: {modelPath}");
        }
    }
}
