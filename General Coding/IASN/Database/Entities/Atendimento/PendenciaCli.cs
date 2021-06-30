using System;
using System.Data.OracleClient;
using System.Data;

namespace Database.Entities
{
    /// <summary>
    /// Classe de Mapeamento com a Entidade de Banco de Dados - T015_PENDENCIA
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
    public class PendenciaCli
    {
        #region Atributos
        private ContatoCliente numContato;
        private ContatoCliente codCliente;
        private int numPendencia;
        private Usuario codUsuario;
        private EscritorioSebrae codEscrSebrae;
        private string dscPendencia;
        private string hraPendencia;
        private int stsPendencia;
        private DateTime dtaInclusao;
        private DateTime dtaPrevisao;
        private string logUsuario;
        #endregion

        #region Propriedades
        public ContatoCliente NumContato
        {
            get { return numContato; }
            set { numContato = value; }
        }
        public ContatoCliente CodCliente
        {
            get { return codCliente; }
            set { codCliente = value; }
        }
        public int NumPendencia
        {
            get { return numPendencia; }
            set { numPendencia = value; }
        }
        public Usuario CodUsuario
        {
            get { return codUsuario; }
            set { codUsuario = value; }
        }
        public EscritorioSebrae CodEscrSebrae
        {
            get { return codEscrSebrae; }
            set { codEscrSebrae = value; }
        }
        public string DscPendencia
        {
            get { return dscPendencia; }
            set { dscPendencia = value; }
        }
        public string HraPendencia
        {
            get { return hraPendencia; }
            set { hraPendencia = value; }
        }
        public int StsPendencia
        {
            get { return stsPendencia; }
            set { stsPendencia = value; }
        }
        public DateTime DtaPrevisao
        {
            get { return dtaPrevisao; }
            set { dtaPrevisao = value; }
        }
        public DateTime DtaInclusao
        {
            get { return dtaInclusao; }
            set { dtaInclusao = value; }
        }
        public string LogUsuario
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }
        #endregion

        #region Construtores
        public PendenciaCli()
            : this(-1)
        { }
        public PendenciaCli(int numPendencia)
        {
            this.numPendencia = numPendencia;
            this.numContato = new ContatoCliente();
            this.codCliente = new ContatoCliente();
            this.CodUsuario = new Usuario();
            this.CodEscrSebrae = new EscritorioSebrae();
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "PendenciaCli.PendenciaCliInc";
        private const string SPUPDATE = "PendenciaCli.PendenciaCliAlt";
        private const string SPDELETE = "PendenciaCli.PendenciaCliDel";
        private const string SPSELECTID = "PendenciaCli.PendenciaCliSelId";
        private const string SPSELECTPAG = "PendenciaCli.PendenciaCliSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMnumPendencia = "numPendencia";
        private const string PARMnumContato = "numContato";
        private const string PARMcodCliente = "codCliente";
        private const string PARMCURSOR = "curPendenciaCli";
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
                    /*0*/ new OracleParameter(PARMnumPendencia, OracleType.Int32, 4, ParameterDirection.InputOutput.ToString()) ,
                    /*1*/ new OracleParameter(PARMnumContato, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*2*/ new OracleParameter(PARMcodCliente, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*3*/ new OracleParameter( "codUsuario", OracleType.Int32),
                    /*4*/ new OracleParameter( "codEscrSebrae", OracleType.Int32),
                    /*5*/ new OracleParameter( "dscPendencia", OracleType.LongVarChar),
                    /*6*/ new OracleParameter( "hraPendencia", OracleType.VarChar),
                    /*7*/ new OracleParameter( "stsPendencia", OracleType.Int32),
                    /*8*/ new OracleParameter( "dtaInclusao", OracleType.DateTime),
                    /*9*/ new OracleParameter( "dtaPrevisao", OracleType.DateTime),
                    /*10*/ new OracleParameter( "logUsuario", OracleType.VarChar)
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
            parms[0].Value = this.numPendencia;
            parms[1].Value = this.numContato.NumContato;
            parms[2].Value = this.codCliente.CodCliente.CodCLIENTE;
            parms[3].Value = this.codUsuario.CodUsuario;
            parms[4].Value = this.codEscrSebrae.CodEscrSebrae;
            parms[5].Value = this.dscPendencia.ToUpper() + " ";
            parms[6].Value = this.hraPendencia;
            parms[7].Value = this.stsPendencia;
            parms[8].Value = this.dtaInclusao;
            parms[9].Value = this.dtaPrevisao;
            parms[10].Value = " ";
            if (this.logUsuario!=null) {
                parms[10].Value = this.logUsuario.ToUpper();
            }
            if (this.numPendencia < 0)
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
                        numPendencia = Convert.ToInt32(cmd.Parameters[PARMnumPendencia].Value);
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
                numPendencia = Convert.ToInt32(cmd.Parameters[PARMnumPendencia].Value);
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
        /// <param name="numPendencia">Código do Registro</param>
        public static void Delete(int numPendencia, int numContato, int codCliente)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMnumPendencia, OracleType.Int32, 4)  ,
                new OracleParameter(PARMnumContato, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodCliente, OracleType.Int32, 4)
            };
            parms[0].Value = numPendencia;
            parms[1].Value = numContato;
            parms[2].Value = codCliente;
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
        /// <param name="numPendencia">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(int numPendencia, int numContato, int codCliente, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMnumPendencia, OracleType.Int32, 4)  ,
                new OracleParameter(PARMnumContato, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodCliente, OracleType.Int32, 4) 
            };
                parms[0].Value = numPendencia;
                parms[1].Value = numContato;
                parms[2].Value = codCliente;
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
        /// <param name="numPendencia">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int numPendencia, int numContato, int codCliente)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMnumPendencia, OracleType.Int32, 4), 
                    new OracleParameter(PARMnumContato, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodCliente, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = numPendencia;
            param[1].Value = numContato;
            param[2].Value = codCliente;
            param[3].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="numPendencia">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int numPendencia, int numContato, int codCliente, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMnumPendencia, OracleType.Int32, 4), 
                    new OracleParameter(PARMnumContato, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodCliente, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = numPendencia;
            param[1].Value = numContato;
            param[2].Value = codCliente;
            param[3].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="numPendencia">Código do Registro</param>
        /// <returns>PendenciaCli</returns>
        public static PendenciaCli GetDataRow(int numPendencia, int numContato, int codCliente)
        {
            OracleDataReader dr = LoadDataDr(numPendencia, numContato, codCliente);
            PendenciaCli PendenciaCli = new PendenciaCli();
            try
            {
                if (dr.Read())
                {
                    PendenciaCli.numPendencia = Convert.ToInt32(dr["A015_num_pend"]);
                    if (dr["A014_num_cont"] != DBNull.Value && dr["A014_num_cont"] != DBNull.Value && dr["A014_num_cont"].ToString() != "")
                        PendenciaCli.numContato = new ContatoCliente(Convert.ToInt32(dr["A014_num_cont"]));
                    if (dr["A012_cd_cli"] != DBNull.Value && dr["A012_cd_cli"] != DBNull.Value && dr["A012_cd_cli"].ToString() != "")
                        PendenciaCli.codCliente.CodCliente.CodCLIENTE = Convert.ToInt32(dr["A012_cd_cli"]);

                    PendenciaCli.codUsuario = new Usuario(Convert.ToInt32(dr["A052_cd_usuario"]));
                    PendenciaCli.codEscrSebrae = new EscritorioSebrae(Convert.ToInt32(dr["A004_cd_escr"]));
                    PendenciaCli.dscPendencia = Convert.ToString(dr["A015_dsc_pendencia"]);
                    PendenciaCli.hraPendencia = Convert.ToString(dr["A015_hr_pend"]);
                    PendenciaCli.stsPendencia = Convert.ToInt32(dr["A015_stat_pend"]);
                    PendenciaCli.dtaInclusao = Convert.ToDateTime(dr["A015_dt_incl"]);
                    if (dr["a015_dt_prev"] != null && dr["a015_dt_prev"] + "." != ".")
                       PendenciaCli.dtaPrevisao = Convert.ToDateTime(dr["A015_dt_prev"]);
                    else
                       PendenciaCli.dtaPrevisao = DateTime.Now;
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        PendenciaCli.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                PendenciaCli = new PendenciaCli();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return PendenciaCli;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="numPendencia">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>PendenciaCli</returns>
        public static PendenciaCli GetDataRow(int numPendencia, int numContato, int codCliente, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(numPendencia, numContato, codCliente, trans);
            PendenciaCli PendenciaCli = new PendenciaCli();
            try
            {
                if (dr.Read())
                {
                    PendenciaCli.numPendencia = Convert.ToInt32(dr["A015_num_pend"]);
                    if (dr["A014_num_cont"] != DBNull.Value && dr["A014_num_cont"] != DBNull.Value && dr["A014_num_cont"].ToString() != "")
                        PendenciaCli.numContato = new ContatoCliente(Convert.ToInt32(dr["A014_num_cont"]));
                    if (dr["A012_cd_cli"] != DBNull.Value && dr["A012_cd_cli"] != DBNull.Value && dr["A012_cd_cli"].ToString() != "")
                        PendenciaCli.codCliente.CodCliente.CodCLIENTE = Convert.ToInt32(dr["A012_cd_cli"]);

                    PendenciaCli.codUsuario = new Usuario(Convert.ToInt32(dr["A052_cd_usuario"]));
                    PendenciaCli.codEscrSebrae = new EscritorioSebrae(Convert.ToInt32(dr["A004_cd_escr"]));
                    PendenciaCli.dscPendencia = Convert.ToString(dr["A015_dsc_pendencia"]);
                    PendenciaCli.hraPendencia = Convert.ToString(dr["A015_hr_pend"]);
                    PendenciaCli.stsPendencia = Convert.ToInt32(dr["A015_stat_pend"]);
                    PendenciaCli.dtaInclusao = Convert.ToDateTime(dr["A015_dt_incl"]);
                    PendenciaCli.dtaPrevisao = Convert.ToDateTime(dr["A015_dt_prev"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        PendenciaCli.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                PendenciaCli = new PendenciaCli();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return PendenciaCli;
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