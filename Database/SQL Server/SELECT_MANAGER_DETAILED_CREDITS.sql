/*
This SQL query was written for use in the Dapper implementation.
The goal:
	* Select main informations of employee (Gestao.Colaborador)
	* Select how much credits (TKUS): total received (tkusRecebidos), used(tkusUtilizados), available (tkusDisponiveis)
	* Filtered by Manager (gestor) of employee (colaborador)
*/				      
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
		   ISNULL(C.quantidade_pontos,0)