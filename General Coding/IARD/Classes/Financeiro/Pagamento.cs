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
    public class Pagamento  // T124_pagamento 
    {
        #region Atributos
        private int numSeqPagto;
        private int? codDepartamento;
        private Cliente codCliente;
        private string codProdSeb;
        private DateTime dtaPagamento;
        private int? indProces;
        private int? indContabil;
        private int? codRecibo;
        private int? indEncam;
        private int? codNotaRecibo;
        private DateTime? dtaFechamentoCaixa;
        private DateTime? dtaFechamentoContabil;
        private int? indEntrada;
        private int? indSiga;
        private DateTime? dtaFechamentoSiga;
        private Contato codContato;
        private string tpoPagto;
        private int? numNotaFiscal;
        private int? codUsuario;
        private decimal? prcIssRetido;
        private int? codPrazo;
        private int? numFechamentoCaixa;
        private string logUsuario;

        #endregion

        #region Propriedades
        public int? CodDepartamento
        {
            get { return codDepartamento; }
            set { codDepartamento = value; }
        }
        public Cliente CodCliente
        {
            get { return codCliente; }
            set { codCliente = value; }
        }
        public string CodProdSeb
        {
            get { return codProdSeb; }
            set { codProdSeb = value; }
        }
        public DateTime DtaPagamento
        {
            get { return dtaPagamento; }
            set { dtaPagamento = value; }
        }
        public int? IndProces
        {
            get { return indProces; }
            set { indProces = value; }
        }
        public int? IndContabil
        {
            get { return indContabil; }
            set { indContabil = value; }
        }
        public int? CodRecibo
        {
            get { return codRecibo; }
            set { codRecibo = value; }
        }
        public int? IndEncam
        {
            get { return indEncam; }
            set { indEncam = value; }
        }
        public int? CodNotaRecibo
        {
            get { return codNotaRecibo; }
            set { codNotaRecibo = value; }
        }
        public DateTime? DtaFechamentoCaixa
        {
            get { return dtaFechamentoCaixa; }
            set { dtaFechamentoCaixa = value; }
        }
        public DateTime? DtaFechamentoContabil
        {
            get { return dtaFechamentoContabil; }
            set { dtaFechamentoContabil = value; }
        }
        public int? IndEntrada
        {
            get { return indEntrada; }
            set { indEntrada = value; }
        }
        public int? IndSiga
        {
            get { return indSiga; }
            set { indSiga = value; }
        }
        public DateTime? DtaFechamentoSiga
        {
            get { return dtaFechamentoSiga; }
            set { dtaFechamentoSiga = value; }
        }
        public Contato CodContato
        {
            get { return codContato; }
            set { codContato = value; }
        }
        public string TpoPagto
        {
            get { return tpoPagto; }
            set { tpoPagto = value; }
        }
        public int? NumNotaFiscal
        {
            get { return numNotaFiscal; }
            set { numNotaFiscal = value; }
        }
        public int? CodUsuario
        {
            get { return codUsuario; }
            set { codUsuario = value; }
        }
        public decimal? PrcIssRetido
        {
            get { return prcIssRetido; }
            set { prcIssRetido = value; }
        }
        public int? CodPrazo
        {
            get { return codPrazo; }
            set { codPrazo = value; }
        }
        public int? NumFechamentoCaixa
        {
            get { return numFechamentoCaixa; }
            set { numFechamentoCaixa = value; }
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
        public Pagamento()
            : this(-1)
        { }
        public Pagamento(int numSeqPagto)
        {
            this.numSeqPagto = numSeqPagto;
            this.CodCliente = new Cliente();
            this.CodContato = new Contato();
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "Pagamento.PagamentoInc";
        private const string SPUPDATE = "Pagamento.PagamentoAlt";
        private const string SPDELETE = "Pagamento.PagamentoDel";
        private const string SPSELECTID = "Pagamento.PagamentoSelId";
        private const string SPSELECTPAG = "Pagamento.PagamentoSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMCODIGO = "numSeqPagto";
        private const string PARMCURSOR = "curPagamento";
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
                /*0*/ new OracleParameter(PARMCODIGO, OracleType.Int32, 4, ParameterDirection.InputOutput.ToString()) ,
                /*1*/ new OracleParameter( "codDepartamento", OracleType.Int32),		
                /*2*/ new OracleParameter( "codCliente", OracleType.Int32),			
                /*3*/ new OracleParameter( "codProdSeb", OracleType.VarChar),			
                /*4*/ new OracleParameter( "dtaPagamento", OracleType.DateTime),		
                /*5*/ new OracleParameter( "indProces", OracleType.Int32),			
                /*6*/ new OracleParameter( "indContabil", OracleType.Int32),			
                /*7*/ new OracleParameter( "codRecibo", OracleType.Int32),			
                /*8*/ new OracleParameter( "indEncam", OracleType.Int32),				
                /*9*/ new OracleParameter( "codNotaRecibo", OracleType.Int32),		
                /*10*/ new OracleParameter( "dtaFechamentoCaixa", OracleType.DateTime),	
                /*11*/ new OracleParameter( "dtaFechamentoContabil", OracleType.DateTime),
                /*12*/ new OracleParameter( "indEntrada", OracleType.Int32),		
                /*13*/ new OracleParameter( "indSiga", OracleType.Int32),			
                /*14*/ new OracleParameter( "dtaFechamentoSiga", OracleType.DateTime),
                /*15*/ new OracleParameter( "codContato", OracleType.Int32),	
                /*16*/ new OracleParameter( "tpoPagto", OracleType.VarChar),	
                /*17*/ new OracleParameter( "numNotaFiscal", OracleType.Int32),
                /*18*/ new OracleParameter( "codUsuario", OracleType.Int32),	
                /*19*/ new OracleParameter( "prcIssRetido", OracleType.Double),
                /*20*/ new OracleParameter( "codPrazo", OracleType.Int32),
                /*21*/ new OracleParameter( "numFechamentoCaixa", OracleType.Int32),
                /*22*/ new OracleParameter( "logUsuario", OracleType.VarChar)
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
            parms[1].Value = DBNull.Value;
            if (this.codDepartamento != null)
            { parms[1].Value = this.codDepartamento; }
            parms[2].Value = this.codCliente.CodCLIENTE;
            parms[3].Value = this.codProdSeb;
            parms[4].Value = this.dtaPagamento;
            parms[5].Value= DBNull.Value;
            if (this.indProces != null)
            { parms[5].Value = this.indProces;}
             parms[6].Value= DBNull.Value;
            if (this.indContabil != null)
            { parms[6].Value = this.indContabil;}
            parms[7].Value= DBNull.Value;
            if (this.codRecibo != null)
            { parms[7].Value = this.codRecibo;}
             parms[8].Value= DBNull.Value;
            if (this.indEncam != null)
            {parms[8].Value = this.indEncam;}
             parms[9].Value= DBNull.Value;
            if (this.codNotaRecibo != null)
            {parms[9].Value = this.codNotaRecibo;}
             parms[10].Value= DBNull.Value;
            if (this.dtaFechamentoCaixa != null)
            {parms[10].Value = this.dtaFechamentoCaixa;}
             parms[11].Value= DBNull.Value;
            if (this.dtaFechamentoContabil != null)
            {parms[11].Value = this.dtaFechamentoContabil;}
             parms[12].Value= DBNull.Value;
            if (this.indEntrada != null)
            {parms[12].Value = this.indEntrada;}
             parms[13].Value= DBNull.Value;
            if (this.indSiga != null)
            {parms[13].Value = this.indSiga;}
             parms[14].Value= DBNull.Value;
            if (this.dtaFechamentoSiga != null)
            {parms[14].Value = this.dtaFechamentoSiga;}
            parms[15].Value = this.codContato.CodContato;
            parms[16].Value = this.tpoPagto;
             parms[17].Value= DBNull.Value;
            if (this.numNotaFiscal != null)
            {parms[17].Value = this.numNotaFiscal;}
            parms[18].Value = this.codUsuario;
             parms[19].Value = DBNull.Value;
            if (this.prcIssRetido != null)
            {parms[19].Value = this.prcIssRetido;}
            parms[20].Value = this.codPrazo;
            parms[21].Value = DBNull.Value;
            if (this.numFechamentoCaixa != null)
            { parms[21].Value = this.numFechamentoCaixa; }
            parms[22].Value = this.logUsuario.ToUpper();
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
        /// <returns>Pagamento</returns>
        public static Pagamento GetDataRow(int codigo)
        {
            OracleDataReader dr = LoadDataDr(codigo);
            Pagamento Pagamento = new Pagamento();
            try
            {
                if (dr.Read())
                {
                    Pagamento.numSeqPagto = Convert.ToInt32(dr["A124_num_seq_pagto"]);
                    if (dr["A236_cd_depto"] != DBNull.Value)
                        Pagamento.codDepartamento = Convert.ToInt32(dr["A236_cd_depto"]);
                    Pagamento.codCliente = new Cliente(Convert.ToInt32(dr["A012_cd_cli"]));
                    Pagamento.codProdSeb = Convert.ToString(dr["A042_CD_PROD_SEB"]);
                    Pagamento.dtaPagamento = Convert.ToDateTime(dr["A124_dt_pagto"]);
                    if (dr["A124_ind_proces"] != DBNull.Value)
                        Pagamento.indProces = Convert.ToInt32(dr["A124_ind_proces"]);
                    if (dr["A124_ind_contabil"] != DBNull.Value) 
                        Pagamento.indContabil = Convert.ToInt32(dr["A124_ind_contabil"]);
                    if (dr["A124_num_recibo"] != DBNull.Value) 
                        Pagamento.codRecibo = Convert.ToInt32(dr["A124_num_recibo"]);
                    if (dr["A124_ind_encam"] != DBNull.Value) 
                        Pagamento.indEncam = Convert.ToInt32(dr["A124_ind_encam"]);
                    if (dr["A124_nota_recibo"] != DBNull.Value) 
                        Pagamento.codNotaRecibo = Convert.ToInt32(dr["A124_nota_recibo"]);
                    if (dr["A124_dt_fechto_caixa"] != DBNull.Value) 
                        Pagamento.dtaFechamentoCaixa = Convert.ToDateTime(dr["A124_dt_fechto_caixa"]);
                    if (dr["A124_dt_fechto_contabil"] != DBNull.Value) 
                        Pagamento.dtaFechamentoContabil = Convert.ToDateTime(dr["A124_dt_fechto_contabil"]);
                    if (dr["A124_ind_entrada"] != DBNull.Value) 
                        Pagamento.indEntrada = Convert.ToInt32(dr["A124_ind_entrada"]);
                    if (dr["A124_ind_siga"] != DBNull.Value) 
                        Pagamento.indSiga = Convert.ToInt32(dr["A124_ind_siga"]);
                    if (dr["A124_dt_fechto_siga"] != DBNull.Value) 
                        Pagamento.dtaFechamentoSiga = Convert.ToDateTime(dr["A124_dt_fechto_siga"]);
                    Pagamento.codContato = new Contato(Convert.ToInt32(dr["A261_CD_CONT"]));
                    if (dr["A124_tp_pagamento"] != DBNull.Value) 
                        Pagamento.tpoPagto = Convert.ToString(dr["A124_tp_pagamento"]);
                    if (dr["A124_num_nota_fiscal"] != DBNull.Value) 
                        Pagamento.numNotaFiscal = Convert.ToInt32(dr["A124_num_nota_fiscal"]);
                    if (dr["A052_cd_usuario"] != DBNull.Value) 
                        Pagamento.codUsuario = Convert.ToInt32(dr["A052_cd_usuario"]);
                    if (dr["A124_perc_iss_retido"] != DBNull.Value) 
                        Pagamento.prcIssRetido = Convert.ToInt32(dr["A124_perc_iss_retido"]);
                    if (dr["A137_cd_prazo"] != DBNull.Value) 
                        Pagamento.codPrazo = Convert.ToInt32(dr["A137_cd_prazo"]);
                    if (dr["A124_num_fechto_caixa"] != DBNull.Value) 
                        Pagamento.numFechamentoCaixa = Convert.ToInt32(dr["A124_num_fechto_caixa"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        Pagamento.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                Pagamento = new Pagamento();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return Pagamento;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>Pagamento</returns>
        public static Pagamento GetDataRow(int codigo, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codigo, trans);
            Pagamento Pagamento = new Pagamento();
            try
            {
                if (dr.Read())
                {
                    Pagamento.numSeqPagto = Convert.ToInt32(dr["A124_num_seq_pagto"]);
                    if (dr["A236_cd_depto"] != DBNull.Value)
                        Pagamento.codDepartamento = Convert.ToInt32(dr["A236_cd_depto"]);
                    Pagamento.codCliente = new Cliente(Convert.ToInt32(dr["A012_cd_cli"]));
                    Pagamento.codProdSeb = Convert.ToString(dr["A042_CD_PROD_SEB"]);
                    Pagamento.dtaPagamento = Convert.ToDateTime(dr["A124_dt_pagto"]);
                    if (dr["A124_ind_proces"] != DBNull.Value)
                        Pagamento.indProces = Convert.ToInt32(dr["A124_ind_proces"]);
                    if (dr["A124_ind_contabil"] != DBNull.Value)
                        Pagamento.indContabil = Convert.ToInt32(dr["A124_ind_contabil"]);
                    if (dr["A124_num_recibo"] != DBNull.Value)
                        Pagamento.codRecibo = Convert.ToInt32(dr["A124_num_recibo"]);
                    if (dr["A124_ind_encam"] != DBNull.Value)
                        Pagamento.indEncam = Convert.ToInt32(dr["A124_ind_encam"]);
                    if (dr["A124_nota_recibo"] != DBNull.Value)
                        Pagamento.codNotaRecibo = Convert.ToInt32(dr["A124_nota_recibo"]);
                    if (dr["A124_dt_fechto_caixa"] != DBNull.Value)
                        Pagamento.dtaFechamentoCaixa = Convert.ToDateTime(dr["A124_dt_fechto_caixa"]);
                    if (dr["A124_dt_fechto_contabil"] != DBNull.Value)
                        Pagamento.dtaFechamentoContabil = Convert.ToDateTime(dr["A124_dt_fechto_contabil"]);
                    if (dr["A124_ind_entrada"] != DBNull.Value)
                        Pagamento.indEntrada = Convert.ToInt32(dr["A124_ind_entrada"]);
                    if (dr["A124_ind_siga"] != DBNull.Value)
                        Pagamento.indSiga = Convert.ToInt32(dr["A124_ind_siga"]);
                    if (dr["A124_dt_fechto_siga"] != DBNull.Value)
                        Pagamento.dtaFechamentoSiga = Convert.ToDateTime(dr["A124_dt_fechto_siga"]);
                    Pagamento.codContato = new Contato(Convert.ToInt32(dr["A261_CD_CONT"]));
                    if (dr["A124_tp_pagamento"] != DBNull.Value)
                        Pagamento.tpoPagto = Convert.ToString(dr["A124_tp_pagamento"]);
                    if (dr["A124_num_nota_fiscal"] != DBNull.Value)
                        Pagamento.numNotaFiscal = Convert.ToInt32(dr["A124_num_nota_fiscal"]);
                    if (dr["A052_cd_usuario"] != DBNull.Value)
                        Pagamento.codUsuario = Convert.ToInt32(dr["A052_cd_usuario"]);
                    if (dr["A124_perc_iss_retido"] != DBNull.Value)
                        Pagamento.prcIssRetido = Convert.ToInt32(dr["A124_perc_iss_retido"]);
                    if (dr["A137_cd_prazo"] != DBNull.Value)
                        Pagamento.codPrazo = Convert.ToInt32(dr["A137_cd_prazo"]);
                    if (dr["A124_num_fechto_caixa"] != DBNull.Value)
                        Pagamento.numFechamentoCaixa = Convert.ToInt32(dr["A124_num_fechto_caixa"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        Pagamento.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                Pagamento = new Pagamento();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return Pagamento;
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
