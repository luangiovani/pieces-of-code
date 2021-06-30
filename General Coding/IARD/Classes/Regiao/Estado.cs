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
    public class Estado // T616_Estado 
    {
        #region Atributos
        private int codEstado;
        private Pais codPais;
        private string nomEstado;
        private string sglEstado;
        private string codIdentificadorMapa;
        private string logUsuario;
        #endregion

        #region Propriedades
        public int CodEstado
        {
            get { return codEstado; }
            set { codEstado = value; }
        }
        public Pais CodPais
        {
            get { return codPais; }
            set { codPais = value; }
        }
        public string NomEstado
        {
            get { return nomEstado; }
            set { nomEstado = value; }
        }
        public string SglEstado
        {
            get { return sglEstado; }
            set { sglEstado = value; }
        }
        public string CodIdentificadorMapa
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
        public Estado()
            : this(-1)
        { }
        public Estado(int codEstado)
        {
            this.codEstado = codEstado;
            this.codPais = new Pais();
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "Estado.EstadoInc";
        private const string SPUPDATE = "Estado.EstadoAlt";
        private const string SPDELETE = "Estado.EstadoDel";
        private const string SPSELECTID = "Estado.EstadoSelId";
        private const string SPSELECTPAG = "Estado.EstadoSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMCODESTADO = "codEstado";
        private const string PARMCODPAIS = "codPais";
        private const string PARMCURSOR = "curEstado";
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
                    /*0*/ new OracleParameter(PARMCODESTADO, OracleType.Int32, 4, ParameterDirection.InputOutput.ToString()) ,
                    /*1*/ new OracleParameter(PARMCODPAIS, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*2*/ new OracleParameter( "nomEstado", OracleType.VarChar),
                    /*3*/ new OracleParameter( "sglEstado", OracleType.VarChar),
                    /*4*/ new OracleParameter( "codIdentificadorMapa", OracleType.VarChar),
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
            parms[0].Value = this.codEstado;
            parms[1].Value = this.codPais;
            parms[2].Value = this.nomEstado;
            parms[3].Value = this.sglEstado;
            parms[4].Value = this.CodIdentificadorMapa;
            parms[5].Value = this.logUsuario;
            if (this.codEstado < 0)
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
                        codEstado = Convert.ToInt32(cmd.Parameters[PARMCODESTADO].Value);
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
                codEstado = Convert.ToInt32(cmd.Parameters[PARMCODESTADO].Value);
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
        /// <param name="codEstado">Código do Registro</param>
        public static void Delete(int codEstado, int codPais)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMCODESTADO, OracleType.Int32, 4)  ,
                new OracleParameter(PARMCODPAIS, OracleType.Int32, 4)
            };
            parms[0].Value = codEstado;
            parms[1].Value = codPais;
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
        /// <param name="codEstado">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(int codEstado, int codPais, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMCODESTADO, OracleType.Int32, 4)  ,
                new OracleParameter(PARMCODPAIS, OracleType.Int32, 4)
            };
                parms[0].Value = codEstado;
                parms[1].Value = codPais;
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
        /// <param name="codEstado">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codEstado, int codPais)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCODESTADO, OracleType.Int32, 4), 
                    new OracleParameter(PARMCODPAIS, OracleType.Int32, 4),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codEstado;
            param[1].Value = codPais;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="codEstado">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codEstado, int codPais, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCODESTADO, OracleType.Int32, 4), 
                    new OracleParameter(PARMCODPAIS, OracleType.Int32, 4),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codEstado;
            param[1].Value = codPais;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="codEstado">Código do Registro</param>
        /// <returns>Estado</returns>
        public static Estado GetDataRow(int codEstado, int codPais)
        {
            OracleDataReader dr = LoadDataDr(codEstado, codPais);
            Estado estado = new Estado();
            try
            {
                if (dr.Read())
                {
                    estado.codEstado = Convert.ToInt32(dr["A021_cd_est"]);
                    if (dr["A035_cd_Pais"] != DBNull.Value && dr["A035_cd_Pais"] != DBNull.Value && dr["A035_cd_Pais"].ToString() != "")
                        estado.codPais = new Pais(Convert.ToInt32(dr["A035_cd_Pais"]));
                    estado.nomEstado = Convert.ToString(dr["A021_nm_est"]);
                    estado.sglEstado = Convert.ToString(dr["A021_sgl_est"]);
                    estado.CodIdentificadorMapa = Convert.ToString(dr["A021_cd_cid_capital"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        estado.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                estado = new Estado();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return estado;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codEstado">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>Estado</returns>
        public static Estado GetDataRow(int codEstado, int codPais, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codEstado, codPais, trans);
            Estado estado = new Estado();
            try
            {
                if (dr.Read())
                {
                    estado.codEstado = Convert.ToInt32(dr["A021_cd_est"]);
                    if (dr["A035_cd_Pais"] != DBNull.Value && dr["A035_cd_Pais"] != DBNull.Value && dr["A035_cd_Pais"].ToString() != "")
                        estado.codPais = new Pais(Convert.ToInt32(dr["A035_cd_Pais"]));
                    estado.nomEstado = Convert.ToString(dr["A021_nm_est"]);
                    estado.sglEstado = Convert.ToString(dr["A021_sgl_est"]);
                    estado.CodIdentificadorMapa = Convert.ToString(dr["A021_cd_cid_capital"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        estado.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                estado = new Estado();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return estado;
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
