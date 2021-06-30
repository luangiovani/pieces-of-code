using System;
using System.Data.OracleClient;
using System.Data;

namespace Database.Entities
{
    /// <summary>
    /// Classe de Mapeamento com a Entidade de Banco de Dados - T038_AUTONOMO_ATIV
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
    public class AutonomoAtividade
    {
        #region Atributos
        private AtividadeAutonomo codAtividadeAutonomo;
        private Cliente codCliente;
        private int? indAtPrin;
        private string logUsuario;
        #endregion

        #region Propriedades
        public AtividadeAutonomo CodAtividadeAutonomo
        {
            get { return codAtividadeAutonomo; }
            set { codAtividadeAutonomo = value; }
        }
        public Cliente CodCliente
        {
            get { return codCliente; }
            set { codCliente = value; }
        }
        public int? IndAtPrin
        {
            get { return indAtPrin; }
            set { indAtPrin = value; }
        }
        public string LogUsuario
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }
        #endregion

        #region Construtores
        public AutonomoAtividade()
            : this(-1)
        { }
        public AutonomoAtividade(int codAtividadeAutonomo)
        {
            this.codAtividadeAutonomo = new AtividadeAutonomo();
            this.codCliente = new Cliente();
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "AutonomoAtividade.AutonomoAtividadeInc";
        private const string SPUPDATE = "AutonomoAtividade.AutonomoAtividadeAlt";
        private const string SPDELETE = "AutonomoAtividade.AutonomoAtividadeDel";
        private const string SPSELECTID = "AutonomoAtividade.AutonomoAtividadeSelId";
        private const string SPSELECTPAG = "AutonomoAtividade.AutonomoAtividadeSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMcodAtividadeAutonomo = "codAtividadeAutonomo";
        private const string PARMcodCliente = "codCliente";
        private const string PARMCURSOR = "curAutonomoAtividade";
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
                    /*0*/ new OracleParameter(PARMcodAtividadeAutonomo, OracleType.VarChar, 4, ParameterDirection.InputOutput.ToString()) ,
                    /*1*/ new OracleParameter(PARMcodCliente, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*2*/ new OracleParameter( "indAtPrin", OracleType.Int32),
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
            parms[0].Value = this.codAtividadeAutonomo.CodAtividadeAutonomo;
            parms[1].Value = this.codCliente.CodCLIENTE;
            parms[2].Value = DBNull.Value;
            if (this.indAtPrin != null)
            {
                parms[2].Value = this.indAtPrin;
            }
            parms[3].Value = this.logUsuario;
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
                        codAtividadeAutonomo = new AtividadeAutonomo(Convert.ToString(cmd.Parameters[PARMcodAtividadeAutonomo].Value));
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
                //codAtividadeAutonomo = Convert.ToString(cmd.Parameters[PARMcodAtividadeAutonomo].Value);
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
        /// <param name="codAtividadeAutonomo">Código do Registro</param>
        public static void Delete(int codAtividadeAutonomo, int codCliente)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMcodAtividadeAutonomo, OracleType.VarChar, 4)  ,
                new OracleParameter(PARMcodCliente, OracleType.Int32, 4)
            };
            parms[0].Value = codAtividadeAutonomo;
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
        /// <param name="codAtividadeAutonomo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(int codAtividadeAutonomo, int codCliente, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMcodAtividadeAutonomo, OracleType.VarChar, 4)  ,
                new OracleParameter(PARMcodCliente, OracleType.Int32, 4)
            };
                parms[0].Value = codAtividadeAutonomo;
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
        /// <param name="codAtividadeAutonomo">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codAtividadeAutonomo, int codCliente)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodAtividadeAutonomo, OracleType.VarChar, 4), 
                    new OracleParameter(PARMcodCliente, OracleType.Int32, 4),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codAtividadeAutonomo;
            param[1].Value = codCliente;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="codAtividadeAutonomo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codAtividadeAutonomo, int codCliente, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodAtividadeAutonomo, OracleType.VarChar, 4), 
                    new OracleParameter(PARMcodCliente, OracleType.Int32, 4),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codAtividadeAutonomo;
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
        /// <param name="codAtividadeAutonomo">Código do Registro</param>
        /// <returns>AutonomoAtividade</returns>
        public static AutonomoAtividade GetDataRow(int codAtividadeAutonomo, int codCliente)
        {
            OracleDataReader dr = LoadDataDr(codAtividadeAutonomo, codCliente);
            AutonomoAtividade AutonomoAtividade = new AutonomoAtividade();
            try
            {
                if (dr.Read())
                {
                    AutonomoAtividade.codAtividadeAutonomo = new AtividadeAutonomo(Convert.ToString(dr["A288_cd_ativ_autonoma"]));
                    AutonomoAtividade.codCliente = new Cliente(Convert.ToInt32(dr["A012_cd_cli"]));
                    if (dr["A038_ind_at_prin"] != DBNull.Value && dr["A038_ind_at_prin"] != DBNull.Value && dr["A038_ind_at_prin"].ToString() != "")
                        AutonomoAtividade.indAtPrin = Convert.ToInt32(dr["A038_ind_at_prin"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        AutonomoAtividade.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                AutonomoAtividade = new AutonomoAtividade();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return AutonomoAtividade;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codAtividadeAutonomo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>AutonomoAtividade</returns>
        public static AutonomoAtividade GetDataRow(int codAtividadeAutonomo, int codCliente, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codAtividadeAutonomo, codCliente, trans);
            AutonomoAtividade AutonomoAtividade = new AutonomoAtividade();
            try
            {
                if (dr.Read())
                {
                    AutonomoAtividade.codAtividadeAutonomo = new AtividadeAutonomo(Convert.ToString(dr["A288_cd_ativ_autonoma"]));
                    AutonomoAtividade.codCliente = new Cliente(Convert.ToInt32(dr["A012_cd_cli"]));
                    if (dr["A038_ind_at_prin"] != DBNull.Value && dr["A038_ind_at_prin"] != DBNull.Value && dr["A038_ind_at_prin"].ToString() != "")
                        AutonomoAtividade.indAtPrin = Convert.ToInt32(dr["A038_ind_at_prin"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        AutonomoAtividade.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                AutonomoAtividade = new AutonomoAtividade();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return AutonomoAtividade;
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