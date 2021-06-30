using System;
using System.Data.OracleClient;
using System.Data;

namespace Database.Entities
{
    /// <summary>
    /// Classe de Mapeamento com a Entidade de Banco de Dados - T286_ATEND_PROGRAMADO
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
    public class AtendimentoProgramado
    {
        #region Atributos
        private int codAtendProgramado;
        private ContatoCliente codCliente;
        private ContatoCliente numContato;
        private Atendimento codEscrSebrae;
        private Atendimento codUsuario;
        private Atendimento numAtend;
        private DateTime dtaProgramada;
        private string hraProgramada;
        private string dscAssunto;

        private string logUsuario;
        #endregion

        #region Propriedades
        public int CodAtendProgramado
        {
            get { return codAtendProgramado; }
            set { codAtendProgramado = value; }
        }
        public ContatoCliente CodCliente
        {
            get { return codCliente; }
            set { codCliente = value; }
        }
        public ContatoCliente NumContato
        {
            get { return numContato; }
            set { numContato = value; }
        }
        public Atendimento CodEscrSebrae
        {
            get { return codEscrSebrae; }
            set { codEscrSebrae = value; }
        }
        public Atendimento CodUsuario
        {
            get { return codUsuario; }
            set { codUsuario = value; }
        }
        public Atendimento NumAtend
        {
            get { return numAtend; }
            set { numAtend = value; }
        }
        public DateTime DtaProgramada
        {
            get { return dtaProgramada; }
            set { dtaProgramada = value; }
        }
        public string HraProgramada
        {
            get { return hraProgramada; }
            set { hraProgramada = value; }
        }
        public string DscAssunto
        {
            get { return dscAssunto; }
            set { dscAssunto = value; }
        }
        public string LogUsuario
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }
        #endregion

        #region Construtores
        public AtendimentoProgramado()
            : this(-1)
        { }
        public AtendimentoProgramado(int codAtendProgramado)
        {
            this.codAtendProgramado = codAtendProgramado;
            this.codCliente = new ContatoCliente();
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "AtendimentoProgramado.AtendimentoProgramadoInc";
        private const string SPUPDATE = "AtendimentoProgramado.AtendimentoProgramadoAlt";
        private const string SPDELETE = "AtendimentoProgramado.AtendimentoProgramadoDel";
        private const string SPSELECTID = "AtendimentoProgramado.AtendimentoProgramadoSelId";
        private const string SPSELECTPAG = "AtendimentoProgramado.AtendimentoProgramadoSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMcodAtendProgramado = "codAtendProgramado";
        private const string PARMcodCliente = "codCliente";
        private const string PARMCURSOR = "curAtendimentoProgramado";
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
                    /*0*/ new OracleParameter(PARMcodAtendProgramado, OracleType.Int32, 4, ParameterDirection.InputOutput.ToString()) ,
                    /*1*/ new OracleParameter(PARMcodCliente, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*2*/ new OracleParameter( "numContato", OracleType.Int32),
                    /*3*/ new OracleParameter( "codEscrSebrae", OracleType.Int32),
                    /*4*/ new OracleParameter( "codUsuario", OracleType.Int32),
                    /*5*/ new OracleParameter( "numAtend", OracleType.Int32),
                    /*6*/ new OracleParameter( "dtaProgramada", OracleType.DateTime),
                    /*7*/ new OracleParameter( "hraProgramada", OracleType.VarChar),
                    /*8*/ new OracleParameter( "dscAssunto", OracleType.LongVarChar),
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
            parms[0].Value = this.codAtendProgramado;
            parms[1].Value = this.CodCliente.CodCliente.CodCLIENTE;
            parms[2].Value = this.NumContato.NumContato;
            parms[3].Value = this.CodEscrSebrae.CodEscrSebrae.CodEscrSebrae;
            parms[4].Value = this.CodUsuario.CodUsuario.CodUsuario;
            parms[5].Value = this.NumAtend.NumAtend;
            parms[6].Value = this.dtaProgramada;
            parms[7].Value = this.hraProgramada;
            parms[8].Value = this.dscAssunto.ToUpper() + " ";
            parms[9].Value = this.logUsuario;
            if (this.codAtendProgramado < 0)
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
                        codAtendProgramado = Convert.ToInt32(cmd.Parameters[PARMcodAtendProgramado].Value);
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
                codAtendProgramado = Convert.ToInt32(cmd.Parameters[PARMcodAtendProgramado].Value);
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
        /// <param name="codAtendProgramado">Código do Registro</param>
        public static void Delete(int codAtendProgramado, int codCliente)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMcodAtendProgramado, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodCliente, OracleType.Int32, 4)
            };
            parms[0].Value = codAtendProgramado;
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
        /// <param name="codAtendProgramado">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(int codAtendProgramado, int codCliente, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMcodAtendProgramado, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodCliente, OracleType.Int32, 4)
            };
                parms[0].Value = codAtendProgramado;
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
        /// <param name="codAtendProgramado">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codAtendProgramado, int codCliente)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodAtendProgramado, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodCliente, OracleType.Int32, 4),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codAtendProgramado;
            param[1].Value = codCliente;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="codAtendProgramado">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codAtendProgramado, int codCliente, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodAtendProgramado, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodCliente, OracleType.Int32, 4),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codAtendProgramado;
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
        /// <param name="codAtendProgramado">Código do Registro</param>
        /// <returns>AtendimentoProgramado</returns>
        public static AtendimentoProgramado GetDataRow(int codAtendProgramado, int codCliente)
        {
            OracleDataReader dr = LoadDataDr(codAtendProgramado, codCliente);
            AtendimentoProgramado AtendimentoProgramado = new AtendimentoProgramado();
            try
            {
                if (dr.Read())
                {
                    AtendimentoProgramado.codAtendProgramado = Convert.ToInt32(dr["A286_cd_atend_programado"]);
                    if (dr["A012_cd_cli"] != DBNull.Value && dr["A012_cd_cli"] != DBNull.Value && dr["A012_cd_cli"].ToString() != "")
                        AtendimentoProgramado.codCliente = new ContatoCliente(Convert.ToInt32(dr["A012_cd_cli"]));
                    AtendimentoProgramado.numContato.NumContato = Convert.ToInt32(dr["A014_num_cont"]);
                    AtendimentoProgramado.codEscrSebrae = new Atendimento(Convert.ToInt32(dr["A004_cd_escr"]));
                    AtendimentoProgramado.codUsuario.CodUsuario.CodUsuario = Convert.ToInt32(dr["A052_cd_usuario"]);
                    AtendimentoProgramado.numAtend.NumAtend = Convert.ToInt32(dr["A001_num_atend"]);
                    AtendimentoProgramado.dtaProgramada = Convert.ToDateTime(dr["A286_dt_programada"]);
                    AtendimentoProgramado.hraProgramada = Convert.ToString(dr["A286_hr_programada"]);
                    AtendimentoProgramado.dscAssunto = Convert.ToString(dr["A286_txt_assunto"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        AtendimentoProgramado.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                AtendimentoProgramado = new AtendimentoProgramado();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return AtendimentoProgramado;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codAtendProgramado">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>AtendimentoProgramado</returns>
        public static AtendimentoProgramado GetDataRow(int codAtendProgramado, int codCliente, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codAtendProgramado, codCliente, trans);
            AtendimentoProgramado AtendimentoProgramado = new AtendimentoProgramado();
            try
            {
                if (dr.Read())
                {
                    AtendimentoProgramado.codAtendProgramado = Convert.ToInt32(dr["A286_cd_atend_programado"]);
                    if (dr["A012_cd_cli"] != DBNull.Value && dr["A012_cd_cli"] != DBNull.Value && dr["A012_cd_cli"].ToString() != "")
                        AtendimentoProgramado.codCliente = new ContatoCliente(Convert.ToInt32(dr["A012_cd_cli"]));
                    AtendimentoProgramado.numContato.NumContato = Convert.ToInt32(dr["A014_num_cont"]);
                    AtendimentoProgramado.codEscrSebrae = new Atendimento(Convert.ToInt32(dr["A004_cd_escr"]));
                    AtendimentoProgramado.codUsuario.CodUsuario.CodUsuario = Convert.ToInt32(dr["A052_cd_usuario"]);
                    AtendimentoProgramado.numAtend.NumAtend = Convert.ToInt32(dr["A001_num_atend"]);
                    AtendimentoProgramado.dtaProgramada = Convert.ToDateTime(dr["A286_dt_programada"]);
                    AtendimentoProgramado.hraProgramada = Convert.ToString(dr["A286_hr_programada"]);
                    AtendimentoProgramado.dscAssunto = Convert.ToString(dr["A286_txt_assunto"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        AtendimentoProgramado.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                AtendimentoProgramado = new AtendimentoProgramado();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return AtendimentoProgramado;
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
