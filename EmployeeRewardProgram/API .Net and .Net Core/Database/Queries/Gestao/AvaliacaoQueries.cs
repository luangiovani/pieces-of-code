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
    /// Implementação da Interface de Queries para operações de banco de dados da tabela Avaliacao
    /// </summary>
    public class AvaliacaoQueries : IQueries
    {
        /// <summary>
        /// Comando de inserção de registro no banco de dados
        /// </summary>
        public string INSERT => @"
                DECLARE @NID VARCHAR(128) = NEWID();

                INSERT INTO [Gestao].[Avaliacao]
                           ([id]
                           ,[recomendacao_id]
                           ,[cs_colaborador_avaliador]
                           ,[situacao_avaliacao_id]
                           ,[data_avaliacao]
                           ,[justificativa]
                           ,[ativo]
                           ,[data_hora_alteracao]
                           ,[cs_colaborador_alteracao])
                     VALUES
                           (@NID
                           ,@recomendacao_id
                           ,@cs_colaborador_avaliador
                           ,@situacao_avaliacao_id
                           ,@data_avaliacao
                           ,@justificativa
                           ,@ativo
                           ,@data_hora_alteracao
                           ,@cs_colaborador_alteracao);

                SELECT  A.[id]
                       ,A.[sequencial]
                       ,CONVERT(VARCHAR(255), A.[recomendacao_id]) recomendacao_id
                       ,A.[cs_colaborador_avaliador]
                       ,ISNULL(C.email,'') email_colaborador_avaliador
                       ,CONVERT(VARCHAR(255), A.[situacao_avaliacao_id]) situacao_avaliacao_id
                       ,A.[data_avaliacao]
                       ,A.[justificativa]
                       ,A.[ativo]
                       ,A.[data_hora_alteracao]
                       ,A.[cs_colaborador_alteracao] 
                  FROM [Gestao].[Avaliacao](NOLOCK) A
                  JOIN [Gestao].[Colaborador](NOLOCK) C ON C.cs = A.cs_colaborador_avaliador
                 WHERE A.id = @NID;";

        /// <summary>
        /// Comando de atualização do registro no banco de dados
        /// </summary>
        public string UPDATE => @"
                UPDATE [Gestao].[Avaliacao]
                   SET [recomendacao_id] = @recomendacao_id
                      ,[cs_colaborador_avaliador] = @cs_colaborador_avaliador
                      ,[situacao_avaliacao_id] = @situacao_avaliacao_id
                      ,[data_avaliacao] = @data_avaliacao
                      ,[justificativa] = @justificativa
                      ,[ativo] = @ativo
                      ,[data_hora_alteracao] = GETDATE()
                      ,[cs_colaborador_alteracao] = @cs_colaborador_alteracao
                 WHERE id = @id";

        /// <summary>
        /// Comando de deleção (atualização para ativo = 0) do registro no banco de dados
        /// </summary>
        public string DELETE => @"
                UPDATE [Gestao].[Avaliacao]
                   SET [ativo] = 0,
                       [cs_colaborador_alteracao] = @cs_colaborador_alteracao,
                       [data_hora_alteracao] = GETDATE()
                 WHERE id = @id";

        /// <summary>
        /// Comando de deleção (atualização para ativo = 0) do registro no banco de dados
        /// </summary>
        public string ACTIVATE => @"
            UPDATE [Gestao].[Avaliacao]
               SET [ativo] = 1,
                   [cs_colaborador_alteracao] = @cs_colaborador_alteracao,
                   [data_hora_alteracao] = GETDATE()
             WHERE id = @id";

        /// <summary>
        /// Comando para selecionar registros do banco de dados
        /// </summary>
        public string SELECT => @"
                SELECT A.[id]
                       ,A.[sequencial]
                       ,CONVERT(VARCHAR(255), A.[recomendacao_id]) recomendacao_id
                       ,A.[cs_colaborador_avaliador]
                       ,ISNULL(C.email,'') email_colaborador_avaliador
                       ,CONVERT(VARCHAR(255), A.[situacao_avaliacao_id]) situacao_avaliacao_id
                       ,A.[data_avaliacao]
                       ,A.[justificativa]
                       ,A.[ativo]
                       ,A.[data_hora_alteracao]
                       ,A.[cs_colaborador_alteracao] 
                  FROM [Gestao].[Avaliacao](NOLOCK) A
                  JOIN [Gestao].[Colaborador](NOLOCK) C ON C.cs = A.cs_colaborador_avaliador ";

        /// <summary>
        /// Selecionar o registro pelo ID informado no banco de dados
        /// </summary>
        public string SELECT_WHERE_ID => 
            SELECT + @"
                 WHERE A.id = @id";

        /// <summary>
        /// Selecionar somente os registros ativos
        /// </summary>
        public string SELECT_WHERE_ATIVO =>
            SELECT + @"
                 WHERE ISNULL(A.ativo,1) = 1";

        /// <summary>
        /// Selecionar somente os registros inativos
        /// </summary>
        public string SELECT_WHERE_INATIVO =>
            SELECT + @"
                 WHERE ISNULL(A.ativo,1) = 0";

        /// <summary>
        /// Selecionar as Avaliações que estão Pendentes de análise pelo Gestor
        /// </summary>
        public string SELECT_AVALIACOES_GESTOR => @"
              SELECT A.[id]
                    ,A.[sequencial]
                    ,CONVERT(VARCHAR(10), A.data_avaliacao, 103) data
		            ,C.cs
		            ,C.nome colaborador
		            ,TR.nome motivo
		            ,R.qtde_pontos pontos
                FROM [Gestao].[Avaliacao](NOLOCK) A
	            JOIN Gestao.Recomendacao (NOLOCK) R ON R.id = A.recomendacao_id
	            JOIN Gestao.Colaborador (NOLOCK) C ON C.cs = R.cs_colaborador
	            JOIN Gestao.TipoRecomendacao (NOLOCK) TR ON TR.id = R.tipo_recomendacao_id
               WHERE ISNULL(A.ativo,1) = 1
                 AND A.cs_colaborador_avaliador = @cs_gestor
                 AND A.situacao_avaliacao_id = 'C402396C-4938-4FAB-9CDF-6B94BE5C5802'
        ";

        /// <summary>
        /// Selecionar as Avaliações que estão Pendentes de análise pelo Gestor
        /// </summary>
        public string SELECT_AVALIACOES_REALIZADAS_GESTOR => @"
              SELECT A.[id]
                    ,A.[sequencial]
                    ,CONVERT(VARCHAR(10), A.data_avaliacao, 103) data
		            ,C.cs
		            ,C.nome colaborador
		            ,TR.nome motivo
		            ,R.qtde_pontos pontos
                    ,SA.nome situacao
                FROM [Gestao].[Avaliacao](NOLOCK) A
	            JOIN Gestao.Recomendacao (NOLOCK) R ON R.id = A.recomendacao_id
	            JOIN Gestao.Colaborador (NOLOCK) C ON C.cs = R.cs_colaborador
	            JOIN Gestao.TipoRecomendacao (NOLOCK) TR ON TR.id = R.tipo_recomendacao_id
                JOIN Gestao.SituacaoAvaliacao (NOLOCK) SA ON SA.id = A.situacao_avaliacao_id
               WHERE ISNULL(A.ativo,1) = 1
                 AND A.cs_colaborador_avaliador = @cs_gestor
                 AND A.situacao_avaliacao_id <> 'C402396C-4938-4FAB-9CDF-6B94BE5C5802'
        ";

        /// <summary>
        /// Selecionar as Avaliações que estão Ativas de uma determinada Recomendação
        /// Data da Avaliação é NULL se a Situação é Pendente 'C402396C-4938-4FAB-9CDF-6B94BE5C5802'
        /// </summary>
        public string SELECT_AVALIACOES_ATIVAS_RECOMENDACAO => @"
              SELECT A.[id]
                    ,A.[sequencial]
		            ,CONVERT(VARCHAR(255), A.recomendacao_id) recomendacao_id
		            ,A.cs_colaborador_avaliador
		            ,(CA.cs + ' - ' + CA.nome) nome_colaborador_avaliador
		            ,CA.email email_colaborador_avaliador
		            ,GCA.nomenclatura cargo_avaliador
		            ,SA.nome situacao_avaliacao
                    ,CONVERT(VARCHAR(255), A.situacao_avaliacao_id) situacao_avaliacao_id
                    ,(CASE WHEN A.situacao_avaliacao_id <> 'C402396C-4938-4FAB-9CDF-6B94BE5C5802' THEN CONVERT(VARCHAR(10), A.data_avaliacao, 103) ELSE '' END) data
		            ,C.cs
		            ,C.nome colaborador
		            ,TR.nome motivo
		            ,R.qtde_pontos pontos
                FROM [Gestao].[Avaliacao](NOLOCK) A
	            JOIN Gestao.Recomendacao (NOLOCK) R ON R.id = A.recomendacao_id
	            JOIN Gestao.Colaborador (NOLOCK) C ON C.cs = R.cs_colaborador
	            JOIN Gestao.Colaborador (NOLOCK) CA ON CA.cs = A.cs_colaborador_avaliador
	            JOIN Gestao.Cargo (NOLOCK) GCA ON GCA.cd_cargo = CA.cd_cargo
	            JOIN Gestao.TipoRecomendacao (NOLOCK) TR ON TR.id = R.tipo_recomendacao_id
	            JOIN Gestao.SituacaoAvaliacao (NOLOCK) SA ON SA.id = A.situacao_avaliacao_id
               WHERE ISNULL(A.ativo,1) = 1
                 AND A.recomendacao_id = @recomendacao_id
            GROUP BY A.[id]
                    ,A.[sequencial]
		            ,CONVERT(VARCHAR(255), A.recomendacao_id)
		            ,A.cs_colaborador_avaliador
		            ,(CA.cs + ' - ' + CA.nome)
		            ,CA.email
		            ,GCA.nomenclatura
		            ,SA.nome
                    ,CONVERT(VARCHAR(255), A.situacao_avaliacao_id)
                    ,(CASE WHEN A.situacao_avaliacao_id <> 'C402396C-4938-4FAB-9CDF-6B94BE5C5802' THEN CONVERT(VARCHAR(10), A.data_avaliacao, 103) ELSE '' END)
		            ,C.cs
		            ,C.nome
		            ,TR.nome
		            ,R.qtde_pontos;
        ";
    }
}
