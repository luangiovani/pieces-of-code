using System;
using System.Data.OracleClient;
using System.Data;

namespace Database.Entities
{
    /// <summary>
    /// Classe de Mapeamento com a Entidade de Banco de Dados - T017_CLI_NAOCAD
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
    public class ClienteParcial
    {
        #region Atributos
        private int codCliente;
        private int tpoCliente;
        private string nomeCliente;
        private string numCpfCNPJ;
        private string dddTelefone;
        private int NumTelefone;
        private string obsTelefone;
        private string codSetor;
        private int? numEmpregado;
        private string codPorte;
        private string dscEmailCliente;
        private string dscSite;
        private int? indInformal;
        private int? indDesat;
        private int? codUsuario;
        private string logUsuario;
        #endregion

        #region Propriedades
        public int NumTelefone1
        {
            get { return NumTelefone; }
            set { NumTelefone = value; }
        }
        public string ObsTelefone
        {
            get { return obsTelefone; }
            set { obsTelefone = value; }
        }
        public string CodSetor
        {
            get { return codSetor; }
            set { codSetor = value; }
        }

        public int? NumEmpregado
        {
            get { return numEmpregado; }
            set { numEmpregado = value; }
        }
        public string CodPorte
        {
            get { return codPorte; }
            set { codPorte = value; }
        }
        public string DscEmailCliente
        {
            get { return dscEmailCliente; }
            set { dscEmailCliente = value; }
        }
        public string DscSite
        {
            get { return dscSite; }
            set { dscSite = value; }
        }
        public int? IndInformal
        {
            get { return indInformal; }
            set { indInformal = value; }
        }
        public int? IndDesat
        {
            get { return indDesat; }
            set { indDesat = value; }
        }
        public int? CodUsuario
        {
            get { return codUsuario; }
            set { codUsuario = value; }
        }
        public int CodCliente
        {
            get { return codCliente; }
            set { codCliente = value; }
        }
        public int TpoCliente
        {
            get { return tpoCliente; }
            set { tpoCliente = value; }
        }
        public string NomeCliente
        {
            get { return nomeCliente; }
            set { nomeCliente = value; }
        }
        public string NumCpfCNPJ
        {
            get { return numCpfCNPJ; }
            set { numCpfCNPJ = value; }
        }
        public string DddTelefone
        {
            get { return dddTelefone; }
            set { dddTelefone = value; }
        }
        public string LogUsuario
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }

        #endregion

        #region Construtores
        public ClienteParcial()
            : this(-1)
        { }
        public ClienteParcial(int codCliente)
        {
            this.codCliente = codCliente;
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "ClienteParcial.ClienteParcialInc";
        private const string SPUPDATE = "ClienteParcial.ClienteParcialAlt";
        private const string SPDELETE = "ClienteParcial.ClienteParcialDel";
        private const string SPSELECTID = "ClienteParcial.ClienteParcialSelId";
        private const string SPSELECTPAG = "ClienteParcial.ClienteParcialSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMCODIGO = "codCliente";
        private const string PARMCURSOR = "curClienteParcial";
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
                /*0*/ new OracleParameter(PARMCODIGO, OracleType.Int32),// 2, ParameterDirection.InputOutput.ToString()) ,
                /*1*/ new OracleParameter( "tpoCliente", OracleType.Int32),
                /*2*/ new OracleParameter( "nomeCliente", OracleType.VarChar),
                /*3*/ new OracleParameter( "numCpfCNPJ", OracleType.VarChar),
                /*4*/ new OracleParameter( "dddTelefone", OracleType.VarChar),
                /*5*/ new OracleParameter( "NumTelefone", OracleType.Int32),
                /*6*/ new OracleParameter( "obsTelefone", OracleType.VarChar),
                /*7*/ new OracleParameter( "codSetor", OracleType.VarChar),
                /*8*/ new OracleParameter( "numEmpregado", OracleType.Int32),
                /*9*/ new OracleParameter( "codPorte", OracleType.VarChar),
                /*10*/ new OracleParameter( "dscEmailCliente", OracleType.VarChar),
                /*11*/ new OracleParameter( "dscSite", OracleType.VarChar),
                /*12*/ new OracleParameter( "indInformal", OracleType.Int32),
                /*13*/ new OracleParameter( "indDesat", OracleType.Int32),
                /*14*/ new OracleParameter( "codUsuario", OracleType.Int32),
                /*15*/ new OracleParameter( "logUsuario", OracleType.VarChar)
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
            parms[0].Value = this.codCliente;
            parms[1].Value = this.tpoCliente;
            parms[2].Value = this.nomeCliente.ToUpper();
            parms[3].Value = this.numCpfCNPJ.ToUpper();
            parms[4].Value = this.dddTelefone.ToUpper();
            parms[5].Value = this.NumTelefone;

            parms[6].Value  = "";
            if (this.obsTelefone != null)
            { parms[6].Value = this.obsTelefone.ToUpper(); }

            parms[7].Value = DBNull.Value;
            if (this.codSetor != null)
            { parms[7].Value = this.codSetor; }

            parms[8].Value= DBNull.Value;
            if (this.numEmpregado != null)
            { parms[8].Value = this.numEmpregado; }

             parms[9].Value= "";
            if (this.codPorte != null)
            { parms[9].Value = this.codPorte.ToUpper();}

           parms[10].Value  = "";
            if (this.dscEmailCliente != null)
            { parms[10].Value = this.dscEmailCliente.ToUpper();}

            parms[11].Value= "";
            if (this.dscSite != null)
            { parms[11].Value = this.dscSite.ToUpper();}

            parms[12].Value = DBNull.Value;
            if (this.indInformal != null)
            { parms[12].Value = this.indInformal; }

            parms[13].Value = DBNull.Value;
            if (this.indDesat != null)
            { parms[13].Value = this.indDesat; }

            parms[14].Value = DBNull.Value;
            if (this.codUsuario != null)
            { parms[14].Value = this.codUsuario; }

            parms[15].Value = this.logUsuario.ToUpper();
            //if (this.codCliente < 0)
            //{
            //    parms[0].Direction = ParameterDirection.Output;
            //}
            //else
            //{
                parms[0].Direction = ParameterDirection.Input;
            //}
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
                        codCliente = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
                codCliente = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
        public static void Delete(int codigo)
        {
            OracleParameter[] parms = new OracleParameter[] { new OracleParameter(PARMCODIGO, OracleType.Int32, 2) };
            parms[0].Value = codigo;
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
        public static void Delete(int codigo, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { new OracleParameter(PARMCODIGO, OracleType.Int32, 2) };
                parms[0].Value = codigo;
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
        public static OracleDataReader LoadDataDr(int codigo)
        {
            OracleParameter[] param = new OracleParameter[] { 

                new OracleParameter(PARMCODIGO, OracleType.Int32, 2), 
                new OracleParameter(PARMCURSOR, OracleType.Cursor)
            };

            param[0].Value = codigo;
            param[1].Direction = ParameterDirection.Output;

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
        public static OracleDataReader LoadDataDr(int codigo, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { new OracleParameter(PARMCODIGO, OracleType.Int32, 2),
                                                              new OracleParameter(PARMCURSOR, OracleType.Cursor)
};
            param[0].Value = codigo;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <returns>ClienteParcial</returns>
        public static ClienteParcial GetDataRow(int codigo)
        {
            OracleDataReader dr = LoadDataDr(codigo);
            ClienteParcial ClienteParcial = new ClienteParcial();
            try
            {
                if (dr.Read())
                {
                    ClienteParcial.codCliente = Convert.ToInt32(dr["A012_cd_cli"]);
                    if (dr["A017_tp_pessoa"] != DBNull.Value && dr["A017_tp_pessoa"] != DBNull.Value && dr["A017_tp_pessoa"].ToString() != "")
                        ClienteParcial.tpoCliente = Convert.ToInt32(dr["A017_tp_pessoa"]);
                    if (dr["A017_nm_cli"] != DBNull.Value && dr["A017_nm_cli"] != DBNull.Value && dr["A017_nm_cli"].ToString() != "")
                        ClienteParcial.nomeCliente = Convert.ToString(dr["A017_nm_cli"]);
                    if (dr["A017_cgc_cpf"] != DBNull.Value && dr["A017_cgc_cpf"] != DBNull.Value && dr["A017_cgc_cpf"].ToString() != "")
                        ClienteParcial.numCpfCNPJ = Convert.ToString(dr["A017_cgc_cpf"]);
                    if (dr["A017_ddd_cont"] != DBNull.Value && dr["A017_ddd_cont"] != DBNull.Value && dr["A017_ddd_cont"].ToString() != "")
                        ClienteParcial.dddTelefone = Convert.ToString(dr["A017_ddd_cont"]);
                    if (dr["A017_tel_cont"] != DBNull.Value && dr["A017_tel_cont"] != DBNull.Value && dr["A017_tel_cont"].ToString() != "")
                        ClienteParcial.NumTelefone = Convert.ToInt32(dr["A017_tel_cont"]);
                    if (dr["A017_obs_tel"] != DBNull.Value && dr["A017_obs_tel"] != DBNull.Value && dr["A017_obs_tel"].ToString() != "")
                        ClienteParcial.obsTelefone = Convert.ToString(dr["A017_obs_tel"]);
                    if (dr["A048_cd_setor"] != DBNull.Value && dr["A048_cd_setor"] != DBNull.Value && dr["A048_cd_setor"].ToString() != "")
                        ClienteParcial.codSetor = Convert.ToString(dr["A048_cd_setor"]);
                    if (dr["A017_num_empreg"] != DBNull.Value && dr["A017_num_empreg"] != DBNull.Value && dr["A017_num_empreg"].ToString() != "")
                        ClienteParcial.numEmpregado = Convert.ToInt32(dr["A017_num_empreg"]);
                    if (dr["A073_porte"] != DBNull.Value && dr["A073_porte"] != DBNull.Value && dr["A073_porte"].ToString() != "")
                        ClienteParcial.codPorte = Convert.ToString(dr["A073_porte"]);
                    if (dr["A017_e_mail"] != DBNull.Value && dr["A017_e_mail"] != DBNull.Value && dr["A017_e_mail"].ToString() != "")
                        ClienteParcial.dscEmailCliente = Convert.ToString(dr["A017_e_mail"]);
                    if (dr["A017_site"] != DBNull.Value && dr["A017_site"] != DBNull.Value && dr["A017_site"].ToString() != "")
                        ClienteParcial.dscSite = Convert.ToString(dr["A017_site"]);
                    if (dr["A017_ind_informal"] != DBNull.Value && dr["A017_ind_informal"] != DBNull.Value && dr["A017_ind_informal"].ToString() != "")
                        ClienteParcial.indInformal = Convert.ToInt32(dr["A017_ind_informal"]);
                    if (dr["A017_ind_desat"] != DBNull.Value && dr["A017_ind_desat"] != DBNull.Value && dr["A017_ind_desat"].ToString() != "")
                        ClienteParcial.indDesat = Convert.ToInt32(dr["A017_ind_desat"]);
                    if (dr["A052_cd_usuario"] != DBNull.Value && dr["A052_cd_usuario"] != DBNull.Value && dr["A052_cd_usuario"].ToString() != "")
                        ClienteParcial.codUsuario = Convert.ToInt32(dr["A052_cd_usuario"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        ClienteParcial.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                ClienteParcial = new ClienteParcial();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return ClienteParcial;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>ClienteParcial</returns>
        public static ClienteParcial GetDataRow(int codigo, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codigo, trans);
            ClienteParcial ClienteParcial = new ClienteParcial();
            try
            {
                if (dr.Read())
                {
                    ClienteParcial.codCliente = Convert.ToInt32(dr["A012_cd_cli"]);
                    if (dr["A017_tp_pessoa"] != DBNull.Value && dr["A017_tp_pessoa"] != DBNull.Value && dr["A017_tp_pessoa"].ToString() != "")
                        ClienteParcial.tpoCliente = Convert.ToInt32(dr["A017_tp_pessoa"]);
                    if (dr["A017_nm_cli"] != DBNull.Value && dr["A017_nm_cli"] != DBNull.Value && dr["A017_nm_cli"].ToString() != "")
                        ClienteParcial.nomeCliente = Convert.ToString(dr["A017_nm_cli"]);
                    if (dr["A017_cgc_cpf"] != DBNull.Value && dr["A017_cgc_cpf"] != DBNull.Value && dr["A017_cgc_cpf"].ToString() != "")
                        ClienteParcial.numCpfCNPJ = Convert.ToString(dr["A017_cgc_cpf"]);
                    if (dr["A017_ddd_con"] != DBNull.Value && dr["A017_ddd_con"] != DBNull.Value && dr["A017_ddd_con"].ToString() != "")
                        ClienteParcial.dddTelefone = Convert.ToString(dr["A017_ddd_con"]);
                    if (dr["A017_tel_cont"] != DBNull.Value && dr["A017_tel_cont"] != DBNull.Value && dr["A017_tel_cont"].ToString() != "")
                        ClienteParcial.NumTelefone = Convert.ToInt32(dr["A017_tel_cont"]);
                    if (dr["A017_obs_tel"] != DBNull.Value && dr["A017_obs_tel"] != DBNull.Value && dr["A017_obs_tel"].ToString() != "")
                        ClienteParcial.obsTelefone = Convert.ToString(dr["A017_obs_tel"]);
                    if (dr["A048_cd_setor"] != DBNull.Value && dr["A048_cd_setor"] != DBNull.Value && dr["A048_cd_setor"].ToString() != "")
                        ClienteParcial.codSetor = Convert.ToString(dr["A048_cd_setor"]);
                    if (dr["A017_num_empreg"] != DBNull.Value && dr["A017_num_empreg"] != DBNull.Value && dr["A017_num_empreg"].ToString() != "")
                        ClienteParcial.numEmpregado = Convert.ToInt32(dr["A017_num_empreg"]);
                    if (dr["A073_porte"] != DBNull.Value && dr["A073_porte"] != DBNull.Value && dr["A073_porte"].ToString() != "")
                        ClienteParcial.codPorte = Convert.ToString(dr["A073_porte"]);
                    if (dr["A017_e_mail"] != DBNull.Value && dr["A017_e_mail"] != DBNull.Value && dr["A017_e_mail"].ToString() != "")
                        ClienteParcial.dscEmailCliente = Convert.ToString(dr["A017_e_mail"]);
                    if (dr["A017_site"] != DBNull.Value && dr["A017_site"] != DBNull.Value && dr["A017_site"].ToString() != "")
                        ClienteParcial.dscSite = Convert.ToString(dr["A017_site"]);
                    if (dr["A017_ind_informal"] != DBNull.Value && dr["A017_ind_informal"] != DBNull.Value && dr["A017_ind_informal"].ToString() != "")
                        ClienteParcial.indInformal = Convert.ToInt32(dr["A017_ind_informal"]);
                    if (dr["A017_ind_desat"] != DBNull.Value && dr["A017_ind_desat"] != DBNull.Value && dr["A017_ind_desat"].ToString() != "")
                        ClienteParcial.indDesat = Convert.ToInt32(dr["A017_ind_desat"]);
                    if (dr["A052_cd_usuario"] != DBNull.Value && dr["A052_cd_usuario"] != DBNull.Value && dr["A052_cd_usuario"].ToString() != "")
                        ClienteParcial.codUsuario = Convert.ToInt32(dr["A052_cd_usuario"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        ClienteParcial.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                ClienteParcial = new ClienteParcial();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return ClienteParcial;
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

    }
}
