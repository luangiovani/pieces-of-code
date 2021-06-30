using System;
using System.Linq;
using Domain.Services.APP;
using Domain.Services.Venda;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Domain.Helpers;
using Domain.DTO.Loja;

namespace ERwPControllers.Loja
{
    [Route("[controller]")]
    [ApiController]
    public class TrocasController : BaseController
    {
        /// <summary>
        /// Objeto local de Serviço de operações relacionadas a Lojas
        /// </summary>
        private readonly CompraService _service;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="service">Injeção do Serviço das Trocas (compras)</param>
        /// <param name="logger">Injeção do Serviço de Log</param>
        public TrocasController(CompraService service, IHttpContextAccessor httpContextAccessor, LogService logger) : base(httpContextAccessor, logger)
        {
            _service = service;
        }

        [HttpPost]
        [Route("mudar-solicitacao-pendente")]
        public IActionResult MudarSituacaoCompra([FromBody] MudaSituacaoDTO dto)
        {
            try
            {
                if (!string.IsNullOrEmpty(dto.compra_id) && !string.IsNullOrEmpty(dto.situacao_compra_id))
                {

                    string msgmLog = "Mudar Situação Compra";
                    var log = base._logger.LogInicioOperacao(cs_colaborador_logado, aplicacaoLogada_id, msgmLog, "");

                    try
                    {
                        if (log != null)
                        {
                            var objRetorno = _service.ObterCompraMudarSituacao(dto, null, cs_colaborador_logado, log.id);
                            base._logger.LogFimOperacao(log.id, "");
                            return Ok(new { success = true, obj = objRetorno, message = "Operação Realizada com Sucesso!" });
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
                            log = base._logger.LogInicioOperacao(cs_colaborador_logado, aplicacaoLogada_id, msgmLog, "Ocorreu uma exceção ao tentar atualizar a Situação de Troca!");
                            base._logger.LogFimOperacao(log.id, exGravar.Message);
                        }
                        return Ok(new { success = false, message = "Erro ao gerar Log da Operação!<br/>" + exGravar.Message });
                    }
                }
                else
                {
                    return BadRequest(new { success = false, message = "Id não informado." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("trocas-pendentes")]
        public IActionResult GetPendentes()
        {
            try
            {
                var objRetorno = _service.TrocasPendentes();
                return Ok(new { success = true, obj = objRetorno, message = "" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("detalhe-troca")]
        public IActionResult GetDetalhe(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    var objRetorno = _service.DetalheTroca(id);
                    return Ok(new { success = true, obj = objRetorno, message = "" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Id não informado." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("historico-trocas")]
        public IActionResult GetHistorico()
        {
            try
            {
                var objRetorno = _service.HistoricoTrocasRealizadas();
                return Ok(new { success = true, total = objRetorno.Count(), results = objRetorno, message = "" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("historico-trocas-colaborador")]
        public IActionResult GetHistoricoColaborador()
        {
            try
            {
                if (!string.IsNullOrEmpty(cs_colaborador_logado))
                {
                    var objRetorno = _service.HistoricoTrocasColaborador(cs_colaborador_logado);
                    return Ok(new { success = true, total = objRetorno.Count(), results = objRetorno, message = "" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "CS Colaborador não informado." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("obter-entregas-lojas")]
        public IActionResult GetOpcoesLojas()
        {
            try
            {
                var objRetorno = _service.ObterOpcoesCombos();
                return Ok(new { success = true, obj = objRetorno, message = "" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("obter-solicitacao-pendente")]
        public IActionResult ObterSolicitacaoPendente(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {

                    var objRetorno = _service.DetalheTroca(id);
                    if (objRetorno.situacao_compra_id == SituacaoCompraEnum.SolicitacaoTroca)
                    {
                        string msgmLog = "Mudar Situação Compra";
                        var log = base._logger.LogInicioOperacao(cs_colaborador_logado, aplicacaoLogada_id, msgmLog, "");

                        try
                        {
                            if (log != null)
                            {
                                var dto = new MudaSituacaoDTO() { compra_id = id, situacao_compra_id = SituacaoCompraEnum.EmAnaliseDaLoja };
                                objRetorno = _service.ObterCompraMudarSituacao(dto, objRetorno, cs_colaborador_logado, log.id);
                                base._logger.LogFimOperacao(log.id, "");
                            }
                            else
                                return Ok(new { success = false, obj = objRetorno, message = "Erro ao gerar Log da Operação" });
                        }
                        catch (Exception exGravar)
                        {
                            if (log != null)
                                base._logger.LogFimOperacao(log.id, exGravar.Message);
                            else
                            {
                                log = base._logger.LogInicioOperacao(cs_colaborador_logado, aplicacaoLogada_id, msgmLog, "Ocorreu uma exceção ao tentar atualizar a Situação de Troca!");
                                base._logger.LogFimOperacao(log.id, exGravar.Message);
                            }
                            return Ok(new { success = false, obj = objRetorno, message = "Erro ao gerar Log da Operação!<br/>" + exGravar.Message });
                        }

                        return Ok(new { success = true, obj = objRetorno, message = "Operação Realizada com Sucesso!" });
                    }
                    return Ok(new { success = true, obj = objRetorno, message = "" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Id não informado." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}