using System;
using System.Data.OracleClient;
using System.Data;

namespace Database.Entities
{
    /// <summary>
    /// Classe de Mapeamento com a Entidade de Banco de Dados - T082_PalavraTituloEvento e T081
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
    public class PalavraTituloEvento
    {
        #region Atributos
        private int codPalavra;
        private int codTituloEv;
        private string logUsuario;
        #endregion

        #region Propriedades
        public int CdPalavra
        {
            get { return codPalavra; }
            set { codPalavra = value; }
        }
        public int CodTituloEv
        {
            get { return codTituloEv; }
            set { codTituloEv = value; }
        }
        public string LogUsuario
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }
        #endregion

        #region Construtores
        public PalavraTituloEvento()
            : this(-1)
        { }
        public PalavraTituloEvento(int codPalavra)
        {
            this.codPalavra = codPalavra;
            //this.codTituloEv = codTituloEv;
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "PalavraTituloEvento.PalavraTituloEventoInc";
        private const string SPUPDATE = "PalavraTituloEvento.PalavraTituloEventoAlt";
        private const string SPDELETE = "PalavraTituloEvento.PalavraTituloEventoDel";
        private const string SPSELECTID = "PalavraTituloEvento.PalavraTituloEventoSelId";
        private const string SPSELECTPAG = "PalavraTituloEvento.PalavraTituloEventoSelPag";
        private const string SPSELECT = "PalavraTituloEvento.PalavraTituloEventoSel";

        #endregion

        #region Parametros Oracle
        private const string PARMCODPALAVRA = "codPalavra";
        private const string PARMCODTITULOEV = "codTituloEv";
        private const string PARMCURSOR = "curPalavra";
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
                    /*0*/ new OracleParameter(PARMCODPALAVRA, OracleType.Int32, 4, ParameterDirection.InputOutput.ToString()) ,
                    /*1*/ new OracleParameter(PARMCODTITULOEV, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*2*/ new OracleParameter( "logUsuario", OracleType.VarChar)
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
            parms[0].Value = this.codPalavra;
            parms[1].Value = this.codTituloEv;
            parms[2].Value = this.logUsuario;
            if (this.codPalavra < 0)
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

            using (OracleConnection conn = new OracleConnection(Context.DataBase.CONN_STRING))
            {
                conn.Open();
                using (OracleTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        OracleCommand cmd = Context.DataBase.ExecuteNonQueryCmd(trans, CommandType.StoredProcedure, SPINSERT, parms);
                        //Obtendo a chave de identificação do registro inserido.
                        codPalavra = Convert.ToInt32(cmd.Parameters[PARMCODPALAVRA].Value);
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
                codPalavra = Convert.ToInt32(cmd.Parameters[PARMCODPALAVRA].Value);
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
        /// <param name="codpalavra">Código do Registro</param>
        public static void Delete(int codpalavra, int codTituloEv)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMCODPALAVRA, OracleType.Int32, 4)  ,
                new OracleParameter(PARMCODTITULOEV, OracleType.Int32, 4)
            };
            parms[0].Value = codpalavra;
            parms[1].Value = codTituloEv;
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
        /// <param name="codpalavra">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(int codpalavra, int codTituloEv, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMCODPALAVRA, OracleType.Int32, 4)  ,
                new OracleParameter(PARMCODTITULOEV, OracleType.Int32, 4)
            };
                parms[0].Value = codpalavra;
                parms[1].Value = codTituloEv;
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
        /// <param name="codPalavra">Codigo do Registro</param>
        /// <param name="codTituloEv">Codigo do Titulo Evento atual</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr( int codPalavra,   int codTituloEv)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCODPALAVRA, OracleType.Int32, 4), 
                    new OracleParameter(PARMCODTITULOEV, OracleType.Int32, 4),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codPalavra;
            param[1].Value = codTituloEv;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="codPalavra"></param>
        /// 
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr( int codPalavra,   int codTituloEv, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCODPALAVRA, OracleType.Int32, 4), 
                    new OracleParameter(PARMCODTITULOEV, OracleType.Int32, 4),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codPalavra;
            param[1].Value = codTituloEv;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="codPalavra">Codigo do Registro</param>
        /// <param name="codTituloEv">Codigo do Titulo Evento atual</param>
        /// <returns>PalavraTituloEvento</returns>
        public static PalavraTituloEvento GetDataRow(int codPalavra, int codTituloEv)
        {
            OracleDataReader dr = LoadDataDr(codPalavra, codTituloEv);
            PalavraTituloEvento palavraTitEv = new PalavraTituloEvento();
            try
            {
                if (dr.Read())
                {
                    palavraTitEv.codPalavra = Convert.ToInt32(dr["A081_cd_palavra"]);
                    palavraTitEv.codTituloEv = Convert.ToInt32(dr["A008_cd_tit_ev"]);
                    palavraTitEv.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                palavraTitEv = new PalavraTituloEvento();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return palavraTitEv;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codPalavra">Codigo do Registro</param>
        /// <param name="codTituloEv">Codigo do Titulo Evento atual</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>PalavraTituloEvento</returns>
        public static PalavraTituloEvento GetDataRow( int codPalavra,   int codTituloEv, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codPalavra, codTituloEv, trans);
            PalavraTituloEvento palavraTitEv = new PalavraTituloEvento();
            try
            {
                if (dr.Read())
                {
                    palavraTitEv.codPalavra = Convert.ToInt32(dr["A081_cd_palavra"]);
                    palavraTitEv.codTituloEv = Convert.ToInt32(dr["A008_cd_tit_ev"]);
                    palavraTitEv.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                palavraTitEv = new PalavraTituloEvento();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return palavraTitEv;
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
                new OracleParameter(PARMCURSOR, OracleType.Cursor),
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

        #region Métodos Específicos

        #region LoadDataDr


        /// <summary>
        /// LoadDataDr
        /// </summary>
        /// <param name="codPalavra">codpalavra do Registro</param>
        /// <param name="codTituloEv">codpalavra do Titulo Evento atual</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDrCodPalavra(int codTituloEv, Int16 Selecionados)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCODTITULOEV, OracleType.Int32, 4), 
                    new OracleParameter("selecionado", OracleType.UInt16),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codTituloEv;
            param[1].Value = Selecionados;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECT, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="codPalavra"></param>
        /// 
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDrCodPalavra(int codTituloEv, bool Selecionados, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCODTITULOEV, OracleType.Int32, 4), 
                    new OracleParameter("selecionados", OracleType.Byte, 1),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codTituloEv;
            param[1].Value = Selecionados;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECT, param);
            return dr;
        }

        #endregion

        #endregion

    }
}