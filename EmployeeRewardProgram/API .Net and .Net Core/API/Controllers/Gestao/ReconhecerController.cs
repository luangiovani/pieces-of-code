using System;
using Domain.DTO.Gestao;
using Domain.Services.APP;
using Domain.Services.Gestao;
using Domain.Services.Venda;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace ERwPControllers.Gestao
{
    [Route("[controller]")]
    [ApiController]
    public class ReconhecerController : BaseController
    {
        /// <summary>
        /// Objeto local de Serviço de operações relacionadas a Tipos de Recomendação
        /// </summary>
        private readonly RecomendacaoService _service;

        /// <summary>
        /// Objeto de Serviço de operações relacionadas aos Colaboradores
        /// </summary>
        private readonly ColaboradorService _colaboradorService;

        /// <summary>
        /// Objeto de Serviço de operações de Compras
        /// </summary>
        private readonly CompraService _compraService;

        /// <summary>
        /// Objeto de Serviço de Operações de Verbas de Gestores
        /// </summary>
        private readonly VerbaService _verbaService;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="service">Injeção do Serviço de TipoRecomendacao</param>
        /// <param name="colaboradorService">Injeção do Serviço de Colaboradores</param>
        /// <param name="compraService">Injeção do Serviço de Compras</param>
        /// <param name="verbaService">Injeção do Serviço de Verbas</param>
        /// <param name="httpContextAccessor">Injeção do Contexto de Requisição</param>
        /// <param name="logger">Injeção do Serviço de Log</param>
        public ReconhecerController(RecomendacaoService service, ColaboradorService colaboradorService, CompraService compraService,
            VerbaService verbaService, IHttpContextAccessor httpContextAccessor, LogService logger) : base(httpContextAccessor, logger)
        {
            _service = service;
            _colaboradorService = colaboradorService;
            _compraService = compraService;
            _verbaService = verbaService;
        }

        /// <summary>
        /// Cadastrar/Atualizar uma Recomendação
        /// </summary>
        /// <param name="recomendacaoDTO">DTO com as informações de Recomendação para cadastro/atualização</param>
        /// <returns>Response Status 200 caso sucesso, StatusCode do Erro com "error" descrição do erro.</returns>
        [HttpPost]
        [Route("cadastrar")]
        [Route("atualizar")]
        public IActionResult Post([FromBody]RecomendacaoDTO recomendacaoDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   string msgmLog = "Recomendar Colaborador";
                    var log = base._logger.LogInicioOperacao(cs_colaborador_logado, aplicacaoLogada_id, msgmLog, "");

                    try
                    {
                        if (log != null)
                        {
                            var saldoVerba = _verbaService.ObterSaldoVerbaGestor(recomendacaoDTO.cs_solicitante);

                            if (saldoVerba != null)
                            {
                                if (saldoVerba.quantidade_pontos >= Convert.ToDecimal(recomendacaoDTO.qtde_pontos))
                                {
                                    recomendacaoDTO = _service.Recomendar(recomendacaoDTO, cs_colaborador_logado, log.id);
                                    base._logger.LogFimOperacao(log.id, "");
                                }
                                else
                                {
                                    throw new Exception("O Gestor Solicitante que está recomendando, não possui saldo de pontos para efetuar a recomendação");
                                }
                            }
                            else
                            {
                                throw new Exception("O Gestor Solicitante que está recomendando, não possui verba");
                            }
                        }
                        else
                            return Ok(new { success = false, obj = recomendacaoDTO, message = "Erro ao gerar Log da Operação" });
                    }
                    catch (Exception exGravar)
                    {
                        if (!string.IsNullOrEmpty(recomendacaoDTO.id.ToString()))
                            _service.Remove(recomendacaoDTO.id.ToString(), cs_colaborador_logado, log.id);

                        if (log != null)
                            base._logger.LogFimOperacao(log.id, exGravar.Message);
                        else
                        {
                            log = base._logger.LogInicioOperacao(cs_colaborador_logado, aplicacaoLogada_id, msgmLog, "Ocorreu uma exceção ao tentar gravar uma Recomendação!");
                            base._logger.LogFimOperacao(log.id, exGravar.Message);
                        }
                        return Ok(new { success = false, obj = recomendacaoDTO, message = "Erro ao gerar Log da Operação!<br/>" + exGravar.Message });
                    }

                    return Ok(new { success = true, obj = recomendacaoDTO, message = "Operação Realizada com Sucesso!" });
                }
                else
                    return Ok(new { success = false, obj = recomendacaoDTO, message = "Ocorreu um erro ao tentar processar a Operação!<br/>Verifique as infdormações passadas." });
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, obj = recomendacaoDTO, message = "Ocorreu um erro ao tentar processar a Operação!<br/>"+ex.Message });
            }
        }

        [HttpGet]
        [Route("listar-status-recomendacoes")]
        public IActionResult Get()
        {
            try
            {
                if (!string .IsNullOrEmpty(cs_colaborador_logado))
                {
                    var lista = _service.StatusRecomendacoesGestor(cs_colaborador_logado);
                    return Ok(new { success = true, obj = lista, message = "" });
                }
                else
                    return Ok(new { success = false, message = "CS do Colaborador Solicitante não foi informado" });
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, message = "Ocorreu um erro ao tentar processar a Operação!<br/>" + ex.Message });
            }
        }

        [HttpGet]
        [Route("detalhe-recomendacao")]
        public IActionResult GetDRec(string id)
        {
            try
            {
                if(!string.IsNullOrEmpty(id))
                {
                    var recomendacao = _service.DetalheRecomendacao(id);
                    return Ok(new { success = true, obj = recomendacao, message = "" });
                }
                else
                    return Ok(new { success = false, message = "Identificador da Recomendação não foi informado" });
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, message = "Ocorreu um erro ao tentar processar a Operação!<br/>" + ex.Message });
            }
        }

        [HttpGet]
        [Route("detalhe-pontuacao")]
        public IActionResult Get(string cs_colaborador, string cs_gestor)
        {
            try
            {
                if (!string .IsNullOrEmpty(cs_colaborador) && !string .IsNullOrEmpty(cs_gestor))
                {
                    var pontuacao = _colaboradorService.DetalheRelatorioPontuacao(cs_colaborador, cs_gestor);
                    return Ok(new { success = true, obj = pontuacao, message = "" });
                }
                else
                    return Ok(new { success = false, message = "Identificador Colaborador ou Gestor não foi informado" });
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, message = "Ocorreu um erro ao tentar processar a Operação!<br/>" + ex.Message });
            }
        }

        [HttpGet]
        [Route("indicadores-home-adm")]
        public IActionResult GetRecMes()
        {
            try
            {
                var indicadores = _service.ObterQuantitativo();
                indicadores.ProdutosMaisTrocados = _compraService.ProdutosMaisTrocados();
                return Ok(new { success = true, obj = indicadores, message = "" });
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, message = "Ocorreu um erro ao tentar processar a Operação!<br/>" + ex.Message });
            }
        }
    }
}