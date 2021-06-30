using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
 
using System.Data;

//-- Classe Classes Sebrae
//-- Data : 
//-- Autor :  Automatico

namespace Classes
{
    public class Sessao //  
    {
        #region Atributos
        private int codSessao;
        private Eventos codEvento;
        private int numMesas;
        private int indBloqueado;
        private string logUsuario;
        #endregion

        #region Propriedades
        public int CodSessao
        {
            get { return codSessao; }
            set { codSessao = value; }
        }
        public Eventos CodEvento
        {
            get { return codEvento; }
            set { codEvento = value; }
        }
        public int NumMesas
        {
            get { return numMesas; }
            set { numMesas = value; }
        }
        public int IndBloqueado
        {
            get { return indBloqueado; }
            set { indBloqueado = value; }
        }
        public string LogUsuario
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }
        #endregion

        #region Construtores
        public Sessao()
            : this(-1)
        { }
        public Sessao(int codSessao)
        {
            this.codEvento = new Eventos();
            this.codSessao = codSessao;
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "Sessao.SessaoInc";
        private const string SPUPDATE = "Sessao.SessaoAlt";
        private const string SPDELETE = "Sessao.SessaoDel";
        private const string SPSELECTID = "Sessao.SessaoSelId";
        private const string SPSELECTPAG = "Sessao.SessaoSelPag";
        private const string SPSELECTEV = "Sessao.SessaoSelEv";
        #endregion

        #region Parametros Oracle
        private const string PARMcodEvento = "codEvento";
        private const string PARMcodSessao = "codSessao";
        private const string PARMnumMesas = "numMesas";
        private const string PARMindBloqueado = "indBloqueado";
        private const string PARMCURSOR = "curSessao";
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
                    /*0*/ new OracleParameter(PARMcodSessao, OracleType.Int32, 4, ParameterDirection.InputOutput.ToString()) ,
                    /*1*/ new OracleParameter(PARMcodEvento, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*2*/ new OracleParameter(PARMnumMesas, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*3*/ new OracleParameter(PARMindBloqueado, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*4*/ new OracleParameter("logUsuario", OracleType.VarChar)
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
            parms[0].Value = this.codSessao;
            parms[1].Value = this.codEvento.CodEvento;
            parms[2].Value = this.numMesas;
            parms[3].Value = this.indBloqueado;
            parms[4].Value = this.logUsuario;

            if (this.codSessao < 0)
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
                        codSessao = Convert.ToInt32(cmd.Parameters[PARMcodSessao].Value);
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
                codSessao = Convert.ToInt32(cmd.Parameters[PARMcodSessao].Value);
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
        public static void Delete(int codEvento, int codSessao)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMcodEvento, OracleType.Int32, 4),
                new OracleParameter(PARMcodSessao, OracleType.Int32, 4)
            };
            parms[0].Value = codEvento;
            parms[1].Value = 0;
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
        public static void Delete(int codEvento, int codSessao, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMcodEvento, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodSessao, OracleType.Int32, 4)
            };
                parms[0].Value = codEvento;
                parms[1].Value = 0;
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
        public static OracleDataReader LoadDataDr(int codEvento, int codSessao)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodEvento, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodSessao, OracleType.Int32, 4),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codEvento;
            param[1].Value = codSessao;
            param[2].Direction = ParameterDirection.Output;

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
        public static OracleDataReader LoadDataDr(int codEvento, int codSessao, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodEvento, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodSessao, OracleType.Int32, 4),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codEvento;
            param[1].Value = codSessao;
            param[2].Direction = ParameterDirection.Output;

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
        public static Sessao GetDataRow(int codEvento, int codSessao)
        {
            OracleDataReader dr = LoadDataDr(codEvento, codSessao);
            Sessao Sessao = new Sessao();
            try
            {
                if (dr.Read())
                {
                    Sessao.codEvento = new Eventos(Convert.ToInt32(dr["A022_cd_ev"]));
                    if (dr["A787_cd_sessao"] != DBNull.Value && dr["A787_cd_sessao"] != DBNull.Value && dr["A787_cd_sessao"].ToString() != "")
                        Sessao.codSessao = Convert.ToInt32(dr["A787_cd_sessao"]);
                    Sessao.numMesas = Convert.ToInt32(dr["A787_num_mesas"]);
                    if (dr["A787_ind_bloqueado"] != DBNull.Value && dr["A787_ind_bloqueado"].ToString() != "")
                        Sessao.indBloqueado = Convert.ToInt32(dr["A787_ind_bloqueado"]);
                    else
                        Sessao.indBloqueado = 0;
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        Sessao.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                Sessao = new Sessao();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return Sessao;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codEvento">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>Sessao</returns>
        public static Sessao GetDataRow(int codEvento, int codSessao, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codEvento, codSessao, trans);
            Sessao Sessao = new Sessao();
            try
            {
                if (dr.Read())
                {
                    Sessao.codEvento = new Eventos(Convert.ToInt32(dr["A022_cd_ev"]));
                    if (dr["A787_cd_sessao"] != DBNull.Value && dr["A787_cd_sessao"] != DBNull.Value && dr["A787_cd_sessao"].ToString() != "")
                        Sessao.codSessao = Convert.ToInt32(dr["A787_cd_sessao"]);
                    Sessao.numMesas = Convert.ToInt32(dr["A787_num_mesas"]);
                    if (dr["A787_ind_bloqueado"] != DBNull.Value && dr["A787_ind_bloqueado"].ToString() != "")
                        Sessao.indBloqueado = Convert.ToInt32(dr["A787_ind_bloqueado"]);
                    else
                        Sessao.indBloqueado = 0;
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        Sessao.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                Sessao = new Sessao();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return Sessao;
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

        #region LoadDataDr

        /// <summary>
        /// LoadDataDr
        /// </summary>
        /// <param name="codEvento">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDrEvento(int codEvento)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodEvento, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codEvento;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTEV, param);
            return dr;
        }
        #endregion

        #endregion

    }
}
