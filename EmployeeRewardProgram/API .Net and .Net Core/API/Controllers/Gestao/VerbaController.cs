using System;
using System.Linq;
using Domain.DTO;
using Domain.DTO.Gestao;
using Domain.Services.APP;
using Domain.Services.Gestao;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace ERwPControllers.Gestao
{
    [Route("[controller]")]
    [ApiController]
    public class VerbaController : BaseController
    {
        /// <summary>
        /// Objeto local de Serviço de operações relacionadas a menu
        /// </summary>
        private readonly VerbaService _service;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="service">Injeção do Serviço de Verbas</param>
        /// <param name="logger">Injeção do Serviço de Log</param>
        public VerbaController(VerbaService service, IHttpContextAccessor httpContextAccessor, LogService logger) : base(httpContextAccessor, logger)
        {
            _service = service;
        }

        [HttpGet]
        [Route("extrato-atribuicoes")]
        public IActionResult GetExtrato()
        {
            try
            {
                var extrato = _service.RelatorioDeExtratoDeAtribuicoes();
                if (extrato != null)
                    return Ok(new { success = true, obj = extrato, message = "" });
                else
                    throw new Exception("Erro ao obter Extrato de Atribuições não localizado!");
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Gravar nova atribuição de Verba
        /// </summary>
        /// <param name="dto">Objeto com as informações de Verba para Cadastrar / Atualizar</param>
        /// <returns>Objeto com o resultado da operação</returns>
        [HttpPost]
        [Route("cadastrar")]
        [Route("atualizar")]
        public IActionResult Post([FromBody] VerbaDTO dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string msgmLog = string.IsNullOrEmpty(dto.id.ToString()) ? "Atribuir Verba" : "Atualizar Atribuição de Verba";

                    var log = base._logger.LogInicioOperacao(cs_colaborador_logado, aplicacaoLogada_id, msgmLog, "");

                    try
                    {
                        if (log != null)
                        {
                            var verba = _service.Gravar(dto, cs_colaborador_logado, log.id);
                            if (verba != null)
                            {
                                base._logger.LogFimOperacao(log.id, "");
                                return Ok(new { success = true, obj = verba, message = "Operação Realizada com Sucesso!" });
                            }
                            else
                                throw new Exception("Ocorreu um erro ao tentar gravar a atribuição de Verba");
                        }
                        else
                            return Ok(new { success = false, message = "Erro ao gerar Log da Operação" });
                    }
                    catch (Exception exGravar)
                    {
                        if (log != null)
                            base._logger.LogFimOperacao(log.id, exGravar.Message);
                        else
                        {
                            log = base._logger.LogInicioOperacao(cs_colaborador_logado, aplicacaoLogada_id, msgmLog, "Ocorreu uma exceção ao tentar gravar uma Taxa de Conversão!");
                            base._logger.LogFimOperacao(log.id, exGravar.Message);
                        }
                        return Ok(new { success = false, message = "Erro ao gerar Log da Operação!<br/>" + exGravar.Message });
                    }
                }
                else
                    return Ok(new { success = false, message = "Ocorreu um erro ao tentar processar a Operação!<br/>Verifique as informações passadas." });
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, message = "Ocorreu um erro ao tentar processar a Operação!<br/>" + ex.Message });
            }
        }

        /// <summary>
        /// Ativar/Inativar verba pelo Id informado
        /// </summary>
        /// <param name="id">Id da verba para inativar</param>
        /// <returns>Response Status 200 caso sucesso, StatusCode do Erro com "error" descrição do erro.</returns>
        [HttpPost]
        [Route("ativar-inativar")]
        public IActionResult Post([FromBody] AtivarInativarDTO dto)
        {
            try
            {
                if (!string.IsNullOrEmpty(dto.id.ToString()) && dto.id.HasValue)
                {
                    string msgmLog = dto.ativar ? "Ativar Verba" : "Inativar Verba";

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
        /// Consultar verba pelo Id informado
        /// </summary>
        /// <param name="id">Id da verba para inativar</param>
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
                    return Ok(new { success = false, message = "Id não informado para obtenção da Verba." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Obter Listagem das Verbas que estão ativas
        /// </summary>
        /// <returns>Lista de Verbas</returns>
        [HttpGet]
        [Route("listar")]
        public IActionResult Get()
        {
            try
            {
                var lista = _service.Listar();

                return Ok(new
                {
                    success = true,
                    message = "",
                    total = lista.Count(),
                    results = lista.ToList()
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}