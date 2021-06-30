using System;
using System.Linq;
using Domain.DTO;
using Domain.DTO.Loja;
using Domain.Services.APP;
using Domain.Services.Loja;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

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
    /// Controller para manipulação de requisições relacionadas a Produto
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class ProdutoController : BaseController
    {
        /// <summary>
        /// Objeto local de Serviço de operações relacionadas a Lojas
        /// </summary>
        private readonly ProdutoService _service;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="service">Injeção do Serviço das Opções de Entrega</param>
        /// <param name="logger">Injeção do Serviço de Log</param>
        public ProdutoController(ProdutoService service, IHttpContextAccessor httpContextAccessor, LogService logger) : base(httpContextAccessor, logger)
        {
            _service = service;
        }

        /// <summary>
        /// Cadastrar/Atualizar um produto
        /// </summary>
        /// <param name="produtoDTO">DTO com as informações de produto para cadastro/atualização</param>
        /// <returns>Response Status 200 caso sucesso, StatusCode do Erro com "error" descrição do erro.</returns>
        [HttpPost]
        [Route("cadastrar")]
        [Route("atualizar")]
        public IActionResult Post([FromBody]ProdutoDTO produtoDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   string msgmLog = string.IsNullOrEmpty(produtoDTO.id.ToString()) ? "Cadastrar novo Produto" : "Atualizar Produto";

                    var log = base._logger.LogInicioOperacao(cs_colaborador_logado, aplicacaoLogada_id, msgmLog, "");

                    try
                    {
                        if (log != null)
                        {
                            _service.Gravar(produtoDTO, cs_colaborador_logado, log.id);
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
                            log = base._logger.LogInicioOperacao(cs_colaborador_logado, aplicacaoLogada_id, msgmLog, "Ocorreu uma exceção ao tentar gravar produto");
                            base._logger.LogFimOperacao(log.id, exGravar.Message);
                        }

                        return BadRequest(new { error = "Erro ao gerar Log da Operação" });
                    }

                    return Ok( new { success = true, message = "Operação realizada com sucesso!" });
                }
                else
                    return BadRequest(produtoDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Ativar/Inativar produto pelo Id informado
        /// </summary>
        /// <param name="id">Id do produto para inativar</param>
        /// <returns>Response Status 200 caso sucesso, StatusCode do Erro com "error" descrição do erro.</returns>
        [HttpPost]
        [Route("ativar-inativar")]
        public IActionResult Post([FromBody] AtivarInativarDTO dto)
        {
            try
            {
                if (!string.IsNullOrEmpty(dto.id.ToString()) && dto.id.HasValue)
                {
                    var log = base._logger.LogInicioOperacao(cs_colaborador_logado, aplicacaoLogada_id, "Inativar Produto", "");

                    try
                    {
                        if (log != null)
                        {
                            _service.Remove(dto.id.ToString(), cs_colaborador_logado, log.id);
                            base._logger.LogFimOperacao(log.id, "");
                        }
                        else
                            return BadRequest(new { error = "Erro ao gerar Log da Operação" });
                    }
                    catch (Exception exInativar)
                    {
                        if (log != null)
                            base._logger.LogFimOperacao(log.id, exInativar.Message);
                        else
                        {
                            log = base._logger.LogInicioOperacao(cs_colaborador_logado, aplicacaoLogada_id, "Inativar Produto", "");
                            base._logger.LogFimOperacao(log.id, exInativar.Message);
                        }

                        return BadRequest(new { error = "Erro ao gerar Log da Operação" });
                    }

                    return Ok( new { success = true, message = "Operação realizada com sucesso!" });
                }
                else
                {
                    return BadRequest(new { error = "Id não informado." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Consultar Produto pelo Id informado
        /// </summary>
        /// <param name="id">Id do Produto para inativar</param>
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
                    return Ok(new { success = false, message = "Id não informado para obtenção do Produto." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("listar-produtos")]
        public IActionResult GetProdutos()
        {
            try
            {
                var produtos = _service.ListarProdutos();
                return Ok(new { success = true, message = "", results = produtos, total = produtos.Count() });
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
                var produtos = _service.FindAll()
                    .Select(p => new {
                        p.id,
                        p.sequencial,
                        p.nome,
                        p.descricao,
                        p.observacao,
                        p.valor_monetario,
                        p.valor_pontos,
                        p.ativo,
                        p.b64_imagem
                    }).OrderBy(p => p.valor_pontos)
                .ToList();

                return Ok(new { success = true, message = "", results = produtos, total = produtos.Count() });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        #region Opcoes e Valores de Opcoes

        [HttpGet]
        [Route("opcoes-valores")]
        public IActionResult GetOpcoes()
        {
            try
            {
                var opcoes = _service.OpcoesValoresOpcoes();
                return Ok(new { success = true, message = "", results = opcoes, total = opcoes.Count() });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        //[HttpGet]
        //[Route("auto")]
        //public IActionResult GetAuto()
        //{
        //    try
        //    {
        //        _service.RedimensionarImagens();
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { success = false, message = ex.Message });
        //    }
        //}

        #endregion
    }
}