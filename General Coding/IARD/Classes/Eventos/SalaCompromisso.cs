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
    public class SalaCompromisso // T217_compromisso
    {
        #region Atributos
        private int codCompromisso;
        private SalaAgenda seqAgenda;
        private SalaVideo numSala;
        private EscritorioSebrae codEscrSebrae;
        private int? codUsuario;
        private string dscCompr;
        private int indDisp;
        private string logUsuario;
        #endregion

        #region Propriedades
        public int CodCompromisso
        {
            get { return codCompromisso; }
            set { codCompromisso = value; }
        }
        public SalaAgenda SeqAgenda
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
        public int? CodUsuario
        {
            get { return codUsuario; }
            set { codUsuario = value; }
        }
        public string DscCompr
        {
            get { return dscCompr; }
            set { dscCompr = value; }
        }
        public int IndDisp
        {
            get { return indDisp; }
            set { indDisp = value; }
        }
        public string LogUsuario
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }
        #endregion

        #region Construtores
        public SalaCompromisso()
            : this(-1)
        { }
        public SalaCompromisso(int codCompromisso)
        {
            this.codCompromisso = codCompromisso;
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "SalaCompromisso.SalaCompromissoInc";
        private const string SPUPDATE = "SalaCompromisso.SalaCompromissoAlt";
        private const string SPDELETE = "SalaCompromisso.SalaCompromissoDel";
        private const string SPSELECTID = "SalaCompromisso.SalaCompromissoSelId";
        private const string SPSELECTPAG = "SalaCompromisso.SalaCompromissoSelPag";
        private const string SPSELECTPAGSala = "SalaCompromisso.SalasCompromissoId";
        #endregion

        #region Parametros Oracle
        private const string PARMcodCompromisso = "codCompromisso";
        private const string PARMseqAgenda = "seqAgenda";
        private const string PARMnumSala = "numSala";
        private const string PARMcodEscrSebrae = "codEscrSebrae";
        private const string PARMCURSOR = "curSalaCompromisso";
        #endregion

        #region Metodos


        #region GetParameters
        public static OracleParameter[] GetParameters()
        {
            OracleParameter[] parms;

            // Tentando buscar os parameters do cache        
            parms = DataBase.GetCachedParameters(SPINSERT);
            //parms = OutputCacheParameters(SPINSERT);
            //if (parms == null)
            //{
                parms = new OracleParameter[]{ 
                    /*3*/ new OracleParameter(PARMcodEscrSebrae, OracleType.Int32, 8, ParameterDirection.Input.ToString()) ,
                    /*2*/ new OracleParameter(PARMnumSala, OracleType.Int32, 8, ParameterDirection.Input.ToString()) ,
                    /*1*/ new OracleParameter(PARMseqAgenda, OracleType.Int32, 8, ParameterDirection.Input.ToString()) ,
                    /*0*/ new OracleParameter(PARMcodCompromisso, OracleType.Int32, 8, ParameterDirection.InputOutput.ToString()) ,
                    /*4*/ new OracleParameter( "codUsuario", OracleType.Int32),
                    /*5*/ new OracleParameter( "dscCompr", OracleType.VarChar),
                    /*6*/ new OracleParameter( "indDisp", OracleType.Int32),
                    /*7*/ new OracleParameter( "logUsuario", OracleType.VarChar)
                };

                // Criando cache dos parameters 
                DataBase.CacheParameters(SPINSERT, parms);
            //}
            return (parms);
        }

        #endregion

        #region SetParameters
        public void SetParameters(OracleParameter[] parms)
        {
            parms[3].Value = this.codCompromisso;
            parms[2].Value = this.seqAgenda.SeqAgenda;
            parms[1].Value = this.numSala.NumSala;
            parms[0].Value = this.codEscrSebrae.CodEscrSebrae;
            if (this.codUsuario != null && this.codUsuario.ToString() != "")
            {
                parms[4].Value = this.codUsuario;
            }
            else
            {
                parms[4].Value = DBNull.Value;
            }

            parms[5].Value = this.dscCompr;
            parms[6].Value = this.indDisp;
            parms[7].Value = this.logUsuario;
            if (this.codCompromisso < 0)
            {
                parms[3].Direction = ParameterDirection.Output;
            }
            else
            {
                parms[3].Direction = ParameterDirection.Input;
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
                        codCompromisso = Convert.ToInt32(cmd.Parameters[PARMcodCompromisso].Value);
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
                codCompromisso = Convert.ToInt32(cmd.Parameters[PARMcodCompromisso].Value);
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
        /// <param name="codCompromisso">Código do Registro</param>
        public static void Delete(int codCompromisso, int seqAgenda, int numSala, int codEscrSebrae)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMcodCompromisso, OracleType.Int32, 4),
                new OracleParameter(PARMseqAgenda, OracleType.Int32, 4),
                new OracleParameter(PARMnumSala, OracleType.Int32, 4),
                new OracleParameter(PARMcodEscrSebrae, OracleType.Int32, 4)
            };
            parms[0].Value = codCompromisso;
            parms[1].Value = seqAgenda;
            parms[2].Value = numSala;
            parms[3].Value = codEscrSebrae;

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
        /// <param name="codCompromisso">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(int codCompromisso, int seqAgenda, int numSala, int codEscrSebrae, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMcodCompromisso, OracleType.Int32, 4),
                new OracleParameter(PARMseqAgenda, OracleType.Int32, 4),
                new OracleParameter(PARMnumSala, OracleType.Int32, 4),
                new OracleParameter(PARMcodEscrSebrae, OracleType.Int32, 4)
            };
                parms[0].Value = codCompromisso;
                parms[1].Value = seqAgenda;
                parms[2].Value = numSala;
                parms[3].Value = codEscrSebrae;

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
        /// <param name="codCompromisso">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codCompromisso, int seqAgenda, int numSala, int codEscrSebrae)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodCompromisso, OracleType.Int32, 4), 
                    new OracleParameter(PARMseqAgenda, OracleType.Int32, 4), 
                    new OracleParameter(PARMnumSala, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodEscrSebrae, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codCompromisso;
            param[1].Value = seqAgenda;
            param[2].Value = numSala;
            param[3].Value = codEscrSebrae;
            param[4].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="codCompromisso">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codCompromisso, int seqAgenda, int numSala, int codEscrSebrae, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodCompromisso, OracleType.Int32, 4), 
                    new OracleParameter(PARMseqAgenda, OracleType.Int32, 4), 
                    new OracleParameter(PARMnumSala, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodEscrSebrae, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codCompromisso;
            param[1].Value = seqAgenda;
            param[2].Value = numSala;
            param[3].Value = codEscrSebrae;
            param[4].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="codCompromisso">Código do Registro</param>
        /// <returns>SalaCompromisso</returns>
        public static SalaCompromisso GetDataRow(int codCompromisso, int seqAgenda, int numSala, int codEscrSebrae)
        {
            OracleDataReader dr = LoadDataDr(codCompromisso, seqAgenda, numSala, codEscrSebrae);
            SalaCompromisso SalaCompromisso = new SalaCompromisso();
            try
            {
                if (dr.Read())
                {
                    SalaCompromisso.codCompromisso = Convert.ToInt32(dr["A217_cd_compr"]);
                    if (dr["A346_seq_agenda"] != DBNull.Value && dr["A346_seq_agenda"] != DBNull.Value && dr["A346_seq_agenda"].ToString() != "")
                        SalaCompromisso.seqAgenda = new SalaAgenda(Convert.ToInt32(dr["A346_seq_agenda"]));
                    if (dr["A089_num_sala"] != DBNull.Value && dr["A089_num_sala"] != DBNull.Value && dr["A089_num_sala"].ToString() != "")
                        SalaCompromisso.numSala = new SalaVideo(Convert.ToInt32(dr["A089_num_sala"]));
                    if (dr["A004_cd_escr"] != DBNull.Value && dr["A004_cd_escr"] != DBNull.Value && dr["A004_cd_escr"].ToString() != "")
                        SalaCompromisso.codEscrSebrae = new EscritorioSebrae(Convert.ToInt32(dr["A004_cd_escr"]));
                    SalaCompromisso.codUsuario = Convert.ToInt32(dr["A052_cd_usuario"]);
                    SalaCompromisso.dscCompr = Convert.ToString(dr["A217_desc_compr"]);
                    SalaCompromisso.indDisp = Convert.ToInt32(dr["A217_ind_disp"]);

                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        SalaCompromisso.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                SalaCompromisso = new SalaCompromisso();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return SalaCompromisso;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codCompromisso">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>SalaCompromisso</returns>
        public static SalaCompromisso GetDataRow(int codCompromisso, int seqAgenda, int numSala, int codEscrSebrae, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codCompromisso, seqAgenda, numSala, codEscrSebrae, trans);
            SalaCompromisso SalaCompromisso = new SalaCompromisso();
            try
            {
                if (dr.Read())
                {
                    SalaCompromisso.codCompromisso = Convert.ToInt32(dr["A217_cd_compr"]);
                    if (dr["A346_seq_agenda"] != DBNull.Value && dr["A346_seq_agenda"] != DBNull.Value && dr["A346_seq_agenda"].ToString() != "")
                        SalaCompromisso.seqAgenda = new SalaAgenda(Convert.ToInt32(dr["A346_seq_agenda"]));
                    if (dr["A089_num_sala"] != DBNull.Value && dr["A089_num_sala"] != DBNull.Value && dr["A089_num_sala"].ToString() != "")
                        SalaCompromisso.numSala = new SalaVideo(Convert.ToInt32(dr["A089_num_sala"]));
                    if (dr["A004_cd_escr"] != DBNull.Value && dr["A004_cd_escr"] != DBNull.Value && dr["A004_cd_escr"].ToString() != "")
                        SalaCompromisso.codEscrSebrae = new EscritorioSebrae(Convert.ToInt32(dr["A004_cd_escr"]));
                    SalaCompromisso.codUsuario = Convert.ToInt32(dr["A052_cd_usuario"]);
                    SalaCompromisso.dscCompr = Convert.ToString(dr["A217_desc_compr"]);
                    SalaCompromisso.indDisp = Convert.ToInt32(dr["A217_ind_disp"]);

                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        SalaCompromisso.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                SalaCompromisso = new SalaCompromisso();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return SalaCompromisso;
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

        #region LoadDataPagSalaComp
        public static Paginacao LoadDataPagSalaComp(string Where, int PaginaCorrente, int TamanhoPagina, string ExpressaoOrdenacao)
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

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTPAGSala, parms);

            paginacao.DataReader = dr;
            //  traz o total de registros de retorno direto dos parametros do ExecuteReader---
            paginacao.TotalRegistros = Convert.ToInt32(parms[5].Value);
            return paginacao;
        }
        
        #endregion

        #endregion

    }
}
