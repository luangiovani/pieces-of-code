using System;
using System.Data.OracleClient;
using System.Data;

namespace Database.Entities
{
    /// <summary>
    /// Classe de Mapeamento com a Entidade de Banco de Dados - T073_PorteEmpresa
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
    public class PorteEmpresa
    {
        #region Atributos
        private string codPorteEmpresa;
        private SetorialMercado codSetorMercado;
        private int qtdEmpregadosDe;
        private int qtdEmpregadosAte;
        private string logusuario;
        #endregion

        #region Propriedades
        public string CodPorteEmpresa
        {
            get { return codPorteEmpresa; }
            set { codPorteEmpresa = value; }
        }
        public SetorialMercado CodSetorMercado
        {
            get { return codSetorMercado; }
            set { codSetorMercado = value; }
        }
        public int QtdEmpregadosDe
        {
            get { return qtdEmpregadosDe; }
            set { qtdEmpregadosDe = value; }
        }
        public int QtdEmpregadosAte
        {
            get { return qtdEmpregadosAte; }
            set { qtdEmpregadosAte = value; }
        }
        public string Logusuario
        {
            get { return logusuario; }
            set { logusuario = value; }
        }
        #endregion

        #region Construtores
        public PorteEmpresa()
            : this("-1")
        { }
        public PorteEmpresa(string codPorteEmpresa)
        {
            this.codPorteEmpresa = codPorteEmpresa;
            this.codSetorMercado = new SetorialMercado();
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "PorteEmpresa.PorteEmpresaInc";
        private const string SPUPDATE = "PorteEmpresa.PorteEmpresaAlt";
        private const string SPDELETE = "PorteEmpresa.PorteEmpresaDel";
        private const string SPSELECTID = "PorteEmpresa.PorteEmpresaSelId";
        private const string SPSELECTPAG = "PorteEmpresa.PorteEmpresaSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMCODIGO = "codPorteEmpresa";
        private const string PARMCODIGO2 = "codSetorMercado";
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
                /*1*/ new OracleParameter(PARMCODIGO2, OracleType.VarChar, 20, ParameterDirection.Input.ToString()) ,
                /*2*/ new OracleParameter( "qtdEmpregadosDe", OracleType.VarChar),
                /*3*/ new OracleParameter( "qtdEmpregadosAte", OracleType.Int32),
                /*4*/ new OracleParameter( "logusuario", OracleType.VarChar)
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
            parms[0].Value = this.codPorteEmpresa;
            parms[1].Value = this.codSetorMercado;
            parms[2].Value = this.qtdEmpregadosDe;
            parms[3].Value = this.qtdEmpregadosAte;
            parms[4].Value = this.logusuario;
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
                        // Primary Key alfanumerica nao usa obtenção de chave do porteEmpresa
                        //codPorteEmpresa = Convert.ToString(cmd.Parameters[PARMCODIGO].Value);
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
                //codPorteEmpresa = Convert.ToString(cmd.Parameters[PARMCODIGO].Value);
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
        /// <param name="codigo">Código do Registro</param>
        public static void Delete(string codigo, string codigo2)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMCODIGO, OracleType.VarChar, 20),
                new OracleParameter(PARMCODIGO2, OracleType.VarChar, 20) };
            parms[0].Value = codigo;
            parms[1].Value = codigo2;
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
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(string codigo, string codigo2, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { 
                    new OracleParameter(PARMCODIGO, OracleType.VarChar, 20),
                    new OracleParameter(PARMCODIGO2, OracleType.VarChar, 20) };
                parms[0].Value = codigo;
                parms[1].Value = codigo2;
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
        /// <param name="codigo">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(string codigo, string codigo2)
        {
            OracleParameter[] param = new OracleParameter[] { 

                new OracleParameter(PARMCODIGO, OracleType.VarChar, 20), 
                new OracleParameter(PARMCODIGO2, OracleType.VarChar, 20), 
                new OracleParameter("curPorteEmpresa", OracleType.Cursor)
            };

            param[0].Value = codigo;
            param[1].Value = codigo2;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(string codigo, string codigo2, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 
                new OracleParameter(PARMCODIGO, OracleType.VarChar, 20), 
                new OracleParameter(PARMCODIGO2, OracleType.VarChar, 20), 
                new OracleParameter("curPorteEmpresa", OracleType.Cursor)
            };

            param[0].Value = codigo;
            param[1].Value = codigo2;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <returns>PorteEmpresa</returns>
        public static PorteEmpresa GetDataRow(string codigo, string codigo2)
        {
            OracleDataReader dr = LoadDataDr(codigo, codigo2);
            PorteEmpresa porteEmpresa = new PorteEmpresa();
            try
            {
                if (dr.Read())
                {
                    porteEmpresa.codPorteEmpresa = Convert.ToString(dr["A073_porte"]);
                    if (dr["A048_cd_setor"] != DBNull.Value && dr["A048_cd_setor"] != DBNull.Value && dr["A048_cd_setor"].ToString() != "")
                        porteEmpresa.codSetorMercado = new SetorialMercado(Convert.ToString(dr["A048_cd_setor"]));
                    porteEmpresa.qtdEmpregadosDe = Convert.ToInt32(dr["A073_num_emp_de"]);
                    porteEmpresa.qtdEmpregadosAte = Convert.ToInt32(dr["A073_num_emp_ate"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        porteEmpresa.logusuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                porteEmpresa = new PorteEmpresa();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return porteEmpresa;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>PorteEmpresa</returns>
        public static PorteEmpresa GetDataRow(string codigo, string codigo2, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codigo, codigo2, trans);
            PorteEmpresa porteEmpresa = new PorteEmpresa();
            try
            {
                if (dr.Read())
                {
                    porteEmpresa.codPorteEmpresa = Convert.ToString(dr["A073_porte"]);
                    if (dr["A048_cd_setor"] != DBNull.Value && dr["A048_cd_setor"] != DBNull.Value && dr["A048_cd_setor"].ToString() != "")
                        porteEmpresa.codSetorMercado = new SetorialMercado(Convert.ToString(dr["A048_cd_setor"]));
                    porteEmpresa.qtdEmpregadosDe = Convert.ToInt32(dr["A073_num_emp_de"]);
                    porteEmpresa.qtdEmpregadosAte = Convert.ToInt32(dr["A073_num_emp_ate"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        porteEmpresa.logusuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                porteEmpresa = new PorteEmpresa();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return porteEmpresa;
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
            new OracleParameter("curPorteEmpresa", OracleType.Cursor),
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
