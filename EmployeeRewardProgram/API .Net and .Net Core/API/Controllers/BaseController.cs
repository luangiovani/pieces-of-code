using Domain.Services.APP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ERwPControllers
{
    /// <autor>
    /// Luan Fernandes - Ewave | 04/2019
    /// </autor>
    /// <tarefa>
    /// Development of the Employee Reward Program application
    /// </tarefa>
    /// <atividades>
    /// Desenvolvimento de Controllers para expôr como Services de API
    /// </atividades>
    /// <summary>
    /// Controller que servirá como base, será herdado por outros controllers 
    /// </summary>
    [ApiController]
    [Authorize]
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// Variável com o usuário logado no sistema
        /// </summary>
        public string cs_colaborador_logado;

        /// <summary>
        /// Variável com a aplicação que o usuário logou no sistema
        /// </summary>
        public Guid aplicacaoLogada_id;

        /// <summary>
        /// Objeto local de Serviço de operações relacionadas aos logs da aplicação
        /// </summary>
        public readonly LogService _logger;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="logger">Injeção de serviço de Log</param>
        public BaseController(IHttpContextAccessor httpContextAccessor, LogService logger)
        {
            cs_colaborador_logado = httpContextAccessor.HttpContext.User.FindFirst("userCS").Value;
            aplicacaoLogada_id = Guid.Parse("F2350E89-DA8D-4B0B-A49E-582F33CDF05E");
            _logger = logger;
        }
    }
}