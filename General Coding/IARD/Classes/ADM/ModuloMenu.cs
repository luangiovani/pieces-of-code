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
    public class ModuloMenu // T373_modulo_menu 
    {
        #region Atributos
        private Menu codMenu;
        private ModuloSistema codModulo;
        private string logUsuario;
        #endregion

        #region Propriedades
        public Menu CodGrupo
        {
            get { return codMenu; }
            set { codMenu = value; }
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
        public ModuloMenu()
            : this(-1)
        { }
        public ModuloMenu(int codMenu)
        {
            this.codMenu = new Menu();
            this.codModulo = new ModuloSistema();
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "ModuloMenu.ModuloMenuInc";
        private const string SPUPDATE = "ModuloMenu.ModuloMenuAlt";
        private const string SPDELETE = "ModuloMenu.ModuloMenuDel";
        private const string SPSELECTID = "ModuloMenu.ModuloMenuSelId";
        private const string SPSELECTPAG = "ModuloMenu.ModuloMenuSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMCODMENU = "codMenu";
        private const string PARMCODMODULO = "codModulo";
        private const string PARMCURSOR = "curModuloMenu";
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
                    /*0*/ new OracleParameter(PARMCODMENU, OracleType.Int32, 4, ParameterDirection.InputOutput.ToString()) ,
                    /*1*/ new OracleParameter(PARMCODMODULO, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*2*/ new OracleParameter( "logUsuario", OracleType.VarChar)
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
            parms[0].Value = this.codMenu;
            parms[1].Value = this.codModulo;
            parms[2].Value = this.logUsuario;
            
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
                        //codMenu = Convert.ToInt32(cmd.Parameters[PARMCODMENU].Value);
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
        public static void Delete(int codMenu, int codModulo)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMCODMENU, OracleType.Int32, 4)  ,
                new OracleParameter(PARMCODMODULO, OracleType.Int32, 4)
            };
            parms[0].Value = codMenu;
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
        /// <param name="codMenu">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(int codMenu, int codModulo, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMCODMENU, OracleType.Int32, 4)  ,
                new OracleParameter(PARMCODMODULO, OracleType.Int32, 4)
            };
                parms[0].Value = codMenu;
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
        /// <param name="codMenu">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codMenu, int codModulo)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCODMENU, OracleType.Int32, 4), 
                    new OracleParameter(PARMCODMODULO, OracleType.Int32, 4),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codMenu;
            param[1].Value = codModulo;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="codMenu">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codMenu, int codModulo, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCODMENU, OracleType.Int32, 4), 
                    new OracleParameter(PARMCODMODULO, OracleType.Int32, 4),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codMenu;
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
        /// <param name="codMenu">Código do Registro</param>
        /// <returns>ModuloMenu</returns>
        public static ModuloMenu GetDataRow(int codMenu, int codModulo)
        {
            OracleDataReader dr = LoadDataDr(codMenu, codModulo);
            ModuloMenu moduloMenu = new ModuloMenu();
            try
            {
                if (dr.Read())
                {
                    moduloMenu.codMenu = new Menu(Convert.ToString(dr["A377_cd_menu"]));
                    moduloMenu.codModulo = new ModuloSistema(Convert.ToInt32(dr["A270_cd_modulo"]));
                    if ( dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                    moduloMenu.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                moduloMenu = new ModuloMenu();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return moduloMenu;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codMenu">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>ModuloMenu</returns>
        public static ModuloMenu GetDataRow(int codMenu, int codModulo, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codMenu, codModulo, trans);
            ModuloMenu moduloMenu = new ModuloMenu();
            try
            {
                if (dr.Read())
                {
                    moduloMenu.codMenu = new Menu(Convert.ToString(dr["A377_cd_menu"]));
                    moduloMenu.codModulo = new ModuloSistema(Convert.ToInt32(dr["A270_cd_modulo"]));
                    if ( dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                    moduloMenu.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                moduloMenu = new ModuloMenu();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return moduloMenu;
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
