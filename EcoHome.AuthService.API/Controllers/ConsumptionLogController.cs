using EcoHome.AuthService.Application.ML;
using EcoHome.AuthService.Application.Services;
using EcoHome.AuthService.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace EcoHome.AuthService.API.Controllers
{
    /// <summary>
    /// Controller para gerenciar logs de consumo.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumptionLogController : ControllerBase
    {
        private readonly ConsumptionLogService _logService;
        private readonly string _dataPath;
        private readonly string _modelPath;

        /// <summary>
        /// Construtor da ConsumptionLogController.
        /// </summary>
        /// <param name="logService">O serviço de logs de consumo.</param>
        public ConsumptionLogController(ConsumptionLogService logService)
        {
            _logService = logService;
            // Configurando os caminhos para o arquivo CSV e o modelo salvo
            _dataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ML", "Data", "consumption-data.csv");
            _modelPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ML", "Model", "consumption-model.zip");
        }

        /// <summary>
        /// Adiciona um novo log de consumo.
        /// </summary>
        /// <param name="dto">Dados do log a ser criado.</param>
        [HttpPost]
        public async Task<IActionResult> AddLog([FromBody] ConsumptionLogCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _logService.AddLogAsync(dto);
            return NoContent();
        }

        /// <summary>
        /// Obtém os logs de consumo de um dispositivo.
        /// </summary>
        /// <param name="deviceId">ID do dispositivo.</param>
        /// <returns>Lista de logs de consumo.</returns>
        [HttpGet("device/{deviceId}")]
        public async Task<IActionResult> GetLogsByDeviceId(int deviceId)
        {
            var result = await _logService.GetLogsByDeviceIdAsync(deviceId);
            return Ok(result);
        }

        /// <summary>
        /// Treina o modelo de previsão de consumo usando os dados no CSV.
        /// </summary>
        /// <returns>Mensagem de sucesso ou erro.</returns>
        [HttpPost("train")]
        public IActionResult TrainModel()
        {
            try
            {
                // Verificar se o arquivo de dados existe
                if (!System.IO.File.Exists(_dataPath))
                    return NotFound("Arquivo de dados de consumo não encontrado.");

                // Treinar o modelo
                var trainer = new ConsumptionModelTrainer();
                trainer.TrainAndSaveModel(_dataPath, _modelPath);
                return Ok("Modelo treinado e salvo com sucesso.");
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Erro ao treinar o modelo: {ex.Message}");
            }
        }

        /// <summary>
        /// Faz uma previsão de consumo para um determinado timestamp.
        /// </summary>
        /// <param name="timestamp">Número de dias no futuro para previsão.</param>
        /// <returns>Consumo previsto.</returns>
        [HttpGet("predict/{timestamp}")]
        public IActionResult PredictConsumption(int timestamp)
        {
            try
            {
                if (!System.IO.File.Exists(_modelPath))
                    return NotFound("Modelo treinado não encontrado. Treine o modelo primeiro.");

                DateTime futureDate = DateTime.UtcNow.AddDays(timestamp);

               
                var consumptionLogDto = new ConsumptionLogCreateDto
                {
                    Timestamp = futureDate  
                };

                var predictor = new ConsumptionPredictor(_modelPath);
                var predictedConsumption = predictor.Predict(consumptionLogDto);  

                return Ok(new
                {
                    Timestamp = futureDate,
                    PredictedConsumption = predictedConsumption
                });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Erro ao fazer previsão: {ex.Message}");
            }
        }
    }
}
