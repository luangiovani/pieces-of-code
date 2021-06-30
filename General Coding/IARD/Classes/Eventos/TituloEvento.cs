using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
 
using System.Data;

    /// <summary>
    //-- Classe Classes Sebrae
    //-- Data : 11/06/2007 
    //-- Autor :  Daniel
    /// </summary>

namespace Classes
{
    public class TituloEvento /// T008_TituloEvento
    {
        #region Atributos
        private int codTituloEv;        
        private int codUsuario;
        private int? codEspec;
        private int codTipoEv;
        private string nomeTituloEv;
        private string contEv;
        private decimal cargaHorar;
        private string preRequisito;
        private string publicoAlvo;
        private string endUrl;
        private string nomObservacao;
        private int? codTexto;
        private string nomObjetivo;
        private int? indModular;
        private int? indDesativado;
        private int? subTopEspec;
        private int? codArea;
        private int? numLibera;
        private int? diasBloqueio;
        private int? numRodizio;
        private string nomInformacao;
        private string codObj;
        private string codAb;
        private string codAe;
        private int? horasConsultoria;
        private string nomSintetizado;
        private int? codProdCredenc;
        private int? codAbordagem;
        private int? codCategoria;
        private int? codInstrumento;
        private string nomTipo;
        private int? codMetodologia;
        private int? indGdEvento;
        private DateTime dataInc;
        private string horaUltAlt;
        private DateTime? dataUltAlt;
        private string logUsuario;
        #endregion
         
        #region Propriedades
        public int CodUsuario
        {
            get { return codUsuario; }
            set { codUsuario = value; }
        }
        public int CodTituloEv
        {
            get { return codTituloEv; }
            set { codTituloEv = value; }
        }
        public int? CodEspec
        {
            get { return codEspec; }
            set { codEspec = value; }
        }
        public int CodTipoEv
        {
            get { return codTipoEv; }
            set { codTipoEv = value; }
        }
        public string NomeTituloEv
        {
            get { return nomeTituloEv; }
            set { nomeTituloEv = value; }
        }
        public string ContEv
        {
            get { return contEv; }
            set { contEv = value; }
        }
        public decimal CargaHorar
        {
            get { return cargaHorar; }
            set { cargaHorar = value; }
        }
        public string PreRequisito
        {
            get { return preRequisito; }
            set { preRequisito = value; }
        }
        public string PublicoAlvo
        {
            get { return publicoAlvo; }
            set { publicoAlvo = value; }
        }
        public string EndUrl
        {
            get { return endUrl; }
            set { endUrl = value; }
        }
        public string NomObservacao
        {
            get { return nomObservacao; }
            set { nomObservacao = value; }
        }
        public int? CodTexto
        {
            get { return codTexto; }
            set { codTexto = value; }
        }
        public string NomObjetivo
        {
            get { return nomObjetivo; }
            set { nomObjetivo = value; }
        }
        public int? IndModular
        {
            get { return indModular; }
            set { indModular = value; }
        }
        public int? SubTopEspec
        {
            get { return subTopEspec; }
            set { subTopEspec = value; }
        }
        public int? IndDesativado
        {
            get { return indDesativado; }
            set { indDesativado = value; }
        }
        public int? CodArea
        {
            get { return codArea; }
            set { codArea = value; }
        }
        public int? NumLibera
        {
            get { return numLibera; }
            set { numLibera = value; }
        }
        public int? DiasBloqueio
        {
            get { return diasBloqueio; }
            set { diasBloqueio = value; }
        }
        public int? NumRodizio
        {
            get { return numRodizio; }
            set { numRodizio = value; }
        }
        public string NomInformacao
        {
            get { return nomInformacao; }
            set { nomInformacao = value; }
        }
        public string CodObj
        {
            get { return codObj; }
            set { codObj = value; }
        }
        public string CodAb
        {
            get { return codAb; }
            set { codAb = value; }
        }
        public string CodAe
        {
            get { return codAe; }
            set { codAe = value; }
        }
        public int? HorasConsultoria
        {
            get { return horasConsultoria; }
            set { horasConsultoria = value; }
        }
        public string NomSintetizado
        {
            get { return nomSintetizado; }
            set { nomSintetizado = value; }
        }
        public int? CodProdCredenc
        {
            get { return codProdCredenc; }
            set { codProdCredenc = value; }
        }
        public int? CodAbordagem
        {
            get { return codAbordagem; }
            set { codAbordagem = value; }
        }
        public int? CodCategoria
        {
            get { return codCategoria; }
            set { codCategoria = value; }
        }
        public int? CodInstrumento
        {
            get { return codInstrumento; }
            set { codInstrumento = value; }
        }
        public string NomTipo
        {
            get { return nomTipo; }
            set { nomTipo = value; }
        }
        public int? CodMetodologia
        {
            get { return codMetodologia; }
            set { codMetodologia = value; }
        }
        public int? IndGdEvento
        {
            get { return indGdEvento; }
            set { indGdEvento = value; }
        }
        public DateTime DataInc
        {
            get { return dataInc; }
            set { dataInc = value; }
        }
        public string HoraUltAlt
        {
            get { return horaUltAlt; }
            set { horaUltAlt = value; }
        }
        public DateTime? DataUltAlt
        {
            get { return dataUltAlt; }
            set { dataUltAlt = value; }
        }
        public string LogUsuario
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "TituloEvento.TituloEventoInc";
        private const string SPUPDATE = "TituloEvento.TituloEventoAlt";
        private const string SPDELETE = "TituloEvento.TituloEventoDel";
        private const string SPSELECTID = "TituloEvento.TituloEventoSelId";
        private const string SPSELECTIDContrato = "TituloEvento.TituloContratoSelId";
        private const string SPSELECTIDKITContrato = "TituloEvento.KITContratoSelId";

        
        private const string SPSELECT = "TituloEvento.TituloEventoSel";
        private const string SPSELECTPAG = "TituloEvento.TituloEventoSelPag";
        private const string SPSELECTPAGContrato = "TituloEvento.TitEveContratoSelPag";
        private const string SPSELECTPAGKITContrato = "TituloEvento.KITContratoSelPag";
        
        private const string SPINTERESSEPAG = "TituloEvento.TituloInteressadoSelPag";
        private const string SPSELECTIDCONTEV = "TituloEvento.TituloEventoSelIdContEv";
        private const string SPSELECTTIPOEVENTO = "TituloEvento.TipoEventoSel";
        private const string SPSELECTAREACONHEC = "TituloEvento.AreaConhecSel";
        private const string SPSELECTMETODO = "TituloEvento.MetodoSel";
        private const string SPSELECTTITULOMAISGRUPO = "TituloEvento.TituloMaisGrupo";
        #endregion
        
        #region Construtores
        public TituloEvento()
            : this(-1)
        { }
        public TituloEvento(int codTituloEv)
        {
            this.codTituloEv = codTituloEv;
        }
        #endregion

        #region Parametros Oracle
        private const string PARMCODIGO = "codTituloEv";
        private const string PARMCURSOR = "curTituloEv";
        #endregion

        #region Metodos


        #region GetParameters
        public static OracleParameter[] GetParameters()
        {
            OracleParameter[] parms;

            // Tentando buscar os parameters do cache        
            parms = DataBase.GetCachedParameters(SPINSERT);
            //parms = OutputCacheParameters(SPINSERT);
            if (parms == null)
            {
                parms = new OracleParameter[]{ 
                    /*0*/ new OracleParameter(PARMCODIGO, OracleType.Int32, 4, ParameterDirection.InputOutput.ToString()) ,
                    /*1*/ new OracleParameter( "codUsuario", OracleType.Int32),
                    /*2*/ new OracleParameter( "codEspec", OracleType.Int32),
                    /*3*/ new OracleParameter( "codTipoEv", OracleType.Int32),
                    /*4*/ new OracleParameter( "nomeTituloEv", OracleType.VarChar),
                    /*5*/ new OracleParameter( "contEv", OracleType.LongVarChar),
                    /*6*/ new OracleParameter( "cargaHorar", OracleType.Float),
                    /*7*/ new OracleParameter( "preRequisito", OracleType.VarChar),
                    /*8*/ new OracleParameter( "publicoAlvo", OracleType.VarChar),
                    /*9*/ new OracleParameter( "endUrl", OracleType.VarChar),
                    /*10*/ new OracleParameter( "nomObservacao", OracleType.VarChar),
                    /*11*/ new OracleParameter( "codTexto", OracleType.Int32),
                    /*12*/ new OracleParameter( "nomObjetivo", OracleType.VarChar),
                    /*13*/ new OracleParameter( "indModular", OracleType.Int32),
                    /*14*/ new OracleParameter( "subTopEspec", OracleType.Int32),                     
                    /*15*/ new OracleParameter( "indDesativado", OracleType.Int32),
                    /*16*/ new OracleParameter( "codArea", OracleType.Int32),
                    /*17*/ new OracleParameter( "numLibera", OracleType.Int32),
                    /*18*/ new OracleParameter( "diasBloqueio", OracleType.Int32),
                    /*19*/ new OracleParameter( "numRodizio", OracleType.Int32),
                    /*20*/ new OracleParameter( "nomInformacao", OracleType.VarChar),
                    /*21*/ new OracleParameter( "codObj", OracleType.VarChar),
                    /*22*/ new OracleParameter( "codAb", OracleType.VarChar),
                    /*23*/ new OracleParameter( "CodAe", OracleType.VarChar),
                    /*24*/ new OracleParameter( "horasConsultoria", OracleType.Int32),
                    /*25*/ new OracleParameter( "nomSintetizado", OracleType.VarChar),
                    /*26*/ new OracleParameter( "codProdCredenc", OracleType.Int32),
                    /*27*/ new OracleParameter( "codAbordagem", OracleType.Int32),
                    /*28*/ new OracleParameter( "codCategoria", OracleType.Int32),
                    /*29*/ new OracleParameter( "codInstrumento", OracleType.Int32),
                    /*30*/ new OracleParameter( "nomTipo", OracleType.VarChar),
                    /*31*/ new OracleParameter( "codMetodologia", OracleType.Int32),
                    /*32*/ new OracleParameter( "indGdEvento", OracleType.Int32),
                    /*33*/ new OracleParameter( "logUsuario", OracleType.VarChar)
                };

                // Criando cache dos parameters 
                DataBase.CacheParameters(SPINSERT, parms);
            }
            return (parms);
        }

        #endregion

        #region SetParameters
        public void SetParameters(OracleParameter[] parms)
        {
            parms[0].Value = this.codTituloEv;
            parms[1].Value = this.codUsuario;
            parms[2].Value = DBNull.Value;
            if (this.codEspec != null)
            {
                parms[2].Value = this.codEspec;
            }
            parms[3].Value = this.codTipoEv;
            parms[4].Value = this.nomeTituloEv.ToUpper();
            parms[5].Value = this.contEv.ToUpper() + " ";//Adicionado espaço branco para impedir erros do formato Longvarchar do campo--
            parms[6].Value = this.cargaHorar;
            parms[7].Value = this.preRequisito;
            parms[8].Value = this.publicoAlvo.ToUpper();
            parms[9].Value = "";
            if (this.endUrl != null)
            {
                parms[9].Value = this.endUrl.ToUpper();
            }
            parms[10].Value = "";
            if (this.nomObservacao != null)
            {
                parms[10].Value = this.nomObservacao.ToUpper();
            }
            parms[11].Value = DBNull.Value;
            if (this.codTexto != null)
            {
                parms[11].Value = this.codTexto;
            }
            parms[12].Value = this.nomObjetivo.ToUpper();
            parms[13].Value = this.indModular;
            parms[14].Value = this.subTopEspec;
            parms[15].Value = DBNull.Value;
            if (this.indDesativado != null)
            {
                parms[15].Value = this.indDesativado;
            }
            parms[16].Value = DBNull.Value;
            if (this.codArea != null)
            {
                parms[16].Value = this.codArea;
            }
            parms[17].Value =DBNull.Value;
            if (this.numLibera != null)
            {
                parms[17].Value = this.numLibera;
            }
            parms[18].Value =DBNull.Value;
            if (this.diasBloqueio != null)
            {
                parms[18].Value = this.diasBloqueio;
            }
            parms[19].Value = this.numRodizio;
            parms[20].Value = "";
            if (this.nomInformacao != null)
            {
                parms[20].Value = this.nomInformacao.ToUpper();
            }
            parms[21].Value = "";
            if (this.codObj != null)
            {
                parms[21].Value = this.codObj.ToUpper();
            }
            parms[22].Value = "";
            if (this.codAb != null)
            {
                parms[22].Value = this.codAb.ToUpper();
            } parms[23].Value = "";
            if (this.CodAe != null)
            {
                parms[23].Value = this.CodAe.ToUpper();
            }
            parms[24].Value =DBNull.Value;
            if (this.horasConsultoria != null)
            {
            parms[24].Value = this.horasConsultoria;
            }
            parms[25].Value = this.nomSintetizado.ToUpper();
            parms[26].Value =DBNull.Value;
            if (this.codProdCredenc != null)
            {
                parms[26].Value = this.codProdCredenc;
            }
            parms[27].Value =DBNull.Value;
            if (this.codAbordagem != null)
            {
                parms[27].Value = this.codAbordagem;
            }
            parms[28].Value =DBNull.Value;
            if (this.codCategoria != null)
            {
                parms[28].Value = this.codCategoria;
            }
            parms[29].Value =DBNull.Value;
            if (this.codInstrumento != null)
            {
                parms[29].Value = this.codInstrumento;
            }
            parms[30].Value = "";
            if (this.nomTipo != null)
            {
                parms[30].Value = this.nomTipo.ToUpper();
            }
            parms[31].Value = this.codMetodologia;
            parms[32].Value =DBNull.Value;
            if (this.indGdEvento != null)
            {
                parms[32].Value = this.indGdEvento;
            }
            parms[33].Value = this.logUsuario.ToUpper();
            if (this.codTituloEv < 0)
            {
                parms[0].Direction = ParameterDirection.Output;
            }
            else
            {
                parms[0].Direction = ParameterDirection.Input;
            }
        }
        #endregion

        #region Insert

        /// <summary>
        /// Insert com tratamento de transação
        /// </summary>
        public void Insert()
        {
            OracleParameter[] parms = GetParameters();
            SetParameters(parms);

            using (OracleConnection conn = new OracleConnection(DataBase.CONN_STRING))
            {
                conn.Open();
                using (OracleTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        OracleCommand cmd = DataBase.ExecuteNonQueryCmd(trans, CommandType.StoredProcedure, SPINSERT, parms);
                        //Obtendo a chave de identificação do registro inserido.
                        codTituloEv = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
                        cmd.Parameters.Clear();
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw (ex);
                    }
                }
            }
        }

        /// <summary>
        /// Insert sem tratamento de transação
        /// </summary>
        /// <param name="trans">OracleTransaction</param>
        public void Insert(OracleTransaction trans)
        {
            OracleParameter[] parms = GetParameters();
            SetParameters(parms);
            try
            {
                OracleCommand cmd = DataBase.ExecuteNonQueryCmd(trans, CommandType.StoredProcedure, SPINSERT, parms);
                //Obtendo a chave de identificação do registro inserido.
                codTituloEv = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
                cmd.Parameters.Clear();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region Update

        /// <summary>
        /// Update com tratamento de transação
        /// </summary>
        public void Update()
        {
            // --------------------------------------------------------  
            // Obtendo e setando os parâmetros 
            OracleParameter[] parms = GetParameters();
            SetParameters(parms);
            // -------------------------------------------------------- 
            using (OracleConnection conn = new OracleConnection(DataBase.CONN_STRING))
            {
                conn.Open();
                using (OracleTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        DataBase.ExecuteNonQuery(trans, CommandType.StoredProcedure, SPUPDATE, parms);
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw ex;
                    }
                }
            }
        } // END UPDATE

        /// <summary>
        /// Update sem tratamento de transação
        /// </summary>
        /// <param name="trans">OracleTransaction</param>
        public void Update(OracleTransaction trans)
        {
            // -------------------------------------------------------- 
            // Obtendo e setando os parâmetros 
            OracleParameter[] parms = GetParameters();
            SetParameters(parms);
            // -------------------------------------------------------- 
            try
            {
                DataBase.ExecuteNonQuery(trans, CommandType.StoredProcedure, SPUPDATE, parms);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } // END UPDATE 
        #endregion

        #region Delete

        /// <summary>
        /// Delete com tratamento de transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        public static void Delete(int codigo)
        {
            OracleParameter[] parms = new OracleParameter[] { new OracleParameter(PARMCODIGO, OracleType.Int32, 4) };
            parms[0].Value = codigo;
            using (OracleConnection conn = new OracleConnection(DataBase.CONN_STRING))
            {
                conn.Open();
                using (OracleTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        DataBase.ExecuteNonQuery(trans, CommandType.StoredProcedure, SPDELETE, parms);
                        trans.Commit();
                    }
                    catch (OracleException ex)
                    {
                        trans.Rollback();
                        throw (ex);
                    }
                }
            }
        } // end DELETE

        /// <summary>
        /// Delete sem tratamento de transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(int codigo, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { new OracleParameter(PARMCODIGO, OracleType.Int32, 4) };
                parms[0].Value = codigo;
                DataBase.ExecuteNonQuery(trans, CommandType.StoredProcedure, SPDELETE, parms);
            }//try
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region LoadDataDr


        /// <summary>
        /// LoadDataDr
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codigo)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCODIGO, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codigo;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }
        //para contratos
        public static OracleDataReader LoadDataDrContrato(int codigo)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCODIGO, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codigo;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTIDContrato, param);
            return dr;
        }

        public static OracleDataReader LoadDataDrKITContrato(int codigo)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCODIGO, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codigo;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTIDKITContrato, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codigo, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { new OracleParameter(PARMCODIGO, OracleType.Int32, 4),
                                                                  new OracleParameter(PARMCURSOR, OracleType.Cursor)
};
            param[0].Value = codigo;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region LoadDataDrTipoEv


        /// <summary>
        /// LoadDataDr
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDrTipoEv(int codigo)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCODIGO, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codigo;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTIDCONTEV, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDrTipoEv(int codigo, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { new OracleParameter(PARMCODIGO, OracleType.Int32, 4),
                                                                  new OracleParameter(PARMCURSOR, OracleType.Cursor)
};
            param[0].Value = codigo;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTIDCONTEV, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <returns>TituloEvento</returns>
        public static TituloEvento GetDataRow(int codigo)
        {
            OracleDataReader dr = LoadDataDr(codigo);
            TituloEvento titEv = new TituloEvento();
            try
            {
                if (dr.Read())
                {
                    titEv.codTituloEv = Convert.ToInt32(dr["A008_cd_tit_ev"]);
                    titEv.codUsuario = Convert.ToInt32(dr["A052_cd_usuario"]);
                    if (dr["A006_cd_espec"] != DBNull.Value && dr["A006_cd_espec"] != DBNull.Value && dr["A006_cd_espec"].ToString() != "")
                        titEv.codEspec = Convert.ToInt32(dr["A006_cd_espec"]);
                    titEv.codTipoEv = Convert.ToInt32(dr["A008_cd_tp_ev"]);
                    titEv.nomeTituloEv = Convert.ToString(dr["A008_tit_ev"]);
                    titEv.cargaHorar = Convert.ToDecimal(dr["A008_carga_hor"]);
                    titEv.preRequisito = Convert.ToString(dr["A008_pre_req"]);
                    titEv.publicoAlvo = Convert.ToString(dr["A008_pub_alvo"]);
                    titEv.dataInc = Convert.ToDateTime(dr["A008_dt_inc"]);
                    titEv.endUrl = Convert.ToString(dr["A008_url"]);
                    titEv.nomObservacao = Convert.ToString(dr["A008_obs"]);
                    if (dr["A075_cd_texto"] != DBNull.Value && dr["A075_cd_texto"] != DBNull.Value && dr["A075_cd_texto"].ToString() != "")
                        titEv.codTexto = Convert.ToInt32(dr["A075_cd_texto"]);
                    titEv.nomObjetivo = Convert.ToString(dr["A008_objetivo"]);
                    if (dr["A008_ind_modular"] != DBNull.Value && dr["A008_ind_modular"] != DBNull.Value && dr["A008_ind_modular"].ToString() != "")
                        titEv.indModular = Convert.ToInt32(dr["A008_ind_modular"]);
                    if (dr["A008_subtop_espec"] != DBNull.Value && dr["A008_subtop_espec"] != DBNull.Value && dr["A008_subtop_espec"].ToString() != "")
                        titEv.subTopEspec = Convert.ToInt32(dr["A008_subtop_espec"]);
                    if (dr["A008_ind_desat"] != DBNull.Value && dr["A008_ind_desat"] != DBNull.Value && dr["A008_ind_desat"].ToString() != "")
                        titEv.indDesativado = Convert.ToInt32(dr["A008_ind_desat"]);
                    if (dr["A1144_cd_area"] != DBNull.Value && dr["A1144_cd_area"] != DBNull.Value && dr["A1144_cd_area"].ToString() != "")
                        titEv.codArea = Convert.ToInt32(dr["A1144_cd_area"]);
                    if (dr["A008_libera"] != DBNull.Value && dr["A008_libera"] != DBNull.Value && dr["A008_libera"].ToString() != "")
                        titEv.numLibera = Convert.ToInt32(dr["A008_libera"]);
                    if (dr["A008_dias_bloqueio"] != DBNull.Value && dr["A008_dias_bloqueio"] != DBNull.Value && dr["A008_dias_bloqueio"].ToString() != "")
                        titEv.diasBloqueio = Convert.ToInt32(dr["A008_dias_bloqueio"]);
                    if (dr["A008_rodizio"] != DBNull.Value && dr["A008_rodizio"] != DBNull.Value && dr["A008_rodizio"].ToString() != "")
                        titEv.numRodizio = Convert.ToInt32(dr["A008_rodizio"]);
                    titEv.nomInformacao = Convert.ToString(dr["A008_informacao"]);
                    titEv.codObj = Convert.ToString(dr["codobj"]);
                    titEv.codAb = Convert.ToString(dr["codab"]);
                    titEv.CodAe = Convert.ToString(dr["codae"]);
                    if (dr["A008_horas_consultoria"] != DBNull.Value)
                        titEv.horasConsultoria = Convert.ToInt32(dr["A008_horas_consultoria"]);
                    titEv.nomSintetizado = Convert.ToString(dr["A008_sintetizado"]);
                    if (dr["A1157_cd_prod_credenc"] != DBNull.Value)
                        titEv.codProdCredenc = Convert.ToInt32(dr["A1157_cd_prod_credenc"]);
                    if (dr["A783_cd_abordagem"] != DBNull.Value)
                        titEv.codAbordagem = Convert.ToInt32(dr["A783_cd_abordagem"]);
                    if (dr["A784_cd_categoria"] != DBNull.Value)
                        titEv.codCategoria = Convert.ToInt32(dr["A784_cd_categoria"]);
                    if (dr["A786_cd_instrumento"] != DBNull.Value)
                        titEv.codInstrumento = Convert.ToInt32(dr["A786_cd_instrumento"]);
                    titEv.nomTipo = Convert.ToString(dr["A785_tipo"]);
                    if (dr["A790_cd_metodologia"] != DBNull.Value)
                        titEv.codMetodologia = Convert.ToInt32(dr["A790_cd_metodologia"]);
                    if (dr["A008_ind_gd_evento"] != DBNull.Value)
                        titEv.indGdEvento = Convert.ToInt32(dr["A008_ind_gd_evento"]);
                    titEv.horaUltAlt = Convert.ToString(dr["A008_hr_ult_alt"]);
                    if (dr["dta_inc_alt"] != DBNull.Value)
                        titEv.dataUltAlt = Convert.ToDateTime(dr["dta_inc_alt"]);
                    titEv.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                titEv = new TituloEvento();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }


            OracleDataReader dr2 = LoadDataDrTipoEv(codigo);
            try
            {
                if (dr2.Read())
                {
                    if (dr2["A008_cont_ev"] != DBNull.Value)
                    titEv.contEv = Convert.ToString(dr2["A008_cont_ev"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr2.IsClosed)
                    dr2.Close();
                titEv = new TituloEvento();
                throw (ex);
            }
            finally
            {
                if (!dr2.IsClosed)
                    dr2.Close();
            }

            return titEv;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>TituloEvento</returns>
        public static TituloEvento GetDataRow(int codigo, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codigo);
            TituloEvento titEv = new TituloEvento();
            try
            {
                if (dr.Read())
                {
                    titEv.codTituloEv = Convert.ToInt32(dr["A008_cd_tit_ev"]);
                    titEv.codUsuario = Convert.ToInt32(dr["A052_cd_usuario"]);
                    if (dr["A006_cd_espec"] != DBNull.Value && dr["A006_cd_espec"] != DBNull.Value && dr["A006_cd_espec"].ToString() != "")
                        titEv.codEspec = Convert.ToInt32(dr["A006_cd_espec"]);
                    titEv.codTipoEv = Convert.ToInt32(dr["A008_cd_tp_ev"]);
                    titEv.nomeTituloEv = Convert.ToString(dr["A008_tit_ev"]);
                    titEv.cargaHorar = Convert.ToDecimal(dr["A008_carga_hor"]);
                    titEv.preRequisito = Convert.ToString(dr["A008_pre_req"]);
                    titEv.publicoAlvo = Convert.ToString(dr["A008_pub_alvo"]);
                    titEv.dataInc = Convert.ToDateTime(dr["A008_dt_inc"]);
                    titEv.endUrl = Convert.ToString(dr["A008_url"]);
                    titEv.nomObservacao = Convert.ToString(dr["A008_obs"]);
                    if (dr["A075_cd_texto"] != DBNull.Value && dr["A075_cd_texto"] != DBNull.Value && dr["A075_cd_texto"].ToString() != "")
                        titEv.codTexto = Convert.ToInt32(dr["A075_cd_texto"]);
                    titEv.nomObjetivo = Convert.ToString(dr["A008_objetivo"]);
                    if (dr["A008_ind_modular"] != DBNull.Value && dr["A008_ind_modular"] != DBNull.Value && dr["A008_ind_modular"].ToString() != "")
                        titEv.indModular = Convert.ToInt32(dr["A008_ind_modular"]);
                    if (dr["A008_subtop_espec"] != DBNull.Value && dr["A008_subtop_espec"] != DBNull.Value && dr["A008_subtop_espec"].ToString() != "")
                        titEv.subTopEspec = Convert.ToInt32(dr["A008_subtop_espec"]);
                    if (dr["A008_ind_desat"] != DBNull.Value && dr["A008_ind_desat"] != DBNull.Value && dr["A008_ind_desat"].ToString() != "")
                        titEv.indDesativado = Convert.ToInt32(dr["A008_ind_desat"]);
                    if (dr["A1144_cd_area"] != DBNull.Value && dr["A1144_cd_area"] != DBNull.Value && dr["A1144_cd_area"].ToString() != "")
                        titEv.codArea = Convert.ToInt32(dr["A1144_cd_area"]);
                    if (dr["A008_libera"] != DBNull.Value && dr["A008_libera"] != DBNull.Value && dr["A008_libera"].ToString() != "")
                        titEv.numLibera = Convert.ToInt32(dr["A008_libera"]);
                    if (dr["A008_dias_bloqueio"] != DBNull.Value && dr["A008_dias_bloqueio"] != DBNull.Value && dr["A008_dias_bloqueio"].ToString() != "")
                        titEv.diasBloqueio = Convert.ToInt32(dr["A008_dias_bloqueio"]);
                    if (dr["A008_rodizio"] != DBNull.Value && dr["A008_rodizio"] != DBNull.Value && dr["A008_rodizio"].ToString() != "")
                        titEv.numRodizio = Convert.ToInt32(dr["A008_rodizio"]);
                    titEv.nomInformacao = Convert.ToString(dr["A008_informacao"]);
                    titEv.codObj = Convert.ToString(dr["codobj"]);
                    titEv.codAb = Convert.ToString(dr["codab"]);
                    titEv.CodAe = Convert.ToString(dr["codae"]);
                    if (dr["A008_horas_consultoria"] != DBNull.Value)
                        titEv.horasConsultoria = Convert.ToInt32(dr["A008_horas_consultoria"]);
                    titEv.nomSintetizado = Convert.ToString(dr["A008_sintetizado"]);
                    if (dr["A1157_cd_prod_credenc"] != DBNull.Value)
                        titEv.codProdCredenc = Convert.ToInt32(dr["A1157_cd_prod_credenc"]);
                    if (dr["A783_cd_abordagem"] != DBNull.Value)
                        titEv.codAbordagem = Convert.ToInt32(dr["A783_cd_abordagem"]);
                    if (dr["A784_cd_categoria"] != DBNull.Value)
                        titEv.codCategoria = Convert.ToInt32(dr["A784_cd_categoria"]);
                    if (dr["A786_cd_instrumento"] != DBNull.Value)
                        titEv.codInstrumento = Convert.ToInt32(dr["A786_cd_instrumento"]);
                    titEv.nomTipo = Convert.ToString(dr["A785_tipo"]);
                    if (dr["A790_cd_metodologia"] != DBNull.Value)
                        titEv.codMetodologia = Convert.ToInt32(dr["A790_cd_metodologia"]);
                    if (dr["A008_ind_gd_evento"] != DBNull.Value)
                        titEv.indGdEvento = Convert.ToInt32(dr["A008_ind_gd_evento"]);
                    titEv.horaUltAlt = Convert.ToString(dr["A008_hr_ult_alt"]);
                    if (dr["dta_inc_alt"] != DBNull.Value)
                        titEv.dataUltAlt = Convert.ToDateTime(dr["dta_inc_alt"]);
                    titEv.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                titEv = new TituloEvento();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }


            OracleDataReader dr2 = LoadDataDrTipoEv(codigo);
            try
            {
                if (dr2.Read())
                {
                    if (dr2["A008_cont_ev"] != DBNull.Value)
                        titEv.contEv = Convert.ToString(dr2["A008_cont_ev"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr2.IsClosed)
                    dr2.Close();
                titEv = new TituloEvento();
                throw (ex);
            }
            finally
            {
                if (!dr2.IsClosed)
                    dr2.Close();
            }

            return titEv;
        }
        #endregion

        #region LoadDataPaginacao


        /// <summary>
        /// LoadDataPaginacao
        /// </summary>
        /// <param name="Where">Cláusula where utilizada na consulta</param>
        /// <param name="PaginaCorrente">Número da página que deseja selecionar</param>
        /// <param name="TamanhoPagina">Quantidade de registros em cada página</param>
        /// <param name="ExpressaoOrdenacao">Expressão de ordenação</param>
        /// <returns>Instância do objeto Paginação, contendo um DataReader e o total de registros</returns>
        /// 
        public static Paginacao LoadDataPaginacao(string Where, int PaginaCorrente, int TamanhoPagina, string ExpressaoOrdenacao)
        {
            Paginacao paginacao = new Paginacao();

            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter("sWhere", OracleType.VarChar,5000),
			    new OracleParameter("CurrentPage", OracleType.Int32),
			    new OracleParameter("PageSize", OracleType.Int32),
			    new OracleParameter("SortExpression", OracleType.VarChar,50),
                new OracleParameter(PARMCURSOR, OracleType.Cursor),
                new OracleParameter("nRegistro", OracleType.Int32)
              };

            parms[0].Value = Where;
            parms[1].Value = PaginaCorrente;
            parms[2].Value = TamanhoPagina;
            parms[3].Value = ExpressaoOrdenacao;
            parms[4].Direction = ParameterDirection.Output;
            parms[5].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTPAG, parms);

            paginacao.DataReader = dr;
            //  traz o total de registros de retorno direto dos parametros do ExecuteReader---
            paginacao.TotalRegistros = Convert.ToInt32(parms[5].Value);
            return paginacao;
        }

        public static Paginacao LoadDataPaginacaoContrato(string Where, int PaginaCorrente, int TamanhoPagina, string ExpressaoOrdenacao)
        {
            Paginacao paginacao = new Paginacao();

            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter("sWhere", OracleType.VarChar,5000),
			    new OracleParameter("CurrentPage", OracleType.Int32),
			    new OracleParameter("PageSize", OracleType.Int32),
			    new OracleParameter("SortExpression", OracleType.VarChar,50),
                new OracleParameter(PARMCURSOR, OracleType.Cursor),
                new OracleParameter("nRegistro", OracleType.Int32)
              };

            parms[0].Value = Where;
            parms[1].Value = PaginaCorrente;
            parms[2].Value = TamanhoPagina;
            parms[3].Value = ExpressaoOrdenacao;
            parms[4].Direction = ParameterDirection.Output;
            parms[5].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTPAGContrato, parms);

            paginacao.DataReader = dr;
            //  traz o total de registros de retorno direto dos parametros do ExecuteReader---
            paginacao.TotalRegistros = Convert.ToInt32(parms[5].Value);
            return paginacao;
        }

        public static Paginacao LoadDataPaginacaoKITContrato(string Where, int PaginaCorrente, int TamanhoPagina, string ExpressaoOrdenacao)
        {
            Paginacao paginacao = new Paginacao();

            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter("sWhere", OracleType.VarChar,5000),
			    new OracleParameter("CurrentPage", OracleType.Int32),
			    new OracleParameter("PageSize", OracleType.Int32),
			    new OracleParameter("SortExpression", OracleType.VarChar,50),
                new OracleParameter(PARMCURSOR, OracleType.Cursor),
                new OracleParameter("nRegistro", OracleType.Int32)
              };

            parms[0].Value = Where;
            parms[1].Value = PaginaCorrente;
            parms[2].Value = TamanhoPagina;
            parms[3].Value = ExpressaoOrdenacao;
            parms[4].Direction = ParameterDirection.Output;
            parms[5].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTPAGKITContrato, parms);

            paginacao.DataReader = dr;
            //  traz o total de registros de retorno direto dos parametros do ExecuteReader---
            paginacao.TotalRegistros = Convert.ToInt32(parms[5].Value);
            return paginacao;
        }

        //igual mas para interessados SPINTERESSEPAG
        public static Paginacao LoadDataPagInteressados(string Where, int PaginaCorrente, int TamanhoPagina, string ExpressaoOrdenacao)
        {
            Paginacao paginacao = new Paginacao();

            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter("sWhere", OracleType.VarChar,5000),
			    new OracleParameter("CurrentPage", OracleType.Int32),
			    new OracleParameter("PageSize", OracleType.Int32),
			    new OracleParameter("SortExpression", OracleType.VarChar,50),
                new OracleParameter(PARMCURSOR, OracleType.Cursor),
                new OracleParameter("nRegistro", OracleType.Int32)
              };

            parms[0].Value = Where;
            parms[1].Value = PaginaCorrente;
            parms[2].Value = TamanhoPagina;
            parms[3].Value = ExpressaoOrdenacao;
            parms[4].Direction = ParameterDirection.Output;
            parms[5].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPINTERESSEPAG, parms);

            paginacao.DataReader = dr;
            //  traz o total de registros de retorno direto dos parametros do ExecuteReader---
            paginacao.TotalRegistros = Convert.ToInt32(parms[5].Value);
            return paginacao;
        }

        #endregion


        #endregion

        #region Metodos Especificos

        #region LoadDataDdlTituloEv
        /// <summary>
        /// Popula o DropDownList Eventos
        /// </summary>
        /// <returns></returns>
        public static OracleDataReader LoadDataDdlTituloEv()
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECT, param);
            return dr;
        }


        /// <summary>
        /// Popula o DropDownList Eventos
        /// </summary>
        /// <returns></returns>
        public static OracleDataReader LoadDataDdlTituloEv(OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] {
                new OracleParameter(PARMCURSOR, OracleType.Cursor)
};

            param[0].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECT, param);
            return dr;
        }

        #endregion

        #region LoadDataDdlEventos
        /// <summary>
        /// Popula o DropDownList Eventos
        /// </summary>
        /// <returns></returns>
        public static OracleDataReader LoadDataDdlEventos()
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTTIPOEVENTO, param);
            return dr;
        }


        /// <summary>
        /// Popula o DropDownList Eventos
        /// </summary>
        /// <returns></returns>
        public static OracleDataReader LoadDataDdlEventos(OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] {
                new OracleParameter(PARMCURSOR, OracleType.Cursor)
};

            param[0].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTTIPOEVENTO, param);
            return dr;
        }

        #endregion

        #region LoadDataDdlAreaConh
        /// <summary>
        /// Popula o DropDownList Area Conhecimento
        /// </summary>
        /// <returns></returns>
        public static OracleDataReader LoadDataDdlAreaConh()
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTAREACONHEC, param);
            return dr;
        }


        /// <summary>
        /// Popula o DropDownList Eventos
        /// </summary>
        /// <returns></returns>
        public static OracleDataReader LoadDataDdlAreaConh(OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] {
                new OracleParameter(PARMCURSOR, OracleType.Cursor)
};

            param[0].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTAREACONHEC, param);
            return dr;
        }

        #endregion

        #region LoadDataDdlMetodo
        /// <summary>
        /// Popula o DropDownList Metodologia
        /// </summary>
        /// <returns></returns>
        public static OracleDataReader LoadDataDdlMetodo()
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTMETODO, param);
            return dr;
        }

        /// <summary>
        /// Popula o DropDownList Eventos
        /// </summary>
        /// <returns></returns>
        public static OracleDataReader LoadDataDdlMetodo(OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] {
                new OracleParameter(PARMCURSOR, OracleType.Cursor)
};

            param[0].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTMETODO, param);
            return dr;
        }

        #endregion

        #region LoadDataDdlTituloMaisGrupo
        /// <summary>
        /// Popula o DropDownList Titulo com Tabela Grupo
        /// </summary>
        /// <returns></returns>
        public static OracleDataReader LoadDataDdltituloMaisGrupo()
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTTITULOMAISGRUPO, param);
            return dr;
        }
        #endregion

        #endregion
    }
}