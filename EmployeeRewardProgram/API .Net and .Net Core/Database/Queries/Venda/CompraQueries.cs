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
    /// Implementação da Interface de Queries para operações de banco de dados da tabela Compra
    /// </summary>
    public class CompraQueries : IQueries
    {
        /// <summary>
        /// Comando de inserção de registro no banco de dados
        /// </summary>
        public string INSERT => @"
            DECLARE @NID VARCHAR(128) = NEWID();

            INSERT INTO [Venda].[Compra]
                       ([id]
                       ,[opcao_entrega_id]
                       ,[situacao_compra_id]
                       ,[meio_de_compra_id]
                       ,[cs_colaborador]
                       ,[cs_colaborador_loja]
                       ,[total_pontos]
                       ,[total_valor]
                       ,[taxa_conversao]
                       ,[loja_id]
                       ,[ativo]
                       ,[data_hora_criacao]
                       ,[cs_colaborador_criacao]
                       ,[data_hora_alteracao]
                       ,[cs_colaborador_alteracao])
                 VALUES
                       (@NID
                       ,@opcao_entrega_id
                       ,@situacao_compra_id
                       ,@meio_de_compra_id                       
                       ,@cs_colaborador
                       ,@cs_colaborador_loja
                       ,@total_pontos
                       ,@total_valor
                       ,@taxa_conversao
                       ,@loja_id
                       ,@ativo
                       ,@data_hora_criacao
                       ,@cs_colaborador_criacao
                       ,@data_hora_alteracao
                       ,@cs_colaborador_alteracao);

                SELECT [id]
                       ,[sequencial]
                       ,CONVERT(VARCHAR(255), [opcao_entrega_id]) opcao_entrega_id
                       ,CONVERT(VARCHAR(255), [situacao_compra_id]) situacao_compra_id
                       ,CONVERT(VARCHAR(255), [meio_de_compra_id]) meio_de_compra_id
                       ,ISNULL([justificativa],'') justificativa
                       ,[cs_colaborador]
                       ,[cs_colaborador_loja]
                       ,[total_pontos]
                       ,[total_valor]
                       ,[taxa_conversao]
                       ,CONVERT(VARCHAR(255), [loja_id]) loja_id
                       ,[ativo]
                       ,[data_hora_criacao]
                       ,[cs_colaborador_criacao]
                       ,[data_hora_alteracao]
                       ,[cs_colaborador_alteracao]
                  FROM [Venda].[Compra](NOLOCK) WHERE id = @NID;";

        /// <summary>
        /// Comando de atualização do registro no banco de dados
        /// </summary>
        public string UPDATE => @"
                UPDATE [Venda].[Compra]
                   SET [opcao_entrega_id] = @opcao_entrega_id
                      ,[situacao_compra_id] = @situacao_compra_id
                      ,[meio_de_compra_id] = @meio_de_compra_id
                      ,[justificativa] = @justificativa
                      ,[informacoes_complementares] = @informacoes_complementares
                      ,[cs_colaborador] = @cs_colaborador
                      ,[cs_colaborador_loja] = @cs_colaborador_loja
                      ,[total_pontos] = @total_pontos
                      ,[total_valor] = @total_valor
                      ,[taxa_conversao] = @taxa_conversao
                      ,[loja_id] = @loja_id
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
                UPDATE [Venda].[Compra]
                   SET [ativo] = 0,
                       [cs_colaborador_alteracao] = @cs_colaborador_alteracao,
                       [data_hora_alteracao] = GETDATE()
                 WHERE id = @id";

        /// <summary>
        /// Comando de deleção (atualização para ativo = 0) do registro no banco de dados
        /// </summary>
        public string ACTIVATE => @"
                UPDATE [Venda].[Compra]
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
                       ,CONVERT(VARCHAR(255), [opcao_entrega_id]) opcao_entrega_id
                       ,CONVERT(VARCHAR(255), [situacao_compra_id]) situacao_compra_id
                       ,CONVERT(VARCHAR(255), [meio_de_compra_id]) meio_de_compra_id
                       ,ISNULL(justificativa,'') justificativa
                       ,ISNULL(informacoes_complementares,'') informacoes_complementares
                       ,[cs_colaborador]
                       ,[cs_colaborador_loja]
                       ,[total_pontos]
                       ,[total_valor]
                       ,[taxa_conversao]
                       ,CONVERT(VARCHAR(255), [loja_id]) loja_id
                       ,[ativo]
                       ,[data_hora_criacao]
                       ,[cs_colaborador_criacao]
                       ,[data_hora_alteracao]
                       ,[cs_colaborador_alteracao]
                  FROM [Venda].[Compra](NOLOCK)";

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
        /// Retorna os Produtos mais trocados pelos colaboradores
        /// </summary>
        public string SELECT_PRODUTOS_MAIS_COMPRADOS => @"
                SELECT IC.nome,
		               IC.valor_monetario valor,
		               IC.valor_pontos pontos,
	                   COUNT(1) trocas
                  FROM Venda.Compra(NOLOCK) CM
                  JOIN Venda.ItemCompra (NOLOCK) IC ON IC.compra_id = CM.id
	              JOIN Venda.SituacaoCompra (NOLOCK) SC ON SC.id = CM.situacao_compra_id
                 WHERE CM.ativo = 1
                   AND SC.sequencial IN(3,4)
              GROUP BY IC.nome,
		               IC.valor_monetario,
		               IC.valor_pontos;";

        /// <summary>
        /// Recupera as Compras(trocas) que estão pendentes ou pendentes de envio
        /// </summary>
        public string SELECT_COMPRAS_PENDENTES => @"
                SELECT CM.id,
	                   CM.sequencial,
		               CB.nome colaborador,
		               CB.cs cs_colaborador,
		               ISNULL(CM.total_pontos,0) pontos,
		               CONVERT(VARCHAR(10), CM.data_hora_criacao, 103) data,
                       SC.descricao situacao
                  FROM Venda.Compra(NOLOCK) CM
	              JOIN Venda.SituacaoCompra (NOLOCK) SC ON SC.id = CM.situacao_compra_id
	              JOIN Gestao.Colaborador (NOLOCK) CB ON CB.cs = CM.cs_colaborador
                 WHERE CM.ativo = 1
                   AND SC.id IN('6196698E-95D7-48CF-B07A-CEC3BDC15D18',
                                'EBBC16C6-DC45-48DC-ACE7-1C31D7A8D909',
                                '72C108D6-E972-437E-82A7-FE83CD54DD0A',
                                '69EA87EC-1D85-4F31-AA98-F3AE33F204FB');";

        /// <summary>
        /// Recupera as Compras(trocas) que estão pendentes de envio apenas
        /// Sequencial Situação de Compra:
        ///     3	Efetivada (Pendente de Envio)
        /// </summary>
        public string SELECT_COMPRAS_PENDENTES_ENVIO => @"
                SELECT CM.id,
		               CM.sequencial,
		               CB.nome colaborador,
		               CB.cs cs_colaborador,
		               ISNULL(CM.total_pontos,0) pontos,
		               CONVERT(VARCHAR(10), CM.data_hora_criacao, 103) data,
                       SC.descricao situacao
                  FROM Venda.Compra(NOLOCK) CM
	              JOIN Venda.SituacaoCompra (NOLOCK) SC ON SC.id = CM.situacao_compra_id
	              JOIN Gestao.Colaborador (NOLOCK) CB ON CB.cs = CM.cs_colaborador
                 WHERE CM.ativo = 1
                   AND SC.sequencial IN(3);";

        /// <summary>
        /// Selecionar as Compras (Trocas de Pontos) Realizadas
        /// </summary>
        public string SELECT_HISTORICO_COMPRAS_REALIZADAS => @"
                SELECT CO.id, 
                       CO.sequencial,
		               CONVERT(VARCHAR(10), CO.data_hora_criacao, 103) Data,
		               CO.total_pontos,
		               CO.total_valor,
		               CO.taxa_conversao,
   	                   CO.cs_colaborador,
                       ISNULL(CO.justificativa,'') justificativa,
   	                   CS.nome colaborador,
   	                   SC.descricao,
   	                   OEC.label_loja
                  FROM Venda.Compra (NOLOCK) CO
                  JOIN Gestao.Colaborador (NOLOCK) CS ON CS.cs = CO.cs_colaborador
                  JOIN Venda.SituacaoCompra (NOLOCK) SC ON SC.id = CO.situacao_compra_id
             LEFT JOIN Venda.OpcaoEntregaCompra (NOLOCK) OEC ON OEC.compra_id = CO.id
                 WHERE CO.ativo = 1
                   AND SC.id NOT IN('6196698E-95D7-48CF-B07A-CEC3BDC15D18',
                                    'EBBC16C6-DC45-48DC-ACE7-1C31D7A8D909',
                                    '72C108D6-E972-437E-82A7-FE83CD54DD0A',
                                    '69EA87EC-1D85-4F31-AA98-F3AE33F204FB');";

        /// <summary>
        /// Selecionar o Detalhe de determinada Compra (Troca de Pontos)
        /// </summary>
        public string SELECT_DETALHE_COMPRA => @"
               SELECT CO.id, 
                      CO.sequencial,
		              CONVERT(VARCHAR(10), CO.data_hora_criacao, 103) Data,
		              CO.total_pontos,
		              CO.total_valor,
		              CO.taxa_conversao,
   	                  CO.cs_colaborador,
                      ISNULL(CO.justificativa,'') justificativa,
                      ISNULL(CO.informacoes_complementares,'') informacoes_complementares,
   	                  CS.nome colaborador,
                      SC.descricao,
   	                  SC.descricao situacao,
                      CONVERT(VARCHAR(255), situacao_compra_id) situacao_compra_id,
   	                  OEC.label_loja,
                      ISNULL(LJ.nome,'') loja,
                      CONVERT(VARCHAR(255), LJ.id) loja_id
                 FROM Venda.Compra (NOLOCK) CO
                 JOIN Gestao.Colaborador (NOLOCK) CS ON CS.cs = CO.cs_colaborador
                 JOIN Venda.SituacaoCompra (NOLOCK) SC ON SC.id = CO.situacao_compra_id
            LEFT JOIN Venda.OpcaoEntregaCompra (NOLOCK) OEC ON OEC.compra_id = CO.id
            LEFT JOIN Gestao.Loja (NOLOCK) LJ ON LJ.id = CO.loja_id
                WHERE CO.id = @compra_id;";

        /// <summary>
        /// Selecionar os Itens da Compra a ser Detalhada
        /// </summary>
        public string SELECT_DETALHE_ITENS_COMPRA => @"
                SELECT IC.id, 
                       IC.sequencial,
	                   IC.nome,
	                   IC.valor_pontos,
	                   IC.valor_monetario,
                       IC.observacao,
	                   PRD.b64_imagem imagem
                  FROM Venda.ItemCompra (NOLOCK) IC
                  JOIN Venda.Compra (NOLOCK) CO ON CO.id = IC.compra_id
                  JOIN Loja.Produto (NOLOCK) PRD ON PRD.id = IC.produto_id
                 WHERE CO.id = @compra_id;";

        /// <summary>
        /// Selecionar os Historico de Compras por Colaboradores
        /// </summary>
        public string SELECT_HISTORICO_COLABORADOR => @"
               SELECT CO.id, 
                      CO.sequencial,
                      CONVERT(VARCHAR(10), CO.data_hora_criacao, 103) Data,
                      CO.total_pontos,
                      CO.total_valor,
                      CO.taxa_conversao,
                      CO.cs_colaborador,
                      ISNULL(CO.justificativa,'') justificativa,
                      ISNULL(CO.informacoes_complementares,'') informacoes_complementares,
                      CS.nome colaborador,
                      SC.descricao,
                      OEC.label_loja,
                      SC.descricao situacao  
                 FROM Venda.Compra (NOLOCK) CO
                 JOIN Gestao.Colaborador (NOLOCK) CS ON CS.cs = CO.cs_colaborador
                 JOIN Venda.SituacaoCompra (NOLOCK) SC ON SC.id = CO.situacao_compra_id
            LEFT JOIN Venda.OpcaoEntregaCompra (NOLOCK) OEC ON OEC.compra_id = CO.id
                WHERE CO.cs_colaborador = @cs_colaborador;";

        public string SELECT_RELATORIO_TROCAS => @"
           SELECT CONVERT(VARCHAR(255), C.id) compra_id,
		          C.sequencial compra,
                  LJ.nome loja,
                  ISNULL(IC.nome,'') produto,
				  ISNULL(IC.descricao,'') produtoDescricao,
	              C.total_pontos,
                  C.total_valor,
		          CONVERT(VARCHAR(10), C.data_hora_criacao, 103) data,
		          SC.descricao situacao_compra,
		          CO.cs,
		          CO.nome colaborador,
		          CONVERT(VARCHAR(255), C.situacao_compra_id) situacao_compra_id,
                  SC.descricao situacao,
		          ISNULL(CAST(BT.pago AS INT), -1) pago,
                  CONVERT(VARCHAR(255), BT.id) faturamento_id
             FROM Venda.Compra (NOLOCK) C
	         JOIN Venda.SituacaoCompra (NOLOCK) SC ON SC.id = C.situacao_compra_id
	         JOIN Gestao.Colaborador (NOLOCK) CO ON CO.cs = C.cs_colaborador
			 JOIN Gestao.Loja (NOLOCK) LJ ON LJ.id = C.loja_id
        LEFT JOIN Venda.ItemCompra(NOLOCK) IC ON IC.compra_id = C.id
        LEFT JOIN Gestao.BalancoTrocas (NOLOCK) BT ON BT.compra_id = C.id
            WHERE C.data_hora_criacao >= @dataDe
	          AND C.data_hora_criacao <= @dataAte";

        public string INSERT_FATURAR_COMPRA => @"
            INSERT INTO Gestao.BalancoTrocas
            (compra_id,
             pago,
             ativo,
             data_hora_criacao,
             cs_colaborador_criacao)
            VALUES
            (@compraId, 
             0, 
             1, 
             GETDATE(), 
             @cs_colaborador_logado);";

        public string UPDATE_PAGAR_COMPRA => @"
            UPDATE Gestao.BalancoTrocas 
               SET Pago = 1,
                   cs_colaborador_alteracao = @cs_colaborador_logado,
                   data_hora_alteracao = GETDATE()
             WHERE id = @id;
        ";
    }
}
