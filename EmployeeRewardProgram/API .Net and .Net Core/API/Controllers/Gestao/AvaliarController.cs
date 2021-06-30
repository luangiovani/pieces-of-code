using Domain.DTO.Gestao;
using Domain.Services.APP;
using Domain.Services.Gestao;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace ERwPControllers.Gestao
{
    [Route("[controller]")]
    [ApiController]
    public class AvaliarController : BaseController
    {
        /// <summary>
        /// Objeto local de Serviço de operações relacionadas a avaliações
        /// </summary>
        private readonly AvaliacaoService _service;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="service">Injeção de Serviço para operações de avaliações</param>
        /// <param name="logger">Injeção do Serviço de Log</param>
        public AvaliarController(AvaliacaoService service, IHttpContextAccessor httpContextAccessor, LogService logger) : base(httpContextAccessor, logger)
        {
            _service = service;
        }

        /// <summary>
        /// Cadastrar/Atualizar uma Avaliação de Recomendação
        /// </summary>
        /// <param name="dto">DTO com as informações da Avaliação de Recomendação para cadastro/atualização</param>
        /// <returns>Response Status 200 caso sucesso, StatusCode do Erro com "error" descrição do erro.</returns>
        [HttpPost]
        [Route("cadastrar")]
        [Route("atualizar")]
        public IActionResult Post([FromBody]AvaliacaoDTO dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!dto.id.HasValue || string.IsNullOrEmpty(dto.id.ToString()))
                    {
                        string msgmLog = "Avaliar recomendação do Colaborador";
                        var log = base._logger.LogInicioOperacao(cs_colaborador_logado, aplicacaoLogada_id, msgmLog, "");

                        try
                        {
                            if (log != null)
                            {
                                dto = _service.Gravar(dto, cs_colaborador_logado, log.id);
                                base._logger.LogFimOperacao(log.id, "");
                            }
                            else
                                return Ok(new { success = false, obj = dto, message = "Erro ao gerar Log da Operação" });
                        }
                        catch (Exception exGravar)
                        {
                            _service.Remove(dto.id.ToString(), cs_colaborador_logado, log.id);
                            if (log != null)
                                base._logger.LogFimOperacao(log.id, exGravar.Message);
                            else
                            {
                                log = base._logger.LogInicioOperacao(cs_colaborador_logado, aplicacaoLogada_id, msgmLog, "Ocorreu uma exceção ao tentar gravar uma Avaliação para a Recomendação!");
                                base._logger.LogFimOperacao(log.id, exGravar.Message);
                            }
                            return Ok(new { success = false, obj = dto, message = "Erro ao gerar Log da Operação!<br/>" + exGravar.Message });
                        }

                        return Ok(new { success = true, obj = dto, message = "Operação Realizada com Sucesso!" });
                    }
                    else
                    {
                        throw new Exception("Para realizar uma avaliação é preciso que a mesma já tenha sido adicionada a uma recomendação.");
                    }
                }
                else
                    return Ok(new { success = false, obj = dto, message = "Ocorreu um erro ao tentar processar a Operação!<br/>Verifique as infdormações passadas." });
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, obj = dto, message = "Ocorreu um erro ao tentar processar a Operação!<br/>" + ex.Message });
            }
        }

        /// <summary>
        /// Efetivar uma Avaliação de uma Recomendação
        /// </summary>
        /// <param name="dto">Objeto com as informações da Avaliacao</param>
        /// <returns>Resultado da Operação</returns>
        [HttpPost]
        [Route("efetivar")]
        public IActionResult Post([FromBody]AvaliarDTO dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (dto.id.HasValue && !string.IsNullOrEmpty(dto.id.ToString()) && (dto.aprovar || (!dto.aprovar && !string.IsNullOrEmpty(dto.justificativa))))
                    {
                        string msgmLog = "Avaliar recomendação do Colaborador";
                        var log = base._logger.LogInicioOperacao(cs_colaborador_logado, aplicacaoLogada_id, msgmLog, "");

                        try
                        {
                            if (log != null)
                            {
                                dto = _service.Avaliar(dto, cs_colaborador_logado, log.id);
                                base._logger.LogFimOperacao(log.id, "");
                            }
                            else
                                return Ok(new { success = false, obj = dto, message = "Erro ao gerar Log da Operação" });
                        }
                        catch (Exception exGravar)
                        {
                            _service.Remove(dto.id.ToString(), cs_colaborador_logado, log.id);
                            if (log != null)
                                base._logger.LogFimOperacao(log.id, exGravar.Message);
                            else
                            {
                                log = base._logger.LogInicioOperacao(cs_colaborador_logado, aplicacaoLogada_id, msgmLog, "Ocorreu uma exceção ao tentar realizar uma Avaliação para a Recomendação!");
                                base._logger.LogFimOperacao(log.id, exGravar.Message);
                            }
                            return Ok(new { success = false, obj = dto, message = "Erro ao gerar Log da Operação!<br/>" + exGravar.Message });
                        }

                        return Ok(new { success = true, obj = dto, message = "Operação Realizada com Sucesso!" });
                    }
                    else
                    {
                        throw new Exception("Para realizar uma avaliação é preciso que a mesma já tenha sido adicionada a uma recomendação.");
                    }
                }
                else
                    return Ok(new { success = false, obj = dto, message = "Ocorreu um erro ao tentar processar a Operação!<br/>Verifique as informações passadas." });
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, obj = dto, message = "Ocorreu um erro ao tentar processar a Operação!<br/>" + ex.Message });
            }
        }

        /// <summary>
        /// Consultar Avaliação de Recomendação pelo Id informado
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
                    var objRetorno = _service.FindByID(id);
                    return Ok(new { success = true, message = "", obj = objRetorno });
                }
                else
                {
                    return Ok(new { success = false, message = "Id não informado para obtenção da Aplicação." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Retorna uma listagem de todas as Avaliações de Recomendações que estão cadastrados no banco de dados.
        /// </summary>
        /// <returns>Lista de Opções</returns>
        [HttpGet]
        [Route("listar")]
        public IActionResult Get()
        {
            try
            {
                var avaliacoes = _service.ListarAvaliacoesPendendesGestor(cs_colaborador_logado);
                return Ok(new { success = true, message = "", results = avaliacoes, total = avaliacoes.Count() });
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Lista Avaliacoes Realizadas pelo Gestor
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("listar-avaliacoes-realizadas")]
        public IActionResult GetListAvaliacoes()
        {
            try
            {
                var avaliacoes = _service.ListarAvaliacoesRealizadasGestor(cs_colaborador_logado);
                return Ok(new { success = true, message = "", results = avaliacoes, total = avaliacoes.Count() });
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, message = ex.Message });
            }
        }
    }
}