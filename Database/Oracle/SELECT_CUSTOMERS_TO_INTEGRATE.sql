   SELECT T018.A012_CD_CLI,
          NVL(MAX(NVL(T014P.A014_NUM_CONT, T014S.A014_NUM_CONT)),0) A014_NUM_CONT,
          NVL(MAX(NVL(T014P.A261_CD_CONT, T014S.A261_CD_CONT)),0) A261_CD_CONT,
          T262.A262_NUM_CPF,
          'senhapadraosge' as Senha,
          T018.A018_NM_CLI,
          decode(T262.A263_cd_esc,1,3,2,5,3,7,4,8,5,10,6,11,7,11,8,5,9,1,10,4,1) GrauEscolaridade,
          decode(T262.A262_dt_nasc,null,'01011995',to_char(A262_dt_nasc ,'ddMMyyyy' )) DtNasc,
          decode(T262.A262_sexo,'M',1,0) Sexo,
          T018.A018_END_CLI|| DECODE(NVL(T018.A018_NMR_END,''), '', '', ', ' || T018.A018_NMR_END) Endereco,
          substr('00000000'||T018.A018_cep_cli,length('00000000'||T018.A018_cep_cli)-7,8) CEP,
          DECODE(NVL(T018.A018_COMP_END,''), '', '', ', ' || T018.A018_COMP_END) EnderecoComplemento,
          NVL(T262.A262_EMAIL,T018.A018_E_MAIL) EMAIL,
          decode(T018.A018_ind_cli_pot,1,1,1) Potencial,
          decode(nvl(T941.a940_cd_ativ,0),0,1005,T941.a940_cd_ativ) CodAtivPF,
          CASE WHEN TO_NUMBER(REPLACE(regexp_replace(NVL(T261.A261_DDD_CONT,''), '[^0-9]', ''),' ','')) IS NULL OR TO_NUMBER(REPLACE(regexp_replace(NVL(T261.A261_DDD_CONT,''), '[^0-9]', ''),' ','')) = 0 
            THEN T018.A018_DDD_CLI
            ELSE T261.A261_DDD_CONT
          END DDDFIXO,
          CASE WHEN TO_NUMBER(REPLACE(regexp_replace(NVL(T261.A261_DDD_CONT,''), '[^0-9]', ''),' ','')) IS NULL OR TO_NUMBER(REPLACE(regexp_replace(NVL(T261.A261_DDD_CONT,''), '[^0-9]', ''),' ','')) = 0 
            THEN T018.A018_TEL_CLI
            ELSE T261.A261_TEL_CONT
          END FONEFIXO,
          CASE WHEN TO_NUMBER(REPLACE(regexp_replace(NVL(T261.A261_2DDD_CONT,''), '[^0-9]', ''),' ','')) IS NULL OR TO_NUMBER(REPLACE(regexp_replace(NVL(T261.A261_2DDD_CONT,''), '[^0-9]', ''),' ','')) = 0 
            THEN T018.A018_2DDD_CLI
            ELSE T261.A261_2DDD_CONT
          END DDDCEL,
          CASE WHEN TO_NUMBER(REPLACE(regexp_replace(NVL(T261.A261_2DDD_CONT,''), '[^0-9]', ''),' ','')) IS NULL OR TO_NUMBER(REPLACE(regexp_replace(NVL(T261.A261_2DDD_CONT,''), '[^0-9]', ''),' ','')) = 0 
            THEN T018.A018_TEL2_CLI
            ELSE T261.A261_CEL_CONT
          END FONECEL,
		  T011.A011_NM_CID
     FROM T018_CLI_CADAST T018
     JOIN T012_CLI_ATEND T012 ON T012.A012_CD_CLI = T018.A012_CD_CLI
LEFT JOIN T941_ATIVCLI T941 ON T941.A012_CD_CLI = T018.A012_CD_CLI     
LEFT JOIN T014_CONTATO_CLIENTE T014P ON T014P.A012_CD_CLI = T018.A012_CD_CLI AND T014P.A014_TP_CONT = 'P'
LEFT JOIN T014_CONTATO_CLIENTE T014S ON T014S.A012_CD_CLI = T018.A012_CD_CLI AND T014S.A014_TP_CONT <> 'P'
     JOIN T262_DAD_AD_CONT T262 ON T262.A261_CD_CONT = NVL(T014P.A261_CD_CONT, T014S.A261_CD_CONT)
     JOIN T261_CONTATO T261 ON T261.A261_CD_CONT = T262.A261_CD_CONT
LEFT JOIN T011_CIDADE T011 ON T011.A011_CD_CID = NVL(T018.A011_CD_CID, T262.A011_CD_CID)
      AND T011.A035_CD_PAIS = NVL(T018.A035_CD_PAIS, T262.A035_CD_PAIS)
      AND T011.A021_CD_EST = NVL(T018.A021_CD_EST, T262.A021_CD_EST)
    WHERE T018.A018_tp_cli = 1
      AND T018.A012_CD_CLI = 1236558
 group by T018.A012_CD_CLI,
          T262.A262_NUM_CPF,
          'senhapadraosge',
          T018.A018_NM_CLI,
          decode(T262.A263_cd_esc,1,3,2,5,3,7,4,8,5,10,6,11,7,11,8,5,9,1,10,4,1),
          decode(T262.A262_dt_nasc,null,'01011995',to_char(A262_dt_nasc ,'ddMMyyyy' )),
          decode(T262.A262_sexo,'M',1,0),
          T018.A018_END_CLI|| DECODE(NVL(T018.A018_NMR_END,''), '', '', ', ' || T018.A018_NMR_END),
          substr('00000000'||T018.A018_cep_cli,length('00000000'||T018.A018_cep_cli)-7,8),
          DECODE(NVL(T018.A018_COMP_END,''), '', '', ', ' || T018.A018_COMP_END),
          NVL(T262.A262_EMAIL,T018.A018_E_MAIL),
          decode(T018.A018_ind_cli_pot,1,1,1),
          decode(nvl(T941.a940_cd_ativ,0),0,1005,T941.a940_cd_ativ),
          T261.A261_DDD_CONT,
          T261.A261_2DDD_CONT,
          T018.A018_DDD_CLI,
          T018.A018_2DDD_CLI,
          T018.A018_TEL_CLI,
          T018.A018_TEL2_CLI,
          T261.A261_CEL_CONT,
          T261.A261_TEL_CONT,
		  T011.A011_NM_CID