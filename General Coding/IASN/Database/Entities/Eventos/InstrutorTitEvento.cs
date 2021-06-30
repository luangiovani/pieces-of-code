using System;
using System.Data.OracleClient;
using System.Data;

namespace Database.Entities
{
    /// <summary>
    /// Classe de Mapeamento com a Entidade de Banco de Dados - T029_INST_SERV
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
    public class InstrutorTitEvento
    {
        #region Atributos
        private Pessoa codPes;
        private TituloEvento codTituloEv;
        private int? codCondPgto;
        private decimal? vlrLocalResid;
        private decimal? vlrForaResid;
        private string logUsuario;
        #endregion

        #region Propriedades
        public Pessoa CodPes
        {
            get { return codPes; }
            set { codPes = value; }
        }
        public TituloEvento CodTituloEv
        {
            get { return codTituloEv; }
            set { codTituloEv = value; }
        }
        public int? CodCondPgto
        {
            get { return codCondPgto; }
            set { codCondPgto = value; }
        }
        public decimal? VlrLocalResid
        {
            get { return vlrLocalResid; }
            set { vlrLocalResid = value; }
        }
        public decimal? VlrForaResid
        {
            get { return vlrForaResid; }
            set { vlrForaResid = value; }
        }
        public string LogUsuario
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }
        #endregion

        #region Construtores
        public InstrutorTitEvento()
            : this(-1)
        { }
        public InstrutorTitEvento(int InstrutorTitEvento )
        {
            this.codPes = new Pessoa();
            this.codTituloEv = new TituloEvento();
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "InstrutorTitEvento.InstrutorTitEventoInc";
        private const string SPUPDATE = "InstrutorTitEvento.InstrutorTitEventoAlt";
        private const string SPDELETE = "InstrutorTitEvento.InstrutorTitEventoDel";
        private const string SPSELECTID = "InstrutorTitEvento.InstrutorTitEventoSelId";
        private const string SPSELECTPAG = "InstrutorTitEvento.InstrutorTitEventoSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMcodPes = "codPes";
        private const string PARMcodTituloEv = "codTituloEv";
        private const string PARMCURSOR = "curInstrutorTitEvento";
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
                    /*0*/ new OracleParameter(PARMcodPes, OracleType.Int32, 4, ParameterDirection.InputOutput.ToString()) ,
                    /*1*/ new OracleParameter(PARMcodTituloEv, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*2*/ new OracleParameter( "codCondPgto", OracleType.Int32),
                    /*3*/ new OracleParameter( "vlrLocalResid", OracleType.Float),
                    /*4*/ new OracleParameter( "vlrForaResid", OracleType.Float),
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
            parms[0].Value = this.codPes.CodPessoa; 
            parms[1].Value = this.codTituloEv.CodTituloEv;
            parms[2].Value = DBNull.Value;
            if (this.codCondPgto != null)
            { parms[2].Value = this.codCondPgto; }
            parms[3].Value = DBNull.Value;
            if (this.vlrLocalResid != null)
            { parms[3].Value = this.vlrLocalResid; }
            parms[4].Value = DBNull.Value;
            if (this.vlrForaResid != null)
            { parms[4].Value = this.vlrForaResid; }
            parms[5].Value = this.logUsuario;
            if (this.codPes.CodPessoa < 0)
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
                        codPes.CodPessoa = Convert.ToInt32(cmd.Parameters[PARMcodPes].Value);
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
                codPes.CodPessoa = Convert.ToInt32(cmd.Parameters[PARMcodPes].Value);
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
        /// <param name="codPes">Código do Registro</param>
        public static void Delete(int codPes, int codTituloEv)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMcodPes, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodTituloEv, OracleType.Int32, 4)
            };
            parms[0].Value = codPes;
            parms[1].Value = codTituloEv;
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
        /// <param name="codPes">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(int codPes, int codTituloEv, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMcodPes, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodTituloEv, OracleType.Int32, 4)
            };
                parms[0].Value = codPes;
                parms[1].Value = codTituloEv;
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
        /// <param name="codPes">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codPes, int codTituloEv)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodPes, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodTituloEv, OracleType.Int32, 4),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codPes;
            param[1].Value = codTituloEv;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="codPes">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codPes, int codTituloEv, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodPes, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodTituloEv, OracleType.Int32, 4),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codPes;
            param[1].Value = codTituloEv;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="codPes">Código do Registro</param>
        /// <returns>InstrutorTitEvento</returns>
        public static InstrutorTitEvento GetDataRow(int codPes, int codTituloEv)
        {
            OracleDataReader dr = LoadDataDr(codPes, codTituloEv);
            InstrutorTitEvento InstrutorTitEvento = new InstrutorTitEvento();
            try
            {
                if (dr.Read())
                {
                    if (dr["A125_cd_instr"] != DBNull.Value && dr["A125_cd_instr"] != DBNull.Value && dr["A125_cd_instr"].ToString() != "")
                       InstrutorTitEvento.codPes = new Pessoa( Convert.ToInt32(dr["A125_cd_instr"]));
                    if (dr["A008_cd_tit_ev"] != DBNull.Value && dr["A008_cd_tit_ev"] != DBNull.Value && dr["A008_cd_tit_ev"].ToString() != "")
                       InstrutorTitEvento.codTituloEv = new TituloEvento( Convert.ToInt32(dr["A008_cd_tit_ev"]));
                    if (dr["A2204_cd_cond_pgto"] != DBNull.Value && dr["A2204_cd_cond_pgto"] != DBNull.Value && dr["A2204_cd_cond_pgto"].ToString() != "")
                       InstrutorTitEvento.codCondPgto = Convert.ToInt32(dr["A2204_cd_cond_pgto"]);
                    if (dr["A029_vlr_local_resid"] != DBNull.Value && dr["A029_vlr_local_resid"] != DBNull.Value && dr["A029_vlr_local_resid"].ToString() != "")
                       InstrutorTitEvento.vlrLocalResid = Convert.ToInt32(dr["A029_vlr_local_resid"]);
                    if (dr["A029_vlr_fora_resid"] != DBNull.Value && dr["A029_vlr_fora_resid"] != DBNull.Value && dr["A029_vlr_fora_resid"].ToString() != "")
                       InstrutorTitEvento.vlrForaResid = Convert.ToInt32(dr["A029_vlr_fora_resid"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                       InstrutorTitEvento.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                InstrutorTitEvento = new InstrutorTitEvento();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return InstrutorTitEvento;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codPes">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>InstrutorTitEvento</returns>
        public static InstrutorTitEvento GetDataRow(int codPes, int codTituloEv, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codPes, codTituloEv, trans);
            InstrutorTitEvento InstrutorTitEvento = new InstrutorTitEvento();
            try
            {
                if (dr.Read())
                {
                    if (dr["A125_cd_instr"] != DBNull.Value && dr["A125_cd_instr"] != DBNull.Value && dr["A125_cd_instr"].ToString() != "")
                        InstrutorTitEvento.codPes = new Pessoa(Convert.ToInt32(dr["A125_cd_instr"]));
                    if (dr["A008_cd_tit_ev"] != DBNull.Value && dr["A008_cd_tit_ev"] != DBNull.Value && dr["A008_cd_tit_ev"].ToString() != "")
                        InstrutorTitEvento.codTituloEv = new TituloEvento(Convert.ToInt32(dr["A008_cd_tit_ev"]));
                    if (dr["A2204_cd_cond_pgto"] != DBNull.Value && dr["A2204_cd_cond_pgto"] != DBNull.Value && dr["A2204_cd_cond_pgto"].ToString() != "")
                        InstrutorTitEvento.codCondPgto = Convert.ToInt32(dr["A2204_cd_cond_pgto"]);
                    if (dr["A029_vlr_local_resid"] != DBNull.Value && dr["A029_vlr_local_resid"] != DBNull.Value && dr["A029_vlr_local_resid"].ToString() != "")
                        InstrutorTitEvento.vlrLocalResid = Convert.ToInt32(dr["A029_vlr_local_resid"]);
                    if (dr["A029_vlr_fora_resid"] != DBNull.Value && dr["A029_vlr_fora_resid"] != DBNull.Value && dr["A029_vlr_fora_resid"].ToString() != "")
                        InstrutorTitEvento.vlrForaResid = Convert.ToInt32(dr["A029_vlr_fora_resid"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        InstrutorTitEvento.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                InstrutorTitEvento = new InstrutorTitEvento();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return InstrutorTitEvento;
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
