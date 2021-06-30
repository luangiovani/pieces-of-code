using System;
using System.Data.OracleClient;
using System.Data;

namespace Database.Entities
{
    /// <summary>
    /// Classe de Mapeamento com a Entidade de Banco de Dados - T996_ATEND_HTML
    /// </summary>
    /// <autor>
    /// Luan Giovani Cassini Fernandes
    /// </autor>
    /// <data>
    /// 07/05/2018
    /// </data>
    /// <atividade>
    /// https://esfera.teamworkpm.net/#tasks/17108514
    /// </atividade>
    public class AtendimentoHtml   
    {
        #region Atributos
        private Atendimento numAtend;
        private Atendimento codUsuario;
        private Atendimento codEscrSebrae;
        private GrupoReserva codEvento;
        private GrupoReserva codGrupoReserva;

        private string logUsuario;
        #endregion

        #region Propriedades
        public Atendimento NumAtend
        {
            get { return numAtend; }
            set { numAtend = value; }
        }
        public Atendimento CodUsuario
        {
            get { return codUsuario; }
            set { codUsuario = value; }
        }
        public Atendimento CodEscrSebrae
        {
            get { return codEscrSebrae; }
            set { codEscrSebrae = value; }
        }
        public GrupoReserva CodEvento
        {
            get { return codEvento; }
            set { codEvento = value; }
        }
        public GrupoReserva CodGrupoReserva
        {
            get { return codGrupoReserva; }
            set { codGrupoReserva = value; }
        }
        public string LogUsuario
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }
        #endregion

        #region Construtores
        public AtendimentoHtml()
            : this(-1)
        { }
        public AtendimentoHtml(int numAtend)
        {
            this.numAtend = new Atendimento();
            this.codUsuario = new Atendimento();
            this.codEscrSebrae = new Atendimento();
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "AtendimentoHtml.AtendimentoHtmlInc";
        private const string SPUPDATE = "AtendimentoHtml.AtendimentoHtmlAlt";
        private const string SPDELETE = "AtendimentoHtml.AtendimentoHtmlDel";
        private const string SPSELECTID = "AtendimentoHtml.AtendimentoHtmlSelId";
        private const string SPSELECTPAG = "AtendimentoHtml.AtendimentoHtmlSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMnumAtend = "numAtend";
        private const string PARMcodUsuario = "codUsuario";
        private const string PARMcodEscrSebrae = "codEscrSebrae";
        private const string PARMCURSOR = "curAtendimentoHtml";
        #endregion

        #region Metodos


        #region GetParameters
        public static OracleParameter[] GetParameters()
        {
            OracleParameter[] parms;

            // Tentando buscar os parameters do cache        
            parms = Context.DataBase.GetCachedParameters(SPINSERT);
            //parms = OutputCacheParameters(SPINSERT);
            if (parms == null)
            {
                parms = new OracleParameter[]{ 
                    /*0*/ new OracleParameter(PARMnumAtend, OracleType.Int32, 4, ParameterDirection.InputOutput.ToString()) ,
                    /*1*/ new OracleParameter(PARMcodUsuario, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*2*/ new OracleParameter(PARMcodEscrSebrae, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*3*/ new OracleParameter( "codEvento", OracleType.Int32),
                    /*4*/ new OracleParameter( "CodGrupoReserva", OracleType.Int32),
                    /*5*/ new OracleParameter( "logUsuario", OracleType.VarChar)
                };

                // Criando cache dos parameters 
                Context.DataBase.CacheParameters(SPINSERT, parms);
            }
            return (parms);
        }

        #endregion

        #region SetParameters
        public void SetParameters(OracleParameter[] parms)
        {
            parms[0].Value = this.NumAtend.NumAtend;
            parms[1].Value = this.CodUsuario.CodUsuario.CodUsuario;
            parms[2].Value = this.CodEscrSebrae.CodEscrSebrae.CodEscrSebrae;
            parms[3].Value = this.CodEvento.CodEvento.CodEvento;
            parms[4].Value = this.CodGrupoReserva.CodGrupoReserva;
            parms[5].Value = this.logUsuario;
            if (this.numAtend.NumAtend < 0)
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

            using (OracleConnection conn = new OracleConnection(Context.DataBase.CONN_STRING))
            {
                conn.Open();
                using (OracleTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        OracleCommand cmd = Context.DataBase.ExecuteNonQueryCmd(trans, CommandType.StoredProcedure, SPINSERT, parms);
                        //Obtendo a chave de identificação do registro inserido.
                        //numAtend = Convert.ToInt32(cmd.Parameters[PARMnumAtend].Value);
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
                OracleCommand cmd = Context.DataBase.ExecuteNonQueryCmd(trans, CommandType.StoredProcedure, SPINSERT, parms);
                //Obtendo a chave de identificação do registro inserido.
                //numAtend = Convert.ToInt32(cmd.Parameters[PARMnumAtend].Value);
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
            using (OracleConnection conn = new OracleConnection(Context.DataBase.CONN_STRING))
            {
                conn.Open();
                using (OracleTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        Context.DataBase.ExecuteNonQuery(trans, CommandType.StoredProcedure, SPUPDATE, parms);
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
                Context.DataBase.ExecuteNonQuery(trans, CommandType.StoredProcedure, SPUPDATE, parms);
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
        /// <param name="numAtend">Código do Registro</param>
        public static void Delete(int numAtend, int codUsuario, int codEscrSebrae)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMnumAtend, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodUsuario, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodEscrSebrae, OracleType.Int32, 4)
            };
            parms[0].Value = numAtend;
            parms[1].Value = codUsuario;
            parms[2].Value = codEscrSebrae;
            using (OracleConnection conn = new OracleConnection(Context.DataBase.CONN_STRING))
            {
                conn.Open();
                using (OracleTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        Context.DataBase.ExecuteNonQuery(trans, CommandType.StoredProcedure, SPDELETE, parms);
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
        /// <param name="numAtend">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(int numAtend, int codUsuario, int codEscrSebrae, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMnumAtend, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodUsuario, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodEscrSebrae, OracleType.Int32, 4) 
            };
                parms[0].Value = numAtend;
                parms[1].Value = codUsuario;
                parms[2].Value = codEscrSebrae;
                Context.DataBase.ExecuteNonQuery(trans, CommandType.StoredProcedure, SPDELETE, parms);
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
        /// <param name="numAtend">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int numAtend, int codUsuario, int codEscrSebrae)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMnumAtend, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodUsuario, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodEscrSebrae, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = numAtend;
            param[1].Value = codUsuario;
            param[2].Value = codEscrSebrae;
            param[3].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="numAtend">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int numAtend, int codUsuario, int codEscrSebrae, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMnumAtend, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodUsuario, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodEscrSebrae, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = numAtend;
            param[1].Value = codUsuario;
            param[2].Value = codEscrSebrae;
            param[3].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="numAtend">Código do Registro</param>
        /// <returns>AtendimentoHtml</returns>
        public static AtendimentoHtml GetDataRow(int numAtend, int codUsuario, int codEscrSebrae)
        {
            OracleDataReader dr = LoadDataDr(numAtend, codUsuario, codEscrSebrae);
            AtendimentoHtml AtendimentoHtml = new AtendimentoHtml();
            try
            {
                if (dr.Read())
                {
                    AtendimentoHtml.numAtend = new Atendimento(Convert.ToInt32(dr["A001_num_atend"]));
                    if (dr["A012_cd_cli"] != DBNull.Value && dr["A012_cd_cli"] != DBNull.Value && dr["A012_cd_cli"].ToString() != "")
                        AtendimentoHtml.codUsuario.CodUsuario.CodUsuario = Convert.ToInt32(dr["A012_cd_cli"]);
                    if (dr["A004_cd_escr"] != DBNull.Value && dr["A004_cd_escr"] != DBNull.Value && dr["A004_cd_escr"].ToString() != "")
                        AtendimentoHtml.codEscrSebrae.CodEscrSebrae.CodEscrSebrae = Convert.ToInt32(dr["A004_cd_escr"]);
                    if (dr["A022_cd_ev"] != DBNull.Value && dr["A022_cd_ev"] != DBNull.Value && dr["A022_cd_ev"].ToString() != "")
                        AtendimentoHtml.codEvento = new GrupoReserva(Convert.ToInt32(dr["A022_cd_ev"]));
                    if (dr["A196_cd_grupo_res"] != DBNull.Value && dr["A196_cd_grupo_res"] != DBNull.Value && dr["A196_cd_grupo_res"].ToString() != "")
                        AtendimentoHtml.CodGrupoReserva.CodGrupoReserva = Convert.ToInt32(dr["A196_cd_grupo_res"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        AtendimentoHtml.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                AtendimentoHtml = new AtendimentoHtml();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return AtendimentoHtml;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="numAtend">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>AtendimentoHtml</returns>
        public static AtendimentoHtml GetDataRow(int numAtend, int codUsuario, int codEscrSebrae, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(numAtend, codUsuario, codEscrSebrae, trans);
            AtendimentoHtml AtendimentoHtml = new AtendimentoHtml();
            try
            {
                if (dr.Read())
                {
                    AtendimentoHtml.numAtend = new Atendimento(Convert.ToInt32(dr["A001_num_atend"]));
                    if (dr["A012_cd_cli"] != DBNull.Value && dr["A012_cd_cli"] != DBNull.Value && dr["A012_cd_cli"].ToString() != "")
                        AtendimentoHtml.codUsuario.CodUsuario.CodUsuario = Convert.ToInt32(dr["A012_cd_cli"]);
                    if (dr["A004_cd_escr"] != DBNull.Value && dr["A004_cd_escr"] != DBNull.Value && dr["A004_cd_escr"].ToString() != "")
                        AtendimentoHtml.codEscrSebrae.CodEscrSebrae.CodEscrSebrae = Convert.ToInt32(dr["A004_cd_escr"]);
                    if (dr["A022_cd_ev"] != DBNull.Value && dr["A022_cd_ev"] != DBNull.Value && dr["A022_cd_ev"].ToString() != "")
                        AtendimentoHtml.codEvento = new GrupoReserva(Convert.ToInt32(dr["A022_cd_ev"]));
                    if (dr["A196_cd_grupo_res"] != DBNull.Value && dr["A196_cd_grupo_res"] != DBNull.Value && dr["A196_cd_grupo_res"].ToString() != "")
                        AtendimentoHtml.CodGrupoReserva.CodGrupoReserva = Convert.ToInt32(dr["A196_cd_grupo_res"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        AtendimentoHtml.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                AtendimentoHtml = new AtendimentoHtml();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return AtendimentoHtml;
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
        public static Context.Paginacao LoadDataPaginacao(string Where, int PaginaCorrente, int TamanhoPagina, string ExpressaoOrdenacao)
        {
            Context.Paginacao paginacao = new Context.Paginacao();

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


            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTPAG, parms);


            paginacao.DataReader = dr;
            //  traz o total de registros de retorno direto dos parametros do ExecuteReader---
            paginacao.TotalRegistros = Convert.ToInt32(parms[5].Value);
            return paginacao;
        }
        #endregion


        #endregion

    }
}