using System;
using System.Data.OracleClient; 
using System.Data;

namespace Database.Entities
{
    /// <summary>
    /// Classe de Mapeamento com a Entidade de Banco de Dados - T214_INDICADORES_CLIENTE
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
    public class IndicadoresCliente
    {
        #region Atributos
        private int codIndicadoresCliente;
        private int indAborEmail;
        private int indAborFone;
        private int indAborSms;
        private int indAborMala;
        private string      logusuario;        
        #endregion

        #region Propriedades

        public int IndAborEmail
        {
            get { return indAborEmail; }
            set { indAborEmail = value; }
        }
        public int IndAborMala
        {
            get { return indAborMala; }
            set { indAborMala = value; }
        }
        public int IndAborSms
        {
            get { return indAborSms; }
            set { indAborSms = value; }
        }
        public int IndAborFone
        {
            get { return indAborFone; }
            set { indAborFone = value; }
        }
        public int CodIndicadoresCliente
        {
            get { return codIndicadoresCliente; }
            set { codIndicadoresCliente = value; }
        }  
        public string Logusuario
        {
            get { return logusuario; }
            set { logusuario = value; }
        }
        #endregion

        #region Construtores
        public IndicadoresCliente()
            : this(-1)
        { }
        public IndicadoresCliente(int codIndicadoresCliente)
        {
            this.codIndicadoresCliente = codIndicadoresCliente;
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "IndicadoresCliente.IndicadoresClienteInc";
        private const string SPUPDATE = "IndicadoresCliente.IndicadoresClienteAlt";
        private const string SPDELETE = "IndicadoresCliente.IndicadoresClienteDel";
        private const string SPSELECTID = "IndicadoresCliente.IndicadoresClienteSelId";
        private const string SPSELECTPAG = "IndicadoresCliente.IndicadoresClienteSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMCODIGO = "codIndicadoresCliente";
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
                    /*0*/ new OracleParameter(PARMCODIGO, OracleType.Int32, 4, ParameterDirection.InputOutput.ToString()) ,
                    /*1*/ new OracleParameter( "indAborEmail", OracleType.Int32),
                    /*1*/ new OracleParameter( "indAborFone", OracleType.Int32),
                    /*1*/ new OracleParameter( "indAborSms", OracleType.Int32),
                    /*1*/ new OracleParameter( "indAborMala", OracleType.Int32),
                    /*1*/ new OracleParameter( "logUsuario", OracleType.VarChar)
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
            parms[0].Value = this.codIndicadoresCliente;
            parms[1].Value = this.indAborEmail;
            parms[2].Value = this.indAborFone;
            parms[3].Value = this.indAborSms;
            parms[4].Value = this.indAborMala;
            parms[5].Value = "";
            if (this.logusuario != null)
            { parms[5].Value = this.logusuario; }

            if (this.codIndicadoresCliente < 0)
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
        /// Insert com tratamento de transa��o
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
                        //Obtendo a chave de identifica��o do registro inserido.
                        codIndicadoresCliente = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
                OracleCommand cmd = Context.DataBase.ExecuteNonQueryCmd(trans, CommandType.StoredProcedure, SPINSERT, parms);
                //Obtendo a chave de identifica��o do registro inserido.
                codIndicadoresCliente = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
        /// Delete com tratamento de transa��o
        /// </summary>
        /// <param name="codigo">C�digo do Registro</param>
        public static void Delete(int codigo)
        {
            OracleParameter[] parms = new OracleParameter[] { new OracleParameter(PARMCODIGO, OracleType.Int32, 4) };
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
        /// Delete sem tratamento de transa��o
        /// </summary>
        /// <param name="codigo">C�digo do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(int codigo, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { new OracleParameter(PARMCODIGO, OracleType.Int32, 4) };
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
        /// <param name="codigo">C�digo do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Ap�s a utiliza��o do LoadDataDr n�o esquecer de fechar a conex�o: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codigo)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCODIGO, OracleType.Int32, 4), 
                    new OracleParameter("curIndicadoresCliente", OracleType.Cursor)
                };

            param[0].Value = codigo;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
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
            OracleParameter[] param = new OracleParameter[] { new OracleParameter(PARMCODIGO, OracleType.Int32, 4),
                                                                  new OracleParameter("curIndicadoresCliente", OracleType.Cursor)
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
        /// <param name="codigo">C�digo do Registro</param>
        /// <returns>IndicadoresCliente</returns>
        public static IndicadoresCliente GetDataRow(int codigo)
        {
            OracleDataReader dr = LoadDataDr(codigo);
            IndicadoresCliente IndicadoresCliente = new IndicadoresCliente();
            try
            {
                if (dr.Read())
                {
                    IndicadoresCliente.codIndicadoresCliente = Convert.ToInt32(dr["A012_Cd_Cli"]);
                    if (dr["A214_ind_AborEmail"] != DBNull.Value && dr["A214_ind_AborEmail"] != DBNull.Value && dr["A214_ind_AborEmail"].ToString() != "")
                        IndicadoresCliente.indAborEmail = Convert.ToInt32(dr["A214_ind_AborEmail"]);
                    if (dr["A214_ind_AborFone"] != DBNull.Value && dr["A214_ind_AborFone"] != DBNull.Value && dr["A214_ind_AborFone"].ToString() != "")
                        IndicadoresCliente.indAborFone = Convert.ToInt32(dr["A214_ind_AborFone"]);
                    if (dr["A214_ind_AborSMS"] != DBNull.Value && dr["A214_ind_AborSMS"] != DBNull.Value && dr["A214_ind_AborSMS"].ToString() != "")
                        IndicadoresCliente.indAborSms = Convert.ToInt32(dr["A214_ind_AborSMS"]);
                    if (dr["A214_ind_AborMala"] != DBNull.Value && dr["A214_ind_AborMala"] != DBNull.Value && dr["A214_ind_AborMala"].ToString() != "")
                        IndicadoresCliente.indAborMala = Convert.ToInt32(dr["A214_ind_AborMala"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                    IndicadoresCliente.logusuario = Convert.ToString(dr["Usu_Inc_Alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                IndicadoresCliente = new IndicadoresCliente();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return IndicadoresCliente;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transa��o
        /// </summary>
        /// <param name="codigo">C�digo do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>IndicadoresCliente</returns>
        public static IndicadoresCliente GetDataRow(int codigo, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codigo, trans);
            IndicadoresCliente IndicadoresCliente = new IndicadoresCliente();
            try
            {
                if (dr.Read())
                {
                    IndicadoresCliente.codIndicadoresCliente = Convert.ToInt32(dr["A012_Cd_Cli"]);
                    if (dr["A214_ind_AborEmail"] != DBNull.Value && dr["A214_ind_AborEmail"] != DBNull.Value && dr["A214_ind_AborEmail"].ToString() != "")
                        IndicadoresCliente.indAborEmail = Convert.ToInt32(dr["A214_ind_AborEmail"]);
                    if (dr["A214_ind_AborFone"] != DBNull.Value && dr["A214_ind_AborFone"] != DBNull.Value && dr["A214_ind_AborFone"].ToString() != "")
                        IndicadoresCliente.indAborFone = Convert.ToInt32(dr["A214_ind_AborFone"]);
                    if (dr["A214_ind_AborSMS"] != DBNull.Value && dr["A214_ind_AborSMS"] != DBNull.Value && dr["A214_ind_AborSMS"].ToString() != "")
                        IndicadoresCliente.indAborSms = Convert.ToInt32(dr["A214_ind_AborSMS"]);
                    if (dr["A214_ind_AborMala"] != DBNull.Value && dr["A214_ind_AborMala"] != DBNull.Value && dr["A214_ind_AborMala"].ToString() != "")
                        IndicadoresCliente.indAborMala = Convert.ToInt32(dr["A214_ind_AborMala"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        IndicadoresCliente.logusuario = Convert.ToString(dr["Usu_Inc_Alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                IndicadoresCliente = new IndicadoresCliente();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return IndicadoresCliente;
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
        public static Context.Paginacao LoadDataPaginacao(string Where, int PaginaCorrente, int TamanhoPagina, string ExpressaoOrdenacao)
        {
            Context.Paginacao paginacao = new Context.Paginacao();

            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter("sWhere", OracleType.VarChar,5000),
			    new OracleParameter("CurrentPage", OracleType.Int32),
			    new OracleParameter("PageSize", OracleType.Int32),
			    new OracleParameter("SortExpression", OracleType.VarChar,50),
                new OracleParameter("curIndicadoresCliente", OracleType.Cursor),
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