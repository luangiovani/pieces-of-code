using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;

using System.Data;

//-- Classe Classes Sebrae
//-- Data : 11/07/2007 
//-- Autor :  Honorato

namespace Classes
{
    public class SalaVideo // T089_SALA_VIDEO
    {
        #region Atributos
        private int numSala;
        private EscritorioSebrae codEscrSebrae;
        private int numVagas;
        private string tpoSala;
        private string dscSala;
        private int? indDesat;
        private string logUsuario;
        #endregion

        #region Propriedades
        public EscritorioSebrae CodEscrSebrae
        {
            get { return codEscrSebrae; }
            set { codEscrSebrae = value; }
        }
        public int NumSala
        {
            get { return numSala; }
            set { numSala = value; }
        }
        public int? IndDesat
        {
            get { return indDesat; }
            set { indDesat = value; }
        }
        public string DscSala
        {
            get { return dscSala; }
            set { dscSala = value; }
        }
        public string TpoSala
        {
            get { return tpoSala; }
            set { tpoSala = value; }
        }
        public int NumVagas
        {
            get { return numVagas; }
            set { numVagas = value; }
        }
        public string LogUsuario
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }
        #endregion

        #region Construtores
        public SalaVideo()
            : this(-1)
        { }
        public SalaVideo(int numSala)
        {
            this.numSala = numSala;
            this.codEscrSebrae = new EscritorioSebrae();
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "SalaVideo.SalaVideoInc";
        private const string SPUPDATE = "SalaVideo.SalaVideoAlt";
        private const string SPDELETE = "SalaVideo.SalaVideoDel";
        private const string SPSELECTID = "SalaVideo.SalaVideoSelId";
        private const string SPSELECTPAG = "SalaVideo.SalaVideoSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMnumSala = "numSala";
        private const string PARMcodEscrSebrae = "codEscrSebrae";
        private const string PARMCURSOR = "curSalaVideo";
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
                    /*0*/ new OracleParameter(PARMnumSala, OracleType.Int32, 4, ParameterDirection.InputOutput.ToString()) ,
                    /*1*/ new OracleParameter(PARMcodEscrSebrae, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*2*/ new OracleParameter( "numVagas;", OracleType.Int32),
                    /*3*/ new OracleParameter( "tpoSala", OracleType.VarChar),
                    /*4*/ new OracleParameter( "dscSala", OracleType.VarChar),
                    /*5*/ new OracleParameter( "indDesat", OracleType.Int32),
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
            parms[0].Value = this.numSala;
            parms[1].Value = this.codEscrSebrae;
            parms[2].Value = this.numVagas;
            parms[3].Value = this.tpoSala;
            parms[4].Value = this.dscSala;
            parms[5].Value = this.indDesat;
            parms[6].Value = this.logUsuario;
            if (this.numSala < 0)
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
                        numSala = Convert.ToInt32(cmd.Parameters[PARMnumSala].Value);
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
                numSala = Convert.ToInt32(cmd.Parameters[PARMnumSala].Value);
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
        /// <param name="numSala">Código do Registro</param>
        public static void Delete(int numSala, int codEscrSebrae)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMnumSala, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodEscrSebrae, OracleType.Int32, 4)
            };
            parms[0].Value = numSala;
            parms[1].Value = codEscrSebrae;
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
        /// <param name="numSala">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(int numSala, int codEscrSebrae, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMnumSala, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodEscrSebrae, OracleType.Int32, 4)
            };
                parms[0].Value = numSala;
                parms[1].Value = codEscrSebrae;
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
        /// <param name="numSala">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int numSala, int codEscrSebrae)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMnumSala, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodEscrSebrae, OracleType.Int32, 4),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = numSala;
            param[1].Value = codEscrSebrae;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="numSala">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int numSala, int codEscrSebrae, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMnumSala, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodEscrSebrae, OracleType.Int32, 4),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = numSala;
            param[1].Value = codEscrSebrae;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="numSala">Código do Registro</param>
        /// <returns>SalaVideo</returns>
        public static SalaVideo GetDataRow(int numSala, int codEscrSebrae)
        {
            OracleDataReader dr = LoadDataDr(numSala, codEscrSebrae);
            SalaVideo SalaVideo = new SalaVideo();
            try
            {
                if (dr.Read())
                {
                    SalaVideo.numSala = Convert.ToInt32(dr["A089_num_sala"]);
                    if (dr["A004_cd_escr"] != DBNull.Value && dr["A004_cd_escr"] != DBNull.Value && dr["A004_cd_escr"].ToString() != "")
                        SalaVideo.codEscrSebrae = new EscritorioSebrae(Convert.ToInt32(dr["A004_cd_escr"]));
                    SalaVideo.numVagas = Convert.ToInt32(dr["A089_num_vagas"]);
                    SalaVideo.tpoSala = Convert.ToString(dr["A089_tp_sala"]);
                    SalaVideo.dscSala = Convert.ToString(dr["A089_desc_sala"]);
                    SalaVideo.indDesat = Convert.ToInt32(dr["A089_ind_desat"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        SalaVideo.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                SalaVideo = new SalaVideo();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return SalaVideo;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="numSala">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>SalaVideo</returns>
        public static SalaVideo GetDataRow(int numSala, int codEscrSebrae, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(numSala, codEscrSebrae, trans);
            SalaVideo SalaVideo = new SalaVideo();
            try
            {
                if (dr.Read())
                {
                    SalaVideo.numSala = Convert.ToInt32(dr["A089_num_sala"]);
                    if (dr["A004_cd_escr"] != DBNull.Value && dr["A004_cd_escr"] != DBNull.Value && dr["A004_cd_escr"].ToString() != "")
                        SalaVideo.codEscrSebrae = new EscritorioSebrae(Convert.ToInt32(dr["A004_cd_escr"]));
                    SalaVideo.numVagas = Convert.ToInt32(dr["A089_num_vagas"]);
                    SalaVideo.tpoSala = Convert.ToString(dr["A089_tp_sala"]);
                    SalaVideo.dscSala = Convert.ToString(dr["A089_desc_sala"]);
                    SalaVideo.indDesat = Convert.ToInt32(dr["A089_ind_desat"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        SalaVideo.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                SalaVideo = new SalaVideo();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return SalaVideo;
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
