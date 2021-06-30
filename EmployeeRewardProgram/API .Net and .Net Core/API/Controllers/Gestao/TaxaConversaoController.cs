using Domain.DTO;
using Domain.DTO.Gestao;
using Domain.Services.APP;
using Domain.Services.Gestao;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace ERwPControllers.Gestao
{
    [Route("[controller]")]
    [ApiController]
    public class TaxaConversaoController : BaseController
    {
        /// <summary>
        /// Objeto local de Serviço de operações relacionadas a Tipos de Recomendação
        /// </summary>
        private readonly TaxaConversaoService _service;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="service">Injeção do Serviço de TipoRecomendacao</param>
        /// <param name="logger">Injeção do Serviço de Log</param>
        public TaxaConversaoController(TaxaConversaoService service, IHttpContextAccessor httpContextAccessor, LogService logger) : base(httpContextAccessor, logger)
        {
            _service = service;
        }

        /// <summary>
        /// Gravar nova taxa de conversão
        /// </summary>
        /// <param name="taxa">Taxa com Valor da taxa de conversão</param>
        /// <returns>Objeto com o resultado da operação</returns>
        [HttpPost]
        [Route("cadastrar")]
        [Route("atualizar")]
        public IActionResult Post([FromBody] TaxaConversaoDTO taxaDTO) {
            try
            {
                if (taxaDTO != null && taxaDTO.valor_moeda > 0)
                {
                    string msgmLog = string.IsNullOrEmpty(taxaDTO.id.ToString()) ? "Cadastrar nova Taxa de Conversão" : "Atualizar Taxa de Conversão";

                    var log = base._logger.LogInicioOperacao(cs_colaborador_logado, aplicacaoLogada_id, msgmLog, "");

                    try
                    {
                        if (log != null)
                        {
                            var taxa = _service.Gravar(taxaDTO, cs_colaborador_logado, log.id);
                            if (taxa != null)
                            {
                                base._logger.LogFimOperacao(log.id, "");
                                return Ok(new { success = true, obj = taxa, message = "Operação Realizada com Sucesso!" });
                            }
                            else
                                throw new Exception("Ocorreu um erro ao tentar gravar a taxa de conversão");
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
        /// Ativar/Inativar taxa de conversão pelo Id informado
        /// </summary>
        /// <param name="id">Id da taxa de conversão para inativar</param>
        /// <returns>Response Status 200 caso sucesso, StatusCode do Erro com "error" descrição do erro.</returns>
        [HttpPost]
        [Route("ativar-inativar")]
        public IActionResult Post([FromBody] AtivarInativarDTO dto)
        {
            try
            {
                if (!string.IsNullOrEmpty(dto.id.ToString()) && dto.id.HasValue)
                {
                    string msgmLog = dto.ativar ? "Ativar Taxa de Conversão" : "Inativar Taxa de Conversão";

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
        /// Consultar taxa de conversão pelo Id informado
        /// </summary>
        /// <param name="id">Id da taxa de conversão para inativar</param>
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
                    return Ok(new { success = false, message = "Id não informado para obtenção da Taxa de Conversão." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Obter Listagem das Taxas de Conversão que estão ativas
        /// </summary>
        /// <returns>Lista de Taxas de Conversões</returns>
        [HttpGet]
        [Route("listar")]
        public IActionResult Get() {
            try
            {
                var lista = _service.FindAll();

                return Ok(new
                {
                    success = true,
                    message = "",
                    total = lista.Count(),
                    results = lista.Select(tx => new {
                        tx.id,
                        tx.sequencial,
                        tx.fator,
                        tx.nome,
                        tx.ativo,
                        tx.valor_moeda,
                        ano_ref = tx.data_hora_criacao.Year.ToString(),
                        data = tx.data_hora_criacao.ToString("dd/MM/yyyy"),
                        taxa = tx.fator,
                        responsavel = tx.cs_colaborador_criacao,
                        status = tx.ativo ? "Ativo" : "Inativo"
                    }).OrderBy(t => t.status).ToList()
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("obter-atual")]
        public ActionResult Get(bool atual = true)
        {
            try
            {
                var taxa = _service.ObterAtiva();
                if (taxa != null)
                {
                    return Ok(new
                    {
                        success = true,
                        message = "",
                        obj = new
                        {
                            taxa = taxa.fator,
                            taxa.valor_moeda
                        }
                    });
                }
                else
                {
                    throw new Exception("Não foi possível encontrar taxa ativa");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}