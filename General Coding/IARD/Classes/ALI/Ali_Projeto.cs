using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
using System.Data;
using System.Net.Mail;
using System.Net;

namespace Classes
{
    public class Ali_Projeto // TB0606_ALI_PROJETO
    {
        #region Atributos
        private int codProjeto;
        private Ali_Grupo grupo;
        private Ali_Questionario questionario;
        private Pessoa pessoaAli;
        private ContatoCliente contato;
        private ContatoCliente contatoRepresentante;
        private Ali_Dominio dominio;
        private Ali_Dominio dominioCadeiaProdutiva;
        private string dscCadeiaProdutiva;
        private string dscCancelamento;
        private string dscFinalizado;
        private int numRepeticao;
        private DateTime? dtaSenior;
        private DateTime? dtaGestor;
        private DateTime? dtaCancelamento;
        private string indStatus; //A - Ativo / C - Cancelado / F - Finalizado
        private string indAnalise;
        private DateTime logDtaCriacao;
        private DateTime logDtaEdicao;
        #endregion

        #region Propriedades
        public int CodProjeto
        {
            get { return codProjeto; }
            set { codProjeto = value; }
        }
        public Ali_Grupo Grupo
        {
            get { return grupo; }
            set { grupo = value; }
        }
        public Ali_Questionario Questionario
        {
            get { return questionario; }
            set { questionario = value; }
        }
        public Pessoa PessoaAli
        {
            get { return pessoaAli; }
            set { pessoaAli = value; }
        }
        public ContatoCliente Contato
        {
            get { return contato; }
            set { contato = value; }
        }
        public ContatoCliente ContatoRepresentante
        {
            get { return contatoRepresentante; }
            set { contatoRepresentante = value; }
        }
        public Ali_Dominio Dominio
        {
            get { return dominio; }
            set { dominio = value; }
        }
        public Ali_Dominio DominioCadeiaProdutiva
        {
            get { return dominioCadeiaProdutiva; }
            set { dominioCadeiaProdutiva = value; }
        }
        public string DscCadeiaProdutiva
        {
            get { return dscCadeiaProdutiva; }
            set { dscCadeiaProdutiva = value; }
        }
        public string DscCancelamento
        {
            get { return dscCancelamento; }
            set { dscCancelamento = value; }
        }
        public string DscFinalizado
        {
            get { return dscFinalizado; }
            set { dscFinalizado = value; }
        }
        public int NumRepeticao
        {
            get { return numRepeticao; }
            set { numRepeticao = value; }
        }
        public DateTime? DtaSenior
        {
            get { return dtaSenior; }
            set { dtaSenior = value; }
        }
        public DateTime? DtaGestor
        {
            get { return dtaGestor; }
            set { dtaGestor = value; }
        }
        public DateTime? DtaCancelamento
        {
            get { return dtaCancelamento; }
            set { dtaCancelamento = value; }
        }
        public string IndStatus
        {
            get { return indStatus; }
            set { indStatus = value; }
        }
        public string IndAnalise
        {
            get { return indAnalise; }
            set { indAnalise = value; }
        }
        public DateTime LogDtaCriacao
        {
            get { return logDtaCriacao; }
            set { logDtaCriacao = value; }
        }
        public DateTime LogDtaEdicao
        {
            get { return logDtaEdicao; }
            set { logDtaEdicao = value; }
        }
        #endregion

        #region Construtores
        public Ali_Projeto()
            : this(-1)
        { }
        public Ali_Projeto(int codProjeto)
        {
            this.codProjeto = codProjeto;
            this.grupo = new Ali_Grupo();
            this.questionario = new Ali_Questionario();
            this.pessoaAli = new Pessoa();
            this.contato = new ContatoCliente();
            this.contatoRepresentante = new ContatoCliente();
            this.dominio = new Ali_Dominio();
            this.dominioCadeiaProdutiva = new Ali_Dominio();
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "ALI_PROJETO.ProjetoInc";
        private const string SPUPDATE = "ALI_PROJETO.ProjetoAlt";
        private const string SPDELETE = "ALI_PROJETO.ProjetoDel";
        private const string SPSELECTID = "ALI_PROJETO.ProjetoSelId";
        private const string SPSELECTPAG = "ALI_PROJETO.ProjetoSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMCODIGO = "codProjeto";
        private const string PARMCURSOR = "curProjeto";
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
                    /*2*/ new OracleParameter("codQuestionario", OracleType.Int32),
                    /*3*/ new OracleParameter("codPessoaAli", OracleType.Int32),
                    /*4*/ new OracleParameter("numContato", OracleType.Int32),
                    /*5*/ new OracleParameter("codClienteContato", OracleType.Int32),
                    /*6*/ new OracleParameter("numContatoRepresentante", OracleType.Int32),
                    /*7*/ new OracleParameter("codClienteRepresentante", OracleType.Int32),
                    /*8*/ new OracleParameter("codDominio", OracleType.Int32),
                    /*9*/ new OracleParameter("codDominioCadeiaProdutiva", OracleType.Int32),
                    /*10*/ new OracleParameter("dscCadeiaProdutiva", OracleType.VarChar),
                    /*11*/ new OracleParameter("dscCancelamento", OracleType.VarChar),
                    /*12*/ new OracleParameter("dscFinalizado", OracleType.VarChar),
                    /*13*/ new OracleParameter("numRepeticao", OracleType.Int32),
                    /*14*/ new OracleParameter("dtaSenior", OracleType.DateTime),
                    /*15*/ new OracleParameter("dtaGestor", OracleType.DateTime),
                    /*16*/ new OracleParameter("dtaCancelamento", OracleType.DateTime),
                    /*17*/ new OracleParameter("indStatus", OracleType.VarChar),
                    /*18*/ new OracleParameter("indAnalise", OracleType.VarChar)
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
            parms[0].Value = this.codProjeto;
            parms[1].Value = this.grupo.CodGrupo;
            parms[2].Value = this.questionario.CodQuestionario;
            parms[3].Value = this.pessoaAli.CodPessoa;
            parms[4].Value = this.contato.NumContato;
            parms[5].Value = this.contato.CodCliente.CodCLIENTE;
            parms[6].Value = this.contatoRepresentante.NumContato;
            parms[7].Value = this.contatoRepresentante.CodCliente.CodCLIENTE;
            parms[8].Value = this.dominio.CodDominio;
            parms[9].Value = this.dominioCadeiaProdutiva.CodDominio;

            if (this.dscCadeiaProdutiva != null)
                parms[10].Value = this.dscCadeiaProdutiva;
            else
                parms[10].Value = "";

            if (this.dscCancelamento != null)
                parms[11].Value = this.dscCancelamento;
            else
                parms[11].Value = "";

            if (this.dscFinalizado != null)
                parms[12].Value = this.dscFinalizado;
            else
                parms[12].Value = "";

            parms[13].Value = this.numRepeticao;

            if (this.DtaSenior != null)
                parms[14].Value = this.dtaSenior;
            else
                parms[14].Value = DBNull.Value;

            if (this.DtaGestor != null)
                parms[15].Value = this.dtaGestor;
            else
                parms[15].Value = DBNull.Value;

            if (this.DtaCancelamento != null)
                parms[16].Value = this.dtaCancelamento;
            else
                parms[16].Value = DBNull.Value;

            parms[17].Value = this.indStatus.Substring(0,1);
            parms[18].Value = this.IndAnalise;
            if (this.CodProjeto < 0)
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
                        codProjeto = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
                codProjeto = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
        public static Ali_Projeto GetDataRow(int codigo)
        {
            OracleDataReader dr = LoadDataDr(codigo);
            Ali_Projeto projeto = new Ali_Projeto();
            try
            {
                if (dr.Read())
                {
                    projeto.codProjeto = Convert.ToInt32(dr["A0606_COD_PROJETO"]);
                    projeto.grupo = new Ali_Grupo(Convert.ToInt32(dr["A0611_COD_GRP"]));
                    projeto.questionario = new Ali_Questionario(Convert.ToInt32(dr["A0612_COD_QUEST"]));
                    projeto.pessoaAli = new Pessoa(Convert.ToInt32(dr["A572_CD_PES_ALI"]));

                    projeto.Contato = new ContatoCliente(Convert.ToInt32(dr["A014_NUM_CONT_CON"]));
                    if (projeto.contato.NumContato > 0)
                        projeto.contato.CodCliente = new Cliente(Convert.ToInt32(dr["A012_CD_CLI_CON"]));

                    projeto.contatoRepresentante = new ContatoCliente(Convert.ToInt32(dr["A014_NUM_CONT_REP"]));
                    if (projeto.contatoRepresentante.NumContato > 0)
                        projeto.contatoRepresentante.CodCliente = new Cliente(Convert.ToInt32(dr["A012_CD_CLI_REP"]));

                    if (dr["A0200_COD_DOMIN"] != DBNull.Value)
                        projeto.dominio = new Ali_Dominio(Convert.ToInt32(dr["A0200_COD_DOMIN"]));

                    projeto.dominioCadeiaProdutiva = new Ali_Dominio(Convert.ToInt32(dr["A0200_COD_DOMIN_CADPROD"]));
                    projeto.dscCadeiaProdutiva = dr["A0606_DSC_CAD_PROD"].ToString();
                    projeto.dscCancelamento = dr["A0606_DSC_JUS_CANC"].ToString();
                    projeto.dscFinalizado = dr["A0606_DSC_JUS_FINALIZ"].ToString();
                    projeto.numRepeticao = Convert.ToInt32(dr["A0606_NR_REPETICAO"]);

                    if (dr["A0606_DT_SENIOR"] != DBNull.Value)
                        projeto.dtaSenior = Convert.ToDateTime(dr["A0606_DT_SENIOR"]);

                    if (dr["A0606_DT_GESTOR"] != DBNull.Value)
                        projeto.dtaGestor = Convert.ToDateTime(dr["A0606_DT_GESTOR"]);

                    if (dr["A0606_DT_CANC"] != DBNull.Value)
                        projeto.dtaCancelamento = Convert.ToDateTime(dr["A0606_DT_CANC"]);

                    projeto.indStatus = dr["A0606_IND_STATUS"].ToString();
                    projeto.indAnalise = dr["A0606_IND_ANALISE"].ToString();
                    projeto.logDtaCriacao = Convert.ToDateTime(dr["A0606_DT_INC"]);
                    projeto.logDtaEdicao = Convert.ToDateTime(dr["A0606_DT_ULT_ATZ"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                projeto = new Ali_Projeto();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return projeto;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>Usuario</returns>
        public static Ali_Projeto GetDataRow(int codigo, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codigo, trans);
            Ali_Projeto projeto = new Ali_Projeto();
            try
            {
                if (dr.Read())
                {
                    projeto.codProjeto = Convert.ToInt32(dr["A0606_COD_PROJETO"]);
                    projeto.grupo = new Ali_Grupo(Convert.ToInt32(dr["A0611_COD_GRP"]));
                    projeto.questionario = new Ali_Questionario(Convert.ToInt32(dr["A0612_COD_QUEST"]));
                    projeto.pessoaAli = new Pessoa(Convert.ToInt32(dr["A572_CD_PES_ALI"]));

                    projeto.Contato = new ContatoCliente(Convert.ToInt32(dr["A014_NUM_CONT_CON"]));
                    if (projeto.contato.NumContato > 0)
                        projeto.contato.CodCliente = new Cliente(Convert.ToInt32(dr["A012_CD_CLI_CON"]));

                    projeto.contatoRepresentante = new ContatoCliente(Convert.ToInt32(dr["A014_NUM_CONT_REP"]));
                    if (projeto.contatoRepresentante.NumContato > 0)
                        projeto.contatoRepresentante.CodCliente = new Cliente(Convert.ToInt32(dr["A012_CD_CLI_REP"]));

                    if (dr["A0200_COD_DOMIN"] != DBNull.Value)
                        projeto.dominio = new Ali_Dominio(Convert.ToInt32(dr["A0200_COD_DOMIN"]));

                    projeto.dominioCadeiaProdutiva = new Ali_Dominio(Convert.ToInt32(dr["A0200_COD_DOMIN_CADPROD"]));
                    projeto.dscCadeiaProdutiva = dr["A0606_DSC_CAD_PROD"].ToString();
                    projeto.dscCancelamento = dr["A0606_DSC_JUS_CANC"].ToString();
                    projeto.dscFinalizado = dr["A0606_DSC_JUS_FINALIZ"].ToString();
                    projeto.numRepeticao = Convert.ToInt32(dr["A0606_NR_REPETICAO"]);

                    if (dr["A0606_DT_SENIOR"] != DBNull.Value)
                        projeto.dtaSenior = Convert.ToDateTime(dr["A0606_DT_SENIOR"]);

                    if (dr["A0606_DT_GESTOR"] != DBNull.Value)
                        projeto.dtaGestor = Convert.ToDateTime(dr["A0606_DT_GESTOR"]);

                    if (dr["A0606_DT_CANC"] != DBNull.Value)
                        projeto.dtaCancelamento = Convert.ToDateTime(dr["A0606_DT_CANC"]);

                    projeto.indStatus = dr["A0606_IND_STATUS"].ToString();
                    projeto.indAnalise = dr["A0606_IND_ANALISE"].ToString();
                    projeto.logDtaCriacao = Convert.ToDateTime(dr["A0606_DT_INC"]);
                    projeto.logDtaEdicao = Convert.ToDateTime(dr["A0606_DT_ULT_ATZ"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                projeto = new Ali_Projeto();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return projeto;
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
