CREATE MATERIALIZED VIEW VW_DADOS_EVENTOS_HRC AS
SELECT A012_CD_CLI, A014_NUM_CONT, A004_CD_ESCR, A052_CD_USUARIO, A001_NUM_ATEND, CODEMPREENDIMENTO, CODCLIENTE, CNPJ, CPR, NOMECLIENTE, A039_NM_FANT, A039_PORTE, NOMECONTATO, DATAHORAINICIOREALIZACAO, DATAHORAFIMREALIZACAO, NOMEREALIZACAO, CODREALIZACAO, CODREALIZACAOCOMP, TIPOREALIZACAO, INSTRUMENTO, ABORDAGEM, DESCREALIZACAO, CODPROJETO, CODACAO, MESANOCOMPETENCIA, CARGAHORARIA, CODSEBRAE, A022_CD_EV, A001_NUMSEQUENCIAL, A022_CD_SIAC, A261_CD_CONT, A784_CD_CATEGORIA, A039_TPO_PJ, NOMEPROJETO, NOMEACAO, CPF, TIPOCLIENTE
FROM(
                  SELECT 0 A001_NUMSEQUENCIAL,
                0 A001_NUM_ATEND,
                T000.A012_CD_cli, 
                T014.A014_num_cont,
                T000.A004_cd_escr,
                T000.A052_cd_usuario,
                CASE WHEN A018_TP_CLI = 1 THEN 0 ELSE NVL(T012.A012_PARCEIRO_SIAC,0) END CodEmpreendimento,
                CASE WHEN A018_TP_CLI = 1 THEN NVL(T012.A012_PARCEIRO_SIAC,NVL(T014.A014_PARCEIRO_SIAC,0)) ELSE NVL(T014.A014_PARCEIRO_SIAC,0) END CodCliente,
                NVL(A039_CGC,'') CNPJ,
                NVL(A039_IE,'') CPR,
                T018.A018_NM_CLI NomeCliente,
                NVL(T039.A039_NM_FANT,T018.A018_NM_CLI) A039_NM_FANT,
                NVL(T039.A039_porte,'') A039_porte,
                T261.A261_NM_CONT NomeContato,
                to_char(A022_dt_inicio_ev,'MM/dd/yyyy hh:mi' ) DataHoraInicioRealizacao,
                to_char(A022_dt_fim_ev,'MM/dd/yyyy hh:mi' ) DataHoraFimRealizacao,
                A022_nm_Ev NomeRealizacao,
                CASE WHEN nvl(T0EV.A022_cd_siac,0) = 0 AND (CASE a022_tiporealizacao||substr(a022_instrumento,1,8)
                                                            WHEN 'CONConsulto' THEN 4
                                                            WHEN 'CONInformaç' THEN 4
                                                            WHEN 'INSConsulto' THEN 4
                                                            ELSE 0
                                                            END)= 4 THEN
                  T022.A022_CD_EV
                ELSE
                  T0EV.A022_cd_siac
                END CodRealizacao,
                T000.A022_cd_ev CodRealizacaoComp,
                A022_tiporealizacao TipoRealizacao,
                substr(a022_instrumento,1,50) Instrumento,
                substr(A783_dsc_abordagem ,1,1) Abordagem,
                substr('Evento '    ||T022.A022_NM_EV ||
           ' Inicio '    ||to_char(T022.A022_dt_INICIO_EV,'dd-mm-yyyy')  ||
           ' Fim '    ||to_char(T022.A022_dt_FIM_EV,'dd-mm-yyyy')    ||
           DECODE(T197.A197_ind_aprov,1,' Curso Concluido ',0,' Curso Não Concluido ')  ||
           ' Cidade '    ||    T011.A011_NM_CID          ||
           DECODE(nvl(T022.A022_ind_certificado,0),1,' Com Certificado ',0,' Sem Certificado '),1,200) DescRealizacao,
                T773.cod_projeto_sge CodProjeto,
                T772.CodAcao_Seq CodAcao,
                CASE WHEN extract(day from sysdate) > 5 THEN 
                  CASE WHEN extract(month from sysdate) < 10 THEN '0' END || extract(month from sysdate)
                ELSE 
                  substr('0'||a022_mes_ref,length(a022_mes_ref),2)
                END ||a022_ano_ref MesAnoCompetencia,
                cast(A127_horas_Consult as integer) CargaHoraria,
                28 CodSebrae,
                T000.A022_cd_ev,
                nvl(T0EV.A022_cd_siac,0) A022_cd_siac,
                T014.A261_cd_cont,
                T022.A784_CD_CATEGORIA,
                NVL(decode(A039_TPO_PJ,'ECC',1,'PR',2),3) A039_TPO_PJ,
                T773.TITSOL NomeProjeto,
                T772.TITOBJ NomeAcao,
                NVL(T262.A262_NUM_CPF,'') CPF,
                CASE WHEN A018_TP_CLI = 1 THEN 'Físico' ELSE 'Jurídico/Produtor Rural' END TipoCliente
      FROM T000_HISTORICO T000
      JOIN T022_EVENTOS T022 ON T022.A022_CD_EV   = T000.a022_cd_Ev
      JOIN T197_partic_ev_fech T197 ON T197.A022_CD_EV = T022.A022_CD_EV
       AND T197.A012_CD_CLI = T000.A012_CD_CLI    
      JOIN T012_CLI_ATEND T012 ON T012.A012_CD_CLI = T197.A012_CD_CLI
      JOIN T018_CLI_CADAST T018 ON T018.A012_CD_CLI = T012.A012_CD_CLI
 LEFT JOIN T039_PESSOA_JUR T039 ON T039.A012_CD_CLI = T018.A012_CD_CLI
      JOIN T014_CONTATO_CLIENTE T014 ON T014.A012_CD_CLI = T197.A012_CD_CLI
       AND T014.A014_NUM_CONT = T197.A014_NUM_CONT
       AND T014.A261_CD_CONT = T000.A261_CD_CONT
      JOIN T261_CONTATO T261 ON T261.A261_CD_CONT = T014.A261_CD_CONT
      JOIN T262_DAD_AD_CONT T262 ON T262.A261_CD_CONT = T014.A261_CD_CONT
 LEFT JOIN T000_EVENTOS_SIAC T0EV ON T0EV.A022_CD_EV = T022.A022_CD_EV
      JOIN T008_TIT_EVENTO T008 ON T008.A008_CD_TIT_EV = T022.A008_CD_TIT_EV 
       AND NVL(A008_CD_TP_EV,0) != 1
      JOIN T011_CIDADE T011 ON T011.A035_CD_PAIS = T022.A035_CD_PAIS
       AND T011.A021_CD_EST = T022.A021_CD_EST
       AND T011.A011_CD_CID = T022.A011_CD_CID
 LEFT JOIN T921_TITULO_PRODUTO T921 ON T921.A008_CD_TIT_EV = T022.A008_CD_TIT_EV
      JOIN t773_solucao T773 ON T773.codsol = T022.CODSOL
       AND T022.a022_ano_ref = T773.zm_ano
      JOIN t772_objetivo T772 ON T022.codobj = T772.codobj 
       and a022_ano_ref = T772.ctt_ano
 LEFT JOIN T784_categoria T784 ON T784.A784_cd_categoria = T022.A784_cd_categoria
 LEFT JOIN T783_abordagem T783 ON T783.A783_cd_abordagem = T022.A783_cd_abordagem       
      JOIN T127_EVENTOS_COMPL T127 ON  T022.A022_cd_ev=t127.A022_cd_ev       
     WHERE (NVL(A000_HISTORICO,0) < 1000) AND (NVL(T000.A000_HISTORICO,0) NOT IN(22,23,28,29,30,31,36,37,45,46,-22,-23,-28,-29,-30,-31,-36,-37,-45,-46))
       AND T022.a022_dt_inicio_ev > '01-jan-2017'
       AND TO_DATE(T022.A022_dt_fim_ev,'dd/mm/yy') <= TO_DATE(sysdate, 'dd/MM/yy')
       and nvl(a022_naoEnviana,0) = 0 
       and a022_ano_ref >= (SELECT MAX(A077_ANO_REF) FROM T077_MES_REF)
       and a022_tiporealizacao not in ('FER')
       and nvl(a022_ind_proc,0) = 1
       and T772.codacao_seq is not null
       and nvl(T022.A022_cd_siac,0) = 1
       and T022.CodSol is not null
       AND T022.A786_cd_instrumento  is not null
       AND NVL(T0EV.A022_CD_SIAC,0) >= (CASE WHEN
              (CASE a022_tiporealizacao||substr(a022_instrumento,1,8)
                  WHEN 'CONConsulto' THEN 4
                  WHEN 'CONInformaç' THEN 4
                  WHEN 'INSConsulto' THEN 4
                ELSE
                  0
              END)= 4 THEN 0 ELSE 1 END
            ))T
GROUP BY A012_CD_CLI, A014_NUM_CONT, A004_CD_ESCR, A052_CD_USUARIO, A001_NUM_ATEND, CODEMPREENDIMENTO, CODCLIENTE, CNPJ, CPR, NOMECLIENTE, A039_NM_FANT, A039_PORTE, NOMECONTATO, DATAHORAINICIOREALIZACAO, DATAHORAFIMREALIZACAO, NOMEREALIZACAO, CODREALIZACAO, CODREALIZACAOCOMP, TIPOREALIZACAO, INSTRUMENTO, ABORDAGEM, DESCREALIZACAO, CODPROJETO, CODACAO, MESANOCOMPETENCIA, CARGAHORARIA, CODSEBRAE, A022_CD_EV, A001_NUMSEQUENCIAL, A022_CD_SIAC, A261_CD_CONT, A784_CD_CATEGORIA, A039_TPO_PJ, NOMEPROJETO, NOMEACAO, CPF, TIPOCLIENTE