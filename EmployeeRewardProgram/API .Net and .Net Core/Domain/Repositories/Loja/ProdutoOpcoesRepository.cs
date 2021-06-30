﻿using Database.Models.Loja;
using Database.Queries.Loja;
using Microsoft.Extensions.Configuration;

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
    /// Implementação da Interface de Repositório para operações de banco de dados da tabela ProdutoOpcoes
    /// </summary>
    public class ProdutoOpcoesRepository : BaseRepository<ProdutoOpcoes>
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="configuration">Configuration para obter a connectionstring do banco de dados</param>
        /// <param name="queries">Objeto Queries para obter os comandos de banco de dados que serão utilizados pelo Repositório</param>
        public ProdutoOpcoesRepository(IConfiguration configuration, ProdutoOpcoesQueries queries)
            : base(configuration, queries) { }
    }
}
