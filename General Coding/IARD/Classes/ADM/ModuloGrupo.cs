using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
 
using System.Data;

//-- Classe Classes Sebrae
//-- Data : 18/05/2007 
//-- Autor :  Daniel

namespace Classes
{
    public class ModuloGrupo // T371_modulo_grupo 
    {
        #region Atributos
        private int codGrupo;
        private int codModulo;
        private string dscGrupo;
        private int idMaster;
        private int codUsuario;
        private string logUsuario;
        #endregion

        #region Propriedades
        public int CodGrupo
        {
            get { return codGrupo; }
            set { codGrupo = value; }
        }
        public int CodModulo
        {
            get { return codModulo; }
            set { codModulo = value; }
        }
        public string DscGrupo
        {
            get { return dscGrupo; }
            set { dscGrupo = value; }
        }
        public int IdMaster
        {
            get { return idMaster; }
            set { idMaster = value; }
        }
        public int CodUsuario
        {
            get { return codUsuario; }
            set { codUsuario = value; }
        }
        public string LogUsuario
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }
        #endregion

        #region Construtores
        public ModuloGrupo()
            : this(-1)
        { }
        public ModuloGrupo(int codGrupo)
        {
            this.codGrupo = codGrupo;
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "ModuloGrupo.ModuloGrupoInc";
        private const string SPUPDATE = "ModuloGrupo.ModuloGrupoAlt";
        private const string SPDELETE = "ModuloGrupo.ModuloGrupoDel";
        private const string SPSELECTID = "ModuloGrupo.ModuloGrupoSelId";
        private const string SPSELECTPAG = "ModuloGrupo.ModuloGrupoSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMCODGRUPO = "codGrupo";
        private const string PARMCODMODULO = "codModulo";
        private const string PARMCURSOR = "curModuloGrupo";
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
                    /*0*/ new OracleParameter(PARMCODGRUPO, OracleType.Int32, 4, ParameterDirection.InputOutput.ToString()) ,
                    /*1*/ new OracleParameter(PARMCODMODULO, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*2*/ new OracleParameter( "dscGrupo", OracleType.VarChar),
                    /*3*/ new OracleParameter( "idMaster", OracleType.Int32),
                    /*4*/ new OracleParameter( "codUsuario", OracleType.Int32),
                    /*5*/ new OracleParameter( "logUsuario", OracleType.VarChar)
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
            parms[0].Value = this.codGrupo;
            parms[1].Value = this.codModulo;
            parms[2].Value = this.dscGrupo;
            parms[3].Value = this.idMaster;
            parms[4].Value = this.CodUsuario;
            parms[5].Value = this.logUsuario;
            if (this.codGrupo < 0)
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

            using (OracleConnection conn = new OracleConnection(DataBase.CONN_STRING))
            {
                conn.Open();
                using (OracleTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        OracleCommand cmd = DataBase.ExecuteNonQueryCmd(trans, CommandType.StoredProcedure, SPINSERT, parms);
                        //Obtendo a chave de identificação do registro inserido.
                        codGrupo = Convert.ToInt32(cmd.Parameters[PARMCODGRUPO].Value);
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
                codGrupo = Convert.ToInt32(cmd.Parameters[PARMCODGRUPO].Value);
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
        /// <param name="codGrupo">Código do Registro</param>
        public static void Delete(int codGrupo, int codModulo)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMCODGRUPO, OracleType.Int32, 4)  ,
                new OracleParameter(PARMCODMODULO, OracleType.Int32, 4)
            };
            parms[0].Value = codGrupo;
            parms[1].Value = codModulo;
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
        /// <param name="codGrupo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(int codGrupo, int codModulo, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMCODGRUPO, OracleType.Int32, 4)  ,
                new OracleParameter(PARMCODMODULO, OracleType.Int32, 4)
            };
                parms[0].Value = codGrupo;
                parms[1].Value = codModulo;
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
        /// <param name="codGrupo">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codGrupo, int codModulo)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCODGRUPO, OracleType.Int32, 4), 
                    new OracleParameter(PARMCODMODULO, OracleType.Int32, 4),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codGrupo;
            param[1].Value = codModulo;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="codGrupo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codGrupo, int codModulo, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCODGRUPO, OracleType.Int32, 4), 
                    new OracleParameter(PARMCODMODULO, OracleType.Int32, 4),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codGrupo;
            param[1].Value = codModulo;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="codGrupo">Código do Registro</param>
        /// <returns>ModuloGrupo</returns>
        public static ModuloGrupo GetDataRow(int codGrupo, int codModulo)
        {
            OracleDataReader dr = LoadDataDr(codGrupo, codModulo);
            ModuloGrupo moduloGrupo = new ModuloGrupo();
            try
            {
                if (dr.Read())
                {
                    moduloGrupo.codGrupo = Convert.ToInt32(dr["A371_cd_grupo"]);
                    moduloGrupo.codModulo = Convert.ToInt32(dr["A270_cd_modulo"]);
                    moduloGrupo.dscGrupo = Convert.ToString(dr["A371_dsc_grupo"]);
                    if (dr["A371_ind_master"] != DBNull.Value && dr["A371_ind_master"] != DBNull.Value && dr["A371_ind_master"].ToString() != "")
                    moduloGrupo.idMaster = Convert.ToInt32(dr["A371_ind_master"]);
                if ( dr["A052_cd_usuario"] != DBNull.Value && dr["A052_cd_usuario"].ToString() != "")
                    moduloGrupo.CodUsuario = Convert.ToInt32(dr["A052_cd_usuario"]);
                if ( dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                    moduloGrupo.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                moduloGrupo = new ModuloGrupo();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return moduloGrupo;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codGrupo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>ModuloGrupo</returns>
        public static ModuloGrupo GetDataRow(int codGrupo, int codModulo, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codGrupo, codModulo, trans);
            ModuloGrupo moduloGrupo = new ModuloGrupo();
            try
            {
                if (dr.Read())
                {
                    moduloGrupo.codGrupo = Convert.ToInt32(dr["A371_cd_grupo"]);
                    moduloGrupo.codModulo = Convert.ToInt32(dr["A270_cd_modulo"]);
                    moduloGrupo.dscGrupo = Convert.ToString(dr["A371_dsc_grupo"]);
                    if (dr["A371_ind_master"] != DBNull.Value && dr["A371_ind_master"] != DBNull.Value && dr["A371_ind_master"].ToString() != "")
                        moduloGrupo.idMaster = Convert.ToInt32(dr["A371_ind_master"]);
                    if ( dr["A052_cd_usuario"] != DBNull.Value && dr["A052_cd_usuario"].ToString() != "")
                        moduloGrupo.CodUsuario = Convert.ToInt32(dr["A052_cd_usuario"]);
                    if ( dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        moduloGrupo.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                moduloGrupo = new ModuloGrupo();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return moduloGrupo;
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


            paginacao.DataReader = dr;
            //  traz o total de registros de retorno direto dos parametros do ExecuteReader---
            paginacao.TotalRegistros = Convert.ToInt32(parms[5].Value);
            return paginacao;
        }
        #endregion


        #endregion

    }
}
