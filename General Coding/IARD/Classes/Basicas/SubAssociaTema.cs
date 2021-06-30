using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;

using System.Data;

//-- Classe Classes Sebrae
//-- Data : 11/10/2007 
//-- Autor :  Honorato

namespace Classes
{
    public class SubAssociaTema // T109_SUBT_TEMA
    {
        #region Atributos
        private SubTema codSubTema;
        private Tema codTema;
        private int numOrdApresentacao;
        private int codTpApresentacao;
        private string logUsuario;
        #endregion

        #region Propriedades

        public int CodTpApresentacao
        {
            get { return codTpApresentacao; }
            set { codTpApresentacao = value; }
        }

        public int NumOrdApresentacao
        {
            get { return numOrdApresentacao; }
            set { numOrdApresentacao = value; }
        }

        public Tema CodTema
        {
            get { return codTema; }
            set { codTema = value; }
        }

        public SubTema CodSubTema
        {
            get { return codSubTema; }
            set { codSubTema = value; }
        }
        public string LogUsuario
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }
        #endregion

        #region Construtores
        public SubAssociaTema()
            : this(-1)
        { }
        public SubAssociaTema(int codSubTema)
        {
            this.codSubTema.CodSubTema = codSubTema;
            this.codTema = new Tema();
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "SubAssociaTema.SubAssociaTemaInc";
        private const string SPUPDATE = "SubAssociaTema.SubAssociaTemaAlt";
        private const string SPDELETE = "SubAssociaTema.SubAssociaTemaDel";
        private const string SPSELECTID = "SubAssociaTema.SubAssociaTemaSelId";
        private const string SPSELECTPAG = "SubAssociaTema.SubAssociaTemaSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMcodSubTema = "codSubTema";
        private const string PARMcodTema = "codTema";
        private const string PARMCURSOR = "curSubAssociaTema";
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
                    /*0*/ new OracleParameter(PARMcodSubTema, OracleType.Int32, 4, ParameterDirection.InputOutput.ToString()) ,
                    /*1*/ new OracleParameter(PARMcodTema, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*2*/ new OracleParameter( "numOrdApresentacao", OracleType.Int32),
                    /*3*/ new OracleParameter( "codTpApresentacao", OracleType.Int32),
                    /*5*/ new OracleParameter( "logUsuario", OracleType.VarChar)
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
            parms[0].Value = this.codSubTema.CodSubTema;
            parms[1].Value = this.codTema.CodTema;
            parms[2].Value = this.numOrdApresentacao;
            parms[3].Value = this.codTpApresentacao;
            parms[4].Value = this.logUsuario;
            if (this.codSubTema.CodSubTema < 0)
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
                        codSubTema.CodSubTema = Convert.ToInt32(cmd.Parameters[PARMcodSubTema].Value);
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
                codSubTema.CodSubTema = Convert.ToInt32(cmd.Parameters[PARMcodSubTema].Value);
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
        /// <param name="codSubTema">Código do Registro</param>
        public static void Delete(int codSubTema, int codTema)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMcodSubTema, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodTema, OracleType.Int32, 4)
            };
            parms[0].Value = codSubTema;
            parms[1].Value = codTema;
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
        /// <param name="codSubTema">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(int codSubTema, int codTema, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMcodSubTema, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodTema, OracleType.Int32, 4)
            };
                parms[0].Value = codSubTema;
                parms[1].Value = codTema;
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
        /// <param name="codSubTema">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codSubTema, int codTema)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodSubTema, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodTema, OracleType.Int32, 4),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codSubTema;
            param[1].Value = codTema;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="codSubTema">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codSubTema, int codTema, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodSubTema, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodTema, OracleType.Int32, 4),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codSubTema;
            param[1].Value = codTema;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="codSubTema">Código do Registro</param>
        /// <returns>SubAssociaTema</returns>
        public static SubAssociaTema GetDataRow(int codSubTema, int codTema)
        {
            OracleDataReader dr = LoadDataDr(codSubTema, codTema);
            SubAssociaTema SubAssociaTema = new SubAssociaTema();
            try
            {
                if (dr.Read())
                {
                    SubAssociaTema.codSubTema = new SubTema(Convert.ToInt32(dr["A050_cd_subt"]));
                    SubAssociaTema.codTema = new Tema(Convert.ToInt32(dr["A053_cd_tema"]));
                    SubAssociaTema.numOrdApresentacao = Convert.ToInt32(dr["A109_ord_apres"]);
                    SubAssociaTema.codTpApresentacao = Convert.ToInt32(dr["A109_tp_apres"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        SubAssociaTema.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                SubAssociaTema = new SubAssociaTema();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return SubAssociaTema;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codSubTema">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>SubAssociaTema</returns>
        public static SubAssociaTema GetDataRow(int codSubTema, int codTema, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codSubTema, codTema, trans);
            SubAssociaTema SubAssociaTema = new SubAssociaTema();
            try
            {
                if (dr.Read())
                {
                    SubAssociaTema.codSubTema = new SubTema(Convert.ToInt32(dr["A050_cd_subt"]));
                    SubAssociaTema.codTema = new Tema(Convert.ToInt32(dr["A053_cd_tema"]));
                    SubAssociaTema.numOrdApresentacao = Convert.ToInt32(dr["A109_ord_apres"]);
                    SubAssociaTema.codTpApresentacao = Convert.ToInt32(dr["A109_tp_apres"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        SubAssociaTema.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                SubAssociaTema = new SubAssociaTema();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return SubAssociaTema;
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
        #endregion


        #endregion

    }
}