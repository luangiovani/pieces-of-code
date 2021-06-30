using Database.Models.Loja;
using Domain.DTO.Loja;
using Domain.Repositories.Loja;
using System;
using System.Collections.Generic;

namespace Domain.Services.Loja
{
    /// <autor>
    /// Luan Fernandes - Ewave | 04/2019
    /// </autor>
    /// <tarefa>
    /// Development of the Employee Reward Program application
    /// </tarefa>
    /// <atividades>
    /// Service do Middleware para operações entre Front e Backend
    /// </atividades>
    /// <summary>
    /// Implementação da Interface de Service para chamadas das operações de banco de dados
    /// </summary>
    public class ProdutoService : BaseService<Produto>
    {
        /// <summary>
        /// Objeto do repositório de manipulação do service
        /// </summary>
        private readonly ProdutoRepository _repository;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="repository">Objeto injetado do repositório de banco de dados</param>
        public ProdutoService(ProdutoRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public ProdutoDTO Gravar(ProdutoDTO produtoDTO, string cs_colaborador_logado, Guid logOperacaoId)
        {
            Produto objProduto = null;
            if (!string .IsNullOrEmpty(produtoDTO.nome) && 
                !string .IsNullOrEmpty(produtoDTO.descricao) && 
                !string .IsNullOrEmpty(cs_colaborador_logado) && 
                string.IsNullOrEmpty(produtoDTO.id.ToString()))
            {
                objProduto = new Produto()
                {
                    descricao = produtoDTO.descricao,
                    disponibilidade = produtoDTO.disponibilidade,
                    /// 6A99C661-48E6-4E46-9B1F-168F4C6E513E = Loja Grife Sede
                    loja_id = string.IsNullOrEmpty(produtoDTO.loja_id) ? "6A99C661-48E6-4E46-9B1F-168F4C6E513E" : produtoDTO.loja_id,
                    nome = produtoDTO.nome,
                    observacao = produtoDTO.observacao,
                    valor_monetario = produtoDTO.valor_monetario,
                    valor_pontos = produtoDTO.valor_pontos,
                    ativo = true,
                    b64_imagem = produtoDTO.b64_imagem,
                    data_disponibilidade = DateTime.ParseExact(produtoDTO.data_disponibilidade, "dd/MM/yyyy", null),
                    data_hora_criacao = DateTime.Now,
                    cs_colaborador_criacao = cs_colaborador_logado
                };

                produtoDTO.id = _repository.Add(objProduto, logOperacaoId).id;
                return produtoDTO;
            }
            else if (!string .IsNullOrEmpty(produtoDTO.nome) && 
                     !string .IsNullOrEmpty(produtoDTO.descricao) && 
                     !string .IsNullOrEmpty(cs_colaborador_logado) && 
                     !string.IsNullOrEmpty(produtoDTO.id.ToString()))
            {

                objProduto = _repository.FindByID(produtoDTO.id.ToString());

                if (objProduto != null)
                {
                    objProduto.descricao = produtoDTO.descricao;
                    objProduto.disponibilidade = produtoDTO.disponibilidade;
                    objProduto.loja_id = string.IsNullOrEmpty(produtoDTO.loja_id) ? objProduto.loja_id : produtoDTO.loja_id;
                    objProduto.nome = produtoDTO.nome;
                    objProduto.observacao = produtoDTO.observacao;
                    objProduto.valor_monetario = produtoDTO.valor_monetario;
                    objProduto.valor_pontos = produtoDTO.valor_pontos;
                    objProduto.ativo = produtoDTO.ativo;
                    objProduto.b64_imagem = produtoDTO.b64_imagem;
                    objProduto.data_disponibilidade = DateTime.ParseExact(produtoDTO.data_disponibilidade, "dd/MM/yyyy", null);
                    objProduto.data_hora_alteracao = DateTime.Now;
                    objProduto.cs_colaborador_alteracao = cs_colaborador_logado;

                    _repository.Update(objProduto, logOperacaoId);

                    return produtoDTO;
                }
                else
                    throw new InvalidOperationException("Produto não encontrado para efetuar a atualização.");
            }
            else
                return null;
        }

        public IEnumerable<ProdutosMaisTrocadosDTO> ListarProdutos()
        {
            return _repository.ListarProdutos();
        }

        public IEnumerable<OpcaoDTO> OpcoesValoresOpcoes()
        {
            return _repository.OpcoesValoresOpcoes();
        }

        public void RedimensionarImagens()
        {
            _repository.RedimensionarImagens();
        }
    }
}