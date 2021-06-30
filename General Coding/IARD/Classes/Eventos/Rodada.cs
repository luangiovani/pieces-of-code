using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
using System.Data;

//-- Classe Rodada
//-- Data : 24/10/2011
//-- Autor :  Denis Douglas Cavalheiro
namespace Classes
{
    public class Rodada
    {
        #region Atributos
        private int codRodada;
        private Eventos codEvento;
        private int tpoReuniao;
        private int tpoEntreReuniao;
        #endregion

        #region Propriedades
        public int CodRodada
        {
            get { return codRodada; }
            set { codRodada = value; }
        }
        public Eventos CodEvento
        {
            get { return codEvento; }
            set { codEvento = value; }
        }
        public int TpoReuniao
        {
            get { return tpoReuniao; }
            set { tpoReuniao = value; }
        }
        public int TpoEntreReuniao
        {
            get { return tpoEntreReuniao; }
            set { tpoEntreReuniao = value; }
        }
        #endregion

        #region Construtores
        public Rodada()
            : this(-1)
        { }
        public Rodada(int codRodada)
        {
            this.codEvento = new Eventos();
            this.codRodada = codRodada;
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "Rodada_Negocio.Rodada_NegocioInc";
        private const string SPUPDATE = "Rodada_Negocio.Rodada_NegocioAlt";
        private const string SPDELETE = "Rodada_Negocio.Rodada_NegocioDel";
        private const string SPSELECTID = "Rodada_Negocio.Rodada_NegocioSelId";
        private const string SPSELECTEVID = "Rodada_Negocio.RODADA_NEGOCIOEVSelId";
        private const string SPSELECTPAG = "Rodada_Negocio.Rodada_NegocioSelPag";
        private const string SPSELECTEV = "Rodada_Negocio.Rodada_NegocioSelEv";
        #endregion

        #region Parametros Oracle
        private const string PARMcodEvento = "codEvento";
        private const string PARMcodRodada = "codRodada";
        private const string PARMtpoReuniao = "tpoReuniao";
        private const string PARMtpoEntreReuniao = "tpoEntreReuniao";
        private const string PARMCURSOR = "curRODADA_NEGOCIO";
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
                    /*0*/ new OracleParameter(PARMcodRodada, OracleType.Int32, 4, ParameterDirection.InputOutput.ToString()) ,
                    /*1*/ new OracleParameter(PARMcodEvento, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*2*/ new OracleParameter(PARMtpoReuniao, OracleType.Int32) ,
                    /*3*/ new OracleParameter(PARMtpoEntreReuniao, OracleType.Int32) 
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
            parms[0].Value = this.codRodada;
            parms[1].Value = this.codEvento.CodEvento;
            parms[2].Value = this.tpoReuniao;
            parms[3].Value = this.tpoEntreReuniao;

            if (this.codRodada < 0)
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
                        codRodada = Convert.ToInt32(cmd.Parameters[PARMcodRodada].Value);
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
                codRodada = Convert.ToInt32(cmd.Parameters[PARMcodRodada].Value);
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
        public static void Delete(int codEvento, int codRodada)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMcodEvento, OracleType.Int32, 4),
                new OracleParameter(PARMcodRodada, OracleType.Int32, 4)
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
        public static void Delete(int codEvento, int codRodada, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMcodEvento, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodRodada, OracleType.Int32, 4)
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
        public static OracleDataReader LoadDataDrEv(int codEvento)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodEvento, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codEvento;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTEVID, param);
            return dr;
        }


        /// <summary>
        /// LoadDataDr
        /// </summary>
        /// <param name="codEvento">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codEvento, int codRodada)
        {
            OracleParameter[] param = new OracleParameter[] { 
                    new OracleParameter(PARMcodRodada, OracleType.Int32, 4),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codRodada;
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
        public static OracleDataReader LoadDataDr(int codEvento, int codRodada, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 
                    new OracleParameter(PARMcodRodada, OracleType.Int32, 4),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };
            
            param[0].Value = codRodada;
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
        public static Rodada GetDataRow(int codEvento, int codRodada)
        {
            OracleDataReader dr = LoadDataDr(codEvento, codRodada);
            Rodada rodada = new Rodada();
            try
            {
                if (dr.Read())
                {
                    rodada.codEvento = new Eventos(Convert.ToInt32(dr["A022_cd_ev"]));
                    if (dr["A930_cd_rodada"] != DBNull.Value && dr["A930_cd_rodada"] != DBNull.Value && dr["A930_cd_rodada"].ToString() != "")
                        rodada.codRodada = Convert.ToInt32(dr["A930_cd_rodada"]);
                    rodada.tpoReuniao = Convert.ToInt32(dr["A930_tpo_reuniao"]);
                    rodada.tpoEntreReuniao = Convert.ToInt32(dr["A930_tpo_entre_reuniao"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                rodada = new Rodada();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return rodada;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codEvento">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>Sessao</returns>
        public static Rodada GetDataRow(int codEvento, int codRodada, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codEvento, codRodada, trans);
            Rodada rodada = new Rodada();
            try
            {
                if (dr.Read())
                {
                    rodada.codEvento = new Eventos(Convert.ToInt32(dr["A022_cd_ev"]));
                    if (dr["A930_cd_rodada"] != DBNull.Value && dr["A930_cd_rodada"] != DBNull.Value && dr["A930_cd_rodada"].ToString() != "")
                        rodada.codRodada = Convert.ToInt32(dr["A930_cd_rodada"]);
                    rodada.tpoReuniao = Convert.ToInt32(dr["A930_tpo_reuniao"]);
                    rodada.tpoEntreReuniao = Convert.ToInt32(dr["A930_tpo_entre_reuniao"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                rodada = new Rodada();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return rodada;
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

        #region LoadDataDrEvento

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

        #region LoadDataDrRodada

        /// <summary>
        /// LoadDataDr
        /// </summary>
        /// <param name="codEvento">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDrRodada(int codRodada)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodRodada, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codRodada;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }
        #endregion

        #endregion
    }
}
