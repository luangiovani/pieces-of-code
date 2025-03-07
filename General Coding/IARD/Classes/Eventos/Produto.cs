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
    public class Produto  // T041_PRODUTO  
    {
        #region Atributos
        private int codProduto;
        private string dscProduto;
        private int? codNmb;
        private string codNcm;
        private Usuario codUsuario;
        private string logUsuario;

        #endregion

        #region Propriedades
        public int CodProduto
        {
          get { return codProduto; }
          set { codProduto = value; }
        }
        public string DscProduto
        {
          get { return dscProduto; }
          set { dscProduto = value; }
        }
        public int? CodNmb
        {
          get { return codNmb; }
          set { codNmb = value; }
        }
        public Usuario CodUsuario
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
        public Produto()
            : this(-1)
        { }
        public Produto(int codProduto)
        {
            this.codProduto = codProduto;
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "Produto.ProdutoInc";
        private const string SPUPDATE = "Produto.ProdutoAlt";
        private const string SPDELETE = "Produto.ProdutoDel";
        private const string SPSELECTID = "Produto.ProdutoSelId";
        private const string SPSELECTPAG = "Produto.ProdutoSelPag";
        //tirando dados de DLL
        private const string SPInserirMovServTemp = "Produto.TInserirMovServTemp";
        private const string SPInserirMovProdTemp = "Produto.TInserirMovProdTemp";

        #endregion

        #region Parametros Oracle
        private const string PARMCODIGO = "codProduto";
        private const string PARMCURSOR = "curProduto";
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
                /*0*/ new OracleParameter(PARMCODIGO, OracleType.Int32, 2, ParameterDirection.InputOutput.ToString()) ,
                      new OracleParameter( "dscProduto", OracleType.VarChar),
                      new OracleParameter( "codNmb", OracleType.Int32),
                      new OracleParameter( "codNcm", OracleType.VarChar),
                      new OracleParameter( "codUsuario", OracleType.Int32),
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
            parms[0].Value = this.codProduto;
            parms[1].Value = this.dscProduto.ToUpper();
            parms[2].Value = DBNull.Value;
            parms[3].Value = "";
            if (this.codNmb != null){
                parms[2].Value = this.codNmb;}
            if (this.codNcm != null){
                parms[3].Value = this.codNcm;}
            parms[4].Value = this.codUsuario.CodUsuario;
			
            parms[5].Value = this.logUsuario.ToUpper();
            if (this.codProduto < 0)
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
        /// Insert com tratamento de transa��o
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
                        //Obtendo a chave de identifica��o do registro inserido.
                        codProduto = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
        /// Insert sem tratamento de transa��o
        /// </summary>
        /// <param name="trans">OracleTransaction</param>
        public void Insert(OracleTransaction trans)
        {
            OracleParameter[] parms = GetParameters();
            SetParameters(parms);
            try
            {
                OracleCommand cmd = DataBase.ExecuteNonQueryCmd(trans, CommandType.StoredProcedure, SPINSERT, parms);
                //Obtendo a chave de identifica��o do registro inserido.
                codProduto = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
        /// Update com tratamento de transa��o
        /// </summary>
        public void Update()
        {
            // --------------------------------------------------------  
            // Obtendo e setando os par�metros 
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
        /// Update sem tratamento de transa��o
        /// </summary>
        /// <param name="trans">OracleTransaction</param>
        public void Update(OracleTransaction trans)
        {
            // -------------------------------------------------------- 
            // Obtendo e setando os par�metros 
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
        /// Delete com tratamento de transa��o
        /// </summary>
        /// <param name="codigo">C�digo do Registro</param>
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
        /// Delete sem tratamento de transa��o
        /// </summary>
        /// <param name="codigo">C�digo do Registro</param>
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
        /// <param name="codigo">C�digo do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Ap�s a utiliza��o do LoadDataDr n�o esquecer de fechar a conex�o: dr.Close();</remarks>
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
        /// LoadDataDr para ser utilizando dentro de alguma transa��o
        /// </summary>
        /// <param name="codigo">C�digo do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Ap�s a utiliza��o do LoadDataDr n�o esquecer de fechar a conex�o: dr.Close();</remarks>
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

        public static OracleDataReader LoadDataInserirMovServTemp(string strDepartamento, int lngCdMov, int lngCdCli, int lngCdProduto, string strDescricao, decimal dblQtde, decimal dblVlDesconto)
        {
            OracleParameter[] param = new OracleParameter[] { 
                    new OracleParameter("strDepartamento", OracleType.VarChar,100),
                    new OracleParameter("lngCdMov", OracleType.Int32),
                    new OracleParameter("lngCdCli", OracleType.Int32),
                    new OracleParameter("lngCdProduto", OracleType.Int32),
                    new OracleParameter("strDescricao", OracleType.VarChar,100),
                    new OracleParameter("dblQtde", OracleType.Float),
                    new OracleParameter("dblVlDesconto", OracleType.Float),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = strDepartamento;
            param[1].Value = lngCdMov;
            param[2].Value = lngCdCli;
            param[3].Value = lngCdProduto;
            param[4].Value = strDescricao;
            param[5].Value = dblQtde;
            param[6].Value = dblVlDesconto;
            param[7].Direction = ParameterDirection.Output;
            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPInserirMovServTemp, param);
            return dr;
        }

        public static OracleDataReader LoadDataInserirMovProdTemp(string strDepartamento, int lngCdMov, int lngCdCli, int lngCdProduto, string strDescricao, decimal dblQtde, decimal dblVlDesconto)
        {
            OracleParameter[] param = new OracleParameter[] { 
                    new OracleParameter("strDepartamento", OracleType.VarChar,100),
                    new OracleParameter("lngCdMov", OracleType.Int32),
                    new OracleParameter("lngCdCli", OracleType.Int32),
                    new OracleParameter("lngCdProduto", OracleType.Int32),
                    new OracleParameter("strDescricao", OracleType.VarChar,100),
                    new OracleParameter("dblQtde", OracleType.Float),
                    new OracleParameter("dblVlDesconto", OracleType.Float),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = strDepartamento;
            param[1].Value = lngCdMov;
            param[2].Value = lngCdCli;
            param[3].Value = lngCdProduto;
            param[4].Value = strDescricao;
            param[5].Value = dblQtde;
            param[6].Value = dblVlDesconto;
            param[7].Direction = ParameterDirection.Output;
            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPInserirMovProdTemp, param);
            return dr;
        }
        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="codigo">C�digo do Registro</param>
        /// <returns>Produto</returns>
        public static Produto GetDataRow(int codigo)
        {
            OracleDataReader dr = LoadDataDr(codigo);
            Produto Produto = new Produto();
            try
            {
                if (dr.Read())
                {
                    Produto.codProduto = Convert.ToInt32(dr["A041_cd_prod"]);
                    Produto.dscProduto = Convert.ToString(dr["A041_dsc_prod"]);
                    if (dr["A041_cd_nbm"] != DBNull.Value && dr["A041_cd_nbm"] != DBNull.Value && dr["A041_cd_nbm"].ToString() != "")
                        Produto.codNmb = Convert.ToInt32(dr["A041_cd_nbm"]);
                    if (dr["A041_cd_ncm"] != DBNull.Value && dr["A041_cd_ncm"] != DBNull.Value && dr["A041_cd_ncm"].ToString() != "")
                        Produto.codNcm = Convert.ToString(dr["A041_cd_ncm"]);
                    Produto.codUsuario = new Usuario(Convert.ToInt32(dr["A052_cd_usuario"]));
					
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        Produto.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                Produto = new Produto();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return Produto;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transa��o
        /// </summary>
        /// <param name="codigo">C�digo do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>Produto</returns>
        public static Produto GetDataRow(int codigo, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codigo, trans);
            Produto Produto = new Produto();
            try
            {
                if (dr.Read())
                {
                    Produto.codProduto = Convert.ToInt32(dr["A041_cd_prod"]);
                    Produto.dscProduto = Convert.ToString(dr["A041_dsc_prod"]);
                    if (dr["A041_cd_nbm"] != DBNull.Value && dr["A041_cd_nbm"] != DBNull.Value && dr["A041_cd_nbm"].ToString() != "")
                        Produto.codNmb = Convert.ToInt32(dr["A041_cd_nbm"]);
                    if (dr["A041_cd_ncm"] != DBNull.Value && dr["A041_cd_ncm"] != DBNull.Value && dr["A041_cd_ncm"].ToString() != "")
                        Produto.codNcm = Convert.ToString(dr["A041_cd_ncm"]);
                    Produto.codUsuario = new Usuario(Convert.ToInt32(dr["A052_cd_usuario"]));

                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        Produto.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                Produto = new Produto();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return Produto;
        }
        #endregion

        #region LoadDataPaginacao

        /// <summary>
        /// LoadDataPaginacao
        /// </summary>
        /// <param name="Where">Cl�usula where utilizada na consulta</param>
        /// <param name="PaginaCorrente">N�mero da p�gina que deseja selecionar</param>
        /// <param name="TamanhoPagina">Quantidade de registros em cada p�gina</param>
        /// <param name="ExpressaoOrdenacao">Express�o de ordena��o</param>
        /// <returns>Inst�ncia do objeto Pagina��o, contendo um DataReader e o total de registros</returns>
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