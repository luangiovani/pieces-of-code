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
    public class ProdutoTipo // T091_PROD_TIPO 
    {
        #region Atributos
        private Produto codProduto;
        private TipoProd tipProd;
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
        public string LogUsuario
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }
        #endregion

        #region Construtores
        public ProdutoTipo()
            : this(-1)
        { }
        public ProdutoTipo(int codProduto)
        {
            this.codProduto = new Produto();
            this.tipProd = new TipoProd();
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "ProdutoTipo.ProdutoTipoInc";
        private const string SPUPDATE = "ProdutoTipo.ProdutoTipoAlt";
        private const string SPDELETE = "ProdutoTipo.ProdutoTipoDel";
        private const string SPSELECTID = "ProdutoTipo.ProdutoTipoSelId";
        private const string SPSELECTPAG = "ProdutoTipo.ProdutoTipoSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMcodProduto = "codProduto";
        private const string PARMtipProd = "tipProd";
        private const string PARMCURSOR = "curProdutoTipo";
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
                    /*0*/ new OracleParameter(PARMcodProduto, OracleType.Int32, 4, ParameterDirection.InputOutput.ToString()) ,
                    /*1*/ new OracleParameter(PARMtipProd, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
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
            parms[0].Value = this.codProduto.CodProduto;
            parms[1].Value = this.tipProd.TipProd;
            parms[2].Value = this.logUsuario;
            if (this.codProduto.CodProduto < 0)
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
                        codProduto = new Produto(Convert.ToInt32(cmd.Parameters[PARMcodProduto].Value));
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
                codProduto = new Produto(Convert.ToInt32(cmd.Parameters[PARMcodProduto].Value));
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
        /// <param name="codProduto">Código do Registro</param>
        public static void Delete(int codProduto, int tipProd)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMcodProduto, OracleType.Int32, 4)  ,
                new OracleParameter(PARMtipProd, OracleType.Int32, 4)
            };
            parms[0].Value = codProduto;
            parms[1].Value = tipProd;
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
        /// <param name="codProduto">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(int codProduto, int tipProd, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMcodProduto, OracleType.Int32, 4)  ,
                new OracleParameter(PARMtipProd, OracleType.Int32, 4)
            };
                parms[0].Value = codProduto;
                parms[1].Value = tipProd;
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
        /// <param name="codProduto">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codProduto, int tipProd)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodProduto, OracleType.Int32, 4), 
                    new OracleParameter(PARMtipProd, OracleType.Int32, 4),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codProduto;
            param[1].Value = tipProd;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="codProduto">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codProduto, int tipProd, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodProduto, OracleType.Int32, 4), 
                    new OracleParameter(PARMtipProd, OracleType.Int32, 4),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codProduto;
            param[1].Value = tipProd;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="codProduto">Código do Registro</param>
        /// <returns>ProdutoTipo</returns>
        public static ProdutoTipo GetDataRow(int codProduto, int tipProd)
        {
            OracleDataReader dr = LoadDataDr(codProduto, tipProd);
            ProdutoTipo ProdutoTipo = new ProdutoTipo();
            try
            {
                if (dr.Read())
                {
                    ProdutoTipo.codProduto = new Produto(Convert.ToInt32(dr["A041_cd_prod"]));
                    if (dr["A066_tp_prod"] != DBNull.Value && dr["A066_tp_prod"] != DBNull.Value && dr["A066_tp_prod"].ToString() != "")
                        ProdutoTipo.tipProd = new TipoProd(Convert.ToString(dr["A066_tp_prod"]));
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        ProdutoTipo.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                ProdutoTipo = new ProdutoTipo();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return ProdutoTipo;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codProduto">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>ProdutoTipo</returns>
        public static ProdutoTipo GetDataRow(int codProduto, int tipProd, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codProduto, tipProd, trans);
            ProdutoTipo ProdutoTipo = new ProdutoTipo();
            try
            {
                if (dr.Read())
                {
                    ProdutoTipo.codProduto = new Produto(Convert.ToInt32(dr["A041_cd_prod"]));
                    if (dr["A066_tp_prod"] != DBNull.Value && dr["A066_tp_prod"] != DBNull.Value && dr["A066_tp_prod"].ToString() != "")
                        ProdutoTipo.tipProd = new TipoProd(Convert.ToString(dr["A066_tp_prod"]));
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        ProdutoTipo.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                ProdutoTipo = new ProdutoTipo();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return ProdutoTipo;
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