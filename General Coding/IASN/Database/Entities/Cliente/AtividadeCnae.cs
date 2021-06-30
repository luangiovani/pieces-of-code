using System;
using System.Data.OracleClient;
using System.Data;

namespace Database.Entities
{
    /// <summary>
    /// Classe de Mapeamento com a Entidade de Banco de Dados - T336_ATIV_CNAE
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
    public class AtividadeCnae
    {
        #region Atributos
        private int codAtivCnae;
        private SgrCnae codSubGrupoCnae;
        private GrupoCnae codGruCnae;
        private string dscAtivCnae;
        private string logUsuario;
        #endregion

        #region Propriedades
        public int CodAtivCnae
        {
            get { return codAtivCnae; }
            set { codAtivCnae = value; }
        }
        public SgrCnae CodSubGrupoCnae
        {
            get { return codSubGrupoCnae; }
            set { codSubGrupoCnae = value; }
        }
        public GrupoCnae CodGruCnae
        {
            get { return codGruCnae; }
            set { codGruCnae = value; }
        }
        public string DscAtivCnae
        {
            get { return dscAtivCnae; }
            set { dscAtivCnae = value; }
        }
        public string LogUsuario 
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }
        #endregion

        #region Construtores
        public AtividadeCnae()
            : this(-1)
        { }
        public AtividadeCnae(int codAtivCnae)
        {
            this.codAtivCnae = codAtivCnae; 
            this.codSubGrupoCnae = new SgrCnae(); 
            this.codGruCnae = new GrupoCnae();
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "AtividadeCnae.AtividadeCnaeInc";
        private const string SPUPDATE = "AtividadeCnae.AtividadeCnaeAlt";
        private const string SPDELETE = "AtividadeCnae.AtividadeCnaeDel";
        private const string SPSELECTID = "AtividadeCnae.AtividadeCnaeSelId";
        private const string SPSELECTPAG = "AtividadeCnae.AtividadeCnaeSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMcodAtivCnae = "codAtivCnae";
        private const string PARMcodSubGrupoCnae = "codSubGrupoCnae";
        private const string PARMcodGruCnae = "codGruCnae";
        private const string PARMCURSOR = "curAtividadeCnae";
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
                    /*0*/ new OracleParameter(PARMcodAtivCnae, OracleType.Int32, 4, ParameterDirection.InputOutput.ToString()) ,
                    /*1*/ new OracleParameter(PARMcodSubGrupoCnae, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*2*/ new OracleParameter(PARMcodGruCnae, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*3*/ new OracleParameter( "dscAtivCnae", OracleType.VarChar),
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
            parms[0].Value = this.codAtivCnae;
            parms[1].Value = this.codSubGrupoCnae;
            parms[2].Value = this.codGruCnae;
            parms[3].Value = this.dscAtivCnae;
            parms[4].Value = this.logUsuario;
            if (this.codAtivCnae < 0)
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
                        codAtivCnae = Convert.ToInt32(cmd.Parameters[PARMcodAtivCnae].Value);
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
                codAtivCnae = Convert.ToInt32(cmd.Parameters[PARMcodAtivCnae].Value);
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
        /// <param name="codAtivCnae">Código do Registro</param>
        public static void Delete(int codAtivCnae, int codSubGrupoCnae, int codGruCnae)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMcodAtivCnae, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodSubGrupoCnae, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodGruCnae, OracleType.Int32, 4)
            };
            parms[0].Value = codAtivCnae;
            parms[1].Value = codSubGrupoCnae;
            parms[2].Value = codGruCnae;
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
        /// <param name="codAtivCnae">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(int codAtivCnae, int codSubGrupoCnae, int codGruCnae, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMcodAtivCnae, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodSubGrupoCnae, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodGruCnae, OracleType.Int32, 4) 
            };
                parms[0].Value = codAtivCnae;
                parms[1].Value = codSubGrupoCnae;
                parms[2].Value = codGruCnae;
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
        /// <param name="codAtivCnae">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codAtivCnae, int codSubGrupoCnae, int codGruCnae)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodAtivCnae, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodSubGrupoCnae, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodGruCnae, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codAtivCnae;
            param[1].Value = codSubGrupoCnae;
            param[2].Value = codGruCnae;
            param[3].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="codAtivCnae">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codAtivCnae, int codSubGrupoCnae, int codGruCnae, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodAtivCnae, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodSubGrupoCnae, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodGruCnae, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codAtivCnae;
            param[1].Value = codSubGrupoCnae;
            param[2].Value = codGruCnae;
            param[3].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="codAtivCnae">Código do Registro</param>
        /// <returns>AtividadeCnae</returns>
        public static AtividadeCnae GetDataRow(int codAtivCnae, int codSubGrupoCnae, int codGruCnae)
        {
            OracleDataReader dr = LoadDataDr(codAtivCnae, codSubGrupoCnae, codGruCnae);
            AtividadeCnae AtividadeCnae = new AtividadeCnae();
            try
            {
                if (dr.Read())
                {
                    AtividadeCnae.codAtivCnae = Convert.ToInt32(dr["A336_cd_ativ_cnae"]);
                    if (dr["A335_cod_sub_grupo_cnae"] != DBNull.Value && dr["A335_cod_sub_grupo_cnae"] != DBNull.Value && dr["A335_cod_sub_grupo_cnae"].ToString() != "")
                        AtividadeCnae.codSubGrupoCnae = new SgrCnae(Convert.ToInt32(dr["A335_cod_sub_grupo_cnae"]));
                    if (dr["a334_cd_gru_cnae"] != DBNull.Value && dr["a334_cd_gru_cnae"] != DBNull.Value && dr["a334_cd_gru_cnae"].ToString() != "")
                        AtividadeCnae.codGruCnae = new GrupoCnae(Convert.ToInt32(dr["a334_cd_gru_cnae"]));
                    AtividadeCnae.dscAtivCnae = Convert.ToString(dr["A336_dsc_ativ_cnae"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        AtividadeCnae.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                AtividadeCnae = new AtividadeCnae();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return AtividadeCnae;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codAtivCnae">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>AtividadeCnae</returns>
        public static AtividadeCnae GetDataRow(int codAtivCnae, int codSubGrupoCnae, int codGruCnae, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codAtivCnae, codSubGrupoCnae, codGruCnae, trans);
            AtividadeCnae AtividadeCnae = new AtividadeCnae();
            try
            {
                if (dr.Read())
                {
                    AtividadeCnae.codAtivCnae = Convert.ToInt32(dr["A336_cd_ativ_cnae"]);
                    if (dr["A335_cod_sub_grupo_cnae"] != DBNull.Value && dr["A335_cod_sub_grupo_cnae"] != DBNull.Value && dr["A335_cod_sub_grupo_cnae"].ToString() != "")
                        AtividadeCnae.codSubGrupoCnae = new SgrCnae(Convert.ToInt32(dr["A335_cod_sub_grupo_cnae"]));
                    if (dr["A035_cd_GrupoCnae"] != DBNull.Value && dr["A035_cd_GrupoCnae"] != DBNull.Value && dr["A035_cd_GrupoCnae"].ToString() != "")
                        AtividadeCnae.codGruCnae = new GrupoCnae(Convert.ToInt32(dr["A035_cd_GrupoCnae"]));
                    AtividadeCnae.dscAtivCnae = Convert.ToString(dr["A336_dsc_ativ_cnae"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        AtividadeCnae.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                AtividadeCnae = new AtividadeCnae();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return AtividadeCnae;
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
