using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
using System.Data;
using System.Net.Mail;
using System.Net;

namespace Classes
{
    public class Ali_Questionario // TB0612_ALI_QUEST
    {
        #region Atributos
        private int codQuestionario;
        private Ali_Grupo grupo;
        private string dscQuestionario;
        private int numOrdem;
        private int tipoMensagem;
        private string tipoQuestionario;
        private int indAtivo;
        private DateTime logDtaInclusao;
        private DateTime logDtaAlteracao;
        private Pessoa logCodPessoa;
        private string pagina;
        #endregion

        #region Propriedades
        public int CodQuestionario
        {
            get { return codQuestionario; }
            set { codQuestionario = value; }
        }
        public Ali_Grupo Grupo
        {
            get { return grupo; }
            set { grupo = value; }
        }
        public string DscQuestionario
        {
            get { return dscQuestionario; }
            set { dscQuestionario = value; }
        }
        public int NumOrdem
        {
            get { return numOrdem; }
            set { numOrdem = value; }
        }
        public int TipoMensagem
        {
            get { return tipoMensagem; }
            set { tipoMensagem = value; }
        }
        public string TipoQuestionario
        {
            get { return tipoQuestionario; }
            set { tipoQuestionario = value; }
        }
        public int IndAtivo
        {
            get { return indAtivo; }
            set { indAtivo = value; }
        }
        public DateTime LogDtaInclusao
        {
            get { return logDtaInclusao; }
            set { logDtaInclusao = value; }
        }
        public DateTime LogDtaAlteracao
        {
            get { return logDtaAlteracao; }
            set { logDtaAlteracao = value; }
        }
        public Pessoa LogCodPessoa
        {
            get { return logCodPessoa; }
            set { logCodPessoa = value; }
        }
        public string Pagina
        {
            get { return pagina; }
            set { pagina = value; }
        }
        #endregion

        #region Construtores
        public Ali_Questionario()
            : this(-1)
        { }
        public Ali_Questionario(int codQuestionario)
        {
            this.codQuestionario = codQuestionario;
            this.grupo = new Ali_Grupo();
            this.logCodPessoa = new Pessoa();
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "ALI_QUESTIONARIO.QuestionarioInc";
        private const string SPUPDATE = "ALI_QUESTIONARIO.QuestionarioAlt";
        private const string SPDELETE = "ALI_QUESTIONARIO.QuestionarioDel";
        private const string SPSELECTID = "ALI_QUESTIONARIO.QuestionarioSelId";
        private const string SPSELECTPAG = "ALI_QUESTIONARIO.QuestionarioSelPag";
        private const string SPSELECTSUBMENU = "ALI_QUESTIONARIO.QuestionarioSelSubMenu";
        private const string SPSELECTORDEM = "ALI_QUESTIONARIO.QuestionarioSelNumOrdem";
        private const string SPSELECTPAGINA = "ALI_QUESTIONARIO.QuestionarioSelPagina";
        private const string SPSELECTTPMENS = "ALI_QUESTIONARIO.QuestionarioSelNumOrdemTpMens";
        private const string SPSELECTSEL = "ALI_QUESTIONARIO.QuestionarioSel";
        #endregion

        #region Parametros Oracle
        private const string PARMCODIGO = "codQuestionario";
        private const string PARMCURSOR = "curQuestionario";
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
                    /*0*/ new OracleParameter(PARMCODIGO, OracleType.Int32, 4, ParameterDirection.InputOutput.ToString()),
                    /*1*/ new OracleParameter("codGrupo", OracleType.Int32),
                    /*2*/ new OracleParameter("dscQuestionario", OracleType.VarChar),
                    /*3*/ new OracleParameter("numOrdem", OracleType.Int32),
                    /*4*/ new OracleParameter("tpMensagem", OracleType.Int32),
                    /*5*/ new OracleParameter("tpQuestionario", OracleType.VarChar),
                    /*6*/ new OracleParameter("indAtivo", OracleType.Int32),
                    /*7*/ new OracleParameter("codPessoa", OracleType.Int32),
                    /*8*/ new OracleParameter("pagina", OracleType.VarChar)
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
            parms[0].Value = this.codQuestionario;
            parms[1].Value = this.grupo.CodGrupo;
            parms[2].Value = this.dscQuestionario;
            parms[3].Value = this.numOrdem;
            parms[4].Value = this.tipoMensagem;
            parms[5].Value = this.tipoQuestionario;
            parms[6].Value = this.indAtivo;
            parms[7].Value = this.logCodPessoa.CodPessoa;
            parms[8].Value = this.pagina;
            if (this.codQuestionario < 0)
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
                        codQuestionario = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
                codQuestionario = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
        /// <param name="codigo">Código do Registro</param>
        public static void Delete(int codigo)
        {
            OracleParameter[] parms = new OracleParameter[] { new OracleParameter(PARMCODIGO, OracleType.Int32, 4) };
            parms[0].Value = codigo;
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
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(int codigo, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { new OracleParameter(PARMCODIGO, OracleType.Int32, 4) };
                parms[0].Value = codigo;
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
        /// <param name="codigo">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codigo)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCODIGO, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codigo;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codigo, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { new OracleParameter(PARMCODIGO, OracleType.Int32, 4),
                                                                  new OracleParameter(PARMCURSOR, OracleType.Cursor)
            };
            param[0].Value = codigo;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }


        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <returns>Usuario</returns>
        public static Ali_Questionario GetDataRow(int codigo)
        {
            OracleDataReader dr = LoadDataDr(codigo);
            Ali_Questionario questionario = new Ali_Questionario();
            try
            {
                if (dr.Read())
                {
                    questionario.codQuestionario = Convert.ToInt32(dr["A0612_COD_QUEST"]);
                    questionario.grupo = new Ali_Grupo(Convert.ToInt32(dr["A0611_COD_GRP"]));
                    questionario.dscQuestionario = dr["A0612_DSC_QUEST"].ToString();
                    questionario.numOrdem = Convert.ToInt32(dr["A0612_NR_ORDEM"]);
                    if (dr["A0612_TP_MENS"] != DBNull.Value)
                        questionario.tipoMensagem = Convert.ToInt32(dr["A0612_TP_MENS"]);
                    if (dr["A0612_TP_QUEST"] != DBNull.Value)
                        questionario.TipoQuestionario = dr["A0612_TP_QUEST"].ToString();
                    questionario.indAtivo = Convert.ToInt32(dr["A0612_IND_ATIVO"]);
                    if (dr["A572_CD_PES_ATZ"] != DBNull.Value)
                        questionario.logCodPessoa = new Pessoa(Convert.ToInt32(dr["A572_CD_PES_ATZ"]));
                    questionario.pagina = dr["A0612_PAGINA"].ToString();
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                questionario = new Ali_Questionario();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return questionario;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>Usuario</returns>
        public static Ali_Questionario GetDataRow(int codigo, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codigo, trans);
            Ali_Questionario questionario = new Ali_Questionario();
            try
            {
                if (dr.Read())
                {
                    questionario.codQuestionario = Convert.ToInt32(dr["A0612_COD_QUEST"]);
                    questionario.grupo = new Ali_Grupo(Convert.ToInt32(dr["A0611_COD_GRP"]));
                    questionario.dscQuestionario = dr["A0612_DSC_QUEST"].ToString();
                    questionario.numOrdem = Convert.ToInt32(dr["A0612_NR_ORDEM"]);
                    if (dr["A0612_TP_MENS"] != DBNull.Value)
                        questionario.tipoMensagem = Convert.ToInt32(dr["A0612_TP_MENS"]);
                    if (dr["A0612_TP_QUEST"] != DBNull.Value)
                        questionario.TipoQuestionario = dr["A0612_TP_QUEST"].ToString();
                    questionario.indAtivo = Convert.ToInt32(dr["A0612_IND_ATIVO"]);
                    if (dr["A572_CD_PES_ATZ"] != DBNull.Value)
                        questionario.logCodPessoa = new Pessoa(Convert.ToInt32(dr["A572_CD_PES_ATZ"]));
                    questionario.pagina = dr["A0612_PAGINA"].ToString();
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                questionario = new Ali_Questionario();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return questionario;
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

        #region LoadDataDrSubMenu
        /// <summary>
        /// LoadDataDr
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDrSubMenu(int codGrupo)
        {
            OracleParameter[] param = new OracleParameter[] { 
                    new OracleParameter("codGrupo", OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codGrupo;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTSUBMENU, param);
            return dr;
        }
        #endregion

        #region GetDataRowOrdem

        /// <summary>
        /// LoadDataDr
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static Ali_Questionario GetDataRowOrdem(int codGrupo, int numOrdem)
        {
            OracleParameter[] param = new OracleParameter[] {
                    new OracleParameter("codGrupo", OracleType.Int32),
                    new OracleParameter("numOrdem", OracleType.Int32),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codGrupo;
            param[1].Value = numOrdem;
            param[2].Direction = ParameterDirection.Output;

            Ali_Questionario questionario = new Ali_Questionario();
            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTORDEM, param);
            if (dr.Read())
            {
                questionario.codQuestionario = Convert.ToInt32(dr["A0612_COD_QUEST"]);
                questionario.grupo = new Ali_Grupo(Convert.ToInt32(dr["A0611_COD_GRP"]));
                questionario.dscQuestionario = dr["A0612_DSC_QUEST"].ToString();
                questionario.numOrdem = Convert.ToInt32(dr["A0612_NR_ORDEM"]);
                if (dr["A0612_TP_MENS"] != DBNull.Value)
                    questionario.tipoMensagem = Convert.ToInt32(dr["A0612_TP_MENS"]);
                if (dr["A0612_TP_QUEST"] != DBNull.Value)
                    questionario.TipoQuestionario = dr["A0612_TP_QUEST"].ToString();
                questionario.indAtivo = Convert.ToInt32(dr["A0612_IND_ATIVO"]);
                if (dr["A572_CD_PES_ATZ"] != DBNull.Value)
                    questionario.logCodPessoa = new Pessoa(Convert.ToInt32(dr["A572_CD_PES_ATZ"]));
                questionario.pagina = dr["A0612_PAGINA"].ToString();
            }
            dr.Close();

            return questionario;
        }
        #endregion

        #region GetDataRowTpMens

        /// <summary>
        /// LoadDataDr
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static Ali_Questionario GetDataRowTpMens(int codGrupo, int numOrdem, int tpMens)
        {
            OracleParameter[] param = new OracleParameter[] {
                    new OracleParameter("codGrupo", OracleType.Int32),
                    new OracleParameter("numOrdem", OracleType.Int32),
                    new OracleParameter("tpMens", OracleType.Int32),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codGrupo;
            param[1].Value = numOrdem;
            param[2].Value = tpMens;
            param[3].Direction = ParameterDirection.Output;

            Ali_Questionario questionario = new Ali_Questionario();
            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTTPMENS, param);
            if (dr.Read())
            {
                questionario.codQuestionario = Convert.ToInt32(dr["A0612_COD_QUEST"]);
                questionario.grupo = new Ali_Grupo(Convert.ToInt32(dr["A0611_COD_GRP"]));
                questionario.dscQuestionario = dr["A0612_DSC_QUEST"].ToString();
                questionario.numOrdem = Convert.ToInt32(dr["A0612_NR_ORDEM"]);
                if (dr["A0612_TP_MENS"] != DBNull.Value)
                    questionario.tipoMensagem = Convert.ToInt32(dr["A0612_TP_MENS"]);
                if (dr["A0612_TP_QUEST"] != DBNull.Value)
                    questionario.TipoQuestionario = dr["A0612_TP_QUEST"].ToString();
                questionario.indAtivo = Convert.ToInt32(dr["A0612_IND_ATIVO"]);
                if (dr["A572_CD_PES_ATZ"] != DBNull.Value)
                    questionario.logCodPessoa = new Pessoa(Convert.ToInt32(dr["A572_CD_PES_ATZ"]));
                questionario.pagina = dr["A0612_PAGINA"].ToString();
            }
            dr.Close();

            return questionario;
        }
        #endregion

        #region GetPagina
        public static string GetPagina(int numPasso, int numSubPasso)
        {
            OracleParameter[] param = new OracleParameter[] {
                    new OracleParameter("v_611_NR_ORDEM", OracleType.Int32),
                    new OracleParameter("v_612_NR_ORDEM", OracleType.Int32),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = numPasso;
            param[1].Value = numSubPasso;
            param[2].Direction = ParameterDirection.Output;

            string pagina = "";
            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTPAGINA, param);
            if (dr.Read())
            {
                pagina = dr["Pagina"].ToString();
            }
            dr.Close();

            return pagina;
        }
        #endregion

        #region LoadDataSelTotal
        public static OracleDataReader LoadDataSelTotal(int codGrupo)
        {
            OracleParameter[] param = new OracleParameter[] { 
                    new OracleParameter("codGrupo", OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codGrupo;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTSEL, param);
            return dr;
        }
        #endregion

        #region LoadDataDreee

        public static OracleDataReader LoadDataDreee(int codigo)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCODIGO, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codigo;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }


        #endregion

        #endregion
    }
}
