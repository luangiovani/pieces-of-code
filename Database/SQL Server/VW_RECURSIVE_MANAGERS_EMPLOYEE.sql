/*
This SQL query was written for use in the Dapper implementation.
During the development of this project, one of the requirements was to return all award assessment managers
to the employee.
PROBLEM > Each employee has one manager, and the assesment occurs in cascade
The goal:
	* Select all assessment managers 
*/				 
			

CREATE VIEW [Gestao].[VW_AVALIADORES_COLABORADOR]
AS

WITH CTE_COLABORADOR (CS_Gestor, CS, Email, Nome, Cargo, Nivel)
AS
(    SELECT CO.cs_superior_imediato CS_Gestor, 
            CO.cs CS, 
            CS.email,
			CS.Nome,
            C.nomenclatura Cargo,
            0 AS Nivel
       FROM Gestao.Colaborador(NOLOCK) CO
       JOIN Gestao.Colaborador(NOLOCK) CS ON CO.cs_superior_imediato = CS.cs
	   JOIN Gestao.Cargo (NOLOCK) C ON C.cd_cargo = CS.cd_cargo
  UNION ALL
     SELECT CT.CS_Gestor, 
            CO.cs CS, 
            CT.email,
			CT.Nome,
            CT.Cargo,
            Nivel + 1
       FROM Gestao.Colaborador(NOLOCK) CO
       JOIN Gestao.Cargo (NOLOCK) C ON C.cd_cargo = CO.cd_cargo
       JOIN CTE_COLABORADOR CT ON CT.CS = CO.cs_superior_imediato)

  SELECT CS,
		 CS_Gestor, 
         Cargo,
         Email,
		 Nome,
         Nivel
    FROM CTE_COLABORADOR; 
   
GO


