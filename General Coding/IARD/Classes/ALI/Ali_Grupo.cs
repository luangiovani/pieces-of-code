using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
using System.Data;
using System.Net.Mail;
using System.Net;

namespace Classes
{
    public class Ali_Grupo // TB0611_ALI_GRP_QUEST
    {
        #region Atributos
        private int codGrupo;
        private string dscGrupo;
        private int numOrdem;
        private int indAtivo;
        private DateTime logDtaInclusao;
        private DateTime logDtaAlteracao;
        private Pessoa logCodPessoa;
        private string pagina;
        #endregion

        #region Propriedades
        public int CodGrupo
        {
            get { return codGrupo; }
            set { codGrupo = value; }
        }
        public string DscGrupo
        {
            get { return dscGrupo; }
            set { dscGrupo = value; }
        }
        public int NumOrdem
        {
            get { return numOrdem; }
            set { numOrdem = value; }
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
        public Ali_Grupo()
            : this(-1)
        { }
        public Ali_Grupo(int codGrupo)
        {
            this.codGrupo = codGrupo;
            this.logCodPessoa = new Pessoa();
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "ALI_GRUPOQUEST.GrupoQuestInc";
        private const string SPUPDATE = "ALI_GRUPOQUEST.GrupoQuestAlt";
        private const string SPDELETE = "ALI_GRUPOQUEST.GrupoQuestDel";
        private const string SPSELECTID = "ALI_GRUPOQUEST.GrupoQuestSelId";
        private const string SPSELECTPAG = "ALI_GRUPOQUEST.GrupoQuestSelPag";
        private const string SPSELECTMENU = "ALI_GRUPOQUEST.GrupoQuestSelMenu";
        private const string SPSELECTORDEM = "ALI_GRUPOQUEST.GrupoQuestSelNumOrdem";
        #endregion

        #region Parametros Oracle
        private const string PARMCODIGO = "codGrupo";
        private const string PARMCURSOR = "curGrupo";
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
                    /*1*/ new OracleParameter("dscGrupo", OracleType.VarChar),
                    /*2*/ new OracleParameter("numOrdem", OracleType.Int32),
                    /*3*/ new OracleParameter("indAtivo", OracleType.Int32),
                    /*4*/ new OracleParameter("codPessoa", OracleType.Int32),
                    /*5*/ new OracleParameter("pagina", OracleType.VarChar)
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
            parms[0].Value = this.codGrupo;
            parms[1].Value = this.dscGrupo;
            parms[2].Value = this.numOrdem;
            parms[3].Value = this.indAtivo;
            parms[4].Value = this.logCodPessoa.CodPessoa;
            parms[5].Value = this.pagina;
            if (this.codGrupo < 0)
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
                        codGrupo = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
                codGrupo = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
        public static Ali_Grupo GetDataRow(int codigo)
        {
            OracleDataReader dr = LoadDataDr(codigo);
            Ali_Grupo grupo = new Ali_Grupo();
            try
            {
                if (dr.Read())
                {
                    grupo.codGrupo = Convert.ToInt32(dr["A0611_COD_GRP"]);
                    grupo.dscGrupo = dr["A0611_DSC_GRP"].ToString();
                    grupo.numOrdem = Convert.ToInt32(dr["A0611_NR_ORDEM"]);
                    grupo.indAtivo = Convert.ToInt32(dr["A0611_IND_ATIVO"]);
                    if (dr["A572_CD_PES_ATZ"] != DBNull.Value)
                        grupo.logCodPessoa = new Pessoa(Convert.ToInt32(dr["A572_CD_PES_ATZ"]));
                    grupo.pagina = dr["A0611_PAGINA"].ToString();
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                grupo = new Ali_Grupo();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return grupo;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>Usuario</returns>
        public static Ali_Grupo GetDataRow(int codigo, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codigo, trans);
            Ali_Grupo grupo = new Ali_Grupo();
            try
            {
                if (dr.Read())
                {
                    grupo.codGrupo = Convert.ToInt32(dr["A0611_COD_GRP"]);
                    grupo.dscGrupo = dr["A0611_DSC_GRP"].ToString();
                    grupo.numOrdem = Convert.ToInt32(dr["A0611_NR_ORDEM"]);
                    grupo.indAtivo = Convert.ToInt32(dr["A0611_IND_ATIVO"]);
                    if (dr["A572_CD_PES_ATZ"] != DBNull.Value)
                        grupo.logCodPessoa = new Pessoa(Convert.ToInt32(dr["A572_CD_PES_ATZ"]));
                    grupo.pagina = dr["A0611_PAGINA"].ToString();
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                grupo = new Ali_Grupo();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return grupo;
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

        #region LoadDataDr

        /// <summary>
        /// LoadDataDr
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr()
        {
            OracleParameter[] param = new OracleParameter[] {
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTMENU, param);
            return dr;
        }
        #endregion

        #region LoadDataDrOrdem

        /// <summary>
        /// LoadDataDr
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static Ali_Grupo GetDataRowOrdem(int numOrdem)
        {
            OracleParameter[] param = new OracleParameter[] {
                    new OracleParameter("numOrdem", OracleType.Int32),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = numOrdem;
            param[1].Direction = ParameterDirection.Output;

            Ali_Grupo grupo = new Ali_Grupo();
            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTORDEM, param);
            if (dr.Read())
            {
                grupo.codGrupo = Convert.ToInt32(dr["A0611_COD_GRP"]);
                grupo.dscGrupo = dr["A0611_DSC_GRP"].ToString();
                grupo.numOrdem = Convert.ToInt32(dr["A0611_NR_ORDEM"]);
                grupo.indAtivo = Convert.ToInt32(dr["A0611_IND_ATIVO"]);
                if (dr["A572_CD_PES_ATZ"] != DBNull.Value)
                    grupo.logCodPessoa = new Pessoa(Convert.ToInt32(dr["A572_CD_PES_ATZ"]));
                grupo.pagina = dr["A0611_PAGINA"].ToString();
            }
            dr.Close();

            return grupo;
        }
        #endregion

        #endregion
    }
}
