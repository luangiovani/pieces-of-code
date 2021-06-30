namespace Database.Queries.Loja
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
    /// Implementação da Interface de Queries para operações de banco de dados da tabela Produto
    /// </summary>
    public class ProdutoQueries : IQueries
    {
        /// <summary>
        /// Comando de inserção de registro no banco de dados
        /// </summary>
        public string INSERT => @"
            DECLARE @NID VARCHAR(128) = NEWID();

            INSERT INTO [Loja].[Produto]
                       ([id]
                       ,[loja_id]
                       ,[nome]
                       ,[descricao]
                       ,[b64_imagem]
                       ,[disponibilidade]
                       ,[data_disponibilidade]
                       ,[valor_pontos]
                       ,[valor_monetario]
                       ,[observacao]
                       ,[ativo]
                       ,[data_hora_criacao]
                       ,[cs_colaborador_criacao]
                       ,[data_hora_alteracao]
                       ,[cs_colaborador_alteracao])
                 VALUES
                       (@NID
                       ,@loja_id
                       ,@nome
                       ,@descricao
                       ,@b64_imagem
                       ,@disponibilidade
                       ,@data_disponibilidade
                       ,@valor_pontos
                       ,@valor_monetario
                       ,@observacao
                       ,@ativo
                       ,@data_hora_criacao
                       ,@cs_colaborador_criacao
                       ,@data_hora_alteracao
                       ,@cs_colaborador_alteracao);" + 
            SELECT + @" WHERE id = @NID;";

        /// <summary>
        /// Comando de atualização do registro no banco de dados
        /// </summary>
        public string UPDATE => @"
            UPDATE [Loja].[Produto]
               SET [loja_id] = @loja_id
                  ,[nome] = @nome
                  ,[descricao] = @descricao
                  ,[b64_imagem] = @b64_imagem
                  ,[disponibilidade] = @disponibilidade
                  ,[data_disponibilidade] = @data_disponibilidade
                  ,[valor_pontos] = @valor_pontos
                  ,[valor_monetario] = @valor_monetario
                  ,[observacao] = @observacao
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
            UPDATE [Loja].[Produto]
               SET [ativo] = 0,
                   [disponibilidade] = 0,
                   [data_disponibilidade] = NULL,
                   [cs_colaborador_alteracao] = @cs_colaborador_alteracao,
                   [data_hora_alteracao] = GETDATE()
             WHERE id = @id";

        /// <summary>
        /// Comando de deleção (atualização para ativo = 0) do registro no banco de dados
        /// </summary>
        public string ACTIVATE => @"
            UPDATE [Loja].[Produto]
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
                  ,CONVERT(VARCHAR(255), [loja_id]) loja_id
                  ,[nome]
                  ,[descricao]
                  ,[b64_imagem]
                  ,[disponibilidade]
                  ,[data_disponibilidade]
                  ,[valor_pontos]
                  ,[valor_monetario]
                  ,observacao
                  ,[ativo]
                  ,[data_hora_criacao]
                  ,[cs_colaborador_criacao]
                  ,[data_hora_alteracao]
                  ,[cs_colaborador_alteracao]
              FROM [Loja].[Produto](NOLOCK)";

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
        /// Listar os Produtos cadastrados no banco de dados
        /// </summary>
        public string SELECT_LISTAR => @"
                SELECT PRD.[id]
		              ,PRD.[sequencial]
		              ,PRD.[nome]
                      ,PRD.descricao
		              ,PRD.[valor_pontos] pontos
		              ,PRD.[valor_monetario] valor
		              ,COUNT(DISTINCT IC.compra_id) trocas
		              ,CASE WHEN ISNULL(PRD.ativo,1) = 1 THEN 'Ativo' ELSE 'Inativo' END situacao
                      ,PRD.[disponibilidade]
                      ,PRD.[data_disponibilidade]
                      ,ISNULL(PRD.b64_Imagem,'') b64Imagem
                 FROM [Loja].[Produto] (NOLOCK) PRD
            LEFT JOIN [Venda].ItemCompra (NOLOCK) IC ON IC.nome = PRD.nome
             GROUP BY PRD.[id]
		              ,PRD.[sequencial]
		              ,PRD.[nome]
                      ,PRD.descricao
		              ,PRD.[valor_pontos]
		              ,PRD.[valor_monetario]
		              ,CASE WHEN ISNULL(PRD.ativo,1) = 1 THEN 'Ativo' ELSE 'Inativo' END
                      ,PRD.[disponibilidade]
                      ,PRD.[data_disponibilidade]
                      ,ISNULL(PRD.b64_Imagem,'');";

        /// <summary>
        /// Retorna as Opções características disponíveis para os Produtos e seus valores para cada Opção
        /// </summary>
        public string OPCOES_VALORES => @"
               SELECT OP.[id]
		             ,OP.[sequencial]
		             ,OP.[nome]
		             ,OP.[observacao]
		             ,CONVERT(VARCHAR(255), OP.[tipo_opcao_id]) tipo_opcao_id
		             ,OP.[ativo]
		             ,OP.[data_hora_criacao]
		             ,OP.[cs_colaborador_criacao]
		             ,OP.[data_hora_alteracao]
		             ,OP.[cs_colaborador_alteracao]
                     ,CONVERT(VARCHAR(255), VO.[opcao_id]) opcao_id		             
                     ,VO.[id]
		             ,VO.[sequencial]
		             ,VO.[valor]
		             ,VO.[ativo]
		             ,VO.[data_hora_criacao]
		             ,VO.[cs_colaborador_criacao]
		             ,VO.[data_hora_alteracao]
		             ,VO.[cs_colaborador_alteracao]
                FROM Loja.Opcoes (NOLOCK) OP
	            JOIN Loja.ValoresOpcoes (NOLOCK) VO ON VO.opcao_id = OP.id
               WHERE OP.ativo = 1
                 AND VO.ativo = 1
            ORDER BY OP.sequencial,
		             VO.sequencial;";
    }
}
