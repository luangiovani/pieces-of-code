using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
using System.Data;

//-- Classe Classes Sebrae
//-- Data : 24/08/2007 
//-- Autor :  Honorato

namespace Classes
{
    public class ProdutoSebrae  // T042_PROD_SEB e T920_PROD_PORT e T188_quest
    {
        #region Atributos
        private string codProdSebrae;
        private Pessoa codPessoa;
        private Usuario codUsuario;
        private string nomProduto;
        private string nomObjetivo;
        private string nomJustificativa;
        private string codSebraeNa;
        private string indAtivo;
        private int? codExtraOrcamento;
        private string dscCccontabil;
        private string codCccontabil;
        private int? indComercializa;
        private int? indFechado;
        private string logUsuario;
        #endregion

        #region Propriedades
        public string CodProdSebrae
        {
            get { return codProdSebrae; }
            set { codProdSebrae = value; }
        }
        public Pessoa CodPessoa
        {
            get { return codPessoa; }
            set { codPessoa = value; }
        }
        public Usuario CodUsuario
        {
            get { return codUsuario; }
            set { codUsuario = value; }
        }
        public string NomProduto
        {
            get { return nomProduto; }
            set { nomProduto = value; }
        }
        public string NomObjetivo
        {
            get { return nomObjetivo; }
            set { nomObjetivo = value; }
        }
        public string NomJustificativa
        {
            get { return nomJustificativa; }
            set { nomJustificativa = value; }
        }
        public string CodSebraeNa
        {
            get { return codSebraeNa; }
            set { codSebraeNa = value; }
        }
        public string IndAtivo
        {
            get { return indAtivo; }
            set { indAtivo = value; }
        }
        public int? CodExtraOrcamento
        {
            get { return codExtraOrcamento; }
            set { codExtraOrcamento = value; }
        }

        public string DscCccontabil
        {
            get { return dscCccontabil; }
            set { dscCccontabil = value; }
        }
        public string CodCccontabil
        {
            get { return codCccontabil; }
            set { codCccontabil = value; }
        }
        public int? IndComercializa
        {
            get { return indComercializa; }
            set { indComercializa = value; }
        }
        public int? IndFechado
        {
            get { return indFechado; }
            set { indFechado = value; }
        }
        public string LogUsuario
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }

        #endregion

        #region Construtores
        public ProdutoSebrae()
            : this("")
        { }
        public ProdutoSebrae(string codProdSebrae)
        {
            this.codProdSebrae = codProdSebrae;
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "ProdutoSebrae.ProdutoSebraeInc";
        private const string SPUPDATE = "ProdutoSebrae.ProdutoSebraeAlt";
        private const string SPDELETE = "ProdutoSebrae.ProdutoSebraeDel";
        private const string SPSELECTID = "ProdutoSebrae.ProdutoSebraeSelId";
        private const string SPSELECTPAG = "ProdutoSebrae.ProdutoSebraeSelPag";
        private const string SPSELECTPAGTIPOEVENTO = "ProdutoSebrae.TipoEVSelPag";
        private const string SPSELECTPAGPRODPORT = "ProdutoSebrae.PRODPORTSelPag";
        private const string SPSELECTPAGQUEST = "ProdutoSebrae.QUESTSelPag";
        
        #endregion

        #region Parametros Oracle
        private const string PARMCODIGO = "codProdSebrae";
        private const string PARMCURSOR = "curProdutoSebrae";
        #endregion

        #region Metodos

        #region GetParameters
        public static OracleParameter[] GetParameters()
        {
            OracleParameter[] parms;

            // Tentando buscar os parameters do cache        
            //parms = DataBase.GetCachedParameters(SPINSERT);
            parms = null;
            //parms = OutputCacheParameters(SPINSERT);
            if (parms == null)
            {
                parms = new OracleParameter[]{ 
                /*0*/ new OracleParameter(PARMCODIGO, OracleType.VarChar, 4, ParameterDirection.InputOutput.ToString()) ,
                /*1*/ new OracleParameter( "codPessoa", OracleType.Int32),
                /*2*/ new OracleParameter( "codUsuario", OracleType.Int32),
                /*3*/ new OracleParameter( "nomProduto", OracleType.VarChar),
                /*4*/ new OracleParameter( "nomObjetivo", OracleType.VarChar),
                /*5*/ new OracleParameter( "nomJustificativa", OracleType.VarChar),
                /*6*/ new OracleParameter( "codSebraeNa", OracleType.VarChar),
                /*7*/ new OracleParameter( "indAtivo", OracleType.VarChar),
                /*8*/ new OracleParameter( "codExtraOrcamento", OracleType.Int32),
                /*9*/ new OracleParameter( "dscCccontabil", OracleType.VarChar),
                /*10*/ new OracleParameter( "codCccontabil", OracleType.VarChar),
                /*11*/ new OracleParameter( "indComercializa", OracleType.Int32),
                /*12*/ new OracleParameter( "indFechado", OracleType.Int32),
                /*13*/ new OracleParameter( "logUsuario", OracleType.VarChar)
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
            parms[0].Value = this.codProdSebrae;
            parms[1].Value = this.codPessoa.CodPessoa;
            parms[2].Value = this.codUsuario.CodUsuario;
            parms[3].Value = this.nomProduto;
            parms[4].Value = this.nomObjetivo;
            parms[5].Value = this.nomJustificativa;
            parms[6].Value = this.codSebraeNa;
            parms[7].Value = this.indAtivo;
            parms[8].Value = this.codExtraOrcamento;
            parms[9].Value = this.dscCccontabil;
            parms[10].Value = this.codCccontabil;
            parms[11].Value = this.indComercializa;
            parms[12].Value = this.indFechado;
            parms[13].Value = this.logUsuario.ToUpper();
            if (this.codProdSebrae == "")
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
                        codProdSebrae = Convert.ToString(cmd.Parameters[PARMCODIGO].Value);
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
                codProdSebrae = Convert.ToString(cmd.Parameters[PARMCODIGO].Value);
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
        /// Delete sem tratamento de transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
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
        /// <param name="codigo">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(string codigo)
        {
            OracleParameter[] param = new OracleParameter[] { 

                new OracleParameter(PARMCODIGO, OracleType.VarChar,50), 
                new OracleParameter(PARMCURSOR, OracleType.Cursor)
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
            OracleParameter[] param = new OracleParameter[] { new OracleParameter(PARMCODIGO, OracleType.Int32, 2),
                                                              new OracleParameter(PARMCURSOR, OracleType.Cursor)
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
        /// <returns>ProdutoSebrae</returns>
        public static ProdutoSebrae GetDataRow(string codigo)
        {
            OracleDataReader dr = LoadDataDr(codigo);
            ProdutoSebrae ProdutoSebrae = new ProdutoSebrae();
            try
            {
                if (dr.Read())
                {
                    ProdutoSebrae.codProdSebrae = Convert.ToString(dr["A042_cd_prod_seb"]);
                    if (dr["A572_cd_pes"] != DBNull.Value)
                        ProdutoSebrae.codPessoa = new Pessoa(Convert.ToInt32(dr["A572_cd_pes"]));
                    if (dr["A052_cd_usuario"] != DBNull.Value)
                        ProdutoSebrae.codUsuario = new Usuario(Convert.ToInt32(dr["A052_cd_usuario"]));
                    ProdutoSebrae.nomProduto = Convert.ToString(dr["A042_nm_prod"]);
                    if (dr["A042_objetivo"] != DBNull.Value && dr["A042_objetivo"] != DBNull.Value && dr["A042_objetivo"].ToString() != "")
                        ProdutoSebrae.nomObjetivo = Convert.ToString(dr["A042_objetivo"]);
                    if (dr["A042_justificativa"] != DBNull.Value && dr["A042_justificativa"] != DBNull.Value && dr["A042_justificativa"].ToString() != "")
                        ProdutoSebrae.nomJustificativa = Convert.ToString(dr["A042_justificativa"]);
                    if (dr["A042_cd_sebraeNA"] != DBNull.Value && dr["A042_cd_sebraeNA"] != DBNull.Value && dr["A042_cd_sebraeNA"].ToString() != "")
                        ProdutoSebrae.codSebraeNa = Convert.ToString(dr["A042_cd_sebraeNA"]);
                    if (dr["A042_ativo"] != DBNull.Value && dr["A042_ativo"] != DBNull.Value && dr["A042_ativo"].ToString() != "")
                        ProdutoSebrae.indAtivo = Convert.ToString(dr["A042_ativo"]);
                    if (dr["A042_extra_orcamentario"] != DBNull.Value && dr["A042_extra_orcamentario"] != DBNull.Value && dr["A042_extra_orcamentario"].ToString() != "")
                        ProdutoSebrae.codExtraOrcamento = Convert.ToInt32(dr["A042_extra_orcamentario"]);
                    if (dr["A042_desc_cccontabil"] != DBNull.Value && dr["A042_desc_cccontabil"] != DBNull.Value && dr["A042_desc_cccontabil"].ToString() != "")
                        ProdutoSebrae.dscCccontabil = Convert.ToString(dr["A042_desc_cccontabil"]);
                    if (dr["A042_cod_cccontabil"] != DBNull.Value && dr["A042_cod_cccontabil"] != DBNull.Value && dr["A042_cod_cccontabil"].ToString() != "")
                        ProdutoSebrae.codCccontabil = Convert.ToString(dr["A042_cod_cccontabil"]);
                    if (dr["A042_ind_comercializa"] != DBNull.Value && dr["A042_ind_comercializa"] != DBNull.Value && dr["A042_ind_comercializa"].ToString() != "")
                        ProdutoSebrae.indComercializa = Convert.ToInt32(dr["A042_ind_comercializa"]);
                    if (dr["A042_ind_fechado"] != DBNull.Value && dr["A042_ind_fechado"] != DBNull.Value && dr["A042_ind_fechado"].ToString() != "")
                        ProdutoSebrae.indFechado = Convert.ToInt32(dr["A042_ind_fechado"]);
                    
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        ProdutoSebrae.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                ProdutoSebrae = new ProdutoSebrae();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return ProdutoSebrae;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>ProdutoSebrae</returns>
        public static ProdutoSebrae GetDataRow(int codigo, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codigo, trans);
            ProdutoSebrae ProdutoSebrae = new ProdutoSebrae();
            try
            {
                if (dr.Read())
                {
                    ProdutoSebrae.codProdSebrae = Convert.ToString(dr["A042_cd_prod_seb"]);
                    ProdutoSebrae.codPessoa = new Pessoa(Convert.ToInt32(dr["A572_cd_pes"]));
                    ProdutoSebrae.codUsuario = new Usuario(Convert.ToInt32(dr["A052_cd_usuario"]));
                    ProdutoSebrae.nomProduto = Convert.ToString(dr["A042_nm_prod"]);
                    if (dr["A042_objetivo"] != DBNull.Value && dr["A042_objetivo"] != DBNull.Value && dr["A042_objetivo"].ToString() != "")
                        ProdutoSebrae.nomObjetivo = Convert.ToString(dr["A042_objetivo"]);
                    if (dr["A042_justificativa"] != DBNull.Value && dr["A042_justificativa"] != DBNull.Value && dr["A042_justificativa"].ToString() != "")
                        ProdutoSebrae.nomJustificativa = Convert.ToString(dr["A042_justificativa"]);
                    if (dr["A042_cd_sebraeNA"] != DBNull.Value && dr["A042_cd_sebraeNA"] != DBNull.Value && dr["A042_cd_sebraeNA"].ToString() != "")
                        ProdutoSebrae.codSebraeNa = Convert.ToString(dr["A042_cd_sebraeNA"]);
                    if (dr["A042_ativo"] != DBNull.Value && dr["A042_ativo"] != DBNull.Value && dr["A042_ativo"].ToString() != "")
                        ProdutoSebrae.indAtivo = Convert.ToString(dr["A042_ativo"]);
                    if (dr["A042_extra_orcamentario"] != DBNull.Value && dr["A042_extra_orcamentario"] != DBNull.Value && dr["A042_extra_orcamentario"].ToString() != "")
                        ProdutoSebrae.codExtraOrcamento = Convert.ToInt32(dr["A042_extra_orcamentario"]);
                    if (dr["A042_desc_cccontabil"] != DBNull.Value && dr["A042_desc_cccontabil"] != DBNull.Value && dr["A042_desc_cccontabil"].ToString() != "")
                        ProdutoSebrae.dscCccontabil = Convert.ToString(dr["A042_desc_cccontabil"]);
                    if (dr["A042_cod_cccontabil"] != DBNull.Value && dr["A042_cod_cccontabil"] != DBNull.Value && dr["A042_cod_cccontabil"].ToString() != "")
                        ProdutoSebrae.codCccontabil = Convert.ToString(dr["A042_cod_cccontabil"]);
                    if (dr["A042_ind_comercializa"] != DBNull.Value && dr["A042_ind_comercializa"] != DBNull.Value && dr["A042_ind_comercializa"].ToString() != "")
                        ProdutoSebrae.indComercializa = Convert.ToInt32(dr["A042_ind_comercializa"]);
                    if (dr["A042_ind_fechado"] != DBNull.Value && dr["A042_ind_fechado"] != DBNull.Value && dr["A042_ind_fechado"].ToString() != "")
                        ProdutoSebrae.indFechado = Convert.ToInt32(dr["A042_ind_fechado"]);

                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        ProdutoSebrae.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                ProdutoSebrae = new ProdutoSebrae();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return ProdutoSebrae;
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

            // dr.Read();

            paginacao.DataReader = dr;
            //  traz o total de registros de retorno direto dos parametros do ExecuteReader---
            paginacao.TotalRegistros = Convert.ToInt32(parms[5].Value);
            return paginacao;
        }

        public static Paginacao LoadDataPaginacaoTipoEvento(string Where, int PaginaCorrente, int TamanhoPagina, string ExpressaoOrdenacao)
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


            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTPAGTIPOEVENTO, parms);

            // dr.Read();

            paginacao.DataReader = dr;
            //  traz o total de registros de retorno direto dos parametros do ExecuteReader---
            paginacao.TotalRegistros = Convert.ToInt32(parms[5].Value);
            return paginacao;
        }

        public static Paginacao LoadDataPaginacaoProdutoPortf(string Where, int PaginaCorrente, int TamanhoPagina, string ExpressaoOrdenacao)
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


            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTPAGPRODPORT, parms);

            // dr.Read();

            paginacao.DataReader = dr;
            //  traz o total de registros de retorno direto dos parametros do ExecuteReader---
            paginacao.TotalRegistros = Convert.ToInt32(parms[5].Value);
            return paginacao;
        }

        public static Paginacao LoadDataPaginacaoQuest(string Where, int PaginaCorrente, int TamanhoPagina, string ExpressaoOrdenacao)
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


            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTPAGQUEST, parms);

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