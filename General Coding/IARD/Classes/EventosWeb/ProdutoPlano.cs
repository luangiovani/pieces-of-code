using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;

using System.Data;

//-- Classe Classes Sebrae
//-- Data : 31/10/2007 
//-- Autor :  Honorato

namespace Classes
{
    public class ProdutoPlano // T416_PROD_PLANO 
    {
        #region Atributos
        private int codPeriodicidade;
        private ProdutoSebrae codProdSebrae;
        private int codNivel;
        private int codPlano;
        private string logUsuario;
        #endregion

        #region Propriedades

        public ProdutoSebrae CodProdSebrae
        {
            get { return codProdSebrae; }
            set { codProdSebrae = value; }
        }

        public int CodNivel
        {
            get { return codNivel; }
            set { codNivel = value; }
        }

        public int CodPlano
        {
            get { return codPlano; }
            set { codPlano = value; }
        }
        public int CodPeriodicidade
        {
            get { return codPeriodicidade; }
            set { codPeriodicidade = value; }
        }
        public string LogUsuario
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }
        #endregion

        #region Construtores
        public ProdutoPlano()
            : this(-1)
        { }
        public ProdutoPlano(int codPeriodicidade)
        {
            this.codPeriodicidade = codPeriodicidade;
            this.codProdSebrae = new ProdutoSebrae();
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "ProdutoPlano.ProdutoPlanoInc";
        private const string SPUPDATE = "ProdutoPlano.ProdutoPlanoAlt";
        private const string SPDELETE = "ProdutoPlano.ProdutoPlanoDel";
        private const string SPSELECTID = "ProdutoPlano.ProdutoPlanoSelId";
        private const string SPSELECTPAG = "ProdutoPlano.ProdutoPlanoSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMcodPeriodicidade = "codPeriodicidade";
        private const string PARMcodProdSebrae = "codProdSebrae";
        private const string PARMCURSOR = "curProdutoPlano";
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
                    /*0*/ new OracleParameter(PARMcodPeriodicidade, OracleType.Int32, 4, ParameterDirection.InputOutput.ToString()) ,
                    /*1*/ new OracleParameter(PARMcodProdSebrae, OracleType.VarChar, 4, ParameterDirection.Input.ToString()) ,
                    /*2*/ new OracleParameter( "codNivel", OracleType.Int32),
                    /*3*/ new OracleParameter( "codPlano", OracleType.Int32),
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
            parms[0].Value = this.codPeriodicidade;
            parms[1].Value = this.codProdSebrae.CodProdSebrae;
            parms[2].Value = this.codNivel;
            parms[3].Value = this.codPlano;
            parms[4].Value = this.logUsuario;
            if (this.codPeriodicidade < 0)
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
                        codPeriodicidade = Convert.ToInt32(cmd.Parameters[PARMcodPeriodicidade].Value);
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
                codPeriodicidade = Convert.ToInt32(cmd.Parameters[PARMcodPeriodicidade].Value);
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
        /// <param name="codPeriodicidade">Código do Registro</param>
        public static void Delete(int codPeriodicidade, string codProdSebrae)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMcodPeriodicidade, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodProdSebrae, OracleType.VarChar, 4)
            };
            parms[0].Value = codPeriodicidade;
            parms[1].Value = codProdSebrae;
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
        /// <param name="codPeriodicidade">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(int codPeriodicidade, string codProdSebrae, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMcodPeriodicidade, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodProdSebrae, OracleType.VarChar, 4)
            };
                parms[0].Value = codPeriodicidade;
                parms[1].Value = codProdSebrae;
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
        /// <param name="codPeriodicidade">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codPeriodicidade, string codProdSebrae)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodPeriodicidade, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodProdSebrae, OracleType.VarChar, 4),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codPeriodicidade;
            param[1].Value = codProdSebrae;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="codPeriodicidade">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codPeriodicidade, string codProdSebrae, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodPeriodicidade, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodProdSebrae, OracleType.VarChar, 4),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codPeriodicidade;
            param[1].Value = codProdSebrae;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="codPeriodicidade">Código do Registro</param>
        /// <returns>ProdutoPlano</returns>
        public static ProdutoPlano GetDataRow(int codPeriodicidade, string codProdSebrae)
        {
            OracleDataReader dr = LoadDataDr(codPeriodicidade, codProdSebrae);
            ProdutoPlano ProdutoPlano = new ProdutoPlano();
            try
            {
                if (dr.Read())
                {
                    ProdutoPlano.codPeriodicidade = Convert.ToInt32(dr["A411_CD_PERIODICIDADE"]);
                    if (dr["A042_cd_prod_seb"] != DBNull.Value && dr["A042_cd_prod_seb"] != DBNull.Value && dr["A042_cd_prod_seb"].ToString() != "")
                        ProdutoPlano.codProdSebrae = new ProdutoSebrae(Convert.ToString(dr["A042_cd_prod_seb"]));
                    ProdutoPlano.codNivel = Convert.ToInt32(dr["A412_CD_NIVEL"]);
                    ProdutoPlano.codPlano = Convert.ToInt32(dr["A414_CD_PLANO"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        ProdutoPlano.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                ProdutoPlano = new ProdutoPlano();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return ProdutoPlano;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codPeriodicidade">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>ProdutoPlano</returns>
        public static ProdutoPlano GetDataRow(int codPeriodicidade, string codProdSebrae, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codPeriodicidade, codProdSebrae, trans);
            ProdutoPlano ProdutoPlano = new ProdutoPlano();
            try
            {
                if (dr.Read())
                {
                    ProdutoPlano.codPeriodicidade = Convert.ToInt32(dr["A411_CD_PERIODICIDADE"]);
                    if (dr["A042_cd_prod_seb"] != DBNull.Value && dr["A042_cd_prod_seb"] != DBNull.Value && dr["A042_cd_prod_seb"].ToString() != "")
                        ProdutoPlano.codProdSebrae = new ProdutoSebrae(Convert.ToString(dr["A042_cd_prod_seb"]));
                    ProdutoPlano.codNivel = Convert.ToInt32(dr["A412_CD_NIVEL"]);
                    ProdutoPlano.codPlano = Convert.ToInt32(dr["A414_CD_PLANO"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        ProdutoPlano.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                ProdutoPlano = new ProdutoPlano();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return ProdutoPlano;
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