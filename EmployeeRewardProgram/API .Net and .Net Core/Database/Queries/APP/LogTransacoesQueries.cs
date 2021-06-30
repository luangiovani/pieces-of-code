namespace Database.Queries.APP
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
    /// Implementação da Interface de Queries para operações de banco de dados da tabela LogTransacoes
    /// </summary>
    public class LogTransacoesQueries : IQueries
    {
        /// <summary>
        /// Comando de inserção de registro no banco de dados
        /// </summary>
        public string INSERT => @"
                DECLARE @NID uniqueidentifier = NEWID();

                INSERT INTO [APP].[LogTransacoes]
                       ([id]
                       ,[log_operacao_id]
                       ,[data_hora_inicio]
                       ,[data_hora_fim]
                       ,[transacao]
                       ,[observacao]
                       ,[erro])
                 VALUES
                       (@NID
                       ,@log_operacao_id
                       ,@data_hora_inicio
                       ,@data_hora_fim
                       ,@transacao
                       ,@observacao
                       ,@erro);

                SELECT [id]
                      ,[sequencial]
                      ,CONVERT(VARCHAR(255), [log_operacao_id]) log_operacao_id
                      ,[data_hora_inicio]
                      ,[data_hora_fim]
                      ,[transacao]
                      ,[observacao]
                      ,[erro] 
                 FROM [APP].[LogTransacoes] (NOLOCK) 
                WHERE id = @NID;";

        /// <summary>
        /// Comando de atualização do registro no banco de dados
        /// </summary>
        public string UPDATE => @"
                UPDATE [APP].[LogTransacoes]
                   SET [log_operacao_id] = @log_operacao_id
                      ,[data_hora_inicio] = @data_hora_inicio
                      ,[data_hora_fim] = @data_hora_fim
                      ,[transacao] = @transacao
                      ,[observacao] = @observacao
                      ,[erro] = @erro
                 WHERE id = @id";

        /// <summary>
        /// Comando de deleção (atualização para ativo = 0) do registro no banco de dados
        /// </summary>
        public string DELETE => @"
                DELETE FROM [APP].[LogTransacoes]
                 WHERE id = @id";

        /// <summary>
        /// Comando de deleção (atualização para ativo = 0) do registro no banco de dados
        /// </summary>
        public string ACTIVATE => @"";

        /// <summary>
        /// Comando para selecionar registros do banco de dados
        /// </summary>
        public string SELECT => @"
                SELECT [id]
                      ,[sequencial]
                      ,CONVERT(VARCHAR(255), [log_operacao_id]) log_operacao_id
                      ,[data_hora_inicio]
                      ,[data_hora_fim]
                      ,[transacao]
                      ,[observacao]
                      ,[erro]
                  FROM [APP].[LogTransacoes](NOLOCK)";

        /// <summary>
        /// Selecionar o registro pelo ID informado no banco de dados
        /// </summary>
        public string SELECT_WHERE_ID =>
            SELECT + @"
                 WHERE id = @id";

        /// <summary>
        /// Selecionar somente os registros ativos
        /// </summary>
        public string SELECT_WHERE_ATIVO =>
            SELECT + @"
                 WHERE ISNULL(ativo,1) = 1";

        /// <summary>
        /// Selecionar somente os registros inativos
        /// </summary>
        public string SELECT_WHERE_INATIVO =>
            SELECT + @"
                 WHERE ISNULL(ativo,1) = 0";
    }
}
