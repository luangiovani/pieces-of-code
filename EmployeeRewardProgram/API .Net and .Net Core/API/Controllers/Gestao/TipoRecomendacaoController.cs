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
    /// Controller para manipulação de requisições relacionadas a Tipos de Recomendação
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class TipoRecomendacaoController : BaseController
    {
        /// <summary>
        /// Objeto local de Serviço de operações relacionadas a Tipos de Recomendação
        /// </summary>
        private readonly TipoRecomendacaoService _service;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="service">Injeção do Serviço de TipoRecomendacao</param>
        /// <param name="logger">Injeção do Serviço de Log</param>
        public TipoRecomendacaoController(TipoRecomendacaoService service, IHttpContextAccessor httpContextAccessor, LogService logger) : base(httpContextAccessor, logger)
        {
            _service = service;
        }

        /// <summary>
        /// Cadastrar/Atualizar um Tipo de Recomendação
        /// </summary>
        /// <param name="tipoRecomendacaoDTO">DTO com as informações de Tipo de Recomendação para cadastro/atualização</param>
        /// <returns>Response Status 200 caso sucesso, StatusCode do Erro com "error" descrição do erro.</returns>
        [HttpPost]
        [Route("cadastrar")]
        [Route("atualizar")]
        public IActionResult Post([FromBody]TipoRecomendacaoDTO tipoRecomendacaoDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   string msgmLog = string.IsNullOrEmpty(tipoRecomendacaoDTO.id.ToString()) ? "Cadastrar novo Tipo de Recomendação" : "Atualizar Tipo de Recomendação";

                    var log = base._logger.LogInicioOperacao(cs_colaborador_logado, aplicacaoLogada_id, msgmLog, "");

                    try
                    {
                        if (log != null)
                        {
                            _service.Gravar(tipoRecomendacaoDTO, cs_colaborador_logado, log.id);
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
                            log = base._logger.LogInicioOperacao(cs_colaborador_logado, aplicacaoLogada_id, msgmLog, "Ocorreu uma exceção ao tentar gravar Tipo de Recomendação");
                            base._logger.LogFimOperacao(log.id, exGravar.Message);
                        }

                        return BadRequest(new { error = "Erro ao gerar Log da Operação" });
                    }

                    return Ok( new { success = true, message = "Operação realizada com sucesso!" });
                }
                else
                    return BadRequest(tipoRecomendacaoDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Ativar/Inativar loja pelo Id informado
        /// </summary>
        /// <param name="id">Id da aplicação para inativar</param>
        /// <returns>Response Status 200 caso sucesso, StatusCode do Erro com "error" descrição do erro.</returns>
        [HttpPost]
        [Route("ativar-inativar")]
        public IActionResult Post([FromBody] AtivarInativarDTO dto)
        {
            try
            {
                if (!string.IsNullOrEmpty(dto.id.ToString()) && dto.id.HasValue)
                {
                    string msgmLog = dto.ativar ? "Ativar Tipo de Recomendação" : "Inativar Tipo de Recomendação";

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
        /// Consultar loja pelo Id informado
        /// </summary>
        /// <param name="id">Id da loja para inativar</param>
        /// <returns>Response Status 200 caso sucesso, StatusCode do Erro com "error" descrição do erro.</returns>
        [HttpGet]
        [Route("consultar")]
        public IActionResult Get(string id)
        {
            try
            {
                if(!string.IsNullOrEmpty(id))
                {
                    var tipo = _service.FindByID(id);
                    return Ok( new { success = true, message = "", obj = tipo });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Id não informado para obtenção do Tipo de Recomendação." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }

        }

        /// <summary>
        /// Listar Colaboradores para popular na Combo
        /// </summary>
        /// <param name="gestor_id">Id do Gestor dos colaboradores</param>
        /// <param name="meuTime">Indica se recupera colaboradores do time do gestor (true) ou não (false)</param>
        /// <returns>Lista de colaboradores de acordo com os parâmetros informados</returns>
        [HttpGet]
        [Route("listar-tiporecomendacao-combo")]
        public IActionResult GetList()
        {
            try
            {
                var lista = _service.ListarTipoRecomendacaoCombo();
                return Ok(new { total = 3, results = lista });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("listar-todos")]
        public IActionResult GetAll()
        {
            try
            {
                var lista = _service.ListarTodosAtivos().Select(t => new {
                    id = t.id.ToString(),
                    t.nome,
                    t.descricao,
                    tipo_pontuacao = t.tipo_pontuacao == 1 ? "Escolha" : "Fixa"
                }).ToList();

                return Ok(new { success = true, total = lista.Count, results = lista, message = "" });
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
                var lista = _service.FindAll().ToList();

                return Ok(new { success = true, total = lista.Count, results = lista, message = "" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}