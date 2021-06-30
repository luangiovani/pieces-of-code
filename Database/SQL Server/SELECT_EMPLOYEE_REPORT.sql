/*
This SQL query was written for use in the Dapper implementation.
The goal:
	* Select main informations of employees (Gestao.Colaborador)
	* Select how much credits (TKUS): total received (tkusRecebidos), used(tkusUtilizados), available (tkusDisponiveis), pending (tkusPendentes - TO DO)
	* Select quantity of rewards (recs)
	* Select how much credits are expired (Expirados) and will expire (AExpirar)
*/				 
				 SELECT C.id,
                        C.cs,
                        C.nome,
                        ISNULL(TKUS_U.tkusUtilizados,0) + ISNULL(C.quantidade_pontos,0) tkusRecebidos,
                        ISNULL(TKUS_U.tkusUtilizados,0) tkusUtilizados,
                        ((ISNULL(TKUS_U.tkusUtilizados,0) + ISNULL(C.quantidade_pontos,0))-(ISNULL(TKUS_U.tkusUtilizados,0))) tkusDisponiveis,
                        0 tkusPendentes,
                        ISNULL(RQS.qtde,0) tkusPendentes,
                        ISNULL(EXPIR.Expirados,0) Expirados,
                        ISNULL(EXPIR.AExpirar,0) AExpirar
                   FROM Gestao.Colaborador(NOLOCK) C
            OUTER APPLY(SELECT COUNT(id) qtde
			              FROM Gestao.Recomendacao (NOLOCK) RQ
			             WHERE RQ.cs_colaborador = C.cs
			               AND RQ.ativo = 1
			               AND RQ.situacao_recomendacao_id = @situacao_recomendacao_id
			            )RQS
            OUTER APPLY(SELECT SUM(CO.total_pontos) tkusUtilizados
                          FROM Venda.Compra(NOLOCK) CO
                          JOIN Venda.SituacaoCompra(NOLOCK) SC ON SC.id = CO.situacao_compra_id
                         WHERE CO.ativo = 1)
                           AND CO.cs_colaborador = C.cs)TKUS_U
            OUTER APPLY(SELECT SUM(CASE WHEN EXC.data_expiracao < GETDATE() THEN R.qtde_pontos ELSE 0 END) Expirados,
				               SUM(CASE WHEN EXC.data_expiracao >= GETDATE() THEN R.qtde_pontos ELSE 0 END) AExpirar,
				               R.id
			              FROM Gestao.ExpiracaoPontosColaborador (NOLOCK) EXC
			              JOIN Gestao.Recomendacao (NOLOCK) R ON R.id = EXC.recomendacao_id
			               AND R.situacao_recomendacao_id = @situacao_recomendacao_id
			             WHERE R.cs_colaborador = C.cs
			               AND R.ativo = 1
			               AND EXC.ativo = 1
			               AND DATEDIFF(DAY, GETDATE(), EXC.data_expiracao) <= 30
		              GROUP BY R.id)EXPIR
                   WHERE C.cs = @cs_colaborador