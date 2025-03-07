using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
 
using System.Data;

//-- Classe Classes Sebrae
//-- Data : 14/05/2007 
//-- Autor :  Daniel

namespace Classes
{
    public class SubgrupoRP // T070_SUB_GRUPO_RP 
    {
        #region Atributos
        private int codSubgrupoRP;
        private GrupoRP codGrupoRP;
        private string dscSubgrupoRP;
        private string logUsuario;
        #endregion

        #region Propriedades
        public int CodSubgrupoRP
        {
            get { return codSubgrupoRP; }
            set { codSubgrupoRP = value; }
        }
        public GrupoRP CodgrupoRP
        {
            get { return codGrupoRP; }
            set { codGrupoRP = value; }
        }
        public string DscSubgrupoRP
        {
            get { return dscSubgrupoRP; }
            set { dscSubgrupoRP = value; }
        }
        public string LogSubgrupoRP
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }
        #endregion

        #region Construtores
        public SubgrupoRP()
            : this(-1)
        { }
        public SubgrupoRP(int codSubgrupoRP)
        {
            this.codSubgrupoRP = codSubgrupoRP;
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "SubgrupoRP.SubgrupoRPInc";
        private const string SPUPDATE = "SubgrupoRP.SubgrupoRPAlt";
        private const string SPDELETE = "SubgrupoRP.SubgrupoRPDel";
        private const string SPSELECTID = "SubgrupoRP.SubgrupoRPSelId";
        private const string SPSELECTPAG = "SubgrupoRP.SubgrupoRPSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMCODSUBGRUPORP = "codSubgrupoRP";
        private const string PARMCODGRUPORP = "codGrupoRP";
        private const string PARMCURSOR = "curSubgrupoRP";
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
                    /*0*/ new OracleParameter(PARMCODSUBGRUPORP, OracleType.Int32, 4, ParameterDirection.InputOutput.ToString()) ,
                    /*0*/ new OracleParameter(PARMCODGRUPORP, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*2*/ new OracleParameter( "dscSubgrupoRP", OracleType.VarChar),
                    /*3*/ new OracleParameter( "logUsuario", OracleType.VarChar)
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
            parms[0].Value = this.codSubgrupoRP;
            parms[1].Value = this.codGrupoRP;
            parms[2].Value = this.dscSubgrupoRP;
            parms[3].Value = this.logUsuario;
            if (this.codSubgrupoRP < 0)
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

            using (OracleConnection conn = new OracleConnection(DataBase.CONN_STRING))
            {
                conn.Open();
                using (OracleTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        OracleCommand cmd = DataBase.ExecuteNonQueryCmd(trans, CommandType.StoredProcedure, SPINSERT, parms);
                        //Obtendo a chave de identifica��o do registro inserido.
                        codSubgrupoRP = Convert.ToInt32(cmd.Parameters[PARMCODSUBGRUPORP].Value);
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
                codSubgrupoRP = Convert.ToInt32(cmd.Parameters[PARMCODSUBGRUPORP].Value);
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
        /// <param name="codSubgrupoRP">C�digo do Registro</param>
        public static void Delete(int codSubgrupoRP, int codGrupoRP)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMCODSUBGRUPORP, OracleType.Int32, 4),
                new OracleParameter(PARMCODGRUPORP, OracleType.Int32, 4)
            };
            parms[0].Value = codSubgrupoRP;
            parms[1].Value = codGrupoRP;

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
        /// <param name="codSubgrupoRP">C�digo do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(int codSubgrupoRP, int codGrupoRP, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMCODSUBGRUPORP, OracleType.Int32, 4),
                new OracleParameter(PARMCODGRUPORP, OracleType.Int32, 4)
            };
                parms[0].Value = codSubgrupoRP;
                parms[1].Value = codGrupoRP;

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
        /// <param name="codSubgrupoRP">C�digo do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Ap�s a utiliza��o do LoadDataDr n�o esquecer de fechar a conex�o: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codSubgrupoRP, int codGrupoRP)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCODSUBGRUPORP, OracleType.Int32, 4), 
                    new OracleParameter(PARMCODGRUPORP, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codSubgrupoRP;
            param[1].Value = codGrupoRP;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transa��o
        /// </summary>
        /// <param name="codSubgrupoRP">C�digo do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Ap�s a utiliza��o do LoadDataDr n�o esquecer de fechar a conex�o: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codSubgrupoRP, int codGrupoRP, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { new OracleParameter(PARMCODSUBGRUPORP, OracleType.Int32, 4),
                                                                  new OracleParameter(PARMCURSOR, OracleType.Cursor)
};
            param[0].Value = codSubgrupoRP;
            param[1].Value = codGrupoRP;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="codSubgrupoRP">C�digo do Registro</param>
        /// <returns>SubgrupoRP</returns>
        public static SubgrupoRP GetDataRow(int codSubgrupoRP, int codGrupoRP)
        {
            OracleDataReader dr = LoadDataDr(codSubgrupoRP, codGrupoRP);
            SubgrupoRP subgrupoRP = new SubgrupoRP();
            try
            {
                if (dr.Read())
                {
                    subgrupoRP.codSubgrupoRP = Convert.ToInt32(dr["A070_cd_sgr_rp"]);
                    subgrupoRP.codGrupoRP = new GrupoRP(Convert.ToInt32(dr["A069_cd_gru_rp"]));
                    subgrupoRP.dscSubgrupoRP = Convert.ToString(dr["A070_dsc_sgr_rp"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                    subgrupoRP.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                subgrupoRP = new SubgrupoRP();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return subgrupoRP;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transa��o
        /// </summary>
        /// <param name="codSubgrupoRP">C�digo do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>SubgrupoRP</returns>
        public static SubgrupoRP GetDataRow(int codSubgrupoRP, int codGrupoRP, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codSubgrupoRP, codGrupoRP, trans);
            SubgrupoRP subgrupoRP = new SubgrupoRP();
            try
            {
                if (dr.Read())
                {
                    subgrupoRP.codSubgrupoRP = Convert.ToInt32(dr["A070_cd_sgr_rp"]);
                    subgrupoRP.codGrupoRP = new GrupoRP(Convert.ToInt32(dr["A069_cd_gru_rp"]));
                    subgrupoRP.dscSubgrupoRP = Convert.ToString(dr["A070_dsc_sgr_rp"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                    subgrupoRP.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                subgrupoRP = new SubgrupoRP();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return subgrupoRP;
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


            paginacao.DataReader = dr;
            //  traz o total de registros de retorno direto dos parametros do ExecuteReader---
            paginacao.TotalRegistros = Convert.ToInt32(parms[5].Value);
            return paginacao;
        }
        #endregion


        #endregion

    }
}
