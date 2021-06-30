namespace Database.Queries.Venda
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
    /// Implementação da Interface de Queries para operações de banco de dados da tabela ItemCompraOpcaoValor
    /// </summary>
    public class ItemCompraOpcaoValorQueries : IQueries
    {
        /// <summary>
        /// Comando de inserção de registro no banco de dados
        /// </summary>
        public string INSERT => @"
            DECLARE @NID VARCHAR(128) = NEWID();

            INSERT INTO [Venda].[ItemCompraOpcaoValor]
                   ([id]
                   ,[item_compra_id]
                   ,[opcao]
                   ,[observacao]
                   ,[valor]
                   ,[ativo]
                   ,[data_hora_criacao]
                   ,[cs_colaborador_criacao]
                   ,[data_hora_alteracao]
                   ,[cs_colaborador_alteracao])
             VALUES
                   (@NID
                   ,@item_compra_id
                   ,@opcao
                   ,@observacao
                   ,@valor
                   ,@ativo
                   ,@data_hora_criacao
                   ,@cs_colaborador_criacao
                   ,@data_hora_alteracao
                   ,@cs_colaborador_alteracao);
            
            SELECT * FROM [Venda].[ItemCompraOpcaoValor](NOLOCK) WHERE id = @NID;";

        /// <summary>
        /// Comando de atualização do registro no banco de dados
        /// </summary>
        public string UPDATE => @"
            UPDATE [Venda].[ItemCompraOpcaoValor]
               SET [item_compra_id] = @item_compra_id
                  ,[opcao] = @opcao
                  ,[observacao] = @observacao
                  ,[valor] = @valor
                  ,[ativo] = @ativo
                  ,[data_hora_criacao] = @data_hora_criacao
                  ,[cs_colaborador_criacao] = @cs_colaborador_criacao
                  ,[data_hora_alteracao] = GETDATE()
                  ,[cs_colaborador_alteracao] = @cs_colaborador_alteracao
             WHERE id = @id";

        /// <summary>
        /// Comando de deleção (atualização para ativo = 0) do registro no banco de dados
        /// </summary>
        public string DELETE => @"
            UPDATE [Venda].[ItemCompraOpcaoValor]
               SET [ativo] = 0,
                   [cs_colaborador_alteracao] = @cs_colaborador_alteracao,
                   [data_hora_alteracao] = GETDATE()
             WHERE id = @id";

        /// <summary>
        /// Comando de deleção (atualização para ativo = 0) do registro no banco de dados
        /// </summary>
        public string ACTIVATE => @"
                UPDATE [Venda].[ItemCompraOpcaoValor]
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
                  ,[item_compra_id]
                  ,[opcao]
                  ,[observacao]
                  ,[valor]
                  ,[ativo]
                  ,[data_hora_criacao]
                  ,[cs_colaborador_criacao]
                  ,[data_hora_alteracao]
                  ,[cs_colaborador_alteracao]
              FROM [Venda].[ItemCompraOpcaoValor](NOLOCK)";

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
