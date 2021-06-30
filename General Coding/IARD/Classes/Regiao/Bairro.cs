using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
 
using System.Data;
 
//-- Classe Classes Sebrae
//-- Data : 15/05/2007 
//-- Autor :  Daniel

namespace Classes
{
    public class Bairro // T616_Bairro   
    {
        #region Atributos
        private int codBairro;
        private Cidade codCidade;
        private Estado codEstado;
        private Pais codPais;
        private int? codSetorCidade;
        private string nomBairro;       
        private string logUsuario;
        #endregion

        #region Propriedades
        public int CodBairro
        {
            get { return codBairro; }
            set { codBairro = value; }
        }
        public Cidade CodCidade
        {
            get { return codCidade; }
            set { codCidade = value; }
        }
        public Estado CodEstado
        {
            get { return codEstado; }
            set { codEstado = value; }
        }
        public Pais CodPais
        {
            get { return codPais; }
            set { codPais = value; }
        }
        public int? CodSetorCidade
        {
            get { return codSetorCidade; }
            set { codSetorCidade = value; }
        }
        public string NomBairro
        {
            get { return nomBairro; }
            set { nomBairro = value; }
        }
        public string LogBairro
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }
        #endregion

        #region Construtores
        public Bairro()
            : this(-1)
        { }
        public Bairro(int codBairro)
        {
            this.codBairro = codBairro;
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "Bairro.BairroInc";
        private const string SPUPDATE = "Bairro.BairroAlt";
        private const string SPDELETE = "Bairro.BairroDel";
        private const string SPSELECTID = "Bairro.BairroSelId";
        private const string SPSELECTPAG = "Bairro.BairroSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMCODBAIRRO = "codBairro";
        private const string PARMCODCIDADE = "codCidade";
        private const string PARMCODESTADO= "codEstado";
        private const string PARMCODPAIS = "codPais";
        private const string PARMCURSOR = "curBairro";
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
                    /*0*/ new OracleParameter(PARMCODBAIRRO, OracleType.Int32, 4, ParameterDirection.InputOutput.ToString()) ,
                    /*1*/ new OracleParameter(PARMCODCIDADE, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*2*/ new OracleParameter(PARMCODESTADO, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*3*/ new OracleParameter(PARMCODPAIS, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*4*/ new OracleParameter( "codSetorCidade", OracleType.Int32),
                    /*5*/ new OracleParameter( "nomBairro", OracleType.VarChar),
                    /*6*/ new OracleParameter( "logUsuario", OracleType.VarChar)
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
            parms[0].Value = this.codBairro;
            parms[1].Value = this.codCidade;
            parms[2].Value = this.codEstado;
            parms[3].Value = this.codPais;
            parms[4].Value = this.codSetorCidade;
            parms[5].Value = this.nomBairro;
            parms[6].Value = this.logUsuario;
            if (this.codBairro < 0)
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
                        codBairro = Convert.ToInt32(cmd.Parameters[PARMCODBAIRRO].Value);
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
                codBairro = Convert.ToInt32(cmd.Parameters[PARMCODBAIRRO].Value);
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
        /// <param name="codBairro">C�digo do Registro</param>
        public static void Delete(int codBairro, int codCidade, int codEstado, int codPais)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMCODBAIRRO, OracleType.Int32, 4),
                new OracleParameter(PARMCODCIDADE, OracleType.Int32, 4),
                new OracleParameter(PARMCODESTADO, OracleType.Int32, 4),
                new OracleParameter(PARMCODPAIS, OracleType.Int32, 4)
            };
            parms[0].Value = codBairro;
            parms[1].Value = codCidade;
            parms[2].Value = codEstado;
            parms[3].Value = codPais;
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
        /// <param name="codBairro">C�digo do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(int codBairro, int codCidade, int codEstado, int codPais, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMCODBAIRRO, OracleType.Int32, 4),
                new OracleParameter(PARMCODCIDADE, OracleType.Int32, 4),
                new OracleParameter(PARMCODESTADO, OracleType.Int32, 4),
                new OracleParameter(PARMCODPAIS, OracleType.Int32, 4),
            };
                parms[0].Value = codBairro;
                parms[1].Value = codCidade;
                parms[2].Value = codEstado;
                parms[3].Value = codPais;
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
        /// <param name="codBairro">C�digo do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Ap�s a utiliza��o do LoadDataDr n�o esquecer de fechar a conex�o: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codBairro, int codCidade, int codEstado, int codPais)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCODBAIRRO, OracleType.Int32, 4), 
                    new OracleParameter(PARMCODCIDADE, OracleType.Int32, 4), 
                    new OracleParameter(PARMCODESTADO, OracleType.Int32, 4), 
                    new OracleParameter(PARMCODPAIS, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codBairro;
            param[1].Value = codCidade;
            param[2].Value = codEstado;
            param[3].Value = codPais;
            param[4].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transa��o
        /// </summary>
        /// <param name="codBairro">C�digo do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Ap�s a utiliza��o do LoadDataDr n�o esquecer de fechar a conex�o: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codBairro, int codCidade, int codEstado, int codPais, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCODBAIRRO, OracleType.Int32, 4), 
                    new OracleParameter(PARMCODCIDADE, OracleType.Int32, 4), 
                    new OracleParameter(PARMCODESTADO, OracleType.Int32, 4), 
                    new OracleParameter(PARMCODPAIS, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codBairro;
            param[1].Value = codCidade;
            param[2].Value = codEstado;
            param[3].Value = codPais;
            param[4].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="codBairro">C�digo do Registro</param>
        /// <returns>Bairro</returns>
        public static Bairro GetDataRow(int codBairro, int codCidade, int codEstado, int codPais)
        {
            OracleDataReader dr = LoadDataDr(codBairro, codCidade, codEstado, codPais);
            Bairro bairro = new Bairro();
            try
            {
                if (dr.Read())
                {
                    bairro.codBairro = Convert.ToInt32(dr["A616_cd_bairro"]);
                    bairro.codCidade = new Cidade(Convert.ToInt32(dr["A011_cd_cid"]));
                    bairro.codEstado = new Estado(Convert.ToInt32(dr["A021_cd_est"]));
                    bairro.codPais = new Pais(Convert.ToInt32(dr["A035_cd_pais"]));
                    if (dr["A634_cd_setor"] != DBNull.Value && dr["A634_cd_setor"] != DBNull.Value && dr["A634_cd_setor"].ToString() != "")
                        bairro.codSetorCidade = Convert.ToInt32(dr["A634_cd_setor"]);
                    bairro.nomBairro = Convert.ToString(dr["A616_nm_bairro"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        bairro.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                bairro = new Bairro();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return bairro;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transa��o
        /// </summary>
        /// <param name="codBairro">C�digo do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>Bairro</returns>
        public static Bairro GetDataRow(int codBairro, int codCidade, int codEstado, int codPais, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codBairro, codCidade, codEstado, codPais, trans);
            Bairro bairro = new Bairro();
            try
            {
                if (dr.Read())
                {
                    bairro.codBairro = Convert.ToInt32(dr["A616_cd_bairro"]);
                    bairro.codCidade = new Cidade(Convert.ToInt32(dr["A011_cd_cid"]));
                    bairro.codEstado = new Estado(Convert.ToInt32(dr["A021_cd_est"]));
                    bairro.codPais = new Pais(Convert.ToInt32(dr["A035_cd_pais"]));
                    if (dr["A634_cd_setor"] != DBNull.Value && dr["A634_cd_setor"] != DBNull.Value && dr["A634_cd_setor"].ToString() != "")
                        bairro.codSetorCidade = Convert.ToInt32(dr["A634_cd_setor"]);
                    bairro.nomBairro = Convert.ToString(dr["A616_nm_bairro"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        bairro.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                bairro = new Bairro();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return bairro;
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


            paginacao.DataReader = dr;
            //  traz o total de registros de retorno direto dos parametros do ExecuteReader---
            paginacao.TotalRegistros = Convert.ToInt32(parms[5].Value);
            return paginacao;
        }
        #endregion


        #endregion

    }
}
