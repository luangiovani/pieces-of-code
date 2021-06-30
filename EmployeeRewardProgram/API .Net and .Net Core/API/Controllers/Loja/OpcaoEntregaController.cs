using System;
using System.Linq;
using Domain.DTO;
using Domain.DTO.Loja;
using Domain.Services.APP;
using Domain.Services.Loja;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Domain.Helpers;

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
    /// Controller para manipulação de requisições relacionadas a Opções de Entrega
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class OpcaoEntregaController : BaseController
    {
        /// <summary>
        /// Objeto local de Serviço de operações relacionadas a Lojas
        /// </summary>
        private readonly OpcaoEntregaService _service;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="service">Injeção do Serviço das Opções de Entrega</param>
        /// <param name="logger">Injeção do Serviço de Log</param>
        public OpcaoEntregaController(OpcaoEntregaService service, IHttpContextAccessor httpContextAccessor, LogService logger) : base(httpContextAccessor, logger)
        {
            _service = service;
        }

        /// <summary>
        /// Cadastrar/Atualizar uma Opção de Entrega
        /// </summary>
        /// <param name="opcaoEntregaDTO">DTO com as informações de Opção de Entrega para cadastro/atualização</param>
        /// <returns>Response Status 200 caso sucesso, StatusCode do Erro com "error" descrição do erro.</returns>
        [HttpPost]
        [Route("cadastrar")]
        [Route("atualizar")]
        public IActionResult Post([FromBody]OpcaoEntregaDTO opcaoEntregaDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   string msgmLog = string.IsNullOrEmpty(opcaoEntregaDTO.id.ToString()) ? "Cadastrar nova Opção de Entrega" : "Atualizar Opção de Entrega";

                    var log = base._logger.LogInicioOperacao(cs_colaborador_logado, aplicacaoLogada_id, msgmLog, "");

                    try
                    {
                        if (log != null)
                        {
                            _service.Gravar(opcaoEntregaDTO, cs_colaborador_logado, log.id);
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
                            log = base._logger.LogInicioOperacao(cs_colaborador_logado, aplicacaoLogada_id, msgmLog, "Ocorreu uma exceção ao tentar gravar Opção de Entrega");
                            base._logger.LogFimOperacao(log.id, exGravar.Message);
                        }

                        return BadRequest(new { error = "Erro ao gerar Log da Operação" });
                    }

                    return Ok( new { success = true, message = "Operação realizada com sucesso!" });
                }
                else
                    return BadRequest(opcaoEntregaDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Ativar/Inativar Opção de Entrega pelo Id informado
        /// </summary>
        /// <param name="id">Id da Opção de Entrega para inativar</param>
        /// <returns>Response Status 200 caso sucesso, StatusCode do Erro com "error" descrição do erro.</returns>
        [HttpPost]
        [Route("ativar-inativar")]
        public IActionResult Post([FromBody] AtivarInativarDTO dto)
        {
            try
            {
                if (!string.IsNullOrEmpty(dto.id.ToString()) && dto.id.HasValue)
                {
                    string msgmLog = dto.ativar ? "Ativar Opção de Entrega" : "Inativar Opção de Entrega";

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
        /// Consultar Opção de Entrega pelo Id informado
        /// </summary>
        /// <param name="id">Id da Opção de Entrega para inativar</param>
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
                    return Ok(new { success = false, message = "Id não informado para obtenção da Opção de Entrega." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Retorna uma listagem de todas as Opções de Entrega que estão cadastrados no banco de dados.
        /// </summary>
        /// <returns>Lista de Opções</returns>
        [HttpGet]
        [Route("listar")]
        public IActionResult Get()
        {
            try
            {
                var opcoes = _service.FindAll();
                return Ok(new { success = true, message = "", results = opcoes, total = opcoes.Count() });
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Retorna uma listagem de todas as Opções de Entrega que estão cadastrados no banco de dados.
        /// </summary>
        /// <returns>Lista de Opções</returns>
        [HttpGet]
        [Route("listar-por-loja")]
        public IActionResult ListPorLoja(string lojaid)
        {
            try
            {
                var opcoes = _service.FindAll().Where(x => x.ativo == true).ToList();

                if (!string.IsNullOrEmpty(lojaid))
                {

                    if (lojaid.ToUpper() != LojaEnum.LojaGrifeSede)
                    {
                        opcoes = opcoes.Where(o => o.id.ToString().ToUpper() == OpcaoEntregaEnum.RetirarNaLojaMaisProxima).ToList();
                    }
                }
                else
                    opcoes.Clear();

                return Ok(new { success = true, message = "", results = opcoes, total = opcoes.Count() });

            }
            catch (Exception ex)
            {
                return Ok(new { success = false, message = ex.Message });
            }
        }
    }
}