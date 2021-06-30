using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
using System.Data;

//-- Classe Classes Sebrae
//-- Data : 30/10/2007 
//-- Autor :  Honorato
//--

namespace Classes
{
    public class Grupo2213  // T2213_GRUPO     
    {
        #region Atributos
        private int codGrupo2213;
        private TituloEvento codTituloEv;
        private string tipCurso;
        private int vlrMin;
        private int vlrMax;
        private Grupo2201 codGrupo2201;
        private CondicaoPagtoEv codCondPagto;
        private ProdutoSebrae codProdSebrae;
        private Pessoa codUsuario;
        private Classifica codClassifica;
        private int indDesativado;
        private string logUsuario;

        #endregion

        #region Propriedades

        public int CodGrupo2213
        {
            get { return codGrupo2213; }
            set { codGrupo2213 = value; }
        }

        public TituloEvento CodTituloEv
        {
            get { return codTituloEv; }
            set { codTituloEv = value; }
        }

        public string TipCurso
        {
            get { return tipCurso; }
            set { tipCurso = value; }
        }

        public int VlrMin
        {
            get { return vlrMin; }
            set { vlrMin = value; }
        }

        public int VlrMax
        {
            get { return vlrMax; }
            set { vlrMax = value; }
        }

        public Grupo2201 CodGrupo2201
        {
            get { return codGrupo2201; }
            set { codGrupo2201 = value; }
        }

        public CondicaoPagtoEv CodCondPagto
        {
            get { return codCondPagto; }
            set { codCondPagto = value; }
        }

        public ProdutoSebrae CodProdSebrae
        {
            get { return codProdSebrae; }
            set { codProdSebrae = value; }
        }

        public Pessoa CodUsuario
        {
            get { return codUsuario; }
            set { codUsuario = value; }
        }

        public Classifica CodClassifica
        {
            get { return codClassifica; }
            set { codClassifica = value; }
        }

        public int IndDesativado
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
        public Grupo2213()
            : this(-1)
        { }
        public Grupo2213(int codGrupo2213)
        {
            this.codGrupo2213 = codGrupo2213;
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "Grupo2213.Grupo2213Inc";
        private const string SPUPDATE = "Grupo2213.Grupo2213Alt";
        private const string SPDELETE = "Grupo2213.Grupo2213Del";
        private const string SPSELECTID = "Grupo2213.Grupo2213SelId";
        private const string SPSELECTPAG = "Grupo2213.Grupo2213SelPag";
        private const string SPSELECT = "Grupo2213.Grupo2213Sel";
        #endregion

        #region Parametros Oracle
        private const string PARMCODIGO = "codGrupo2213";
        private const string PARMCURSOR = "curGrupo2213";
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
                    new OracleParameter( "codTituloEv", OracleType.Int32),
                    new OracleParameter( "tipCurso", OracleType.VarChar),
                    new OracleParameter( "vlrMin", OracleType.Int32),
                    new OracleParameter( "vlrMax", OracleType.Int32),
                    new OracleParameter( "codGrupo2201", OracleType.Int32),
                    new OracleParameter( "codCondPagto", OracleType.Int32),
                    new OracleParameter( "codProdSebrae", OracleType.VarChar),
                    new OracleParameter( "codUsuario", OracleType.Int32),
                    new OracleParameter( "codClassifica", OracleType.Int32),
                    new OracleParameter( "indDesativado", OracleType.Int32),
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
            parms[0].Value = this.codGrupo2213;
            parms[1].Value = this.codTituloEv.CodTituloEv;
            parms[2].Value = this.tipCurso;
            parms[3].Value = this.vlrMin;
            parms[4].Value = this.vlrMax;
            parms[5].Value = this.codGrupo2201.CodGrupo2201;
            parms[6].Value = this.codCondPagto.CodCondPagto;
            parms[7].Value = this.codProdSebrae.CodProdSebrae;
            parms[8].Value = this.codUsuario.CodPessoa;
            parms[9].Value = this.codClassifica.CodClassifica;
            parms[10].Value = this.indDesativado;
            parms[11].Value = this.logUsuario.ToUpper();
            if (this.codGrupo2213 < 0)
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
                        codGrupo2213 = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
                codGrupo2213 = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
        public static OracleDataReader LoadDataDr()
        {
            OracleParameter[] param = new OracleParameter[] { 
                new OracleParameter(PARMCURSOR, OracleType.Cursor)
            };

            param[0].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECT, param);
            return dr;
        }


        /// <summary>
        /// LoadDataDr
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
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
        /// <returns>Grupo2213</returns>
        public static Grupo2213 GetDataRow(int codigo)
        {
            OracleDataReader dr = LoadDataDr(codigo);
            Grupo2213 Grupo2213 = new Grupo2213();
            try
            {
                if (dr.Read())
                {
                    Grupo2213.codGrupo2213 = Convert.ToInt32(dr["A2213_cd_grupo"]);
                    Grupo2213.codTituloEv = new TituloEvento(Convert.ToInt32(dr["A008_cd_tit_ev"]));
                    Grupo2213.tipCurso = Convert.ToString(dr["A2213_tp_curso"]);
                    Grupo2213.vlrMin = Convert.ToInt32(dr["A2213_pr_min"]);
                    Grupo2213.vlrMax = Convert.ToInt32(dr["A2213_pr_max"]);
                    Grupo2213.codGrupo2201 = new Grupo2201(Convert.ToInt32(dr["A2201_cd_grupo"]));
                    Grupo2213.codCondPagto = new CondicaoPagtoEv(Convert.ToInt32(dr["A2204_cd_cond_pgto"]));
                    Grupo2213.codProdSebrae = new ProdutoSebrae(Convert.ToString(dr["A042_cd_prod_sev"]));
                    Grupo2213.codUsuario = new Pessoa(Convert.ToInt32(dr["A572_cd_pes"]));
                    Grupo2213.codClassifica = new Classifica(Convert.ToInt32(dr["A1150_cd_classifica"]));
                    if (dr["A2213_ind_des"] != DBNull.Value)
                    { Grupo2213.indDesativado = Convert.ToInt32(dr["A2213_ind_des"]); }
                    if ( dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        Grupo2213.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                Grupo2213 = new Grupo2213();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return Grupo2213;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>Grupo2213</returns>
        public static Grupo2213 GetDataRow(int codigo, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codigo, trans);
            Grupo2213 Grupo2213 = new Grupo2213();
            try
            {
                if (dr.Read())
                {
                    Grupo2213.codGrupo2213 = Convert.ToInt32(dr["A2213_cd_grupo"]);
                    Grupo2213.codTituloEv = new TituloEvento(Convert.ToInt32(dr["A008_cd_tit_ev"]));
                    Grupo2213.tipCurso = Convert.ToString(dr["A2213_tp_curso"]);
                    Grupo2213.vlrMin = Convert.ToInt32(dr["A2213_pr_min"]);
                    Grupo2213.vlrMax = Convert.ToInt32(dr["A2213_pr_max"]);
                    Grupo2213.codGrupo2201 = new Grupo2201(Convert.ToInt32(dr["A2201_cd_grupo"]));
                    Grupo2213.codCondPagto = new CondicaoPagtoEv(Convert.ToInt32(dr["A2204_cd_cond_pgto"]));
                    Grupo2213.codProdSebrae = new ProdutoSebrae(Convert.ToString(dr["A042_cd_prod_sev"]));
                    Grupo2213.codUsuario = new Pessoa(Convert.ToInt32(dr["A572_cd_pes"]));
                    Grupo2213.codClassifica = new Classifica(Convert.ToInt32(dr["A1150_cd_classifica"]));
                    if (dr["A2213_ind_des"] != DBNull.Value)
                    { Grupo2213.indDesativado = Convert.ToInt32(dr["A2213_ind_des"]); }
                    if ( dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        Grupo2213.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                Grupo2213 = new Grupo2213();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return Grupo2213;
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
        #endregion


        #endregion

    }
}