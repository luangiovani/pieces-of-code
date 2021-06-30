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
    /// Implementação da Interface de Queries para operações de banco de dados da tabela Recomendacao
    /// </summary>
    public class RecomendacaoQueries : IQueries
    {
        /// <summary>
        /// Comando de inserção de registro no banco de dados
        /// </summary>
        public string INSERT => @"
            DECLARE @NID VARCHAR(128) = NEWID();

            INSERT INTO [Gestao].[Recomendacao]
                       ([id]
                       ,[cs_colaborador]
                       ,[cs_colaborador_solicitante]
                       ,[subordinado]
                       ,[tipo_recomendacao_id]
                       ,[justificativa]
                       ,[qtde_pontos]
                       ,[situacao_recomendacao_id]
                       ,[ativo]
                       ,[data_hora_criacao]
                       ,[cs_colaborador_criacao]
                       ,[data_hora_alteracao]
                       ,[cs_colaborador_alteracao])
                 VALUES
                       (@NID
                       ,@cs_colaborador
                       ,@cs_colaborador_solicitante
                       ,@subordinado
                       ,@tipo_recomendacao_id
                       ,@justificativa
                       ,@qtde_pontos
                       ,@situacao_recomendacao_id
                       ,@ativo
                       ,@data_hora_criacao
                       ,@cs_colaborador_criacao
                       ,@data_hora_alteracao
                       ,@cs_colaborador_alteracao);

            SELECT [id]
                   ,[sequencial]
                   ,[cs_colaborador]
                   ,[cs_colaborador_solicitante]
                   ,[subordinado]
                   ,CONVERT(VARCHAR(255), [tipo_recomendacao_id]) tipo_recomendacao_id
                   ,[justificativa]
                   ,[qtde_pontos]
                   ,CONVERT(VARCHAR(255), [situacao_recomendacao_id]) situacao_recomendacao_id
                   ,[ativo]
                   ,[data_hora_criacao]
                   ,[cs_colaborador_criacao]
                   ,[data_hora_alteracao]
                   ,[cs_colaborador_alteracao] 
              FROM [Gestao].[Recomendacao](NOLOCK) WHERE id = @NID;";

        /// <summary>
        /// Comando de atualização do registro no banco de dados
        /// </summary>
        public string UPDATE => @"
            UPDATE [Gestao].[Recomendacao]
               SET [tipo_recomendacao_id] = @tipo_recomendacao_id
                   ,[cs_colaborador] = @cs_colaborador
                   ,[cs_colaborador_solicitante] = @cs_colaborador_solicitante
                   ,[subordinado] = @subordinado
                   ,[justificativa] = @justificativa
                   ,[qtde_pontos] = @qtde_pontos
                   ,[situacao_recomendacao_id] = @situacao_recomendacao_id
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
            UPDATE [Gestao].[Recomendacao]
               SET [ativo] = 0,
                   [cs_colaborador_alteracao] = @cs_colaborador_alteracao,
                   [data_hora_alteracao] = GETDATE()
             WHERE id = @id";

        /// <summary>
        /// Comando de deleção (atualização para ativo = 0) do registro no banco de dados
        /// </summary>
        public string ACTIVATE => @"
            UPDATE [Gestao].[Recomendacao]
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
                    ,[cs_colaborador]
                    ,[cs_colaborador_solicitante]
                    ,[subordinado]
                    ,CONVERT(VARCHAR(255), [tipo_recomendacao_id]) tipo_recomendacao_id
                    ,[justificativa]
                    ,[qtde_pontos]
                    ,CONVERT(VARCHAR(255), [situacao_recomendacao_id]) situacao_recomendacao_id
                    ,[ativo]
                    ,[data_hora_criacao]
                    ,[cs_colaborador_criacao]
                    ,[data_hora_alteracao]
                    ,[cs_colaborador_alteracao]
               FROM [Gestao].[Recomendacao](NOLOCK)";

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
        /// Selecionar as Posições de Recomendações
        /// </summary>
        public string SELECT_POSICAO_RECOMENDACOES_APROVADAS => @"
            SELECT Rec.id,
	               Rec.sequencial,
                   Colab.cs cs_colaborador,
	               Colab.nome colaborador,
                   ISNULL(GestorColab.nome,'') gestor,
	               SRec.nome status,
	               TRec.nome motivo,
                   Rec.qtde_pontos
              FROM [Gestao].[Recomendacao] (NOLOCK) Rec
              JOIN [Gestao].[TipoRecomendacao] (NOLOCK) TRec ON TRec.id = Rec.tipo_recomendacao_id
              JOIN [Gestao].[SituacaoRecomendacao] (NOLOCK) SRec ON SRec.id = Rec.situacao_recomendacao_id
              JOIN [Gestao].[Colaborador] (NOLOCK) Colab ON Colab.cs = Rec.cs_colaborador
         LEFT JOIN [Gestao].[Colaborador] (NOLOCK) GestorColab ON GestorColab.cs = ISNULL(Colab.cs_superior_imediato,'')
             WHERE Colab.cs = @cs
               AND Rec.ativo = 1
               AND Rec.situacao_recomendacao_id = '5468FA72-FCA6-4A92-8BAC-BFC9E971BEB6'
          GROUP BY Rec.id,
		           Rec.sequencial,
                   Colab.cs,
		           Colab.nome,
                   ISNULL(GestorColab.nome,''),
		           SRec.nome,
		           TRec.nome,
                   Rec.qtde_pontos ";

        /// <summary>
        /// Selecionar as Posições de Recomendações
        /// </summary>
        public string SELECT_POSICAO_RECOMENDACOES_GESTOR => @"
            SELECT Rec.id,
	               Rec.sequencial,
                   Colab.cs cs_colaborador,
	               Colab.nome colaborador,
                   ISNULL(GestorColab.nome,'') gestor,
	               SRec.nome status,
	               TRec.nome motivo,
                   Rec.qtde_pontos
              FROM [Gestao].[Recomendacao] (NOLOCK) Rec
              JOIN [Gestao].[TipoRecomendacao] (NOLOCK) TRec ON TRec.id = Rec.tipo_recomendacao_id
              JOIN [Gestao].[SituacaoRecomendacao] (NOLOCK) SRec ON SRec.id = Rec.situacao_recomendacao_id
              JOIN [Gestao].[Colaborador] (NOLOCK) Colab ON Colab.cs = Rec.cs_colaborador
         LEFT JOIN [Gestao].[Colaborador] (NOLOCK) GestorColab ON GestorColab.cs = ISNULL(Colab.cs_superior_imediato,'')
             WHERE (Rec.cs_colaborador_solicitante = @cs_solicitante OR Colab.cs_superior_imediato = @cs_solicitante)
               AND Rec.ativo = 1
          GROUP BY Rec.id,
		           Rec.sequencial,
                   Colab.cs,
		           Colab.nome,
                   ISNULL(GestorColab.nome,''),
		           SRec.nome,
		           TRec.nome,
                   Rec.qtde_pontos ";

        /// <summary>
        /// Selecionar os detalhes das Recomendações
        /// </summary>
        public string SELECT_DETALHE_RECOMENDACAO => @"
            SELECT Rec.id,
                   Rec.sequencial,
	               Colab.nome colaborador,
	               Colab.cs cs_colaborador,
	               Gestor.nome gestor_colaborador,
	               Gestor.cs cs_gestor_colaborador,
                   GestorS.nome gestor_solicitante,
	               GestorS.cs cs_gestor_solicitante,
	               SRec.nome status,
	               TipoRec.nome tipo_recomendacao,
	               Rec.qtde_pontos,
	               Rec.justificativa
              FROM [Gestao].[Recomendacao] (NOLOCK) Rec
              JOIN [Gestao].[SituacaoRecomendacao] (NOLOCK) SRec ON SRec.id = Rec.situacao_recomendacao_id
              JOIN [Gestao].[TipoRecomendacao] (NOLOCK) TipoRec ON TipoRec.id = Rec.tipo_recomendacao_id
              JOIN [Gestao].[Colaborador] (NOLOCK) Colab ON Colab.cs = Rec.cs_colaborador
              JOIN [Gestao].[Colaborador] (NOLOCK) Gestor ON Gestor.cs = Colab.cs_superior_imediato
              JOIN [Gestao].[Colaborador] (NOLOCK) GestorS ON GestorS.cs = Rec.cs_colaborador_solicitante
             WHERE Rec.id = @id";

        /// <summary>
        /// Selecionar os detalhes das Pontuações
        /// </summary>
        public string SELECT_DETALHE_PONTUACAO => @"
            SELECT Rec.id,
			       Colab.nome colaborador,
                   Colab.cs cs_colaborador,
                   Gestor.nome gestor_colaborador,
                   Gestor.cs cs_gestor_colaborador,
			       SRec.nome status,
                   TipoRec.nome tipo_recomendacao,
                   Rec.qtde_pontos,
                   Rec.justificativa
              FROM [Gestao].[Recomendacao] (NOLOCK) Rec
              JOIN [Gestao].[SituacaoRecomendacao] (NOLOCK) SRec ON SRec.id = Rec.situacao_recomendacao_id
              JOIN [Gestao].[TipoRecomendacao] (NOLOCK) TipoRec ON TipoRec.id = Rec.tipo_recomendacao_id
              JOIN [Gestao].[Colaborador] (NOLOCK) Colab ON Colab.cs = Rec.cs_colaborador
              JOIN [Gestao].[Colaborador] (NOLOCK) Gestor ON Gestor.cs = Colab.cs_superior_imediato
             WHERE UPPER(SRec.nome) = 'AUTORIZADA'
               AND (Rec.cs_colaborador_solicitante = @cs_gestor OR Colab.cs_superior_imediato = @cs_gestor)
               AND Rec.cs_colaborador = @cs_colaborador
          ORDER BY Colab.nome
        ";

        /// <summary>
        /// Selecionar os Indicadores
        /// </summary>
        public string SELECT_INDICADORES => @"
            SELECT DATEPART(MONTH, Rec.data_hora_criacao) mes,
		           COUNT(1) quantidade_mes,
		           SUM(CASE WHEN UPPER(SRec.nome) = 'EM ANALISE' THEN 1 ELSE 0 END) Aguardando,
		           SUM(CASE WHEN UPPER(SRec.nome) = 'APROVADA' THEN 1 ELSE 0 END) Aprovadas
              FROM Gestao.Recomendacao (NOLOCK) Rec
              JOIN [Gestao].[SituacaoRecomendacao] (NOLOCK) SRec ON SRec.id = Rec.situacao_recomendacao_id
             WHERE DATEPART(MONTH, Rec.data_hora_criacao) = DATEPART(MONTH, GETDATE())
          GROUP BY DATEPART(MONTH, Rec.data_hora_criacao)";
    }
}
