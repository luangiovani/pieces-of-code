using System;
using System.Data.OracleClient;
using System.Data;

namespace Database.Entities
{
    /// <summary>
    /// Classe de Mapeamento com a Entidade de Banco de Dados - T110_TAB_CONS
    /// </summary>
    /// <autor>
    /// Luan Giovani Cassini Fernandes
    /// </autor>
    /// <data>
    /// 07/05/2018
    /// </data>
    /// <atividade>
    /// https://esfera.teamworkpm.net/#tasks/17108514
    /// </atividade>
    public class TabCons
    {
        #region Atributos
        private ConsultaAtend numAtend;
        private ConsultaAtend codTipoConsAt;
        private int codUsuario;
        private int codEscrSebrae;
        private string codItCons;
        private string dscItCons;
        private CaracTab codTabCons;
        private int numSeq;
        private string logUsuario;
        #endregion

        #region Propriedades
        public ConsultaAtend NumAtend
        {
            get { return numAtend; }
            set { numAtend = value; }
        }
        public ConsultaAtend CodTipoConsAt
        {
            get { return codTipoConsAt; }
            set { codTipoConsAt = value; }
        }
        public int CodUsuario
        {
            get { return codUsuario; }
            set { codUsuario = value; }
        }
        public int CodEscrSebrae
        {
            get { return codEscrSebrae; }
            set { codEscrSebrae = value; }
        }
        public string CodItCons
        {
            get { return codItCons; }
            set { codItCons = value; }
        }
        public string DscItCons
        {
            get { return dscItCons; }
            set { dscItCons = value; }
        }
        public CaracTab CodTabCons
        {
            get { return codTabCons; }
            set { codTabCons = value; }
        }
        public int NumSeq
        {
            get { return numSeq; }
            set { numSeq = value; }
        }
        public string LogUsuario
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }
        #endregion

        #region Construtores
        public TabCons()
            : this(-1)
        { }
        public TabCons(int numAtend)
        {
            this.numAtend = new ConsultaAtend();
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "TabCons.TabConsInc";
        private const string SPUPDATE = "TabCons.TabConsAlt";
        private const string SPDELETE = "TabCons.TabConsDel";
        private const string SPSELECTID = "TabCons.TabConsSelId";
        private const string SPSELECTPAG = "TabCons.TabConsSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMnumAtend = "numAtend";
        private const string PARMcodTipoConsAt = "codTipoConsAt";
        private const string PARMcodUsuario = "codUsuario";
        private const string PARMcodEscrSebrae = "codEscrSebrae";
        private const string PARMcodTabCons = "codTabCons";
        private const string PARMnumSeq = "numSeq";	 
        private const string PARMCURSOR = "curTabCons";
        #endregion

        #region Metodos


        #region GetParameters
        public static OracleParameter[] GetParameters()
        {
            OracleParameter[] parms;

            // Tentando buscar os parameters do cache        
            parms = Context.DataBase.GetCachedParameters(SPINSERT);
            //parms = OutputCacheParameters(SPINSERT);
            if (parms == null)
            {
                parms = new OracleParameter[]{ 
                    /*0*/ new OracleParameter(PARMnumAtend, OracleType.Int32, 4, ParameterDirection.InputOutput.ToString()) ,
                    /*1*/ new OracleParameter(PARMcodTipoConsAt, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*2*/ new OracleParameter(PARMcodUsuario, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*3*/ new OracleParameter(PARMcodEscrSebrae, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
			        /*4*/ new OracleParameter(PARMcodTabCons, OracleType.VarChar, 4, ParameterDirection.Input.ToString()) ,
			        /*5*/ new OracleParameter(PARMnumSeq, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*6*/ new OracleParameter( "dscItCons", OracleType.VarChar),
                    /*7*/ new OracleParameter( "codItCons", OracleType.VarChar),

                    /*8*/ new OracleParameter( "logUsuario", OracleType.VarChar)
                };

                // Criando cache dos parameters 
                Context.DataBase.CacheParameters(SPINSERT, parms);
            }
            return (parms);
        }

        #endregion

        #region SetParameters
        public void SetParameters(OracleParameter[] parms)
        {
            parms[0].Value = this.numAtend.NumAtend.NumAtend;
            parms[1].Value = this.codTipoConsAt.CodTipoConsAt.CodTipoConsAt;
            parms[2].Value = this.CodUsuario;
            parms[3].Value = this.CodEscrSebrae;
            parms[4].Value = this.codTabCons.CodTabCons;
            parms[5].Value = this.NumSeq;
			
            if (this.dscItCons != null && this.dscItCons.ToString() != "")
            {
                parms[6].Value = this.dscItCons;
            }
            else 
            {
                parms[6].Value = "";
            }
            if (this.codItCons != null && this.codItCons.ToString() != "")
            {
                parms[7].Value = this.codItCons;
            }
            else 
            {
                parms[7].Value = "";
            }
            
            parms[8].Value = this.logUsuario;
            if (this.numAtend.NumAtend.NumAtend < 0)
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

            using (OracleConnection conn = new OracleConnection(Context.DataBase.CONN_STRING))
            {
                conn.Open();
                using (OracleTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        OracleCommand cmd = Context.DataBase.ExecuteNonQueryCmd(trans, CommandType.StoredProcedure, SPINSERT, parms);
                        //Obtendo a chave de identificação do registro inserido.
                        //numAtend = Convert.ToInt32(cmd.Parameters[PARMnumAtend].Value);
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
                OracleCommand cmd = Context.DataBase.ExecuteNonQueryCmd(trans, CommandType.StoredProcedure, SPINSERT, parms);
                //Obtendo a chave de identificação do registro inserido.
                //numAtend = Convert.ToInt32(cmd.Parameters[PARMnumAtend].Value);
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
            using (OracleConnection conn = new OracleConnection(Context.DataBase.CONN_STRING))
            {
                conn.Open();
                using (OracleTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        Context.DataBase.ExecuteNonQuery(trans, CommandType.StoredProcedure, SPUPDATE, parms);
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
                Context.DataBase.ExecuteNonQuery(trans, CommandType.StoredProcedure, SPUPDATE, parms);
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
        /// <param name="numAtend">Código do Registro</param>
        public static void Delete(int numAtend, int codTipoConsAt, int codUsuario, int codEscrSebrae, string codTabCons, int numSeq)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMnumAtend, OracleType.Int32, 4),
                new OracleParameter(PARMcodTipoConsAt, OracleType.Int32, 4),
                new OracleParameter(PARMcodUsuario, OracleType.Int32, 4),
                new OracleParameter(PARMcodEscrSebrae, OracleType.Int32, 4),
                new OracleParameter(PARMcodTabCons, OracleType.VarChar, 4),
                new OracleParameter(PARMnumSeq, OracleType.Int32, 4)
            };
            parms[0].Value = numAtend;
            parms[1].Value = codTipoConsAt;
            parms[2].Value = codUsuario;
            parms[3].Value = codEscrSebrae;
            parms[4].Value = codTabCons;
            parms[5].Value = numSeq;		 
            using (OracleConnection conn = new OracleConnection(Context.DataBase.CONN_STRING))
            {
                conn.Open();
                using (OracleTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        Context.DataBase.ExecuteNonQuery(trans, CommandType.StoredProcedure, SPDELETE, parms);
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
        /// <param name="numAtend">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(int numAtend, int codTipoConsAt, int codUsuario, int codEscrSebrae, string codTabCons, int numSeq, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMnumAtend, OracleType.Int32, 4),
                new OracleParameter(PARMcodTipoConsAt, OracleType.Int32, 4),
                new OracleParameter(PARMcodUsuario, OracleType.Int32, 4),
                new OracleParameter(PARMcodEscrSebrae, OracleType.Int32, 4),
		        new OracleParameter(PARMcodTabCons, OracleType.VarChar, 4),
		        new OracleParameter(PARMnumSeq, OracleType.Int32, 4)
            };
                parms[0].Value = numAtend;
                parms[1].Value = codTipoConsAt;
                parms[2].Value = codUsuario;
                parms[3].Value = codEscrSebrae;
                parms[4].Value = codTabCons;
                parms[5].Value = numSeq;

                Context.DataBase.ExecuteNonQuery(trans, CommandType.StoredProcedure, SPDELETE, parms);
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
        /// <param name="numAtend">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int numAtend, int codTipoConsAt, int codUsuario, int codEscrSebrae, string codTabCons, int numSeq)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMnumAtend, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodTipoConsAt, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodUsuario, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodEscrSebrae, OracleType.Int32, 4), 
				    new OracleParameter(PARMcodTabCons, OracleType.VarChar, 4), 
				    new OracleParameter(PARMnumSeq, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = numAtend;
            param[1].Value = codTipoConsAt;
            param[2].Value = codUsuario;
            param[3].Value = codEscrSebrae;
            param[4].Value = codTabCons;
            param[5].Value = numSeq;
            param[6].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="numAtend">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int numAtend, int codTipoConsAt, int codUsuario, int codEscrSebrae, string codTabCons, int numSeq, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMnumAtend, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodTipoConsAt, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodUsuario, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodEscrSebrae, OracleType.Int32, 4), 
			        new OracleParameter(PARMcodTabCons, OracleType.VarChar, 4), 
			        new OracleParameter(PARMnumSeq, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = numAtend;
            param[1].Value = codTipoConsAt;
            param[2].Value = codUsuario;
            param[3].Value = codEscrSebrae;
            param[4].Value = codTabCons;
            param[5].Value = numSeq;
            param[6].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="numAtend">Código do Registro</param>
        /// <returns>TabCons</returns>
        public static TabCons GetDataRow(int numAtend, int codTipoConsAt, int codUsuario, int codEscrSebrae, string codTabCons, int numSeq)
        {
            OracleDataReader dr = LoadDataDr(numAtend, codTipoConsAt, codUsuario, codEscrSebrae, codTabCons, numSeq);
            TabCons TabCons = new TabCons();
            try
            {
                if (dr.Read())
                {
                    TabCons.numAtend = new ConsultaAtend(Convert.ToInt32(dr["A001_num_atend"]));
                    if (dr["A005_cd_tp_cons"] != DBNull.Value && dr["A005_cd_tp_cons"] != DBNull.Value && dr["A005_cd_tp_cons"].ToString() != "")
                        TabCons.codTipoConsAt.CodTipoConsAt.CodTipoConsAt = Convert.ToInt32(dr["A005_cd_tp_cons"]);
                    if (dr["A052_cd_usuario"] != DBNull.Value && dr["A052_cd_usuario"] != DBNull.Value && dr["A052_cd_usuario"].ToString() != "")
                        TabCons.CodUsuario = Convert.ToInt32(dr["A052_cd_usuario"]);
                    if (dr["A004_cd_escr"] != DBNull.Value && dr["A004_cd_escr"] != DBNull.Value && dr["A004_cd_escr"].ToString() != "")
                        TabCons.CodEscrSebrae = Convert.ToInt32(dr["A004_cd_escr"]);

                    TabCons.codTabCons.CodTabCons = Convert.ToString(dr["A118_tab_cons"]);
                    TabCons.numSeq = Convert.ToInt32(dr["A110_num_seq"]);

                    if (dr["A110_dsc_it_cons"] != DBNull.Value && dr["A110_dsc_it_cons"] != DBNull.Value && dr["A110_dsc_it_cons"].ToString() != "")
                        TabCons.dscItCons = Convert.ToString(dr["A110_dsc_it_cons"]);
                    if (dr["A110_cd_it_cons"] != DBNull.Value && dr["A110_cd_it_cons"] != DBNull.Value && dr["A110_cd_it_cons"].ToString() != "")
                        TabCons.codItCons = Convert.ToString(dr["A110_cd_it_cons"]);

                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        TabCons.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                TabCons = new TabCons();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return TabCons;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="numAtend">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>TabCons</returns>
        public static TabCons GetDataRow(int numAtend, int codTipoConsAt, int codUsuario, int codEscrSebrae, string codTabCons, int numSeq, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(numAtend, codTipoConsAt, codUsuario, codEscrSebrae, codTabCons, numSeq, trans);
            TabCons TabCons = new TabCons();
            try
            {
                if (dr.Read())
                {
                    TabCons.numAtend = new ConsultaAtend(Convert.ToInt32(dr["A001_num_atend"]));
                    if (dr["A005_cd_tp_cons"] != DBNull.Value && dr["A005_cd_tp_cons"] != DBNull.Value && dr["A005_cd_tp_cons"].ToString() != "")
                        TabCons.codTipoConsAt.CodTipoConsAt.CodTipoConsAt = Convert.ToInt32(dr["A005_cd_tp_cons"]);
                    if (dr["A052_cd_usuario"] != DBNull.Value && dr["A052_cd_usuario"] != DBNull.Value && dr["A052_cd_usuario"].ToString() != "")
                        TabCons.CodUsuario = Convert.ToInt32(dr["A052_cd_usuario"]);
                    if (dr["A004_cd_escr"] != DBNull.Value && dr["A004_cd_escr"] != DBNull.Value && dr["A004_cd_escr"].ToString() != "")
                        TabCons.CodEscrSebrae = Convert.ToInt32(dr["A004_cd_escr"]);

                    TabCons.codTabCons.CodTabCons = Convert.ToString(dr["A118_tab_cons"]);
                    TabCons.numSeq = Convert.ToInt32(dr["A110_num_seq"]);

                    if (dr["A110_dsc_it_cons"] != DBNull.Value && dr["A110_dsc_it_cons"] != DBNull.Value && dr["A110_dsc_it_cons"].ToString() != "")
                        TabCons.dscItCons = Convert.ToString(dr["A110_dsc_it_cons"]);
                    if (dr["A110_cd_it_cons"] != DBNull.Value && dr["A110_cd_it_cons"] != DBNull.Value && dr["A110_cd_it_cons"].ToString() != "")
                        TabCons.codItCons = Convert.ToString(dr["A110_cd_it_cons"]);

                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        TabCons.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                TabCons = new TabCons();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return TabCons;
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
        public static Context.Paginacao LoadDataPaginacao(string Where, int PaginaCorrente, int TamanhoPagina, string ExpressaoOrdenacao)
        {
            Context.Paginacao paginacao = new Context.Paginacao();

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


            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTPAG, parms);


            paginacao.DataReader = dr;
            //  traz o total de registros de retorno direto dos parametros do ExecuteReader---
            paginacao.TotalRegistros = Convert.ToInt32(parms[5].Value);
            return paginacao;
        }
        #endregion


        #endregion

    }
}