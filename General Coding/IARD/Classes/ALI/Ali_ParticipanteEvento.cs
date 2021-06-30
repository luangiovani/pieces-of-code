using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
using System.Data;
using System.Net.Mail;
using System.Net;

namespace Classes
{
    public class Ali_ParticipanteEvento // TB0087_PARTICIPANTE_EVENTO
    {
        #region Atributos
        private int codParticipanteEvento;
        private Eventos evento;
        private Pessoa pessoaAli;
        private ContatoCliente contatoCliente;
        private string justificativa;
        private DateTime logDtaInclusao;
        private DateTime logDtaAlteracao;
        #endregion

        #region Propriedades
        public int CodParticipanteEvento
        {
            get { return codParticipanteEvento; }
            set { codParticipanteEvento = value; }
        }
        public Eventos Evento
        {
            get { return evento; }
            set { evento = value; }
        }
        public Pessoa PessoaAli
        {
            get { return pessoaAli; }
            set { pessoaAli = value; }
        }
        public ContatoCliente ContatoCliente
        {
            get { return contatoCliente; }
            set { contatoCliente = value; }
        }
        public string Justificativa
        {
            get { return justificativa; }
            set { justificativa = value; }
        }
        public DateTime LogDtaInclusao
        {
            get { return logDtaInclusao; }
            set { logDtaInclusao = value; }
        }
        public DateTime LogDtaAlteracao
        {
            get { return logDtaAlteracao; }
            set { logDtaAlteracao = value; }
        }
        #endregion

        #region Construtores
        public Ali_ParticipanteEvento()
            : this(-1)
        { }
        public Ali_ParticipanteEvento(int codParticipanteEvento)
        {
            this.codParticipanteEvento = codParticipanteEvento;
            this.evento = new Eventos();
            this.pessoaAli = new Pessoa();
            this.contatoCliente = new ContatoCliente();
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "ALI_PARTICIPANTEEVENTO.ParticipanteEventoInc";
        private const string SPUPDATE = "ALI_PARTICIPANTEEVENTO.ParticipanteEventoAlt";
        private const string SPDELETE = "ALI_PARTICIPANTEEVENTO.ParticipanteEventoDel";
        private const string SPSELECTID = "ALI_PARTICIPANTEEVENTO.ParticipanteEventoSelId";
        private const string SPSELECTPAG = "ALI_PARTICIPANTEEVENTO.ParticipanteEventoSelPag";
        private const string SPSELECTCONTATO = "ALI_PARTICIPANTEEVENTO.ParticipanteEventoSelContato";
        private const string SPDELETECONTATO = "ALI_PARTICIPANTEEVENTO.ParticipanteEventoDelContato";
        #endregion

        #region Parametros Oracle
        private const string PARMCODIGO = "codParticipanteEvento";
        private const string PARMCURSOR = "curParticipanteEvento";
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
                            /*1*/ new OracleParameter("codEvento", OracleType.Int32),
                            /*2*/ new OracleParameter("codPessoaAli", OracleType.Int32),
                            /*3*/ new OracleParameter("numContato", OracleType.Int32),
                            /*4*/ new OracleParameter("codCliente", OracleType.Int32),
                            /*5*/ new OracleParameter("justificativa", OracleType.VarChar)
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
            parms[0].Value = this.codParticipanteEvento;
            parms[1].Value = this.evento.CodEvento;
            parms[2].Value = this.pessoaAli.CodPessoa;
            parms[3].Value = this.contatoCliente.NumContato;
            parms[4].Value = this.contatoCliente.CodCliente.CodCLIENTE;
            parms[5].Value = this.justificativa;
            if (this.codParticipanteEvento < 0)
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
                        codParticipanteEvento = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
                codParticipanteEvento = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
        public static Ali_ParticipanteEvento GetDataRow(int codigo)
        {
            OracleDataReader dr = LoadDataDr(codigo);
            Ali_ParticipanteEvento participante = new Ali_ParticipanteEvento();
            try
            {
                if (dr.Read())
                {
                    participante.codParticipanteEvento = Convert.ToInt32(dr["A0087_COD_PARTIC"]);
                    participante.evento = new Eventos(Convert.ToInt32(dr["A022_CD_EV"]));
                    participante.pessoaAli = new Pessoa(Convert.ToInt32(dr["A572_cd_pes_ali"]));
                    participante.contatoCliente = new ContatoCliente(Convert.ToInt32(dr["A014_NUM_CONT"]));
                    participante.contatoCliente.CodCliente = new Cliente(Convert.ToInt32(dr["A012_CD_CLI"]));
                    participante.justificativa = dr["A0087_JUSTIF_GRAT"].ToString();
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                participante = new Ali_ParticipanteEvento();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return participante;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>Usuario</returns>
        public static Ali_ParticipanteEvento GetDataRow(int codigo, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codigo, trans);
            Ali_ParticipanteEvento participante = new Ali_ParticipanteEvento();
            try
            {
                if (dr.Read())
                {
                    participante.codParticipanteEvento = Convert.ToInt32(dr["A0087_COD_PARTIC"]);
                    participante.evento = new Eventos(Convert.ToInt32(dr["A022_CD_EV"]));
                    participante.pessoaAli = new Pessoa(Convert.ToInt32(dr["A572_cd_pes_ali"]));
                    participante.contatoCliente = new ContatoCliente(Convert.ToInt32(dr["A014_NUM_CONT"]));
                    participante.contatoCliente.CodCliente = new Cliente(Convert.ToInt32(dr["A012_CD_CLI"]));
                    participante.justificativa = dr["A0087_JUSTIF_GRAT"].ToString();
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                participante = new Ali_ParticipanteEvento();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return participante;
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

        #region LoadDataDrContato

        /// <summary>
        /// LoadDataDr
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDrContato(int codEvento, int codAli)
        {
            OracleParameter[] param = new OracleParameter[] { 

                            new OracleParameter("codEvento", OracleType.Int32, 4), 
                            new OracleParameter("codAli", OracleType.Int32, 4), 
                            new OracleParameter(PARMCURSOR, OracleType.Cursor)
                        };

            param[0].Value = codEvento;
            param[1].Value = codAli;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTCONTATO, param);
            return dr;
        }
        #endregion

        #region DeleteContato
        /// <summary>
        /// Delete com tratamento de transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        public static void Delete(int codEvento, int codCliente, int numContato)
        {
            OracleParameter[] parms = new OracleParameter[] { new OracleParameter("codEvento", OracleType.Int32),
                                                              new OracleParameter("codCliente", OracleType.Int32),
                                                              new OracleParameter("numContato", OracleType.Int32)};
            parms[0].Value = codEvento;
            parms[1].Value = codCliente;
            parms[2].Value = numContato;
            using (OracleConnection conn = new OracleConnection(DataBase.CONN_STRING))
            {
                conn.Open();
                using (OracleTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        DataBase.ExecuteNonQuery(trans, CommandType.StoredProcedure, SPDELETECONTATO, parms);
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
        #endregion

        #endregion
    }
}
