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
    public class Grupo2201  // T2201_GRUPO    
    {
        #region Atributos
        private int codGrupo2201;
        private Pessoa codPessoa;
        private int codDiaEncerra;
        private string indParticipa;
        private int codNrParticipa;
        private int dscAcresParticipa;
        private int codMinParticipa;
        private string dscDsGrupo;
        private int codIdTipoEv;
        private string logUsuario;

        #endregion

        #region Propriedades
        public int CodGrupo2201
        {
            get { return codGrupo2201; }
            set { codGrupo2201 = value; }
        }
        public Pessoa CodPessoa
        {
            get { return codPessoa; }
            set { codPessoa = value; }
        }
        public int CodDiaEncerra
        {
            get { return codDiaEncerra; }
            set { codDiaEncerra = value; }
        }
        public string IndParticipa
        {
            get { return indParticipa; }
            set { indParticipa = value; }
        }
        public int CodNrParticipa
        {
            get { return codNrParticipa; }
            set { codNrParticipa = value; }
        }
        public int DscAcresParticipa
        {
            get { return dscAcresParticipa; }
            set { dscAcresParticipa = value; }
        }
        public int CodMinParticipa
        {
            get { return codMinParticipa; }
            set { codMinParticipa = value; }
        }
        public string DscDsGrupo
        {
            get { return dscDsGrupo; }
            set { dscDsGrupo = value; }
        }
        public int CodIdTipoEv
        {
            get { return codIdTipoEv; }
            set { codIdTipoEv = value; }
        }
        public string LogUsuario
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }

        #endregion

        #region Construtores
        public Grupo2201()
            : this(-1)
        { }
        public Grupo2201(int codGrupo2201)
        {
            this.codGrupo2201 = codGrupo2201;
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "Grupo2201.Grupo2201Inc";
        private const string SPUPDATE = "Grupo2201.Grupo2201Alt";
        private const string SPDELETE = "Grupo2201.Grupo2201Del";
        private const string SPSELECTID = "Grupo2201.Grupo2201SelId";
        private const string SPSELECTPAG = "Grupo2201.Grupo2201SelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMCODIGO = "codGrupo2201";
        private const string PARMCURSOR = "curGrupo2201";
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
                    new OracleParameter( "codPessoa", OracleType.Int32),
                    new OracleParameter( "codDiaEncerra", OracleType.Int32),
                    new OracleParameter( "indParticipa", OracleType.VarChar),
                    new OracleParameter( "codNrParticipa", OracleType.Int32),
                    new OracleParameter( "dscAcresParticipa", OracleType.Int32),
                    new OracleParameter( "codMinParticipa", OracleType.Int32),
                    new OracleParameter( "dscDsGrupo", OracleType.VarChar),
                    new OracleParameter( "codIdTipoEv", OracleType.Int32),
                /*9*/ new OracleParameter( "logUsuario", OracleType.VarChar)
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
            parms[0].Value = this.codGrupo2201;
            parms[1].Value = this.codPessoa.CodPessoa;
            parms[2].Value = this.codDiaEncerra;
            parms[3].Value = this.indParticipa.ToUpper();
            parms[4].Value = this.codNrParticipa;
            parms[5].Value = this.dscAcresParticipa;
            parms[6].Value = this.codMinParticipa;
            parms[7].Value = this.dscDsGrupo.ToUpper();
            parms[8].Value = this.codIdTipoEv;
            parms[9].Value = this.logUsuario.ToUpper();
            if (this.codGrupo2201 < 0)
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
                        codGrupo2201 = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
                codGrupo2201 = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
        /// <returns>Grupo2201</returns>
        public static Grupo2201 GetDataRow(int codigo)
        {
            OracleDataReader dr = LoadDataDr(codigo);
            Grupo2201 Grupo2201 = new Grupo2201();
            try
            {
                if (dr.Read())
                {
                    Grupo2201.codGrupo2201 = Convert.ToInt32(dr["A2201_cd_grupo"]);
                    Grupo2201.codPessoa = new Pessoa(Convert.ToInt32(dr["A052_cd_usuario"]));
                    Grupo2201.codDiaEncerra = Convert.ToInt32(dr["A2201_dia_encerra"]);
                    Grupo2201.indParticipa = Convert.ToString(dr["A2201_ind_participa"]);
                    Grupo2201.codNrParticipa = Convert.ToInt32(dr["A2201_nr_participa"]);
                    Grupo2201.dscAcresParticipa = Convert.ToInt32(dr["A2201_acres_participa"]);
                    Grupo2201.codMinParticipa = Convert.ToInt32(dr["A2201_min_participa"]);
                    Grupo2201.dscDsGrupo = Convert.ToString(dr["A2201_ds_grupo"]);
                    Grupo2201.codIdTipoEv = Convert.ToInt32(dr["A2201_ID_TIPO_EV"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        Grupo2201.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                Grupo2201 = new Grupo2201();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return Grupo2201;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>Grupo2201</returns>
        public static Grupo2201 GetDataRow(int codigo, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codigo, trans);
            Grupo2201 Grupo2201 = new Grupo2201();
            try
            {
                if (dr.Read())
                {
                    Grupo2201.codGrupo2201 = Convert.ToInt32(dr["A2201_cd_grupo"]);
                    Grupo2201.codPessoa = new Pessoa(Convert.ToInt32(dr["A052_cd_usuario"]));
                    Grupo2201.codDiaEncerra = Convert.ToInt32(dr["A2201_dia_encerra"]);
                    Grupo2201.indParticipa = Convert.ToString(dr["A2201_ind_participa"]);
                    Grupo2201.codNrParticipa = Convert.ToInt32(dr["A2201_nr_participa"]);
                    Grupo2201.dscAcresParticipa = Convert.ToInt32(dr["A2201_acres_participa"]);
                    Grupo2201.codMinParticipa = Convert.ToInt32(dr["A2201_min_participa"]);
                    Grupo2201.dscDsGrupo = Convert.ToString(dr["A2201_ds_grupo"]);
                    Grupo2201.codIdTipoEv = Convert.ToInt32(dr["A2201_ID_TIPO_EV"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        Grupo2201.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                Grupo2201 = new Grupo2201();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return Grupo2201;
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