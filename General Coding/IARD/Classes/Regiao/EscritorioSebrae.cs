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
    public class EscritorioSebrae /// T004_ESCR_SEBRAE 
    {
        #region Atributos
        private int codEscrSebrae;
        private string indModoAtendimento;
        private int? tpoEscritorio;
        private int? codEscrRegional;
        private int? indDesativado;
        private int? indCalculaRetencao;
        private string codSOL;
        private string codObj;
        private string codAB;
        private string codAE;
        private string logUsuario;
        #endregion

        #region Propriedades
        public int? CodEscrRegional
        {
            get { return codEscrRegional; }
            set { codEscrRegional = value; }
        }
        public int CodEscrSebrae
        {
            get { return codEscrSebrae; }
            set { codEscrSebrae = value; }
        }
        public int? IndCalculaRetencao
        {
            get { return indCalculaRetencao; }
            set { indCalculaRetencao = value; }
        }        
        public int? TpoEscritorio
        {
            get { return tpoEscritorio; }
            set { tpoEscritorio = value; }
        }        
        public string IndModoAtendimento
        {
            get { return indModoAtendimento; }
            set { indModoAtendimento = value; }
        }       
        public string CodSOL
        {
            get { return codSOL; }
            set { codSOL = value; }
        }        
        public string CodAE
        {
            get { return codAE; }
            set { codAE = value; }
        }        
        public string CodAB
        {
            get { return codAB; }
            set { codAB = value; }
        }        
        public string CodObj
        {
            get { return codObj; }
            set { codObj = value; }
        }
        public int? IndDesativado
        {
            get { return indDesativado; }
            set { indDesativado = value; }
        }        
        public string LogUsuario
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }
        #endregion
        
        #region Construtores
        public EscritorioSebrae()
            : this(-1)
        { }
        public EscritorioSebrae(int codEscrSebrae)
        {
            this.codEscrSebrae = codEscrSebrae;
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "EscritorioSebrae.EscritorioSebraeInc";
        private const string SPUPDATE = "EscritorioSebrae.EscritorioSebraeAlt";
        private const string SPDELETE = "EscritorioSebrae.EscritorioSebraeDel";
        private const string SPSELECTID = "EscritorioSebrae.EscritorioSebraeSelId";
        private const string SPSELECTPAG = "EscritorioSebrae.EscritorioSebraeSelPag";
        private const string SPSELECTIDUSUARIO = "EscritorioSebrae.EscritorioUsuarioSelId";
        private const string SPSELECTIDDEPARTAMENTO = "EscritorioSebrae.EscritorioDepartamentoSelId";
        private const string SPDEPARTAMESELID = "EscritorioSebrae.DepartamentoSelId";
        
        #endregion

        #region Parametros Oracle
        private const string PARMCODIGO = "codEscrSebrae";
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
                    /*1*/ new OracleParameter( "codEscrRegional", OracleType.Int32),
                    /*2*/ new OracleParameter( "indCalculaRetencao", OracleType.Int32),
                    /*3*/ new OracleParameter( "tpoEscritorio", OracleType.VarChar),
                    /*4*/ new OracleParameter( "IndModoAtendimento", OracleType.Int32),
                    /*5*/ new OracleParameter( "codSOL", OracleType.VarChar),
                    /*6*/ new OracleParameter( "codAE", OracleType.VarChar),
                    /*7*/ new OracleParameter( "codAB", OracleType.VarChar),
                    /*8*/ new OracleParameter( "codObj", OracleType.VarChar),
                    /*9*/ new OracleParameter( "indDesativado", OracleType.Int32),
                    /*10*/ new OracleParameter( "logUsuario", OracleType.VarChar)
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
            parms[0].Value = this.codEscrSebrae;
            parms[1].Value = this.codEscrRegional;
            parms[2].Value = this.indCalculaRetencao;
            parms[3].Value = this.tpoEscritorio;
            parms[4].Value = this.indModoAtendimento;
            parms[5].Value = this.codSOL;
            parms[6].Value = this.codAE;
            parms[7].Value = this.codAB;
            parms[8].Value = this.codObj;
            parms[9].Value = this.IndDesativado;
            parms[10].Value = this.logUsuario;
            if (this.codEscrSebrae < 0)
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
                        codEscrSebrae = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
                CodEscrSebrae = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
                    new OracleParameter("curEscritorioSebrae", OracleType.Cursor)
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
                                                                  new OracleParameter("curEscritorioSebrae", OracleType.Cursor)
};
            param[0].Value = codigo;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <returns>EscritorioSebrae</returns>
        public static EscritorioSebrae GetDataRow(int codigo)
        {
            OracleDataReader dr = LoadDataDr(codigo);
            EscritorioSebrae escrSebrae    = new EscritorioSebrae();
            try
            {
                if (dr.Read())
                {
                    escrSebrae.codEscrSebrae = Convert.ToInt32(dr["A004_cd_escr"]);
                    escrSebrae.indModoAtendimento = Convert.ToString(dr["A004_ind_modo_atend"]);
                    if (dr["A020_cd_esc_reg"] != DBNull.Value && dr["A020_cd_esc_reg"] != DBNull.Value && dr["A020_cd_esc_reg"].ToString() != "")
                        escrSebrae.codEscrRegional = Convert.ToInt32(dr["A020_cd_esc_reg"]);
                    if (dr["A004_calcula_retencao"] != DBNull.Value && dr["A004_calcula_retencao"] != DBNull.Value && dr["A004_calcula_retencao"].ToString() != "")
                        escrSebrae.indCalculaRetencao = Convert.ToInt32(dr["A004_calcula_retencao"]);
                    if (dr["A079_cd_tp_esc"] != DBNull.Value && dr["A079_cd_tp_esc"] != DBNull.Value && dr["A079_cd_tp_esc"].ToString() != "")
                        escrSebrae.tpoEscritorio = Convert.ToInt32(dr["A079_cd_tp_esc"]);
                    if (dr["A004_ind_desat"] != DBNull.Value && dr["A004_ind_desat"] != DBNull.Value && dr["A004_ind_desat"].ToString() != "")
                        escrSebrae.IndDesativado = Convert.ToInt32(dr["A004_ind_desat"]);
                    escrSebrae.codSOL = Convert.ToString(dr["CodSOL"]);
                    escrSebrae.CodAE = Convert.ToString(dr["CodAE"]);
                    escrSebrae.codAB = Convert.ToString(dr["CodAB"]);
                    escrSebrae.codObj = Convert.ToString(dr["CodObj"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        escrSebrae.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                escrSebrae = new EscritorioSebrae();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return escrSebrae;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>EscritorioSebrae</returns>
        public static EscritorioSebrae GetDataRow(int codigo, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codigo, trans);
            EscritorioSebrae escrSebrae = new EscritorioSebrae();
            try
            {
                if (dr.Read())
                {
                    escrSebrae.codEscrSebrae = Convert.ToInt32(dr["A004_cd_escr"]);
                    escrSebrae.indModoAtendimento = Convert.ToString(dr["A004_ind_modo_atend"]);
                    if (dr["A020_cd_esc_reg"] != DBNull.Value && dr["A020_cd_esc_reg"] != DBNull.Value && dr["A020_cd_esc_reg"].ToString() != "")
                        escrSebrae.codEscrRegional = Convert.ToInt32(dr["A020_cd_esc_reg"]);
                    if (dr["A004_calcula_retencao"] != DBNull.Value && dr["A004_calcula_retencao"] != DBNull.Value && dr["A004_calcula_retencao"].ToString() != "")
                        escrSebrae.indCalculaRetencao = Convert.ToInt32(dr["A004_calcula_retencao"]);
                    if (dr["A079_cd_tp_esc"] != DBNull.Value && dr["A079_cd_tp_esc"] != DBNull.Value && dr["A079_cd_tp_esc"].ToString() != "")
                        escrSebrae.tpoEscritorio = Convert.ToInt32(dr["A079_cd_tp_esc"]);
                    if (dr["A004_ind_desat"] != DBNull.Value && dr["A004_ind_desat"] != DBNull.Value && dr["A004_ind_desat"].ToString() != "")
                        escrSebrae.IndDesativado = Convert.ToInt32(dr["A004_ind_desat"]);
                    escrSebrae.codSOL = Convert.ToString(dr["CodSOL"]);
                    escrSebrae.CodAE = Convert.ToString(dr["CodAE"]);
                    escrSebrae.codAB = Convert.ToString(dr["CodAB"]);
                    escrSebrae.codObj = Convert.ToString(dr["CodObj"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        escrSebrae.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                escrSebrae = new EscritorioSebrae();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return escrSebrae;
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
                new OracleParameter("curEscritorioSebrae", OracleType.Cursor),
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


        #region LoadDataEscritorioUsuario


        /// <summary>
        /// LoadDataPaginacao
        /// </summary>
        /// <param name="Where">Cláusula where utilizada na consulta</param>
        /// <param name="PaginaCorrente">Número da página que deseja selecionar</param>
        /// <param name="TamanhoPagina">Quantidade de registros em cada página</param>
        /// <param name="ExpressaoOrdenacao">Expressão de ordenação</param>
        /// <returns>Instância do objeto Paginação, contendo um DataReader e o total de registros</returns>
        /// 
        public static Paginacao LoadDataEscritorioUsuario(int codPessoa)
        {
            Paginacao paginacao = new Paginacao();

            OracleParameter[] parms = new OracleParameter[] { 
			    new OracleParameter("codPessoa", OracleType.Int32),
                new OracleParameter("curEscritorioSebrae", OracleType.Cursor) 
              };

            parms[0].Value = codPessoa; 
            parms[1].Direction = ParameterDirection.Output; 

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTIDUSUARIO, parms);
            
            paginacao.DataReader = dr;
            //  traz o total de registros de retorno direto dos parametros do ExecuteReader---
            //paginacao.TotalRegistros = Convert.ToInt32(parms[5].Value);
            return paginacao;
        }
        #endregion


        #region LoadDataEscritorioDepartamento


        /// <summary>
        /// LoadDataPaginacao
        /// </summary>
        /// <param name="Where">Cláusula where utilizada na consulta</param>
        /// <param name="PaginaCorrente">Número da página que deseja selecionar</param>
        /// <param name="TamanhoPagina">Quantidade de registros em cada página</param>
        /// <param name="ExpressaoOrdenacao">Expressão de ordenação</param>
        /// <returns>Instância do objeto Paginação, contendo um DataReader e o total de registros</returns>
        /// 
        public static Paginacao LoadDataEscritorioDepartamento(int codDepartamento)
        {
            Paginacao paginacao = new Paginacao();

            OracleParameter[] parms = new OracleParameter[] { 
			    new OracleParameter("codDepartamento", OracleType.Int32),
                new OracleParameter("curEscritorioSebrae", OracleType.Cursor) 
              };

            parms[0].Value = codDepartamento;
            parms[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTIDDEPARTAMENTO, parms);

            paginacao.DataReader = dr;
            //  traz o total de registros de retorno direto dos parametros do ExecuteReader---
            //paginacao.TotalRegistros = Convert.ToInt32(parms[5].Value);
            return paginacao;
        }

        public static Paginacao LoadDataDepartamento(int codEscrSebrae)
        {
            Paginacao paginacao = new Paginacao();

            OracleParameter[] parms = new OracleParameter[] { 
			    new OracleParameter("codEscrSebrae", OracleType.Int32),
                new OracleParameter("curEscritorioSebrae", OracleType.Cursor) 
              };

            parms[0].Value = codEscrSebrae;
            parms[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPDEPARTAMESELID, parms);

            paginacao.DataReader = dr;
            //  traz o total de registros de retorno direto dos parametros do ExecuteReader---
            //paginacao.TotalRegistros = Convert.ToInt32(parms[5].Value);
            return paginacao;
        }

        #endregion

        #endregion

    }
}
