using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
 
using System.Data;

//-- Classe Classes Sebrae
//-- Data : 16/05/2007 
//-- Autor :  Daniel

namespace Classes
{
    public class Cidade // T011_Cidade  
    {
        #region Atributos
        private int codCidade;
        private Estado codEstado;
        private Pais codPais;
        private int? codMicrorregiao;
        private string nomCidade;
        private string numDDDCidade;
        private int? codEscritorio;
        private string numCEP;
        private string sglCidade;
        private string indCEPMultiplo;
        private int? codIdentificadorMapa;        
        private string logUsuario;
        #endregion

        #region Propriedades
        public int CodCidade
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
        public int? CodMicrorregiao
        {
            get { return codMicrorregiao; }
            set { codMicrorregiao = value; }
        }
        public string NomCidade
        {
            get { return nomCidade; }
            set { nomCidade = value; }
        }
        public string NumDDDCidade
        {
            get { return numDDDCidade; }
            set { numDDDCidade = value; }
        }
        public int? CodEscritorio
        {
            get { return codEscritorio; }
            set { codEscritorio = value; }
        }
        public string NumCEP
        {
            get { return numCEP; }
            set { numCEP = value; }
        }
        public string SglCidade
        {
            get { return sglCidade; }
            set { sglCidade = value; }
        }
        public string IndCEPMultiplo
        {
            get { return indCEPMultiplo; }
            set { indCEPMultiplo = value; }
        }

        public int? CodIdentificadorMapa
        {
            get { return codIdentificadorMapa; }
            set { codIdentificadorMapa = value; }
        }
        public string LogUsuario 
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }
        #endregion

        #region Construtores
        public Cidade()
            : this(-1)
        { }
        public Cidade(int codCidade)
        {
            this.codCidade = codCidade; 
            this.CodEstado = new Estado(); 
            this.CodPais = new Pais();
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "Cidade.CidadeInc";
        private const string SPUPDATE = "Cidade.CidadeAlt";
        private const string SPDELETE = "Cidade.CidadeDel";
        private const string SPSELECTID = "Cidade.CidadeSelId";
        private const string SPSELECTPAG = "Cidade.CidadeSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMCODCIDADE = "codCidade";
        private const string PARMCODESTADO = "codEstado";
        private const string PARMCODPAIS = "codPais";
        private const string PARMCURSOR = "curCidade";
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
                    /*0*/ new OracleParameter(PARMCODCIDADE, OracleType.Int32, 4, ParameterDirection.InputOutput.ToString()) ,
                    /*1*/ new OracleParameter(PARMCODESTADO, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*2*/ new OracleParameter(PARMCODPAIS, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*3*/ new OracleParameter( "codMicrorregiao", OracleType.Int32),
                    /*4*/ new OracleParameter( "nomCidade", OracleType.VarChar),
                    /*5*/ new OracleParameter( "numDDDCidade", OracleType.VarChar),
                    /*6*/ new OracleParameter( "codEscritorio", OracleType.Int32),
                    /*7*/ new OracleParameter( "numCEP", OracleType.Int32),
                    /*8*/ new OracleParameter( "sglCidade", OracleType.VarChar),
                    /*9*/ new OracleParameter( "indCEPMultiplo", OracleType.Int32),
                    /*10*/ new OracleParameter( "codIdentificadorMapa", OracleType.VarChar),
                    /*11*/ new OracleParameter( "logUsuario", OracleType.VarChar)
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
            parms[0].Value = this.codCidade;
            parms[1].Value = this.codEstado;
            parms[2].Value = this.codPais;
            parms[3].Value = this.codMicrorregiao;
            parms[4].Value = this.nomCidade;
            parms[5].Value = this.numDDDCidade;
            parms[6].Value = this.codEscritorio;
            parms[7].Value = this.numCEP;
            parms[8].Value = this.sglCidade;
            parms[9].Value = this.indCEPMultiplo;
            parms[10].Value = this.CodIdentificadorMapa;
            parms[11].Value = this.logUsuario;
            if (this.codCidade < 0)
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
                        codCidade = Convert.ToInt32(cmd.Parameters[PARMCODCIDADE].Value);
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
                codCidade = Convert.ToInt32(cmd.Parameters[PARMCODCIDADE].Value);
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
        /// <param name="CodCidade">Código do Registro</param>
        public static void Delete(int CodCidade, int CodEstado, int CodPais)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMCODCIDADE, OracleType.Int32, 4)  ,
                new OracleParameter(PARMCODESTADO, OracleType.Int32, 4)  ,
                new OracleParameter(PARMCODPAIS, OracleType.Int32, 4)
            };
            parms[0].Value = CodCidade;
            parms[1].Value = CodEstado;
            parms[2].Value = CodPais;
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
        /// <param name="CodCidade">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(int CodCidade, int CodEstado, int CodPais, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMCODCIDADE, OracleType.Int32, 4)  ,
                new OracleParameter(PARMCODESTADO, OracleType.Int32, 4)  ,
                new OracleParameter(PARMCODPAIS, OracleType.Int32, 4) 
            };
                parms[0].Value = CodCidade;
                parms[1].Value = CodEstado;
                parms[2].Value = CodPais;
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
        /// <param name="CodCidade">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int CodCidade, int CodEstado, int CodPais)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCODCIDADE, OracleType.Int32, 4), 
                    new OracleParameter(PARMCODESTADO, OracleType.Int32, 4), 
                    new OracleParameter(PARMCODPAIS, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = CodCidade;
            param[1].Value = CodEstado;
            param[2].Value = CodPais;
            param[3].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="CodCidade">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int CodCidade, int CodEstado, int CodPais, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCODCIDADE, OracleType.Int32, 4), 
                    new OracleParameter(PARMCODESTADO, OracleType.Int32, 4), 
                    new OracleParameter(PARMCODPAIS, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = CodCidade;
            param[1].Value = CodEstado;
            param[2].Value = CodPais;
            param[3].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="CodCidade">Código do Registro</param>
        /// <returns>Cidade</returns>
        public static Cidade GetDataRow(int CodCidade, int CodEstado, int CodPais)
        {
            OracleDataReader dr = LoadDataDr(CodCidade, CodEstado, CodPais);
            Cidade cidade = new Cidade();
            try
            {
                if (dr.Read())
                {
                    cidade.codCidade = Convert.ToInt32(dr["A011_cd_cid"]);
                    if (dr["A021_cd_est"] != DBNull.Value && dr["A021_cd_est"] != DBNull.Value && dr["A021_cd_est"].ToString() != "")
                        cidade.codEstado = new Estado(Convert.ToInt32(dr["A021_cd_est"]));
                    if (dr["A035_cd_Pais"] != DBNull.Value && dr["A035_cd_Pais"] != DBNull.Value && dr["A035_cd_Pais"].ToString() != "")
                        cidade.codPais = new Pais(Convert.ToInt32(dr["A035_cd_Pais"]));
                    if (dr["A033_cd_micr"] != DBNull.Value && dr["A033_cd_micr"] != DBNull.Value && dr["A033_cd_micr"].ToString() != "")
                    cidade.codMicrorregiao = Convert.ToInt32(dr["A033_cd_micr"]);
                    cidade.nomCidade = Convert.ToString(dr["A011_nm_cid"]);
                    cidade.numDDDCidade = Convert.ToString(dr["A011_ddd"]);
                    if (dr["A004_cd_escr"] != DBNull.Value && dr["A004_cd_escr"] != DBNull.Value && dr["A004_cd_escr"].ToString() != "") 
                        cidade.codEscritorio = Convert.ToInt32(dr["A004_cd_escr"]);
                    cidade.numCEP = Convert.ToString(dr["A011_CEP"]);
                    cidade.sglCidade = Convert.ToString(dr["A011_sigla_cid"]);
                    cidade.indCEPMultiplo = Convert.ToString(dr["A011_ind_cep_log"]);
                    if (dr["A011_ID_MAPA"] != DBNull.Value && dr["A011_ID_MAPA"] != DBNull.Value && dr["A011_ID_MAPA"].ToString() != "") 
                        cidade.CodIdentificadorMapa = Convert.ToInt32(dr["A011_ID_MAPA"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        cidade.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                cidade = new Cidade();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return cidade;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="CodCidade">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>Cidade</returns>
        public static Cidade GetDataRow(int CodCidade, int CodEstado, int CodPais, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(CodCidade, CodEstado, CodPais, trans);
            Cidade cidade = new Cidade();
            try
            {
                if (dr.Read())
                {
                    cidade.codCidade = Convert.ToInt32(dr["A011_cd_cid"]);
                    if (dr["A021_cd_est"] != DBNull.Value && dr["A021_cd_est"] != DBNull.Value && dr["A021_cd_est"].ToString() != "")
                        cidade.codEstado = new Estado(Convert.ToInt32(dr["A021_cd_est"]));
                    if (dr["A035_cd_Pais"] != DBNull.Value && dr["A035_cd_Pais"] != DBNull.Value && dr["A035_cd_Pais"].ToString() != "")
                        cidade.codPais = new Pais(Convert.ToInt32(dr["A035_cd_Pais"]));
                    cidade.codMicrorregiao = Convert.ToInt32(dr["A033_cd_micr"]);
                    cidade.nomCidade = Convert.ToString(dr["A011_nm_cid"]);
                    cidade.numDDDCidade = Convert.ToString(dr["A011_ddd"]);
                    cidade.codEscritorio = Convert.ToInt32(dr["A004_cd_escr"]);
                    cidade.numCEP = Convert.ToString(dr["A011_CEP"]);
                    cidade.sglCidade = Convert.ToString(dr["A011_sigla_cid"]);
                    cidade.indCEPMultiplo = Convert.ToString(dr["A011_ind_cep_log"]);
                    cidade.CodIdentificadorMapa = Convert.ToInt32(dr["A011_ID_MAPA"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        cidade.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                cidade = new Cidade();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return cidade;
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
