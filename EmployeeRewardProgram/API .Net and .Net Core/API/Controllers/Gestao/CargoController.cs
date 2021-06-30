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
    /// Controller para manipulação de requisições relacionadas a Cargo
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class CargoController : BaseController
    {
        /// <summary>
        /// Objeto local de Serviço de operações relacionadas a aplicação
        /// </summary>
        private readonly CargoService _service;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="service">Injeção do Serviço da aplicação</param>
        /// <param name="logger">Injeção do Serviço de Log</param>
        public CargoController(CargoService service, IHttpContextAccessor httpContextAccessor, LogService logger) : base(httpContextAccessor, logger)
        {
            _service = service;
        }

        /// <summary>
        /// Cadastrar/Atualizar um Cargo
        /// </summary>
        /// <param name="cargo">DTO com as informações de Cargo para cadastro</param>
        /// <returns>Response Status 200 caso sucesso, StatusCode do Erro com "error" descrição do erro.</returns>
        [HttpPost]
        [Route("cadastrar")]
        [Route("atualizar")]
        public IActionResult Post([FromBody]CargoDTO cargoDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   string msgmLog = string.IsNullOrEmpty(cargoDTO.id.ToString()) ? "Cadastrar novo Cargo" : "Atualizar Cargo";

                    var log = base._logger.LogInicioOperacao(cs_colaborador_logado, aplicacaoLogada_id, msgmLog, "");

                    try
                    {
                        if (log != null)
                        {
                            _service.Gravar(cargoDTO, cs_colaborador_logado, log.id);
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
                            log = base._logger.LogInicioOperacao(cs_colaborador_logado, aplicacaoLogada_id, msgmLog, "Ocorreu uma exceção ao tentar gravar Cargo");
                            base._logger.LogFimOperacao(log.id, exGravar.Message);
                        }

                        return BadRequest(new { error = "Erro ao gerar Log da Operação" });
                    }

                    return Ok( new { success = true, message = "Operação realizada com sucesso!" });
                }
                else
                    return BadRequest(cargoDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Ativar/Inativar aplicacao pelo Id informado
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
                    string msgmLog = "Marcar Cargo como " + (dto.ativar ? "elegível" : "inelegível");

                    var log = base._logger.LogInicioOperacao(cs_colaborador_logado, aplicacaoLogada_id, msgmLog, "");

                    try
                    {
                        if (log != null)
                        {
                            _service.ElegivelInelegivel(dto.id.ToString(), dto.ativar, cs_colaborador_logado, log.id);

                            //if (dto.ativar)
                            //    _service.Activate(dto.id.ToString(), cs_colaborador_logado, log.id);
                            //else
                            //    _service.Remove(dto.id.ToString(), cs_colaborador_logado, log.id);

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
        /// Consultar aplicacao pelo Id informado
        /// </summary>
        /// <param name="id">Id da aplicação para inativar</param>
        /// <returns>Response Status 200 caso sucesso, StatusCode do Erro com "error" descrição do erro.</returns>
        [HttpGet]
        [Route("consultar")]
        public IActionResult Get(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    var cargo = _service.FindByID(id);
                    return Ok(cargo);
                }
                else
                {
                    return BadRequest(new { Error = "Id não informado para obtenção do cargo." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }

        }

        /// <summary>
        /// Obtém a listagem de Cargos Cadastrados no Banco de Dados
        /// </summary>
        /// <returns>Objeto com a Lista de Cargos</returns>
        [HttpGet]
        [Route("listar")]
        public IActionResult Get()
        {
            try
            {
                var cargos = _service.FindAll();
                return Ok(new { success = true, message = "", results = cargos, total = cargos.Count() });
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, message = ex.Message });
            }
        }
    }
}