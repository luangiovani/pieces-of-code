using System;
using System.Collections.Generic;
using Domain.DTO.Loja;
using Domain.Helpers;
using Domain.Services.APP;
using Domain.Services.Gestao;
using Domain.Services.Venda;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERwPControllers.Venda
{
    [Route("[controller]")]
    [ApiController]
    public class CompraController : BaseController
    {
        /// <summary>
        /// Objeto local de Serviço de operações relacionadas a compras
        /// </summary>
        private readonly CompraService _service;

        /// <summary>
        /// Objeto de Serviço de Informações de Colaboradores
        /// </summary>
        private readonly ColaboradorService _colaboradorService;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="service">Injeção do Serviço de Menu</param>
        /// <param name="logger">Injeção do Serviço de Log</param>
        public CompraController(CompraService service, ColaboradorService colaboradorService,
            IHttpContextAccessor httpContextAccessor, LogService logger) : base(httpContextAccessor, logger)
        {
            _service = service;
            _colaboradorService = colaboradorService;
        }

        [HttpPost]
        [Route("solicitar-troca-pontos")]
        public IActionResult Post(SolicitacaoCompraDTO dto)
        {
            string msgmLog = "Solicitar Troca de Pontos por Produtos";
            var log = base._logger.LogInicioOperacao(cs_colaborador_logado, aplicacaoLogada_id, msgmLog, "");

            try
            {
                if (log != null)
                {
                    dto = _service.GravarSolicitacaoDeTrocaDePontos(dto, cs_colaborador_logado, log.id);
                    base._logger.LogFimOperacao(log.id, "");
                }
                else
                    return Ok(new { success = false, obj = dto, message = "Erro ao gerar Log da Operação" });
            }
            catch (Exception exGravar)
            {
                if(dto.id.HasValue)
                    _service.Remove(dto.id.ToString(), cs_colaborador_logado, log.id);

                if (log != null)
                    base._logger.LogFimOperacao(log.id, exGravar.Message);
                else
                {
                    log = base._logger.LogInicioOperacao(cs_colaborador_logado, aplicacaoLogada_id, msgmLog, "Ocorreu uma exceção ao tentar gravar uma Troca de Pontos por Produto!");
                    base._logger.LogFimOperacao(log.id, exGravar.Message);
                }
                return Ok(new { success = false, obj = dto, message = "Erro ao gerar Log da Operação!<br/>" + exGravar.Message });
            }

            return Ok(new { success = true, obj = dto, message = "Operação Realizada com Sucesso!" });
        }

        [HttpGet]
        [Route("cancelar-solicitacao-troca")]
        public IActionResult CancelarSolicitacaoTroca(string id)
        {
            if (!string.IsNullOrEmpty(id) && !string.IsNullOrWhiteSpace(id))
            {
                string msgmLog = "Cancelar Solicitação de Troca de Pontos por Produtos";
                var log = base._logger.LogInicioOperacao(cs_colaborador_logado, aplicacaoLogada_id, msgmLog, "");

                try
                {
                    if (log != null)
                    {
                        _service.CancelarCompra(id, cs_colaborador_logado, log.id);
                        base._logger.LogFimOperacao(log.id, "");
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
                        log = base._logger.LogInicioOperacao(cs_colaborador_logado, aplicacaoLogada_id, msgmLog, "Ocorreu uma exceção ao tentar cancelar uma Troca de Pontos por Produto!");
                        base._logger.LogFimOperacao(log.id, exGravar.Message);
                    }
                    return Ok(new { success = false, message = "Erro ao gerar Log da Operação!<br/>" + exGravar.Message });
                }

                return Ok(new { success = true, message = "Operação Realizada com Sucesso!" });
            }
            else
            {
                return Ok(new { success = false, message = "Id da Compra não informado para o Cancelamento." });
            }
        }

        [HttpGet]
        [Route("receber-produtos-troca")]
        public IActionResult ConfirmarRecebimentoProdutos(string id)
        {
            if (!string.IsNullOrEmpty(id) && !string.IsNullOrWhiteSpace(id))
            {
                string msgmLog = "Confirmar o recebimento dos Produtos";
                var log = base._logger.LogInicioOperacao(cs_colaborador_logado, aplicacaoLogada_id, msgmLog, "");

                try
                {
                    if (log != null)
                    {
                        var mdto = new MudaSituacaoDTO() {
                            compra_id = id,
                            justificativa = "Colaborador confirmou que já recebeu os produtos da troca.",
                            situacao_compra_id = SituacaoCompraEnum.ProdutosRecebidos
                        };

                        _service.ObterCompraMudarSituacao(mdto, null, cs_colaborador_logado, log.id);
                        base._logger.LogFimOperacao(log.id, "");
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
                        log = base._logger.LogInicioOperacao(cs_colaborador_logado, aplicacaoLogada_id, msgmLog, "Ocorreu uma exceção ao tentar receber os Produto da Compra!");
                        base._logger.LogFimOperacao(log.id, exGravar.Message);
                    }
                    return Ok(new { success = false, message = "Erro ao gerar Log da Operação!<br/>" + exGravar.Message });
                }

                return Ok(new { success = true, message = "Operação Realizada com Sucesso!" });
            }
            else
            {
                return Ok(new { success = false, message = "Id da Compra não informado para receber os Produto da Compra." });
            }
        }

        [HttpGet]
        [Route("relatorio-trocas-efetuadas")]
        public IActionResult GetRelatorioTrocasRealizadas(string dataDe, string dataAte, string situacao, string pago, string loja_id)
        {
            try
            {
                if (string.IsNullOrEmpty(loja_id))
                {
                    var usr = _colaboradorService.FindByCS(cs_colaborador_logado);
                    if (usr != null)
                    {
                        loja_id = usr.loja_id;
                    }
                    else
                    {
                        throw new Exception("Colaborador não localizado pelo CS informado");
                    }
                }
                var trocas = _service.ObterRelatorioTrocas(dataDe, dataAte, situacao, pago, loja_id);

                return Ok(new { success = true, message = "", results = trocas, total = trocas.Count });
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        [Route("faturar-compras")]
        public IActionResult FaturarCompras(List<string> comprasId)
        {
            if (comprasId.Count > 0)
            {
                string msgmLog = "Enviar compras para Faturamento";
                var log = base._logger.LogInicioOperacao(cs_colaborador_logado, aplicacaoLogada_id, msgmLog, "");

                try
                {
                    if (log != null)
                    {
                        _service.FaturarCompras(comprasId, cs_colaborador_logado, log.id);
                        base._logger.LogFimOperacao(log.id, "");
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
                        log = base._logger.LogInicioOperacao(cs_colaborador_logado, aplicacaoLogada_id, msgmLog, "Ocorreu uma exceção ao tentar enviar compras para Faturamento!");
                        base._logger.LogFimOperacao(log.id, exGravar.Message);
                    }
                    return Ok(new { success = false, message = "Erro ao gerar Log da Operação!<br/>" + exGravar.Message });
                }

                return Ok(new { success = true, message = "Operação Realizada com Sucesso!" });
            }
            else
            {
                return Ok(new { success = false, message = "Compras não informadas para o Faturamento." });
            }
        }

        [HttpPost]
        [Route("pagar-faturamentos")]
        public IActionResult PagarFaturamentos(List<string> faturamentosId)
        {
            if (faturamentosId.Count > 0)
            {
                string msgmLog = "Pagar compras enviadas para Faturamento";
                var log = base._logger.LogInicioOperacao(cs_colaborador_logado, aplicacaoLogada_id, msgmLog, "");

                try
                {
                    if (log != null)
                    {
                        _service.PagarCompras(faturamentosId, cs_colaborador_logado, log.id);
                        base._logger.LogFimOperacao(log.id, "");
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
                        log = base._logger.LogInicioOperacao(cs_colaborador_logado, aplicacaoLogada_id, msgmLog, "Ocorreu uma exceção ao tentar pagar as compras em Faturamento!");
                        base._logger.LogFimOperacao(log.id, exGravar.Message);
                    }
                    return Ok(new { success = false, message = "Erro ao gerar Log da Operação!<br/>" + exGravar.Message });
                }

                return Ok(new { success = true, message = "Operação Realizada com Sucesso!" });
            }
            else
            {
                return Ok(new { success = false, message = "Compras não informadas para o Faturamento." });
            }
        }
    }
}