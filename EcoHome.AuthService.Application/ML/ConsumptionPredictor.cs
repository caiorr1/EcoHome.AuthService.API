using EcoHome.AuthService.Domain.Dtos;
using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.IO;

namespace EcoHome.AuthService.Application.ML
{
    public class ConsumptionPredictor
    {
        private readonly MLContext _mlContext;
        private readonly ITransformer _model;

        public ConsumptionPredictor(string modelPath)
        {
            _mlContext = new MLContext();

            if (!File.Exists(modelPath))
                throw new FileNotFoundException($"Modelo não encontrado: {modelPath}");

            _model = _mlContext.Model.Load(modelPath, out _);
        }

        public float Predict(ConsumptionLogCreateDto consumptionLog)
        {
            var timestampAsFloat = ConvertTimestampToFloat(consumptionLog.Timestamp);

            var predictionEngine = _mlContext.Model.CreatePredictionEngine<ConsumptionLogData, ConsumptionPrediction>(_model);

            var prediction = predictionEngine.Predict(new ConsumptionLogData { Timestamp = timestampAsFloat });
            return prediction.PredictedConsumption;
        }

        private float ConvertTimestampToFloat(DateTime timestamp)
        {
            DateTime baseDate = new DateTime(2024, 1, 1); 
            return (float)(timestamp - baseDate).TotalDays;
        }
    }
}
