using System;
using System.Linq;
using Domain.DTO.APP;
using Domain.Services.APP;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERwPControllers.APP
{
    [Route("[controller]")]
    [ApiController]
    public class LogsController : BaseController
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="logger">Injeção do Serviço de Log</param>
        public LogsController(IHttpContextAccessor httpContextAccessor, LogService logger) : base(httpContextAccessor, logger)
        {
        }

        [HttpPost]
        [Route("listar")]
        public IActionResult Get([FromBody] LogsBuscaDTO logsDTO)
        {
            try
            {
                var logs = base._logger.ListarLogs(logsDTO).Select(l => new {
                    l.sequencial,
                    data_hora_inicio = l.data_hora_inicio.ToString("dd/MM/yyyy"),
                    nome_colaborador = l.cs_colaborador + " - " + l.nome_colaborador,
                    l.operacao,
                    l.observacao
                });
                return Ok(new { success = true, message = "", results = logs, total = logs.Count() });
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("listar-operacoes")]
        public IActionResult Get()
        {
            try
            {
                var logsOperacoes = base._logger.ListarOperacoes();
                return Ok(new { success = true, message = "", results = logsOperacoes, total = logsOperacoes.Count() });
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, message = ex.Message });
            }
        }
    }
}