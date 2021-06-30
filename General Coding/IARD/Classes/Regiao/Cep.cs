using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;

using System.Data;

//-- Classe Classes Sebrae
//-- Data : 27/07/2007 
//-- Autor :  Honorato

namespace Classes
{
    public class Cep // T046_CEP
    {
        #region Atributos
        private int codCep;
        private Cidade codCidade;
        private Estado codEstado;
        private Pais codPais;
        private Rua codRua;
        private Bairro codBairroIni;
        private Bairro codBairroFim;
        private string dscCep;
        private string logUsuario;
        #endregion

        #region Propriedades
        public string DscCep
        {
            get { return dscCep; }
            set { dscCep = value; }
        }
        public Bairro CodBairroFim
        {
            get { return codBairroFim; }
            set { codBairroFim = value; }
        }
        public Bairro CodBairroIni
        {
            get { return codBairroIni; }
            set { codBairroIni = value; }
        }
        public Rua CodRua
        {
            get { return codRua; }
            set { codRua = value; }
        }
        public int CodCep
        {
            get { return codCep; }
            set { codCep = value; }
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
        public string LogUsuario
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }
        #endregion

        #region Construtores
        public Cep()
            : this(-1)
        { }
        public Cep(int codCep)
        {
            this.codCep = codCep;
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "Cep.CepInc";
        private const string SPUPDATE = "Cep.CepAlt";
        private const string SPDELETE = "Cep.CepDel";
        private const string SPSELECTID = "Cep.CepSelId";
        private const string SPSELECTPAG = "Cep.CepSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMCODPAIS = "codPais";
        private const string PARMCODESTADO = "codEstado";
        private const string PARMCODCIDADE = "codCidade";
        private const string PARMCODRUA = "codRua";
        private const string PARMCODCep = "codCep";
        private const string PARMCURSOR = "curCep";
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
                    /*0*/ new OracleParameter(PARMCODPAIS, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*1*/ new OracleParameter(PARMCODESTADO, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*2*/ new OracleParameter(PARMCODCIDADE, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*3*/ new OracleParameter(PARMCODRUA, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*4*/ new OracleParameter(PARMCODCep, OracleType.Int32, 4, ParameterDirection.InputOutput.ToString()) ,
                    /*5*/ new OracleParameter( "codBairroIni", OracleType.Int32),
                    /*6*/ new OracleParameter( "codBairroFim", OracleType.Int32),
                    /*7*/ new OracleParameter( "dscCep", OracleType.VarChar),
                    /*8*/ new OracleParameter( "logUsuario", OracleType.VarChar)
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
            parms[0].Value = this.codPais;
            parms[1].Value = this.codEstado;
            parms[2].Value = this.codCidade;
            parms[3].Value = this.codRua;
            parms[4].Value = this.codCep;
            if (this.codBairroIni != null && this.codBairroIni.ToString() != "")
            {
                parms[5].Value = this.codBairroIni;
            }
            else
            {
                parms[5].Value = DBNull.Value;
            }
            if (this.codBairroFim != null && this.codBairroFim.ToString() != "")
            {
                parms[6].Value = this.codBairroFim;
            }
            else
            {
                parms[6].Value = DBNull.Value;
            }

            parms[7].Value = this.dscCep;
            parms[8].Value = this.logUsuario;
            if (this.codCep < 0)
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
                        codCep = Convert.ToInt32(cmd.Parameters[PARMCODCep].Value);
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
                codCep = Convert.ToInt32(cmd.Parameters[PARMCODCep].Value);
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
        /// <param name="codCep">Código do Registro</param>
        public static void Delete(int codPais, int codEstado, int codCidade, int codRua, int codCep)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMCODPAIS, OracleType.Int32, 4),
                new OracleParameter(PARMCODESTADO, OracleType.Int32, 4),
                new OracleParameter(PARMCODCIDADE, OracleType.Int32, 4),
                new OracleParameter(PARMCODRUA, OracleType.Int32, 4),
                new OracleParameter(PARMCODCep, OracleType.Int32, 4)
            };
            parms[0].Value = codPais;
            parms[1].Value = codEstado;
            parms[2].Value = codCidade;
            parms[3].Value = codRua;
            parms[4].Value = codCep;

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
        /// <param name="codCep">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(int codPais, int codCidade, int codEstado, int codRua, int codCep, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMCODPAIS, OracleType.Int32, 4),
                new OracleParameter(PARMCODESTADO, OracleType.Int32, 4),
                new OracleParameter(PARMCODCIDADE, OracleType.Int32, 4),
                new OracleParameter(PARMCODRUA, OracleType.Int32, 4),
                new OracleParameter(PARMCODCep, OracleType.Int32, 4)
            };
                parms[0].Value = codPais;
                parms[1].Value = codEstado;
                parms[2].Value = codCidade;
                parms[3].Value = codRua;
                parms[4].Value = codCep;
                

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
        /// <param name="codCep">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codCep )
        {
            OracleParameter[] param = new OracleParameter[] { 
                    new OracleParameter(PARMCODCep, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codCep;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="codCep">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codCep, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 
                    new OracleParameter(PARMCODCep, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codCep;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="codCep">Código do Registro</param>
        /// <returns>Cep</returns>
        public static Cep GetDataRow(int codCep)
        {
            OracleDataReader dr = LoadDataDr(codCep);
            Cep Cep = new Cep();
            try
            {
                if (dr.Read())
                {
                    Cep.codCep = Convert.ToInt32(dr["A046_cep"]);
                    if (dr["A011_cd_cid"] != DBNull.Value && dr["A011_cd_cid"] != DBNull.Value && dr["A011_cd_cid"].ToString() != "")
                        Cep.codCidade = new Cidade(Convert.ToInt32(dr["A011_cd_cid"]));
                    if (dr["A021_cd_est"] != DBNull.Value && dr["A021_cd_est"] != DBNull.Value && dr["A021_cd_est"].ToString() != "")
                        Cep.codEstado = new Estado(Convert.ToInt32(dr["A021_cd_est"]));
                    if (dr["A035_cd_pais"] != DBNull.Value && dr["A035_cd_pais"] != DBNull.Value && dr["A035_cd_pais"].ToString() != "")
                        Cep.codPais = new Pais(Convert.ToInt32(dr["A035_cd_pais"]));
                    if (dr["A615_cd_rua"] != DBNull.Value && dr["A615_cd_rua"] != DBNull.Value && dr["A615_cd_rua"].ToString() != "")
                        Cep.codRua = new Rua(Convert.ToInt32(dr["A615_cd_rua"]));
                    if (dr["A616_cd_bairro_ini"] != DBNull.Value && dr["A616_cd_bairro_ini"] != DBNull.Value && dr["A616_cd_bairro_ini"].ToString() != "")
                        Cep.codBairroIni = new Bairro(Convert.ToInt32(dr["A616_cd_bairro_ini"]));
                    if (dr["A616_cd_bairro_fim"] != DBNull.Value && dr["A616_cd_bairro_fim"] != DBNull.Value && dr["A616_cd_bairro_fim"].ToString() != "")
                        Cep.codBairroFim = new Bairro(Convert.ToInt32(dr["A616_cd_bairro_fim"]));
                    Cep.dscCep = Convert.ToString(dr["A046_dsc_Cep"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        Cep.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                Cep = new Cep();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return Cep;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codCep">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>Cep</returns>
        public static Cep GetDataRow(int codCep, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codCep, trans);
            Cep Cep = new Cep();
            try
            {
                if (dr.Read())
                {
                    Cep.codCep = Convert.ToInt32(dr["A046_cep"]);
                    if (dr["A011_cd_cid"] != DBNull.Value && dr["A011_cd_cid"] != DBNull.Value && dr["A011_cd_cid"].ToString() != "")
                        Cep.codCidade = new Cidade(Convert.ToInt32(dr["A011_cd_cid"]));
                    if (dr["A021_cd_est"] != DBNull.Value && dr["A021_cd_est"] != DBNull.Value && dr["A021_cd_est"].ToString() != "")
                        Cep.codEstado = new Estado(Convert.ToInt32(dr["A021_cd_est"]));
                    if (dr["A035_cd_pais"] != DBNull.Value && dr["A035_cd_pais"] != DBNull.Value && dr["A035_cd_pais"].ToString() != "")
                        Cep.codPais = new Pais(Convert.ToInt32(dr["A035_cd_pais"]));
                    if (dr["A615_cd_rua"] != DBNull.Value && dr["A615_cd_rua"] != DBNull.Value && dr["A615_cd_rua"].ToString() != "")
                        Cep.codRua = new Rua(Convert.ToInt32(dr["A615_cd_rua"]));
                    if (dr["A616_cd_bairro_ini"] != DBNull.Value && dr["A616_cd_bairro_ini"] != DBNull.Value && dr["A616_cd_bairro_ini"].ToString() != "")
                        Cep.codBairroIni = new Bairro(Convert.ToInt32(dr["A616_cd_bairro_ini"]));
                    if (dr["A616_cd_bairro_fim"] != DBNull.Value && dr["A616_cd_bairro_fim"] != DBNull.Value && dr["A616_cd_bairro_fim"].ToString() != "")
                        Cep.codBairroFim = new Bairro(Convert.ToInt32(dr["A616_cd_bairro_fim"]));
                    Cep.dscCep = Convert.ToString(dr["A046_dsc_Cep"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        Cep.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                Cep = new Cep();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return Cep;
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
