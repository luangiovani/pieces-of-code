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
    /// Implementação da Interface de Queries para operações de banco de dados da tabela MenuDeNavegacao
    /// </summary>
    public class MenuDeNavegacaoQueries : IQueries
    {
        /// <summary>
        /// Comando de inserção de registro no banco de dados
        /// </summary>
        public string INSERT => @"
                DECLARE @NID VARCHAR(128) = NEWID();

                INSERT INTO [APP].[MenuDeNavegacao]
                           ([id]
                           ,[aplicacao_id]
                           ,[menu_superior_id]
                           ,[nome]
                           ,[ordem]
                           ,[controller]
                           ,[acao]
                           ,[ajuda]
                           ,[icone]
                           ,[ativo]
                           ,[data_hora_criacao]
                           ,[cs_colaborador_criacao]
                           ,[data_hora_alteracao]
                           ,[cs_colaborador_alteracao])
                     VALUES
                           (@NID
                           ,@aplicacao_id
                           ,@menu_superior_id
                           ,@nome
                           ,@ordem
                           ,@controller
                           ,@acao
                           ,@ajuda
                           ,@icone
                           ,@ativo
                           ,@data_hora_criacao
                           ,@cs_colaborador_criacao
                           ,@data_hora_alteracao
                           ,@cs_colaborador_alteracao);
                
                SELECT [id]
                      ,[sequencial]
                      ,CONVERT(VARCHAR(255), [aplicacao_id]) aplicacao_id
                      ,CONVERT(VARCHAR(255), [menu_superior_id]) menu_superior_id
                      ,[nome]
                      ,[ordem]
                      ,[controller]
                      ,[acao]
                      ,[ajuda]
                      ,[icone]
                      ,[ativo]
                      ,[data_hora_criacao]
                      ,[cs_colaborador_criacao]
                      ,[data_hora_alteracao]
                      ,[cs_colaborador_alteracao]
                 FROM [APP].[MenuDeNavegacao](NOLOCK) WHERE id = @NID;";

        /// <summary>
        /// Comando de atualização do registro no banco de dados
        /// </summary>
        public string UPDATE => @"
                UPDATE [APP].[MenuDeNavegacao]
                   SET [aplicacao_id] = @aplicacao_id
                      ,[menu_superior_id] = @menu_superior_id
                      ,[nome] = @nome
                      ,[ordem] = @ordem
                      ,[controller] = @controller
                      ,[acao] = @acao
                      ,[ajuda] = @ajuda
                      ,[icone] = @icone
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
                UPDATE [APP].[MenuDeNavegacao]
                   SET [ativo] = 0,
                       [cs_colaborador_alteracao] = @cs_colaborador_alteracao,
                       [data_hora_alteracao] = GETDATE()
                 WHERE id = @id";

        /// <summary>
        /// Comando de deleção (atualização para ativo = 0) do registro no banco de dados
        /// </summary>
        public string ACTIVATE => @"
                UPDATE [APP].[MenuDeNavegacao]
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
                      ,CONVERT(VARCHAR(255), [aplicacao_id]) aplicacao_id
                      ,CONVERT(VARCHAR(255), [menu_superior_id]) menu_superior_id
                      ,[nome]
                      ,[ordem]
                      ,[controller]
                      ,[acao]
                      ,[ajuda]
                      ,[icone]
                      ,[ativo]
                      ,[data_hora_criacao]
                      ,[cs_colaborador_criacao]
                      ,[data_hora_alteracao]
                      ,[cs_colaborador_alteracao]
                  FROM [APP].[MenuDeNavegacao] (NOLOCK)";

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

        /// <summary>
        /// Selecionar os Menus e SubMenus para listar em tela
        /// </summary>
        public string SELECT_LISTAR_MENUS => @"
            SELECT MN.[id]
                  ,MN.[sequencial]
                  ,CONVERT(VARCHAR(255), MN.[aplicacao_id]) aplicacao_id
                  ,AP.descricao aplicacao
                  ,CONVERT(VARCHAR(255), MN.[menu_superior_id]) menu_superior_id
		          ,ISNULL(MNS.nome,'') nome_menu_superior
                  ,MN.[nome]
                  ,MN.[ordem]
                  ,MN.[controller]
                  ,MN.[acao]
                  ,MN.[ajuda]
                  ,MN.[icone]
                  ,MN.[ativo]
                  ,MN.[data_hora_criacao]
                  ,MN.[cs_colaborador_criacao]
                  ,MN.[data_hora_alteracao]
                  ,MN.[cs_colaborador_alteracao]
             FROM [APP].[MenuDeNavegacao] (NOLOCK) MN
             JOIN [APP].[Aplicacao] (NOLOCK) AP ON AP.id = MN.aplicacao_id
        LEFT JOIN [APP].[MenuDeNavegacao] (NOLOCK) MNS ON MNS.id = MN.menu_superior_id
            WHERE MN.ativo = 1
              AND AP.ativo = 1
              AND MNS.ativo = 1
         ORDER BY MN.Sequencial,
		          ISNULL(MNS.menu_superior_id,MN.id), 
                  MN.menu_superior_id, 
		          MN.ordem";
    }
}
