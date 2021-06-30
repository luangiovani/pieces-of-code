using System;
using System.Data.OracleClient;
using System.Data;

namespace Database.Entities
{
    /// <summary>
    /// Classe de Mapeamento com a Entidade de Banco de Dados - T794_Sit_Atend
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
    public class Sit_Atend
    {
        #region Atributos
        private int codSit_Atend;
        private string dscSit_Atend;
        private int codIndAtivo;
        private string logusuario;
        #endregion

        #region Propriedades
        public int CodSit_Atend
        {
            get { return codSit_Atend; }
            set { codSit_Atend = value; }
        }
        public string DscSit_Atend
        {
            get { return dscSit_Atend; }
            set { dscSit_Atend = value; }
        }
        public int CodIndAtivo
        {
            get { return codIndAtivo; }
            set { codIndAtivo = value; }
        }
        public string Logusuario
        {
            get { return logusuario; }
            set { logusuario = value; }
        }
        #endregion

        #region Construtores
        public Sit_Atend()
            : this(-1)
        { }
        public Sit_Atend(int codSit_Atend)
        {
            this.codSit_Atend = codSit_Atend;
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "Sit_Atend.Sit_AtendInc";
        private const string SPUPDATE = "Sit_Atend.Sit_AtendAlt";
        private const string SPDELETE = "Sit_Atend.Sit_AtendDel";
        private const string SPSELECTID = "Sit_Atend.Sit_AtendSelId";
        private const string SPSELECTPAG = "Sit_Atend.Sit_AtendSelPag";

        private const string SPSELECTPAGAE = "Sit_Atend.AtividadeEconSelPag";
        private const string SPSELECTAEID = "Sit_Atend.AtividadeEconSelId";
        private const string SPINSERTATIV = "Sit_Atend.AtividadeEconInc";

        private const string SPINSERTSTAT = "Sit_Atend.AtendSitInc";
        
        #endregion

        #region Parametros Oracle
        private const string PARMCODIGO = "codSit_Atend";
        private const string PARMCODIGOCli = "codCliente";
        private const string PARMCODIGOAtiv = "codAtividade";
        
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
                /*1*/ new OracleParameter( "dscSit_Atend", OracleType.VarChar),
                /*2*/ new OracleParameter( "codIndAtivo", OracleType.Int32),
                /*3*/ new OracleParameter( "logusuario", OracleType.VarChar)
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
            parms[0].Value = this.codSit_Atend;
            parms[1].Value = this.dscSit_Atend.ToUpper();
            parms[2].Value = this.codIndAtivo;
            parms[3].Value = this.logusuario.ToUpper();
            if (this.codSit_Atend < 0)
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
                        codSit_Atend = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
                codSit_Atend = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
                cmd.Parameters.Clear();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public static void InsertAtendSitComTrans(int codSit, int codAtend, int codEscr, int codUsuario, OracleTransaction trans)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter("codSit", OracleType.Int32, 8 ) ,
                new OracleParameter("codAtend", OracleType.Int32, 4) ,
                new OracleParameter("codEscr", OracleType.Int32, 4) ,
                new OracleParameter("codUsuario", OracleType.Int32, 4)
            };
            parms[0].Value = codSit;
            parms[1].Value = codAtend;
            parms[2].Value = codEscr;
            parms[3].Value = codUsuario;

            try
            {
                OracleCommand cmd = Context.DataBase.ExecuteNonQueryCmd(trans, CommandType.StoredProcedure, SPINSERTSTAT, parms);
                cmd.Parameters.Clear();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public static void InsertAtendSitSemTrans(int codSit, int codAtend, int codEscr, int codUsuario)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter("codSit", OracleType.Int32, 8) ,
                new OracleParameter("codAtend", OracleType.Int32, 4) ,
                new OracleParameter("codEscr", OracleType.Int32, 4) ,
                new OracleParameter("codUsuario", OracleType.Int32, 4)
            };
            parms[0].Value = codSit;
            parms[1].Value = codAtend;
            parms[2].Value = codEscr;
            parms[3].Value = codUsuario;

            using (OracleConnection conn = new OracleConnection(Context.DataBase.CONN_STRING))
            {
                conn.Open();
                using (OracleTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        Context.DataBase.ExecuteNonQuery(trans, CommandType.StoredProcedure, SPINSERTSTAT, parms);
                        trans.Commit();
                    }
                    catch (OracleException ex)
                    {
                        trans.Rollback();
                        throw (ex);
                    }
                }
            }
        } // end 

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
                new OracleParameter("cursit_Atend", OracleType.Cursor)
            };

            param[0].Value = codigo;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }
        
        public static OracleDataReader LoadAtivEconDr(int codcliente)
        {
            OracleParameter[] param = new OracleParameter[] { 

                new OracleParameter(PARMCODIGOCli, OracleType.Int32, 2), 
                new OracleParameter("cursit_Atend", OracleType.Cursor)
            };

            param[0].Value = codcliente;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTAEID, param);
            return dr;
        }

        public static OracleDataReader InsereAtivEcon(int codcliente, int codAtividade)
        {
            OracleParameter[] param = new OracleParameter[] { 

                new OracleParameter(PARMCODIGOCli, OracleType.Int32, 2), 
                new OracleParameter(PARMCODIGOAtiv, OracleType.Int32, 2), 
                new OracleParameter("cursit_Atend", OracleType.Cursor)
            };

            param[0].Value = codcliente;
            param[1].Value = codAtividade;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPINSERTATIV, param);
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
                                                              new OracleParameter("curSit_Atend", OracleType.Cursor)
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
        /// <returns>Sit_Atend</returns>
        public static Sit_Atend GetDataRow(int codigo)
        {
            OracleDataReader dr = LoadDataDr(codigo);
            Sit_Atend sit_Atend = new Sit_Atend();
            try
            {
                if (dr.Read())
                {
                    sit_Atend.codSit_Atend = Convert.ToInt32(dr["A794_cd_sit_atend"]);
                    sit_Atend.dscSit_Atend = Convert.ToString(dr["A794_dsc_sit_atend"]);
                    sit_Atend.codIndAtivo = Convert.ToInt32(dr["A794_ind_ativo"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                    sit_Atend.logusuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                sit_Atend = new Sit_Atend();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return sit_Atend;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>Sit_Atend</returns>
        public static Sit_Atend GetDataRow(int codigo, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codigo, trans);
            Sit_Atend sit_Atend = new Sit_Atend();
            try
            {
                if (dr.Read())
                {
                    sit_Atend.codSit_Atend = Convert.ToInt32(dr["A794_cd_sit_atend"]);
                    sit_Atend.dscSit_Atend = Convert.ToString(dr["A794_dsc_sit_atend"]);
                    sit_Atend.codIndAtivo = Convert.ToInt32(dr["A794_ind_ativo"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                    sit_Atend.logusuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                sit_Atend = new Sit_Atend();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return sit_Atend;
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
            new OracleParameter("curSit_Atend", OracleType.Cursor),
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
        //Economizando. como nao tem dados para ativide eco PF 
        public static Context.Paginacao LoadDataPaginacaoAtivEcon(string Where, int PaginaCorrente, int TamanhoPagina, string ExpressaoOrdenacao)
        {
            Context.Paginacao paginacao = new Context.Paginacao();

            OracleParameter[] parms = new OracleParameter[] { 
            new OracleParameter("sWhere", OracleType.VarChar,5000),
		    new OracleParameter("CurrentPage", OracleType.Int32),
		    new OracleParameter("PageSize", OracleType.Int32),
		    new OracleParameter("SortExpression", OracleType.VarChar,50),
            new OracleParameter("curSit_Atend", OracleType.Cursor),
            new OracleParameter("nRegistro", OracleType.Int32)
          };

            parms[0].Value = Where;
            parms[1].Value = PaginaCorrente;
            parms[2].Value = TamanhoPagina;
            parms[3].Value = ExpressaoOrdenacao;
            parms[4].Direction = ParameterDirection.Output;
            parms[5].Direction = ParameterDirection.Output;


            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTPAGAE, parms);

            paginacao.DataReader = dr;
            //  traz o total de registros de retorno direto dos parametros do ExecuteReader---
            paginacao.TotalRegistros = Convert.ToInt32(parms[5].Value);
            return paginacao;
        }

        #endregion


        #endregion

    }
}
