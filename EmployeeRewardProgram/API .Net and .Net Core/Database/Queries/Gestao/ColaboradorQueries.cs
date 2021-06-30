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
    /// Implementação da Interface de Queries para operações de banco de dados da tabela Colaborador
    /// </summary>
    public class ColaboradorQueries : IQueries
    {
        /// <summary>
        /// Comando de inserção de registro no banco de dados
        /// </summary>
        public string INSERT => @"";

        /// <summary>
        /// Comando de atualização do registro no banco de dados
        /// </summary>
        public string UPDATE => @"";

        public string UPDATE_PONTOS_COLABORADOR => @"
            UPDATE Gestao.Colaborador
               SET quantidade_pontos = @quantidade_pontos,
                   data_hora_alteracao = GETDATE(),
                   cs_colaborador_alteracao = @cs_colaborador
             WHERE id = @id;
        ";

        /// <summary>
        /// Comando de deleção (atualização para ativo = 0) do registro no banco de dados
        /// </summary>
        public string DELETE => @"";

        /// <summary>
        /// Comando de deleção (atualização para ativo = 0) do registro no banco de dados
        /// </summary>
        public string ACTIVATE => @"";

        /// <summary>
        /// Comando para selecionar registros do banco de dados
        /// </summary>
        public string SELECT => @"
                SELECT C.[id]
                       ,C.[sequencial]
                       ,C.[cs]
                       ,C.[cs_superior_imediato]
                       ,C.[cd_cargo]
                       ,CGO.[nomenclatura] cargo
                       ,CASE WHEN ISNULL(CGO.elegivel,0) = 1 THEN 'Sim' ELSE 'Não' END elegivel
                       ,C.[uo]
                       ,CONVERT(VARCHAR(255), C.[perfil_id]) perfil_id
                       ,ISNULL(P.nome,'') perfil
                       ,C.[nome]
                       ,C.[email]
                       ,C.[data_admissao]
                       ,C.[local_trabalho]
                       ,C.[quantidade_pontos]
                       ,C.[ativo]
                       ,C.[data_hora_criacao]
                       ,C.[cs_colaborador_criacao]
                       ,C.[data_hora_alteracao]
                       ,C.[cs_colaborador_alteracao]
                       ,CONVERT(VARCHAR(255), ColabLoja.loja_id) loja_id
                  FROM [Gestao].[Colaborador] (NOLOCK) C
                  JOIN [Gestao].[Cargo] (NOLOCK) CGO ON CGO.cd_cargo = C.cd_cargo
             LEFT JOIN APP.Perfil (NOLOCK) P ON P.id = C.perfil_id
             LEFT JOIN Gestao.ColaboradorLoja (NOLOCK) ColabLoja ON ColabLoja.cs = C.cs ";

        /// <summary>
        /// Selecionar o registro pelo CS informado no banco de dados
        /// </summary>
        public string SELECT_WHERE_CS =>
            SELECT + @"
                 WHERE C.cs = @cs";

        /// <summary>
        /// Selecionar o registro pelo ID informado no banco de dados
        /// </summary>
        public string SELECT_WHERE_ID =>
            SELECT + @"
                 WHERE C.id = @id";

        /// <summary>
        /// Selecionar somente os registros ativos
        /// </summary>
        public string SELECT_WHERE_ATIVO =>
            SELECT + @"
                 WHERE ISNULL(C.ativo,1) = 1";

        /// <summary>
        /// Selecionar somente os registros inativos
        /// </summary>
        public string SELECT_WHERE_INATIVO =>
            SELECT + @"
                 WHERE ISNULL(C.ativo,1) = 0";

        /// <summary>
        /// Listar colaboradores subordinados ao Gestor
        /// </summary>
        public string LISTAR_POR_GESTOR => @"
              SELECT Colab.cs,
                     Colab.cs_superior_imediato,
                     Colab.nome,
                     Cargo.nomenclatura cargo,
                     CASE WHEN ISNULL(Cargo.elegivel,0) = 1 THEN 'Sim' ELSE 'Não' END elegivel
                FROM [Gestao].[Colaborador] (NOLOCK) Colab
	            JOIN Gestao.Cargo (NOLOCK) Cargo ON Cargo.cd_cargo = Colab.cd_cargo
               WHERE cs_superior_imediato = CASE WHEN @meuTime = 1 THEN @cs_superior_imediato else cs_superior_imediato END
                 AND cs_superior_imediato <> CASE WHEN @meuTime = 1 THEN 0 else @cs_superior_imediato END
                 AND Colab.cs <> @cs_superior_imediato
            GROUP BY Colab.cs,
                     Colab.cs_superior_imediato,
                     Colab.nome,
                     Cargo.nomenclatura,
                     CASE WHEN ISNULL(Cargo.elegivel,0) = 1 THEN 'Sim' ELSE 'Não' END
            ORDER BY [nome]";

        /// <summary>
        /// Listar colaboradores
        /// </summary>
        public string LISTAR => @"
              SELECT Colab.id,
                     Colab.cs,
                     Colab.cs_superior_imediato,
                     Colab.nome,
                     Cargo.nomenclatura cargo,
                     CASE WHEN ISNULL(Cargo.elegivel,0) = 1 THEN 'Sim' ELSE 'Não' END elegivel,
                     Colab.uo,
                     CONVERT(VARCHAR(255), Colab.perfil_id) perfil_id,
                     ISNULL(P.nome,'') perfil 
                FROM [Gestao].[Colaborador] (NOLOCK) Colab
	            JOIN Gestao.Cargo (NOLOCK) Cargo ON Cargo.cd_cargo = Colab.cd_cargo
           LEFT JOIN APP.Perfil (NOLOCK) P ON P.id = Colab.perfil_id 
            GROUP BY Colab.id,
                     Colab.cs,
                     Colab.cs_superior_imediato,
                     Colab.nome,
                     Cargo.nomenclatura,
                     CASE WHEN ISNULL(Cargo.elegivel,0) = 1 THEN 'Sim' ELSE 'Não' END,
                     Colab.uo,
                     CONVERT(VARCHAR(255), Colab.perfil_id),
                     ISNULL(P.nome,'')
            ORDER BY [nome]";

        /// <summary>
        /// Listar colaboradores subordinados ao Gestor
        /// </summary>
        public string LISTAR_COM_TROCAS_POR_GESTOR => @"
              SELECT Colab.cs,
                     Colab.cs_superior_imediato,
                     Colab.nome,
                     Cargo.nomenclatura cargo,
                     CASE WHEN ISNULL(Cargo.elegivel,0) = 1 THEN 'Sim' ELSE 'Não' END elegivel
                FROM [Gestao].[Colaborador] (NOLOCK) Colab
	            JOIN Gestao.Cargo (NOLOCK) Cargo ON Cargo.cd_cargo = Colab.cd_cargo
	            JOIN Venda.Compra (NOLOCK) CO ON CO.cs_colaborador = Colab.cs
               WHERE cs_superior_imediato = CASE WHEN @meuTime = 1 THEN @cs_superior_imediato else cs_superior_imediato END
                 AND cs_superior_imediato <> CASE WHEN @meuTime = 1 THEN 0 else @cs_superior_imediato END
                 AND Colab.cs <> @cs_superior_imediato
                 AND ISNULL(CO.ativo,1) = 1
            GROUP BY Colab.cs,
                     Colab.cs_superior_imediato,
                     Colab.nome,
                     Cargo.nomenclatura,
                     CASE WHEN ISNULL(Cargo.elegivel,0) = 1 THEN 'Sim' ELSE 'Não' END
            ORDER BY [nome]";

        /// <summary>
        /// Relatório de Pontuação, Trocas e Acumulo de TKU'S
        /// </summary>
        public string RELATORIO_PONTUACAO_COLABORADOR => @"
                 SELECT Rec.sequencial,
                        CONVERT(VARCHAR(10), Rec.data_hora_criacao, 103) data,
                        Colab.nome colaborador,
     	                Colab.cs cs_colaborador,
     	                Gestor.nome gestor_colaborador,
     	                Gestor.cs cs_gestor_colaborador,
     	                TipoRec.nome tipo_recomendacao,
     	                Rec.qtde_pontos,
     	                ISNULL(Trocas.Qtde,0) Trocas
                   FROM [Gestao].[Recomendacao] (NOLOCK) Rec
                   JOIN [Gestao].[TipoRecomendacao] (NOLOCK) TipoRec ON TipoRec.id = Rec.tipo_recomendacao_id
                   JOIN [Gestao].[Colaborador] (NOLOCK) Colab ON Colab.cs = Rec.cs_colaborador
                   JOIN [Gestao].[Colaborador] (NOLOCK) Gestor ON Gestor.cs = Colab.cs_superior_imediato
            OUTER APPLY( SELECT COUNT(DISTINCT CO.id) Qtde
                           FROM Venda.Compra (NOLOCK) CO
	                       JOIN Venda.SituacaoCompra (NOLOCK) SC ON SC.id = CO.situacao_compra_id
	                        AND SC.ativo = 1
	                        AND SC.descricao = 'Finalizada'
	                      WHERE CO.cs_colaborador = Colab.cs
	                        AND CO.total_pontos > 0
	                        AND ISNULL(CO.ativo,1) = 1) Trocas
            -- Situação de Recomendação = Aprovada
               WHERE Rec.situacao_recomendacao_id = '5468FA72-FCA6-4A92-8BAC-BFC9E971BEB6'
                 AND (Rec.cs_colaborador_solicitante = @cs_gestor OR Colab.cs_superior_imediato = @cs_gestor)
                 AND Colab.cs <> @cs_gestor
                 AND ISNULL(Rec.ativo,1) = 1
            ORDER BY Colab.nome";

        /// <summary>
        /// Selecionar o Extrato das Recomendações do Colaborador
        /// </summary>
        public string SELECT_EXTRATO_COLABORADOR => @"
	             SELECT C.id,
                        C.cs,
                        C.nome,
                        ISNULL(TKUS_U.tkusUtilizados,0) + ISNULL(C.quantidade_pontos,0) tkusRecebidos,
                        ISNULL(TKUS_U.tkusUtilizados,0) tkusUtilizados,
                        ((ISNULL(TKUS_U.tkusUtilizados,0) + ISNULL(C.quantidade_pontos,0))-(ISNULL(TKUS_U.tkusUtilizados,0))) tkusDisponiveis,
                        0 tkusPendentes,
                        ISNULL(RQS.qtde,0) recs,
                        ISNULL(EXPIR.Expirados,0) Expirados,
                        ISNULL(EXPIR.AExpirar,0) AExpirar
                   FROM Gestao.Colaborador(NOLOCK) C
            OUTER APPLY(SELECT COUNT(id) qtde
			              FROM Gestao.Recomendacao (NOLOCK) RQ
			             WHERE RQ.cs_colaborador = C.cs
			               AND RQ.ativo = 1
			               AND RQ.situacao_recomendacao_id = '5468FA72-FCA6-4A92-8BAC-BFC9E971BEB6'
			            )RQS
            OUTER APPLY(SELECT SUM(CO.total_pontos) tkusUtilizados
                          FROM Venda.Compra(NOLOCK) CO
                          JOIN Venda.SituacaoCompra(NOLOCK) SC ON SC.id = CO.situacao_compra_id
                         WHERE CO.ativo = 1
                           AND SC.id IN('6196698E-95D7-48CF-B07A-CEC3BDC15D18',
							            'EBBC16C6-DC45-48DC-ACE7-1C31D7A8D909',
							            '72C108D6-E972-437E-82A7-FE83CD54DD0A',
							            '7D4E5C01-FF0D-433A-9A72-1BCD68612A5E')
                           AND CO.cs_colaborador = C.cs)TKUS_U
            OUTER APPLY(SELECT SUM(CASE WHEN EXC.data_expiracao < GETDATE() THEN R.qtde_pontos ELSE 0 END) Expirados,
				               SUM(CASE WHEN EXC.data_expiracao >= GETDATE() THEN R.qtde_pontos ELSE 0 END) AExpirar,
				               R.id
			              FROM Gestao.ExpiracaoPontosColaborador (NOLOCK) EXC
			              JOIN Gestao.Recomendacao (NOLOCK) R ON R.id = EXC.recomendacao_id
			               AND R.situacao_recomendacao_id = '5468FA72-FCA6-4A92-8BAC-BFC9E971BEB6'
			             WHERE R.cs_colaborador = C.cs
			               AND R.ativo = 1
			               AND EXC.ativo = 1
			               AND DATEDIFF(DAY, GETDATE(), EXC.data_expiracao) <= 30
		              GROUP BY R.id)EXPIR
                   WHERE C.cs = @cs_colaborador";

        /// <summary>
        /// Selecionar o Extrato das Recomendações do Colaborador
        /// </summary>
        public string SELECT_EXTRATO_COLABORADORES_GESTOR => @"
	             SELECT C.id,
		                C.cs,
		                C.nome,
		                ISNULL(C.quantidade_pontos,0) tkusRecebidos,
			            SUM(ISNULL(TKUS_U.tkusUtilizados,0)) tkusUtilizados,
			            (ISNULL(C.quantidade_pontos,0)-SUM(ISNULL(TKUS_U.tkusUtilizados,0))) tkusDisponiveis
	               FROM Gestao.Colaborador(NOLOCK) C
            OUTER APPLY(	SELECT CO.total_pontos tkusUtilizados
				              FROM Venda.Compra(NOLOCK) CO
				              JOIN Venda.SituacaoCompra(NOLOCK) SC ON SC.id = CO.situacao_compra_id
				             WHERE CO.ativo = 1
				               AND SC.sequencial IN(1,2,3,4)
				               AND CO.cs_colaborador = C.cs)TKUS_U
                 WHERE C.cs_superior_imediato = @cs_gestor
                   AND C.cs <> @cs_gestor
              GROUP BY C.id,
		               C.cs,
		               C.nome,
		               ISNULL(C.quantidade_pontos,0)";

        /// <summary>
        /// Selecionar as Trocas do Colaborador
        /// </summary>
        public string SELECT_TROCAS_COLABORADOR => @"
                SELECT IC.compra_id,
		               IC.nome,
		               IC.valor_monetario valor,
		               IC.valor_pontos pontos,
		               CONVERT(VARCHAR(10), CO.data_hora_criacao, 103) data
                  FROM [Venda].ItemCompra (NOLOCK) IC
	              JOIN [Venda].[Compra](NOLOCK) CO ON CO.id = IC.compra_id
	              JOIN Venda.SituacaoCompra(NOLOCK) SC ON SC.id = CO.situacao_compra_id
	             WHERE CO.ativo = 1
	               AND SC.sequencial IN(1,2,3,4)
	               AND CO.cs_colaborador = @cs_colaborador
              GROUP BY IC.compra_id,
		               IC.nome,
		               IC.valor_monetario,
		               IC.valor_pontos,
		               CONVERT(VARCHAR(10), CO.data_hora_criacao, 103);";

        /// <summary>
        /// Selecionar as Recomendações do Colaborador
        /// </summary>
        public string SELECT_RECOMENDACOES_COLABORADOR => @"
              SELECT Rec.id,
                     TipoRec.nome tipo_recomendacao,
                     Rec.qtde_pontos pontos,
	                 CONVERT(VARCHAR(10), Rec.data_hora_criacao, 103) data,
                     Gestor.cs cs_gestor_solicitante,
                     Gestor.nome gestor_solicitante
                FROM [Gestao].[Recomendacao] (NOLOCK) Rec
                JOIN [Gestao].[TipoRecomendacao] (NOLOCK) TipoRec ON TipoRec.id = Rec.tipo_recomendacao_id
                JOIN [Gestao].[Colaborador] (NOLOCK) Colab ON Colab.cs = Rec.cs_colaborador
	            JOIN Gestao.SituacaoRecomendacao (NOLOCK) SR ON SR.id = Rec.situacao_recomendacao_id
                JOIN [Gestao].[Colaborador] (NOLOCK) Gestor ON Gestor.cs = Rec.cs_colaborador_solicitante
               WHERE Rec.cs_colaborador = @cs_colaborador
                 AND SR.id = '5468FA72-FCA6-4A92-8BAC-BFC9E971BEB6'
                 AND ISNULL(Rec.ativo,1) = 1
            ORDER BY Rec.qtde_pontos;";

        /// <summary>
        /// Validar informações de acesso do Colaborador
        /// </summary>
        public string SELECT_LOGIN => @"
              SELECT C.id,
	                 C.cs, 
                     C.nome,
	                 P.nome Perfil, 
                     CONVERT(VARCHAR(255), P.id) perfilId,
		             c.email
                FROM Gestao.Colaborador (NOLOCK) C
                JOIN APP.Perfil (NOLOCK) P ON P.id = C.perfil_id
               WHERE C.cs = @cs
	             AND P.ativo = 1
	             AND C.ativo = 1;";

        /// <summary>
        /// Obtém Menus do Colaborador
        /// </summary>
        public string SELECT_MENUS_USUARIOS => @"
              SELECT M.id menu_id,
		             M.menu_superior_id,
	                 M.nome menu_opcao,
                     M.icone,
		             M.controller,
		             M.acao,
		             M.ordem,
		             PMN.visualizar,
		             PMN.cadastrar,
		             PMN.excluir
                FROM APP.PerfilMenuDeNavegacao (NOLOCK) PMN
                JOIN APP.Perfil (NOLOCK) P ON P.id = PMN.perfil_id
                JOIN APP.MenuDeNavegacao (NOLOCK) M ON M.id = PMN.menu_navegacao_id
                JOIN Gestao.Colaborador (NOLOCK) C ON C.perfil_id = P.id
               WHERE C.cs = @cs
                 AND PMN.ativo = 1
	             AND P.ativo = 1
	             AND M.ativo = 1
	             AND C.ativo = 1
	             AND M.aplicacao_id = @aplicacao_id
            GROUP BY PMN.perfil_id,
					 M.id,
		             M.menu_superior_id,
	                 M.nome,
                     M.icone,
		             M.controller,
		             M.acao,
		             M.sequencial,
		             M.ordem,
		             PMN.visualizar,
		             PMN.cadastrar,
		             PMN.excluir
            ORDER BY PMN.perfil_id,
					 M.menu_superior_id,
                     M.ordem;";

        /// <summary>
        /// Querie para atualizar o Perfil do Colaborador
        /// </summary>
        public string UPDATE_PERFIL_COLABORADOR => @"
            UPDATE Gestao.Colaborador
               SET perfil_id = @perfil_id,
                   data_hora_alteracao = GETDATE(),
                   cs_colaborador_alteracao = @cs_colaborador
             WHERE id = @id;
        ";

        public string SELECT_GESTORES => @"
                SELECT Colab.cs,
                       Colab.nome
                  FROM [Gestao].[Colaborador] (NOLOCK) Colab
             LEFT JOIN APP.Perfil (NOLOCK) P ON P.id = Colab.perfil_id 
                 WHERE P.id IN('40A7DAF7-B890-4A2F-9EE6-51595F6E6B85','A358766F-28E2-4790-87D9-CD1DDFA91493')
                   AND P.ativo = 1
                   AND Colab.ativo = 1
              GROUP BY Colab.cs,
                       Colab.nome
              ORDER BY nome";

        public string SELECT_GESTORES_AVALIADORES => @"
              SELECT cs_gestor, 
                     cargo,
                     email,
		             nome,
                     nivel
                FROM Gestao.VW_AVALIADORES_COLABORADOR (NOLOCK)
               WHERE CS = @cs_colaborador
            ORDER BY nivel;";

        public string SELECT_COMPRAS_COLABORADOR_GESTOR => @"
            SELECT CO.id,
	               CO.sequencial,
	               C.cs,
	               c.nome,
	               CO.total_pontos pontos,
	               CO.total_valor valor
              FROM Venda.Compra (NOLOCK) CO
              JOIN Gestao.Colaborador (NOLOCK) C ON C.cs = CO.cs_colaborador
             WHERE C.cs_superior_imediato = @cs_gestor
               AND C.cs <> @cs_gestor
               AND ISNULL(CO.ativo,1) = 1
        ";

        public string SELECT_COMPRAS_COLABORADOR => @"
            SELECT CO.id,
                   CO.sequencial,
                   C.cs,
                   c.nome,
	               CONVERT(VARCHAR(10), CO.data_hora_criacao, 103) data,
                   CO.total_pontos pontos,
                   CO.total_valor valor,
	               SC.descricao situacao,
                   CONVERT(VARCHAR(255), SC.id) situacao_id,
                   ISNULL(CO.informacoes_complementares,'') informacoes_complementares,
	               STUFF((SELECT ',<br/># ' + CONVERT(VARCHAR, IC.sequencial) + ' Nome.: ' + nome + ' pontos.: ' + CONVERT(VARCHAR, valor_pontos)
			                FROM Venda.ItemCompra (NOLOCK) IC 
			               WHERE IC.compra_id = CO.id FOR XML PATH('')), 1, 1,'') produtos
              FROM Venda.Compra (NOLOCK) CO
              JOIN Venda.SituacaoCompra (NOLOCK) SC ON SC.id = CO.situacao_compra_id
              JOIN Gestao.Colaborador (NOLOCK) C ON C.cs = CO.cs_colaborador
             WHERE C.cs = @cs
               AND ISNULL(CO.ativo,1) = 1
        ";

        public string SELECT_ITEMS_COMPRAS_COLABORADOR => @"
             SELECT IC.id,
		            IC.sequencial,
		            CONVERT(VARCHAR(255), IC.compra_id) compra_id,
		            IC.nome,
		            IC.descricao,
		            IC.valor_pontos,
		            IC.valor_monetario,
		            IC.observacao
               FROM Venda.ItemCompra (NOLOCK) IC 
               JOIN Venda.Compra (NOLOCK) CO ON CO.id = IC.compra_id
              WHERE IC.compra_id = @compra_id
                AND ISNULL(CO.ativo,1) = 1
           ORDER BY IC.sequencial";

        public string SELECT_COLABORADOR_LOJA => @"
                SELECT C.[id]
                      ,C.[sequencial]
                      ,C.[cs]
                      ,C.[cs_superior_imediato]
                      ,C.[cd_cargo]
                      ,CGO.[nomenclatura] cargo
                      ,CASE WHEN ISNULL(CGO.elegivel,0) = 1 THEN 'Sim' ELSE 'Não' END elegivel
                      ,C.[uo]
                      ,CONVERT(VARCHAR(255), C.[perfil_id]) perfil_id
                      ,ISNULL(P.nome,'') perfil
                      ,C.[nome]
                      ,ISNULL(C.[email], ColabLoja.email) email
                      ,C.[data_admissao]
                      ,C.[local_trabalho]
                      ,C.[quantidade_pontos]
                      ,C.[ativo]
                      ,C.[data_hora_criacao]
                      ,C.[cs_colaborador_criacao]
                      ,C.[data_hora_alteracao]
                      ,C.[cs_colaborador_alteracao]
                      ,CONVERT(VARCHAR(255), ColabLoja.loja_id) loja_id
                 FROM [Gestao].[Colaborador] (NOLOCK) C
                 JOIN [Gestao].[Cargo] (NOLOCK) CGO ON CGO.cd_cargo = C.cd_cargo
            LEFT JOIN APP.Perfil (NOLOCK) P ON P.id = C.perfil_id
                 JOIN Gestao.ColaboradorLoja (NOLOCK) ColabLoja ON ColabLoja.cs = C.cs
                --PERFIL LOJA
                WHERE C.perfil_id = 'ADF3ECB0-AF51-41F2-9DEF-5C29D9EC4424'
                  AND C.cs = CASE WHEN ISNULL(@cs,'') = '' THEN C.cs ELSE @cs END
                  AND ColabLoja.loja_id = CASE WHEN ISNULL(@loja_id,'') = '' THEN ColabLoja.loja_id ELSE @loja_id END
            GROUP BY C.[id]
                     ,C.[sequencial]
                     ,C.[cs]
                     ,C.[cs_superior_imediato]
                     ,C.[cd_cargo]
                     ,CGO.[nomenclatura]
                     ,CASE WHEN ISNULL(CGO.elegivel,0) = 1 THEN 'Sim' ELSE 'Não' END
                     ,C.[uo]
                     ,CONVERT(VARCHAR(255), C.[perfil_id])
                     ,ISNULL(P.nome,'')
                     ,C.[nome]
                     ,ISNULL(C.[email], ColabLoja.email)
                     ,C.[data_admissao]
                     ,C.[local_trabalho]
                     ,C.[quantidade_pontos]
                     ,C.[ativo]
                     ,C.[data_hora_criacao]
                     ,C.[cs_colaborador_criacao]
                     ,C.[data_hora_alteracao]
                     ,C.[cs_colaborador_alteracao]
                     ,CONVERT(VARCHAR(255), ColabLoja.loja_id); ";
    }
}
