using Domain.DTO.Gestao;
using Domain.Services.APP;
using Domain.Services.Gestao;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Http;

namespace ERwPControllers.Gestao
{
    [Route("[controller]")]
    [ApiController]
    public class ConfigurarController : BaseController
    {
        /// <summary>
        /// Serviço para Manipulação de informações de Configuração de Distribuição de Verbas
        /// </summary>
        private readonly ConfiguracaoVerbaService _serviceCfgVerba;

        /// <summary>
        /// Serviço para Manipulação de informações de Configuração de Expiração de Pontos
        /// </summary>
        private readonly ConfiguracaoExpiracaoService _serviceCfgExp;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="serviceCfgVerba">Injeção do Serviço de Configuração de Distribuição de Verbas</param>
        /// <param name="serviceCfgExp">Injeção do Serviço de Expiração de Pontos</param>
        /// <param name="logger">Injeção do Serviço de Log</param>
        public ConfigurarController(ConfiguracaoVerbaService serviceCfgVerba, 
            ConfiguracaoExpiracaoService serviceCfgExp,
            IHttpContextAccessor httpContextAccessor,
            LogService logger) : base(httpContextAccessor, logger)
        {
            _serviceCfgVerba = serviceCfgVerba;
            _serviceCfgExp = serviceCfgExp;
        }

        [HttpPost]
        [Route("cadastrar-atualizar-verba")]
        public IActionResult Post(ConfiguracaoDistribuicaoVerbasDTO dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string msgmLog = string.IsNullOrEmpty(dto.id.ToString()) ? "Cadastrar Distribuição de Verbas" : "Atualizar Distribuição de Verbas";

                    var log = base._logger.LogInicioOperacao(cs_colaborador_logado, aplicacaoLogada_id, msgmLog, "");

                    try
                    {
                        if (log != null)
                        {
                            _serviceCfgVerba.Gravar(dto, cs_colaborador_logado, log.id);
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
                            log = base._logger.LogInicioOperacao(cs_colaborador_logado, aplicacaoLogada_id, msgmLog, "Ocorreu uma exceção ao tentar inserir/atualizar Distribuição de Verbas.");
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

        [HttpGet]
        [Route("obter-configuracao-verba")]
        public IActionResult GetVerba()
        {
            try
            {
                var objRetorno = _serviceCfgVerba.ObterConfiguracaoVerba();
                return Ok(new { success = true, message = "", obj = objRetorno });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        [Route("cadastrar-atualizar-expiracao")]
        public IActionResult Post(ConfiguracaoExpiracaoPontosDTO dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string msgmLog = string.IsNullOrEmpty(dto.id.ToString()) ? "Cadastrar Expiração de Pontos" : "Atualizar Expiração de Pontos";

                    var log = base._logger.LogInicioOperacao(cs_colaborador_logado, aplicacaoLogada_id, msgmLog, "");

                    try
                    {
                        if (log != null)
                        {
                            _serviceCfgExp.Gravar(dto, cs_colaborador_logado, log.id);
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
                            log = base._logger.LogInicioOperacao(cs_colaborador_logado, aplicacaoLogada_id, msgmLog, "Ocorreu uma exceção ao tentar inserir/atualizar Expiração de Pontos.");
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

        [HttpGet]
        [Route("obter-configuracao-expiracao")]
        public IActionResult GetExpiracao()
        {
            try
            {
                var objRetorno = _serviceCfgExp.ObterConfiguracaoExpiracao();
                return Ok(new { success = true, message = "", obj = objRetorno });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}