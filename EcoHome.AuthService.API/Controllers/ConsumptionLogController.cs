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
            _dataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ML", "Data", "consumption_data.csv");
            _modelPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ML", "Modelo", "consumption_model.zip");

            EnsureDirectoryExists(_dataPath);
            EnsureDirectoryExists(_modelPath);
        }

        /// <summary>
        /// Garante que o diretório para um caminho de arquivo exista.
        /// </summary>
        /// <param name="path">Caminho do arquivo.</param>
        private void EnsureDirectoryExists(string path)
        {
            var directory = Path.GetDirectoryName(path);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
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
        /// Atualiza um log de consumo existente.
        /// </summary>
        /// <param name="id">ID do log de consumo.</param>
        /// <param name="dto">Dados atualizados do log.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateConsumptionLog(int id, [FromBody] ConsumptionLogCreateDto dto)
        {
            if (id <= 0 || dto == null || !ModelState.IsValid)
                return BadRequest("Dados inválidos");

            var result = await _logService.UpdateLogAsync(id, dto);
            if (!result)
                return NotFound($"Log de consumo com ID {id} não encontrado");

            return Ok("Log de consumo atualizado com sucesso");
        }

        /// <summary>
        /// Exclui um log de consumo.
        /// </summary>
        /// <param name="id">ID do log de consumo.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConsumptionLog(int id)
        {
            if (id <= 0)
                return BadRequest("ID inválido");

            var result = await _logService.DeleteLogAsync(id);
            if (!result)
                return NotFound($"Log de consumo com ID {id} não encontrado");

            return Ok("Log de consumo excluído com sucesso");
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
                if (!System.IO.File.Exists(_dataPath))
                    return NotFound("Arquivo de dados de consumo não encontrado.");

                var trainer = new ConsumptionModelTrainer();
                trainer.TrainAndSaveModelUsingFastTree(_dataPath, _modelPath);
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