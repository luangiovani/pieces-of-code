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
    /// Implementação da Interface de Queries para operações de banco de dados da tabela LogOperacoes
    /// </summary>
    public class LogOperacoesQueries : IQueries
    {
        /// <summary>
        /// Comando de inserção de registro no banco de dados
        /// </summary>
        public string INSERT => @"
                DECLARE @NID uniqueidentifier = NEWID();

                INSERT INTO [APP].[LogOperacoes]
                       ([id]
                       ,[cs_colaborador]
                       ,[aplicacao_id]
                       ,[data_hora_inicio]
                       ,[data_hora_fim]
                       ,[operacao]
                       ,[observacao]
                       ,[erro])
                 VALUES
                       (@NID
                       ,@cs_colaborador
                       ,@aplicacao_id
                       ,@data_hora_inicio
                       ,@data_hora_fim
                       ,@operacao
                       ,@observacao
                       ,@erro);

                SELECT id,
	                   sequencial,
	                   cs_colaborador,
	                   CONVERT(VARCHAR(255), aplicacao_id) aplicacao_id,
	                   data_hora_inicio,
	                   data_hora_fim,
	                   operacao,
	                   observacao,
	                   erro
                  FROM APP.LogOperacoes (NOLOCK) 
                 WHERE id = @NID;";

        /// <summary>
        /// Comando de atualização do registro no banco de dados
        /// </summary>
        public string UPDATE => @"
                UPDATE [APP].[LogOperacoes]
                   SET [cs_colaborador] = @cs_colaborador
                      ,[aplicacao_id] = @aplicacao_id
                      ,[data_hora_inicio] = @data_hora_inicio
                      ,[data_hora_fim] = @data_hora_fim
                      ,[operacao] = @operacao
                      ,[observacao] = @observacao
                      ,[erro] = @erro
                 WHERE id = @id";

        /// <summary>
        /// Comando de deleção (atualização para ativo = 0) do registro no banco de dados
        /// </summary>
        public string DELETE => @"
                DELETE FROM [APP].[LogOperacoes]
                 WHERE id = @id";

        /// <summary>
        /// Comando de deleção (atualização para ativo = 0) do registro no banco de dados
        /// </summary>
        public string ACTIVATE => @"";

        /// <summary>
        /// Comando para selecionar registros do banco de dados
        /// </summary>
        public string SELECT => @"
              SELECT LO.[id]
                    ,LO.[sequencial]
                    ,LO.[cs_colaborador]
		            ,CO.[nome] nome_colaborador
                    ,CONVERT(VARCHAR(255), LO.aplicacao_id) aplicacao_id
                    ,LO.[data_hora_inicio]
                    ,LO.[data_hora_fim]
                    ,LO.[operacao]
                    ,LO.[observacao]
                    ,LO.[erro]
                FROM [APP].[LogOperacoes] LO (NOLOCK)
	            JOIN [Gestao].[Colaborador] CO (NOLOCK) ON CO.cs = LO.cs_colaborador";

        /// <summary>
        /// Selecionar o registro pelo ID informado no banco de dados
        /// </summary>
        public string SELECT_WHERE_ID =>
            SELECT + @"
                 WHERE LO.id = @id";

        /// <summary>
        /// Selecionar somente os registros ativos
        /// </summary>
        public string SELECT_WHERE_ATIVO =>
            SELECT + @"
                 WHERE ISNULL(LO.ativo,1) = 1";

        /// <summary>
        /// Selecionar somente os registros inativos
        /// </summary>
        public string SELECT_WHERE_INATIVO =>
            SELECT + @"
                 WHERE ISNULL(LO.ativo,1) = 0";
    }
}
