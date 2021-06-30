using Dapper;
using Database.Models.Loja;
using Database.Queries.Loja;
using Domain.DTO.Loja;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Domain.Repositories.Loja
{
    /// <autor>
    /// Luan Fernandes - Ewave | 04/2019
    /// </autor>
    /// <tarefa>
    /// Development of the Employee Reward Program application
    /// </tarefa>
    /// <atividades>
    /// Repositórios para operações de banco de dados
    /// </atividades>
    /// <summary>
    /// Implementação da Interface de Repositório para operações de banco de dados da tabela Produto
    /// </summary>
    public class ProdutoRepository : BaseRepository<Produto>
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="configuration">Configuration para obter a connectionstring do banco de dados</param>
        /// <param name="queries">Objeto Queries para obter os comandos de banco de dados que serão utilizados pelo Repositório</param>
        public ProdutoRepository(IConfiguration configuration, ProdutoQueries queries)
            : base(configuration, queries) { }

        public IEnumerable<ProdutosMaisTrocadosDTO> ListarProdutos()
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                dbConnection.Open();
                return dbConnection.Query<ProdutosMaisTrocadosDTO>(new ProdutoQueries().SELECT_LISTAR);
            }
        }

        public IEnumerable<OpcaoDTO> OpcoesValoresOpcoes()
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                dbConnection.Open();
                var opcoes = new Dictionary<Guid?, OpcaoDTO>();
                dbConnection.Query<OpcaoDTO, ValoresOpcoesDTO, OpcaoDTO>(
                    new ProdutoQueries().OPCOES_VALORES,
                    (opcao, valor) =>
                    {
                        OpcaoDTO opDto;
                        if (!opcoes.TryGetValue(opcao.id, out opDto))
                        {
                            opcoes.Add(opcao.id, opDto = opcao);
                        }

                        if (valor != null)
                        {
                            if (!opDto.Valores.Any(x => x.id == valor.id))
                            {
                                opDto.Valores.Add(valor);
                            }
                        }

                        return opDto;
                    },
                    splitOn: "id, opcao_id"
                );

                return opcoes.Values;
            }
        }

        public void RedimensionarImagens()
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                dbConnection.Open();
                var prods = dbConnection.Query<ProdutosColaboradorTrocaDTO>(new ProdutoQueries().SELECT_LISTAR);
                foreach (var item in prods)
                {
                    if (!string.IsNullOrEmpty(item.b64Imagem))
                    {
                        string vUP = "UPDATE Loja.Produto SET b64_Imagem = @b64Imagem WHERE id = @id";
                        dbConnection.Execute(vUP, new { item.b64Imagem, item.id });
                    }
                }
            }
        }
    }
}