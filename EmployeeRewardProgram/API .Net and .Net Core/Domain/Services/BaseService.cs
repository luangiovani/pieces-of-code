using Domain.Repositories;
using System;
using System.Collections.Generic;

namespace Domain.Services
{
    /// <autor>
    /// Luan Fernandes - Ewave | 04/2019
    /// </autor>
    /// <tarefa>
    /// Development of the Employee Reward Program application
    /// </tarefa>
    /// <atividades>
    /// Implementação da Interface de Services para manipulações e chamadas das operações de banco de dados
    /// </atividades>
    /// <summary>
    /// Implementação da Interface de Services para manipulações e chamadas das operações de banco de dados
    /// Classe base que será herdada pelas outras implementações
    /// </summary>
    public class BaseService<Model> : IDisposable, IBaseService<Model>
        where Model : class
    {
        /// <summary>
        /// Objeto do repositório da implementação deste service
        /// </summary>
        private readonly BaseRepository<Model> _repository;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="repository">Injeção do repositório para o service</param>
        public BaseService(BaseRepository<Model> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Adicionar um novo registro no banco de dados
        /// </summary>
        /// <param name="item">Objeto com os dados para inserção</param>
        /// <param name="logOperacaoId">Id do Log de Operação que a transação pertence</param>
        /// <returns>Id do registro gravado.</returns>
        public virtual Model Add(Model item, Guid logOperacaoId)
        {
            return _repository.Add(item, logOperacaoId);
        }

        /// <summary>
        /// Remover ou Inativar um registro
        /// </summary>
        /// <param name="id">Id do registro que se deseja realizar a operação</param>
        /// <param name="cs_colaborador_logado"></param>
        /// <param name="logOperacaoId">Id do Log de Operação que a transação pertence</param>
        public virtual void Remove(string id, string cs_colaborador_logado, Guid logOperacaoId)
        {
            _repository.Remove(id, cs_colaborador_logado, logOperacaoId);
        }

        /// <summary>
        /// Ativar um registro
        /// </summary>
        /// <param name="id">Id do registro que se deseja realizar a operação</param>
        /// <param name="cs_colaborador_logado"></param>
        /// <param name="logOperacaoId">Id do Log de Operação que a transação pertence</param>
        public virtual void Activate(string id, string cs_colaborador_logado, Guid logOperacaoId)
        {
            _repository.Activate(id, cs_colaborador_logado, logOperacaoId);
        }

        /// <summary>
        /// Atualizar um registro no banco de dados
        /// </summary>
        /// <param name="item">Objeto de parâmetro com as informações para atualizar o registro no banco de dados</param>
        /// <param name="logOperacaoId">Id do Log de Operação que a transação pertence</param>
        public virtual void Update(Model item, Guid logOperacaoId)
        {
            _repository.Update(item, logOperacaoId);
        }

        /// <summary>
        /// Obter registro pelo ID informado
        /// </summary>
        /// <param name="id">Id do registro que se deseja obter o detalhamento</param>
        /// <returns>Objeto Model localizado pelo Id Informado</returns>
        public virtual Model FindByID(string id)
        {
            return _repository.FindByID(id);
        }

        /// <summary>
        /// Obter todos os registros do banco de dados
        /// </summary>
        /// <returns>Lista de Objetos do tipo Model</returns>
        public virtual IEnumerable<Model> FindAll()
        {
            return _repository.FindAll();
        }

        /// <summary>
        /// Método para encerrar e liberar a memória deste objeto injetado
        /// </summary>
        public virtual void Dispose()
        {
            _repository.Dispose();
        }
    }
}