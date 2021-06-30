using System;
using System.Data.OracleClient;
using System.Data;

namespace Database.Entities
{
    /// <summary>
    /// Classe de Mapeamento com a Entidade de Banco de Dados - T066_TIPO_PROD
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
    public class TipoProd  
    {
        #region Atributos
        private string tipProd;
        private string dscTipProd;
        private int? indDadAd;
        private int? indExcFranquia;
        private int? indExcCad;
        private string logUsuario;

        #endregion

        #region Propriedades
        public string DscTipProd
        {
          get { return dscTipProd; }
          set { dscTipProd = value; }
        }
        public int? IndDadAd
        {
          get { return indDadAd; }
          set { indDadAd = value; }
        }
        public int? IndExcFranquia
        {
          get { return indExcFranquia; }
          set { indExcFranquia = value; }
        }
        public int? IndExcCad
        {
          get { return indExcCad; }
          set { indExcCad = value; }
        }
        public string TipProd
        {
          get { return tipProd; }
          set { tipProd = value; }
        }
        public string LogUsuario
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }

        #endregion

        #region Construtores
        public TipoProd()
            : this("")
        { }
        public TipoProd(string tipProd)
        {
            this.tipProd = tipProd;
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "TipoProd.TipoProdInc";
        private const string SPUPDATE = "TipoProd.TipoProdAlt";
        private const string SPDELETE = "TipoProd.TipoProdDel";
        private const string SPSELECTID = "TipoProd.TipoProdSelId";
        private const string SPSELECTPAG = "TipoProd.TipoProdSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMCODIGO = "tipProd";
        private const string PARMCURSOR = "curTipoProd";
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
                /*0*/ new OracleParameter(PARMCODIGO, OracleType.Int32, 2, ParameterDirection.InputOutput.ToString()) ,
                      new OracleParameter( "dscTipProd", OracleType.VarChar),
                      new OracleParameter( "indDadAd", OracleType.Int32),
                      new OracleParameter( "indExcFranquia", OracleType.Int32),
                      new OracleParameter( "indExcCad", OracleType.Int32),
                /*22*/ new OracleParameter( "logUsuario", OracleType.VarChar)
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
            parms[0].Value = this.tipProd;
            parms[1].Value = this.dscTipProd.ToUpper();
            parms[2].Value = DBNull.Value;
            parms[3].Value = DBNull.Value;
            parms[4].Value = DBNull.Value;
            if (this.indDadAd != null){
                parms[2].Value = this.indDadAd;}
            if (this.indExcFranquia != null){
                parms[3].Value = this.indExcFranquia;}
            if (this.indExcCad != null){
                parms[4].Value = this.indExcCad;}

            parms[5].Value = this.logUsuario.ToUpper();

            //if (this.tipProd < 0)
            //{
            //    parms[0].Direction = ParameterDirection.Output;
            //}
            //else
            //{
            //    parms[0].Direction = ParameterDirection.Input;
            //}
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
                        tipProd = Convert.ToString(cmd.Parameters[PARMCODIGO].Value);
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
                tipProd = Convert.ToString(cmd.Parameters[PARMCODIGO].Value);
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
        /// <param name="codigo">Código do Registro</param>
        public static void Delete(int codigo)
        {
            OracleParameter[] parms = new OracleParameter[] { new OracleParameter(PARMCODIGO, OracleType.Int32, 2) };
            parms[0].Value = codigo;
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
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(int codigo, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { new OracleParameter(PARMCODIGO, OracleType.Int32, 2) };
                parms[0].Value = codigo;
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
        /// <param name="codigo">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codigo)
        {
            OracleParameter[] param = new OracleParameter[] { 

                new OracleParameter(PARMCODIGO, OracleType.Int32, 2), 
                new OracleParameter(PARMCURSOR, OracleType.Cursor)
            };

            param[0].Value = codigo;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
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
            OracleParameter[] param = new OracleParameter[] { new OracleParameter(PARMCODIGO, OracleType.Int32, 2),
                                                              new OracleParameter(PARMCURSOR, OracleType.Cursor)
};
            param[0].Value = codigo;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <returns>TipoProd</returns>
        public static TipoProd GetDataRow(int codigo)
        {
            OracleDataReader dr = LoadDataDr(codigo);
            TipoProd TipoProd = new TipoProd();
            try
            {
                if (dr.Read())
                {
                    TipoProd.tipProd = Convert.ToString(dr["A066_tp_prod"]);
                    TipoProd.dscTipProd = Convert.ToString(dr["A066_dsc_tp_prod"]);
                    if (dr["A066_ind_dad_ad"] != DBNull.Value && dr["A066_ind_dad_ad"] != DBNull.Value && dr["A066_ind_dad_ad"].ToString() != "")
                        TipoProd.indDadAd = Convert.ToInt32(dr["A066_ind_dad_ad"]);
                    if (dr["A066_ind_exc_franquia"] != DBNull.Value && dr["A066_ind_exc_franquia"] != DBNull.Value && dr["A066_ind_exc_franquia"].ToString() != "")
                        TipoProd.indExcFranquia = Convert.ToInt32(dr["A066_ind_exc_franquia"]);
                    if (dr["a066_ind_exc_cad"] != DBNull.Value && dr["a066_ind_exc_cad"] != DBNull.Value && dr["a066_ind_exc_cad"].ToString() != "")
                        TipoProd.indExcCad = Convert.ToInt32(dr["a066_ind_exc_cad"]);
					
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        TipoProd.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                TipoProd = new TipoProd();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return TipoProd;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>TipoProd</returns>
        public static TipoProd GetDataRow(int codigo, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codigo, trans);
            TipoProd TipoProd = new TipoProd();
            try
            {
                if (dr.Read())
                {
                    TipoProd.tipProd = Convert.ToString(dr["A066_tp_prod"]);
                    TipoProd.dscTipProd = Convert.ToString(dr["A066_dsc_tp_prod"]);
                    if (dr["A066_ind_dad_ad"] != DBNull.Value && dr["A066_ind_dad_ad"] != DBNull.Value && dr["A066_ind_dad_ad"].ToString() != "")
                        TipoProd.indDadAd = Convert.ToInt32(dr["A066_ind_dad_ad"]);
                    if (dr["A066_ind_exc_franquia"] != DBNull.Value && dr["A066_ind_exc_franquia"] != DBNull.Value && dr["A066_ind_exc_franquia"].ToString() != "")
                        TipoProd.indExcFranquia = Convert.ToInt32(dr["A066_ind_exc_franquia"]);
                    if (dr["a066_ind_exc_cad"] != DBNull.Value && dr["a066_ind_exc_cad"] != DBNull.Value && dr["a066_ind_exc_cad"].ToString() != "")
                        TipoProd.indExcCad = Convert.ToInt32(dr["a066_ind_exc_cad"]);

                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        TipoProd.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                TipoProd = new TipoProd();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return TipoProd;
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