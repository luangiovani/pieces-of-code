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
    /// Implementação da Interface de Queries para operações de banco de dados da tabela Loja
    /// </summary>
    public class LojaQueries : IQueries
    {
        /// <summary>
        /// Comando de inserção de registro no banco de dados
        /// </summary>
        public string INSERT => @"
            DECLARE @NID VARCHAR(128) = NEWID();

            INSERT INTO [Gestao].[Loja]
                   ([id]
                   ,[nome]
                   ,[status_loja]
                   ,[codigo]
                   ,[observacao]
                   ,[complemento]
                   ,[localizacao]
                   ,[ativo]
                   ,[data_hora_criacao]
                   ,[cs_colaborador_criacao]
                   ,[data_hora_alteracao]
                   ,[cs_colaborador_alteracao])
             VALUES
                   (@NID
                   ,@nome
                   ,@status_loja
                   ,@codigo
                   ,@observacao
                   ,@complemento
                   ,@localizacao
                   ,@ativo
                   ,@data_hora_criacao
                   ,@cs_colaborador_criacao
                   ,@data_hora_alteracao
                   ,@cs_colaborador_alteracao);

            SELECT * FROM [Gestao].[Loja](NOLOCK) WHERE id = @NID;";

        /// <summary>
        /// Comando de atualização do registro no banco de dados
        /// </summary>
        public string UPDATE => @"
                UPDATE [Gestao].[Loja]
                   SET [nome] = @nome
                      ,[status_loja] = @status_loja
                      ,[codigo] = @codigo
                      ,[observacao] = @observacao
                      ,[complemento] = @complemento
                      ,[localizacao] = @localizacao
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
                UPDATE [Gestao].[Loja]
                   SET [ativo] = 0,
                       [cs_colaborador_alteracao] = @cs_colaborador_alteracao,
                       [data_hora_alteracao] = GETDATE()
                 WHERE id = @id";

        /// <summary>
        /// Comando de deleção (atualização para ativo = 0) do registro no banco de dados
        /// </summary>
        public string ACTIVATE => @"
            UPDATE [Gestao].[Loja]
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
                      ,[nome]
                      ,[status_loja]
                      ,[codigo]
                      ,[observacao]
                      ,[complemento]
                      ,[localizacao]
                      ,[ativo]
                      ,[data_hora_criacao]
                      ,[cs_colaborador_criacao]
                      ,[data_hora_alteracao]
                      ,[cs_colaborador_alteracao]
                  FROM [Gestao].[Loja](NOLOCK)";

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
        /// Selecionar todos os colaboradores vinculados a lojas ou não mas que possuam 
        /// Perfil de Loja
        /// </summary>
        public string SELECT_COLABORADORES_LOJA => @"
               SELECT CONVERT(VARCHAR(255), CLJ.id) id, 
                      ISNULL(CLJ.sequencial,0) sequencial,
	                  C.cs,
	                  C.nome,
                      ISNULL(C.email,'') email,
		              CONVERT(VARCHAR(255), LJ.id) loja_id,
		              LJ.nome loja
                 FROM Gestao.Colaborador (NOLOCK) C
	             JOIN APP.Perfil (NOLOCK) P ON P.id = C.perfil_id
            LEFT JOIN Gestao.ColaboradorLoja (NOLOCK) CLJ ON CLJ.cs = C.cs
            LEFT JOIN Gestao.Loja (NOLOCK) LJ ON LJ.id = CLJ.loja_id
                WHERE C.ativo = 1
                  AND P.ativo = 1
                  AND ISNULL(CLJ.ativo, 1) = 1
                  AND P.id = @perfilId 
                  AND ISNULL(CONVERT(VARCHAR(255), LJ.id),'') = CASE WHEN ISNULL(@lojaId,'') = '' THEN ISNULL(CONVERT(VARCHAR(255), LJ.id),'') ELSE @lojaId END
                  AND C.cs = CASE WHEN ISNULL(@cs,'') = '' THEN C.cs ELSE @cs END
             GROUP BY CONVERT(VARCHAR(255), CLJ.id), 
                      ISNULL(CLJ.sequencial,0),
	                  C.cs,
	                  C.nome,
                      ISNULL(C.email,''),
		              CONVERT(VARCHAR(255), LJ.id),
		              LJ.nome; ";

        public string INSERT_COLABORADOR_LOJA => @"
                DECLARE @NID VARCHAR(128) = NEWID();

                INSERT INTO Gestao.ColaboradorLoja
                ([id],
                 [cs],
                 [email],
                 [loja_id],
                 [ativo],
                 [data_hora_criacao],
                 [cs_colaborador_criacao])
                VALUES
                (@NID,
                 @cs,
                 @email,
                 @loja_id,
                 1,
                 GETDATE(),
                 @cs_colaborador_logado);

               SELECT CONVERT(VARCHAR(255), CLJ.id) id, 
                      C.sequencial,
	                  C.cs,
	                  C.nome,
                      ISNULL(C.email,'') email,
		              CONVERT(VARCHAR(255), LJ.id) loja_id,
		              LJ.nome loja
                 FROM Gestao.Colaborador (NOLOCK) C
	             JOIN APP.Perfil (NOLOCK) P ON P.id = C.perfil_id
            LEFT JOIN Gestao.ColaboradorLoja (NOLOCK) CLJ ON CLJ.cs = C.cs
            LEFT JOIN Gestao.Loja (NOLOCK) LJ ON LJ.id = CLJ.loja_id
                WHERE CLJ.id = @NID
             GROUP BY CONVERT(VARCHAR(255), CLJ.id), 
                      C.sequencial,
	                  C.cs,
	                  C.nome,
                      ISNULL(C.email,''),
		              CONVERT(VARCHAR(255), LJ.id),
		              LJ.nome;
            ";

        public string UPDATE_COLABORADOR_LOJA => @"
            UPDATE Gestao.ColaboradorLoja
               SET cs = @cs,
                   loja_id = @lojaId,
                   data_hora_alteracao = GETDATE(),
                   cs_colaborador_alteracao = @cs_colaborador_logado
             WHERE id = @id;
        ";

        public string DESVINCULAR_COLABORADOR_LOJA => @"
            DELETE FROM Gestao.ColaboradorLoja WHERE id = @id;
        ";

    }
}
