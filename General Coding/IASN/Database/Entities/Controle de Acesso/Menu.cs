using System;
using System.Data.OracleClient;
using System.Data;

namespace Database.Entities
{
    /// <summary>
    /// Classe de Mapeamento com a Entidade de Banco de Dados - T377_Menu
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
    public class Menu
    {
        #region Atributos
        private string codMenu;
        private string dscMenu;
        private string nomeObjeto;
        private string nomPaginaWeb;
        private string logusuario;
        private string dscImagem;
        #endregion

        #region Propriedades
        public string CodMenu
        {
            get { return codMenu; }
            set { codMenu = value; }
        }
        public string DscMenu
        {
            get { return dscMenu; }
            set { dscMenu = value; }
        }
        public string NomPaginaWeb
        {
            get { return nomPaginaWeb; }
            set { nomPaginaWeb = value; }
        }
        public string NomeObjeto
        {
            get { return nomeObjeto; }
            set { nomeObjeto = value; }
        }
        public string Logusuario
        {
            get { return logusuario; }
            set { logusuario = value; }
        }
        public string DscImagem
        {
            get { return dscImagem; }
            set { dscImagem = value; }
        }
        #endregion

        #region Construtores
        public Menu()
            : this("-1")
        { }
        public Menu(string codMenu)
        {
            this.codMenu = codMenu;
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "Menu.MenuInc";
        private const string SPUPDATE = "Menu.MenuAlt";
        private const string SPDELETE = "Menu.MenuDel";
        private const string SPSELECTID = "Menu.MenuSelId";
        private const string SPSELECTPAG = "Menu.MenuSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMCODIGO = "codMenu";
        private const string PARMCURSOR = "curMenu";
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
                /*0*/ new OracleParameter(PARMCODIGO, OracleType.VarChar, 20, ParameterDirection.Input.ToString()) ,
                /*1*/ new OracleParameter( "dscMenu", OracleType.VarChar),
                /*2*/ new OracleParameter( "nomeObjeto", OracleType.VarChar),
                /*3*/ new OracleParameter( "nomPaginaWeb", OracleType.Int32),
                /*4*/ new OracleParameter( "logusuario", OracleType.VarChar),
                /*5*/ new OracleParameter( "dscImagem", OracleType.VarChar)
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
            parms[1].Value = this.dscMenu;
            parms[2].Value = this.nomeObjeto;
            parms[3].Value = this.nomPaginaWeb;
            parms[4].Value = this.logusuario;
            parms[5].Value = this.dscImagem;
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
                        codMenu = Convert.ToString(cmd.Parameters[PARMCODIGO].Value);
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
                //codMenu = Convert.ToString(cmd.Parameters[PARMCODIGO].Value);
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
        /// <param name="codigo">Código do MENU</param>
        public static void Delete(string codigo)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMCODIGO, OracleType.VarChar, 20) };
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
        /// <param name="codigo">Código do MENU</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(string codigo, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { 
                    new OracleParameter(PARMCODIGO, OracleType.VarChar, 20) };
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
        /// <param name="codigo">Código do MENU</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(string codigo)
        {
            OracleParameter[] param = new OracleParameter[] { 

                new OracleParameter(PARMCODIGO, OracleType.VarChar, 20), 
                new OracleParameter(PARMCURSOR, OracleType.Cursor)
            };

            param[0].Value = codigo;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="codigo">Código do MENU</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(string codigo, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 
                new OracleParameter(PARMCODIGO, OracleType.VarChar, 4),                                                            new OracleParameter(PARMCURSOR, OracleType.Cursor)
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
        /// <param name="codigo">Código do MENU</param>
        /// <returns>Menu</returns>
        public static Menu GetDataRow(string codigo)
        {
            OracleDataReader dr = LoadDataDr(codigo);
            Menu cargo = new Menu();
            try
            {
                if (dr.Read())
                {
                    cargo.codMenu = Convert.ToString(dr["A377_cd_Menu"]);
                    cargo.dscMenu = Convert.ToString(dr["A377_dsc_Menu"]);
                    cargo.nomeObjeto = Convert.ToString(dr["A377_nome_objeto"]);
                    if (dr["A377_nom_pagina_web"] != DBNull.Value && dr["A377_nom_pagina_web"] != DBNull.Value && dr["A377_nom_pagina_web"].ToString() != "")
                        cargo.nomPaginaWeb = Convert.ToString(dr["A377_nom_pagina_web"]);
                    if ( dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        cargo.logusuario = Convert.ToString(dr["usu_inc_alt"]);
                    cargo.dscImagem = dr["A377_dscimagem"].ToString();
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                cargo = new Menu();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return cargo;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codigo">Código do MENU</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>Menu</returns>
        public static Menu GetDataRow(string codigo, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codigo, trans);
            Menu cargo = new Menu();
            try
            {
                if (dr.Read())
                {
                    cargo.codMenu = Convert.ToString(dr["A377_cd_Menu"]);
                    cargo.dscMenu = Convert.ToString(dr["A377_dsc_Menu"]);
                    cargo.nomeObjeto = Convert.ToString(dr["A377_nome_objeto"]);
                    if (dr["A377_nom_pagina_web"] != DBNull.Value && dr["A377_nom_pagina_web"] != DBNull.Value && dr["A377_nom_pagina_web"].ToString() != "")
                        cargo.nomPaginaWeb = Convert.ToString(dr["A377_nom_pagina_web"]);
                    if ( dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        cargo.logusuario = Convert.ToString(dr["usu_inc_alt"]);
                    cargo.dscImagem = dr["A377_dscimagem"].ToString();
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                cargo = new Menu();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return cargo;
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
