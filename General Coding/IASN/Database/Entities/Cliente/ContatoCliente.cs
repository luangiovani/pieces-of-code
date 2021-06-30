using System;
using System.Text;
using System.Data.OracleClient;
using System.Net;
using System.Data;
using System.IO;

namespace Database.Entities
{
    /// <summary>
    /// Classe de Mapeamento com a Entidade de Banco de Dados - T014_CONTATO_CLIENTE
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
    public class ContatoCliente
    {
        #region Atributos
        private int numContato;
        private Cliente codCliente;
        private string senhaContato;
        private string loginContato;
        private DateTime dtaSebraeNa;
        private string codSebraeNa;        
        private int? indExConselheiro;
        private int? anoSocio;
        private Cargo codCargo;
        private int? indDesativado;
        private string tpoContato;
        private int indSocio;
        private Contato codContato;
        private int? codNegocioFuturo;
        private int? parceiroSiac;
        private string logusuario;
        #endregion

        #region Propriedades
        public Cliente CodCliente
        {
            get { return codCliente; }
            set { codCliente = value; }
        }
        public string SenhaContato
        {
            get { return senhaContato; }
            set { senhaContato = value; }
        }
        public string LoginContato
        {
            get { return loginContato; }
            set { loginContato = value; }
        }
        public DateTime DtaSebraeNa
        {
            get { return dtaSebraeNa; }
            set { dtaSebraeNa = value; }
        }
        public string CodSebraeNa
        {
            get { return codSebraeNa; }
            set { codSebraeNa = value; }
        }
        public int NumContato
        {
            get { return numContato; }
            set { numContato = value; }
        }
        public int? IndExConselheiro
        {
            get { return indExConselheiro; }
            set { indExConselheiro = value; }
        }
        public int? AnoSocio
        {
            get { return anoSocio; }
            set { anoSocio = value; }
        }
        public Cargo CodCargo
        {
            get { return codCargo; }
            set { codCargo = value; }
        }
        public int? IndDesativado
        {
            get { return indDesativado; }
            set { indDesativado = value; }
        }
        public string TpoContato
        {
            get { return tpoContato; }
            set { tpoContato = value; }
        }
        public int IndSocio
        {
            get { return indSocio; }
            set { indSocio = value; }
        }
        public Contato CodContato
        {
            get { return codContato; }
            set { codContato = value; }
        }
        public int ?CodNegocioFuturo
        {
            get { return codNegocioFuturo; }
            set { codNegocioFuturo = value; }
        }
        public int? ParceiroSiac
        {
            get { return parceiroSiac; }
            set { parceiroSiac = value; }
        }
        public string Logusuario
        {
            get { return logusuario; }
            set { logusuario = value; }
        }
        #endregion

        #region Construtores
        public ContatoCliente()
            : this(-1)
        { }
        public ContatoCliente(int numContato)
        {
            this.numContato = numContato;
            this.CodCliente = new Cliente();
            this.codCargo = new Cargo();
            this.codContato = new Contato();
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "ContatoCliente.ContatoClienteInc";
        private const string SPUPDATE = "ContatoCliente.ContatoClienteAlt";
        private const string SPDELETE = "ContatoCliente.ContatoClienteDel";
        private const string SPATIVAR = "ContatoCliente.ContatoClienteAtivar";
        private const string SPSELECTID = "ContatoCliente.ContatoClienteSelId";
        private const string SPSELECTPAG = "ContatoCliente.ContatoClienteSelPag";
        private const string SPENQUADRAMENTOID = "ContatoCliente.EnquadramentoSelId";
        #endregion

        #region Parametros Oracle
        private const string PARMCODIGO = "numContato";
        private const string PARMCODCLI = "codCliente"; 
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
                    /*0*/ new OracleParameter(PARMCODIGO, OracleType.Int32, 8, ParameterDirection.InputOutput.ToString()) ,
                    /*1*/ new OracleParameter("codCliente", OracleType.Int32),
                    /*2*/ new OracleParameter( "codContato", OracleType.Int32),
                    /*3*/ new OracleParameter( "codCargo", OracleType.VarChar),
                    /*4*/ new OracleParameter( "tpoContato", OracleType.VarChar),
                    /*5*/ new OracleParameter( "indDesativado", OracleType.Int32),
                    /*6*/ new OracleParameter( "indSocio", OracleType.Int32),
                    /*7*/ new OracleParameter( "anoSocio", OracleType.Int32),
                    /*8*/ new OracleParameter( "indExConselheiro", OracleType.Int32),
                    /*9*/ new OracleParameter( "loginContato", OracleType.VarChar),
                    /*10*/ new OracleParameter( "senhaContato", OracleType.VarChar),
                    /*11*/ new OracleParameter( "codSebraeNa", OracleType.VarChar),
                    /*12*/ new OracleParameter( "dtaSebraeNa", OracleType.DateTime),
                    /*13*/ new OracleParameter( "codNegocioFuturo", OracleType.Int32),
                    /*14*/ new OracleParameter( "parceiroSiac", OracleType.Int32),
                    /*15*/ new OracleParameter( "logUsuario", OracleType.VarChar)
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
            parms[0].Value = this.numContato;
            parms[1].Value = this.codCliente.CodCLIENTE;

            parms[2].Value = DBNull.Value;
            if (this.codContato != null)
            { parms[2].Value = this.codContato.CodContato; }

            parms[3].Value = "";
            if (this.codCargo != null)
            { parms[3].Value = this.codCargo.CodCargo; }

            parms[4].Value = "";
            if (this.tpoContato != null)
            { parms[4].Value = this.tpoContato; }

            parms[5].Value = DBNull.Value;
            if (this.indDesativado != null)
            { parms[5].Value = this.indDesativado; }

            parms[6].Value = this.IndSocio;

            parms[7].Value = DBNull.Value;
            if (this.anoSocio != null)
            { parms[7].Value = this.anoSocio; }

            parms[8].Value = DBNull.Value;
            if (this.indExConselheiro != null)
            { parms[8].Value = this.indExConselheiro; }

            parms[9].Value = "";
            if (this.loginContato != null)
            { parms[9].Value = this.loginContato; }

            parms[10].Value = "";
            if (this.senhaContato != null)
            { parms[10].Value = this.senhaContato; }

            parms[11].Value = "";
            if (this.codSebraeNa != null)
            { parms[11].Value = this.codSebraeNa; }

            parms[12].Value = DBNull.Value;
            if (this.dtaSebraeNa != null)
            { parms[12].Value = this.dtaSebraeNa; }

            parms[13].Value = DBNull.Value;
            if (this.codNegocioFuturo != null)
            { parms[13].Value = this.codNegocioFuturo; }

            parms[14].Value = DBNull.Value;
            if (this.parceiroSiac != null)
            { parms[14].Value = this.parceiroSiac; } 

            parms[15].Value = "";
            if (this.logusuario != null)
            { parms[15].Value = this.logusuario; }

            if (this.numContato < 0)
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
                        numContato = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
                        cmd.Parameters.Clear();
                        trans.Commit();
                        IntegraRDStation();
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
                numContato = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
                cmd.Parameters.Clear();
                IntegraRDStation();
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
                        IntegraRDStation();
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
                IntegraRDStation();
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
        public static void Delete(int codigoContato, int codigoCliente)
        {
            OracleParameter[] parms = new OracleParameter[] { new OracleParameter(PARMCODIGO, OracleType.Int32, 4),
                                                              new OracleParameter(PARMCODCLI, OracleType.Int32, 4)};
            parms[0].Value = codigoContato;
            parms[1].Value = codigoCliente;
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

        public static void Ativar(int codigoContato, int codigoCliente)
        {
            OracleParameter[] parms = new OracleParameter[] { new OracleParameter(PARMCODIGO, OracleType.Int32, 4),
                                                              new OracleParameter(PARMCODCLI, OracleType.Int32, 4)};
            parms[0].Value = codigoContato;
            parms[1].Value = codigoCliente;
            using (OracleConnection conn = new OracleConnection(Context.DataBase.CONN_STRING))
            {
                conn.Open();
                using (OracleTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        Context.DataBase.ExecuteNonQuery(trans, CommandType.StoredProcedure, SPATIVAR, parms);
                        trans.Commit();
                    }
                    catch (OracleException ex)
                    {
                        trans.Rollback();
                        throw (ex);
                    }
                }
            }
        } // end UPDATE

        /// <summary>
        /// Delete sem tratamento de transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(int codigoContato, int codigoCliente, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { new OracleParameter(PARMCODIGO, OracleType.Int32, 4),
                                                                  new OracleParameter(PARMCODCLI, OracleType.Int32, 4)};
                parms[0].Value = codigoContato;
                parms[1].Value = codigoCliente;
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
        /// <param name="codigo">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int numContato, int codCli)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCODIGO, OracleType.Int32, 4),  
                    new OracleParameter(PARMCODCLI, OracleType.Int32, 4),  
                    new OracleParameter("curcontatoCliente", OracleType.Cursor)
                };

            param[0].Value = numContato;
            param[1].Value = codCli; 
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int numContato, int codCli, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCODIGO, OracleType.Int32, 4),  
                    new OracleParameter(PARMCODCLI, OracleType.Int32, 4),  
                    new OracleParameter("curcontatoCliente", OracleType.Cursor)
                };

            param[0].Value = numContato;
            param[1].Value = codCli;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <returns>contCli</returns>
        public static ContatoCliente GetDataRow(int numContato, int codCli)
        {
            OracleDataReader dr = LoadDataDr(numContato, codCli);
            ContatoCliente contCli = new ContatoCliente();
            try
            {
                if (dr.Read())
                {
                    contCli.numContato = Convert.ToInt32(dr["A014_num_cont"]);
                    contCli.codCliente = new Cliente(Convert.ToInt32(dr["A012_cd_cli"]));
                    contCli.senhaContato = Convert.ToString(dr["A014_senha"]);
                    contCli.loginContato = Convert.ToString(dr["A014_login"]);
                    if (dr["A014_dt_sebraena"] != DBNull.Value && dr["A014_dt_sebraena"] != DBNull.Value && dr["A014_dt_sebraena"].ToString() != "")
                        contCli.dtaSebraeNa = Convert.ToDateTime(dr["A014_dt_sebraena"]);
                    contCli.codSebraeNa = Convert.ToString(dr["A014_cd_sebraena"]);
                    if (dr["A014_ind_ex_consel"] != DBNull.Value && dr["A014_ind_ex_consel"] != DBNull.Value && dr["A014_ind_ex_consel"].ToString() != "")
                        contCli.indExConselheiro = Convert.ToInt32(dr["A014_ind_ex_consel"]);
                    if (dr["A014_ano_socio"] != DBNull.Value && dr["A014_ano_socio"] != DBNull.Value && dr["A014_ano_socio"].ToString() != "")
                        contCli.anoSocio = Convert.ToInt32(dr["A014_ano_socio"]);
                    if (dr["A010_cd_cargo"] != DBNull.Value && dr["A010_cd_cargo"] != DBNull.Value && dr["A010_cd_cargo"].ToString() != "")
                        contCli.codCargo = new Cargo(Convert.ToString(dr["A010_cd_cargo"]));
                    if (dr["A014_ind_desat"] != DBNull.Value && dr["A014_ind_desat"] != DBNull.Value && dr["A014_ind_desat"].ToString() != "")
                        if (dr["A014_ind_desat"] != DBNull.Value && dr["A014_ind_desat"] != DBNull.Value && dr["A014_ind_desat"].ToString() != "")
                            contCli.indDesativado = Convert.ToInt32(dr["A014_ind_desat"]);
                    contCli.tpoContato = Convert.ToString(dr["A014_tp_cont"]);
                    contCli.indSocio = Convert.ToInt32(dr["A014_ind_socio"]);
                   if (dr["A261_cd_cont"] != DBNull.Value && dr["A261_cd_cont"] != DBNull.Value && dr["A261_cd_cont"].ToString() != "")
                            contCli.codContato = new Contato(Convert.ToInt32(dr["A261_cd_cont"]));
                    if(dr["A1045_cd_negocio_futuro"]!=DBNull.Value)
                    contCli.codNegocioFuturo = Convert.ToInt32(dr["A1045_cd_negocio_futuro"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        contCli.logusuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                contCli = new ContatoCliente();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return contCli;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>contCli</returns>
        public static ContatoCliente GetDataRow(int numContato, int codCli, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(numContato, codCli, trans);
            ContatoCliente contCli = new ContatoCliente();
            try
            {
                if (dr.Read())
                {
                    contCli.numContato = Convert.ToInt32(dr["A014_num_cont"]);
                    contCli.codCliente = new Cliente(Convert.ToInt32(dr["A012_cd_cli"]));
                    contCli.senhaContato = Convert.ToString(dr["A014_senha"]);
                    contCli.loginContato = Convert.ToString(dr["A014_login"]);
                    if (dr["A014_dt_sebraena"] != DBNull.Value && dr["A014_dt_sebraena"] != DBNull.Value && dr["A014_dt_sebraena"].ToString() != "")
                        contCli.dtaSebraeNa = Convert.ToDateTime(dr["A014_dt_sebraena"]);
                    contCli.codSebraeNa = Convert.ToString(dr["A014_cd_sebraena"]);
                    if (dr["A014_ind_ex_consel"] != DBNull.Value && dr["A014_ind_ex_consel"] != DBNull.Value && dr["A014_ind_ex_consel"].ToString() != "")
                        contCli.indExConselheiro = Convert.ToInt32(dr["A014_ind_ex_consel"]);
                    if (dr["A014_ano_socio"] != DBNull.Value && dr["A014_ano_socio"] != DBNull.Value && dr["A014_ano_socio"].ToString() != "")
                        contCli.anoSocio = Convert.ToInt32(dr["A014_ano_socio"]);
                    if (dr["A010_cd_cargo"] != DBNull.Value && dr["A010_cd_cargo"] != DBNull.Value && dr["A010_cd_cargo"].ToString() != "")
                        contCli.codCargo = new Cargo(Convert.ToString(dr["A010_cd_cargo"]));
                    if (dr["A014_ind_desat"] != DBNull.Value && dr["A014_ind_desat"] != DBNull.Value && dr["A014_ind_desat"].ToString() != "")
                        if (dr["A014_ind_desat"] != DBNull.Value && dr["A014_ind_desat"] != DBNull.Value && dr["A014_ind_desat"].ToString() != "")
                            contCli.indDesativado = Convert.ToInt32(dr["A014_ind_desat"]);
                    contCli.tpoContato = Convert.ToString(dr["A014_tp_cont"]);
                    contCli.indSocio = Convert.ToInt32(dr["A014_ind_socio"]);
                    if (dr["A261_cd_cont"] != DBNull.Value && dr["A261_cd_cont"] != DBNull.Value && dr["A261_cd_cont"].ToString() != "")
                        if (dr["A261_cd_cont"] != DBNull.Value && dr["A261_cd_cont"] != DBNull.Value && dr["A261_cd_cont"].ToString() != "")
                            contCli.codContato = new Contato(Convert.ToInt32(dr["A261_cd_cont"]));
                    contCli.codNegocioFuturo = Convert.ToInt32(dr["A1045_cd_negocio_futuro"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        contCli.logusuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                contCli = new ContatoCliente();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return contCli;
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
                new OracleParameter("curcontatoCliente", OracleType.Cursor),
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

        #region LoadPagEnquadramento


        /// <summary>
        /// LoadDataPaginacao
        /// </summary>
        /// <param name="codContato">Codigo do Contato para filtro</param>
        /// <returns>Instância do objeto Paginação, contendo um DataReader e o total de registros</returns>
        /// 
        public static Context.Paginacao LoadPagEnquadramento(int codContato)
        {
            Context.Paginacao paginacao = new Context.Paginacao();

            OracleParameter[] parms = new OracleParameter[] {  
			    new OracleParameter("codContato", OracleType.Int32), 
                new OracleParameter("curcontatoCliente", OracleType.Cursor) 
              };

            parms[0].Value = codContato; 
            parms[1].Direction = ParameterDirection.Output;


            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPENQUADRAMENTOID, parms);


            paginacao.DataReader = dr;
            //  traz o total de registros de retorno direto dos parametros do ExecuteReader---
            //paginacao.TotalRegistros = Convert.ToInt32(parms[5].Value);
            return paginacao;
        }
        #endregion

        private void IntegraRDStation()
        {
            try
            {
                string getTpCli = @"SELECT T018.A012_CD_CLI, T014.A261_CD_CONT, T262.A262_DT_ULT_ALT
                                       FROM T018_CLI_CADAST T018
                                       JOIN T014_CONTATO_CLIENTE T014 ON T014.A012_CD_CLI = T018.A012_CD_CLI
                                       JOIN T262_DAD_AD_CONT T262 ON T262.A261_CD_CONT = T014.A261_CD_CONT
                                       JOIN T039_PESSOA_JUR T039 ON T039.A012_CD_CLI = T018.A012_CD_CLI
                                      WHERE T018.A018_TP_CLI <> 1
                                        AND NVL(T018.A018_IND_DES_CLI,0) = 0
                                        AND NVL(T014.A014_IND_DESAT,0) = 0
                                        AND T014.A261_CD_CONT = " + (this.codContato != null && this.codContato.CodContato > 0 ? this.codContato.CodContato : numContato) + @"
                                    GROUP BY T018.A012_CD_CLI, T014.A261_CD_CONT, T262.A262_DT_ULT_ALT    
                                    ORDER BY A262_DT_ULT_ALT DESC";

                using (var rdrCLI = Context.DataBase.ExecuteReader(CommandType.Text, getTpCli))
                {
                    if (rdrCLI.Read())
                    {
                        #region Dados de Contato para Integração RD
                        string getDados = @"SELECT '{""token_rdstation"":""7aea823368c80461ecd7857485c8d802"",""identificador"" : ""SIA"",
""nome"" : ""'||T261.A261_NM_CONT||'"",
""CPF"" : ""'||NVL(T262.A262_NUM_CPF,0)||'"",
""email"" : ""'||T262.A262_EMAIL||'"",
""celular"" : ""('||CASE WHEN TO_NUMBER(REPLACE(regexp_replace(NVL(T261.A261_2DDD_CONT,''), '[^0-9]', ''),' ','')) IS NULL OR TO_NUMBER(REPLACE(regexp_replace(NVL(T261.A261_2DDD_CONT,''), '[^0-9]', ''),' ','')) = 0 
            THEN T018.A018_2DDD_CLI
            ELSE T261.A261_2DDD_CONT
          END||') '||(CASE WHEN TO_NUMBER(REPLACE(regexp_replace(NVL(T261.A261_2DDD_CONT,''), '[^0-9]', ''),' ','')) IS NULL OR TO_NUMBER(REPLACE(regexp_replace(NVL(T261.A261_2DDD_CONT,''), '[^0-9]', ''),' ','')) = 0 
            THEN T018.A018_TEL2_CLI
            ELSE T261.A261_CEL_CONT
          END)||'"",
""empresa"" : ""'||NVL(T039.A039_NM_FANT,T018.A018_NM_CLI)||'"",
""CNPJ"" : ""'||NVL(T039.A039_cgc,0)||'"",
""Código SIA (PJ)"" : ""'||T014.A012_CD_CLI||'"",
""Código Contato (PJ)"" : ""'||T014.A261_CD_CONT||'"",
""Porte da Empresa"" : ""'||T039.A039_PORTE||'"",
""Perfil"": ""'||CASE WHEN T039.A039_PORTE = 'EI' THEN 'Microempreendedor Individual'
               WHEN T039.A039_PORTE = 'ME' THEN 'Micro Empresa'
               WHEN T039.A039_PORTE = 'PQ' THEN 'Pequena Empresa'
               ELSE ''
          END ||'"",
""Ramo de atividade"" : ""'||T048.A048_DSC_SETOR||'"",
""estado"" : ""'||T021C.A021_SGL_EST||'"",
""cidade"" : ""'||T011C.A011_NM_CID||'""}' SJSON,
       T014.A012_CD_CLI,
       T014.A261_CD_CONT,
       T262.A262_EMAIL,
       T261.A261_NM_CONT,
       NVL(T039.A039_NM_FANT,T018.A018_NM_CLI) Empresa,
       NVL(T039.A039_NUM_EMPREG,0) NumFuncionarios,
       T018.A018_END_CLI || ' ' || T018.A018_NMR_END || ', ' || T018.A018_COMP_END || ' - Bairro.: ' || T018.A018_BAIR_CLI || ' - ' || T018.A018_CEP_CLI || ' | ' || T011.A011_NM_CID || '/' || T021.A021_SGL_EST Endereco,
        CASE WHEN TO_NUMBER(REPLACE(regexp_replace(NVL(T261.A261_DDD_CONT,''), '[^0-9]', ''),' ','')) IS NULL OR TO_NUMBER(REPLACE(regexp_replace(NVL(T261.A261_DDD_CONT,''), '[^0-9]', ''),' ','')) = 0 
            THEN T018.A018_DDD_CLI
            ELSE T261.A261_DDD_CONT
          END DDDFIXO,
          CASE WHEN TO_NUMBER(REPLACE(regexp_replace(NVL(T261.A261_DDD_CONT,''), '[^0-9]', ''),' ','')) IS NULL OR TO_NUMBER(REPLACE(regexp_replace(NVL(T261.A261_DDD_CONT,''), '[^0-9]', ''),' ','')) = 0 
            THEN T018.A018_TEL_CLI
            ELSE T261.A261_TEL_CONT
          END FONEFIXO,
          CASE WHEN TO_NUMBER(REPLACE(regexp_replace(NVL(T261.A261_2DDD_CONT,''), '[^0-9]', ''),' ','')) IS NULL OR TO_NUMBER(REPLACE(regexp_replace(NVL(T261.A261_2DDD_CONT,''), '[^0-9]', ''),' ','')) = 0 
            THEN T018.A018_2DDD_CLI
            ELSE T261.A261_2DDD_CONT
          END DDDCEL,
          CASE WHEN TO_NUMBER(REPLACE(regexp_replace(NVL(T261.A261_2DDD_CONT,''), '[^0-9]', ''),' ','')) IS NULL OR TO_NUMBER(REPLACE(regexp_replace(NVL(T261.A261_2DDD_CONT,''), '[^0-9]', ''),' ','')) = 0 
            THEN T018.A018_TEL2_CLI
            ELSE T261.A261_CEL_CONT
          END FONECEL,
          T018.A018_SITE,
          T039.A039_PORTE,
          CASE WHEN T039.A039_PORTE = 'EI' THEN 'Microempreendedor Individual'
               WHEN T039.A039_PORTE = 'ME' THEN 'Micro Empresa'
               WHEN T039.A039_PORTE = 'PQ' THEN 'Pequena Empresa'
               ELSE ''
          END Perfil,
          T048.A048_DSC_SETOR,
          T011C.A011_NM_CID,
          T021C.A021_SGL_EST
  FROM T014_CONTATO_CLIENTE T014
  JOIN T018_CLI_CADAST T018 ON T018.A012_CD_CLI = T014.A012_CD_CLI
  JOIN T011_CIDADE T011 ON T011.A011_CD_CID = T018.A011_CD_CID
   AND T011.A035_CD_PAIS = T018.A035_CD_PAIS
   AND T011.A021_CD_EST = T018.A021_CD_EST
  JOIN T021_ESTADO T021 ON T021.A021_CD_EST = T011.A021_CD_EST
   AND T021.A035_CD_PAIS = T011.A035_CD_PAIS
  JOIN T039_PESSOA_JUR T039 ON T039.A012_CD_CLI = T018.A012_CD_CLI
  JOIN T262_DAD_AD_CONT T262 ON T262.A261_CD_CONT = T014.A261_CD_CONT
  JOIN T261_CONTATO T261 ON T261.A261_CD_CONT =T262.A261_CD_CONT
  JOIN T048_SETOR T048 ON T048.A048_CD_SETOR = T039.A048_CD_SETOR
  JOIN T011_CIDADE T011C ON T011C.A011_CD_CID = T262.A011_CD_CID
   AND T011C.A035_CD_PAIS = T262.A035_CD_PAIS
   AND T011C.A021_CD_EST = T262.A021_CD_EST
  JOIN T021_ESTADO T021C ON T021C.A021_CD_EST = T011C.A021_CD_EST
   AND T021C.A035_CD_PAIS = T011C.A035_CD_PAIS
 WHERE T014.A261_CD_CONT = " + (this.codContato != null && this.codContato.CodContato > 0 ? this.codContato.CodContato : numContato) + @"
   AND T262.A262_EMAIL IS NOT NULL
   AND T018.A018_TP_CLI <> 1
   AND NVL(T014.A014_IND_DESAT,0) = 0
   AND NVL(T018.A018_IND_DES_CLI,0) = 0
ORDER BY T014.A014_DT_INC DESC";
                        #endregion

                        using (var rdContato = Context.DataBase.ExecuteReader(CommandType.Text, getDados))
                        {
                            if (rdContato.Read())
                            {
                                var http = (HttpWebRequest)WebRequest.Create(new Uri("https://www.rdstation.com.br/api/1.3/conversions"));
                                http.Accept = "application/json";
                                http.ContentType = "application/json";
                                http.Method = "POST";
                                http.Timeout = Convert.ToInt32(TimeSpan.FromMinutes(5).TotalMilliseconds);
                                string parsedContent = rdContato["SJSON"].ToString();
                                parsedContent = parsedContent.Replace("Codigo", "Código");
                                parsedContent = parsedContent.Replace("C?digo", "Código");
                                //ASCIIEncoding encoding = new ASCIIEncoding();
                                UTF8Encoding encoding = new UTF8Encoding();
                                Byte[] bytes = encoding.GetBytes(parsedContent);

                                Stream newStream = http.GetRequestStream();
                                newStream.Write(bytes, 0, bytes.Length);
                                newStream.Close();

                                var response = http.GetResponse();

                                var stream = response.GetResponseStream();
                                var sr = new StreamReader(stream);
                                var content = sr.ReadToEnd();

                                http.Abort();
                                response.Close();
                                stream.Close();
                                sr.Close();
                            }
                            rdContato.Close();
                        }
                    }
                    rdrCLI.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion
    }
}
