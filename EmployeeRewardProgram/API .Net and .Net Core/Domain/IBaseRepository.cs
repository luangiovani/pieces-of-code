using System;
using System.Collections.Generic;

namespace Domain
{
    /// <autor>
    /// Luan Fernandes - Ewave | 04/2019
    /// </autor>
    /// <tarefa>
    /// Development of the Employee Reward Program application
    /// </tarefa>
    /// <atividades>
    /// Criação de Interface de Repositórios para operações de banco de dados
    /// </atividades>
    /// <summary>
    /// Interface de Repositório para operações de banco de dados
    /// </summary>
    /// <typeparam name="Model">Model é o objeto da Entidade de Banco de dados a que o repository é o manipulador</typeparam>
    public interface IBaseRepository<Model> : IDisposable
    {
        /// <summary>
        /// Contrato para método de inserção no banco de dados
        /// </summary>
        /// <param name="item">Objeto com as informações para inserção no banco de dados</param>
        /// <param name="logOperacaoId">Id do Log de Operação que a transação pertence</param>
        /// <returns>Objeto de entidade inserido no banco de dados</returns>
        Model Add(Model item, Guid logOperacaoId);

        /// <summary>
        /// Contrato para método de deleção (inativação) no banco de dados
        /// </summary>
        /// <param name="id">Id do registro no banco de dados</param>
        /// <param name="cs_colaborador_logado">Id do usuário logado que está realizando a operação</param>
        /// <param name="logOperacaoId">Id do Log de Operação que a transação pertence</param>
        void Remove(string id, string cs_colaborador_logado, Guid logOperacaoId);

        /// <summary>
        /// Contrato para método de deleção (ativação) no banco de dados
        /// </summary>
        /// <param name="id">Id do registro no banco de dados</param>
        /// <param name="cs_colaborador_logado">Id do usuário logado que está realizando a operação</param>
        /// <param name="logOperacaoId">Id do Log de Operação que a transação pertence</param>
        void Activate(string id, string cs_colaborador_logado, Guid logOperacaoId);

        /// <summary>
        /// Contrato para método de atualização do registro no banco de dados
        /// </summary>
        /// <param name="item">Objeto com as informações para atualização do registro no banco de dados</param>
        /// <param name="logOperacaoId">Id do Log de Operação que a transação pertence</param>
        void Update(Model item, Guid logOperacaoId);

        /// <summary>
        /// Contrato para método para obter o registro no banco de dados pelo Id informado
        /// </summary>
        /// <param name="id">Id do registro no banco de dados</param>
        /// <returns>Objeto de entidade recuperado no banco de dados</returns>
        Model FindByID(string id);

        /// <summary>
        /// Contrato para método de recuperar todos os registros do banco de dados
        /// </summary>
        /// <returns>Lista de objetos de entidade recuperados do banco de dados</returns>
        IEnumerable<Model> FindAll();
    }
}
