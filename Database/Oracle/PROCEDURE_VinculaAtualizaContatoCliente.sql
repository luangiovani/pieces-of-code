create or replace PROCEDURE VinculaAtualizaContatoCliente
(nA012_cd_cli IN INTEGER, nA261_CD_CONT IN INTEGER)
is
sTipo   SEBRAE.t014_contato_cliente.A014_tp_cont%TYPE;
nA014_num_cont INTEGER;
nContatoCliente INTEGER;
BEGIN
  
    SELECT COUNT(DISTINCT A261_CD_CONT) into nContatoCliente FROM T014_CONTATO_CLIENTE WHERE A012_CD_CLI = nA012_CD_CLI AND A261_CD_CONT = nA261_CD_CONT;
    
    if nvl(nContatoCliente, 0) = 0 then
           
      select COUNT(DISTINCT A261_CD_CONT) into nContatoCliente from sebrae.t014_contato_cliente where A014_tp_cont='P' and a012_Cd_Cli = nA012_cd_cli;
    
      if nvl(nContatoCliente,0) = 0 then
        sTipo := 'P';
      else
        sTipo := 'S';
      end if;
    
      select max(a014_num_cont)+1 into nA014_num_cont from sebrae.t014_contato_cliente where a012_Cd_Cli = nA012_cd_cli;
      if nvl(nA014_num_cont,0) = 0 then
        nA014_num_cont := 1;
      end if;
      
      insert into sebrae.t014_contato_cliente (a012_cd_Cli,a014_num_cont,a261_cd_Cont,a014_tp_cont,a014_ind_socio) 
      values (nA012_cd_cli,nA014_num_cont,nA261_CD_CONT,sTipo,0);
      COMMIT;
    else
      UPDATE t014_contato_cliente SET A014_IND_DESAT = 0 WHERE A012_CD_CLI = nA012_CD_CLI AND A261_CD_CONT = nA261_CD_CONT;
      COMMIT;
    end if;

END VinculaAtualizaContatoCliente;