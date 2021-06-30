using System;
using System.Linq;
using Domain.DTO;
using Domain.DTO.Gestao;
using Domain.Services.APP;
using Domain.Services.Gestao;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace ERwPControllers.Loja
{
    [Route("[controller]")]
    [ApiController]
    public class MeioDeCompraController : BaseController
    {
        /// <summary>
        /// Objeto local de Serviço de operações relacionadas a Meios de Compra
        /// </summary>
        private readonly MeioDeCompraService _service;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="service">Injeção do Serviço de Meios de Compra</param>
        /// <param name="logger">Injeção do Serviço de Log</param>
        public MeioDeCompraController(MeioDeCompraService service, IHttpContextAccessor httpContextAccessor, LogService logger) : base(httpContextAccessor, logger)
        {
            _service = service;
        }

        /// <summary>
        /// Cadastrar/Atualizar um Meio de compra
        /// </summary>
        /// <param name="dto">DTO com as informações do meio de compra para cadastro/atualização</param>
        /// <returns>Response Status 200 caso sucesso, StatusCode do Erro com "error" descrição do erro.</returns>
        [HttpPost]
        [Route("cadastrar")]
        [Route("atualizar")]
        public IActionResult Post([FromBody]MeioDeCompraDTO dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string msgmLog = string.IsNullOrEmpty(dto.id.ToString()) ? "Cadastrar novo Meio de Compra" : "Atualizar Meio de Compra";

                    var log = base._logger.LogInicioOperacao(cs_colaborador_logado, aplicacaoLogada_id, msgmLog, "");

                    try
                    {
                        if (log != null)
                        {
                            _service.Gravar(dto, cs_colaborador_logado, log.id);
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
                            log = base._logger.LogInicioOperacao(cs_colaborador_logado, aplicacaoLogada_id, msgmLog, "Ocorreu uma exceção ao tentar gravar Meio de Compra");
                            base._logger.LogFimOperacao(log.id, exGravar.Message);
                        }

                        return BadRequest(new { error = "Erro ao gerar Log da Operação" });
                    }

                    return Ok(new { success = true, message = "Operação realizada com sucesso!" });
                }
                else
                    return BadRequest(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Ativar/Inativar meio de compra pelo Id informado
        /// </summary>
        /// <param name="id">Id do meio de compra para inativar</param>
        /// <returns>Response Status 200 caso sucesso, StatusCode do Erro com "error" descrição do erro.</returns>
        [HttpPost]
        [Route("ativar-inativar")]
        public IActionResult Post([FromBody] AtivarInativarDTO dto)
        {
            try
            {
                if (!string.IsNullOrEmpty(dto.id.ToString()) && dto.id.HasValue)
                {
                    var log = base._logger.LogInicioOperacao(cs_colaborador_logado, aplicacaoLogada_id, "Inativar Meio de Compra", "");

                    try
                    {
                        if (log != null)
                        {
                            _service.Remove(dto.id.ToString(), cs_colaborador_logado, log.id);
                            base._logger.LogFimOperacao(log.id, "");
                        }
                        else
                            return BadRequest(new { error = "Erro ao gerar Log da Operação" });
                    }
                    catch (Exception exInativar)
                    {
                        if (log != null)
                            base._logger.LogFimOperacao(log.id, exInativar.Message);
                        else
                        {
                            log = base._logger.LogInicioOperacao(cs_colaborador_logado, aplicacaoLogada_id, "Inativar Meio de Compra", "");
                            base._logger.LogFimOperacao(log.id, exInativar.Message);
                        }

                        return BadRequest(new { error = "Erro ao gerar Log da Operação" });
                    }

                    return Ok(new { success = true, message = "Operação realizada com sucesso!" });
                }
                else
                {
                    return BadRequest(new { error = "Id não informado." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Consultar Meio de Compra pelo Id informado
        /// </summary>
        /// <param name="id">Id do Meio de Compra para inativar</param>
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
                    return Ok(objRetorno);
                }
                else
                {
                    return BadRequest(new { Error = "Id não informado para obtenção do Meio de Compra." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }

        }

        /// <summary>
        /// Retorna uma listagem de Todos os Perfis que estão cadastrados no banco de dados.
        /// </summary>
        /// <returns>Lista de Perfis</returns>
        [HttpGet]
        [Route("listar")]
        public IActionResult Get()
        {
            try
            {
                var meios = _service.FindAll();
                return Ok(new { success = true, message = "", results = meios, total = meios.Count() });
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, message = ex.Message });
            }
        }
    }
}