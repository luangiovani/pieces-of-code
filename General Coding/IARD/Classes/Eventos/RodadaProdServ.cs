using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
using System.Data;

//-- Classe Rodada Produtos e Serviços
//-- Data : 24/10/2011
//-- Autor :  Denis Douglas Cavalheiro
namespace Classes
{
    public class RodadaProdServ
    {
        #region Atributos
        private int codProdServ;
        private string dscProdServ;
        #endregion

        #region Propriedades
        public int CodProdServ
        {
            get { return codProdServ; }
            set { codProdServ = value; }
        }
        public string DscProdServ
        {
            get { return dscProdServ.ToUpper(); }
            set { dscProdServ = value.ToUpper(); }
        }
        #endregion

        #region Construtores
        public RodadaProdServ()
            : this(-1)
        { }
        public RodadaProdServ(int codProdServ)
        {
            this.codProdServ = codProdServ;
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "Rodada_Prod_Serv.Rodada_Prod_ServInc";
        private const string SPUPDATE = "Rodada_Prod_Serv.Rodada_Prod_ServAlt";
        private const string SPDELETE = "Rodada_Prod_Serv.Rodada_Prod_ServDel";
        private const string SPSELECTID = "Rodada_Prod_Serv.Rodada_Prod_ServSelId";
        private const string SPSELECTPAG = "Rodada_Prod_Serv.Rodada_Prod_ServSelPag";
        private const string SPSELECTEV = "Rodada_Prod_Serv.Rodada_Prod_ServSelEv";
        #endregion

        #region Parametros Oracle
        private const string PARMcodProdServ = "codProdServ";
        private const string PARMdscProdServ = "dscProdServ";
        private const string PARMCURSOR = "curRODADA_PROD_SERV";
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
                    /*0*/ new OracleParameter(PARMcodProdServ, OracleType.Int32, 4, ParameterDirection.InputOutput.ToString()) ,
                    /*1*/ new OracleParameter(PARMdscProdServ, OracleType.VarChar)
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
            parms[0].Value = this.codProdServ;
            parms[1].Value = this.dscProdServ;

            if (this.codProdServ < 0)
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
                        codProdServ = Convert.ToInt32(cmd.Parameters[PARMcodProdServ].Value);
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
                codProdServ = Convert.ToInt32(cmd.Parameters[PARMcodProdServ].Value);
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
        /// <param name="codEvento">Código do Registro</param>
        public static void Delete(int codProdServ)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMcodProdServ, OracleType.Int32, 4),
            };
            parms[0].Value = codProdServ;
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
        /// <param name="codEvento">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(int codProdServ, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMcodProdServ, OracleType.Int32, 4)  ,
            };
                parms[0].Value = codProdServ;
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
        /// <param name="codEvento">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codProdServ)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodProdServ, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codProdServ;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="codEvento">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codProdServ, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodProdServ, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };
            
            param[0].Value = codProdServ;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="codEvento">Código do Registro</param>
        /// <returns>Sessao</returns>
        public static RodadaProdServ GetDataRow(int codProdServ)
        {
            OracleDataReader dr = LoadDataDr(codProdServ);
            RodadaProdServ rodadaProdServ = new RodadaProdServ();
            try
            {
                if (dr.Read())
                {
                    rodadaProdServ.codProdServ = Convert.ToInt32(dr["A934_cd_prod_serv"]);
                    rodadaProdServ.dscProdServ = Convert.ToString(dr["A934_dsc_prod_serv"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                rodadaProdServ = new RodadaProdServ();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return rodadaProdServ;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codEvento">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>Sessao</returns>
        public static RodadaProdServ GetDataRow(int codProdServ, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codProdServ, trans);
            RodadaProdServ rodadaProdServ = new RodadaProdServ();
            try
            {
                if (dr.Read())
                {
                    rodadaProdServ.codProdServ = Convert.ToInt32(dr["A934_cd_prod_serv"]);
                    rodadaProdServ.dscProdServ = Convert.ToString(dr["A934_dsc_prod_serv"]);

                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                rodadaProdServ = new RodadaProdServ();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return rodadaProdServ;
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
