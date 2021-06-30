using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
using System.Data;

//-- Classe Classes Sebrae
//-- Data : 21/09/2007 
//-- Autor :  Honorato
//--

namespace Classes
{
    public class Autonomo  // T023_AUTONOMO  
    {
        #region Atributos
        private Cliente codCliente;
        private GrupoCnae codGruCnae;
        private SgrCnae codSubGrupoCnae;
        private AtividadeCnae codAtivCnae;
        private string tpoAutonomo;
        private string codCei;
        private string logUsuario;

        #endregion

        #region Propriedades
        public Cliente CodCliente
        {
            get { return codCliente; }
            set { codCliente = value; }
        }
        public GrupoCnae CodGruCnae
        {
            get { return codGruCnae; }
            set { codGruCnae = value; }
        }
        public SgrCnae CodSubGrupoCnae
        {
            get { return codSubGrupoCnae; }
            set { codSubGrupoCnae = value; }
        }
        public AtividadeCnae CodAtivCnae
        {
            get { return codAtivCnae; }
            set { codAtivCnae = value; }
        }
        public string TpoAutonomo
        {
            get { return tpoAutonomo; }
            set { tpoAutonomo = value; }
        }
        public string CodCei
        {
            get { return codCei; }
            set { codCei = value; }
        }
        public string LogUsuario
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }

        #endregion

        #region Construtores
        public Autonomo()
            : this(-1)
        { }
        public Autonomo(int codCliente)
        {
            this.codCliente = new Cliente();
            this.codGruCnae = new GrupoCnae();
            this.codSubGrupoCnae = new SgrCnae();
            this.codAtivCnae = new AtividadeCnae();
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "Autonomo.AutonomoInc";
        private const string SPUPDATE = "Autonomo.AutonomoAlt";
        private const string SPDELETE = "Autonomo.AutonomoDel";
        private const string SPSELECTID = "Autonomo.AutonomoSelId";
        private const string SPSELECTPAG = "Autonomo.AutonomoSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMCODIGO = "codCliente";
        private const string PARMCURSOR = "curAutonomo";
        #endregion

        #region Metodos

        #region GetParameters
        public static OracleParameter[] GetParameters()
        {
            OracleParameter[] parms;

            // Tentando buscar os parameters do cache        
            parms = DataBase.GetCachedParameters(SPINSERT);
            //parms = OutputCacheParameters(SPINSERT);
            if (parms == null)
            {
                parms = new OracleParameter[]{ 
                /*0*/ new OracleParameter(PARMCODIGO, OracleType.Int32),
                new OracleParameter( "codGruCnae", OracleType.Int32),
                new OracleParameter( "codSubGrupoCnae", OracleType.Int32),
                new OracleParameter( "codAtivCnae", OracleType.Int32),
                new OracleParameter( "tpoAutonomo", OracleType.VarChar),
                new OracleParameter( "codCei", OracleType.VarChar),
                /*22*/ new OracleParameter( "logUsuario", OracleType.VarChar)
            };

                // Criando cache dos parameters 
                DataBase.CacheParameters(SPINSERT, parms);
            }
            return (parms);
        }

        #endregion

        #region SetParameters
        public void SetParameters(OracleParameter[] parms)
        {
            parms[0].Value = this.codCliente.CodCLIENTE;
            parms[1].Value = this.codGruCnae.CodGruCnae;
            parms[2].Value = this.codSubGrupoCnae.CodSubGrupoCnae;
            parms[3].Value = this.codAtivCnae.CodAtivCnae;
            parms[4].Value = "";
            parms[5].Value = "";
            if (this.tpoAutonomo != null) 
            { parms[4].Value = this.tpoAutonomo.ToUpper(); }
            if (this.codCei != null) 
            { parms[5].Value = this.codCei.ToUpper();  }
            parms[6].Value = this.logUsuario.ToUpper();

                parms[0].Direction = ParameterDirection.Input; 
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

            using (OracleConnection conn = new OracleConnection(DataBase.CONN_STRING))
            {
                conn.Open();
                using (OracleTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        OracleCommand cmd = DataBase.ExecuteNonQueryCmd(trans, CommandType.StoredProcedure, SPINSERT, parms);
                        //Obtendo a chave de identificação do registro inserido.
                        codCliente.CodCLIENTE = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
                OracleCommand cmd = DataBase.ExecuteNonQueryCmd(trans, CommandType.StoredProcedure, SPINSERT, parms);
                //Obtendo a chave de identificação do registro inserido.
                codCliente.CodCLIENTE = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
            using (OracleConnection conn = new OracleConnection(DataBase.CONN_STRING))
            {
                conn.Open();
                using (OracleTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        DataBase.ExecuteNonQuery(trans, CommandType.StoredProcedure, SPUPDATE, parms);
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
                DataBase.ExecuteNonQuery(trans, CommandType.StoredProcedure, SPUPDATE, parms);
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
            using (OracleConnection conn = new OracleConnection(DataBase.CONN_STRING))
            {
                conn.Open();
                using (OracleTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        DataBase.ExecuteNonQuery(trans, CommandType.StoredProcedure, SPDELETE, parms);
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
                DataBase.ExecuteNonQuery(trans, CommandType.StoredProcedure, SPDELETE, parms);
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

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
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

            OracleDataReader dr = DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <returns>Autonomo</returns>
        public static Autonomo GetDataRow(int codigo)
        {
            OracleDataReader dr = LoadDataDr(codigo);
            Autonomo Autonomo = new Autonomo();
            try
            {
                if (dr.Read())
                {
                    Autonomo.codCliente = new Cliente(Convert.ToInt32(dr["a012_cd_Cli"]));
                    Autonomo.codGruCnae = new GrupoCnae(Convert.ToInt32(dr["A334_cd_gru_cnae"]));
                    Autonomo.codSubGrupoCnae = new SgrCnae(Convert.ToInt32(dr["A335_cod_sub_grupo_cnae"]));
                    Autonomo.codAtivCnae = new AtividadeCnae(Convert.ToInt32(dr["A336_cd_ativ_cnae"]));
                    if (dr["A023_tpo_autonomo"] != DBNull.Value && dr["A023_tpo_autonomo"] != DBNull.Value && dr["A023_tpo_autonomo"].ToString() != "")
                        Autonomo.tpoAutonomo = Convert.ToString(dr["A023_tpo_autonomo"]);
                    if (dr["A023_cd_cei"] != DBNull.Value && dr["A023_cd_cei"] != DBNull.Value && dr["A023_cd_cei"].ToString() != "")
                        Autonomo.codCei = Convert.ToString(dr["A023_cd_cei"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        Autonomo.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                Autonomo = new Autonomo();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return Autonomo;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>Autonomo</returns>
        public static Autonomo GetDataRow(int codigo, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codigo, trans);
            Autonomo Autonomo = new Autonomo();
            try
            {
                if (dr.Read())
                {
                    Autonomo.codCliente = new Cliente(Convert.ToInt32(dr["a012_cd_Cli"]));
                    Autonomo.codGruCnae = new GrupoCnae(Convert.ToInt32(dr["A334_cd_gru_cnae"]));
                    Autonomo.codSubGrupoCnae = new SgrCnae(Convert.ToInt32(dr["A335_cod_sub_grupo_cnae"]));
                    Autonomo.codAtivCnae = new AtividadeCnae(Convert.ToInt32(dr["A336_cd_ativ_cnae"]));
                    if (dr["A023_tpo_autonomo"] != DBNull.Value && dr["A023_tpo_autonomo"] != DBNull.Value && dr["A023_tpo_autonomo"].ToString() != "")
                        Autonomo.tpoAutonomo = Convert.ToString(dr["A023_tpo_autonomo"]);
                    if (dr["A023_cd_cei"] != DBNull.Value && dr["A023_cd_cei"] != DBNull.Value && dr["A023_cd_cei"].ToString() != "")
                        Autonomo.codCei = Convert.ToString(dr["A023_cd_cei"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        Autonomo.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                Autonomo = new Autonomo();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return Autonomo;
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
        public static Paginacao LoadDataPaginacao(string Where, int PaginaCorrente, int TamanhoPagina, string ExpressaoOrdenacao)
        {
            Paginacao paginacao = new Paginacao();

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


            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTPAG, parms);

            // dr.Read();

            paginacao.DataReader = dr;
            //  traz o total de registros de retorno direto dos parametros do ExecuteReader---
            paginacao.TotalRegistros = Convert.ToInt32(parms[5].Value);
            return paginacao;
        }
        #endregion


        #endregion

    }
}