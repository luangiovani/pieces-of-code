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
    public class Rua // T615_Rua
    {
        #region Atributos
        private int codRua;
        private Cidade codCidade;
        private Estado codEstado;
        private Pais codPais;
        private int? codTipoLogradouro;
        private string nomRua;
        private string logUsuario;
        #endregion

        #region Propriedades
        public int CodRua
        {
            get { return codRua; }
            set { codRua = value; }
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
        public int? CodTipoLogradouro
        {
            get { return codTipoLogradouro; }
            set { codTipoLogradouro = value; }
        }
        public string NomRua
        {
            get { return nomRua; }
            set { nomRua = value; }
        }
        public string LogUsuario
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }
        #endregion

        #region Construtores
        public Rua()
            : this(-1)
        { }
        public Rua(int codRua)
        {
            this.codRua = codRua;
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "Rua.RuaInc";
        private const string SPUPDATE = "Rua.RuaAlt";
        private const string SPDELETE = "Rua.RuaDel";
        private const string SPSELECTID = "Rua.RuaSelId";
        private const string SPSELECTPAG = "Rua.RuaSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMCODRUA = "codRua";
        private const string PARMCODCIDADE = "codCidade";
        private const string PARMCODESTADO = "codEstado";
        private const string PARMCODPAIS = "codPais";
        private const string PARMCURSOR = "curRua";
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
                    /*0*/ new OracleParameter(PARMCODRUA, OracleType.Int32, 4, ParameterDirection.InputOutput.ToString()) ,
                    /*1*/ new OracleParameter(PARMCODCIDADE, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*2*/ new OracleParameter(PARMCODESTADO, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*3*/ new OracleParameter(PARMCODPAIS, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*4*/ new OracleParameter( "codTipoLogradouro", OracleType.Int32),
                    /*5*/ new OracleParameter( "nomRua", OracleType.VarChar),
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
            parms[0].Value = this.codRua;
            parms[1].Value = this.codCidade;
            parms[2].Value = this.codEstado;
            parms[3].Value = this.codPais;
            if (this.codTipoLogradouro != null && this.codTipoLogradouro.ToString() != "")
            {
                parms[4].Value = this.codTipoLogradouro;
            }
            else 
            {
                parms[4].Value = DBNull.Value;
            }
            
            parms[5].Value = this.nomRua;
            parms[6].Value = this.logUsuario;
            if (this.codRua < 0)
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
                        codRua = Convert.ToInt32(cmd.Parameters[PARMCODRUA].Value);
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
                codRua = Convert.ToInt32(cmd.Parameters[PARMCODRUA].Value);
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
        /// <param name="codRua">Código do Registro</param>
        public static void Delete(int codRua, int codCidade, int codEstado, int codPais)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMCODRUA, OracleType.Int32, 4),
                new OracleParameter(PARMCODCIDADE, OracleType.Int32, 4),
                new OracleParameter(PARMCODESTADO, OracleType.Int32, 4),
                new OracleParameter(PARMCODPAIS, OracleType.Int32, 4)
            };
            parms[0].Value = codRua;
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
        /// Delete sem tratamento de transação
        /// </summary>
        /// <param name="codRua">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(int codRua, int codCidade, int codEstado, int codPais, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMCODRUA, OracleType.Int32, 4),
                new OracleParameter(PARMCODCIDADE, OracleType.Int32, 4),
                new OracleParameter(PARMCODESTADO, OracleType.Int32, 4),
                new OracleParameter(PARMCODPAIS, OracleType.Int32, 4)
            };
                parms[0].Value = codRua;
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
        /// <param name="codRua">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codRua, int codCidade, int codEstado, int codPais)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCODRUA, OracleType.Int32, 4), 
                    new OracleParameter(PARMCODCIDADE, OracleType.Int32, 4), 
                    new OracleParameter(PARMCODESTADO, OracleType.Int32, 4), 
                    new OracleParameter(PARMCODPAIS, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codRua;
            param[1].Value = codCidade;
            param[2].Value = codEstado;
            param[3].Value = codPais;
            param[4].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="codRua">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codRua, int codCidade, int codEstado, int codPais, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCODRUA, OracleType.Int32, 4), 
                    new OracleParameter(PARMCODCIDADE, OracleType.Int32, 4), 
                    new OracleParameter(PARMCODESTADO, OracleType.Int32, 4), 
                    new OracleParameter(PARMCODPAIS, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codRua;
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
        /// <param name="codRua">Código do Registro</param>
        /// <returns>Rua</returns>
        public static Rua GetDataRow(int codRua, int codCidade, int codEstado, int codPais)
        {
            OracleDataReader dr = LoadDataDr(codRua, codCidade, codEstado, codPais);
            Rua rua = new Rua();
            try
            {
                if (dr.Read())
                {
                    rua.codRua = Convert.ToInt32(dr["A615_cd_rua"]);
                    if (dr["A011_cd_cid"] != DBNull.Value && dr["A011_cd_cid"] != DBNull.Value && dr["A011_cd_cid"].ToString() != "")
                        rua.codCidade = new Cidade(Convert.ToInt32(dr["A011_cd_cid"]));
                    if (dr["A021_cd_est"] != DBNull.Value && dr["A021_cd_est"] != DBNull.Value && dr["A021_cd_est"].ToString() != "")
                        rua.codEstado = new Estado(Convert.ToInt32(dr["A021_cd_est"]));
                    if (dr["A035_cd_pais"] != DBNull.Value && dr["A035_cd_pais"] != DBNull.Value && dr["A035_cd_pais"].ToString() != "")
                        rua.codPais = new Pais(Convert.ToInt32(dr["A035_cd_pais"]));
                    if (dr["A040_cd_tp_log"] != DBNull.Value && dr["A040_cd_tp_log"] != DBNull.Value && dr["A040_cd_tp_log"].ToString() != "")
                        rua.codTipoLogradouro = Convert.ToInt32(dr["A040_cd_tp_log"]);
                    rua.nomRua = Convert.ToString(dr["A040_DSC_TP_LOG"]) + " " + Convert.ToString(dr["A615_nm_rua"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        rua.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                rua = new Rua();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return rua;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codRua">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>Rua</returns>
        public static Rua GetDataRow(int codRua, int codCidade, int codEstado, int codPais, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codRua, codCidade, codEstado, codPais, trans);
            Rua rua = new Rua();
            try
            {
                if (dr.Read())
                {
                    rua.codRua = Convert.ToInt32(dr["A615_cd_rua"]);
                    if (dr["A011_cd_cid"] != DBNull.Value && dr["A011_cd_cid"] != DBNull.Value && dr["A011_cd_cid"].ToString() != "")
                        rua.codCidade = new Cidade(Convert.ToInt32(dr["A011_cd_cid"]));
                    if (dr["A021_cd_est"] != DBNull.Value && dr["A021_cd_est"] != DBNull.Value && dr["A021_cd_est"].ToString() != "")
                        rua.codEstado = new Estado(Convert.ToInt32(dr["A021_cd_est"]));
                    if (dr["A035_cd_pais"] != DBNull.Value && dr["A035_cd_pais"] != DBNull.Value && dr["A035_cd_pais"].ToString() != "")
                        rua.codPais = new Pais(Convert.ToInt32(dr["A035_cd_pais"]));
                    if (dr["A040_cd_tp_log"] != DBNull.Value && dr["A040_cd_tp_log"] != DBNull.Value && dr["A040_cd_tp_log"].ToString() != "")
                        rua.codTipoLogradouro = Convert.ToInt32(dr["A040_cd_tp_log"]);
                    rua.nomRua = Convert.ToString(dr["A040_DSC_TP_LOG"]) + " " + Convert.ToString(dr["A615_nm_rua"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        rua.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                rua = new Rua();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return rua;
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
