using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
using System.Data;
using System.Net.Mail;
using System.Net;

namespace Classes
{
    public class Ali_PerguntaResposta // TB0616_ALI_PERG_RESP
    {
        #region Atributos
        private int codPerguntaResposta;
        private Ali_QuestionarioPergunta questionarioPergunta;
        private Ali_Resposta resposta;
        private int indDescritivo;
        private int numOrdem;
        private string tipoCampo;
        private int vlrResposta;
        private int indAtivo;
        private DateTime logDtaInclusao;
        private DateTime logDtaAlteracao;
        private Pessoa logCodPessoa;
        #endregion

        #region Propriedades
        public int CodPerguntaResposta
        {
            get { return codPerguntaResposta; }
            set { codPerguntaResposta = value; }
        }
        public Ali_QuestionarioPergunta QuestionarioPergunta
        {
            get { return questionarioPergunta; }
            set { questionarioPergunta = value; }
        }
        public Ali_Resposta Resposta
        {
            get { return resposta; }
            set { resposta = value; }
        }
        public int IndDescritivo
        {
            get { return indDescritivo; }
            set { indDescritivo = value; }
        }
        public int NumOrdem
        {
            get { return numOrdem; }
            set { numOrdem = value; }
        }
        public string TipoCampo
        {
            get { return tipoCampo; }
            set { tipoCampo = value; }
        }
        public int VlrResposta
        {
            get { return vlrResposta; }
            set { vlrResposta = value; }
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
        public Ali_PerguntaResposta()
            : this(-1)
        { }
        public Ali_PerguntaResposta(int codPerguntaResposta)
        {
            this.codPerguntaResposta = codPerguntaResposta;
            this.questionarioPergunta = new Ali_QuestionarioPergunta();
            this.resposta = new Ali_Resposta();
            this.logCodPessoa = new Pessoa();
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "ALI_PERGUNTARESPOSTA.PerguntaRespostaInc";
        private const string SPUPDATE = "ALI_PERGUNTARESPOSTA.PerguntaRespostaAlt";
        private const string SPDELETE = "ALI_PERGUNTARESPOSTA.PerguntaRespostaDel";
        private const string SPSELECTID = "ALI_PERGUNTARESPOSTA.PerguntaRespostaSelId";
        private const string SPSELECTPAG = "ALI_PERGUNTARESPOSTA.PerguntaRespostaSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMCODIGO = "codPerguntaResposta";
        private const string PARMCURSOR = "curPerguntaResposta";
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
                    /*1*/ new OracleParameter("codQuestionarioPergunta", OracleType.Int32),
                    /*2*/ new OracleParameter("codResposta", OracleType.Int32),
                    /*3*/ new OracleParameter("indDescritivo", OracleType.Int32),
                    /*4*/ new OracleParameter("numOrdem", OracleType.Int32),
                    /*5*/ new OracleParameter("tpCampo", OracleType.VarChar),
                    /*6*/ new OracleParameter("vlrResposta", OracleType.Int32),
                    /*7*/ new OracleParameter("indAtivo", OracleType.Int32),
                    /*8*/ new OracleParameter("codPessoa", OracleType.Int32)
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
            parms[0].Value = this.codPerguntaResposta;
            parms[1].Value = this.questionarioPergunta.CodQuestionarioPergunta;
            parms[2].Value = this.resposta.CodResposta;
            parms[3].Value = this.indDescritivo;
            parms[4].Value = this.numOrdem;
            parms[5].Value = this.tipoCampo;
            parms[6].Value = this.vlrResposta;
            parms[7].Value = this.indAtivo;
            parms[8].Value = this.logCodPessoa.CodPessoa;
            if (this.CodPerguntaResposta < 0)
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
                        codPerguntaResposta = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
                codPerguntaResposta = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
        public static Ali_PerguntaResposta GetDataRow(int codigo)
        {
            OracleDataReader dr = LoadDataDr(codigo);
            Ali_PerguntaResposta pergResposta = new Ali_PerguntaResposta();
            try
            {
                if (dr.Read())
                {
                    pergResposta.codPerguntaResposta = Convert.ToInt32(dr["A0616_COD_PERG_RESP"]);
                    pergResposta.questionarioPergunta = new Ali_QuestionarioPergunta(Convert.ToInt32(dr["A0613_COD_QUEST_PERG"]));
                    pergResposta.resposta = new Ali_Resposta(Convert.ToInt32(dr["A0615_COD_RESP"]));
                    pergResposta.indDescritivo = Convert.ToInt32(dr["A0616_IND_DESCRITIVO"]);
                    pergResposta.numOrdem = Convert.ToInt32(dr["A0616_NR_ORDEM"]);
                    pergResposta.tipoCampo = dr["A0616_TP_CAMPO"].ToString();
                    pergResposta.vlrResposta = Convert.ToInt32(dr["A0616_VLR_RESP"]);
                    pergResposta.indAtivo = Convert.ToInt32(dr["A0616_IND_ATIVO"]);
                    pergResposta.logCodPessoa = new Pessoa(Convert.ToInt32(dr["A572_CD_PES_ATZ"]));
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                pergResposta = new Ali_PerguntaResposta();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return pergResposta;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>Usuario</returns>
        public static Ali_PerguntaResposta GetDataRow(int codigo, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codigo, trans);
            Ali_PerguntaResposta pergResposta = new Ali_PerguntaResposta();
            try
            {
                if (dr.Read())
                {
                    pergResposta.codPerguntaResposta = Convert.ToInt32(dr["A0616_COD_PERG_RESP"]);
                    pergResposta.questionarioPergunta = new Ali_QuestionarioPergunta(Convert.ToInt32(dr["A0613_COD_QUEST_PERG"]));
                    pergResposta.resposta = new Ali_Resposta(Convert.ToInt32(dr["A0615_COD_RESP"]));
                    pergResposta.indDescritivo = Convert.ToInt32(dr["A0616_IND_DESCRITIVO"]);
                    pergResposta.numOrdem = Convert.ToInt32(dr["A0616_NR_ORDEM"]);
                    pergResposta.tipoCampo = dr["A0616_TP_CAMPO"].ToString();
                    pergResposta.vlrResposta = Convert.ToInt32(dr["A0616_VLR_RESP"]);
                    pergResposta.indAtivo = Convert.ToInt32(dr["A0616_IND_ATIVO"]);
                    pergResposta.logCodPessoa = new Pessoa(Convert.ToInt32(dr["A572_CD_PES_ATZ"]));
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                pergResposta = new Ali_PerguntaResposta();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return pergResposta;
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
