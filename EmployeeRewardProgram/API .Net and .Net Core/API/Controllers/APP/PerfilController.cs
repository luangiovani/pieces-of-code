using System;
using System.Linq;
using Domain.DTO;
using Domain.DTO.APP;
using Domain.Services.APP;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace ERwPControllers.APP
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
    /// Controller para manipulação de requisições relacionadas aos perfis de acesso ao Sistema
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class PerfilController : BaseController
    {
        /// <summary>
        /// Objeto local de Serviço de operações relacionadas a perfis
        /// </summary>
        private readonly PerfilService _service;

        /// <summary>
        /// Objeto com o Service de Acesso aos Menus por Perfil
        /// </summary>
        private readonly PerfilMenuDeNavegacaoService _perfilMenuService;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="service">Injeção do Serviço de Perfil</param>
        /// <param name="perfilMenuService">Injeção do Serviço de Permissões de Menus por Perfil</param>
        /// <param name="logger">Injeção do Serviço de Log</param>
        public PerfilController(PerfilService service, PerfilMenuDeNavegacaoService perfilMenuService, IHttpContextAccessor httpContextAccessor, LogService logger) : base(httpContextAccessor, logger)
        {
            _service = service;
            _perfilMenuService = perfilMenuService;
        }

        /// <summary>
        /// Cadastrar um novo perfil ou permissão de perfil para menu
        /// </summary>
        /// <param name="perfilDTO">DTO com as informações de Perfil para cadastro</param>
        /// <returns>Response Status 200 caso sucesso, StatusCode do Erro com "error" descrição do erro.</returns>
        [HttpPost]
        [Route("cadastrar")]
        [Route("atualizar")]
        public IActionResult Post([FromBody]PerfilDTO perfilDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string msgmLog = string.IsNullOrEmpty(perfilDTO.id.ToString()) ? "Cadastrar novo Perfil" : "Atualizar Perfil";

                    var log = base._logger.LogInicioOperacao(cs_colaborador_logado, aplicacaoLogada_id, msgmLog, "");

                    try
                    {
                        if (log != null)
                        {
                            perfilDTO = _service.Gravar(perfilDTO, cs_colaborador_logado, log.id);
                            _perfilMenuService.VincularDesvincularMenuPerfil(perfilDTO.listaMenus, perfilDTO.id.ToString(), cs_colaborador_logado, log.id);
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
                            log = base._logger.LogInicioOperacao(cs_colaborador_logado, aplicacaoLogada_id, msgmLog, "Ocorreu uma exceção ao tentar inserir/atualizar Perfil.");
                            base._logger.LogFimOperacao(log.id, ex.Message);
                        }

                        return Ok(new { success = false, message = ex.Message });
                    }

                    return Ok(new { success = true, message = "Operação realizada com sucesso!" });
                }
                else
                    return Ok(new { success = false, message = "Verifique as informações passadas, parecem incompletas para concluir a operação." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Cadastrar um novo vínculo entre perfil e menu
        /// </summary>
        /// <param name="perfilMenuDTO">DTO com as informações de vínculo entre Perfil e Menu para cadastro</param>
        /// <returns>Response Status 200 caso sucesso, StatusCode do Erro com "error" descrição do erro.</returns>
        [HttpPost]
        [Route("cadastrar-vinculo-perfil-menu")]
        [Route("atualizar-vinculo-perfil-menu")]
        public IActionResult Post([FromBody]PerfilMenuNavegacaoDTO perfilMenuDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string msgmLog = string.IsNullOrEmpty(perfilMenuDTO.id.ToString()) ? "Cadastrar novo Vínculo entre Perfil e Menu" : "Atualizar Vínculo entre Perfil e Menu";

                    var log = base._logger.LogInicioOperacao(cs_colaborador_logado, aplicacaoLogada_id, msgmLog, "");

                    try
                    {
                        if (log != null)
                        {
                            _perfilMenuService.Gravar(perfilMenuDTO, cs_colaborador_logado, log.id);
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
                            log = base._logger.LogInicioOperacao(cs_colaborador_logado, aplicacaoLogada_id, msgmLog, "Ocorreu uma exceção ao tentar inserir/atualizar Vínculo entre Perfil e Menu.");
                            base._logger.LogFimOperacao(log.id, ex.Message);
                        }

                        return Ok(new { success = false, message = ex.Message });
                    }

                    return Ok(new { success = true, message = "Operação realizada com sucesso!" });
                }
                else
                    return Ok(new { success = false, message = "Verifique as informações passadas, parecem incompletas para concluir a operação." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Ativar/Inativar perfil pelo Id informado
        /// </summary>
        /// <param name="id">Id do perfil para inativar</param>
        /// <returns>Response Status 200 caso sucesso, StatusCode do Erro com "error" descrição do erro.</returns>
        [HttpPost]
        [Route("ativar-inativar")]
        public IActionResult Post([FromBody] AtivarInativarDTO dto)
        {
            try
            {
                if (!string.IsNullOrEmpty(dto.id.ToString()) && dto.id.HasValue)
                {
                    string msgmLog = dto.ativar ? "Ativar Perfil" : "Inativar Perfil";

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
                        return Ok(new { success = false, message = "Ocorreu um erro ao tentar inativar Perfil.: " + ex.Message });
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
        /// Consultar Perfil pelo Id informado
        /// </summary>
        /// <param name="id">Id do Perfil para consultar</param>
        /// <returns>Response Status 200 caso sucesso, StatusCode do Erro com "error" descrição do erro.</returns>
        [HttpGet]
        [Route("consultar")]
        public IActionResult Get(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    PerfilDTO objRetorno = null;
                    var objPerfil = _service.FindByID(id);
                    if (objPerfil != null)
                    {
                        objRetorno = new PerfilDTO()
                        {
                            id = objPerfil.id,
                            ativo = objPerfil.ativo,
                            cs_colaborador_alteracao = objPerfil.cs_colaborador_alteracao,
                            cs_colaborador_criacao = objPerfil.cs_colaborador_criacao,
                            data_hora_alteracao = objPerfil.data_hora_alteracao,
                            data_hora_criacao = objPerfil.data_hora_criacao,
                            descricao = objPerfil.descricao,
                            nome = objPerfil.nome
                        };

                        objRetorno.listaMenus = _perfilMenuService.ListarMenusIdPerfil(objPerfil.id.ToString()).ToList();
                    }
                    else
                    {
                        return Ok(new { success = false, message = "Perfil não localizado pelo Id Informado" });
                    }
                    return Ok(new { success = true, message = "", obj = objRetorno });
                }
                else
                {
                    return Ok(new { success = false, message = "Id não informado para obtenção do Perfil." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Retorna uma listagem de Todos os Perfis que estão cadastrados no banco de dados.
        /// </summary>
        /// <returns>Lista de Perfis</returns>
        [HttpGet]
        [Route("listar")]
        public IActionResult Get()
        {
            try
            {
                var perfis = _service.FindAll();
                return Ok(new { success = true, message = "", results = perfis, total = perfis.Count() });
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, message = ex.Message });
            }
        }
    }
}