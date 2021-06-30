using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
using System.Data;

//-- Classe Classes Sebrae
//-- Data : 11/07/2007 
//-- Autor :  Honorato

namespace Classes
{
    public class GrupoCredenciamentoEv  // T914_GRUPO_CREDENC 
    {
        #region Atributos
        private int codGrpCredenc;
        private EventoCredenciamento codEvento;
        private string nomGrpCredenc;
        private string nomContato;
        private string numFone;
        private string numFax;
        private string nomEmail;
        private Cidade codPais;
        private Cidade codEstado;
        private Cidade codCidade;
        private string logUsuario;

        #endregion

        #region Propriedades
        public int CodGrpCredenc
        {
            get { return codGrpCredenc; }
            set { codGrpCredenc = value; }
        }
        public EventoCredenciamento CodEvento
        {
            get { return codEvento; }
            set { codEvento = value; }
        }
        public string NomGrpCredenc
        {
            get { return nomGrpCredenc; }
            set { nomGrpCredenc = value; }
        }
        public string NomContato
        {
            get { return nomContato; }
            set { nomContato = value; }
        }
        public string NumFone
        {
            get { return numFone; }
            set { numFone = value; }
        }
        public string NumFax
        {
            get { return numFax; }
            set { numFax = value; }
        }
        public string NomEmail
        {
            get { return nomEmail; }
            set { nomEmail = value; }
        }
        public Cidade CodPais
        {
            get { return codPais; }
            set { codPais = value; }
        }
        public Cidade CodEstado
        {
            get { return codEstado; }
            set { codEstado = value; }
        }
        public Cidade CodCidade
        {
            get { return codCidade; }
            set { codCidade = value; }
        }
        public string LogUsuario
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }

        #endregion

        #region Construtores
        public GrupoCredenciamentoEv()
            : this(-1)
        { }
        public GrupoCredenciamentoEv(int codGrpCredenc)
        {
            this.codGrpCredenc = codGrpCredenc;
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "GrupoCredenciamentoEv.GrupoCredenciamentoEvInc";
        private const string SPUPDATE = "GrupoCredenciamentoEv.GrupoCredenciamentoEvAlt";
        private const string SPDELETE = "GrupoCredenciamentoEv.GrupoCredenciamentoEvDel";
        private const string SPSELECTID = "GrupoCredenciamentoEv.GrupoCredenciamentoEvSelId";
        private const string SPSELECTPAG = "GrupoCredenciamentoEv.GrupoCredenciamentoEvSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMCODIGO = "codGrpCredenc";
        private const string PARMCURSOR = "curGrupoCredenciamentoEv";
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
                /*1*/ new OracleParameter( "codEvento", OracleType.Int32),
                /*2*/ new OracleParameter( "nomGrpCredenc", OracleType.VarChar),
                /*3*/ new OracleParameter( "nomContato", OracleType.VarChar),
                /*4*/ new OracleParameter( "numFone", OracleType.VarChar),
                /*5*/ new OracleParameter( "numFax", OracleType.VarChar),
                /*6*/ new OracleParameter( "nomEmail", OracleType.VarChar),
                /*7*/ new OracleParameter( "codPais", OracleType.Int32),
                /*8*/ new OracleParameter( "codEstado", OracleType.Int32),
                /*9*/ new OracleParameter( "codCidade", OracleType.Int32),
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
            parms[0].Value = this.codGrpCredenc;
            parms[1].Value = this.CodEvento.CodEvento;
            parms[2].Value = this.nomGrpCredenc.ToUpper();
            parms[3].Value = this.nomContato.ToUpper();
            parms[4].Value = this.numFone;
            parms[5].Value = this.numFax;
            parms[6].Value = this.nomEmail;
            parms[7].Value = DBNull.Value;
            parms[8].Value = DBNull.Value;
            parms[9].Value = DBNull.Value;
            if (codCidade != null)
            {
                parms[7].Value = this.codCidade.CodPais.CodPais;
                parms[8].Value = this.codCidade.CodEstado.CodEstado;
                parms[9].Value = this.codCidade.CodCidade;
            }
            parms[10].Value = this.logUsuario.ToUpper();
            if (this.codGrpCredenc < 0)
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
                        codGrpCredenc = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
                codGrpCredenc = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
        /// <returns>GrupoCredenciamentoEv</returns>
        public static GrupoCredenciamentoEv GetDataRow(int codigo)
        {
            OracleDataReader dr = LoadDataDr(codigo);
            GrupoCredenciamentoEv GrupoCredenciamentoEv = new GrupoCredenciamentoEv();
            try
            {
                if (dr.Read())
                {
                    GrupoCredenciamentoEv.codGrpCredenc = Convert.ToInt32(dr["A914_cod_grp_credenc"]);
                    GrupoCredenciamentoEv.codEvento = new EventoCredenciamento(Convert.ToInt32(dr["A022_cd_ev"]));
                    if (dr["A914_nom_grp_credenc"] != DBNull.Value && dr["A914_nom_grp_credenc"] != DBNull.Value && dr["A914_nom_grp_credenc"].ToString() != "")
                        GrupoCredenciamentoEv.nomGrpCredenc = Convert.ToString(dr["A914_nom_grp_credenc"]);
                    if (dr["A914_nom_contato"] != DBNull.Value && dr["A914_nom_contato"] != DBNull.Value && dr["A914_nom_contato"].ToString() != "")
                        GrupoCredenciamentoEv.nomContato = Convert.ToString(dr["A914_nom_contato"]);
                    if (dr["A914_num_fone"] != DBNull.Value && dr["A914_num_fone"] != DBNull.Value && dr["A914_num_fone"].ToString() != "")
                        GrupoCredenciamentoEv.numFone = Convert.ToString(dr["A914_num_fone"]);
                    if (dr["A914_num_fax"] != DBNull.Value && dr["A914_num_fax"] != DBNull.Value && dr["A914_num_fax"].ToString() != "")
                        GrupoCredenciamentoEv.numFax = Convert.ToString(dr["A914_num_fax"]);
                    if (dr["A914_email"] != DBNull.Value && dr["A914_email"] != DBNull.Value && dr["A914_email"].ToString() != "")
                        GrupoCredenciamentoEv.nomEmail = Convert.ToString(dr["A914_email"]);
                    if (dr["A011_cd_cid"] != DBNull.Value && dr["A011_cd_cid"] != DBNull.Value && dr["A011_cd_cid"].ToString() != "")
                        GrupoCredenciamentoEv.codCidade = new Cidade(Convert.ToInt32(dr["A011_cd_cid"]));
                    if (dr["A021_cd_est"] != DBNull.Value && dr["A021_cd_est"] != DBNull.Value && dr["A021_cd_est"].ToString() != "")
                        GrupoCredenciamentoEv.codCidade.CodEstado.CodEstado = Convert.ToInt32(dr["A021_cd_est"]);
                    if (dr["A035_cd_pais"] != DBNull.Value && dr["A035_cd_pais"] != DBNull.Value && dr["A035_cd_pais"].ToString() != "")
                        GrupoCredenciamentoEv.codCidade.CodPais.CodPais = Convert.ToInt32(dr["A035_cd_pais"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        GrupoCredenciamentoEv.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                GrupoCredenciamentoEv = new GrupoCredenciamentoEv();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return GrupoCredenciamentoEv;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>GrupoCredenciamentoEv</returns>
        public static GrupoCredenciamentoEv GetDataRow(int codigo, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codigo, trans);
            GrupoCredenciamentoEv GrupoCredenciamentoEv = new GrupoCredenciamentoEv();
            try
            {
                if (dr.Read())
                {
                    GrupoCredenciamentoEv.codGrpCredenc = Convert.ToInt32(dr["A914_cod_grp_credenc"]);
                    GrupoCredenciamentoEv.codEvento = new EventoCredenciamento(Convert.ToInt32(dr["A022_cd_ev"]));
                    if (dr["A914_nom_grp_credenc"] != DBNull.Value && dr["A914_nom_grp_credenc"] != DBNull.Value && dr["A914_nom_grp_credenc"].ToString() != "")
                        GrupoCredenciamentoEv.nomGrpCredenc = Convert.ToString(dr["A914_nom_grp_credenc"]);
                    if (dr["A914_nom_contato"] != DBNull.Value && dr["A914_nom_contato"] != DBNull.Value && dr["A914_nom_contato"].ToString() != "")
                        GrupoCredenciamentoEv.nomContato = Convert.ToString(dr["A914_nom_contato"]);
                    if (dr["A914_num_fone"] != DBNull.Value && dr["A914_num_fone"] != DBNull.Value && dr["A914_num_fone"].ToString() != "")
                        GrupoCredenciamentoEv.numFone = Convert.ToString(dr["A914_num_fone"]);
                    if (dr["A914_num_fax"] != DBNull.Value && dr["A914_num_fax"] != DBNull.Value && dr["A914_num_fax"].ToString() != "")
                        GrupoCredenciamentoEv.numFax = Convert.ToString(dr["A914_num_fax"]);
                    if (dr["A914_email"] != DBNull.Value && dr["A914_email"] != DBNull.Value && dr["A914_email"].ToString() != "")
                        GrupoCredenciamentoEv.nomEmail = Convert.ToString(dr["A914_email"]);
                    if (dr["A011_cd_cid"] != DBNull.Value && dr["A011_cd_cid"] != DBNull.Value && dr["A011_cd_cid"].ToString() != "")
                        GrupoCredenciamentoEv.codCidade = new Cidade(Convert.ToInt32(dr["A011_cd_cid"]));
                    if (dr["A021_cd_est"] != DBNull.Value && dr["A021_cd_est"] != DBNull.Value && dr["A021_cd_est"].ToString() != "")
                        GrupoCredenciamentoEv.codCidade.CodEstado.CodEstado = Convert.ToInt32(dr["A021_cd_est"]);
                    if (dr["A035_cd_pais"] != DBNull.Value && dr["A035_cd_pais"] != DBNull.Value && dr["A035_cd_pais"].ToString() != "")
                        GrupoCredenciamentoEv.codCidade.CodPais.CodPais = Convert.ToInt32(dr["A035_cd_pais"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        GrupoCredenciamentoEv.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                GrupoCredenciamentoEv = new GrupoCredenciamentoEv();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return GrupoCredenciamentoEv;
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