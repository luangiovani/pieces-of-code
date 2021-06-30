using System;
using System.Data.OracleClient;
using System.Data;

namespace Database.Entities
{
    /// <summary>
    /// Classe de Mapeamento com a Entidade de Banco de Dados - T125_INSTRUTOR
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
    public class Instrutor
    {
        #region Atributos
        private Pessoa codInstrutor;
        private Usuario codUsuario;
        private Cliente codCliente;
        private string EnderecoCorresponde;
        private EscritorioSebrae codEscritorioReginal;
        private string nomeResumido;
        private int? indAtivo;
        private int? numSocio;
        private decimal? notaMinimaAula;
        private string numContrato;
        private string situacaoCadastro;
        private string logUsuario;
        #endregion

        #region Propriedades
        public int? NumSocio
        {
            get { return numSocio; }
            set { numSocio = value; }
        }
        public int? IndAtivo
        {
            get { return indAtivo; }
            set { indAtivo = value; }
        }
        public Usuario CodUsuario
        {
            get { return codUsuario; }
            set { codUsuario = value; }
        }
        public Pessoa CodInstrutor
        {
            get { return codInstrutor; }
            set { codInstrutor = value; }
        }
        public Cliente CodCliente
        {
            get { return codCliente; }
            set { codCliente = value; }
        }
        public string NumContratoCorresponde
        {
            get { return EnderecoCorresponde; }
            set { EnderecoCorresponde = value; }
        }
        public EscritorioSebrae CodEscritorioReginal
        {
            get { return codEscritorioReginal; }
            set { codEscritorioReginal = value; }
        }
        public string NomeResumido
        {
            get { return nomeResumido; }
            set { nomeResumido = value; }
        }
        public decimal? NotaMinimaAula
        {
            get { return notaMinimaAula; }
            set { notaMinimaAula = value; }
        }
        public string NumContrato
        {
            get { return numContrato; }
            set { numContrato = value; }
        }
        public string SituacaoCadastro
        {
            get { return situacaoCadastro; }
            set { situacaoCadastro = value; }
        }
        //public int? indAtivoNasc
        //{
        //    get { return indAtivoNasc; }
        //    set { indAtivoNasc = value; }
        //}
        public string LogUsuario
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }
        #endregion

        #region Construtores
        public Instrutor()
            : this (null)  //this(null)//this(-1)
        { }
        public Instrutor(Pessoa codInstrutor)
        {
            this.codInstrutor = new Pessoa();
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "Instrutor.InstrutorInc";
        private const string SPUPDATE = "Instrutor.InstrutorAlt";
        private const string SPDELETE = "Instrutor.InstrutorDel";
        private const string SPSELECTID = "Instrutor.InstrutorSelId";
        private const string SPSELECTREGIAOID = "Instrutor.InstrutorRegiaoSelId";
        private const string SPSELECTAREAID = "Instrutor.InstrutorAreaSelId";
        private const string SPSELECTCREDENCIADOID = "Instrutor.InstrutorCredenciadoSelId";
        private const string SPSELECTPAG = "Instrutor.InstrutorSelPag";
        private const string SPSELECTPAGQuestionario = "Instrutor.QuestionarioSelPag";
        private const string SPSELECESTInstrutorHabilitado = "Instrutor.InstrutorHabilitadoSelId";
        private const string SPSELECESTInstrutorPagamento = "Instrutor.InstrutorPagamentoSelId";
        
        #endregion

        #region Parametros Oracle
        private const string PARMCODIGO = "codCliente";
        private const string PARMCURSOR = "curInstrutor";
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
                    /*0*/ new OracleParameter(PARMCODIGO, OracleType.Int32, 4, ParameterDirection.InputOutput.ToString()) ,
                    /*1*/ new OracleParameter( "codUsuario", OracleType.Int32),
                    /*2*/ new OracleParameter( "codCliente", OracleType.Int32),
                    /*3*/ new OracleParameter( "EnderecoCorresponde", OracleType.VarChar),
                    /*4*/ new OracleParameter( "codEscritorioReginal", OracleType.Int32),
                    /*5*/ new OracleParameter( "nomeResumido", OracleType.VarChar),
                    /*6*/ new OracleParameter( "indAtivo", OracleType.Int32),
                    /*7*/ new OracleParameter( "numSocio", OracleType.Int32),
                    /*8*/ new OracleParameter( "notaMinimaAula", OracleType.Float),
                    /*9*/ new OracleParameter( "numContrato", OracleType.VarChar),
                    /*10*/ new OracleParameter( "situacaoCadastro", OracleType.VarChar),
                    /*11*/ new OracleParameter( "logUsuario", OracleType.VarChar)
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
            parms[0].Value = this.codInstrutor.CodPessoa;
            parms[1].Value = this.codUsuario.CodUsuario;
            parms[2].Value = this.codCliente.CodCLIENTE;
            parms[3].Value = this.EnderecoCorresponde;
            parms[4].Value = this.codEscritorioReginal;
            parms[5].Value = this.nomeResumido;
            parms[6].Value = this.indAtivo;
            parms[7].Value = this.numSocio;
            parms[8].Value = this.notaMinimaAula;
            parms[9].Value = this.numContrato;
            parms[10].Value = this.situacaoCadastro;
            parms[11].Value = this.logUsuario;
            
            if (this.codInstrutor.CodPessoa < 0)
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
                        codInstrutor = new Pessoa(Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value));
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
                codInstrutor = new Pessoa(Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value));
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
        /// <param name="codigo">Código do Registro</param>
        public static void Delete(int codigo)
        {
            OracleParameter[] parms = new OracleParameter[] { new OracleParameter(PARMCODIGO, OracleType.Int32, 4) };
            parms[0].Value = codigo;
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
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(int codigo, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { new OracleParameter(PARMCODIGO, OracleType.Int32, 4) };
                parms[0].Value = codigo;
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
        public static OracleDataReader LoadDataDr(int codigo)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter("codInstrutor", OracleType.Int32, 8), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codigo;
            param[1].Direction = ParameterDirection.Output;

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
        public static OracleDataReader LoadDataDr(int codigo, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { new OracleParameter(PARMCODIGO, OracleType.Int32, 4),
                                                                  new OracleParameter(PARMCURSOR, OracleType.Cursor)
};
            param[0].Value = codigo;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        public static OracleDataReader LoadDataRegiaoDr(int codigo)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter("codInstrutor", OracleType.Int32, 8), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codigo;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTREGIAOID, param);
            return dr;
        }

        public static OracleDataReader LoadDataAreaDr(int codigo)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter("codInstrutor", OracleType.Int32, 8), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codigo;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTAREAID, param);
            return dr;
        }

        public static OracleDataReader LoadDataCredenciadoDr(int codigo)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter("codInstrutor", OracleType.Int32, 8), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codigo;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTCREDENCIADOID, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <returns>Instrutor</returns>
        public static Instrutor GetDataRow(int codigo)
        {
            OracleDataReader dr = LoadDataDr(codigo);
            Instrutor ins = new Instrutor();
            try
            {
                if (dr.Read())
                {
                    ins.codInstrutor = new Pessoa(Convert.ToInt32(dr["a125_cd_instr"]));
                    ins.codUsuario = new Usuario(Convert.ToInt32(dr["a052_cd_usuario"]));
                    ins.codCliente = new Cliente(Convert.ToInt32(dr["a012_cd_cli"]));
                    ins.EnderecoCorresponde = Convert.ToString(dr["a125_end_cor"]);
                    ins.codEscritorioReginal = new EscritorioSebrae(Convert.ToInt32(dr["a020_cd_esc_reg"]));
                    ins.nomeResumido = Convert.ToString(dr["a125_nm_resumido"]);
                    if (dr["a125_ind_ativo"]!=null)
                    {ins.indAtivo = Convert.ToInt32(dr["a125_ind_ativo"]);}
                    if (dr["a125_socio"] != null)
                    {ins.numSocio = Convert.ToInt32(dr["a125_socio"]);}
                    if (dr["a125_nota_mini_aula"] != null)
                    { ins.notaMinimaAula = Convert.ToDecimal(dr["a125_nota_mini_aula"]);}
                    ins.numContrato = Convert.ToString(dr["a1160_num_contrato"]);
                    ins.situacaoCadastro = Convert.ToString(dr["a125_sit_cad"]);
                    ins.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                ins = new Instrutor();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return ins;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>ins</returns>
        public static Instrutor GetDataRow(int codigo, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codigo, trans);
            Instrutor ins = new Instrutor();
            try
            {
                if (dr.Read())
                {
                    ins.codInstrutor = new Pessoa(Convert.ToInt32(dr["a125_cd_instr"]));
                    ins.codUsuario = new Usuario(Convert.ToInt32(dr["a052_cd_usuario"]));
                    ins.codCliente = new Cliente(Convert.ToInt32(dr["a012_cd_cli"]));
                    ins.EnderecoCorresponde = Convert.ToString(dr["a125_end_cor"]);
                    ins.codEscritorioReginal = new EscritorioSebrae(Convert.ToInt32(dr["a020_cd_esc_reg"]));
                    ins.nomeResumido = Convert.ToString(dr["a125_nm_resumido"]);
                    if (dr["a125_ind_ativo"] != null)
                    { ins.indAtivo = Convert.ToInt32(dr["a125_ind_ativo"]); }
                    if (dr["a125_socio"] != null)
                    { ins.numSocio = Convert.ToInt32(dr["a125_socio"]); }
                    if (dr["a125_nota_mini_aula"] != null)
                    { ins.notaMinimaAula = Convert.ToDecimal(dr["a125_nota_mini_aula"]); }
                    ins.numContrato = Convert.ToString(dr["a1160_num_contrato"]);
                    ins.situacaoCadastro = Convert.ToString(dr["a125_sit_cad"]);
                    ins.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                ins = new Instrutor();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return ins;
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

        public static Context.Paginacao LoadDataPaginacaoQuestionario(string Where, int PaginaCorrente, int TamanhoPagina, string ExpressaoOrdenacao)
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

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTPAGQuestionario, parms);

            paginacao.DataReader = dr;
            paginacao.TotalRegistros = Convert.ToInt32(parms[5].Value);
            return paginacao;
        }

        #endregion


        #endregion

        #region Metodos Especificos

        #region InstrutorHabilitadoSelId
        /// <summary>
        /// Popula o DropDownList 
        /// </summary>
        /// <returns></returns>
        public static OracleDataReader InstrutorHabilitadoSelId(int codInstrutor, int codEscritorio, int codTituloEv, int codEscritorioReginal)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter("codInstrutor", OracleType.Int32, 8), 
                    new OracleParameter("codEscritorio", OracleType.Int32, 8), 
                    new OracleParameter("codTituloEv", OracleType.Int32, 8), 
                    new OracleParameter("codEscritorioReginal", OracleType.Int32, 8), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codInstrutor;
            param[1].Value = codEscritorio;
            param[2].Value = codTituloEv;
            param[3].Value = codEscritorioReginal;
            param[4].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECESTInstrutorHabilitado, param);
            return dr;
        }

        #endregion

        #region InstrutorPagamentoSelId
        /// <summary>
        /// Popula o DropDownList 
        /// </summary>
        /// <returns></returns>
        public static OracleDataReader InstrutorPagamentoSelId(int codEvento)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter("codEvento", OracleType.Int32, 8), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codEvento;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECESTInstrutorPagamento, param);
            return dr;
        }

        #endregion

        #endregion

    }
}
