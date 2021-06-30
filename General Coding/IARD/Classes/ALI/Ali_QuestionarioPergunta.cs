using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
using System.Data;
using System.Net.Mail;
using System.Net;

namespace Classes
{
    public class Ali_QuestionarioPergunta // TB0613_ALI_QUEST_PERG
    {
        #region Atributos
        private int codQuestionarioPergunta;
        private Ali_Dimensao dimensao;
        private Ali_Questionario questionario;
        private Ali_Pergunta pergunta;
        private Ali_Diagnostico diagnostico;
        private int numOrdem;
        private int indMultiplaResposta;
        private int indObrigatorio;
        private int indAtivo;
        private DateTime logDtaInclusao;
        private DateTime logDtaAlteracao;
        private Pessoa logCodPessoa;
        #endregion

        #region Propriedades
        public int CodQuestionarioPergunta
        {
            get { return codQuestionarioPergunta; }
            set { codQuestionarioPergunta = value; }
        }
        public Ali_Dimensao Dimensao
        {
            get { return dimensao; }
            set { dimensao = value; }
        }
        public Ali_Questionario Questionario
        {
            get { return questionario; }
            set { questionario = value; }
        }
        public Ali_Pergunta Pergunta
        {
            get { return pergunta; }
            set { pergunta = value; }
        }
        public Ali_Diagnostico Diagnostico
        {
            get { return diagnostico; }
            set { diagnostico = value; }
        }
        public int NumOrdem
        {
            get { return numOrdem; }
            set { numOrdem = value; }
        }
        public int IndMultiplaResposta
        {
            get { return indMultiplaResposta; }
            set { indMultiplaResposta = value; }
        }
        public int IndObrigatorio
        {
            get { return indObrigatorio; }
            set { indObrigatorio = value; }
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
        #endregion

        #region Construtores
        public Ali_QuestionarioPergunta()
            : this(-1)
        { }
        public Ali_QuestionarioPergunta(int codQuestionarioPergunta)
        {
            this.codQuestionarioPergunta = codQuestionarioPergunta;
            this.dimensao = new Ali_Dimensao();
            this.questionario = new Ali_Questionario();
            this.pergunta = new Ali_Pergunta();
            this.diagnostico = new Ali_Diagnostico();
            this.logCodPessoa = new Pessoa();
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "ALI_QUESTIONARIOPERGUNTA.QuestionarioPerguntaInc";
        private const string SPUPDATE = "ALI_QUESTIONARIOPERGUNTA.QuestionarioPerguntaAlt";
        private const string SPDELETE = "ALI_QUESTIONARIOPERGUNTA.QuestionarioPerguntaDel";
        private const string SPSELECTID = "ALI_QUESTIONARIOPERGUNTA.QuestionarioPerguntaSelId";
        private const string SPSELECTPAG = "ALI_QUESTIONARIOPERGUNTA.QuestionarioPerguntaSelPag";
        private const string SPSELECTREL = "ALI_QUESTIONARIOPERGUNTA.QuestionarioPerguntaRelatorio";
        private const string SPSELECTREL2 = "ALI_QUESTIONARIOPERGUNTA.QuestionarioPerguntaRelatorio2";
        #endregion

        #region Parametros Oracle
        private const string PARMCODIGO = "codQuestionarioPergunta";
        private const string PARMCURSOR = "curQuestionarioPergunta";
        private const string PARMCURSORREL = "curQuestPerRelatorio";
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
                    /*1*/ new OracleParameter("codDimensao", OracleType.Int32),
                    /*2*/ new OracleParameter("codQuestionario", OracleType.Int32),
                    /*3*/ new OracleParameter("codPergunta", OracleType.Int32),
                    /*4*/ new OracleParameter("codDiagnostico", OracleType.Int32),
                    /*5*/ new OracleParameter("numOrdem", OracleType.Int32),
                    /*6*/ new OracleParameter("indMultResposta", OracleType.Int32),
                    /*7*/ new OracleParameter("indObrigatorio", OracleType.Int32),
                    /*8*/ new OracleParameter("indAtivo", OracleType.Int32),
                    /*9*/ new OracleParameter("codPessoa", OracleType.Int32)
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
            parms[0].Value = this.codQuestionarioPergunta;
            parms[1].Value = this.dimensao.CodDimensao;
            parms[2].Value = this.questionario.CodQuestionario;
            parms[3].Value = this.pergunta.CodPergunta;
            parms[4].Value = this.diagnostico.CodDiagnostico;
            parms[5].Value = this.numOrdem;
            parms[6].Value = this.indMultiplaResposta;
            parms[7].Value = this.indObrigatorio;
            parms[8].Value = this.indAtivo;
            parms[9].Value = this.logCodPessoa.CodPessoa;
            if (this.codQuestionarioPergunta < 0)
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
                        codQuestionarioPergunta = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
                codQuestionarioPergunta = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
        public static Ali_QuestionarioPergunta GetDataRow(int codigo)
        {
            OracleDataReader dr = LoadDataDr(codigo);
            Ali_QuestionarioPergunta questPergunta = new Ali_QuestionarioPergunta();
            try
            {
                if (dr.Read())
                {
                    questPergunta.codQuestionarioPergunta = Convert.ToInt32(dr["A0612_COD_QUEST"]);
                    questPergunta.dimensao = new Ali_Dimensao(Convert.ToInt32(dr["A0619_COD_DIMENSAO"]));
                    questPergunta.questionario = new Ali_Questionario(Convert.ToInt32(dr["A0612_COD_QUEST"]));
                    questPergunta.pergunta = new Ali_Pergunta(Convert.ToInt32(dr["A0614_COD_PERG"]));
                    questPergunta.diagnostico = new Ali_Diagnostico(Convert.ToInt32(dr["A0655_COD_DIAG"]));
                    questPergunta.numOrdem = Convert.ToInt32(dr["A0613_NR_ORDEM"]);
                    questPergunta.indMultiplaResposta = Convert.ToInt32(dr["A0613_IND_MULT_RESP"]);
                    questPergunta.indObrigatorio = Convert.ToInt32(dr["A0613_IND_OBRIG"]);
                    questPergunta.indAtivo = Convert.ToInt32(dr["A0613_IND_ATIVO"]);
                    questPergunta.logCodPessoa = new Pessoa(Convert.ToInt32(dr["A572_CD_PES_ATZ"]));
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                questPergunta = new Ali_QuestionarioPergunta();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return questPergunta;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>Usuario</returns>
        public static Ali_QuestionarioPergunta GetDataRow(int codigo, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codigo, trans);
            Ali_QuestionarioPergunta questPergunta = new Ali_QuestionarioPergunta();
            try
            {
                if (dr.Read())
                {
                    questPergunta.codQuestionarioPergunta = Convert.ToInt32(dr["A0612_COD_QUEST"]);
                    questPergunta.dimensao = new Ali_Dimensao(Convert.ToInt32(dr["A0619_COD_DIMENSAO"]));
                    questPergunta.questionario = new Ali_Questionario(Convert.ToInt32(dr["A0612_COD_QUEST"]));
                    questPergunta.pergunta = new Ali_Pergunta(Convert.ToInt32(dr["A0614_COD_PERG"]));
                    questPergunta.diagnostico = new Ali_Diagnostico(Convert.ToInt32(dr["A0655_COD_DIAG"]));
                    questPergunta.numOrdem = Convert.ToInt32(dr["A0613_NR_ORDEM"]);
                    questPergunta.indMultiplaResposta = Convert.ToInt32(dr["A0613_IND_MULT_RESP"]);
                    questPergunta.indObrigatorio = Convert.ToInt32(dr["A0613_IND_OBRIG"]);
                    questPergunta.indAtivo = Convert.ToInt32(dr["A0613_IND_ATIVO"]);
                    questPergunta.logCodPessoa = new Pessoa(Convert.ToInt32(dr["A572_CD_PES_ATZ"]));
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                questPergunta = new Ali_QuestionarioPergunta();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return questPergunta;
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

        #region GetDataRowRel

        /// <summary>
        /// LoadDataDr
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static decimal GetDataRowRel(int codDimensao, int codProjeto, int codgrp, int numRep)
        {
            OracleParameter[] param = new OracleParameter[] {
                    new OracleParameter("codDimensao", OracleType.Int32),
                    new OracleParameter("codProjeto", OracleType.Int32),
                    new OracleParameter("codgrp", OracleType.Int32),
                    new OracleParameter("numRep", OracleType.Int32),
                    new OracleParameter(PARMCURSORREL, OracleType.Cursor)
                };

            param[0].Value = codDimensao;
            param[1].Value = codProjeto;
            param[2].Value = codgrp;
            param[3].Value = numRep;
            param[4].Direction = ParameterDirection.Output;

            decimal vlr = 0;
            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTREL, param);
            if (dr.Read())
            {
                if (dr["media"].ToString() != "")
                    vlr = Convert.ToDecimal(dr["media"].ToString());
                else
                    vlr = 0;
            }
            dr.Close();

            return vlr;
        }
        #endregion

        #region GetDataRowRel2

        /// <summary>
        /// LoadDataDr
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static decimal GetDataRowRel2(int codDimensao, int codProjeto, int codgrp)
        {
            OracleParameter[] param = new OracleParameter[] {
                    new OracleParameter("codDimensao", OracleType.Int32),
                    new OracleParameter("codProjeto", OracleType.Int32),
                    new OracleParameter("codgrp", OracleType.Int32),
                    new OracleParameter(PARMCURSORREL, OracleType.Cursor)
                };

            param[0].Value = codDimensao;
            param[1].Value = codProjeto;
            param[2].Value = codgrp;
            param[3].Direction = ParameterDirection.Output;

            decimal vlr = 0;
            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTREL2, param);
            if (dr.Read())
            {
                if (dr["media"].ToString() != "")
                    vlr = Convert.ToDecimal(dr["media"].ToString());
                else
                    vlr = 0;
            }
            dr.Close();

            return vlr;
        }
        #endregion
        #endregion
    }
}
