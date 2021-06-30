using System;
using System.Data.OracleClient;
using System.Data;

namespace Database.Entities
{
    /// <summary>
    /// Classe de Mapeamento com a Entidade de Banco de Dados - T616_UsuarioGrupo
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
    public class UsuarioGrupo
    {
        #region Atributos
        private Usuario codUsuario;
        private ModuloGrupo codGrupo;
        private ModuloSistema codModulo;
        private string logUsuario;
        #endregion

        #region Propriedades
        public Usuario CodUsuario
        {
            get { return codUsuario; }
            set { codUsuario = value; }
        }
        public ModuloGrupo CodGrupo
        {
            get { return codGrupo; }
            set { codGrupo = value; }
        }
        public ModuloSistema CodModulo
        {
            get { return codModulo; }
            set { codModulo = value; }
        }
        public string LogUsuario
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }
        #endregion

        #region Construtores
        public UsuarioGrupo()
            : this(-1)
        { }
        public UsuarioGrupo(int codUsuario)
        {
            this.codUsuario = new Usuario();
            this.codGrupo = new ModuloGrupo();
            this.codModulo = new ModuloSistema();
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "UsuarioGrupo.UsuarioGrupoInc";
        private const string SPUPDATE = "UsuarioGrupo.UsuarioGrupoAlt";
        private const string SPDELETE = "UsuarioGrupo.UsuarioGrupoDel";
        private const string SPSELECTID = "UsuarioGrupo.UsuarioGrupoSelId";
        private const string SPSELECTPAG = "UsuarioGrupo.UsuarioGrupoSelPag";
        private const string SPSELECTUSUARIO = "UsuarioGrupo.UsuarioGrupoSelUsuario";
        #endregion

        #region Parametros Oracle
        private const string PARMCODUSUARIO= "codUsuario";
        private const string PARMCODGRUPO = "codGrupo";
        private const string PARMCODMODULO = "codModulo";
        private const string PARMCURSOR = "curUsuarioGrupo";
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
                    /*0*/ new OracleParameter(PARMCODUSUARIO, OracleType.Int32, 4, ParameterDirection.InputOutput.ToString()) ,
                    /*1*/ new OracleParameter(PARMCODGRUPO, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*2*/ new OracleParameter(PARMCODMODULO, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,                    
                    /*3*/ new OracleParameter( "logUsuario", OracleType.VarChar)
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
            parms[0].Value = this.codUsuario;
            parms[1].Value = this.codGrupo;
            parms[2].Value = this.codModulo;
            parms[3].Value = this.logUsuario;
            
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

            using (OracleConnection conn = new OracleConnection(Context.DataBase.CONN_STRING))
            {
                conn.Open();
                using (OracleTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        OracleCommand cmd = Context.DataBase.ExecuteNonQueryCmd(trans, CommandType.StoredProcedure, SPINSERT, parms);
                        //Obtendo a chave de identificação do registro inserido.
                        //codUsuario = new Usuario(Convert.ToInt32(cmd.Parameters[PARMCODUSUARIO].Value));
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
                //codUsuario = Convert.ToInt32(cmd.Parameters[PARMCODUSUARIO].Value);
                cmd.Parameters.Clear();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion
                
        #region Delete

        /// <summary>
        /// Delete com tratamento de transação
        /// </summary>
        /// <param name="codUsuario">Código do Registro</param>
        public static void Delete(int codUsuario, int codGrupo, int codModulo)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMCODUSUARIO, OracleType.Int32, 4)  ,
                new OracleParameter(PARMCODGRUPO, OracleType.Int32, 4)  ,
                new OracleParameter(PARMCODMODULO, OracleType.Int32, 4)
            };
            parms[0].Value = codUsuario;
            parms[1].Value = codGrupo;
            parms[2].Value = codModulo;
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
        /// <param name="codUsuario">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(int codUsuario, int codGrupo, int codModulo, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMCODUSUARIO, OracleType.Int32, 4)  ,
                new OracleParameter(PARMCODGRUPO, OracleType.Int32, 4)  ,
                new OracleParameter(PARMCODMODULO, OracleType.Int32, 4) 
            };
                parms[0].Value = codUsuario;
                parms[1].Value = codGrupo;
                parms[2].Value = codModulo;
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
        /// <param name="codUsuario">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codUsuario, int codGrupo, int codModulo)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCODUSUARIO, OracleType.Int32, 4), 
                    new OracleParameter(PARMCODGRUPO, OracleType.Int32, 4), 
                    new OracleParameter(PARMCODMODULO, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codUsuario;
            param[1].Value = codGrupo;
            param[2].Value = codModulo;
            param[3].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="codUsuario">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codUsuario, int codGrupo, int codModulo, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCODUSUARIO, OracleType.Int32, 4), 
                    new OracleParameter(PARMCODGRUPO, OracleType.Int32, 4), 
                    new OracleParameter(PARMCODMODULO, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codUsuario;
            param[1].Value = codGrupo;
            param[2].Value = codModulo;
            param[3].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region LoadDataDrUsuario
        /// <summary>
        /// LoadDataDr
        /// </summary>
        /// <param name="codUsuario">Código do USUARIO</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDrUsuario(int codUsuario)
        {
            OracleParameter[] param = new OracleParameter[] { 

                new OracleParameter(PARMCODUSUARIO, OracleType.Int32, 4), 
                new OracleParameter(PARMCURSOR, OracleType.Cursor)
            };

            param[0].Value = codUsuario;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTUSUARIO, param);
            return dr;
        }
        /// <summary>
        /// LoadDataDr
        /// </summary>
        /// <param name="codUsuario">Código do USUARIO</param>
        /// <param name="trans"></param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        /// 
        public static OracleDataReader LoadDataDrUsuario(int codUsuario, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                new OracleParameter(PARMCODUSUARIO, OracleType.Int32, 4), 
                new OracleParameter(PARMCURSOR, OracleType.Cursor)
            };

            param[0].Value = codUsuario;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTUSUARIO, param);
            return dr;
        }
        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="codUsuario">Código do Registro</param>
        /// <returns>UsuarioGrupo</returns>
        public static UsuarioGrupo GetDataRow(int codUsuario, int codGrupo, int codModulo)
        {
            OracleDataReader dr = LoadDataDr(codUsuario, codGrupo, codModulo);
            UsuarioGrupo usuarioGrupo = new UsuarioGrupo();
            try
            {
                if (dr.Read())
                {
                    usuarioGrupo.codUsuario = new Usuario(Convert.ToInt32(dr["A052_cd_usuario"]));
                    usuarioGrupo.codGrupo = new ModuloGrupo(Convert.ToInt32(dr["A371_cd_grupo"]));
                    usuarioGrupo.codModulo = new ModuloSistema(Convert.ToInt32(dr["A270_cd_modulo"]));
                    if ( dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                    usuarioGrupo.logUsuario = Convert.ToString(dr["T375_usuario_grupo"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                usuarioGrupo = new UsuarioGrupo();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return usuarioGrupo;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codUsuario">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>UsuarioGrupo</returns>
        public static UsuarioGrupo GetDataRow(int codUsuario, int codGrupo, int codModulo, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codUsuario, codGrupo, codModulo, trans);
            UsuarioGrupo usuarioGrupo = new UsuarioGrupo();
            try
            {
                if (dr.Read())
                {

                    usuarioGrupo.codUsuario = new Usuario(Convert.ToInt32(dr["A052_cd_usuario"]));
                    usuarioGrupo.codGrupo = new ModuloGrupo(Convert.ToInt32(dr["A371_cd_grupo"]));
                    usuarioGrupo.codModulo = new ModuloSistema(Convert.ToInt32(dr["A270_cd_modulo"]));
                    if (dr["T375_usuario_grupo"] != DBNull.Value && dr["T375_usuario_grupo"] != DBNull.Value && dr["T375_usuario_grupo"].ToString() != "")
                    usuarioGrupo.logUsuario = Convert.ToString(dr["T375_usuario_grupo"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                usuarioGrupo = new UsuarioGrupo();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return usuarioGrupo;
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
