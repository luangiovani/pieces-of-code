using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
 
using System.Data;

//-- Classe Classes Sebrae
//-- Data : 01/11/2007 
//-- Autor :  Honorato

namespace Classes
{
    public class Metrica // T785_METRICA 
    {
        #region Atributos
        private string dscTipoMetrica;
        private Instrumento codInstrumento;
        private Categoria codCategoria;
        private Abordagem codAbordagem;
        private string logUsuario;
        #endregion

        #region Propriedades
        public string LogUsuario
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }
        #endregion

        #region Construtores
        public Metrica()
            : this("")
        { }
        public Metrica(string dscTipoMetrica)
        {
            this.dscTipoMetrica = dscTipoMetrica;
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "Metrica.MetricaInc";
        private const string SPUPDATE = "Metrica.MetricaAlt";
        private const string SPDELETE = "Metrica.MetricaDel";
        private const string SPSELECTID = "Metrica.MetricaSelId";
        private const string SPSELECTPAG = "Metrica.MetricaSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMdscTipoMetrica = "dscTipoMetrica";
        private const string PARMcodInstrumento = "codInstrumento";
        private const string PARMcodCategoria = "codCategoria";
        private const string PARMcodAbordagem = "codAbordagem";
        private const string PARMCURSOR = "curMetrica";
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
                    /*0*/ new OracleParameter(PARMdscTipoMetrica, OracleType.VarChar, 4, ParameterDirection.InputOutput.ToString()) ,
                    /*1*/ new OracleParameter(PARMcodInstrumento, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*2*/ new OracleParameter(PARMcodCategoria, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*3*/ new OracleParameter(PARMcodAbordagem, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*6*/ new OracleParameter( "logUsuario", OracleType.VarChar)
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
            parms[0].Value = this.dscTipoMetrica;
            parms[1].Value = this.codInstrumento.CodInstrumento;
            parms[2].Value = this.codCategoria.CodCategoria;
            parms[3].Value = this.codAbordagem.CodAbordagem;
            parms[4].Value = this.logUsuario;
            if (this.dscTipoMetrica == "" )
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
                        dscTipoMetrica = Convert.ToString(cmd.Parameters[PARMdscTipoMetrica].Value);
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
                dscTipoMetrica = Convert.ToString(cmd.Parameters[PARMdscTipoMetrica].Value);
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
        /// <param name="dscTipoMetrica">Código do Registro</param>
        public static void Delete(string dscTipoMetrica, int codInstrumento, int codCategoria, int codAbordagem)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMdscTipoMetrica, OracleType.VarChar, 4),
                new OracleParameter(PARMcodInstrumento, OracleType.Int32, 4),
                new OracleParameter(PARMcodCategoria, OracleType.Int32, 4),
                new OracleParameter(PARMcodAbordagem, OracleType.Int32, 4)
            };
            parms[0].Value = dscTipoMetrica;
            parms[1].Value = codInstrumento;
            parms[2].Value = codCategoria;
            parms[3].Value = codAbordagem;

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
        /// <param name="dscTipoMetrica">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(string dscTipoMetrica, int codInstrumento, int codCategoria, int codAbordagem, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMdscTipoMetrica, OracleType.VarChar, 4),
                new OracleParameter(PARMcodInstrumento, OracleType.Int32, 4),
                new OracleParameter(PARMcodCategoria, OracleType.Int32, 4),
                new OracleParameter(PARMcodAbordagem, OracleType.Int32, 4)
            };
                parms[0].Value = dscTipoMetrica;
                parms[1].Value = codInstrumento;
                parms[2].Value = codCategoria;
                parms[3].Value = codAbordagem;

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
        /// <param name="dscTipoMetrica">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(string dscTipoMetrica, int codInstrumento, int codCategoria, int codAbordagem)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMdscTipoMetrica, OracleType.VarChar, 4), 
                    new OracleParameter(PARMcodInstrumento, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodCategoria, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodAbordagem, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = dscTipoMetrica;
            param[1].Value = codInstrumento;
            param[2].Value = codCategoria;
            param[3].Value = codAbordagem;
            param[4].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="dscTipoMetrica">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(string dscTipoMetrica, int codInstrumento, int codCategoria, int codAbordagem, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMdscTipoMetrica, OracleType.VarChar, 4), 
                    new OracleParameter(PARMcodInstrumento, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodCategoria, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodAbordagem, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = dscTipoMetrica;
            param[1].Value = codInstrumento;
            param[2].Value = codCategoria;
            param[3].Value = codAbordagem;
            param[4].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="dscTipoMetrica">Código do Registro</param>
        /// <returns>Metrica</returns>
        public static Metrica GetDataRow(string dscTipoMetrica, int codInstrumento, int codCategoria, int codAbordagem)
        {
            OracleDataReader dr = LoadDataDr(dscTipoMetrica, codInstrumento, codCategoria, codAbordagem);
            Metrica Metrica = new Metrica();
            try
            {
                if (dr.Read())
                {
                    Metrica.dscTipoMetrica = Convert.ToString(dr["A785_TIPO"]);
                    Metrica.codInstrumento = new Instrumento(Convert.ToInt32(dr["A786_cd_instrumento"]));
                    Metrica.codCategoria = new Categoria(Convert.ToInt32(dr["A784_cd_categoria"]));
                    Metrica.codAbordagem = new Abordagem(Convert.ToInt32(dr["A783_cd_abordagem"]));
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        Metrica.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                Metrica = new Metrica();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return Metrica;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="dscTipoMetrica">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>Metrica</returns>
        public static Metrica GetDataRow(string dscTipoMetrica, int codInstrumento, int codCategoria, int codAbordagem, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(dscTipoMetrica, codInstrumento, codCategoria, codAbordagem, trans);
            Metrica Metrica = new Metrica();
            try
            {
                if (dr.Read())
                {
                    Metrica.dscTipoMetrica = Convert.ToString(dr["A785_TIPO"]);
                    Metrica.codInstrumento = new Instrumento(Convert.ToInt32(dr["A786_cd_instrumento"]));
                    Metrica.codCategoria = new Categoria(Convert.ToInt32(dr["A784_cd_categoria"]));
                    Metrica.codAbordagem = new Abordagem(Convert.ToInt32(dr["A783_cd_abordagem"]));
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        Metrica.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                Metrica = new Metrica();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return Metrica;
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