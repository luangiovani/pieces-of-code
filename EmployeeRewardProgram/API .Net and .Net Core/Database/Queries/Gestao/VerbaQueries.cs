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
    /// Implementação da Interface de Queries para operações de banco de dados da tabela Verba
    /// </summary>
    public class VerbaQueries : IQueries
    {
        /// <summary>
        /// Comando de inserção de registro no banco de dados
        /// </summary>
        public string INSERT => @"
            DECLARE @NID VARCHAR(128) = NEWID();

            INSERT INTO [Gestao].[Verba]
                       ([id]
                       ,[cs_colaborador]
                       ,[valor_pontos]
                       ,[data_atribuicao]
                       ,[observacao]
                       ,[valor_moeda]
                       ,[taxa_conversao]
                       ,[ativo]
                       ,[data_hora_criacao]
                       ,[cs_colaborador_criacao]
                       ,[data_hora_alteracao]
                       ,[cs_colaborador_alteracao])
                 VALUES
                       (@NID
                       ,@cs_colaborador
                       ,@valor_pontos
                       ,@data_atribuicao
                       ,@observacao
                       ,@valor_moeda
                       ,@taxa_conversao
                       ,@ativo
                       ,@data_hora_criacao
                       ,@cs_colaborador_criacao
                       ,@data_hora_alteracao
                       ,@cs_colaborador_alteracao);

            INSERT INTO Gestao.ColaboradorGestorSaldoVerba
            (id, cs_colaborador_gestor, quantidade_pontos, data_hora_criacao, cs_colaborador_criacao)
            VALUES
            (NEWID(), @cs_colaborador, @valor_pontos, @data_hora_criacao, @cs_colaborador_criacao);

            SELECT * FROM [Gestao].[Verba](NOLOCK) WHERE id = @NID;";

        /// <summary>
        /// Comando de atualização do registro no banco de dados
        /// </summary>
        public string UPDATE => @"
                UPDATE [Gestao].[Verba]
                   SET [cs_colaborador] = @cs_colaborador
                      ,[valor_pontos] = @valor_pontos
                      ,[data_atribuicao] = @data_atribuicao
                      ,[observacao] = @observacao
                      ,[valor_moeda] = @valor_moeda
                      ,[taxa_conversao] = @taxa_conversao
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
                UPDATE [Gestao].[Verba]
                   SET [ativo] = 0,
                       [cs_colaborador_alteracao] = @cs_colaborador_alteracao,
                       [data_hora_alteracao] = GETDATE()
                 WHERE id = @id";

        /// <summary>
        /// Comando de deleção (atualização para ativo = 0) do registro no banco de dados
        /// </summary>
        public string ACTIVATE => @"
            UPDATE [Gestao].[Verba]
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
                      ,[valor_pontos]
                      ,[data_atribuicao]
                      ,[observacao]
                      ,[valor_moeda]
                      ,[taxa_conversao]
                      ,[ativo]
                      ,[data_hora_criacao]
                      ,[cs_colaborador_criacao]
                      ,[data_hora_alteracao]
                      ,[cs_colaborador_alteracao]
                  FROM [Gestao].[Verba](NOLOCK)";

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
        /// Seleciona todas as atribuições de verbas que foram realizadas
        /// </summary>
        public string SELECT_ATRIBUICOES_REALIZADAS => @"
               SELECT ISNULL(ColabSup.nome, 'Admin') gestor,
  	                  SUM(VB.valor_pontos) total_atribuido
                 FROM Gestao.Verba (NOLOCK) VB
                 JOIN Gestao.Colaborador (NOLOCK) Colab ON Colab.cs = VB.cs_colaborador
            LEFT JOIN Gestao.Colaborador (NOLOCK) ColabSup ON ColabSup.cs = Colab.cs_superior_imediato
                WHERE VB.ativo = 1
             GROUP BY ISNULL(ColabSup.nome, 'Admin');";

        /// <summary>
        /// Seleciona todas as atribuições de verbas que foram recebidas
        /// </summary>
        public string SELECT_ATRIBUICOES_RECEBIDAS => @"
               SELECT ISNULL(ColabSup.nome, 'Admin') gestor,
		              Colab.nome colaborador,
  	                  SUM(VB.valor_pontos) total_atribuido
                 FROM Gestao.Verba (NOLOCK) VB
                 JOIN Gestao.Colaborador (NOLOCK) Colab ON Colab.cs = VB.cs_colaborador
            LEFT JOIN Gestao.Colaborador (NOLOCK) ColabSup ON ColabSup.cs = Colab.cs_superior_imediato
                WHERE VB.ativo = 1
             GROUP BY ISNULL(ColabSup.nome, 'Admin'), Colab.nome;";

        /// <summary>
        /// Listar as Verbas atribuídas para o Colaborador Gestor
        /// </summary>
        public string SELECT_LISTAR => @"
               SELECT V.[id]
                     ,V.[sequencial]
                     ,V.[cs_colaborador]
                     ,V.[valor_pontos]
                     ,CONVERT(VARCHAR(10), V.[data_atribuicao], 103) data
                     ,V.[observacao]
                     ,V.[valor_moeda]
                     ,V.[taxa_conversao]
                     ,V.[ativo]
                     ,V.[data_hora_criacao]
                     ,V.[cs_colaborador_criacao]
                     ,V.[data_hora_alteracao]
                     ,V.[cs_colaborador_alteracao],
		             C.nome gestor
                FROM [Gestao].[Verba] V (NOLOCK)
                JOIN [Gestao].[Colaborador] C (NOLOCK) ON C.cs = V.cs_colaborador
               WHERE ISNULL(V.ativo,1) = 1
            ORDER BY c.nome
        ";

        /// <summary>
        /// Saldo de Verba do Gestor
        /// </summary>
        public string SELECT_SALDO_VERBA_GESTOR => @"
            SELECT SV.* 
              FROM Gestao.ColaboradorGestorSaldoVerba (NOLOCK) SV
             WHERE SV.cs_colaborador_gestor = @cs_gestor;";


        public string UPDATE_SALDO_VERBA_GESTOR => @"
            UPDATE Gestao.ColaboradorGestorSaldoVerba
               SET quantidade_pontos = @quantidade_pontos
             WHERE id = @id;";
    }
}
