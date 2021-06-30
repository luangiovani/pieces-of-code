namespace Database.Queries
{
    /// <autor>
    /// Luan Fernandes - Ewave | 04/2019
    /// </autor>
    /// <tarefa>
    /// Development of the Employee Reward Program application
    /// </tarefa>
    /// <atividades>
    /// Queries para operações de banco de dados
    /// </atividades>
    /// <summary>
    /// Interface de Queries para operações de banco de dados
    /// </summary>
    public interface IQueries
    {
        /// <summary>
        /// Comando de inserção de registro no banco de dados
        /// </summary>
        string INSERT { get; }

        /// <summary>
        /// Comando de atualização do registro no banco de dados
        /// </summary>
        string UPDATE { get; }

        /// <summary>
        /// Comando de deleção (atualização para ativo = 0) do registro no banco de dados
        /// </summary>
        string DELETE { get; }

        /// <summary>
        /// Comando de deleção (atualização para ativo = 0) do registro no banco de dados
        /// </summary>
        string ACTIVATE { get; }

        /// <summary>
        /// Comando para selecionar registros do banco de dados
        /// </summary>
        string SELECT { get; }

        /// <summary>
        /// Selecionar o registro pelo ID informado no banco de dados
        /// </summary>
       string SELECT_WHERE_ID { get; }

        /// <summary>
        /// Selecionar somente os registros ativos
        /// </summary>
       string SELECT_WHERE_ATIVO { get; }

        /// <summary>
        /// Selecionar somente os registros inativos
        /// </summary>
       string SELECT_WHERE_INATIVO { get; }
    }
}
