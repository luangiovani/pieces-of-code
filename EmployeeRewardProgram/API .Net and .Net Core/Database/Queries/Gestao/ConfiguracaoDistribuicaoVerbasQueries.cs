namespace Database.Queries.Gestao
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
    /// Implementação da Interface de Queries para operações de banco de dados da tabela ConfiguracaoDistribuicaoVerbas
    /// </summary>
    public class ConfiguracaoDistribuicaoVerbasQueries : IQueries
    {
        /// <summary>
        /// Comando de inserção de registro no banco de dados
        /// </summary>
        public string INSERT => @"
                DECLARE @NID VARCHAR(128) = NEWID();

                INSERT INTO [Gestao].[ConfiguracaoDistribuicaoVerbas]
                           ([id]
                           ,[pontos_minimos]
                           ,[pontos_por_colaborador]
                           ,[pontos_por_area]
                           ,[disponivel_apartir]
                           ,[bloquear_apartir]
                           ,[ativo]
                           ,[data_hora_criacao]
                           ,[cs_colaborador_criacao]
                           ,[data_hora_alteracao]
                           ,[cs_colaborador_alteracao])
                     VALUES
                           (@NID
                           ,@pontos_minimos
                           ,@pontos_por_colaborador
                           ,@pontos_por_area
                           ,@disponivel_apartir
                           ,@bloquear_apartir
                           ,@ativo
                           ,@data_hora_criacao
                           ,@cs_colaborador_criacao
                           ,@data_hora_alteracao
                           ,@cs_colaborador_alteracao);

                    SELECT * FROM [Gestao].[ConfiguracaoDistribuicaoVerbas](NOLOCK) WHERE id = @NID;";

        /// <summary>
        /// Comando de atualização do registro no banco de dados
        /// </summary>
        public string UPDATE => @"
                UPDATE [Gestao].[ConfiguracaoDistribuicaoVerbas]
                   SET [pontos_minimos] = @pontos_minimos
                      ,[pontos_por_colaborador] = @pontos_por_colaborador
                      ,[pontos_por_area] = @pontos_por_area
                      ,[disponivel_apartir] = @disponivel_apartir
                      ,[bloquear_apartir] = @bloquear_apartir
                      ,[ativo] = @ativo
                      ,[data_hora_criacao] = GETDATE()
                      ,[cs_colaborador_criacao] = @cs_colaborador_criacao
                      ,[data_hora_alteracao] = GETDATE()
                      ,[cs_colaborador_alteracao] = @cs_colaborador_alteracao
                 WHERE id = @id";

        /// <summary>
        /// Comando de deleção (atualização para ativo = 0) do registro no banco de dados
        /// </summary>
        public string DELETE => @"
                UPDATE [Gestao].[ConfiguracaoDistribuicaoVerbas]
                   SET [ativo] = 0,
                       [cs_colaborador_alteracao] = @cs_colaborador_alteracao,
                       [data_hora_alteracao] = GETDATE()
                 WHERE id = @id";

        /// <summary>
        /// Comando de deleção (atualização para ativo = 0) do registro no banco de dados
        /// </summary>
        public string ACTIVATE => @"
            UPDATE [Gestao].[ConfiguracaoDistribuicaoVerbas]
               SET [ativo] = 1,
                   [cs_colaborador_alteracao] = @cs_colaborador_alteracao,
                   [data_hora_alteracao] = GETDATE()
             WHERE id = @id";

        /// <summary>
        /// Comando para selecionar registros do banco de dados
        /// </summary>
        public string SELECT => @"
                SELECT [id]
                      ,[sequencial]
                      ,[pontos_minimos]
                      ,[pontos_por_colaborador]
                      ,[pontos_por_area]
                      ,[disponivel_apartir]
                      ,[bloquear_apartir]
                      ,[ativo]
                      ,[data_hora_criacao]
                      ,[cs_colaborador_criacao]
                      ,[data_hora_alteracao]
                      ,[cs_colaborador_alteracao]
                  FROM [Gestao].[ConfiguracaoDistribuicaoVerbas](NOLOCK)";

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
