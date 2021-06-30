create or replace TRIGGER "SEBRAE".TI_T211_IR 
AFTER 
  INSERT OR UPDATE  
ON 
  SEBRAE.T211_CRUZAMENTO FOR EACH ROW 

declare    

nRows integer;

begin    

     if NVL( :new.A203_CD_SOLIC_PROD, 0 ) >  0 then

	     nRows := 0;

	     select 
      	  count(*) 
	     into 
      	  nRows
	     from
      	  sebrae.T203_DADOS_SOL_PROD
	     where
      	  	A203_CD_SOLIC_PROD = :new.A203_CD_SOLIC_PROD AND 
		  	A200_NUM_SEQ = :NEW.A200_NUM_SEQ AND
			A201_CD_CLASSE = :NEW.A201_CD_CLASSE ;
		
	     if nRows = 0 Then 
      	  Raise_Application_error(-20999, 'Dados de Solicitacao de Produto Inválido');
	     end if;

     end if;

end;

