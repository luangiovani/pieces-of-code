using System;
using System.Linq;
using Domain.DTO;
using Domain.DTO.Gestao;
using Domain.Services.APP;
using Domain.Services.Gestao;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Domain.Helpers;

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
    /// Controller para manipulação de requisições relacionadas a Lojas
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class LojasController : BaseController
    {
        /// <summary>
        /// Objeto local de Serviço de operações relacionadas a Lojas
        /// </summary>
        private readonly LojaService _service;

        private readonly ColaboradorService _colabService;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="service">Injeção do Serviço das Lojas</param>
        /// <param name="logger">Injeção do Serviço de Log</param>
        public LojasController(LojaService service, ColaboradorService colabService,
            IHttpContextAccessor httpContextAccessor, LogService logger) : base(httpContextAccessor, logger)
        {
            _service = service;
            _colabService = colabService;
        }

        /// <summary>
        /// Cadastrar/Atualizar uma Loja
        /// </summary>
        /// <param name="lojaDTO">DTO com as informações de Loja para cadastro/atualização</param>
        /// <returns>Response Status 200 caso sucesso, StatusCode do Erro com "error" descrição do erro.</returns>
        [HttpPost]
        [Route("cadastrar")]
        [Route("atualizar")]
        public IActionResult Post([FromBody]LojasDTO lojaDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   string msgmLog = string.IsNullOrEmpty(lojaDTO.id.ToString()) ? "Cadastrar nova Loja" : "Atualizar Loja";

                    var log = base._logger.LogInicioOperacao(cs_colaborador_logado, aplicacaoLogada_id, msgmLog, "");

                    try
                    {
                        if (log != null)
                        {
                            _service.Gravar(lojaDTO, cs_colaborador_logado, log.id);
                            base._logger.LogFimOperacao(log.id, "");
                        }
                        else
                            return Ok(new { error = "Erro ao gerar Log da Operação" });
                    }
                    catch (Exception exGravar)
                    {
                        if (log != null)
                            base._logger.LogFimOperacao(log.id, exGravar.Message);
                        else
                        {
                            log = base._logger.LogInicioOperacao(cs_colaborador_logado, aplicacaoLogada_id, msgmLog, "Ocorreu uma exceção ao tentar gravar Loja");
                            base._logger.LogFimOperacao(log.id, exGravar.Message);
                        }

                        return Ok(new { error = "Erro ao gerar Log da Operação" });
                    }

                    return Ok( new { success = true, message = "Operação realizada com sucesso!" });
                }
                else
                    return Ok(lojaDTO);
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, message = ex.Message });
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
                    string msgmLog = dto.ativar ? "Ativar Loja" : "Inativar Loja";

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
                return Ok(new { success = false, message = ex.Message });
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
                if (!string.IsNullOrEmpty(id))
                {
                    var objRetorno = _service.FindByID(id);
                    return Ok(new { success = true, message = "", obj = objRetorno });
                }
                else
                {
                    return Ok(new { success = false, message = "Id não informado para obtenção da Loja." });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, message = ex.Message });
            }

        }

        /// <summary>
        /// Obtém a listagem de Lojas Cadastradas no Banco de Dados
        /// </summary>
        /// <returns>Objeto com a Lista de Lojas</returns>
        [HttpGet]
        [Route("listar")]
        public IActionResult Get()
        {
            try
            {
                var colab = _colabService.FindByCS(cs_colaborador_logado);
                var lojas = _service.FindAll();
                if (colab != null && !string.IsNullOrEmpty(colab.loja_id) && colab.perfil_id == PerfilAcessoEnum.Loja)
                {
                    lojas = lojas.Where(lj => lj.id.ToString().ToUpper() == colab.loja_id).ToList();
                }
                
                return Ok(new { success = true, message = "", results = lojas, total = lojas.Count() });
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, message = ex.Message });
            }
        }


        [HttpGet]
        [Route("obter-colaboradores-loja")]
        public IActionResult GetColaboradoresLoja(string lojaId = "", string cs = "")
        {
            try
            {
                if (string.IsNullOrEmpty(cs))
                {
                    var colaboradorLojas = _service.ObterColaboradoresLoja(lojaId, cs);
                    return Ok(new { success = true, message = "", results = colaboradorLojas, total = colaboradorLojas.Count() });
                }
                else
                {
                    var colaboradorLoja = _service.ObterColaboradoresLoja(lojaId, cs);
                    return Ok(new { success = true, message = "", obj = colaboradorLoja });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        [Route("gravar-vinculo-colaborador-loja")]
        public IActionResult GravarVinculoColaboradorLoja(ColaboradoresLojasDTO dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string msgmLog = string.IsNullOrEmpty(dto.id.ToString()) ? "Cadastrar vinculo colaborador Loja" : "Atualizar vinculo colaborador Loja";
                    var log = base._logger.LogInicioOperacao(cs_colaborador_logado, aplicacaoLogada_id, msgmLog, "");
                    dto.cs_colaborador_logado = cs_colaborador_logado;

                    var colab = _colabService.FindByCS(dto.cs);
                    if (colab != null)
                    {
                        dto.email = colab.email;
                        dto.nome = colab.nome;
                    }
                    else
                    {
                        return Ok(new { success = false, message = "Erro ao tentar localizar o Colaborador pelo Código informado." });
                    }

                    var loja = _service.FindByID(dto.loja_id);
                    if (loja != null)
                    {
                        dto.loja = loja.nome;
                    }
                    else
                    {
                        return Ok(new { success = false, message = "Erro ao tentar localizar a loja pelo Código informado." });
                    }

                    try
                    {
                        if (log != null)
                        {
                            _service.GravarVinculo(dto, cs_colaborador_logado, log.id);
                            base._logger.LogFimOperacao(log.id, "");
                        }
                        else
                            return Ok(new { error = "Erro ao gerar Log da Operação" });
                    }
                    catch (Exception exGravar)
                    {
                        if (log != null)
                            base._logger.LogFimOperacao(log.id, exGravar.Message);
                        else
                        {
                            log = base._logger.LogInicioOperacao(cs_colaborador_logado, aplicacaoLogada_id, msgmLog, "Ocorreu uma exceção ao tentar gravar vinculo colaborador Loja");
                            base._logger.LogFimOperacao(log.id, exGravar.Message);
                            return Ok(new { error = "Erro ao gerar Log da Operação" });
                        }
                        return Ok(new { success = false, message = exGravar.Message });
                    }

                    return Ok(new { success = true, message = "Operação realizada com sucesso!" });
                }
                else
                    return Ok(dto);
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("desvincular-colaborador-loja")]
        public IActionResult DesvincularColaboradorLoja(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    string msgmLog = "Desvincular colaborador Loja";
                    var log = base._logger.LogInicioOperacao(cs_colaborador_logado, aplicacaoLogada_id, msgmLog, "");

                    try
                    {
                        if (log != null)
                        {
                            _service.DesvincularColaboradorLoja(id, cs_colaborador_logado, log.id);
                            base._logger.LogFimOperacao(log.id, "");
                        }
                        else
                            return Ok(new { error = "Erro ao gerar Log da Operação" });
                    }
                    catch (Exception exGravar)
                    {
                        if (log != null)
                            base._logger.LogFimOperacao(log.id, exGravar.Message);
                        else
                        {
                            log = base._logger.LogInicioOperacao(cs_colaborador_logado, aplicacaoLogada_id, msgmLog, "Ocorreu uma exceção ao tentar desvincular colaborador Loja");
                            base._logger.LogFimOperacao(log.id, exGravar.Message);
                        }

                        return Ok(new { error = "Erro ao gerar Log da Operação" });
                    }

                    return Ok(new { success = true, message = "Operação realizada com sucesso!" });
                }
                else
                    return Ok(new { success = false, message = "Vinculo inexistente!" });
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, message = ex.Message });
            }
        }
    }
}