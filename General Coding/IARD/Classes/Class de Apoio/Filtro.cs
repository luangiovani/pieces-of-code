//-- Classe gerada  pelo ClassGen
//-- Data : 1/30/2007 9:44:11 AM
//-- Autor :  CIMS

using System;
using System.Collections;
using System.Data;
using System.Data.OracleClient;


namespace Classes
{
    public class Filtro // T001_filtro
    {
        #region Atributos
        private Int32 codFiltro;
        private string nomTela;
        private string nomAtributo;
        private string nomAlias;
        private TipoFiltro tipo;
        private DateTime logDtaCriacao;
        private DateTime logDtaEdicao;
        private string logUsuario;
        #endregion

        #region Propriedades

        #region codFiltro
        public Int32 CodFiltro
        {
            get { return codFiltro; }
            set { codFiltro = value; }
        }
        #endregion

        #region nomTela
        public string NomTela
        {
            get { return nomTela; }
            set { nomTela = value; }
        }
        #endregion

        #region nomAtributo
        public string NomAtributo
        {
            get { return nomAtributo; }
            set { nomAtributo = value; }
        }
        #endregion

        #region nomAlias
        public string NomAlias
        {
            get { return nomAlias; }
            set { nomAlias = value; }
        }
        #endregion

        #region tipo
        public TipoFiltro Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }
        #endregion

        #region logDtaCriacao
        public DateTime LogDtaCriacao
        {
            get { return logDtaCriacao; }
            set { logDtaCriacao = value; }
        }
        #endregion

        #region logDtaEdicao
        public DateTime LogDtaEdicao
        {
            get { return logDtaEdicao; }
            set { logDtaEdicao = value; }
        }
        #endregion

        #region logUsuario
        public string LogUsuario
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }
        #endregion

        #region TamanhoPagina
        public static Int32 TamanhoPagina
        {
            get { return Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["TamanhoPagina"]); }
        }
        #endregion

        #endregion

        #region Construtores
        public Filtro()
            : this(-1)
        { }
        public Filtro(Int32 codFiltro)
        {
            this.codFiltro = codFiltro;
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "Filtro.FiltroIns";
        private const string SPUPDATE = "Filtro.FiltroAlt";
        private const string SPDELETE = "Filtro.FiltroDel";
        private const string SPSELECT = "Filtro.FiltroSel";
        private const string SPSELECTID = "Filtro.FiltroSelId";
        private const string SPSELECTPAG = "Filtro.FiltroSelPag";
        private const string SPSELECTTELA = "Filtro.FiltroSelTela";
        #endregion

        #region Parametros Oracle
        private const string PARMCODIGO = "pcodFiltro";
        private const string PARMCURSOR = "curFiltro";
        #endregion

        #region Metodos

        #region GetParameters
        public static OracleParameter[] GetParameters()
        {
            OracleParameter[] parms;
            // Tentando buscar os parameters do cache
            parms = DataBase.GetCachedParameters(SPINSERT);
            if (parms == null)
            {
                parms = new OracleParameter[]{ 
				/*0*/ new OracleParameter(PARMCODIGO, OracleType.Int32, 4, ParameterDirection.InputOutput.ToString()) ,
				/*1*/ new OracleParameter( "pnomTela", OracleType.VarChar),
				/*2*/ new OracleParameter( "pnomAtributo", OracleType.VarChar),
				/*3*/ new OracleParameter( "pnomAlias", OracleType.VarChar),
				/*4*/ new OracleParameter( "ptipo", OracleType.VarChar, 1),
				/*5*/ new OracleParameter( "plogUsuario", OracleType.VarChar)	};

                // Criando cache dos parameters 
                DataBase.CacheParameters(SPINSERT, parms);
            }
            return (parms);
        }
        #endregion

        #region SetParameters
        public void SetParameters(OracleParameter[] parms)
        {
            parms[0].Value = this.codFiltro;
            parms[1].Value = this.nomTela;
            parms[2].Value = this.nomAtributo;
            parms[3].Value = this.nomAlias;
            parms[4].Value = this.tipo;
            parms[5].Value = this.logUsuario;
            if (this.codFiltro < 0)
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
                        codFiltro = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
                codFiltro = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
        public static void Delete(Int32 codigo)
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
        public static void Delete(Int32 codigo, OracleTransaction trans)
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
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr()
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter("sWhere", OracleType.VarChar,5000),
		        new OracleParameter("CurrentPage", OracleType.Int32),
		        new OracleParameter("PageSize", OracleType.Int32),
		        new OracleParameter("SortExpression", OracleType.VarChar,50),
                new OracleParameter("curFiltro", OracleType.Cursor),
                new OracleParameter("nRegistro", OracleType.Int32)
            };

            parms[0].Value = " ";
            parms[1].Value = 1;
            parms[2].Value = 10000;
            parms[3].Value = "codFiltro ASC";
            parms[4].Direction = ParameterDirection.Output;
            parms[5].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTPAG, parms);

            //OracleParameter[] parms = new OracleParameter[] { new OracleParameter(PARMCODIGO, OracleType.Int32, 4) };
            //parms[0].Value = null;
            //OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECT, parms);
            return dr;
        }

        /// <summary>
        /// LoadDataDr
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(Int32 codigo)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMCODIGO, OracleType.Int32, 4),
                new OracleParameter(PARMCURSOR, OracleType.Cursor)
            };
            parms[0].Value = codigo;
            parms[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, parms);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(Int32 codigo, OracleTransaction trans)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMCODIGO, OracleType.Int32, 4) ,
                new OracleParameter(PARMCURSOR, OracleType.Cursor)
            };
            parms[0].Value = codigo;
            parms[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, parms);
            return dr;
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
                new OracleParameter("curFiltro", OracleType.Cursor),
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

        #region LoadDataTela
        /// <summary>
        /// LoadDataTela
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataTela(String nomeTela)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter("pnomTela", OracleType.VarChar,30),
                new OracleParameter(PARMCURSOR, OracleType.Cursor)
            };
            parms[0].Value = nomeTela;
            parms[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTTELA, parms);
            return dr;
        }
        #endregion

        #region LoadDataDw

        /// <summary>
        /// LoadDataDw
        /// </summary>
        /// <returns>DataView</returns>
        public static DataView LoadDataDw()
        {
            return new DataView(DataBase.ExecuteReaderDs(CommandType.StoredProcedure, SPSELECT, null).Tables[0]);
        }

        /// <summary>
        /// LoadDataDw para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataView</returns>
        public static DataView LoadDataDw(OracleTransaction trans)
        {
            return new DataView(DataBase.ExecuteReaderDs(trans, CommandType.StoredProcedure, SPSELECT, null).Tables[0]);
        }
        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <returns>Filtro</returns>
        public static Filtro GetDataRow(Int32 codigo)
        {
            OracleDataReader dr = LoadDataDr(codigo);
            Filtro filtro = new Filtro();
            try
            {
                if (dr.Read())
                {
                    filtro.codFiltro = Convert.ToInt32(dr["A001_codFiltro"]);
                    filtro.nomTela = Convert.ToString(dr["A001_nomeTela"]);
                    filtro.nomAtributo = Convert.ToString(dr["A001_nomeAtributo"]);
                    filtro.nomAlias = Convert.ToString(dr["A001_nomeAlias"]);
                    filtro.tipo = (TipoFiltro)Convert.ToChar(dr["A001_tipo"]);
                    filtro.logDtaCriacao = Convert.ToDateTime(dr["A001_logDtaCriacao"]);
                    filtro.logDtaEdicao = Convert.ToDateTime(dr["A001_logDtaEdicao"]);
                    filtro.logUsuario = Convert.ToString(dr["logUsuario"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                filtro = new Filtro();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return filtro;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>Filtro</returns>
        public static Filtro GetDataRow(Int32 codigo, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codigo, trans);
            Filtro filtro = new Filtro();
            try
            {
                if (dr.Read())
                {
                    filtro.codFiltro = Convert.ToInt32(dr["A001_codFiltro"]);
                    filtro.nomTela = Convert.ToString(dr["A001_nomeTela"]);
                    filtro.nomAtributo = Convert.ToString(dr["A001_nomeAtributo"]);
                    filtro.nomAlias = Convert.ToString(dr["A001_nomeAlias"]);
                    filtro.tipo = (TipoFiltro)Convert.ToChar(dr["A001_tipo"]);
                    filtro.logDtaCriacao = Convert.ToDateTime(dr["A001_logDtaCriacao"]);
                    filtro.logDtaEdicao = Convert.ToDateTime(dr["A001_logDtaEdicao"]);
                    filtro.logUsuario = Convert.ToString(dr["logUsuario"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                filtro = new Filtro();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return filtro;
        }
        #endregion

        #endregion

        #region Metodos Especificos
        #region LoadDataDr
        /// <summary>
        /// LoadDataDr
        /// </summary>
        /// <param name="nomeTela">Nome da tela</param>
        /// <returns>SqlDataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(string nomeTela)
        {
            OracleParameter[] parms = new OracleParameter[] {
                new OracleParameter("pnomTela", OracleType.VarChar, 30),
                new OracleParameter(PARMCURSOR, OracleType.Cursor) 
            };
            parms[0].Value = nomeTela;
            parms[1].Direction = ParameterDirection.Output;
            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECT, parms);
            return dr;
        }
        #endregion

        #region LoadDataDw
        /// <summary>
        /// LoadDataView
        /// </summary>
        /// <param name="nomeTela">Nome da tela</param>
        /// <returns>DataView</returns>
        public static DataView LoadDataView(string nomeTela)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter("pnomTela", OracleType.VarChar), 
                new OracleParameter(PARMCURSOR, OracleType.Cursor) 
            };
            parms[0].Value = nomeTela.ToUpper();
            parms[1].Direction = ParameterDirection.Output;

            return new DataView(DataBase.ExecuteReaderDs(CommandType.StoredProcedure, SPSELECT, parms).Tables[0]);
        }
        #endregion
        #endregion

    }//classe
}//namespace