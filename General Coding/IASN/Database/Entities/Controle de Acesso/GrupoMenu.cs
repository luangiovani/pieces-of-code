using System;
using System.Data.OracleClient;
using System.Data;

namespace Database.Entities
{
    /// <summary>
    /// Classe de Mapeamento com a Entidade de Banco de Dados - T616_GrupoMenu
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
    public class GrupoMenu
    {
        #region Atributos
        private Menu codMenu;
        private ModuloGrupo codGrupo;
        private ModuloSistema codModulo;
        private string logUsuario;
        private int indManutencao;
        #endregion

        #region Propriedades
        public Menu CodMenu
        {
            get { return codMenu; }
            set { codMenu = value; }
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
        public int IndManutencao
        {
            get { return indManutencao; }
            set { indManutencao = value; }
        }
        #endregion

        #region Construtores
        public GrupoMenu()
            : this("-1")
        { }
        public GrupoMenu(string codMenu)
        {
            this.codMenu = new Menu();
            this.codGrupo = new ModuloGrupo();
            this.codModulo = new ModuloSistema();
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "GrupoMenu.GrupoMenuInc";
        private const string SPUPDATE = "GrupoMenu.GrupoMenuAlt";
        private const string SPDELETE = "GrupoMenu.GrupoMenuDel";
        private const string SPSELECTID = "GrupoMenu.GrupoMenuSelId";
        private const string SPSELECTUSUARIOMODULO = "GrupoMenu.GrupoMenuSelUsuarioModulo";
        private const string SPSELECTUSUARIOMODULOMENU = "GrupoMenu.GrupoMenuSelUsuarioModuloMenu";
        private const string SPSELECTMODULOMENU = "GrupoMenu.GrupoMenuSelModuloMenu";
        private const string SPSELECTPAG = "GrupoMenu.GrupoMenuSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMCODMENU = "codMenu";
        private const string PARMCODGRUPO = "codGrupo";
        private const string PARMCODMODULO = "codModulo";
        private const string PARMCODUSUARIO = "codUsuario";
        private const string PARMCURSOR = "curGrupoMenu";
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
                    /*0*/ new OracleParameter(PARMCODMENU, OracleType.VarChar, 20, ParameterDirection.Input.ToString()) ,
                    /*1*/ new OracleParameter(PARMCODGRUPO, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*2*/ new OracleParameter(PARMCODMODULO, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,                    
                    /*3*/ new OracleParameter( "logUsuario", OracleType.VarChar),
                    /*4*/ new OracleParameter( "indManutencao", OracleType.Int32)
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
            parms[0].Value = this.codMenu;
            parms[1].Value = this.codGrupo;
            parms[2].Value = this.codModulo;
            parms[3].Value = this.logUsuario;
            parms[4].Value = this.indManutencao;

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
                        //codMenu = new Usuario(Convert.ToInt32(cmd.Parameters[PARMCODMENU].Value));
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
                //codMenu = Convert.ToInt32(cmd.Parameters[PARMCODMENU].Value);
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
        /// <param name="codMenu">Código do Registro</param>
        public static void Delete(string codMenu, int codGrupo, int codModulo)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMCODMENU, OracleType.VarChar)  ,
                new OracleParameter(PARMCODGRUPO, OracleType.Int32, 4)  ,
                new OracleParameter(PARMCODMODULO, OracleType.Int32, 4)
            };
            parms[0].Value = codMenu;
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
        /// <param name="codMenu">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(string codMenu, int codGrupo, int codModulo, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMCODMENU, OracleType.VarChar)  ,
                new OracleParameter(PARMCODGRUPO, OracleType.Int32, 4)  ,
                new OracleParameter(PARMCODMODULO, OracleType.Int32, 4) 
            };
                parms[0].Value = codMenu;
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
        /// <param name="codMenu">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(string codMenu, int codGrupo, int codModulo)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCODMENU, OracleType.VarChar), 
                    new OracleParameter(PARMCODGRUPO, OracleType.Int32, 4), 
                    new OracleParameter(PARMCODMODULO, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codMenu;
            param[1].Value = codGrupo;
            param[2].Value = codModulo;
            param[3].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="codMenu">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(string codMenu, int codGrupo, int codModulo, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCODMENU, OracleType.VarChar), 
                    new OracleParameter(PARMCODGRUPO, OracleType.Int32, 4), 
                    new OracleParameter(PARMCODMODULO, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codMenu;
            param[1].Value = codGrupo;
            param[2].Value = codModulo;
            param[3].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region LoadDataDrUsuarioModulo
        /// <summary>
        /// LoadDataDrUsuarioModulo
        /// Responsavel por retornar todos os menus do grupo e modulos escolhidos.
        /// </summary>
        /// <param name="codModulo"> codMenu do modulo</param>
        /// <param name="codUsuario"> codMenu do usuario</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDrUsuarioModulo(int codUsuario, int codModulo)
        {
            OracleParameter[] param = new OracleParameter[] { 

                new OracleParameter(PARMCODUSUARIO, OracleType.Int32, 4), 
                new OracleParameter(PARMCODMODULO, OracleType.Int32, 4), 
                new OracleParameter(PARMCURSOR, OracleType.Cursor)
            };

            param[0].Value = codUsuario;
            param[1].Value = codModulo;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTUSUARIOMODULO, param);
            return dr;
        }
        /// <summary>
        /// LoadDataDrUsuarioModulo
        /// Responsavel por retornar todos os menus do grupo e modulos escolhidos.
        /// </summary>
        /// <param name="codModulo"> codMenu do modulo</param>
        /// <param name="codUsuario"> codMenu do usuario</param>
        /// <param name="trans"></param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDrUsuarioModulo(int codUsuario, int codModulo, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                new OracleParameter(PARMCODUSUARIO, OracleType.Int32, 4), 
                new OracleParameter(PARMCODMODULO, OracleType.Int32, 4), 
                new OracleParameter(PARMCURSOR, OracleType.Cursor)
            };

            param[0].Value = codUsuario;
            param[1].Value = codModulo;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTUSUARIOMODULO, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDrUsuarioModulo
        /// Responsavel por retornar todos os menus do grupo e modulos escolhidos.
        /// </summary>
        /// <param name="codModulo"> codMenu do modulo</param>
        /// <param name="codUsuario"> codMenu do usuario</param>
        /// <param name="codMenu"> codMenu do menu, não null</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDrUsuarioModulo(int codUsuario, int codModulo, string codMenu)
        {
            OracleParameter[] param = new OracleParameter[] { 

                new OracleParameter(PARMCODUSUARIO, OracleType.Int32, 4), 
                new OracleParameter(PARMCODMODULO, OracleType.Int32, 4), 
                new OracleParameter(PARMCODMENU, OracleType.VarChar), 
                new OracleParameter(PARMCURSOR, OracleType.Cursor)
            };

            param[0].Value = codUsuario;
            param[1].Value = codModulo;
            param[2].Value = codMenu;
            param[3].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTUSUARIOMODULOMENU, param);
            return dr;
        }
        /// <summary>
        /// LoadDataDrUsuarioModulo
        /// Responsavel por retornar todos os menus do grupo e modulos escolhidos.
        /// </summary>
        /// <param name="codModulo"> codMenu do modulo</param>
        /// <param name="codUsuario"> codMenu do usuario</param>
        /// <param name="codMenu"> codMenu do menu, não null</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDrUsuarioModulo(int codUsuario, int codModulo, string codMenu, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                new OracleParameter(PARMCODUSUARIO, OracleType.Int32, 4), 
                new OracleParameter(PARMCODMODULO, OracleType.Int32, 4), 
                new OracleParameter(PARMCODMENU, OracleType.VarChar), 
                new OracleParameter(PARMCURSOR, OracleType.Cursor)
            };

            param[0].Value = codUsuario;
            param[1].Value = codModulo;
            param[2].Value = codMenu;
            param[3].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTUSUARIOMODULOMENU, param);
            return dr;
        }
        #endregion

        #region LoadDataDrModulomenu

        /// <summary>
        /// LoadDataDrUsuarioModulo
        /// Responsavel por retornar todos os menus do grupo e modulos escolhidos.
        /// </summary>
        /// <param name="codModulo"> codMenu do modulo</param>
        /// <param name="codUsuario"> codMenu do usuario</param>
        /// <param name="codMenu"> codMenu do menu, não null</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDrModuloMenu(int codModulo, string codMenu)
        {
            OracleParameter[] param = new OracleParameter[] { 

                new OracleParameter(PARMCODMODULO, OracleType.Int32, 4), 
                new OracleParameter(PARMCODMENU, OracleType.VarChar), 
                new OracleParameter(PARMCURSOR, OracleType.Cursor)
            };

            param[0].Value = codModulo;
            param[1].Value = codMenu;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTMODULOMENU, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDrUsuarioModulo
        /// Responsavel por retornar todos os menus do grupo e modulos escolhidos.
        /// </summary>
        /// <param name="codModulo"> codMenu do modulo</param>
        /// <param name="codUsuario"> codMenu do usuario</param>
        /// <param name="codMenu"> codMenu do menu, não null</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDrModuloMenu(int codModulo, string codMenu, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                new OracleParameter(PARMCODMODULO, OracleType.Int32, 4), 
                new OracleParameter(PARMCODMENU, OracleType.VarChar), 
                new OracleParameter(PARMCURSOR, OracleType.Cursor)
            };

            param[0].Value = codModulo;
            param[1].Value = codMenu;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTMODULOMENU, param);
            return dr;
        }
        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="codMenu">Código do Registro</param>
        /// <returns>GrupoMenu</returns>
        public static GrupoMenu GetDataRow(string codMenu, int codGrupo, int codModulo)
        {
            OracleDataReader dr = LoadDataDr(codMenu, codGrupo, codModulo);
            GrupoMenu usuarioGrupo = new GrupoMenu();
            try
            {
                if (dr.Read())
                {
                    usuarioGrupo.codMenu = new Menu(Convert.ToString(dr["A377_cd_menu"]));
                    usuarioGrupo.codGrupo = new ModuloGrupo(Convert.ToInt32(dr["A371_cd_grupo"]));
                    usuarioGrupo.codModulo = new ModuloSistema(Convert.ToInt32(dr["A270_cd_modulo"]));
                    if ( dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                    usuarioGrupo.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                if (dr["A374_ind_manut"] != DBNull.Value && dr["A374_ind_manut"] != DBNull.Value && dr["A374_ind_manut"].ToString() != "") 
                    usuarioGrupo.indManutencao = Convert.ToInt32(dr["A374_ind_manut"]);               
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                usuarioGrupo = new GrupoMenu();
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
        /// <param name="codMenu">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>GrupoMenu</returns>
        public static GrupoMenu GetDataRow(string codMenu, int codGrupo, int codModulo, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codMenu, codGrupo, codModulo, trans);
            GrupoMenu usuarioGrupo = new GrupoMenu();
            try
            {
                if (dr.Read())
                {
                    usuarioGrupo.codMenu = new Menu(Convert.ToString(dr["A377_cd_menu"]));
                    usuarioGrupo.codGrupo = new ModuloGrupo(Convert.ToInt32(dr["A371_cd_grupo"]));
                    usuarioGrupo.codModulo = new ModuloSistema(Convert.ToInt32(dr["A270_cd_modulo"]));
                    if ( dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        usuarioGrupo.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                    if (dr["A374_ind_manut"] != DBNull.Value && dr["A374_ind_manut"] != DBNull.Value && dr["A374_ind_manut"].ToString() != "")
                        usuarioGrupo.indManutencao = Convert.ToInt32(dr["A374_ind_manut"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                usuarioGrupo = new GrupoMenu();
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
