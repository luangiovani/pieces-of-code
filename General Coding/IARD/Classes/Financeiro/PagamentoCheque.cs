using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
using System.Data;

//-- Classe Classes Sebrae
//-- Data : 03/07/2007 
//-- Autor :  Honorato


namespace Classes
{
    public class PagamentoCheque  // T135_pagto_cheque
    {
        #region Atributos
        private int numSeqPagto;
        private string numCheque;
        private DateTime dtaCheque;
        private decimal vlrCheque;
        private Banco codBanco;
        private string sitCheque;
        private string nomEmitente;
        private string logUsuario;
        #endregion

        #region Propriedades
        public int NumSeqPagto
        {
            get { return numSeqPagto; }
            set { numSeqPagto = value; }
        }
        public string NumCheque
        {
            get { return numCheque; }
            set { numCheque = value; }
        }
        public DateTime DtaCheque
        {
            get { return dtaCheque; }
            set { dtaCheque = value; }
        }
        public decimal VlrCheque
        {
            get { return vlrCheque; }
            set { vlrCheque = value; }
        }
        public string NomEmitente
        {
            get { return nomEmitente; }
            set { nomEmitente = value; }
        }
        public string SitCheque
        {
            get { return sitCheque; }
            set { sitCheque = value; }
        }
        public Banco CodBanco
        {
            get { return codBanco; }
            set { codBanco = value; }
        }
        public string LogUsuario
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }
        #endregion

        #region Construtores
        public PagamentoCheque()
            : this(-1)
        { }

        public PagamentoCheque(int numSeqPagto)
        {
            this.numSeqPagto = numSeqPagto;
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "PagamentoCheque.PagamentoChequeInc";
        private const string SPUPDATE = "PagamentoCheque.PagamentoChequeAlt";
        private const string SPDELETE = "PagamentoCheque.PagamentoChequeDel";
        private const string SPSELECTID = "PagamentoCheque.PagamentoChequeSelId";
        private const string SPSELECTPAG = "PagamentoCheque.PagamentoChequeSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMCODIGO = "numSeqPagto";
        private const string PARMCURSOR = "curPagamentoCheque";
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
                /*0*/ new OracleParameter(PARMCODIGO, OracleType.Int32),
                      new OracleParameter( "numCheque", OracleType.VarChar),
                      new OracleParameter( "dtaCheque", OracleType.DateTime),
                /*3*/ new OracleParameter( "vlrCheque", OracleType.Float),		
                      new OracleParameter( "codBanco", OracleType.VarChar),
                      new OracleParameter( "sitCheque", OracleType.VarChar),
                /*6*/ new OracleParameter( "nomEmitente", OracleType.VarChar),		
                /*7*/ new OracleParameter( "logUsuario", OracleType.VarChar)
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
            parms[0].Value = this.numSeqPagto;
            parms[1].Value = "";
            if (this.numCheque != null)
            { parms[1].Value = this.numCheque; }
            parms[2].Value = this.dtaCheque;
            parms[3].Value = this.vlrCheque;
            parms[4].Value = "";
            parms[5].Value = "";
            parms[6].Value = "";
            if (this.codBanco.CodBanco != null)
            { parms[4].Value = this.codBanco.CodBanco; }
            if (this.sitCheque != null)
            { parms[5].Value = this.sitCheque; }
            if (this.nomEmitente != null)
            { parms[6].Value = this.nomEmitente; }
            parms[7].Value = this.logUsuario.ToUpper();

                parms[0].Direction = ParameterDirection.Input;
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
                        numSeqPagto = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
                numSeqPagto = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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

                new OracleParameter(PARMCODIGO, OracleType.Int32, 2), 
                new OracleParameter(PARMCURSOR, OracleType.Cursor)
            };

            param[0].Value = codigo;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
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

            OracleDataReader dr = DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <returns>PagamentoCheque</returns>
        public static PagamentoCheque GetDataRow(int codigo)
        {
            OracleDataReader dr = LoadDataDr(codigo);
            PagamentoCheque PagamentoCheque = new PagamentoCheque();
            try
            {
                if (dr.Read())
                {
                    PagamentoCheque.numSeqPagto = Convert.ToInt32(dr["A124_num_seq_pagto"]);
                    PagamentoCheque.numCheque = Convert.ToString(dr["A135_num_cheque"]);
                    PagamentoCheque.dtaCheque = Convert.ToDateTime(dr["A135_dt_cheque"]);
                    PagamentoCheque.vlrCheque = Convert.ToInt32(dr["A136_valor"]);
                    if (dr["A315_cd_banco"] != DBNull.Value && dr["A315_cd_banco"] != DBNull.Value && dr["A315_cd_banco"].ToString() != "")
                        PagamentoCheque.codBanco = new Banco(Convert.ToString(dr["A315_cd_banco"]));
                    if (dr["A135_status_cheque"] != DBNull.Value && dr["A135_status_cheque"] != DBNull.Value && dr["A135_status_cheque"].ToString() != "")
                        PagamentoCheque.sitCheque = Convert.ToString(dr["A135_status_cheque"]);
                    if (dr["A135_nm_emitente"] != DBNull.Value && dr["A135_nm_emitente"] != DBNull.Value && dr["A135_nm_emitente"].ToString() != "")
                        PagamentoCheque.nomEmitente = Convert.ToString(dr["A135_nm_emitente"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        PagamentoCheque.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                PagamentoCheque = new PagamentoCheque();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return PagamentoCheque;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>PagamentoCheque</returns>
        public static PagamentoCheque GetDataRow(int codigo, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codigo, trans);
            PagamentoCheque PagamentoCheque = new PagamentoCheque();
            try
            {
                if (dr.Read())
                {
                    PagamentoCheque.numSeqPagto = Convert.ToInt32(dr["A124_num_seq_pagto"]);
                    PagamentoCheque.numCheque = Convert.ToString(dr["A135_num_cheque"]);
                    PagamentoCheque.dtaCheque = Convert.ToDateTime(dr["A135_dt_cheque"]);
                    PagamentoCheque.vlrCheque = Convert.ToInt32(dr["A136_valor"]);
                    if (dr["A315_cd_banco"] != DBNull.Value && dr["A315_cd_banco"] != DBNull.Value && dr["A315_cd_banco"].ToString() != "")
                        PagamentoCheque.codBanco = new Banco(Convert.ToString(dr["A315_cd_banco"]));
                    if (dr["A135_status_cheque"] != DBNull.Value && dr["A135_status_cheque"] != DBNull.Value && dr["A135_status_cheque"].ToString() != "")
                        PagamentoCheque.sitCheque = Convert.ToString(dr["A135_status_cheque"]);
                    if (dr["A135_nm_emitente"] != DBNull.Value && dr["A135_nm_emitente"] != DBNull.Value && dr["A135_nm_emitente"].ToString() != "")
                        PagamentoCheque.nomEmitente = Convert.ToString(dr["A135_nm_emitente"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        PagamentoCheque.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                PagamentoCheque = new PagamentoCheque();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return PagamentoCheque;
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
