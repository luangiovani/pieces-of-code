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
    public class SalaAgenda // T346_sala_agenda  
    {
        #region Atributos
        private int seqAgenda;
        private SalaVideo numSala;
        private EscritorioSebrae codEscrSebrae;
        private DateTime dtaInicio;
        private string hraInicio;
        private DateTime dtaTermino;
        private string hraTermino;
        private string dscFormatoSala;
        private int? numPessoas;
        private string logUsuario;
        #endregion

        #region Propriedades
        public int SeqAgenda
        {
            get { return seqAgenda; }
            set { seqAgenda = value; }
        }

        public SalaVideo NumSala
        {
            get { return numSala; }
            set { numSala = value; }
        }

        public EscritorioSebrae CodEscrSebrae
        {
            get { return codEscrSebrae; }
            set { codEscrSebrae = value; }
        }

        public DateTime DtaInicio
        {
            get { return dtaInicio; }
            set { dtaInicio = value; }
        }

        public string HraInicio
        {
            get { return hraInicio; }
            set { hraInicio = value; }
        }

        public DateTime DtaTermino
        {
            get { return dtaTermino; }
            set { dtaTermino = value; }
        }

        public string HraTermino
        {
            get { return hraTermino; }
            set { hraTermino = value; }
        }

        public string DscFormatoSala
        {
            get { return dscFormatoSala; }
            set { dscFormatoSala = value; }
        }

        public int? NumPessoas
        {
            get { return numPessoas; }
            set { numPessoas = value; }
        }
        public string LogUsuario
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }
        #endregion

        #region Construtores
        public SalaAgenda()
            : this(-1)
        { }
        public SalaAgenda(int seqAgenda)
        {
            this.seqAgenda = seqAgenda;
            this.numSala = new SalaVideo();
            this.codEscrSebrae = new EscritorioSebrae();
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "SalaAgenda.SalaAgendaInc";
        private const string SPUPDATE = "SalaAgenda.SalaAgendaAlt";
        private const string SPDELETE = "SalaAgenda.SalaAgendaDel";
        private const string SPSELECTID = "SalaAgenda.SalaAgendaSelId";
        private const string SPSELECTPAG = "SalaAgenda.SalaAgendaSelPag";
        private const string SPSELECTEXISTENTE = "SalaAgenda.SalaAgendaSelExistente";
        #endregion

        #region Parametros Oracle
        private const string PARMseqAgenda = "seqAgenda";
        private const string PARMnumSala = "numSala";
        private const string PARMcodEscrSebrae = "codEscrSebrae";
        private const string PARMCURSOR = "curSalaAgenda";
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
                    /*0*/ new OracleParameter(PARMseqAgenda, OracleType.Int32, 4, ParameterDirection.InputOutput.ToString()) ,
                    /*1*/ new OracleParameter(PARMnumSala, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*2*/ new OracleParameter(PARMcodEscrSebrae, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*3*/ new OracleParameter( "dtaInicio", OracleType.DateTime),
                    /*4*/ new OracleParameter( "hraInicio", OracleType.VarChar),
                    /*5*/ new OracleParameter( "dtaTermino", OracleType.DateTime),
                    /*6*/ new OracleParameter( "hraTermino", OracleType.VarChar),
                    /*7*/ new OracleParameter( "dscFormatoSala", OracleType.VarChar),
                    /*8*/ new OracleParameter( "numPessoas", OracleType.Int32),
                    /*9*/ new OracleParameter( "logUsuario", OracleType.VarChar)
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
            parms[0].Value = this.seqAgenda;
            parms[1].Value = this.numSala.NumSala;
            parms[2].Value = this.codEscrSebrae.CodEscrSebrae;
            parms[3].Value = this.dtaInicio;
            parms[4].Value = this.hraInicio;
            parms[5].Value = this.dtaTermino;
            parms[6].Value = this.hraTermino;
            parms[7].Value = "";
            if (this.dscFormatoSala != null)
            { parms[7].Value = this.dscFormatoSala; }
            parms[8].Value = DBNull.Value;
            if (this.numPessoas!=null)
            {parms[8].Value = this.numPessoas;}
            parms[9].Value = this.logUsuario;
            if (this.seqAgenda < 0)
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
                        seqAgenda = Convert.ToInt32(cmd.Parameters[PARMseqAgenda].Value);
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
                seqAgenda = Convert.ToInt32(cmd.Parameters[PARMseqAgenda].Value);
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
        /// <param name="seqAgenda">Código do Registro</param>
        public static void Delete(int seqAgenda, int numSala, int codEscrSebrae)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMseqAgenda, OracleType.Int32, 4)  ,
                new OracleParameter(PARMnumSala, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodEscrSebrae, OracleType.Int32, 4)
            };
            parms[0].Value = seqAgenda;
            parms[1].Value = numSala;
            parms[2].Value = codEscrSebrae;
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
        /// <param name="seqAgenda">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(int seqAgenda, int numSala, int codEscrSebrae, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMseqAgenda, OracleType.Int32, 8)  ,
                new OracleParameter(PARMnumSala, OracleType.Int32, 8)  ,
                new OracleParameter(PARMcodEscrSebrae, OracleType.Int32, 8) 
            };
                parms[0].Value = seqAgenda;
                parms[1].Value = numSala;
                parms[2].Value = codEscrSebrae;
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
        /// <param name="seqAgenda">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int seqAgenda, int numSala, int codEscrSebrae)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMseqAgenda, OracleType.Int32, 4), 
                    new OracleParameter(PARMnumSala, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodEscrSebrae, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = seqAgenda;
            param[1].Value = numSala;
            param[2].Value = codEscrSebrae;
            param[3].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="seqAgenda">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int seqAgenda, int numSala, int codEscrSebrae, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMseqAgenda, OracleType.Int32, 4), 
                    new OracleParameter(PARMnumSala, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodEscrSebrae, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = seqAgenda;
            param[1].Value = numSala;
            param[2].Value = codEscrSebrae;
            param[3].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region LoadDataDr


        /// <summary>
        /// LoadDataDr
        /// </summary>
        /// <param name="seqAgenda">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataSalaExistente(int numSala, int codEscrSebrae,
            int codEvento, DateTime dtaInicio, string hraInicio, DateTime dtaTermino, string	hraTermino)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMseqAgenda, OracleType.Int32, 8), 
                    new OracleParameter(PARMnumSala, OracleType.Int32, 8), 
                    new OracleParameter(PARMcodEscrSebrae, OracleType.Int32, 8), 
                    new OracleParameter("codEvento", OracleType.Int32, 8), 
                    new OracleParameter("dtaInicio", OracleType.DateTime), 
                    new OracleParameter("hraInicio", OracleType.VarChar), 
                    new OracleParameter("dtaTermino", OracleType.DateTime), 
                    new OracleParameter("hraTermino", OracleType.VarChar), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[1].Value = numSala;
            param[2].Value = codEscrSebrae;
            param[3].Value = codEvento;
            param[4].Value = dtaInicio;
            param[5].Value = hraInicio;
            param[6].Value = dtaTermino;
            param[7].Value = hraTermino;
            param[8].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTEXISTENTE, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="seqAgenda">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataSalaExistente(int numSala, int codEscrSebrae,
            int codEvento, DateTime dtaInicio, string hraInicio, DateTime dtaTermino, string hraTermino, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMseqAgenda, OracleType.Int32, 8), 
                    new OracleParameter(PARMnumSala, OracleType.Int32, 8), 
                    new OracleParameter(PARMcodEscrSebrae, OracleType.Int32, 8), 
                    new OracleParameter("codEvento", OracleType.Int32, 8), 
                    new OracleParameter("dtaInicio", OracleType.DateTime), 
                    new OracleParameter("hraInicio", OracleType.VarChar), 
                    new OracleParameter("dtaTermino", OracleType.DateTime), 
                    new OracleParameter("hraTermino", OracleType.VarChar), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[1].Value = numSala;
            param[2].Value = codEscrSebrae;
            param[3].Value = codEvento;
            param[4].Value = dtaInicio;
            param[5].Value = hraInicio;
            param[6].Value = dtaTermino;
            param[7].Value = hraTermino;
            param[8].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTEXISTENTE, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="seqAgenda">Código do Registro</param>
        /// <returns>SalaAgenda</returns>
        public static SalaAgenda GetDataRow(int seqAgenda, int numSala, int codEscrSebrae)
        {
            OracleDataReader dr = LoadDataDr(seqAgenda, numSala, codEscrSebrae);
            SalaAgenda SalaAgenda = new SalaAgenda();
            try
            {
                if (dr.Read())
                {
                    SalaAgenda.seqAgenda = Convert.ToInt32(dr["A346_seq_agenda"]);
                    if (dr["A089_num_sala"] != DBNull.Value && dr["A089_num_sala"] != DBNull.Value && dr["A089_num_sala"].ToString() != "")
                        SalaAgenda.numSala = new SalaVideo(Convert.ToInt32(dr["A089_num_sala"]));
                    if (dr["A004_cd_escr"] != DBNull.Value && dr["A004_cd_escr"] != DBNull.Value && dr["A004_cd_escr"].ToString() != "")
                        SalaAgenda.codEscrSebrae = new EscritorioSebrae(Convert.ToInt32(dr["A004_cd_escr"]));
                    SalaAgenda.dtaInicio = Convert.ToDateTime(dr["A346_dt_inicio"]);
                    SalaAgenda.hraInicio = Convert.ToString(dr["A346_hr_inicio"]);
                    SalaAgenda.dtaTermino = Convert.ToDateTime(dr["A346_dt_termino"]);
                    SalaAgenda.hraTermino = Convert.ToString(dr["A346_hr_termino"]);
                    SalaAgenda.dscFormatoSala = Convert.ToString(dr["A346_formato_sala"]);
                    if (dr["A346_num_pessoas"] != DBNull.Value && dr["A346_num_pessoas"] != DBNull.Value && dr["A346_num_pessoas"].ToString() != "")
                        SalaAgenda.numPessoas = Convert.ToInt32(dr["A346_num_pessoas"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        SalaAgenda.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                SalaAgenda = new SalaAgenda();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return SalaAgenda;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="seqAgenda">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>SalaAgenda</returns>
        public static SalaAgenda GetDataRow(int seqAgenda, int numSala, int codEscrSebrae, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(seqAgenda, numSala, codEscrSebrae, trans);
            SalaAgenda SalaAgenda = new SalaAgenda();
            try
            {
                if (dr.Read())
                {
                    SalaAgenda.seqAgenda = Convert.ToInt32(dr["A346_seq_agenda"]);
                    if (dr["A089_num_sala"] != DBNull.Value && dr["A089_num_sala"] != DBNull.Value && dr["A089_num_sala"].ToString() != "")
                        SalaAgenda.numSala = new SalaVideo(Convert.ToInt32(dr["A089_num_sala"]));
                    if (dr["A004_cd_escr"] != DBNull.Value && dr["A004_cd_escr"] != DBNull.Value && dr["A004_cd_escr"].ToString() != "")
                        SalaAgenda.codEscrSebrae = new EscritorioSebrae(Convert.ToInt32(dr["A004_cd_escr"]));
                    SalaAgenda.dtaInicio = Convert.ToDateTime(dr["A346_dt_inicio"]);
                    SalaAgenda.hraInicio = Convert.ToString(dr["A346_hr_inicio"]);
                    SalaAgenda.dtaTermino = Convert.ToDateTime(dr["A346_dt_termino"]);
                    SalaAgenda.hraTermino = Convert.ToString(dr["A346_hr_termino"]);
                    SalaAgenda.dscFormatoSala = Convert.ToString(dr["A346_formato_sala"]);
                    if (dr["A346_num_pessoas"] != DBNull.Value && dr["A346_num_pessoas"] != DBNull.Value && dr["A346_num_pessoas"].ToString() != "")
                        SalaAgenda.numPessoas = Convert.ToInt32(dr["A346_num_pessoas"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        SalaAgenda.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                SalaAgenda = new SalaAgenda();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return SalaAgenda;
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
