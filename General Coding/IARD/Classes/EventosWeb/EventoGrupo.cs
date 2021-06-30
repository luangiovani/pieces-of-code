using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
using System.Data;

//-- Classe Classes Sebrae
//-- Data : 30/10/2007 
//-- Autor :  Honorato
//--

namespace Classes
{
    public class EventoGrupo  // T2222_EV_GRUPO     
    {
        #region Atributos
        private int codEvento; //
        private string tipCurso;
        private decimal vlrMin;
        private decimal vlrMax;
        private int indAlterado;
        private Grupo2213 codGrupo2213;
        private CondicaoPagtoEv codCondPagto;
        private ProdutoSebrae codProdSebrae;
        private Classifica codClassifica;
        private Pessoa codPessoa;
        private string logUsuario;

        #endregion

        #region Propriedades

        public int CodEvento
        {
            get { return codEvento; }
            set { codEvento = value; }
        }

        public string TipCurso
        {
            get { return tipCurso; }
            set { tipCurso = value; }
        }

        public decimal VlrMin
        {
            get { return vlrMin; }
            set { vlrMin = value; }
        }

        public decimal VlrMax
        {
            get { return vlrMax; }
            set { vlrMax = value; }
        }

        public int IndAlterado
        {
            get { return indAlterado; }
            set { indAlterado = value; }
        }

        public Grupo2213 CodGrupo2213
        {
            get { return codGrupo2213; }
            set { codGrupo2213 = value; }
        }

        public CondicaoPagtoEv CodCondPagto
        {
            get { return codCondPagto; }
            set { codCondPagto = value; }
        }

        public ProdutoSebrae CodProdSebrae
        {
            get { return codProdSebrae; }
            set { codProdSebrae = value; }
        }

        public Classifica CodClassifica
        {
            get { return codClassifica; }
            set { codClassifica = value; }
        }

        public Pessoa CodPessoa
        {
            get { return codPessoa; }
            set { codPessoa = value; }
        }
        public string LogUsuario
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }

        #endregion

        #region Construtores
        public EventoGrupo()
            : this(-1)
        { }
        public EventoGrupo(int codEvento)
        {
            this.codEvento = codEvento;
            //this.codEvento = new Eventos();
            this.codGrupo2213 = new Grupo2213();
            this.codCondPagto = new CondicaoPagtoEv();
            this.codProdSebrae = new ProdutoSebrae();
            this.codClassifica = new Classifica();
            this.codPessoa = new Pessoa();
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "EventoGrupo.EventoGrupoInc";
        private const string SPUPDATE = "EventoGrupo.EventoGrupoAlt";
        private const string SPDELETE = "EventoGrupo.EventoGrupoDel";
        private const string SPSELECTID = "EventoGrupo.EventoGrupoSelId";
        private const string SPSELECTPAG = "EventoGrupo.EventoGrupoSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMCODIGO = "codEvento";
        private const string PARMCURSOR = "curEventoGrupo";
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
                /*0*/ new OracleParameter(PARMCODIGO, OracleType.Int32, 8),
                    new OracleParameter( "tipCurso", OracleType.VarChar),
                    new OracleParameter( "vlrMin", OracleType.Float),
                    new OracleParameter( "vlrMax", OracleType.Float),
                    new OracleParameter( "indAlterado", OracleType.Int32),
                    new OracleParameter( "codGrupo2213", OracleType.Int32),
                    new OracleParameter( "codCondPagto", OracleType.Int32),
                    new OracleParameter( "codProdSebrae", OracleType.VarChar),
                    new OracleParameter( "codClassifica", OracleType.Int32),
                    new OracleParameter( "codPessoa", OracleType.Int32),
                /*10*/ new OracleParameter( "logUsuario", OracleType.VarChar)
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
            parms[0].Value = this.codEvento;
            parms[1].Value = this.tipCurso;
            parms[2].Value = this.vlrMin;
            parms[3].Value = this.vlrMax;
            parms[4].Value = this.indAlterado;
            parms[5].Value = this.codGrupo2213.CodGrupo2213;
            parms[6].Value = this.codCondPagto.CodCondPagto;
            parms[7].Value = this.codProdSebrae.CodProdSebrae;
            parms[8].Value = this.codClassifica.CodClassifica;
            parms[9].Value = this.codPessoa.CodPessoa;
            parms[10].Value = this.logUsuario.ToUpper();
             
                parms[0].Direction = ParameterDirection.Input;
             
        }
        #endregion

        #region Insert

        /// <summary>
        /// Insert com tratamento de transa��o
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
                        //Obtendo a chave de identifica��o do registro inserido.
                        codEvento = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
        /// Insert sem tratamento de transa��o
        /// </summary>
        /// <param name="trans">OracleTransaction</param>
        public void Insert(OracleTransaction trans)
        {
            OracleParameter[] parms = GetParameters();
            SetParameters(parms);
            try
            {
                OracleCommand cmd = DataBase.ExecuteNonQueryCmd(trans, CommandType.StoredProcedure, SPINSERT, parms);
                //Obtendo a chave de identifica��o do registro inserido.
                codEvento = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
        /// Update com tratamento de transa��o
        /// </summary>
        public void Update()
        {
            // --------------------------------------------------------  
            // Obtendo e setando os par�metros 
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
        /// Update sem tratamento de transa��o
        /// </summary>
        /// <param name="trans">OracleTransaction</param>
        public void Update(OracleTransaction trans)
        {
            // -------------------------------------------------------- 
            // Obtendo e setando os par�metros 
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
        /// Delete com tratamento de transa��o
        /// </summary>
        /// <param name="codigo">C�digo do Registro</param>
        public static void Delete(int codigo)
        {
            OracleParameter[] parms = new OracleParameter[] { new OracleParameter(PARMCODIGO, OracleType.Int32, 2) };
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
        /// Delete sem tratamento de transa��o
        /// </summary>
        /// <param name="codigo">C�digo do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(int codigo, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { new OracleParameter(PARMCODIGO, OracleType.Int32, 2) };
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
        /// <param name="codigo">C�digo do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Ap�s a utiliza��o do LoadDataDr n�o esquecer de fechar a conex�o: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codigo)
        {
            OracleParameter[] param = new OracleParameter[] { 

                new OracleParameter(PARMCODIGO, OracleType.Int32, 2), 
                new OracleParameter(PARMCURSOR, OracleType.Cursor)
            };

            param[0].Value = codigo;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transa��o
        /// </summary>
        /// <param name="codigo">C�digo do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Ap�s a utiliza��o do LoadDataDr n�o esquecer de fechar a conex�o: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codigo, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { new OracleParameter(PARMCODIGO, OracleType.Int32, 2),
                                                              new OracleParameter(PARMCURSOR, OracleType.Cursor)
};
            param[0].Value = codigo;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="codigo">C�digo do Registro</param>
        /// <returns>EventoGrupo</returns>
        public static EventoGrupo GetDataRow(int codigo)
        {
            OracleDataReader dr = LoadDataDr(codigo);
            EventoGrupo EventoGrupo = new EventoGrupo();
            try
            {
                if (dr.Read())
                {
                    EventoGrupo.codEvento = Convert.ToInt32(dr["A022_cd_ev"]);
                    EventoGrupo.tipCurso = Convert.ToString(dr["A2222_tp_curso"]);
                    EventoGrupo.vlrMin = Convert.ToDecimal(dr["A2222_pr_min"]);
                    EventoGrupo.vlrMax = Convert.ToDecimal(dr["A2222_pr_max"]);
                    EventoGrupo.indAlterado = Convert.ToInt32(dr["A2222_ind_alterado"]);
                    EventoGrupo.codGrupo2213 = new Grupo2213(Convert.ToInt32(dr["A2213_cd_grupo"]));
                    EventoGrupo.codCondPagto = new CondicaoPagtoEv(Convert.ToInt32(dr["A2204_cd_cond_pgto"]));
                    EventoGrupo.codProdSebrae = new ProdutoSebrae(Convert.ToString(dr["A042_cd_prod_sev"]));
                    EventoGrupo.codClassifica = new Classifica(Convert.ToInt32(dr["A1150_cd_classifica"]));
                    EventoGrupo.codPessoa = new Pessoa(Convert.ToInt32(dr["A052_cd_usuario"]));
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        EventoGrupo.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                EventoGrupo = new EventoGrupo();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return EventoGrupo;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transa��o
        /// </summary>
        /// <param name="codigo">C�digo do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>EventoGrupo</returns>
        public static EventoGrupo GetDataRow(int codigo, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codigo, trans);
            EventoGrupo EventoGrupo = new EventoGrupo();
            try
            {
                if (dr.Read())
                {
                    EventoGrupo.codEvento = Convert.ToInt32(dr["A022_cd_ev"]);
                    EventoGrupo.tipCurso = Convert.ToString(dr["A2222_tp_curso"]);
                    EventoGrupo.vlrMin = Convert.ToDecimal(dr["A2222_pr_min"]);
                    EventoGrupo.vlrMax = Convert.ToDecimal(dr["A2222_pr_max"]);
                    EventoGrupo.indAlterado = Convert.ToInt32(dr["A2222_ind_alterado"]);
                    EventoGrupo.codGrupo2213 = new Grupo2213(Convert.ToInt32(dr["A2213_cd_grupo"]));
                    EventoGrupo.codCondPagto = new CondicaoPagtoEv(Convert.ToInt32(dr["A2204_cd_cond_pgto"]));
                    EventoGrupo.codProdSebrae = new ProdutoSebrae(Convert.ToString(dr["A042_cd_prod_sev"]));
                    EventoGrupo.codClassifica = new Classifica(Convert.ToInt32(dr["A1150_cd_classifica"]));
                    EventoGrupo.codPessoa = new Pessoa(Convert.ToInt32(dr["A052_cd_usuario"]));
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        EventoGrupo.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                EventoGrupo = new EventoGrupo();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return EventoGrupo;
        }
        #endregion

        #region LoadDataPaginacao

        /// <summary>
        /// LoadDataPaginacao
        /// </summary>
        /// <param name="Where">Cl�usula where utilizada na consulta</param>
        /// <param name="PaginaCorrente">N�mero da p�gina que deseja selecionar</param>
        /// <param name="TamanhoPagina">Quantidade de registros em cada p�gina</param>
        /// <param name="ExpressaoOrdenacao">Express�o de ordena��o</param>
        /// <returns>Inst�ncia do objeto Pagina��o, contendo um DataReader e o total de registros</returns>
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

            // dr.Read();

            paginacao.DataReader = dr;
            //  traz o total de registros de retorno direto dos parametros do ExecuteReader---
            paginacao.TotalRegistros = Convert.ToInt32(parms[5].Value);
            return paginacao;
        }
        #endregion


        #endregion

    }
}