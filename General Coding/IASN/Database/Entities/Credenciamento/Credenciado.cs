using System;
using System.Data.OracleClient;
using System.Data;

namespace Database.Entities
{
    /// <summary>
    /// Classe de Mapeamento com a Entidade de Banco de Dados - T910_CREDENCIADO
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
    public class Credenciado
    {
        #region Atributos
        private int codCredenciado;
        private EventoCredenciamento codEvento;
        private Instrutor codInstrutor;
        private Cliente codCliente;
        private ContatoCliente numContato;
        private CategoriaEvento codCategoria;
        private GrupoCredenciamentoEv codGrpCredenc;
        private string logUsuario;

        #endregion

        #region Propriedades
        public Instrutor CodInstrutor
        {
            get { return codInstrutor; }
            set { codInstrutor = value; }
        }
        public Cliente CodCliente
        {
            get { return codCliente; }
            set { codCliente = value; }
        }
        public ContatoCliente NumContato
        {
            get { return numContato; }
            set { numContato = value; }
        }
        public CategoriaEvento CodCategoria
        {
            get { return codCategoria; }
            set { codCategoria = value; }
        }
        public GrupoCredenciamentoEv CodGrpCredenc
        {
            get { return codGrpCredenc; }
            set { codGrpCredenc = value; }
        }
        public EventoCredenciamento CodEvento
        {
            get { return codEvento; }
            set { codEvento = value; }
        }
        public int CodCredenciado
        {
            get { return codCredenciado; }
            set { codCredenciado = value; }
        }
        public string LogUsuario
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }

        #endregion

        #region Construtores
        public Credenciado()
            : this(-1)
        { }
        public Credenciado(int codCredenciado)
        {
            this.codCredenciado = codCredenciado;
            this.codInstrutor = new Instrutor();
            this.codEvento = new EventoCredenciamento();
            this.CodCliente = new Cliente();
            this.numContato = new ContatoCliente();
            this.codCategoria = new CategoriaEvento();
            this.codGrpCredenc = new GrupoCredenciamentoEv();
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "Credenciado.CredenciadoInc";
        private const string SPUPDATE = "Credenciado.CredenciadoAlt";
        private const string SPDELETE = "Credenciado.CredenciadoDel";
        private const string SPSELECTID = "Credenciado.CredenciadoSelId";
        private const string SPSELECTPAG = "Credenciado.CredenciadoSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMCODIGO = "codCredenciado";
        private const string PARMCURSOR = "curCredenciado";
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
                /*0*/ new OracleParameter(PARMCODIGO, OracleType.Int32, 2, ParameterDirection.InputOutput.ToString()) ,
                /*1*/ new OracleParameter( "codEvento", OracleType.Int32),
                      new OracleParameter( "codInstrutor", OracleType.Int32),
                      new OracleParameter( "codCliente", OracleType.Int32),
                      new OracleParameter( "numContato", OracleType.Int32),
                      new OracleParameter( "codCategoria", OracleType.Int32),
                      new OracleParameter( "codGrpCredenc", OracleType.Int32),
                /*7*/ new OracleParameter( "logUsuario", OracleType.VarChar)
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
            parms[0].Value = this.codCredenciado;
            parms[1].Value = DBNull.Value;
            if (this.CodEvento != null)
            { parms[1].Value = this.CodEvento.CodEvento; }
            parms[2].Value = DBNull.Value;
            if (this.CodInstrutor != null)
            {parms[2].Value = this.CodInstrutor.CodInstrutor.CodPessoa;}
            parms[3].Value =DBNull.Value;
            if (this.CodCliente != null)
            {parms[3].Value = this.CodCliente.CodCLIENTE;}
            parms[4].Value =DBNull.Value;
            if (this.NumContato != null)
            {parms[4].Value = this.NumContato.NumContato;}
            parms[5].Value =DBNull.Value;
            if (this.CodCategoria != null)
            {parms[5].Value = this.CodCategoria.CodCategoria;}
            parms[6].Value =DBNull.Value;
            if (this.CodGrpCredenc != null)
            { parms[6].Value = this.CodGrpCredenc.CodGrpCredenc; }
            parms[7].Value = this.logUsuario.ToUpper();
            if (this.codCredenciado < 0)
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
                        codCredenciado = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
                codCredenciado = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
            OracleParameter[] parms = new OracleParameter[] { new OracleParameter(PARMCODIGO, OracleType.Int32, 2) };
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
                OracleParameter[] parms = new OracleParameter[] { new OracleParameter(PARMCODIGO, OracleType.Int32, 2) };
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

                new OracleParameter(PARMCODIGO, OracleType.Int32, 2), 
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
            OracleParameter[] param = new OracleParameter[] { new OracleParameter(PARMCODIGO, OracleType.Int32, 2),
                                                              new OracleParameter(PARMCURSOR, OracleType.Cursor)
};
            param[0].Value = codigo;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <returns>Credenciado</returns>
        public static Credenciado GetDataRow(int codigo)
        {
            OracleDataReader dr = LoadDataDr(codigo);
            Credenciado Credenciado = new Credenciado();
            try
            {
                if (dr.Read())
                {
                    Credenciado.codCredenciado = Convert.ToInt32(dr["A910_cod_credenciado"]);
                    Credenciado.codEvento = new EventoCredenciamento(Convert.ToInt32(dr["A022_cd_ev"]));
                    if (dr["A125_cd_instr"] != DBNull.Value)
                    { Credenciado.CodInstrutor.CodInstrutor =new Pessoa(Convert.ToInt32(dr["A125_cd_instr"])); }
                    if (dr["A012_cd_cli"] != DBNull.Value)
                    { Credenciado.codCliente = new Cliente(Convert.ToInt32(dr["A012_cd_cli"]));}
                    if (dr["A014_num_cont"] != DBNull.Value)
                    { Credenciado.numContato =new ContatoCliente(Convert.ToInt32(dr["A014_num_cont"]));}
                    Credenciado.codCategoria = new CategoriaEvento(Convert.ToInt32(dr["A911_cod_categoria"]));
                    if (dr["A914_cod_grp_credenc"] != DBNull.Value)
                    { Credenciado.codGrpCredenc = new GrupoCredenciamentoEv(Convert.ToInt32(dr["A914_cod_grp_credenc"]));}
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        Credenciado.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                Credenciado = new Credenciado();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return Credenciado;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>Credenciado</returns>
        public static Credenciado GetDataRow(int codigo, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codigo, trans);
            Credenciado Credenciado = new Credenciado();
            try
            {
                if (dr.Read())
                {
                    Credenciado.codCredenciado = Convert.ToInt32(dr["A910_cod_credenciado"]);
                    Credenciado.codEvento = new EventoCredenciamento(Convert.ToInt32(dr["A022_cd_ev"]));
                    if (dr["A125_cd_instr"] != DBNull.Value)
                    { Credenciado.CodInstrutor.CodInstrutor = new Pessoa(Convert.ToInt32(dr["A125_cd_instr"])); }
                    if (dr["A012_cd_cli"] != DBNull.Value)
                    { Credenciado.codCliente = new Cliente(Convert.ToInt32(dr["A012_cd_cli"])); }
                    if (dr["A014_num_cont"] != DBNull.Value)
                    { Credenciado.numContato.CodContato.CodContato = Convert.ToInt32(dr["A014_num_cont"]); }
                    Credenciado.codCategoria = new CategoriaEvento(Convert.ToInt32(dr["A911_cod_categoria"]));
                    if (dr["A914_cod_grp_credenc"] != DBNull.Value)
                    { Credenciado.codGrpCredenc = new GrupoCredenciamentoEv(Convert.ToInt32(dr["A914_cod_grp_credenc"])); }
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        Credenciado.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                Credenciado = new Credenciado();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return Credenciado;
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
