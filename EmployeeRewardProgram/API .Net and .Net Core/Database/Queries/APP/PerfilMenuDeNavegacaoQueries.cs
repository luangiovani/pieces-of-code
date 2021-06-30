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
    /// Implementação da Interface de Queries para operações de banco de dados da tabela PerfilMenuDeNavegacao
    /// </summary>
    public class PerfilMenuDeNavegacaoQueries : IQueries
    {
        /// <summary>
        /// Comando de inserção de registro no banco de dados
        /// </summary>
        public string INSERT => @"
                DECLARE @NID VARCHAR(128) = NEWID();

                INSERT INTO [APP].[PerfilMenuDeNavegacao]
                           ([id]
                           ,[perfil_id]
                           ,[menu_navegacao_id]
                           ,[visualizar]
                           ,[cadastrar]
                           ,[excluir]
                           ,[ativo]
                           ,[data_hora_criacao]
                           ,[cs_colaborador_criacao]
                           ,[data_hora_alteracao]
                           ,[cs_colaborador_alteracao])
                     VALUES
                           (@NID
                           ,@perfil_id
                           ,@menu_navegacao_id
                           ,@visualizar
                           ,@cadastrar
                           ,@excluir
                           ,@ativo
                           ,@data_hora_criacao
                           ,@cs_colaborador_criacao
                           ,@data_hora_alteracao
                           ,@cs_colaborador_alteracao);

                    SELECT [id]
                           ,[sequencial]
                           ,CONVERT(VARCHAR(255), [perfil_id]) perfil_id
                           ,CONVERT(VARCHAR(255), [menu_navegacao_id]) menu_navegacao_id
                           ,ISNULL([visualizar],0) visualizar
                           ,ISNULL([cadastrar],0) cadastrar
                           ,ISNULL([excluir],0) excluir
                           ,ISNULL([ativo],0) ativo
                           ,[data_hora_criacao]
                           ,[cs_colaborador_criacao]
                           ,[data_hora_alteracao]
                           ,[cs_colaborador_alteracao]
                      FROM [APP].[PerfilMenuDeNavegacao] (NOLOCK) 
                     WHERE id = @NID;";

        /// <summary>
        /// Comando de atualização do registro no banco de dados
        /// </summary>
        public string UPDATE => @"
                UPDATE [APP].[PerfilMenuDeNavegacao]
                   SET [perfil_id] = @perfil_id
                      ,[menu_navegacao_id] = @menu_navegacao_id
                      ,[visualizar] = @visualizar
                      ,[cadastrar] = @cadastrar
                      ,[excluir] = @excluir
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
                UPDATE [APP].[PerfilMenuDeNavegacao]
                   SET [ativo] = 0,
                       [cs_colaborador_alteracao] = @cs_colaborador_alteracao,
                       [data_hora_alteracao] = GETDATE()
                 WHERE id = @id";

        /// <summary>
        /// Comando de deleção (atualização para ativo = 0) do registro no banco de dados
        /// </summary>
        public string ACTIVATE => @"
                UPDATE [APP].[PerfilMenuDeNavegacao]
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
                       ,CONVERT(VARCHAR(255), [perfil_id]) perfil_id
                       ,CONVERT(VARCHAR(255), [menu_navegacao_id]) menu_navegacao_id
                       ,ISNULL([visualizar],0) visualizar
                       ,ISNULL([cadastrar],0) cadastrar
                       ,ISNULL([excluir],0) excluir
                       ,ISNULL([ativo],0) ativo
                       ,[data_hora_criacao]
                       ,[cs_colaborador_criacao]
                       ,[data_hora_alteracao]
                       ,[cs_colaborador_alteracao]
                  FROM [APP].[PerfilMenuDeNavegacao] (NOLOCK)";

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
        /// Adiciona e Remove os Menus de Permissões para o Perfil
        /// </summary>
        public string VINCULAR_DESVINCULAR => @"
            /*
            INATIVA OS MENUS QUE NÃO SERÃO MAIS DESTE PERFIL
            */
            UPDATE APP.PerfilMenuDeNavegacao
               SET ativo = 0,
                   data_hora_alteracao = GETDATE(),
                   cs_colaborador_alteracao = @cs_colaborador
             WHERE CONVERT(VARCHAR(255), menu_navegacao_id) NOT IN @menu_list
               AND perfil_id = @perfil_id;

            /*
            INSERE OS MENUS QUE AINDA NÃO SÃO PERMITIDOS PARA O PERFIL
            */
            INSERT INTO APP.PerfilMenuDeNavegacao
            (perfil_id, menu_navegacao_id, visualizar, cadastrar, excluir, ativo, data_hora_criacao, cs_colaborador_criacao)
               SELECT @perfil_id, MN.id, 1, 1, 1, 1, GETDATE(), @cs_colaborador
                 FROM App.MenuDeNavegacao MN (NOLOCK)
            LEFT JOIN APP.PerfilMenuDeNavegacao (NOLOCK) PMN ON PMN.menu_navegacao_id = MN.id
                  AND PMN.perfil_id = @perfil_id
                WHERE CONVERT(VARCHAR(255), MN.id) IN @menu_list
	              AND PMN.id IS NULL;

            /*
            ATIVA OS MENUS QUE SERÃO DESTE PERFIL
            */
            UPDATE APP.PerfilMenuDeNavegacao
               SET ativo = 1,
                   data_hora_alteracao = GETDATE(),
                   cs_colaborador_alteracao = @cs_colaborador
             WHERE CONVERT(VARCHAR(255), menu_navegacao_id) IN @menu_list
               AND perfil_id = @perfil_id;
        ";

        /// <summary>
        /// Seleciona somente os IDs dos Menus que este Perfil tem permissão.
        /// </summary>
        public string SELECT_MENUS_ATIVO_PERFIL => @"
            SELECT CONVERT(VARCHAR(255), [menu_navegacao_id]) menu_navegacao_id
              FROM [APP].[PerfilMenuDeNavegacao] (NOLOCK)
             WHERE ativo = 1
               AND perfil_id = @perfil_id";
    }
}
