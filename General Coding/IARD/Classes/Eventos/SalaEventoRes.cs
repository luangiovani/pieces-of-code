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
    public class SalaEventoRes // T138_SALA_EVENTO_RES
    {
        #region Atributos
        private int codReserva;
        private SalaAgenda seqAgenda;
        private SalaAgenda numSala;
        private EscritorioSebrae codEscrSebrae;
        private Eventos codEvento;
        private string logUsuario;
        #endregion

        #region Propriedades
        public Eventos CodEvento
        {
            get { return codEvento; }
            set { codEvento = value; }
        }
        public EscritorioSebrae CodEscrSebrae
        {
            get { return codEscrSebrae; }
            set { codEscrSebrae = value; }
        }
        public SalaAgenda NumSala
        {
            get { return numSala; }
            set { numSala = value; }
        }
        public SalaAgenda SeqAgenda
        {
            get { return seqAgenda; }
            set { seqAgenda = value; }
        }
        public int CodReserva
        {
            get { return codReserva; }
            set { codReserva = value; }
        }
        public string LogUsuario
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }
        #endregion

        #region Construtores
        public SalaEventoRes()
            : this(-1)
        { }
        public SalaEventoRes(int codReserva)
        {
            this.codReserva = codReserva;
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "SalaEventoRes.SalaEventoResInc";
        private const string SPUPDATE = "SalaEventoRes.SalaEventoResAlt";
        private const string SPDELETE = "SalaEventoRes.SalaEventoResDel";
        private const string SPSELECTID = "SalaEventoRes.SalaEventoResSelId";
        private const string SPSELECTPAG = "SalaEventoRes.SalaEventoResSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMcodReserva = "codReserva";
        private const string PARMseqAgenda = "seqAgenda";
        private const string PARMnumSala = "numSala";
        private const string PARMcodEscrSebrae = "codEscrSebrae";
        private const string PARMcodEvento = "codEvento";
        private const string PARMCURSOR = "curSalaEventoRes";
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
                    /*0*/ new OracleParameter(PARMcodReserva, OracleType.Int32, 4, ParameterDirection.InputOutput.ToString()) ,
                    /*1*/ new OracleParameter(PARMseqAgenda, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*2*/ new OracleParameter(PARMnumSala, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*3*/ new OracleParameter(PARMcodEscrSebrae, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*4*/ new OracleParameter(PARMcodEvento, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
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
            parms[0].Value = this.codReserva;
            parms[1].Value = this.seqAgenda.SeqAgenda;
            parms[2].Value = this.seqAgenda.NumSala.NumSala;
            parms[3].Value = this.codEscrSebrae.CodEscrSebrae;
            parms[4].Value = this.codEvento.CodEvento;
            parms[5].Value = this.logUsuario;
            if (this.codReserva < 0)
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
                        codReserva = Convert.ToInt32(cmd.Parameters[PARMcodReserva].Value);
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
                codReserva = Convert.ToInt32(cmd.Parameters[PARMcodReserva].Value);
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
        /// <param name="codReserva">Código do Registro</param>
        public static void Delete(int codReserva, int seqAgenda, int numSala, int codEscrSebrae, int codEvento)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMcodReserva, OracleType.Int32, 4),
                new OracleParameter(PARMseqAgenda, OracleType.Int32, 4),
                new OracleParameter(PARMnumSala, OracleType.Int32, 4),
                new OracleParameter(PARMcodEscrSebrae, OracleType.Int32, 4),
                new OracleParameter(PARMcodEvento, OracleType.Int32, 4)
            };
            parms[0].Value = codReserva;
            parms[1].Value = seqAgenda;
            parms[2].Value = numSala;
            parms[3].Value = codEscrSebrae;
            parms[4].Value = codEvento;

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
        /// <param name="codReserva">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(int codReserva, int seqAgenda, int numSala, int codEscrSebrae, int codEvento, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMcodReserva, OracleType.Int32, 4),
                new OracleParameter(PARMseqAgenda, OracleType.Int32, 4),
                new OracleParameter(PARMnumSala, OracleType.Int32, 4),
                new OracleParameter(PARMcodEscrSebrae, OracleType.Int32, 4),
                new OracleParameter(PARMcodEvento, OracleType.Int32, 4)
            };
                parms[0].Value = codReserva;
                parms[1].Value = seqAgenda;
                parms[2].Value = numSala;
                parms[3].Value = codEscrSebrae;
                parms[4].Value = codEvento;

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
        /// <param name="codReserva">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codReserva, int seqAgenda, int numSala, int codEscrSebrae, int codEvento)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodReserva, OracleType.Int32, 4), 
                    new OracleParameter(PARMseqAgenda, OracleType.Int32, 4), 
                    new OracleParameter(PARMnumSala, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodEscrSebrae, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodEvento, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codReserva;
            param[1].Value = seqAgenda;
            param[2].Value = numSala;
            param[3].Value = codEscrSebrae;
            param[4].Value = codEvento;
            param[5].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="codReserva">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codReserva, int seqAgenda, int numSala, int codEscrSebrae, int codEvento, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodReserva, OracleType.Int32, 4), 
                    new OracleParameter(PARMseqAgenda, OracleType.Int32, 4), 
                    new OracleParameter(PARMnumSala, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodEscrSebrae, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodEvento, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codReserva;
            param[1].Value = seqAgenda;
            param[2].Value = numSala;
            param[3].Value = codEscrSebrae;
            param[4].Value = codEvento;
            param[5].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="codReserva">Código do Registro</param>
        /// <returns>SalaEventoRes</returns>
        public static SalaEventoRes GetDataRow(int codReserva, int seqAgenda, int numSala, int codEscrSebrae, int codEvento)
        {
            OracleDataReader dr = LoadDataDr(codReserva, seqAgenda, numSala, codEscrSebrae, codEvento);
            SalaEventoRes SalaEventoRes = new SalaEventoRes();
            try
            {
                if (dr.Read())
                {
                    SalaEventoRes.codReserva = Convert.ToInt32(dr["A217_cd_compr"]);
                    if (dr["A346_seq_agenda"] != DBNull.Value && dr["A346_seq_agenda"] != DBNull.Value && dr["A346_seq_agenda"].ToString() != "")
                        SalaEventoRes.seqAgenda = new SalaAgenda(Convert.ToInt32(dr["A346_seq_agenda"]));
                    if (dr["A089_num_sala"] != DBNull.Value && dr["A089_num_sala"] != DBNull.Value && dr["A089_num_sala"].ToString() != "")
                        SalaEventoRes.numSala = new SalaAgenda(Convert.ToInt32(dr["A089_num_sala"]));
                    if (dr["A004_cd_escr"] != DBNull.Value && dr["A004_cd_escr"] != DBNull.Value && dr["A004_cd_escr"].ToString() != "")
                        SalaEventoRes.codEscrSebrae = new EscritorioSebrae(Convert.ToInt32(dr["A004_cd_escr"]));
                    if (dr["A022_cd_Ev"] != DBNull.Value && dr["A022_cd_Ev"] != DBNull.Value && dr["A022_cd_Ev"].ToString() != "")
                        SalaEventoRes.codEvento = new Eventos(Convert.ToInt32(dr["A022_cd_Ev"]));
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        SalaEventoRes.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                SalaEventoRes = new SalaEventoRes();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return SalaEventoRes;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codReserva">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>SalaEventoRes</returns>
        public static SalaEventoRes GetDataRow(int codReserva, int seqAgenda, int numSala, int codEscrSebrae, int codEvento, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codReserva, seqAgenda, numSala, codEscrSebrae, codEvento, trans);
            SalaEventoRes SalaEventoRes = new SalaEventoRes();
            try
            {
                if (dr.Read())
                {
                    SalaEventoRes.codReserva = Convert.ToInt32(dr["A217_cd_compr"]);
                    if (dr["A346_seq_agenda"] != DBNull.Value && dr["A346_seq_agenda"] != DBNull.Value && dr["A346_seq_agenda"].ToString() != "")
                        SalaEventoRes.seqAgenda = new SalaAgenda(Convert.ToInt32(dr["A346_seq_agenda"]));
                    if (dr["A089_num_sala"] != DBNull.Value && dr["A089_num_sala"] != DBNull.Value && dr["A089_num_sala"].ToString() != "")
                        SalaEventoRes.numSala = new SalaAgenda(Convert.ToInt32(dr["A089_num_sala"]));
                    if (dr["A004_cd_escr"] != DBNull.Value && dr["A004_cd_escr"] != DBNull.Value && dr["A004_cd_escr"].ToString() != "")
                        SalaEventoRes.codEscrSebrae = new EscritorioSebrae(Convert.ToInt32(dr["A004_cd_escr"]));
                    if (dr["A022_cd_Ev"] != DBNull.Value && dr["A022_cd_Ev"] != DBNull.Value && dr["A022_cd_Ev"].ToString() != "")
                        SalaEventoRes.codEvento = new Eventos(Convert.ToInt32(dr["A022_cd_Ev"]));
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        SalaEventoRes.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                SalaEventoRes = new SalaEventoRes();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return SalaEventoRes;
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
