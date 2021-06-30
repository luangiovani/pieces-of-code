using System;
using System.Data.OracleClient;
using System.Data;

namespace Database.Entities
{
    /// <summary>
    /// Classe de Mapeamento com a Entidade de Banco de Dados - T024_CONTATO_ACAO
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
    public class ContatoAcao
    {
        #region Atributos
        private int codAcao;
        private ContatoCliente numContato;
        private ContatoCliente codCliente;
        private FonteInform codFonte;
        private string logUsuario;
        #endregion

        #region Propriedades
        public int CodAcao
        {
            get { return codAcao; }
            set { codAcao = value; }
        }
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
        public FonteInform CodFonte
        {
            get { return codFonte; }
            set { codFonte = value; }
        }
        public string LogUsuario
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }
        #endregion

        #region Construtores
        public ContatoAcao()
            : this(-1)
        { }
        public ContatoAcao(int codAcao)
        {
            this.codAcao = codAcao;
            this.numContato = new ContatoCliente();
            this.codCliente = new ContatoCliente();
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "ContatoAcao.ContatoAcaoInc";
        private const string SPUPDATE = "ContatoAcao.ContatoAcaoAlt";
        private const string SPDELETE = "ContatoAcao.ContatoAcaoDel";
        private const string SPSELECTID = "ContatoAcao.ContatoAcaoSelId";
        private const string SPSELECTPAG = "ContatoAcao.ContatoAcaoSelPag";
        private const string SPSELECTACAOID = "ContatoAcao.AcaoCombo";
        #endregion

        #region Parametros Oracle
        private const string PARMcodAcao = "codAcao";
        private const string PARMnumContato = "numContato";
        private const string PARMcodCliente = "codCliente";
        private const string PARMCURSOR = "curContatoAcao";
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
                    /*0*/ new OracleParameter(PARMcodAcao, OracleType.Int32, 4, ParameterDirection.InputOutput.ToString()) ,
                    /*1*/ new OracleParameter(PARMnumContato, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*2*/ new OracleParameter(PARMcodCliente, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*3*/ new OracleParameter( "codFonte", OracleType.Int32),
                    /*4*/ new OracleParameter( "logUsuario", OracleType.VarChar)
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
            parms[0].Value = this.codAcao;
            parms[1].Value = this.numContato.NumContato;
            parms[2].Value = this.codCliente.CodCliente.CodCLIENTE;
            parms[3].Value = DBNull.Value; 
            if (this.codFonte != null)
            {
                parms[3].Value = this.codFonte;
            }
            parms[4].Value = this.logUsuario;
            if (this.codAcao < 0)
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
                        codAcao = Convert.ToInt32(cmd.Parameters[PARMcodAcao].Value);
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
                codAcao = Convert.ToInt32(cmd.Parameters[PARMcodAcao].Value);
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
        /// <param name="codAcao">Código do Registro</param>
        public static void Delete(int codAcao, int numContato, int codCliente)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMcodAcao, OracleType.Int32, 4)  ,
                new OracleParameter(PARMnumContato, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodCliente, OracleType.Int32, 4)
            };
            parms[0].Value = codAcao;
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
        /// <param name="codAcao">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(int codAcao, int numContato, int codCliente, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMcodAcao, OracleType.Int32, 4)  ,
                new OracleParameter(PARMnumContato, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodCliente, OracleType.Int32, 4) 
            };
                parms[0].Value = codAcao;
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
        /// <param name="codAcao">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codAcao, int numContato, int codCliente)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodAcao, OracleType.Int32, 4), 
                    new OracleParameter(PARMnumContato, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodCliente, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codAcao;
            param[1].Value = numContato;
            param[2].Value = codCliente;
            param[3].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="codAcao">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codAcao, int numContato, int codCliente, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodAcao, OracleType.Int32, 4), 
                    new OracleParameter(PARMnumContato, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodCliente, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codAcao;
            param[1].Value = numContato;
            param[2].Value = codCliente;
            param[3].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region LoadDataDdlAcaoCombo
        public static OracleDataReader LoadDataDdlAcaoCombo()
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTACAOID, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="codAcao">Código do Registro</param>
        /// <returns>ContatoAcao</returns>
        public static ContatoAcao GetDataRow(int codAcao, int numContato, int codCliente)
        {
            OracleDataReader dr = LoadDataDr(codAcao, numContato, codCliente);
            ContatoAcao ContatoAcao = new ContatoAcao();
            try
            {
                if (dr.Read())
                {
                    ContatoAcao.codAcao = Convert.ToInt32(dr["A027_cd_acao"]);
                    if (dr["A014_num_cont"] != DBNull.Value && dr["A014_num_cont"] != DBNull.Value && dr["A014_num_cont"].ToString() != "")
                        ContatoAcao.numContato = new ContatoCliente(Convert.ToInt32(dr["A014_num_cont"]));
                    if (dr["A012_cd_cli"] != DBNull.Value && dr["A012_cd_cli"] != DBNull.Value && dr["A012_cd_cli"].ToString() != "")
                        ContatoAcao.codCliente.CodCliente.CodCLIENTE = Convert.ToInt32(dr["A012_cd_cli"]);
                    if (dr["A044_cd_fonte"] != DBNull.Value && dr["A044_cd_fonte"] != DBNull.Value && dr["A044_cd_fonte"].ToString() != "")
                        ContatoAcao.codFonte = new FonteInform(Convert.ToInt32(dr["A044_cd_fonte"]));
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        ContatoAcao.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                ContatoAcao = new ContatoAcao();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return ContatoAcao;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codAcao">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>ContatoAcao</returns>
        public static ContatoAcao GetDataRow(int codAcao, int numContato, int codCliente, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codAcao, numContato, codCliente, trans);
            ContatoAcao ContatoAcao = new ContatoAcao();
            try
            {
                if (dr.Read())
                {
                    ContatoAcao.codAcao = Convert.ToInt32(dr["A027_cd_acao"]);
                    if (dr["A014_num_cont"] != DBNull.Value && dr["A014_num_cont"] != DBNull.Value && dr["A014_num_cont"].ToString() != "")
                        ContatoAcao.numContato = new ContatoCliente(Convert.ToInt32(dr["A014_num_cont"]));
                    if (dr["A012_cd_cli"] != DBNull.Value && dr["A012_cd_cli"] != DBNull.Value && dr["A012_cd_cli"].ToString() != "")
                        ContatoAcao.codCliente.CodCliente.CodCLIENTE = Convert.ToInt32(dr["A012_cd_cli"]);
                    if (dr["A044_cd_fonte"] != DBNull.Value && dr["A044_cd_fonte"] != DBNull.Value && dr["A044_cd_fonte"].ToString() != "")
                        ContatoAcao.codFonte = new FonteInform(Convert.ToInt32(dr["A044_cd_fonte"]));
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        ContatoAcao.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                ContatoAcao = new ContatoAcao();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return ContatoAcao;
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