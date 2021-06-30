using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
using System.Data;
using System.Net.Mail;
using System.Net;

namespace Classes
{
    public class Ali_Acao // TB0608_ALI_ACAO
    {
        #region Atributos
        private int codAcao;
        private Ali_Questionario questionario;
        private Ali_Avaliacao avaliacao;
        private Ali_Dimensao dimensao;
        private Ali_Dominio dominioArea;
        private DateTime dtaInicio;
        private DateTime dtaTermino;
        private string dscAcao;
        private string dscPlano;
        private string dscRecurso;
        private string observacao;
        private string tipoAcao;
        private int indAtivo;
        private DateTime logDtaInclusao;
        private DateTime logDtaAlteracao;
        private Pessoa logCodPessoa;
        #endregion

        #region Propriedades
        public int CodAcao
        {
            get { return codAcao; }
            set { codAcao = value; }
        }
        public Ali_Questionario Questionario
        {
            get { return questionario; }
            set { questionario = value; }
        }
        public Ali_Avaliacao Avaliacao
        {
            get { return avaliacao; }
            set { avaliacao = value; }
        }
        public Ali_Dimensao Dimensao
        {
            get { return dimensao; }
            set { dimensao = value; }
        }
        public Ali_Dominio DominioArea
        {
            get { return dominioArea; }
            set { dominioArea = value; }
        }
        public DateTime DtaInicio
        {
            get { return dtaInicio; }
            set { dtaInicio = value; }
        }
        public DateTime DtaTermino
        {
            get { return dtaTermino; }
            set { dtaTermino = value; }
        }
        public string DscAcao
        {
            get { return dscAcao; }
            set { dscAcao = value; }
        }
        public string DscPlano
        {
            get { return dscPlano; }
            set { dscPlano = value; }
        }
        public string DscRecurso
        {
            get { return dscRecurso; }
            set { dscRecurso = value; }
        }
        public string Observacao
        {
            get { return observacao; }
            set { observacao = value; }
        }
        public string TipoAcao
        {
            get { return tipoAcao; }
            set { tipoAcao = value; }
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
        public Ali_Acao()
            : this(-1)
        { }
        public Ali_Acao(int codAcao)
        {
            this.codAcao = codAcao;
            this.questionario = new Ali_Questionario();
            this.avaliacao = new Ali_Avaliacao();
            this.dimensao = new Ali_Dimensao();
            this.dominioArea = new Ali_Dominio();
            this.logCodPessoa = new Pessoa();
        }
        #endregion

        #region StoredProcedures
        //private const string SPINSERT = "PC_ALI_PORTAL.AliInc";
        //private const string SPUPDATE = "PC_ALI_PORTAL.AliAlt";
        //private const string SPDELETE = "PC_ALI_PORTAL.AliDel";
        //private const string SPSELECTID = "PC_ALI_PORTAL.AliSelId";
        //private const string SPSELECTPAG = "PC_ALI_PORTAL.AliSelPag";
        //private const string SPSELECTLOGIN = "PC_ALI_PORTAL.AliLogin";
        private const string SPINSERT = "ALI_ACAO.AcaoInc";
        private const string SPUPDATE = "ALI_ACAO.AcaoAlt";
        private const string SPDELETE = "ALI_ACAO.AcaoDel";
        private const string SPSELECTID = "ALI_ACAO.AcaoSelId";
        private const string SPSELECTPAG = "ALI_ACAO.AcaoSelPag";
        private const string SPSELECTPLANO = "ALI_ACAO.AcaoSelPlano";
        private const string SPSELECTACAO = "ALI_ACAO.AnaliseAcaoSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMCODIGO = "codAcao";
        private const string PARMCURSOR = "curAcao";
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
                    /*1*/ new OracleParameter("codQuestionario", OracleType.Int32),
                    /*2*/ new OracleParameter("codAvaliacao", OracleType.Int32),
                    /*3*/ new OracleParameter("codDimensao", OracleType.Int32),
                    /*4*/ new OracleParameter("codDominioArea", OracleType.Int32),
                    /*5*/ new OracleParameter("dtaInicio", OracleType.DateTime),
                    /*6*/ new OracleParameter("dtaTermino", OracleType.DateTime),
                    /*7*/ new OracleParameter("dscAcao", OracleType.VarChar),
                    /*8*/ new OracleParameter("dscPlano", OracleType.VarChar),
                    /*9*/ new OracleParameter("dscRecurso", OracleType.VarChar),
                    /*10*/ new OracleParameter("dscObservacao", OracleType.VarChar),
                    /*11*/ new OracleParameter("tpAcao", OracleType.VarChar),
                    /*12*/ new OracleParameter("indAtivo", OracleType.Int32),
                    /*13*/ new OracleParameter("codPessoa", OracleType.Int32)
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
            parms[0].Value = this.codAcao;
            parms[1].Value = this.questionario.CodQuestionario;
            parms[2].Value = this.avaliacao.CodAvaliacao;
            parms[3].Value = this.dimensao.CodDimensao;
            parms[4].Value = this.dominioArea.CodDominio;
            parms[5].Value = this.dtaInicio;
            parms[6].Value = this.dtaTermino;
            parms[7].Value = this.dscAcao;
            parms[8].Value = this.dscPlano;
            parms[9].Value = this.DscRecurso;
            parms[10].Value = this.observacao;
            parms[11].Value = this.tipoAcao;
            parms[12].Value = this.indAtivo;
            parms[13].Value = this.logCodPessoa.CodPessoa;

            if (this.CodAcao < 0)
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
                        codAcao = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
                codAcao = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
        public static Ali_Acao GetDataRow(int codigo)
        {
            OracleDataReader dr = LoadDataDr(codigo);
            Ali_Acao acao = new Ali_Acao();
            try
            {
                if (dr.Read())
                {
                    acao.codAcao = Convert.ToInt32(dr["A0608_COD_ACAO"]);
                    acao.questionario = new Ali_Questionario(Convert.ToInt32(dr["A0612_COD_QUEST"]));
                    acao.avaliacao = new Ali_Avaliacao(Convert.ToInt32(dr["A0607_COD_AVAL"]));
                    acao.dimensao = new Ali_Dimensao(Convert.ToInt32(dr["A0619_COD_DIMENSAO"]));
                    acao.dominioArea = new Ali_Dominio(Convert.ToInt32(dr["A0200_COD_DOMIN_AREA"]));
                    acao.dtaInicio = Convert.ToDateTime(dr["A0608_DT_INICIO"]);
                    acao.dtaTermino = Convert.ToDateTime(dr["A0608_DT_TERMINO"]);
                    acao.dscAcao = dr["A0608_DSC_ACAO"].ToString();
                    acao.dscPlano = dr["A0608_DSC_PLANO"].ToString();
                    acao.dscRecurso = dr["a0608_DSC_RECURSO"].ToString();
                    acao.observacao = dr["A0608_DSC_OBS"].ToString();
                    acao.tipoAcao = dr["A0608_TP_ACAO"].ToString();
                    acao.indAtivo = Convert.ToInt32(dr["A0608_IND_ATIVO"]);
                    acao.logCodPessoa = new Pessoa(Convert.ToInt32(dr["A572_CD_PES_ATZ"]));
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                acao = new Ali_Acao();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return acao;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>Usuario</returns>
        public static Ali_Acao GetDataRow(int codigo, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codigo, trans);
            Ali_Acao acao = new Ali_Acao();
            try
            {
                if (dr.Read())
                {
                    acao.codAcao = Convert.ToInt32(dr["A0608_COD_ACAO"]);
                    acao.questionario = new Ali_Questionario(Convert.ToInt32(dr["A0612_COD_QUEST"]));
                    acao.avaliacao = new Ali_Avaliacao(Convert.ToInt32(dr["A0607_COD_AVAL"]));
                    acao.dimensao = new Ali_Dimensao(Convert.ToInt32(dr["A0619_COD_DIMENSAO"]));
                    acao.dominioArea = new Ali_Dominio(Convert.ToInt32(dr["A0200_COD_DOMIN_AREA"]));
                    acao.dtaInicio = Convert.ToDateTime(dr["A0608_DT_INICIO"]);
                    acao.dtaTermino = Convert.ToDateTime(dr["A0608_DT_TERMINO"]);
                    acao.dscAcao = dr["A0608_DSC_ACAO"].ToString();
                    acao.dscPlano = dr["A0608_DSC_PLANO"].ToString();
                    acao.dscRecurso = dr["a0608_DSC_RECURSO"].ToString();
                    acao.observacao = dr["A0608_DSC_OBS"].ToString();
                    acao.tipoAcao = dr["A0608_TP_ACAO"].ToString();
                    acao.indAtivo = Convert.ToInt32(dr["A0608_IND_ATIVO"]);
                    acao.logCodPessoa = new Pessoa(Convert.ToInt32(dr["A572_CD_PES_ATZ"]));
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                acao = new Ali_Acao();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return acao;
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

        #region LoadDataDrPlano
        public static OracleDataReader LoadDataDrPlano(int codprojeto, string tpAcao)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter("codprojeto", OracleType.Int32, 4), 
                    new OracleParameter("tpAcao", OracleType.VarChar), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codprojeto;
            param[1].Value = tpAcao;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTPLANO, param);
            return dr;
        }
        #endregion

        #region LoadDataDrPlanoAcao

        public static OracleDataReader LoadDataDrPlanoAcao(int codprojeto, string tpAcao)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter("codprojeto", OracleType.Int32, 4), 
                    new OracleParameter("tpAcao", OracleType.VarChar), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codprojeto;
            param[1].Value = tpAcao;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTPLANO, param);
            return dr;
        }
        #endregion

        #region LoadDataProjAcao

        public static OracleDataReader LoadDataProjAcao(int codprojeto, string tpAcao)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter("codprojeto", OracleType.Int32, 4), 
                    new OracleParameter("tpAcao", OracleType.VarChar), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codprojeto;
            param[1].Value = tpAcao;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTPLANO, param);
            return dr;
        }


        #endregion

        #region LoadDataPaginacaoAcao
        /// <summary>
        /// LoadDataPaginacao
        /// </summary>
        /// <param name="Where">Cláusula where utilizada na consulta</param>
        /// <param name="PaginaCorrente">Número da página que deseja selecionar</param>
        /// <param name="TamanhoPagina">Quantidade de registros em cada página</param>
        /// <param name="ExpressaoOrdenacao">Expressão de ordenação</param>
        /// <returns>Instância do objeto Paginação, contendo um DataReader e o total de registros</returns>
        /// 
        public static Paginacao LoadDataPaginacaoAcao(string Where, int PaginaCorrente, int TamanhoPagina, string ExpressaoOrdenacao)
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

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTACAO, parms);
            paginacao.DataReader = dr;
            //  traz o total de registros de retorno direto dos parametros do ExecuteReader---
            paginacao.TotalRegistros = Convert.ToInt32(parms[5].Value);
            return paginacao;
        }
        #endregion

        #endregion
    }
}
