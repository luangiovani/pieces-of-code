using System;
using System.Linq;
using Domain.DTO.Gestao;
using Domain.Services.APP;
using Domain.Services.Gestao;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace ERwPControllers.Gestao
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
    /// Controller para manipulação de requisições relacionadas aos Menus de Navegação no Sistema
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class ColaboradorController : BaseController
    {
        /// <summary>
        /// Objeto local de Serviço de operações relacionadas a menu
        /// </summary>
        private readonly ColaboradorService _service;

        /// <summary>
        /// Objeto de Serviço de Operações de Verbas de Gestores
        /// </summary>
        private readonly VerbaService _verbaService;

        /// <summary>
        /// Objeto de Serviço de Recomendações para Colaboradores
        /// </summary>
        private readonly RecomendacaoService _recomendacaoService;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="service">Injeção do Serviço de Menu</param>
        /// <param name="logger">Injeção do Serviço de Log</param>
        public ColaboradorController(ColaboradorService service, VerbaService verbaService,
            RecomendacaoService recomendacaoService, IHttpContextAccessor httpContextAccessor, LogService logger) : base(httpContextAccessor, logger)
        {
            _service = service;
            _verbaService = verbaService;
            _recomendacaoService = recomendacaoService;
        }

        /// <summary>
        /// Listar Colaboradores para popular na Combo
        /// </summary>
        /// <param name="gestor_id">Id do Gestor dos colaboradores</param>
        /// <param name="meuTime">Indica se recupera colaboradores do time do gestor (true) ou não (false)</param>
        /// <param name="comTrocas">Indica se recupera colaboradores que possuem alguma troca de pontos (true) ou não (false)</param>
        /// <returns>Lista de colaboradores de acordo com os parâmetros informados</returns>
        [HttpGet]
        [Route("listar-colaboradores")]
        public IActionResult GetList(string gestor_id, bool meuTime, bool comTrocas)
        {
            try
            {
                if (!string .IsNullOrEmpty(gestor_id.Trim()))
                {
                    var lista = _service.ListarColaboradores(gestor_id, meuTime, comTrocas);
                    return Ok(new {success = true, message="", total = 3, results = lista.Select(c => new {
                        c.cs,
                        c.cs_superior_imediato,
                        c.nome,
                        c.cargo,
                        time = (meuTime ? "Meu Time" : "Outro Time - CS Gestor: " + c.cs_superior_imediato),
                        c.elegivel,
                        pontos = c.quantidade_pontos
                    }).ToList() });
                }
                else
                {
                    return BadRequest(new { Error = "Gestor não foi informado para obtenção da lista de Colaboradores." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("obter")]
        public IActionResult GetList(string colaboradorId, string superiorId)
        {
            try
            {
                if (!string .IsNullOrEmpty(colaboradorId))
                {
                    var objColaborador = _service.FindByCS(colaboradorId);
                    if (objColaborador != null)
                    {
                        return Ok(new {
                                objColaborador.cs,
                                csSuperior = objColaborador.cs_superior_imediato,
                                objColaborador.nome,
                                cargo = objColaborador.cd_cargo,
                                time = (objColaborador.cs_superior_imediato == superiorId ? "Meu Time" : "Outro Time - CS Gestor: " + objColaborador.cs_superior_imediato),
                        });
                    }
                    else
                    {
                        throw new Exception("Colaborador não localizado!");
                    }
                }
                else
                {
                    return BadRequest(new { Error = "Gestor não foi informado para obtenção da lista de Colaboradores." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("relatorio-pontuacao")]
        public IActionResult GetRelatorioPontuacao()
        {
            try
            {
                /// CS do Colaborador Gestor
                if (!string .IsNullOrEmpty(cs_colaborador_logado))
                {
                    var relatorio = _service.RelatorioPontuacao(cs_colaborador_logado);
                    if (relatorio != null)
                        return Ok(new { success = true, obj = relatorio, message = "" });
                    else
                        throw new Exception("Colaborador não localizado!");
                }
                else
                {
                    return BadRequest(new { Error = "Colaborador não foi informado para obtenção do relatório." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("extrato")]
        public IActionResult GetExtrato()
        {
            try
            {
                if (!string.IsNullOrEmpty(cs_colaborador_logado))
                {
                    var relatorio = _service.ExtratoColaborador(cs_colaborador_logado);
                    if (relatorio != null)
                        return Ok(new { success = true, obj = relatorio, message = "" });
                    else
                        throw new Exception("Colaborador não localizado!");
                }
                else
                {
                    return BadRequest(new { Error = "Colaborador não foi informado para obtenção do relatório." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("extrato-colaboradores-gestor")]
        public IActionResult GetExtratoColaboradores()
        {
            try
            {
                if (!string.IsNullOrEmpty(cs_colaborador_logado))
                {
                    var relatorio = _service.ExtratoColaboradoresGestor(cs_colaborador_logado);
                    if (relatorio != null)
                        return Ok(new { success = true, results = relatorio, message = "", count = relatorio.Count() });
                    else
                        throw new Exception("Ocorreu um erro ao tentar obter o relatório de pontuação de Colaboradores para o gestor!");
                }
                else
                {
                    return BadRequest(new { Error = "Colaborador Gestor não foi informado para obtenção do relatório." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("recomendacoes-recebidas")]
        public IActionResult GetRecomendacoes()
        {
            try
            {
                if (!string.IsNullOrEmpty(cs_colaborador_logado))
                {
                    var relatorio = _service.ObterRecomendacoes(cs_colaborador_logado);
                    if (relatorio != null)
                        return Ok(new { success = true, message = "", total = relatorio.Count(), results = relatorio });
                    else
                        throw new Exception("Colaborador não localizado!");
                }
                else
                {
                    return BadRequest(new { Error = "Colaborador não foi informado para obtenção do relatório." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("colaborador-troca")]
        public IActionResult GetCTroca()
        {
            try
            {
                if (!string.IsNullOrEmpty(cs_colaborador_logado))
                {
                    var relatorio = _service.ObterExtratoParaTroca(cs_colaborador_logado);
                    if (relatorio != null)
                        return Ok(new { success = true, obj = relatorio, message = "" });
                    else
                        throw new Exception("Colaborador não localizado!");
                }
                else
                {
                    return BadRequest(new { Error = "Colaborador não foi informado para obtenção do relatório." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("listar")]
        public IActionResult Get()
        {
            try
            {
                var colaboradores = _service.ListarColaboradores().Select(c => new {
                    c.id,
                    c.cs,
                    c.nome,
                    c.cargo,
                    c.UO,
                    c.perfil_id,
                    c.perfil
                }).ToList();
                return Ok(new { success = true, message = "", results = colaboradores, total = colaboradores.Count() });
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("consultar")]
        public IActionResult GetColaborador(string cs)
        {
            try
            {
                if (!string.IsNullOrEmpty(cs))
                {
                    var colaborador = _service.FindByCS(cs);
                    if (colaborador != null)
                        return Ok(new { success = true, obj = colaborador, message = "" });
                    else
                        return Ok(new { success = false, message = "Colaborador não localizado para o CS informado" });
                }
                else
                {
                    return Ok(new { success = false, message = "CS não informado para o Colaborador." });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("consultar-colaborador-loja")]
        public IActionResult GetColaboradorLoja(string cs)
        {
            try
            {
                if (!string.IsNullOrEmpty(cs))
                {
                    var colaborador = _service.ObterColaboradorLoja(cs, "");
                    return Ok(new { success = true, obj = colaborador, message = "" });
                }
                else
                {
                    return Ok(new { success = false, message = "CS não informado para o Colaborador." });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        [Route("atualizar-perfil")]
        public IActionResult Post([FromBody] PerfilColaboradorDTO dto)
        {
            try
            {
                if (!string.IsNullOrEmpty(dto.colaborador_id))
                {
                    var colaborador = _service.FindByID(dto.colaborador_id);
                    if (colaborador != null)
                    {
                        string msgmLog = "Atualizar Perfil do Colaborador de " + colaborador.perfil + " para " + dto.perfil;
                        var log = base._logger.LogInicioOperacao(cs_colaborador_logado, aplicacaoLogada_id, msgmLog, "");

                        try
                        {
                            if (log != null)
                            {
                                _service.AtualizarPerfil(dto, cs_colaborador_logado, log.id);
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
                            return Ok(new { success = false, message = "Ocorreu um erro ao tentar alterat Perfil co Colaborador.: " + ex.Message });
                        }

                        return Ok(new { success = true, message = "Operação realizada com sucesso!" });
                    }
                    else
                        return Ok(new { success = false, message = "Colaborador não localizado pelo Id Informado!" });
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

        [HttpGet]
        [Route("gestores")]
        public IActionResult GetGestores()
        {
            try
            {
                var colaboradores = _service.ListarGestores().ToList();

                return Ok(new { success = true, message = "", results = colaboradores, total = colaboradores.Count() });
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("obter-saldo-pontos")]
        public IActionResult GetSaldoPontos()
        {
            try
            {
                var colaborador = _service.FindByCS(cs_colaborador_logado);
                var saldoVerba = _verbaService.ObterSaldoVerbaGestor(cs_colaborador_logado);

                if (colaborador != null)
                {
                    return Ok(new
                    {
                        success = true,
                        message = "",
                        obj = new {
                            qtde_verba = (saldoVerba != null ? saldoVerba.quantidade_pontos : 0),
                            qtde_pontos = colaborador.quantidade_pontos
                        }
                    });
                }
                else
                {
                    return Ok(new { success = false, message = "Colaborador não localizado" });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("relatorio-troca-pontos")]
        public IActionResult GetRelTrocaPontosColaboradores()
        {
            try
            {
                if (!string.IsNullOrEmpty(cs_colaborador_logado))
                {
                    var relatorio = _service.ListarComprasColaboradoresGestor(cs_colaborador_logado);
                    if (relatorio != null)
                        return Ok(new { success = true, results = relatorio, message = "", count = relatorio.Count() });
                    else
                        throw new Exception("Ocorreu um erro ao tentar obter o relatório de troca de pontos de Colaboradores para o gestor!");
                }
                else
                {
                    return BadRequest(new { Error = "Colaborador Gestor não foi informado para obtenção do relatório." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("minhas-trocas")]
        public IActionResult GetRelMinhasTrocasPontos()
        {
            try
            {
                if (!string.IsNullOrEmpty(cs_colaborador_logado))
                {
                    var relatorio = _service.ListarComprasColaboradores(cs_colaborador_logado);
                    if (relatorio != null)
                        return Ok(new { success = true, results = relatorio, message = "", count = relatorio.Count() });
                    else
                        throw new Exception("Ocorreu um erro ao tentar obter o relatório de troca de pontos!");
                }
                else
                {
                    return BadRequest(new { Error = "Colaborador Gestor não foi informado para obtenção do relatório." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("dashboard")]
        public IActionResult GetDashboard()
        {
            DashboardDTO dto = new DashboardDTO();

            try
            {
                if (!string.IsNullOrEmpty(cs_colaborador_logado))
                {
                    dto.extrato = _service.ExtratoColaborador(cs_colaborador_logado);
                    var recs = _recomendacaoService.StatusRecomendacoesColaborador(cs_colaborador_logado);
                    dto.recomendacoesPendentes = recs.Where(r => r.status == "Em Análise").ToList();
                    dto.recomendacoesAprovadas = recs.Where(r => r.status == "Aprovada").ToList();
                    dto.comprasRealizadas = _service.ListarComprasColaboradores(cs_colaborador_logado);
                    if (dto.extrato != null)
                        return Ok(new { success = true, obj = dto, message = "" });
                    else
                        throw new Exception("Colaborador não localizado!");
                }
                else
                {
                    return BadRequest(new { Error = "Colaborador não foi informado para obtenção do relatório." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}