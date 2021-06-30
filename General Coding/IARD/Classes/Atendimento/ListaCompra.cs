using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;

using System.Data;

//-- Classe Classes Sebrae
//-- Data : 13/08/2007 
//-- Autor :  Honorato

namespace Classes
{
    public class ListaCompra // T289_LISTA_COMPRA
    {
        #region Atributos
        private int codItem;
        private Cliente codCliente;
        private EscritorioSebrae codEscrSebrae;
        private int? codUsuario;
        private int? numAtend;
        private int? numSeqPagto;
        private string tpoItem;
        private string dscItem;
        private decimal vlrItem;
        private decimal qtdItem;
        private string dscStatus;
        private int? indDisponivel;
        private string dscMeioEnvio;
        private decimal? prcDesconto;
        private decimal? vlrDesconto;
        private string dscObservacao;
        private int? indItemAberto;
        private string logUsuario;
        #endregion

        #region Propriedades
        public int CodItem
        {
            get { return codItem; }
            set { codItem = value; }
        }
        public Cliente CodCliente
        {
            get { return codCliente; }
            set { codCliente = value; }
        }
        public EscritorioSebrae CodEscrSebrae
        {
            get { return codEscrSebrae; }
            set { codEscrSebrae = value; }
        }
        public int? CodUsuario
        {
            get { return codUsuario; }
            set { codUsuario = value; }
        }
        public int? NumAtend
        {
            get { return numAtend; }
            set { numAtend = value; }
        }
        public int? NumSeqPagto
        {
            get { return numSeqPagto; }
            set { numSeqPagto = value; }
        }
        public string TpoItem
        {
            get { return tpoItem; }
            set { tpoItem = value; }
        }
        public string DscItem
        {
            get { return dscItem; }
            set { dscItem = value; }
        }
        public decimal VlrItem
        {
            get { return vlrItem; }
            set { vlrItem = value; }
        }
        public decimal QtdItem
        {
            get { return qtdItem; }
            set { qtdItem = value; }
        }
        public string DscStatus
        {
            get { return dscStatus; }
            set { dscStatus = value; }
        }
        public int? IndDisponivel
        {
            get { return indDisponivel; }
            set { indDisponivel = value; }
        }
        public string DscMeioEnvio
        {
            get { return dscMeioEnvio; }
            set { dscMeioEnvio = value; }
        }
        public decimal? PrcDesconto
        {
            get { return prcDesconto; }
            set { prcDesconto = value; }
        }
        public decimal? VlrDesconto
        {
            get { return vlrDesconto; }
            set { vlrDesconto = value; }
        }
        public string DscObservacao
        {
            get { return dscObservacao; }
            set { dscObservacao = value; }
        }
        public int? IndItemAberto
        {
            get { return indItemAberto; }
            set { indItemAberto = value; }
        }
        public string LogUsuario
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }
        #endregion

        #region Construtores
        public ListaCompra()
            : this(-1)
        { }
        public ListaCompra(int codItem)
        {
            this.codItem = codItem;
            this.codCliente = new Cliente();
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "ListaCompra.ListaCompraInc";
        private const string SPUPDATE = "ListaCompra.ListaCompraAlt";
        private const string SPDELETE = "ListaCompra.ListaCompraDel";
        private const string SPSELECTID = "ListaCompra.ListaCompraSelId";
        private const string SPSELECTPAG = "ListaCompra.ListaCompraSelPag";
        private const string SPSELECTBUSCA = "ListaCompra.BuscaEventosPagarSelPag";
        private const string SPSELECTBUSCAPAGOS = "ListaCompra.BuscaEventosPagosSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMcodItem = "codItem";
        private const string PARMcodCliente = "codCliente";
        private const string PARMCURSOR = "curListaCompra";
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
                    /*0*/ new OracleParameter(PARMcodItem, OracleType.Int32, 4, ParameterDirection.InputOutput.ToString()) ,
                    /*1*/ new OracleParameter(PARMcodCliente, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /***/ new OracleParameter( "codEscrSebrae", OracleType.Int32),
                    /***/ new OracleParameter( "codUsuario", OracleType.Int32),
                    /***/ new OracleParameter( "numAtend", OracleType.Int32),
                    /***/ new OracleParameter( "numSeqPagto", OracleType.Int32),
                    /***/ new OracleParameter( "tpoItem", OracleType.VarChar),
                    /***/ new OracleParameter( "dscItem", OracleType.VarChar),
                    /***/ new OracleParameter( "vlrItem", OracleType.Int32),
                    /***/ new OracleParameter( "qtdItem", OracleType.Int32),
                    /***/ new OracleParameter( "dscStatus", OracleType.VarChar),
                    /***/ new OracleParameter( "indDisponivel", OracleType.Int32),
                    /***/ new OracleParameter( "dscMeioEnvio", OracleType.VarChar),
                    /***/ new OracleParameter( "prcDesconto", OracleType.Int32),
                    /***/ new OracleParameter( "vlrDesconto", OracleType.Int32),
                    /***/ new OracleParameter( "dscObservacao", OracleType.VarChar),
                    /***/ new OracleParameter( "indItemAberto", OracleType.Int32),
                    /*17*/ new OracleParameter( "logUsuario", OracleType.VarChar)
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
            parms[0].Value = this.codItem;
            parms[1].Value = this.CodCliente.CodCLIENTE;
            parms[2].Value = this.CodEscrSebrae.CodEscrSebrae;
            parms[3].Value = this.codUsuario;
            parms[4].Value =DBNull.Value;
            if (this.numAtend != null)
            { parms[4].Value = this.numAtend; }
            parms[5].Value=DBNull.Value;
            if (this.numSeqPagto != null)
            { parms[5].Value = this.numSeqPagto; }
            parms[6].Value = this.tpoItem;
            parms[7].Value = this.dscItem;
            parms[8].Value = this.vlrItem;
            parms[9].Value = this.qtdItem;
            parms[10].Value ="";
            if (this.dscStatus != null)
            {parms[10].Value = this.dscStatus;}
            parms[11].Value = this.indDisponivel;
            parms[12].Value="";
            if (this.dscMeioEnvio != null)
            {parms[12].Value = this.dscMeioEnvio;}
            parms[13].Value =DBNull.Value;
            if (this.prcDesconto != null)
            {parms[13].Value = this.prcDesconto;}
            parms[14].Value =DBNull.Value;
            if (this.vlrDesconto != null)
            {parms[14].Value = this.vlrDesconto;}
            parms[15].Value="";
            if (this.dscObservacao != null)
            {parms[15].Value = this.dscObservacao;}
            parms[16].Value =DBNull.Value;
            if (this.indItemAberto != null)
            {parms[16].Value = this.indItemAberto;}
            parms[17].Value = this.logUsuario;
            if (this.codItem < 0)
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
                        codItem = Convert.ToInt32(cmd.Parameters[PARMcodItem].Value);
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
                codItem = Convert.ToInt32(cmd.Parameters[PARMcodItem].Value);
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
        /// <param name="codItem">Código do Registro</param>
        public static void Delete(int codItem, int codCliente)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMcodItem, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodCliente, OracleType.Int32, 4)
            };
            parms[0].Value = codItem;
            parms[1].Value = codCliente;
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
        /// <param name="codItem">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(int codItem, int codCliente, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMcodItem, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodCliente, OracleType.Int32, 4)
            };
                parms[0].Value = codItem;
                parms[1].Value = codCliente;
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
        /// <param name="codItem">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codItem, int codCliente)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodItem, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodCliente, OracleType.Int32, 4),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codItem;
            param[1].Value = codCliente;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="codItem">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codItem, int codCliente, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodItem, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodCliente, OracleType.Int32, 4),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codItem;
            param[1].Value = codCliente;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="codItem">Código do Registro</param>
        /// <returns>ListaCompra</returns>
        public static ListaCompra GetDataRow(int codItem, int codCliente)
        {
            OracleDataReader dr = LoadDataDr(codItem, codCliente);
            ListaCompra ListaCompra = new ListaCompra();
            try
            {
                if (dr.Read())
                {
                    ListaCompra.codItem = Convert.ToInt32(dr["A289_cd_item"]);
                    if (dr["A012_cd_cli"] != DBNull.Value && dr["A012_cd_cli"] != DBNull.Value && dr["A012_cd_cli"].ToString() != "")
                        ListaCompra.codCliente = new Cliente(Convert.ToInt32(dr["A012_cd_cli"]));
                    if (dr["A004_cd_escr"] != DBNull.Value && dr["A004_cd_escr"] != DBNull.Value && dr["A004_cd_escr"].ToString() != "")
                        ListaCompra.codEscrSebrae = new EscritorioSebrae( Convert.ToInt32(dr["A004_cd_escr"]));
                    if (dr["A052_cd_usuario"] != DBNull.Value && dr["A052_cd_usuario"] != DBNull.Value && dr["A052_cd_usuario"].ToString() != "")
                        ListaCompra.codUsuario = Convert.ToInt32(dr["A052_cd_usuario"]);
                    if (dr["A001_num_atend"] != DBNull.Value && dr["A001_num_atend"] != DBNull.Value && dr["A001_num_atend"].ToString() != "")
                        ListaCompra.numAtend = Convert.ToInt32(dr["A001_num_atend"]);
                    if (dr["A124_num_seq_pagto"] != DBNull.Value && dr["A124_num_seq_pagto"] != DBNull.Value && dr["A124_num_seq_pagto"].ToString() != "")
                        ListaCompra.numSeqPagto = Convert.ToInt32(dr["A124_num_seq_pagto"]);
                    if (dr["A289_tp_item"] != DBNull.Value && dr["A289_tp_item"] != DBNull.Value && dr["A289_tp_item"].ToString() != "")
                        ListaCompra.tpoItem = Convert.ToString(dr["A289_tp_item"]);
                    if (dr["A289_desc_item"] != DBNull.Value && dr["A289_desc_item"] != DBNull.Value && dr["A289_desc_item"].ToString() != "")
                        ListaCompra.dscItem = Convert.ToString(dr["A289_desc_item"]);
                    if (dr["A289_vlr_item"] != DBNull.Value && dr["A289_vlr_item"] != DBNull.Value && dr["A289_vlr_item"].ToString() != "")
                        ListaCompra.vlrItem = Convert.ToInt32(dr["A289_vlr_item"]);
                    if (dr["A289_qtde_item"] != DBNull.Value && dr["A289_qtde_item"] != DBNull.Value && dr["A289_qtde_item"].ToString() != "")
                        ListaCompra.qtdItem = Convert.ToInt32(dr["A289_qtde_item"]);
                    if (dr["A289_status"] != DBNull.Value && dr["A289_status"] != DBNull.Value && dr["A289_status"].ToString() != "")
                        ListaCompra.dscStatus = Convert.ToString(dr["A289_status"]);
                    if (dr["A289_ind_dispon"] != DBNull.Value && dr["A289_ind_dispon"] != DBNull.Value && dr["A289_ind_dispon"].ToString() != "")
                        ListaCompra.indDisponivel = Convert.ToInt32(dr["A289_ind_dispon"]);
                    if (dr["A289_MEIOENVIO"] != DBNull.Value && dr["A289_MEIOENVIO"] != DBNull.Value && dr["A289_MEIOENVIO"].ToString() != "")
                        ListaCompra.dscMeioEnvio = Convert.ToString(dr["A289_MEIOENVIO"]);
                    if (dr["A289_perc_desconto"] != DBNull.Value && dr["A289_perc_desconto"] != DBNull.Value && dr["A289_perc_desconto"].ToString() != "")
                        ListaCompra.prcDesconto = Convert.ToInt32(dr["A289_perc_desconto"]);
                    if (dr["A289_valor_desconto"] != DBNull.Value && dr["A289_valor_desconto"] != DBNull.Value && dr["A289_valor_desconto"].ToString() != "")
                        ListaCompra.vlrDesconto = Convert.ToInt32(dr["A289_valor_desconto"]);
                    if (dr["A289_observacao"] != DBNull.Value && dr["A289_observacao"] != DBNull.Value && dr["A289_observacao"].ToString() != "")
                        ListaCompra.dscObservacao = Convert.ToString(dr["A289_observacao"]);
                    if (dr["A289_ind_item_aberto"] != DBNull.Value && dr["A289_ind_item_aberto"] != DBNull.Value && dr["A289_ind_item_aberto"].ToString() != "")
                        ListaCompra.indItemAberto = Convert.ToInt32(dr["A289_ind_item_aberto"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        ListaCompra.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                ListaCompra = new ListaCompra();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return ListaCompra;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codItem">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>ListaCompra</returns>
        public static ListaCompra GetDataRow(int codItem, int codCliente, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codItem, codCliente, trans);
            ListaCompra ListaCompra = new ListaCompra();
            try
            {
                if (dr.Read())
                {
                    ListaCompra.codItem = Convert.ToInt32(dr["A289_cd_item"]);
                    if (dr["A012_cd_cli"] != DBNull.Value && dr["A012_cd_cli"] != DBNull.Value && dr["A012_cd_cli"].ToString() != "")
                        ListaCompra.codCliente = new Cliente(Convert.ToInt32(dr["A012_cd_cli"]));
                    if (dr["A004_cd_escr"] != DBNull.Value && dr["A004_cd_escr"] != DBNull.Value && dr["A004_cd_escr"].ToString() != "")
                        ListaCompra.codEscrSebrae = new EscritorioSebrae(Convert.ToInt32(dr["A004_cd_escr"]));
                    if (dr["A052_cd_usuario"] != DBNull.Value && dr["A052_cd_usuario"] != DBNull.Value && dr["A052_cd_usuario"].ToString() != "")
                        ListaCompra.codUsuario = Convert.ToInt32(dr["A052_cd_usuario"]);
                    if (dr["A001_num_atend"] != DBNull.Value && dr["A001_num_atend"] != DBNull.Value && dr["A001_num_atend"].ToString() != "")
                        ListaCompra.numAtend = Convert.ToInt32(dr["A001_num_atend"]);
                    if (dr["A124_num_seq_pagto"] != DBNull.Value && dr["A124_num_seq_pagto"] != DBNull.Value && dr["A124_num_seq_pagto"].ToString() != "")
                        ListaCompra.numSeqPagto = Convert.ToInt32(dr["A124_num_seq_pagto"]);
                    if (dr["A289_tp_item"] != DBNull.Value && dr["A289_tp_item"] != DBNull.Value && dr["A289_tp_item"].ToString() != "")
                        ListaCompra.tpoItem = Convert.ToString(dr["A289_tp_item"]);
                    if (dr["A289_desc_item"] != DBNull.Value && dr["A289_desc_item"] != DBNull.Value && dr["A289_desc_item"].ToString() != "")
                        ListaCompra.dscItem = Convert.ToString(dr["A289_desc_item"]);
                    if (dr["A289_vlr_item"] != DBNull.Value && dr["A289_vlr_item"] != DBNull.Value && dr["A289_vlr_item"].ToString() != "")
                        ListaCompra.vlrItem = Convert.ToInt32(dr["A289_vlr_item"]);
                    if (dr["A289_qtde_item"] != DBNull.Value && dr["A289_qtde_item"] != DBNull.Value && dr["A289_qtde_item"].ToString() != "")
                        ListaCompra.qtdItem = Convert.ToInt32(dr["A289_qtde_item"]);
                    if (dr["A289_status"] != DBNull.Value && dr["A289_status"] != DBNull.Value && dr["A289_status"].ToString() != "")
                        ListaCompra.dscStatus = Convert.ToString(dr["A289_status"]);
                    if (dr["A289_ind_dispon"] != DBNull.Value && dr["A289_ind_dispon"] != DBNull.Value && dr["A289_ind_dispon"].ToString() != "")
                        ListaCompra.indDisponivel = Convert.ToInt32(dr["A289_ind_dispon"]);
                    if (dr["A289_MEIOENVIO"] != DBNull.Value && dr["A289_MEIOENVIO"] != DBNull.Value && dr["A289_MEIOENVIO"].ToString() != "")
                        ListaCompra.dscMeioEnvio = Convert.ToString(dr["A289_MEIOENVIO"]);
                    if (dr["A289_perc_desconto"] != DBNull.Value && dr["A289_perc_desconto"] != DBNull.Value && dr["A289_perc_desconto"].ToString() != "")
                        ListaCompra.prcDesconto = Convert.ToInt32(dr["A289_perc_desconto"]);
                    if (dr["A289_valor_desconto"] != DBNull.Value && dr["A289_valor_desconto"] != DBNull.Value && dr["A289_valor_desconto"].ToString() != "")
                        ListaCompra.vlrDesconto = Convert.ToInt32(dr["A289_valor_desconto"]);
                    if (dr["A289_observacao"] != DBNull.Value && dr["A289_observacao"] != DBNull.Value && dr["A289_observacao"].ToString() != "")
                        ListaCompra.dscObservacao = Convert.ToString(dr["A289_observacao"]);
                    if (dr["A289_ind_item_aberto"] != DBNull.Value && dr["A289_ind_item_aberto"] != DBNull.Value && dr["A289_ind_item_aberto"].ToString() != "")
                        ListaCompra.indItemAberto = Convert.ToInt32(dr["A289_ind_item_aberto"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        ListaCompra.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                ListaCompra = new ListaCompra();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return ListaCompra;
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

        #region LoadDataBuscaEventos
        /// <summary>
        /// LoadDataBuscaEventos
        /// </summary>
        /// <param name="codEvento">Código do Evento</param>
        /// <param name="codCliente">Código do Clente</param>       
        public static OracleDataReader LoadDataBuscaEventos(int codEvento, int codCliente)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter("nCliente", OracleType.Int32,8),
                new OracleParameter("nEvento", OracleType.Int32,8),
                new OracleParameter("curBusca", OracleType.Cursor)
              };

            parms[0].Value = codCliente;
            parms[1].Value = codEvento;
            parms[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTBUSCA, parms);
            return dr;
        }
        #endregion


        #region LoadDataBuscaPagos
        /// <summary>
        /// LoadDataBuscaEventos
        /// </summary> 
        /// <param name="codCliente">Código do Clente</param>       
        public static OracleDataReader LoadDataBuscaPagos(int NumSeqPagto)
        {
            OracleParameter[] parms = new OracleParameter[] {  
                new OracleParameter("NumSeqPagto", OracleType.Int32,8),
                new OracleParameter("curBusca", OracleType.Cursor)
              };

            parms[0].Value = NumSeqPagto; 
            parms[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTBUSCAPAGOS, parms);
            return dr;
        }
        #endregion
        #endregion

    }
}
