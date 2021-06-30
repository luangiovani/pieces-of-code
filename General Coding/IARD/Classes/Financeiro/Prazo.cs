using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
using System.Data;

//-- Classe Classes Sebrae
//-- Data : 14/08/2007 
//-- Autor :  Honorato

namespace Classes
{
    public class Prazo  // T137_PRAZO  
    {
        #region Atributos
        private int codPrazo;
        private string dscPrazo;
        private int? numDiaInicial;
        private int? numParcelas;
        private int? numDiaParcela;
        private int? vlrMinPagto;
        private int indPrimeiraAVista;
        private string tpoPagto;
        private int? indDesat;
        private string logUsuario;

        #endregion

        #region Propriedades
        public int CodPrazo
        {
            get { return codPrazo; }
            set { codPrazo = value; }
        }
        public string DscPrazo
        {
            get { return dscPrazo; }
            set { dscPrazo = value; }
        }
        public int? NumDiaInicial
        {
            get { return numDiaInicial; }
            set { numDiaInicial = value; }
        }
        public int? NumParcelas
        {
            get { return numParcelas; }
            set { numParcelas = value; }
        }
        public int? NumDiaParcela
        {
            get { return numDiaParcela; }
            set { numDiaParcela = value; }
        }
        public int? VlrMinPagto
        {
            get { return vlrMinPagto; }
            set { vlrMinPagto = value; }
        }
        public int IndPrimeiraAVista
        {
            get { return indPrimeiraAVista; }
            set { indPrimeiraAVista = value; }
        }
        public string TpoPagto
        {
            get { return tpoPagto; }
            set { tpoPagto = value; }
        }
        public int? IndDesat
        {
            get { return indDesat; }
            set { indDesat = value; }
        }
        public string LogUsuario
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }

        #endregion

        #region Construtores
        public Prazo()
            : this(-1)
        { }
        public Prazo(int codPrazo)
        {
            this.codPrazo = codPrazo;
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "Prazo.PrazoInc";
        private const string SPUPDATE = "Prazo.PrazoAlt";
        private const string SPDELETE = "Prazo.PrazoDel";
        private const string SPSELECTID = "Prazo.PrazoSelId";
        private const string SPSELECTPAG = "Prazo.PrazoSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMCODIGO = "codPrazo";
        private const string PARMCURSOR = "curPrazo";
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
                /*1*/ new OracleParameter( "dscPrazo", OracleType.VarChar),
                /*2*/ new OracleParameter( "numDiaInicial", OracleType.Int32),
                /*3*/ new OracleParameter( "numParcelas", OracleType.Int32),
                /*4*/ new OracleParameter( "numDiaParcela", OracleType.Int32),
                /*5*/ new OracleParameter( "vlrMinPagto", OracleType.Int32),
                /*6*/ new OracleParameter( "indPrimeiraAVista", OracleType.Int32),
                /*7*/ new OracleParameter( "tpoPagto", OracleType.VarChar),
                /*8*/ new OracleParameter( "indDesat", OracleType.Int32),
                /*9*/ new OracleParameter( "logUsuario", OracleType.VarChar)
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
            parms[0].Value = this.codPrazo;
            parms[1].Value = this.dscPrazo.ToUpper();
            parms[2].Value = DBNull.Value;
            parms[3].Value = DBNull.Value;
            parms[4].Value = DBNull.Value;
            parms[5].Value = DBNull.Value;
            parms[6].Value = DBNull.Value;
            parms[7].Value = "";
            parms[8].Value = DBNull.Value;
            if (this.numDiaInicial != null){
                parms[2].Value = this.numDiaInicial; }
            if (this.numParcelas != null){
                parms[3].Value = this.numParcelas;}
            if (this.numDiaParcela != null){
                parms[4].Value = this.numDiaParcela;}
            if (this.vlrMinPagto != null){
                parms[5].Value = this.vlrMinPagto;}
            if (this.indPrimeiraAVista != null){
                parms[6].Value = this.indPrimeiraAVista;}
            if (this.tpoPagto != null){
                parms[7].Value = this.tpoPagto.ToUpper();}
            if (this.indDesat != null){
                parms[8].Value = this.indDesat;}
            parms[9].Value = this.logUsuario.ToUpper();
            if (this.codPrazo < 0)
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
                        codPrazo = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
                codPrazo = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
        /// <returns>Prazo</returns>
        public static Prazo GetDataRow(int codigo)
        {
            OracleDataReader dr = LoadDataDr(codigo);
            Prazo Prazo = new Prazo();
            try
            {
                if (dr.Read())
                {
                    Prazo.codPrazo = Convert.ToInt32(dr["A137_cd_prazo"]);
                    if (dr["A137_dsc_prazo"] != DBNull.Value && dr["A137_dsc_prazo"] != DBNull.Value && dr["A137_dsc_prazo"].ToString() != "")                    
                        Prazo.dscPrazo = Convert.ToString(dr["A137_dsc_prazo"]);
                    if (dr["A137_num_dia_inicial"] != DBNull.Value && dr["A137_num_dia_inicial"] != DBNull.Value && dr["A137_num_dia_inicial"].ToString() != "")                    
                        Prazo.numDiaInicial = Convert.ToInt32(dr["A137_num_dia_inicial"]);
                    if (dr["A137_num_parcelas"] != DBNull.Value && dr["A137_num_parcelas"] != DBNull.Value && dr["A137_num_parcelas"].ToString() != "")                    
                        Prazo.numParcelas = Convert.ToInt32(dr["A137_num_parcelas"]);
                    if (dr["A137_num_dia_parcela"] != DBNull.Value && dr["A137_num_dia_parcela"] != DBNull.Value && dr["A137_num_dia_parcela"].ToString() != "")                    
                        Prazo.numDiaParcela = Convert.ToInt32(dr["A137_num_dia_parcela"]);
                    if (dr["A137_vl_min_pagto"] != DBNull.Value && dr["A137_vl_min_pagto"] != DBNull.Value && dr["A137_vl_min_pagto"].ToString() != "")                    
                        Prazo.vlrMinPagto = Convert.ToInt32(dr["A137_vl_min_pagto"]);
                    if (dr["A137_1parcela_a_vista"] != DBNull.Value && dr["A137_1parcela_a_vista"] != DBNull.Value && dr["A137_1parcela_a_vista"].ToString() != "")                    
                        Prazo.indPrimeiraAVista = Convert.ToInt32(dr["A137_1parcela_a_vista"]);
                    if (dr["A137_tpo_pgto"] != DBNull.Value && dr["A137_tpo_pgto"] != DBNull.Value && dr["A137_tpo_pgto"].ToString() != "")
                        Prazo.tpoPagto = Convert.ToString(dr["A137_tpo_pgto"]);
                    if (dr["A137_ind_desat"] != DBNull.Value && dr["A137_ind_desat"] != DBNull.Value && dr["A137_ind_desat"].ToString() != "")
                        Prazo.indDesat = Convert.ToInt32(dr["A137_ind_desat"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        Prazo.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                Prazo = new Prazo();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return Prazo;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>Prazo</returns>
        public static Prazo GetDataRow(int codigo, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codigo, trans);
            Prazo Prazo = new Prazo();
            try
            {
                if (dr.Read())
                {
                    Prazo.codPrazo = Convert.ToInt32(dr["A137_cd_prazo"]);
                    if (dr["A137_dsc_prazo"] != DBNull.Value && dr["A137_dsc_prazo"] != DBNull.Value && dr["A137_dsc_prazo"].ToString() != "")
                        Prazo.dscPrazo = Convert.ToString(dr["A137_dsc_prazo"]);
                    if (dr["A137_num_dia_inicial"] != DBNull.Value && dr["A137_num_dia_inicial"] != DBNull.Value && dr["A137_num_dia_inicial"].ToString() != "")
                        Prazo.numDiaInicial = Convert.ToInt32(dr["A137_num_dia_inicial"]);
                    if (dr["A137_num_parcelas"] != DBNull.Value && dr["A137_num_parcelas"] != DBNull.Value && dr["A137_num_parcelas"].ToString() != "")
                        Prazo.numParcelas = Convert.ToInt32(dr["A137_num_parcelas"]);
                    if (dr["A137_num_dia_parcela"] != DBNull.Value && dr["A137_num_dia_parcela"] != DBNull.Value && dr["A137_num_dia_parcela"].ToString() != "")
                        Prazo.numDiaParcela = Convert.ToInt32(dr["A137_num_dia_parcela"]);
                    if (dr["A137_vl_min_pagto"] != DBNull.Value && dr["A137_vl_min_pagto"] != DBNull.Value && dr["A137_vl_min_pagto"].ToString() != "")
                        Prazo.vlrMinPagto = Convert.ToInt32(dr["A137_vl_min_pagto"]);
                    if (dr["A137_1parcela_a_vista"] != DBNull.Value && dr["A137_1parcela_a_vista"] != DBNull.Value && dr["A137_1parcela_a_vista"].ToString() != "")
                        Prazo.indPrimeiraAVista = Convert.ToInt32(dr["A137_1parcela_a_vista"]);
                    if (dr["A137_tpo_pgto"] != DBNull.Value && dr["A137_tpo_pgto"] != DBNull.Value && dr["A137_tpo_pgto"].ToString() != "")
                        Prazo.tpoPagto = Convert.ToString(dr["A137_tpo_pgto"]);
                    if (dr["A137_ind_desat"] != DBNull.Value && dr["A137_ind_desat"] != DBNull.Value && dr["A137_ind_desat"].ToString() != "")
                        Prazo.indDesat = Convert.ToInt32(dr["A137_ind_desat"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        Prazo.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                Prazo = new Prazo();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return Prazo;
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