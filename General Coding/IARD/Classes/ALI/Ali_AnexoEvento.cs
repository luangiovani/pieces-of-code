using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
using System.Data;
using System.Net.Mail;
using System.Net;

namespace Classes
{
    public class Ali_AnexoEvento // TB0659_ANEXOS_EVENTO
    {
        #region Atributos
        private int codAnexo;
        private string nome;
        private string url;
        private string tamanho;
        private DateTime logDtaInclusao;
        private Eventos evento;
        private Pessoa logCodPessoa;
        #endregion

        #region Propriedades
        public int CodAnexo
        {
            get { return codAnexo; }
            set { codAnexo = value; }
        }
        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }
        public string Url
        {
            get { return url; }
            set { url = value; }
        }
        public string Tamanho
        {
            get { return tamanho; }
            set { tamanho = value; }
        }
        public DateTime LogDtaInclusao
        {
            get { return logDtaInclusao; }
            set { logDtaInclusao = value; }
        }
        public Eventos Evento
        {
            get { return evento; }
            set { evento = value; }
        }
        public Pessoa LogCodPessoa
        {
            get { return logCodPessoa; }
            set { logCodPessoa = value; }
        }
        #endregion

        #region Construtores
        public Ali_AnexoEvento()
            : this(-1)
        { }
        public Ali_AnexoEvento(int codAnexo)
        {
            this.codAnexo = codAnexo;
            this.evento = new Eventos();
            this.logCodPessoa = new Pessoa();
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "ALI_AnexoEvento.AnexosEventoInc";
        private const string SPUPDATE = "ALI_AnexoEvento.AnexosEventoAlt";
        private const string SPDELETE = "ALI_AnexoEvento.AnexosEventoDel";
        private const string SPSELECTID = "ALI_AnexoEvento.AnexosEventoSelId";
        private const string SPSELECTPAG = "ALI_AnexoEvento.AnexosEventoSelPag";
        private const string SPSELECTEVENTO = "ALI_AnexoEvento.AnexosEventoSelEvento";
        #endregion

        #region Parametros Oracle
        private const string PARMCODIGO = "codAnexo";
        private const string PARMCURSOR = "curAnexo";
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
                            /*0*/ new OracleParameter(PARMCODIGO, OracleType.Int32, 4, ParameterDirection.InputOutput.ToString()),
                            /*1*/ new OracleParameter("nome", OracleType.VarChar),
                            /*2*/ new OracleParameter("url", OracleType.VarChar),
                            /*3*/ new OracleParameter("tamanho", OracleType.VarChar),
                            /*4*/ new OracleParameter("codEvento", OracleType.Int32),
                            /*5*/ new OracleParameter("codPessoa", OracleType.Int32)
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
            parms[0].Value = this.codAnexo;
            parms[1].Value = this.nome;
            parms[2].Value = this.url;
            parms[3].Value = this.tamanho;
            parms[4].Value = this.evento.CodEvento;
            parms[5].Value = this.logCodPessoa.CodPessoa;
            if (this.codAnexo < 0)
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
                        codAnexo = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
                codAnexo = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
            OracleParameter[] param = new OracleParameter[] { new OracleParameter(PARMCODIGO, OracleType.Int32, 4),
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
        /// <returns>Usuario</returns>
        public static Ali_AnexoEvento GetDataRow(int codigo)
        {
            OracleDataReader dr = LoadDataDr(codigo);
            Ali_AnexoEvento anexo = new Ali_AnexoEvento();
            try
            {
                if (dr.Read())
                {
                    anexo.codAnexo = Convert.ToInt32(dr["A0659_COD_ANEXO"]);
                    anexo.nome = dr["A0659_NM_ARQUIVO"].ToString();
                    anexo.url = dr["A0659_URL"].ToString();
                    anexo.tamanho = dr["A0659_TAMANHO"].ToString();
                    anexo.evento = new Eventos(Convert.ToInt32(dr["A022_CD_EV"]));
                    anexo.logCodPessoa = new Pessoa(Convert.ToInt32(dr["A572_cd_pes_atz"]));
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                anexo = new Ali_AnexoEvento();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return anexo;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>Usuario</returns>
        public static Ali_AnexoEvento GetDataRow(int codigo, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codigo, trans);
            Ali_AnexoEvento anexo = new Ali_AnexoEvento();
            try
            {
                if (dr.Read())
                {
                    anexo.codAnexo = Convert.ToInt32(dr["A0659_COD_ANEXO"]);
                    anexo.nome = dr["A0659_NM_ARQUIVO"].ToString();
                    anexo.url = dr["A0659_URL"].ToString();
                    anexo.tamanho = dr["A0659_TAMANHO"].ToString();
                    anexo.evento = new Eventos(Convert.ToInt32(dr["A022_CD_EV"]));
                    anexo.logCodPessoa = new Pessoa(Convert.ToInt32(dr["A572_cd_pes_atz"]));
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                anexo = new Ali_AnexoEvento();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return anexo;
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

        #region Métodos Específicos

        #region LoadDataDrEvento
        /// <summary>
        /// LoadDataDr
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDrEvento(int codEvento)
        {
            OracleParameter[] param = new OracleParameter[] { 

                            new OracleParameter("codEvento", OracleType.Int32, 4), 
                            new OracleParameter(PARMCURSOR, OracleType.Cursor)
                        };

            param[0].Value = codEvento;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTEVENTO, param);
            return dr;
        }
        #endregion

        #endregion
    }
}
