using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
using System.Data;

//-- Classe Classes Sebrae
//-- Data : 03/07/2007 
//-- Autor :  Honorato

/*
numSeqPagto
numSeqCartao
numCartao
dtaValidadeCartao
vlrCartao
sitParcela
codOperad
dtaDebito
indDebCre
logUsuario

A124_num_seq_pagto, A133_num_seq_cartao, A133_num_cartao, A133_val_cartao, A133_valor,
A133_status_parcela, A297_cd_operad, A133_dt_debito, A133_tp_deb_cre, usu_inc_alt

 */

namespace Classes
{
    public class PagamentoCartao  // T133_pagto_cartao_credito
    {
        #region Atributos
        private int numSeqPagto;
        private int numSeqCartao;
        private int numCartao;
        private string dtaValidadeCartao;
        private decimal vlrCartao;
        private string sitParcela;
        private OperadoraCartao codOperad;
        private DateTime dtaDebito;
        private string indDebCre;
        private string logUsuario;
        #endregion

        #region Propriedades
        public int NumCartao
        {
            get { return numCartao; }
            set { numCartao = value; }
        }
        public string DtaValidadeCartao
        {
            get { return dtaValidadeCartao; }
            set { dtaValidadeCartao = value; }
        }
        public decimal VlrCartao
        {
            get { return vlrCartao; }
            set { vlrCartao = value; }
        }
        public string SitParcela
        {
            get { return sitParcela; }
            set { sitParcela = value; }
        }
        public OperadoraCartao CodOperad
        {
            get { return codOperad; }
            set { codOperad = value; }
        }
        public DateTime DtaDebito
        {
            get { return dtaDebito; }
            set { dtaDebito = value; }
        }
        public string IndDebCre
        {
            get { return indDebCre; }
            set { indDebCre = value; }
        }
        public int NumSeqCartao
        {
            get { return numSeqCartao; }
            set { numSeqCartao = value; }
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
        public PagamentoCartao()
            : this(-1)
        { }

        public PagamentoCartao(int numSeqPagto)
        {
            this.numSeqPagto = numSeqPagto;
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "PagamentoCartao.PagamentoCartaoInc";
        private const string SPUPDATE = "PagamentoCartao.PagamentoCartaoAlt";
        private const string SPDELETE = "PagamentoCartao.PagamentoCartaoDel";
        private const string SPSELECTID = "PagamentoCartao.PagamentoCartaoSelId";
        private const string SPSELECTPAG = "PagamentoCartao.PagamentoCartaoSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMCODIGO = "numSeqPagto";
        private const string PARMCURSOR = "curPagamentoCartao";
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
                /*1*/ new OracleParameter( "numSeqCartao", OracleType.Int32),		
                /*1*/ new OracleParameter( "numCartao", OracleType.Int32),		
                /*1*/ new OracleParameter( "dtaValidadeCartao", OracleType.VarChar),		
                /*1*/ new OracleParameter( "vlrCartao", OracleType.Float), 
                /*1*/ new OracleParameter( "sitParcela", OracleType.VarChar),		
                /*1*/ new OracleParameter( "codOperad", OracleType.Int32),		
                /*1*/ new OracleParameter( "dtaDebito", OracleType.DateTime),		
                /*1*/ new OracleParameter( "indDebCre", OracleType.VarChar),		
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
            parms[0].Value = this.numSeqPagto;
            parms[1].Value = this.numSeqCartao;
            parms[2].Value = this.numCartao;
            parms[3].Value = this.dtaValidadeCartao;
            parms[4].Value = this.vlrCartao;
            parms[5].Value = "";
            if (this.sitParcela != null)
            { parms[5].Value = this.sitParcela; }
            parms[6].Value = DBNull.Value;
            if (this.codOperad != null)
            { parms[6].Value = this.codOperad.CodOperadoraCartao; }
            parms[7].Value = this.dtaDebito;
            parms[8].Value = "";
            if (this.indDebCre != null)
            { parms[8].Value = this.indDebCre; }
            parms[9].Value = this.logUsuario.ToUpper();

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
        /// <returns>PagamentoCartao</returns>
        public static PagamentoCartao GetDataRow(int codigo)
        {
            OracleDataReader dr = LoadDataDr(codigo);
            PagamentoCartao PagamentoCartao = new PagamentoCartao();
            try
            {
                if (dr.Read())
                {
                    PagamentoCartao.numSeqPagto = Convert.ToInt32(dr["A124_num_seq_pagto"]);
                    PagamentoCartao.numSeqCartao = Convert.ToInt32(dr["A133_num_seq_cartao"]);
                    PagamentoCartao.numCartao = Convert.ToInt32(dr["A133_num_cartao"]);
                    PagamentoCartao.dtaValidadeCartao = dr["A133_val_cartao"].ToString();
                    PagamentoCartao.vlrCartao = Convert.ToInt32(dr["A136_valor"]);
                    PagamentoCartao.sitParcela = Convert.ToString(dr["A133_status_parcela"]);
                    PagamentoCartao.codOperad = new OperadoraCartao(Convert.ToInt32(dr["A297_cd_operad"]));
                    PagamentoCartao.dtaDebito = Convert.ToDateTime(dr["A133_dt_debito"]);
                    PagamentoCartao.indDebCre = Convert.ToString(dr["A133_tp_deb_cre"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        PagamentoCartao.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                PagamentoCartao = new PagamentoCartao();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return PagamentoCartao;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>PagamentoCartao</returns>
        public static PagamentoCartao GetDataRow(int codigo, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codigo, trans);
            PagamentoCartao PagamentoCartao = new PagamentoCartao();
            try
            {
                if (dr.Read())
                {
                    PagamentoCartao.numSeqPagto = Convert.ToInt32(dr["A124_num_seq_pagto"]);
                    PagamentoCartao.numSeqCartao = Convert.ToInt32(dr["A133_num_seq_cartao"]);
                    PagamentoCartao.numCartao = Convert.ToInt32(dr["A133_num_cartao"]);
                    PagamentoCartao.dtaValidadeCartao = dr["A133_val_cartao"].ToString();
                    PagamentoCartao.vlrCartao = Convert.ToInt32(dr["A136_valor"]);
                    PagamentoCartao.sitParcela = Convert.ToString(dr["A133_status_parcela"]);
                    PagamentoCartao.codOperad = new OperadoraCartao(Convert.ToInt32(dr["A297_cd_operad"]));
                    PagamentoCartao.dtaDebito = Convert.ToDateTime(dr["A133_dt_debito"]);
                    PagamentoCartao.indDebCre = Convert.ToString(dr["A133_tp_deb_cre"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        PagamentoCartao.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                PagamentoCartao = new PagamentoCartao();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return PagamentoCartao;
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
