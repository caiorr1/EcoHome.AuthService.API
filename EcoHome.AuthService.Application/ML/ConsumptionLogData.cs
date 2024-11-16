using Microsoft.ML.Data;
using System;

namespace EcoHome.AuthService.Application.ML
{
    public class ConsumptionLogData
    {
        [LoadColumn(0)] 
        public float Timestamp { get; set; }

        [LoadColumn(1)] 
        public float Consumption { get; set; }

        [LoadColumn(2)] 
        public int DeviceId { get; set; }
    }

    public class ConsumptionPrediction
    {
        [ColumnName("Score")]
        public float PredictedConsumption { get; set; }
    }
}
