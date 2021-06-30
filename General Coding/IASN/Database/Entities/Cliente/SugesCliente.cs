using System;
using System.Data.OracleClient;
using System.Data;

namespace Database.Entities
{
    /// <summary>
    /// Classe de Mapeamento com a Entidade de Banco de Dados - T255_SUGEST_CLIENTE
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
    public class SugesCliente
    {
        #region Atributos
        private int codSugest;
        private Cliente codCliente;
        private Atendimento codUsuario;
        private Atendimento codEscrSebrae;
        private Atendimento numAtend;
        private int numContato;
        private DateTime dtaSugest;
        private string dscSugest;
        private string tpoSugest;

        private string logUsuario;
        #endregion

        #region Propriedades
        public int CodSugest
        {
            get { return codSugest; }
            set { codSugest = value; }
        }
        public Cliente CodCliente
        {
            get { return codCliente; }
            set { codCliente = value; }
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
        public Atendimento NumAtend
        {
            get { return numAtend; }
            set { numAtend = value; }
        }
        public int NumContato
        {
            get { return numContato; }
            set { numContato = value; }
        }
        public DateTime DtaSugest
        {
            get { return dtaSugest; }
            set { dtaSugest = value; }
        }
        public string DscSugest
        {
            get { return dscSugest; }
            set { dscSugest = value; }
        }
        public string TpoSugest
        {
            get { return tpoSugest; }
            set { tpoSugest = value; }
        }
        public string LogUsuario
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }
        #endregion

        #region Construtores
        public SugesCliente()
            : this(-1)
        { }
        public SugesCliente(int codSugest)
        {
            this.codSugest = codSugest;
            this.codCliente = new Cliente();
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "SugesCliente.SugesClienteInc";
        private const string SPUPDATE = "SugesCliente.SugesClienteAlt";
        private const string SPDELETE = "SugesCliente.SugesClienteDel";
        private const string SPSELECTID = "SugesCliente.SugesClienteSelId";
        private const string SPSELECTPAG = "SugesCliente.SugesClienteSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMcodSugest = "codSugest";
        private const string PARMcodCliente = "codCliente";
        private const string PARMCURSOR = "curSugesCliente";
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
                    /*0*/ new OracleParameter(PARMcodSugest, OracleType.Int32, 4, ParameterDirection.InputOutput.ToString()) ,
                    /*1*/ new OracleParameter(PARMcodCliente, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*2*/ new OracleParameter( "codUsuario", OracleType.Int32),
                    /*3*/ new OracleParameter( "codEscrSebrae", OracleType.Int32),
                    /*4*/ new OracleParameter( "numAtend", OracleType.Int32),
                    /*5*/ new OracleParameter( "numContato", OracleType.Int32),
                    /*6*/ new OracleParameter( "dtaSugest", OracleType.DateTime),
                    /*7*/ new OracleParameter( "dscSugest", OracleType.VarChar),
                    /*8*/ new OracleParameter( "tpoSugest", OracleType.VarChar),
                    /*9*/ new OracleParameter( "logUsuario", OracleType.VarChar)
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
            parms[0].Value = this.codSugest;
            parms[1].Value = this.codCliente.CodCLIENTE;
            parms[2].Value = this.codUsuario.CodUsuario;
            parms[3].Value = this.codEscrSebrae.CodEscrSebrae.CodEscrSebrae;
            parms[4].Value = this.numAtend.NumAtend;
            parms[5].Value = this.numContato;
            parms[6].Value = this.dtaSugest;
            parms[7].Value = this.dscSugest;
            parms[8].Value = this.tpoSugest;
            parms[9].Value = this.logUsuario;
            if (this.codSugest < 0)
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
                        codSugest = Convert.ToInt32(cmd.Parameters[PARMcodSugest].Value);
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
                codSugest = Convert.ToInt32(cmd.Parameters[PARMcodSugest].Value);
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
        /// <param name="codSugest">Código do Registro</param>
        public static void Delete(int codSugest, int codCliente)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMcodSugest, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodCliente, OracleType.Int32, 4)
            };
            parms[0].Value = codSugest;
            parms[1].Value = codCliente;
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
        /// <param name="codSugest">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(int codSugest, int codCliente, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMcodSugest, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodCliente, OracleType.Int32, 4)
            };
                parms[0].Value = codSugest;
                parms[1].Value = codCliente;
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
        /// <param name="codSugest">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codSugest, int codCliente)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodSugest, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodCliente, OracleType.Int32, 4),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codSugest;
            param[1].Value = codCliente;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="codSugest">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codSugest, int codCliente, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodSugest, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodCliente, OracleType.Int32, 4),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codSugest;
            param[1].Value = codCliente;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="codSugest">Código do Registro</param>
        /// <returns>SugesCliente</returns>
        public static SugesCliente GetDataRow(int codSugest, int codCliente)
        {
            OracleDataReader dr = LoadDataDr(codSugest, codCliente);
            SugesCliente SugesCliente = new SugesCliente();
            try
            {
                if (dr.Read())
                {
                    SugesCliente.codSugest = Convert.ToInt32(dr["A255_cd_sugest"]);
                    if (dr["A012_cd_cli"] != DBNull.Value && dr["A012_cd_cli"] != DBNull.Value && dr["A012_cd_cli"].ToString() != "")
                        SugesCliente.codCliente = new Cliente(Convert.ToInt32(dr["A012_cd_cli"]));
                    SugesCliente.codUsuario = new Atendimento(Convert.ToInt32(dr["A052_cd_usuario"]));
                    SugesCliente.codEscrSebrae.CodEscrSebrae.CodEscrSebrae = Convert.ToInt32(dr["A004_cd_escr"]);
                    SugesCliente.numAtend.NumAtend = Convert.ToInt32(dr["A001_num_atend"]);
                    SugesCliente.numContato = Convert.ToInt32(dr["A014_num_cont"]);
                    SugesCliente.dtaSugest = Convert.ToDateTime(dr["A255_dt_sugest"]);
                    SugesCliente.dscSugest = Convert.ToString(dr["A255_sugest"]);
                    SugesCliente.tpoSugest = Convert.ToString(dr["A255_tipo"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        SugesCliente.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                SugesCliente = new SugesCliente();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return SugesCliente;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codSugest">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>SugesCliente</returns>
        public static SugesCliente GetDataRow(int codSugest, int codCliente, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codSugest, codCliente, trans);
            SugesCliente SugesCliente = new SugesCliente();
            try
            {
                if (dr.Read())
                {
                    SugesCliente.codSugest = Convert.ToInt32(dr["A255_cd_sugest"]);
                    if (dr["A012_cd_cli"] != DBNull.Value && dr["A012_cd_cli"] != DBNull.Value && dr["A012_cd_cli"].ToString() != "")
                        SugesCliente.codCliente = new Cliente(Convert.ToInt32(dr["A012_cd_cli"]));
                    SugesCliente.codUsuario = new Atendimento(Convert.ToInt32(dr["A052_cd_usuario"]));
                    SugesCliente.codEscrSebrae.CodEscrSebrae.CodEscrSebrae = Convert.ToInt32(dr["A004_cd_escr"]);
                    SugesCliente.numAtend.NumAtend = Convert.ToInt32(dr["A001_num_atend"]);
                    SugesCliente.numContato = Convert.ToInt32(dr["A014_num_cont"]);
                    SugesCliente.dtaSugest = Convert.ToDateTime(dr["A255_dt_sugest"]);
                    SugesCliente.dscSugest = Convert.ToString(dr["A255_sugest"]);
                    SugesCliente.tpoSugest = Convert.ToString(dr["A255_tipo"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        SugesCliente.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                SugesCliente = new SugesCliente();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return SugesCliente;
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
