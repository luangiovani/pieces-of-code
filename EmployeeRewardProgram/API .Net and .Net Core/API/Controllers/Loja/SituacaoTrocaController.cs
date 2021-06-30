using System;
using System.Linq;
using Domain.DTO;
using Domain.DTO.Loja;
using Domain.Services.APP;
using Domain.Services.Venda;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace ERwPControllers.Loja
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
    /// Controller para manipulação de requisições relacionadas a Situações de Troca
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class SituacaoTrocaController : BaseController
    {
        /// <summary>
        /// Objeto local de Serviço de operações relacionadas a Lojas
        /// </summary>
        private readonly SituacaoTrocaService _service;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="service">Injeção do Serviço das Situações de Compra</param>
        /// <param name="logger">Injeção do Serviço de Log</param>
        public SituacaoTrocaController(SituacaoTrocaService service, IHttpContextAccessor httpContextAccessor, LogService logger) : base(httpContextAccessor, logger)
        {
            _service = service;
        }

        /// <summary>
        /// Cadastrar/Atualizar um produto
        /// </summary>
        /// <param name="situacaoTrocaDTO">DTO com as informações de situação de troca para cadastro/atualização</param>
        /// <returns>Response Status 200 caso sucesso, StatusCode do Erro com "error" descrição do erro.</returns>
        [HttpPost]
        [Route("cadastrar")]
        [Route("atualizar")]
        public IActionResult Post([FromBody]SituacaoTrocaDTO situacaoTrocaDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   string msgmLog = string.IsNullOrEmpty(situacaoTrocaDTO.id.ToString()) ? "Cadastrar nova Situação de Troca" : "Atualizar Situação de Troca";

                    var log = base._logger.LogInicioOperacao(cs_colaborador_logado, aplicacaoLogada_id, msgmLog, "");

                    try
                    {
                        if (log != null)
                        {
                            _service.Gravar(situacaoTrocaDTO, cs_colaborador_logado, log.id);
                            base._logger.LogFimOperacao(log.id, "");
                        }
                        else
                            return BadRequest(new { error = "Erro ao gerar Log da Operação" });
                    }
                    catch (Exception exGravar)
                    {
                        if (log != null)
                            base._logger.LogFimOperacao(log.id, exGravar.Message);
                        else
                        {
                            log = base._logger.LogInicioOperacao(cs_colaborador_logado, aplicacaoLogada_id, msgmLog, "Ocorreu uma exceção ao tentar gravar Situação de Troca");
                            base._logger.LogFimOperacao(log.id, exGravar.Message);
                        }

                        return BadRequest(new { error = "Erro ao gerar Log da Operação" });
                    }

                    return Ok( new { success = true, message = "Operação realizada com sucesso!" });
                }
                else
                    return BadRequest(situacaoTrocaDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Ativar/Inativar situação de troca pelo Id informado
        /// </summary>
        /// <param name="id">Id da situação de troca para inativar</param>
        /// <returns>Response Status 200 caso sucesso, StatusCode do Erro com "error" descrição do erro.</returns>
        [HttpPost]
        [Route("ativar-inativar")]
        public IActionResult Post([FromBody] AtivarInativarDTO dto)
        {
            try
            {
                if (!string.IsNullOrEmpty(dto.id.ToString()) && dto.id.HasValue)
                {
                    string msgmLog = dto.ativar ? "Ativar Situação de Troca" : "Inativar Situação de Troca";

                    var log = base._logger.LogInicioOperacao(cs_colaborador_logado, aplicacaoLogada_id, msgmLog, "");

                    try
                    {
                        if (log != null)
                        {
                            if (dto.ativar)
                                _service.Activate(dto.id.ToString(), cs_colaborador_logado, log.id);
                            else
                                _service.Remove(dto.id.ToString(), cs_colaborador_logado, log.id);

                            base._logger.LogFimOperacao(log.id, "");
                        }
                        else
                            return Ok(new { success = false, message = "Erro ao gerar Log da Operação!" });
                    }
                    catch (Exception ex)
                    {
                        if (log != null)
                            base._logger.LogFimOperacao(log.id, ex.Message);
                        else
                        {
                            log = base._logger.LogInicioOperacao(cs_colaborador_logado, aplicacaoLogada_id, msgmLog, "");
                            base._logger.LogFimOperacao(log.id, ex.Message);
                        }
                        return Ok(new { success = false, message = "Ocorreu um erro ao tentar " + msgmLog + ".: " + ex.Message });
                    }

                    return Ok(new { success = true, message = "Operação realizada com sucesso!" });
                }
                else
                {
                    return Ok(new { success = false, message = "Id não informado." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Consultar Situação de Troca pelo Id informado
        /// </summary>
        /// <param name="id">Id da Situação de Troca para inativar</param>
        /// <returns>Response Status 200 caso sucesso, StatusCode do Erro com "error" descrição do erro.</returns>
        [HttpGet]
        [Route("consultar")]
        public IActionResult Get(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    var objRetorno = _service.FindByID(id);
                    return Ok(new { success = true, message = "", obj = objRetorno });
                }
                else
                {
                    return Ok(new { success = false, message = "Id não informado para obtenção da Situação de Troca." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Retorna uma listagem de todas as Situações de Troca que estão cadastradas no banco de dados.
        /// </summary>
        /// <returns>Lista de Situações de Troca</returns>
        [HttpGet]
        [Route("listar")]
        public IActionResult Get()
        {
            try
            {
                var situacoes = _service.FindAll();
                return Ok(new { success = true, message = "", results = situacoes, total = situacoes.Count() });
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, message = ex.Message });
            }
        }
    }
}