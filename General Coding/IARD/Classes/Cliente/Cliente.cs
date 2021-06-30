using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
 
using System.Data;

//-- Classe Classes Sebrae
//-- Data : 11/05/2007 
//-- Autor :  Daniel

namespace Classes
{
    public class Cliente  // T012_cli_atend  
    {
        #region Atributos
        private int         codCLIENTE;
        private int         tpCliAten;
        private string      codSebraeNa;
        private DateTime?   dataSebraeNa;
        private int?        parceiroSiac;
        private DateTime?   dataSiac;
        private string      logusuario;        
        #endregion

        #region Propriedades
        public int CodCLIENTE
        {
            get { return codCLIENTE; }
            set { codCLIENTE = value; }
        }  
        public int TpCliAten
        {
            get { return tpCliAten; }
            set { tpCliAten = value; }
        }
        public string CodSebraeNa
        {
            get { return codSebraeNa; }
            set { codSebraeNa = value; }
        }
        public DateTime? DataSebraeNa
        {
            get { return dataSebraeNa; }
            set { dataSebraeNa = value; }
        }
        public int? ParceiroSiac
        {
            get { return parceiroSiac; }
            set { parceiroSiac = value; }
        }
        public DateTime? DataSiac
        {
            get { return dataSiac; }
            set { dataSiac = value; }
        }
        public string Logusuario
        {
            get { return logusuario; }
            set { logusuario = value; }
        }
        #endregion

        #region Construtores
        public Cliente()
            : this(-1)
        { }
        public Cliente(int codCLIENTE)
        {
            this.codCLIENTE = codCLIENTE;
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "Cliente.ClienteInc";
        private const string SPUPDATE = "Cliente.ClienteAlt";
        private const string SPDELETE = "Cliente.ClienteDel";
        private const string SPSELECTID = "Cliente.ClienteSelId";
        private const string SPSELECTPAG = "Cliente.ClienteSelPag";
        private const string SPSELECTESCRITORIO = "Cliente.ClienteSelEsc";
        private const string SPSELECTRegEsc = "Cliente.ClienteSelRegEsc"; //regional e escritorio
        private const string SPSELECTParametro = "Cliente.ClienteParametro"; //busca tabela T994_CONFIG_GERAL
        private const string SPSelectComun = "Cliente.SelectComun"; //para select sem classe
        private const string SPInsertComun = "Cliente.InsertComun"; //para inserir sem classe

        #endregion

        #region Parametros Oracle
        private const string PARMCODIGO = "codCLIENTE";
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
                    /*0*/ new OracleParameter(PARMCODIGO, OracleType.Int32, 4, ParameterDirection.InputOutput.ToString()) ,
                    /*1*/ new OracleParameter( "tpCliAten", OracleType.Int32),
                    /*1*/ new OracleParameter( "codSebraeNa", OracleType.VarChar),
                    /*1*/ new OracleParameter( "dataSebraeNa", OracleType.DateTime),
                    /*1*/ new OracleParameter( "parceiroSiac", OracleType.Int32),
                    /*1*/ new OracleParameter( "dataSiac", OracleType.DateTime),
                    /*1*/ new OracleParameter( "logUsuario", OracleType.VarChar)
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
            parms[0].Value = this.codCLIENTE;
            parms[1].Value = this.tpCliAten;
            parms[2].Value = "";
            if (this.CodSebraeNa != null)
            { parms[2].Value = this.CodSebraeNa; }
            parms[3].Value = DBNull.Value;
            if (this.dataSebraeNa != null)
            { parms[3].Value = this.dataSebraeNa; }
            parms[4].Value = DBNull.Value;
            if (this.parceiroSiac != null)
            { parms[4].Value = this.parceiroSiac; }
            parms[5].Value = DBNull.Value;
            if (this.dataSiac != null)
            { parms[5].Value = this.dataSiac; }
            parms[6].Value = "";
            if (this.logusuario != null)
            { parms[6].Value = this.logusuario; }

            if (this.codCLIENTE < 0)
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
                        codCLIENTE = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
                codCLIENTE = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
        /// <param name="codigo">Código do Registro</param>
        public static void Delete(int codigo)
        {
            OracleParameter[] parms = new OracleParameter[] { new OracleParameter(PARMCODIGO, OracleType.Int32, 4) };
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
        /// Delete sem tratamento de transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(int codigo, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { new OracleParameter(PARMCODIGO, OracleType.Int32, 4) };
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
        /// <param name="codigo">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codigo)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCODIGO, OracleType.Int32, 4), 
                    new OracleParameter("curCliente", OracleType.Cursor)
                };

            param[0].Value = codigo;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codigo, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { new OracleParameter(PARMCODIGO, OracleType.Int32, 4),
                                                                  new OracleParameter("curCliente", OracleType.Cursor)
};
            param[0].Value = codigo;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region LoadDataDrEscritorio
        /// <summary>
        /// LoadDataDr
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDrEscritorio()
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter("curCliente", OracleType.Cursor)
                };

            param[0].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTESCRITORIO, param);
            return dr;
        }

        public static OracleDataReader LoadDataDrRegEsc(int codigo)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter("codRegional", OracleType.Int32, 4), 
                    new OracleParameter("curCliente", OracleType.Cursor)
                };

            param[0].Value = codigo;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTRegEsc, param);
            return dr;
        }

        public static OracleDataReader LoadDataDrParametro(string busca)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter("codParametro", OracleType.VarChar, 100), 
                    new OracleParameter("curCliente", OracleType.Cursor)
                };

            param[0].Value = busca;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTParametro, param);
            return dr;
        }

        public static OracleDataReader LoadDataDrSelectComun(string sCampos,string sTabela,string sWhere)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter("sCampos", OracleType.VarChar, 1000), 
                    new OracleParameter("sTabela", OracleType.VarChar, 1000), 
                    new OracleParameter("sWhere", OracleType.VarChar, 1000), 
                    new OracleParameter("curCliente", OracleType.Cursor)
                };

            param[0].Value = sCampos;
            param[1].Value = sTabela;
            param[2].Value = sWhere;
            param[3].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSelectComun, param);
            return dr;
        }

        public static OracleDataReader LoadDataDrInsertComun(string sCampos, string sTabela, string sValores)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter("sCampos", OracleType.VarChar, 1000), 
                    new OracleParameter("sTabela", OracleType.VarChar, 1000), 
                    new OracleParameter("sValores", OracleType.VarChar, 1000), 
                    new OracleParameter("curCliente", OracleType.Cursor)
                };

            param[0].Value = sCampos;
            param[1].Value = sTabela;
            param[2].Value = sValores;
            param[3].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPInsertComun, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <returns>CLIENTE</returns>
        public static Cliente GetDataRow(int codigo)
        {
            OracleDataReader dr = LoadDataDr(codigo);
            Cliente cliente = new Cliente();
            try
            {
                if (dr.Read())
                {
                    cliente.codCLIENTE = Convert.ToInt32(dr["A012_Cd_Cli"]);
                    cliente.tpCliAten = Convert.ToInt32(dr["A012_Tp_Cli_Aten"]);
                    cliente.CodSebraeNa = Convert.ToString(dr["A012_Cd_Sebraena"]);
                    if (dr["A012_Dt_Sebraena"] != DBNull.Value && dr["A012_Dt_Sebraena"] != DBNull.Value && dr["A012_Dt_Sebraena"].ToString() != "")
                        cliente.dataSebraeNa = Convert.ToDateTime(dr["A012_Dt_Sebraena"]);
                    if (dr["A012_Parceiro_Siac"] != DBNull.Value && dr["A012_Parceiro_Siac"] != DBNull.Value && dr["A012_Parceiro_Siac"].ToString() != "")
                        cliente.parceiroSiac = Convert.ToInt32(dr["A012_Parceiro_Siac"]);
                    if (dr["A012_Dt_Siac"] != DBNull.Value && dr["A012_Dt_Siac"] != DBNull.Value && dr["A012_Dt_Siac"].ToString() != "")
                    cliente.dataSiac = Convert.ToDateTime(dr["A012_Dt_Siac"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                    cliente.logusuario = Convert.ToString(dr["Usu_Inc_Alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                cliente = new Cliente();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return cliente;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>CLIENTE</returns>
        public static Cliente GetDataRow(int codigo, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codigo, trans);
            Cliente cliente = new Cliente();
            try
            {
                if (dr.Read())
                {
                    cliente.codCLIENTE = Convert.ToInt32(dr["A012_Cd_Cli"]);
                    cliente.tpCliAten = Convert.ToInt32(dr["A012_Tp_Cli_Aten"]);
                    cliente.CodSebraeNa = Convert.ToString(dr["A012_Cd_Sebraena"]);
                    if (dr["A012_Dt_Sebraena"] != DBNull.Value && dr["A012_Dt_Sebraena"] != DBNull.Value && dr["A012_Dt_Sebraena"].ToString() != "")
                        cliente.dataSebraeNa = Convert.ToDateTime(dr["A012_Dt_Sebraena"]);
                    if (dr["A012_Parceiro_Siac"] != DBNull.Value && dr["A012_Parceiro_Siac"] != DBNull.Value && dr["A012_Parceiro_Siac"].ToString() != "")
                        cliente.parceiroSiac = Convert.ToInt32(dr["A012_Parceiro_Siac"]);
                    if (dr["A012_Dt_Siac"] != DBNull.Value && dr["A012_Dt_Siac"] != DBNull.Value && dr["A012_Dt_Siac"].ToString() != "")
                        cliente.dataSiac = Convert.ToDateTime(dr["A012_Dt_Siac"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        cliente.logusuario = Convert.ToString(dr["Usu_Inc_Alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                cliente = new Cliente();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return cliente;
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
                new OracleParameter("curCliente", OracleType.Cursor),
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
