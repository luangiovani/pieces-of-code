using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
 
using System.Data;

//-- Classe Classes Sebrae
//-- Data : 24/09/2007 
//-- Autor :  Honorato

namespace Classes
{
    public class ProdutoEmpresa // T043_PROD_EMPR 
    {
        #region Atributos
        private TipoProd tipProd;
        private Produto codProduto;
        private Cliente codCliente;
        private int numSeqImp;
        private int? indIndispBolsa;
        private string logUsuario;
        #endregion

        #region Propriedades
        public TipoProd TipProd
        {
            get { return tipProd; }
            set { tipProd = value; }
        }
        public Produto CodProduto
        {
            get { return codProduto; }
            set { codProduto = value; }
        }
        public Cliente CodCliente
        {
            get { return codCliente; }
            set { codCliente = value; }
        }
        public int NumSeqImp
        {
            get { return numSeqImp; }
            set { numSeqImp = value; }
        }
        public int? IndIndispBolsa
        {
            get { return indIndispBolsa; }
            set { indIndispBolsa = value; }
        }
        public string LogUsuario 
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }
        #endregion

        #region Construtores
        public ProdutoEmpresa()
            : this(-1)
        { }
        public ProdutoEmpresa(int tipProd)
        {
            this.tipProd = new TipoProd(); 
            this.codProduto = new Produto(); 
            this.codCliente = new Cliente();
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "ProdutoEmpresa.ProdutoEmpresaInc";
        private const string SPUPDATE = "ProdutoEmpresa.ProdutoEmpresaAlt";
        private const string SPDELETE = "ProdutoEmpresa.ProdutoEmpresaDel";
        private const string SPSELECTID = "ProdutoEmpresa.ProdutoEmpresaSelId";
        private const string SPSELECTPAG = "ProdutoEmpresa.ProdutoEmpresaSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMtipProd = "tipProd";
        private const string PARMcodProduto = "codProduto";
        private const string PARMcodCliente = "codCliente";
        private const string PARMCURSOR = "curProdutoEmpresa";
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
                    /*0*/ new OracleParameter(PARMtipProd, OracleType.VarChar),
                    /*1*/ new OracleParameter(PARMcodProduto, OracleType.Int32),
                    /*2*/ new OracleParameter(PARMcodCliente, OracleType.Int32),
                    /*3*/ new OracleParameter( "numSeqImp", OracleType.Int32),
                    /*4*/ new OracleParameter( "indIndispBolsa", OracleType.Int32),
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
            parms[0].Value = this.tipProd.TipProd.ToUpper();
            parms[1].Value = this.codProduto.CodProduto;
            parms[2].Value = this.codCliente.CodCLIENTE;
            parms[3].Value = this.numSeqImp;
            parms[4].Value = DBNull.Value;
            if (this.indIndispBolsa != null)
            { parms[4].Value = this.indIndispBolsa;  }
            parms[5].Value = this.logUsuario.ToUpper();
            
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
                        //tipProd = Convert.ToInt32(cmd.Parameters[PARMtipProd].Value);
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
                //tipProd = Convert.ToInt32(cmd.Parameters[PARMtipProd].Value);
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
        /// <param name="tipProd">Código do Registro</param>
        public static void Delete(string tipProd, int codProduto, int codCliente)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMtipProd, OracleType.VarChar)  ,
                new OracleParameter(PARMcodProduto, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodCliente, OracleType.Int32, 4)
            };
            parms[0].Value = tipProd;
            parms[1].Value = codProduto;
            parms[2].Value = codCliente;
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
        /// <param name="tipProd">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(string tipProd, int codProduto, int codCliente, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMtipProd, OracleType.VarChar)  ,
                new OracleParameter(PARMcodProduto, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodCliente, OracleType.Int32, 4) 
            };
                parms[0].Value = tipProd;
                parms[1].Value = codProduto;
                parms[2].Value = codCliente;
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
        /// <param name="tipProd">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int tipProd, int codProduto, int codCliente)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMtipProd, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodProduto, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodCliente, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = tipProd;
            param[1].Value = codProduto;
            param[2].Value = codCliente;
            param[3].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="tipProd">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int tipProd, int codProduto, int codCliente, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMtipProd, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodProduto, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodCliente, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = tipProd;
            param[1].Value = codProduto;
            param[2].Value = codCliente;
            param[3].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="tipProd">Código do Registro</param>
        /// <returns>ProdutoEmpresa</returns>
        public static ProdutoEmpresa GetDataRow(int tipProd, int codProduto, int codCliente)
        {
            OracleDataReader dr = LoadDataDr(tipProd, codProduto, codCliente);
            ProdutoEmpresa ProdutoEmpresa = new ProdutoEmpresa();
            try
            {
                if (dr.Read())
                {
                    ProdutoEmpresa.tipProd = new TipoProd(Convert.ToString(dr["A066_tp_prod"]));
                    if (dr["A041_cd_prod"] != DBNull.Value && dr["A041_cd_prod"] != DBNull.Value && dr["A041_cd_prod"].ToString() != "")
                        ProdutoEmpresa.codProduto = new Produto(Convert.ToInt32(dr["A041_cd_prod"]));
                    if (dr["A012_cd_cli"] != DBNull.Value && dr["A012_cd_cli"] != DBNull.Value && dr["A012_cd_cli"].ToString() != "")
                        ProdutoEmpresa.codCliente = new Cliente(Convert.ToInt32(dr["A012_cd_cli"]));
                    if (dr["A043_num_seq_imp"] != DBNull.Value && dr["A043_num_seq_imp"] != DBNull.Value && dr["A043_num_seq_imp"].ToString() != "")
                        ProdutoEmpresa.numSeqImp = Convert.ToInt32(dr["A043_num_seq_imp"]);
                    if (dr["A043_ind_indisp_bolsa"] != DBNull.Value && dr["A043_ind_indisp_bolsa"] != DBNull.Value && dr["A043_ind_indisp_bolsa"].ToString() != "")
                        ProdutoEmpresa.indIndispBolsa = Convert.ToInt32(dr["A043_ind_indisp_bolsa"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        ProdutoEmpresa.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                ProdutoEmpresa = new ProdutoEmpresa();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return ProdutoEmpresa;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="tipProd">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>ProdutoEmpresa</returns>
        public static ProdutoEmpresa GetDataRow(int tipProd, int codProduto, int codCliente, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(tipProd, codProduto, codCliente, trans);
            ProdutoEmpresa ProdutoEmpresa = new ProdutoEmpresa();
            try
            {
                if (dr.Read())
                {
                    ProdutoEmpresa.tipProd = new TipoProd(Convert.ToString(dr["A066_tp_prod"]));
                    if (dr["A041_cd_prod"] != DBNull.Value && dr["A041_cd_prod"] != DBNull.Value && dr["A041_cd_prod"].ToString() != "")
                        ProdutoEmpresa.codProduto = new Produto(Convert.ToInt32(dr["A041_cd_prod"]));
                    if (dr["A012_cd_cli"] != DBNull.Value && dr["A012_cd_cli"] != DBNull.Value && dr["A012_cd_cli"].ToString() != "")
                        ProdutoEmpresa.codCliente = new Cliente(Convert.ToInt32(dr["A012_cd_cli"]));
                    if (dr["A043_num_seq_imp"] != DBNull.Value && dr["A043_num_seq_imp"] != DBNull.Value && dr["A043_num_seq_imp"].ToString() != "")
                        ProdutoEmpresa.numSeqImp = Convert.ToInt32(dr["A043_num_seq_imp"]);
                    if (dr["A043_ind_indisp_bolsa"] != DBNull.Value && dr["A043_ind_indisp_bolsa"] != DBNull.Value && dr["A043_ind_indisp_bolsa"].ToString() != "")
                        ProdutoEmpresa.indIndispBolsa = Convert.ToInt32(dr["A043_ind_indisp_bolsa"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        ProdutoEmpresa.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                ProdutoEmpresa = new ProdutoEmpresa();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return ProdutoEmpresa;
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


        #region Métodos Específicos

        #region LoadDataDrClienteComissao
        public static OracleDataReader LoadDataDrClienteComissao(int CodCli)
        {
            string where = " and A012_cd_cli = " + CodCli;
            Paginacao dr = ProdutoEmpresa.LoadDataPaginacao(where, 1, 100, "2");

            return dr.DataReader;
        }
        #endregion

        #region LoadDataDrClienteCusto
        public static OracleDataReader LoadDataDrClienteCusto(int CodCli)
        {
            string where = " and A012_cd_cli = " + CodCli;
            Paginacao dr = ProdutoEmpresa.LoadDataPaginacao( where, 1, 100, "2");

            return dr.DataReader;
        }
        #endregion
        
        #endregion

    }
}
