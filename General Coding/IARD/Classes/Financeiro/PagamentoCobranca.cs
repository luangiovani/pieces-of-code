using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
using System.Data;

//-- Classe Classes Sebrae
//-- Data : 03/07/2007 
//-- Autor :  Honorato


/*
A124_num_seq_pagto, A163_num_bloq, A163_dt_vecto, A162_cd_remessa,
A162_cd_retorno, A163_vlr_parcela, A163_nm_sacado, A163_dt_pagto,
A163_valor_pago, A163_num_boleto, usu_inc_alt

numSeqPagto, numBloq, dtaVencto, codRemessa,
codRetorno, vlrParcela, nomSacado, dtaPagto,
vlrPgtoCobranca, numBoleto, logUsuario
*/

namespace Classes
{
    public class PagamentoCobranca  // T163_Pagto_cobranca 
    {
        #region Atributos
        private int numSeqPagto;
        private int numBloq;
        private DateTime dtaVencto;
        private int? codRemessa;
        private int? codRetorno;
        private decimal vlrParcela;
        private string nomSacado;
        private DateTime? dtaPagto;
        private decimal? vlrPgtoCobranca;
        private int? numBoleto;
        private string logUsuario;
        #endregion

        #region Propriedades
        public int? NumBoleto
        {
            get { return numBoleto; }
            set { numBoleto = value; }
        }
        public decimal? VlrPgtoCobranca
        {
            get { return vlrPgtoCobranca; }
            set { vlrPgtoCobranca = value; }
        }
        
        public DateTime? DtaPagto
        {
            get { return dtaPagto; }
            set { dtaPagto = value; }
        }
        public string NomSacado
        {
            get { return nomSacado; }
            set { nomSacado = value; }
        }
        public decimal VlrParcela
        {
            get { return vlrParcela; }
            set { vlrParcela = value; }
        }
        public int? CodRetorno
        {
            get { return codRetorno; }
            set { codRetorno = value; }
        }
        public int? CodRemessa
        {
            get { return codRemessa; }
            set { codRemessa = value; }
        }
        public DateTime DtaVencto
        {
            get { return dtaVencto; }
            set { dtaVencto = value; }
        }
        public int NumBloq
        {
            get { return numBloq; }
            set { numBloq = value; }
        }
        public int NumSeqPagto
        {
            get { return numSeqPagto; }
            set { numSeqPagto = value; }
        }
        public string LogUsuario
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }

        #endregion

        #region Construtores
        public PagamentoCobranca()
            : this(-1)
        { }
        public PagamentoCobranca(int numSeqPagto)
        {
            this.numSeqPagto = numSeqPagto;
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "PagamentoCobranca.PagamentoCobrancaInc";
        private const string SPUPDATE = "PagamentoCobranca.PagamentoCobrancaAlt";
        private const string SPDELETE = "PagamentoCobranca.PagamentoCobrancaDel";
        private const string SPSELECTID = "PagamentoCobranca.PagamentoCobrancaSelId";
        private const string SPSELECTPAG = "PagamentoCobranca.PagamentoCobrancaSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMCODIGO = "numSeqPagto";
        private const string PARMCURSOR = "curPagamentoCobranca";
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
                /*0*/ new OracleParameter(PARMCODIGO, OracleType.Int32, 2, ParameterDirection.InputOutput.ToString()) ,
                /*1*/ new OracleParameter( "numBloq", OracleType.Int32),
                /*2*/ new OracleParameter( "dtaVencto", OracleType.DateTime),
                /*3*/ new OracleParameter( "codRemessa", OracleType.Int32),
                /*4*/ new OracleParameter( "codRetorno", OracleType.Int32),
                /*5*/ new OracleParameter( "vlrParcela", OracleType.Float),
                /*6*/ new OracleParameter( "nomSacado", OracleType.VarChar),
                /*7*/ new OracleParameter( "dtaPagto", OracleType.DateTime),
                /*8*/ new OracleParameter( "vlrPgtoCobranca", OracleType.Float),
                /*9*/ new OracleParameter( "numBoleto", OracleType.Int32),
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
            parms[0].Value = this.numSeqPagto;
            parms[1].Value = this.numBloq;
            parms[2].Value = this.dtaVencto;
            parms[3].Value = this.codRemessa;
            parms[4].Value = this.codRetorno;
            parms[5].Value = this.vlrParcela;
            parms[6].Value = this.nomSacado.ToUpper();
            parms[7].Value = this.dtaPagto;
            parms[8].Value = this.vlrPgtoCobranca;
            parms[9].Value = this.numBoleto;
            parms[10].Value = this.logUsuario.ToUpper();
            if (this.numSeqPagto < 0)
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
        /// <returns>PagamentoCobranca</returns>
        public static PagamentoCobranca GetDataRow(int codigo)
        {
            OracleDataReader dr = LoadDataDr(codigo);
            PagamentoCobranca PagamentoCobranca = new PagamentoCobranca();
            try
            {
                if (dr.Read())
                {
                    PagamentoCobranca.numSeqPagto = Convert.ToInt32(dr["A124_num_seq_pagto"]);
                    PagamentoCobranca.numBloq = Convert.ToInt32(dr["A163_num_bloq"]);
                    PagamentoCobranca.dtaVencto = Convert.ToDateTime(dr["A163_dt_vecto"]);
                    if (dr["codRemessa"] != DBNull.Value)
                        PagamentoCobranca.codRemessa = Convert.ToInt32(dr["A162_cd_remessa"]);
                    if (dr["codRetorno"] != DBNull.Value)
                        PagamentoCobranca.codRetorno = Convert.ToInt32(dr["A162_cd_retorno"]);
                    PagamentoCobranca.vlrParcela = Convert.ToInt32(dr["A163_vlr_parcela"]);
                    PagamentoCobranca.nomSacado = Convert.ToString(dr["A163_nm_sacado"]);
                    if (dr["dtaPagto"] != DBNull.Value)
                        PagamentoCobranca.dtaPagto = Convert.ToDateTime(dr["A163_dt_pagto"]);
                    PagamentoCobranca.vlrPgtoCobranca = Convert.ToInt32(dr["A163_valor_pago"]);
                    if (dr["numBoleto"] != DBNull.Value)
                        PagamentoCobranca.numBoleto = Convert.ToInt32(dr["A163_num_boleto"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        PagamentoCobranca.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                PagamentoCobranca = new PagamentoCobranca();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return PagamentoCobranca;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>PagamentoCobranca</returns>
        public static PagamentoCobranca GetDataRow(int codigo, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codigo, trans);
            PagamentoCobranca PagamentoCobranca = new PagamentoCobranca();
            try
            {
                if (dr.Read())
                {
                    PagamentoCobranca.numSeqPagto = Convert.ToInt32(dr["A124_num_seq_pagto"]);
                    PagamentoCobranca.numBloq = Convert.ToInt32(dr["A163_num_bloq"]);
                    PagamentoCobranca.dtaVencto = Convert.ToDateTime(dr["A163_dt_vecto"]);
                    if (dr["codRemessa"] != DBNull.Value)
                        PagamentoCobranca.codRemessa = Convert.ToInt32(dr["A162_cd_remessa"]);
                    if (dr["codRetorno"] != DBNull.Value)
                        PagamentoCobranca.codRetorno = Convert.ToInt32(dr["A162_cd_retorno"]);
                    PagamentoCobranca.vlrParcela = Convert.ToInt32(dr["A163_vlr_parcela"]);
                    PagamentoCobranca.nomSacado = Convert.ToString(dr["A163_nm_sacado"]);
                    if (dr["dtaPagto"] != DBNull.Value)
                        PagamentoCobranca.dtaPagto = Convert.ToDateTime(dr["A163_dt_pagto"]);
                    PagamentoCobranca.vlrPgtoCobranca = Convert.ToInt32(dr["A163_valor_pago"]);
                    if (dr["numBoleto"] != DBNull.Value)
                        PagamentoCobranca.numBoleto = Convert.ToInt32(dr["A163_num_boleto"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        PagamentoCobranca.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                PagamentoCobranca = new PagamentoCobranca();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return PagamentoCobranca;
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
