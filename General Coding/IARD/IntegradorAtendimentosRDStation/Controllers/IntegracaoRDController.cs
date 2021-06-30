using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using IntegradorAtendimentosRDStation.Models;
using Newtonsoft.Json;

namespace IntegradorAtendimentosRDStation.Controllers
{
    [AllowAnonymous]
    public class IntegracaoRDController : ApiController
    {
        [Route("api/{route}")]
        public async Task<IHttpActionResult> Post(string route)
        {
            #region Rota não informada ou Vazia
            if (String.IsNullOrEmpty(route) || String.IsNullOrWhiteSpace(route))
            {
                SendEmail("Rota não encontrada.: " + (route ?? "N/A"), (route ?? "N/A"), "Integracao RD-Station via POST - ERRO");
                return NotFound();
            }
            #endregion
            else
            {
                try
                {
                    #region Obter Dados de integrador

                    IntegradorModel integrador = null;

                    #region sSql
                    string sSqlIntegrador = @"SELECT DISTINCT I.SEQUENCIAL, I.API, I.CATEGORIAATENDIMENTO, I.TIPOATENDIMENTO, I.A001_OBS, I.CODSOL, I.CODOBJ, I.COD_PROJETO_SGE, NVL(I.COD_ACAO_SEQ,0) COD_ACAO_SEQ, I.DT_INC, I.USUARIO_INC, I.DT_ALT, I.USUARIO_ALT, I.ATIVO, T773.TITSOL PROJETO, T772.TITOBJ ACAO, T773.ZM_ANO, COUNT(DISTINCT LI.A001_NUM_SEQUENCIAL) CONVERSOES"
                                          + @"      FROM INTEGRADOR_RDSTATION I"
                                          + @"      JOIN T773_SOLUCAO T773 ON T773.COD_PROJETO_SGE = I.COD_PROJETO_SGE"
                                          + @"       AND T773.CODSOL = I.CODSOL"
                                          + @"      JOIN T772_OBJETIVO T772 ON T772.COD_PROJETO_SGE = T773.COD_PROJETO_SGE"
                                          + @"       AND T772.CODACAO_SEQ = I.COD_ACAO_SEQ"
                                          + @"       AND T772.CTT_ANO = T773.ZM_ANO"
                                          + @" LEFT JOIN LOG_INTEGRADOR_RDSTATION LI ON LI.SEQUENCIAL_INTEGRADOR = I.SEQUENCIAL "
                                          + @"     WHERE T773.ZM_ANO >= " + DateTime.Now.Year
                                          + @"       AND UPPER(API) = '" + route.ToUpper().Replace(" ", "") + @"'"
                                          + @"  GROUP BY I.SEQUENCIAL, I.API,  I.CATEGORIAATENDIMENTO, I.TIPOATENDIMENTO, I.A001_OBS, I.CODSOL, I.CODOBJ, I.COD_PROJETO_SGE, I.COD_ACAO_SEQ, I.DT_INC, I.USUARIO_INC, I.DT_ALT, I.USUARIO_ALT, I.ATIVO, T773.TITSOL, T772.TITOBJ, T773.ZM_ANO"
                                          + @"  ORDER BY T773.ZM_ANO DESC";
                    #endregion

                    OracleDataReader rdr = null;
                    try
                    {
                        using (rdr = Classes.DataBase.ExecuteReader(CommandType.Text, sSqlIntegrador))
                        {
                            try
                            {
                                if (rdr.Read())
                                {
                                    integrador = new IntegradorModel()
                                    {
                                        A001_OBS = rdr["A001_OBS"].ToString(),
                                        API = rdr["API"].ToString(),
                                        COD_ACAO_SEQ = rdr["COD_ACAO_SEQ"].ToString(),
                                        ACAO = rdr["ACAO"].ToString(),
                                        COD_PROJETO_SGE = rdr["COD_PROJETO_SGE"].ToString(),
                                        PROJETO = rdr["PROJETO"].ToString(),
                                        CODOBJ = rdr["CODOBJ"].ToString(),
                                        CODSOL = rdr["CODSOL"].ToString(),
                                        DT_ALT = DateTime.Parse(rdr["DT_ALT"].ToString()),
                                        DT_INC = DateTime.Parse(rdr["DT_INC"].ToString()),
                                        SEQUENCIAL = Convert.ToInt32(rdr["SEQUENCIAL"].ToString()),
                                        USUARIO_ALT = rdr["USUARIO_ALT"].ToString(),
                                        USUARIO_INC = rdr["USUARIO_INC"].ToString(),
                                        ATIVO = Convert.ToInt32(rdr["ATIVO"].ToString()),
                                        TIPOATENDIMENTO = Convert.ToInt32(rdr["TIPOATENDIMENTO"].ToString()),
                                        CATEGORIAATENDIMENTO = Convert.ToInt32(rdr["CATEGORIAATENDIMENTO"].ToString())
                                    };

                                    rdr.Close();
                                }
                                else
                                {
                                    SendEmail("Integrador não localizado", (route ?? "N/A"), "Integracao RD-Station via POST - ERRO");
                                    rdr.Close();
                                }
                            }
                            catch (Exception exDadosIntegrador)
                            {
                                SendEmail("Erro ao tentar obter dados do Integrador.: " + exDadosIntegrador.Message, (route ?? "N/A"), "Integracao RD-Station via POST - ERRO");
                            }

                        }
                    }
                    catch
                    {
                        if (rdr != null)
                        {
                            rdr.Close();
                        }

                        SendEmail("Erro ao tentar obter dados do Integrador", (route ?? "N/A"), "Integracao RD-Station via POST - ERRO");
                    }

                    #endregion

                    if (integrador != null && integrador.ATIVO == 1 && Request != null && Request.Content != null)
                    {
                        #region Leitura da String do Request
                        byte[] byteArray = null;
                        string contentString = "";
                        bool deuErro = false;
                        try
                        {
                            byteArray = await Request.Content.ReadAsByteArrayAsync();
                            contentString = await Request.Content.ReadAsStringAsync();
                        }
                        catch (Exception exByteArray)
                        {
                            deuErro = true;
                            string msg = "Erro ao tentar obter dados do Request.: " + exByteArray.Message;
                            msg += (contentString ?? "");
                            SendEmail(msg, (route ?? "N/A"), "Integracao RD-Station via POST - ERRO");
                        }
                        #endregion

                        #region Tem conteúdo no POST
                        if (byteArray != null && byteArray.Length > 0 && deuErro == false)
                        {
                            string responseString = "";
                            string vOriginal = "";
                            try
                            {
                                responseString = Encoding.UTF8.GetString(byteArray, 0, byteArray.Length);
                                vOriginal = responseString;

                                if (!String.IsNullOrEmpty(vOriginal) && !String.IsNullOrWhiteSpace(vOriginal))
                                {
                                    bool isErro = false;
                                    try
                                    {
                                        vOriginal = vOriginal.Replace("Código SIA (PJ)", "A012_CD_CLI_PJ");
                                        vOriginal = vOriginal.Replace("Código SIA (PF)", "A012_CD_CLI_PF");
                                        vOriginal = vOriginal.Replace("Código Contato (PJ)", "A261_CD_CONT");
                                        vOriginal = vOriginal.Replace("Porte da Empresa", "Porte");
                                        vOriginal = vOriginal.Replace("Ramo de atividade", "Ramo");
                                    }
                                    catch (Exception exReplace)
                                    {
                                        isErro = true;
                                        SendEmail("Erro no replace da string.: " + exReplace.Message, route, "Integracao RD-Station via POST - ERRO");
                                    }

                                    if (isErro == false)
                                    {
                                        RootObject lead = null;
                                        try
                                        {
                                            lead = JsonConvert.DeserializeObject<RootObject>(vOriginal);
                                            if (lead.leads == null)
                                                lead.leads = new List<Lead>();
                                        }
                                        catch (Exception exDeserialize)
                                        {
                                            isErro = true;
                                            string msge = "Erro na deserialização.: " + exDeserialize.Message + " - JSON.: " + vOriginal;
                                            SendEmail(msge, route, "Integracao RD-Station via POST - ERRO");
                                        }

                                        if (isErro == false)
                                        {
                                            try
                                            {
                                                var Conversao = lead != null ? (lead.leads.Count() > 0 ? lead.leads.FirstOrDefault() : null) : null;
                                                if (Conversao != null && (!String.IsNullOrEmpty(Conversao.custom_fields.A012_CD_CLI_PF) || !String.IsNullOrEmpty(Conversao.custom_fields.A012_CD_CLI_PJ)))
                                                {
                                                    #region A012_CD_CLI_PJ || A012_CD_CLI_PF || A261_CD_CONT
                                                    string A012_CD_CLI_PJ = Conversao.custom_fields != null ? Conversao.custom_fields.A012_CD_CLI_PJ : "";
                                                    string A012_CD_CLI_PF = Conversao.custom_fields != null ? Conversao.custom_fields.A012_CD_CLI_PF : "";
                                                    string A261_CD_CONT = Conversao.custom_fields != null ? Conversao.custom_fields.A261_CD_CONT : "";

                                                    #region Parse em Tipo Numérico
                                                    int A012_CD_CLI_PJ_NUM = 0;
                                                    int A012_CD_CLI_PF_NUM = 0;
                                                    int A261_CD_CONT_NUM = 0;

                                                    if (!Int32.TryParse(A012_CD_CLI_PJ, out A012_CD_CLI_PJ_NUM))
                                                    {
                                                        A012_CD_CLI_PJ = "0";
                                                    }

                                                    if (!Int32.TryParse(A012_CD_CLI_PF, out A012_CD_CLI_PF_NUM))
                                                    {
                                                        A012_CD_CLI_PF = "0";
                                                    }

                                                    if (!Int32.TryParse(A261_CD_CONT, out A261_CD_CONT_NUM))
                                                    {
                                                        A261_CD_CONT = "0";
                                                    }
                                                    #endregion

                                                    #region Verifica CodCliente e CodContato
                                                    if (A012_CD_CLI_PJ_NUM > 0)
                                                    {
                                                        A012_CD_CLI_PJ = A012_CD_CLI_PJ_NUM.ToString();
                                                    }
                                                    else
                                                    {
                                                        A012_CD_CLI_PJ = "";
                                                    }

                                                    if (A012_CD_CLI_PF_NUM > 0)
                                                    {
                                                        A012_CD_CLI_PF = A012_CD_CLI_PF_NUM.ToString();
                                                    }
                                                    else
                                                    {
                                                        A012_CD_CLI_PF = "";
                                                    }

                                                    if (A261_CD_CONT_NUM > 0)
                                                    {
                                                        A261_CD_CONT = A261_CD_CONT_NUM.ToString();
                                                    }
                                                    else
                                                    {
                                                        A261_CD_CONT = "";
                                                    }
                                                    #endregion

                                                    #endregion

                                                    if (!String.IsNullOrEmpty(A012_CD_CLI_PF) && !String.IsNullOrEmpty(A012_CD_CLI_PJ))
                                                    {
                                                        int A012_CD_CLI = !String.IsNullOrEmpty(A012_CD_CLI_PJ) ? Convert.ToInt32(A012_CD_CLI_PJ) : Convert.ToInt32(A012_CD_CLI_PF);
                                                        int A014_NUM_CONT = 0;
                                                        string A001_NUMSEQUENCIAL = "0";

                                                        #region ObterContato
                                                        if (!String.IsNullOrEmpty(A261_CD_CONT))
                                                        {
                                                            string sSqlObterContato = "SELECT A014_NUM_CONT FROM T014_CONTATO_CLIENTE WHERE A012_CD_CLI = " + A012_CD_CLI.ToString() + " AND A261_CD_CONT = " + A261_CD_CONT;
                                                            OracleDataReader drContatoCliente = null;
                                                            try
                                                            {
                                                                drContatoCliente = Classes.DataBase.ExecuteReader(CommandType.Text, sSqlObterContato);
                                                                if (drContatoCliente.Read())
                                                                {
                                                                    A014_NUM_CONT = Convert.ToInt32(drContatoCliente["A014_NUM_CONT"].ToString());
                                                                }
                                                                drContatoCliente.Close();
                                                            }
                                                            catch (Exception exDCont)
                                                            {
                                                                if (drContatoCliente != null)
                                                                    drContatoCliente.Close();

                                                                SendEmail("Erro ao tentar obter Dados de Contato do Cliente.: " + exDCont.Message, route, "Integracao RD-Station via POST - ERRO");
                                                            }

                                                        }
                                                        else
                                                        {
                                                            string sSqlObterContato = "SELECT A014_NUM_CONT FROM T014_CONTATO_CLIENTE WHERE A012_CD_CLI = " + A012_CD_CLI.ToString() + " AND A014_TP_CONT = 'P'";
                                                            OracleDataReader drContatoCliente = Classes.DataBase.ExecuteReader(CommandType.Text, sSqlObterContato);
                                                            try
                                                            {
                                                                if (drContatoCliente.Read())
                                                                {
                                                                    A014_NUM_CONT = Convert.ToInt32(drContatoCliente["A014_NUM_CONT"].ToString());
                                                                }
                                                                else
                                                                {
                                                                    drContatoCliente.Close();
                                                                    sSqlObterContato = "SELECT A014_NUM_CONT FROM T014_CONTATO_CLIENTE WHERE A012_CD_CLI = " + A012_CD_CLI.ToString();
                                                                    drContatoCliente = Classes.DataBase.ExecuteReader(CommandType.Text, sSqlObterContato);
                                                                    if (drContatoCliente.Read())
                                                                    {
                                                                        A014_NUM_CONT = Convert.ToInt32(drContatoCliente["A014_NUM_CONT"].ToString());
                                                                    }
                                                                }
                                                                drContatoCliente.Close();
                                                            }
                                                            catch (Exception exDCont)
                                                            {
                                                                if (drContatoCliente != null)
                                                                    drContatoCliente.Close();

                                                                SendEmail("Erro ao tentar obter Dados de Contato do Cliente.: " + exDCont.Message, route, "Integracao RD-Station via POST - ERRO");
                                                            }

                                                        }

                                                        #endregion

                                                        if (A012_CD_CLI > 0 && A014_NUM_CONT > 0)
                                                        {
                                                            #region Inserir Atendimento

                                                            int nNovoEscritorio = 727;
                                                            int nCount = 0;
                                                            int na011_cd_cid = 0;

                                                            #region ObtemEscritorio
                                                            string sSqlCID = "select a011_cd_cid from T018_cli_cadast where a012_cd_cli = " + A012_CD_CLI.ToString();
                                                            var drCID = Classes.DataBase.ExecuteReader(CommandType.Text, sSqlCID);
                                                            try
                                                            {
                                                                if (drCID.Read())
                                                                {
                                                                    na011_cd_cid = Convert.ToInt32(drCID["a011_cd_cid"].ToString());
                                                                }
                                                                drCID.Close();
                                                            }
                                                            catch (Exception exCidade)
                                                            {
                                                                if (drCID != null)
                                                                    drCID.Close();

                                                                SendEmail("Erro ao tentar obter Cidade para Atendimento.: " + exCidade.Message, route, "Integracao RD-Station via POST - ERRO");
                                                            }

                                                            if (na011_cd_cid > 0)
                                                            {
                                                                #region sSqlCOUNT
                                                                string sSqlCOUNT = @"
                                                                    select count(*) nCount from   SEBRAE.T021_ESTADO E,
                                                                        SEBRAE.T035_PAIS P,
                                                                        SEBRAE.T033_MICRORREG M,
                                                                        SEBRAE.T018_CLI_CADAST T018,
                                                                        SEBRAE.T011_CIDADE C
                                                                        where
                                                                        C.a011_cd_cid = " + na011_cd_cid.ToString() + @" and
                                                                        C.a035_cd_pais = 1 and
                                                                        C.a021_cd_est = 42 and
                                                                        E.a035_cd_pais = P.a035_cd_pais and
                                                                        C.a035_cd_pais = E.a035_cd_pais and
                                                                        C.a021_cd_est  = E.a021_cd_est and
                                                                        C.a004_cd_escr = T018.a012_cd_cli(+) and
                                                                        C.A033_cd_micr = M.A033_cd_micr (+) and
                                                                        C.A035_cd_pais = M.A035_cd_pais (+) and
                                                                        C.A021_cd_est = M.A021_cd_est (+)";
                                                                #endregion

                                                                var drCount = Classes.DataBase.ExecuteReader(CommandType.Text, sSqlCOUNT);
                                                                try
                                                                {
                                                                    if (drCount.Read())
                                                                    {
                                                                        nCount = Convert.ToInt32(drCount["nCount"].ToString());
                                                                    }
                                                                    drCount.Close();
                                                                }
                                                                catch (Exception exCont)
                                                                {
                                                                    if (drCount != null)
                                                                        drCount.Close();

                                                                    SendEmail("Erro ao tentar obter Contagem de Cidade para Atendimento.: " + exCont.Message, route, "Integracao RD-Station via POST - ERRO");
                                                                }

                                                                if (nCount > 0)
                                                                {
                                                                    string sSqlObterEscritorio = @"Select C.A004_cd_escr nNovoEscritorio
                                                                      from   SEBRAE.T021_ESTADO E,
                                                                        SEBRAE.T035_PAIS P,
                                                                        SEBRAE.T033_MICRORREG M,
                                                                        SEBRAE.T018_CLI_CADAST T018,
                                                                        SEBRAE.T011_CIDADE C
                                                                      where
                                                                        C.a011_cd_cid = " + na011_cd_cid.ToString() + @" and
                                                                        C.a035_cd_pais = 1 and
                                                                        C.a021_cd_est = 42 and
                                                                        E.a035_cd_pais = P.a035_cd_pais and
                                                                        C.a035_cd_pais = E.a035_cd_pais and
                                                                        C.a021_cd_est  = E.a021_cd_est and
                                                                        C.a004_cd_escr = T018.a012_cd_cli(+) and
                                                                        C.A033_cd_micr = M.A033_cd_micr (+) and
                                                                        C.A035_cd_pais = M.A035_cd_pais (+) and
                                                                        C.A021_cd_est = M.A021_cd_est (+)  and rownum < 2";

                                                                    var drEscr = Classes.DataBase.ExecuteReader(CommandType.Text, sSqlObterEscritorio);

                                                                    try
                                                                    {
                                                                        if (drEscr.Read())
                                                                        {
                                                                            nNovoEscritorio = Convert.ToInt32(drEscr["nNovoEscritorio"].ToString());
                                                                        }
                                                                        drEscr.Close();
                                                                    }
                                                                    catch (Exception exEscr)
                                                                    {
                                                                        if (drEscr != null)
                                                                            drEscr.Close();

                                                                        SendEmail("Erro ao tentar obter Escritório para Atendimento.: " + exEscr.Message, route, "Integracao RD-Station via POST - ERRO");
                                                                    }
                                                                }
                                                            }

                                                            #endregion

                                                            #region Insere Atendimento
                                                            string sCODAB = "99";
                                                            string sCODAE = "A5B62A22-B3D1-4D44-B552-382C8ECF2E71";
                                                            string nUsuario = "1";
                                                            string nNumAtendimento = "-1";
                                                            if (A014_NUM_CONT > 0)
                                                            {
                                                                string sSqlNumATN = @"Select SEBRAE.sequencial(" + nNovoEscritorio.ToString() + "," + nUsuario + ") nNumAtendimento From dual";
                                                                var drNumAtn = Classes.DataBase.ExecuteReader(CommandType.Text, sSqlNumATN);
                                                                try
                                                                {
                                                                    if (drNumAtn.Read())
                                                                    {
                                                                        nNumAtendimento = drNumAtn["nNumAtendimento"].ToString();
                                                                    }
                                                                    drNumAtn.Close();
                                                                }
                                                                catch (Exception exNumAtend)
                                                                {
                                                                    if (drNumAtn != null)
                                                                        drNumAtn.Close();

                                                                    SendEmail("Erro ao tentar obter sequencial para Atendimento.: " + exNumAtend.Message, route, "Integracao RD-Station via POST - ERRO");
                                                                }

                                                                if (nNumAtendimento != "-1" && !String.IsNullOrEmpty(nNumAtendimento))
                                                                {
                                                                    #region sSqlINS
                                                                    string sSqlINS = @"Insert into SEBRAE.T001_ATEND
                                                                    (a004_cd_escr,
                                                                    a052_cd_usuario,
                                                                    a001_num_atend,
                                                                    a012_cd_cli,
                                                                    a014_num_cont,
                                                                    a054_cd_tp_at,
                                                                    a001_dt_atend,
                                                                    a001_tem_atend,
                                                                    a001_dt_inc,
                                                                    a001_dt_ult_alt,
                                                                    a001_ano_ref,
                                                                    a001_mes_ref,
                                                                    a001_obs
                                                                    )
                                                                    Values
                                                                    (" + nNovoEscritorio + @",
                                                                        " + nUsuario + @",
                                                                        " + nNumAtendimento + @",
                                                                        " + A012_CD_CLI.ToString() + @",
                                                                        " + A014_NUM_CONT.ToString() + @",
                                                                        " + integrador.TIPOATENDIMENTO.ToString() + @",
                                                                        sysdate,
                                                                        '0100', 
                                                                        sysdate,
                                                                        sysdate,
                                                                        " + DateTime.Now.Year + @",
                                                                        " + DateTime.Now.Month + @",
                                                                        '" + (integrador.A001_OBS + @" " + Conversao.last_conversion.source.Replace("-", " ").Replace("_", " ")).Replace("'", "''") + @"')";
                                                                    #endregion

                                                                    OracleDataReader drINSATN = null;

                                                                    try
                                                                    {
                                                                        using (drINSATN = Classes.DataBase.ExecuteReader(CommandType.Text, sSqlINS))
                                                                        {
                                                                            drINSATN.Close();
                                                                        }
                                                                    }
                                                                    catch (Exception exInsereAtend)
                                                                    {
                                                                        if (drINSATN != null)
                                                                            drINSATN.Close();

                                                                        SendEmail("Erro ao tentar inserir novo Atendimento.: " + exInsereAtend.Message, route, "Integracao RD-Station via POST - ERRO");
                                                                    }

                                                                    string sSqlATNS = @"insert into SEBRAE.T001_ATEND_SINCO ( A001_NUM_ATEND,   A004_CD_ESCR,   A052_CD_USUARIO,
                                          CODAB,   CODAE,   CODOBJ,   CODSOL,
                                          A783_cd_abordagem, A784_cd_categoria, A786_cd_instrumento, A785_tipo) Values (
                                        " + nNumAtendimento + @", " + nNovoEscritorio + @", " + nUsuario + @",
                                         '" + sCODAB + @"',  '" + sCODAE + @"',   '" + integrador.CODOBJ + @"', '" + integrador.CODSOL + @"',
                                          1,
                                          " + integrador.CATEGORIAATENDIMENTO.ToString() + @"
                                          ,3,'A' )";
                                                                    var drINSATNS = Classes.DataBase.ExecuteReader(CommandType.Text, sSqlATNS);
                                                                    drINSATNS.Close();

                                                                    string sSqlInsT013 = @"Insert INTO SEBRAE.T013_CONSULTA
                                            (a004_cd_escr, a052_cd_usuario,a001_num_atend,a005_cd_tp_cons)
                                            values
                                            (" + nNovoEscritorio + @", " + nUsuario + @", " + nNumAtendimento + @", 
                                            113)";
                                                                    var drInsT013 = Classes.DataBase.ExecuteReader(CommandType.Text, sSqlInsT013);
                                                                    drInsT013.Close();

                                                                    string sSqlATN = "SELECT A001_NUMSEQUENCIAL FROM T001_ATEND WHERE A012_CD_CLI = " + A012_CD_CLI + " AND A001_NUM_ATEND = " + nNumAtendimento + " AND A014_NUM_CONT = " + A014_NUM_CONT.ToString();
                                                                    var drATNNovo = Classes.DataBase.ExecuteReader(CommandType.Text, sSqlATN);
                                                                    if (drATNNovo.Read())
                                                                    {
                                                                        A001_NUMSEQUENCIAL = drATNNovo["A001_NUMSEQUENCIAL"].ToString();
                                                                    }

                                                                    drATNNovo.Close();
                                                                }
                                                            }
                                                            else
                                                            {
                                                                SendEmail("Não tem Contato para Gerar Atendimento", route, "Integracao RD-Station via POST - ERRO");
                                                            }
                                                            #endregion

                                                            #endregion
                                                        }

                                                        if (A001_NUMSEQUENCIAL != "0")
                                                        {
                                                            int SEQUENCIAL;

                                                            #region Obtem Sequencial
                                                            string seq = "SELECT MAX(SEQUENCIAL) + 1 SEQ FROM LOG_INTEGRADOR_RDSTATION";
                                                            var rdrSQ = Classes.DataBase.ExecuteReader(System.Data.CommandType.Text, seq);
                                                            if (rdrSQ.Read() && !String.IsNullOrEmpty(rdrSQ["SEQ"].ToString()))
                                                                SEQUENCIAL = Convert.ToInt32(rdrSQ["SEQ"].ToString());
                                                            else
                                                                SEQUENCIAL = 1;
                                                            rdrSQ.Close();
                                                            #endregion

                                                            #region Insere LOG

                                                            string sSql = "INSERT INTO LOG_INTEGRADOR_RDSTATION(SEQUENCIAL, SEQUENCIAL_INTEGRADOR, A001_NUM_SEQUENCIAL, DADOS_JSON, A012_CD_CLI_PF, A012_CD_CLI_PJ, A261_CD_CONT, DT_INTEGRACAO) VALUES";
                                                            sSql += "(" + SEQUENCIAL + ",";
                                                            sSql += integrador.SEQUENCIAL + ",";
                                                            sSql += A001_NUMSEQUENCIAL + ",";
                                                            sSql += "'" + vOriginal.Replace("'", "''") + "',";
                                                            sSql += (String.IsNullOrEmpty(Conversao.custom_fields.A012_CD_CLI_PF) ? "0" : Conversao.custom_fields.A012_CD_CLI_PF) + ",";
                                                            sSql += (String.IsNullOrEmpty(Conversao.custom_fields.A012_CD_CLI_PJ) ? "0" : Conversao.custom_fields.A012_CD_CLI_PJ) + ",";
                                                            sSql += (String.IsNullOrEmpty(Conversao.custom_fields.A261_CD_CONT) ? "0" : Conversao.custom_fields.A261_CD_CONT) + ",";
                                                            sSql += "systimestamp)";

                                                            var rdrIns = Classes.DataBase.ExecuteReader(System.Data.CommandType.Text, sSql);
                                                            rdrIns.Close();

                                                            #endregion
                                                        }
                                                    }
                                                }
                                            }
                                            catch (Exception exConversao)
                                            {
                                                string msge = "Erro.: " + exConversao.Message + " - JSON.: " + vOriginal;
                                                SendEmail(msge, route, "Integracao RD-Station via POST - ERRO");
                                            }

                                        }
                                    }
                                }
                                else
                                {
                                    SendEmail("string de byteArra vazia", route, "Integracao RD-Station via POST - ERRO");
                                }
                            }
                            catch (Exception exDecodeString)
                            {
                                SendEmail("Erro ao ler string de byteArray.: " + exDecodeString.Message, route, "Integracao RD-Station via POST - ERRO");
                            }

                            return Ok();
                        }
                        #endregion
                        #region Não tem conteúdo no POST
                        else
                        {
                            SendEmail((contentString ?? "Content sem Conteúdo"), route, "Integracao RD-Station via POST - ERRO");
                        }
                        #endregion
                    }
                    else
                    {
                        SendEmail("Integrador não Localizado", (route ?? "N/A"), "Integracao RD-Station via POST - ERRO");
                        return NotFound();
                    }
                }
                catch (Exception ex)
                {
                    string msgErro = ex.Message;
                    try
                    {
                        SendEmail(ex.Message, route, "Integracao RD-Station via POST - ERRO");
                        //string _comando = "insert into sebrae.t1500_email (a1500_email,a1500_de,a1500_para,a1500_assunto,a1500_msg,a1500_enviado,a1500_dt_inc,a1500_dt_alt) values ";
                        //_comando += " (nvl((select max(a1500_email)+1 from sebrae.t1500_email),0) ,'web@sc.sebrae.com.br','luan@esfera.com.br','Integracao RD-Station via POST - ERRO','" + ex.Message.Replace("'", "''") + "',0,sysdate,sysdate)";
                        //OracleDataReader dr2 = Classes.DataBase.ExecuteReader(CommandType.Text, _comando);
                        //dr2.Close();
                    }
                    catch (Exception ex2)
                    {
                        msgErro += " " + route + " " + ex2.Message;
                        string _comando = "insert into sebrae.t1500_email (a1500_email,a1500_de,a1500_para,a1500_assunto,a1500_msg,a1500_enviado,a1500_dt_inc,a1500_dt_alt) values ";
                        _comando += " (nvl((select max(a1500_email)+1 from sebrae.t1500_email),0) ,'web@sc.sebrae.com.br','luan@esfera.com.br','Integracao RD-Station via POST - ERRO','" + msgErro.Replace("'", "''") + "',0,sysdate,sysdate)";
                        OracleDataReader dr2 = Classes.DataBase.ExecuteReader(CommandType.Text, _comando);
                        dr2.Close();
                    }
                }
                return Ok();
            }
        }

        [AcceptVerbs("GET")]
        [Route("api/sendlead/{A261_CD_CONT}")]
        public string SendLead(string A261_CD_CONT)
        {
            try
            {
                if (!String.IsNullOrEmpty(A261_CD_CONT) && !String.IsNullOrWhiteSpace(A261_CD_CONT) && A261_CD_CONT != "0")
                {
                    #region Obtém o Tipo de Cliente
                    string getTpCli = @"SELECT T018.A012_CD_CLI, T014.A261_CD_CONT, T262.A262_DT_ULT_ALT
                                       FROM T018_CLI_CADAST T018
                                       JOIN T014_CONTATO_CLIENTE T014 ON T014.A012_CD_CLI = T018.A012_CD_CLI
                                       JOIN T262_DAD_AD_CONT T262 ON T262.A261_CD_CONT = T014.A261_CD_CONT
                                       JOIN T039_PESSOA_JUR T039 ON T039.A012_CD_CLI = T018.A012_CD_CLI
                                      WHERE T018.A018_TP_CLI <> 1
                                        AND NVL(T018.A018_IND_DES_CLI,0) = 0
                                        AND NVL(T014.A014_IND_DESAT,0) = 0
                                        AND T014.A261_CD_CONT = " + A261_CD_CONT + @"
                                    GROUP BY T018.A012_CD_CLI, T014.A261_CD_CONT, T262.A262_DT_ULT_ALT    
                                    ORDER BY A262_DT_ULT_ALT DESC";
                    #endregion

                    using (var rdrCLI = Classes.DataBase.ExecuteReader(CommandType.Text, getTpCli))
                    {
                        if (rdrCLI.Read())
                        {
                            rdrCLI.Close();
                            try
                            {
                                #region Dados de Contato para Integração RD
                                string getDados = @"SELECT '{""token_rdstation"":""7aea823368c80461ecd7857485c8d802"",""identificador"" : ""SIA"",
""nome"" : ""'||T261.A261_NM_CONT||'"",
""CPF"" : ""'||NVL(T262.A262_NUM_CPF,0)||'"",
""email"" : ""'||T262.A262_EMAIL||'"",
""celular"" : ""('||CASE WHEN TO_NUMBER(REPLACE(regexp_replace(NVL(T261.A261_2DDD_CONT,''), '[^0-9]', ''),' ','')) IS NULL OR TO_NUMBER(REPLACE(regexp_replace(NVL(T261.A261_2DDD_CONT,''), '[^0-9]', ''),' ','')) = 0 
            THEN T018.A018_2DDD_CLI
            ELSE T261.A261_2DDD_CONT
          END||') '||(CASE WHEN TO_NUMBER(REPLACE(regexp_replace(NVL(T261.A261_2DDD_CONT,''), '[^0-9]', ''),' ','')) IS NULL OR TO_NUMBER(REPLACE(regexp_replace(NVL(T261.A261_2DDD_CONT,''), '[^0-9]', ''),' ','')) = 0 
            THEN T018.A018_TEL2_CLI
            ELSE T261.A261_CEL_CONT
          END)||'"",
""empresa"" : ""'||NVL(T039.A039_NM_FANT,T018.A018_NM_CLI)||'"",
""CNPJ"" : ""'||NVL(T039.A039_cgc,0)||'"",
""Código SIA (PJ)"" : ""'||T014.A012_CD_CLI||'"",
""Código Contato (PJ)"" : ""'||T014.A261_CD_CONT||'"",
""Porte da Empresa"" : ""'||T039.A039_PORTE||'"",
""Perfil"": ""'||CASE WHEN T039.A039_PORTE = 'EI' THEN 'Microempreendedor Individual'
               WHEN T039.A039_PORTE = 'ME' THEN 'Micro e Pequena Empresa'
               WHEN T039.A039_PORTE = 'PQ' THEN 'Micro e Pequena Empresa'
               ELSE ''
          END ||'"",
""Ramo de atividade"" : ""'||T048.A048_DSC_SETOR||'"",
""estado"" : ""'||T021C.A021_SGL_EST||'"",
""cidade"" : ""'||T011C.A011_NM_CID||'""}' SJSON,
       T014.A012_CD_CLI,
       T014.A261_CD_CONT,
       T262.A262_EMAIL,
       T261.A261_NM_CONT,
       NVL(T039.A039_NM_FANT,T018.A018_NM_CLI) Empresa,
       NVL(T039.A039_NUM_EMPREG,0) NumFuncionarios,
       T018.A018_END_CLI || ' ' || T018.A018_NMR_END || ', ' || T018.A018_COMP_END || ' - Bairro.: ' || T018.A018_BAIR_CLI || ' - ' || T018.A018_CEP_CLI || ' | ' || T011.A011_NM_CID || '/' || T021.A021_SGL_EST Endereco,
        CASE WHEN TO_NUMBER(REPLACE(regexp_replace(NVL(T261.A261_DDD_CONT,''), '[^0-9]', ''),' ','')) IS NULL OR TO_NUMBER(REPLACE(regexp_replace(NVL(T261.A261_DDD_CONT,''), '[^0-9]', ''),' ','')) = 0 
            THEN T018.A018_DDD_CLI
            ELSE T261.A261_DDD_CONT
          END DDDFIXO,
          CASE WHEN TO_NUMBER(REPLACE(regexp_replace(NVL(T261.A261_DDD_CONT,''), '[^0-9]', ''),' ','')) IS NULL OR TO_NUMBER(REPLACE(regexp_replace(NVL(T261.A261_DDD_CONT,''), '[^0-9]', ''),' ','')) = 0 
            THEN T018.A018_TEL_CLI
            ELSE T261.A261_TEL_CONT
          END FONEFIXO,
          CASE WHEN TO_NUMBER(REPLACE(regexp_replace(NVL(T261.A261_2DDD_CONT,''), '[^0-9]', ''),' ','')) IS NULL OR TO_NUMBER(REPLACE(regexp_replace(NVL(T261.A261_2DDD_CONT,''), '[^0-9]', ''),' ','')) = 0 
            THEN T018.A018_2DDD_CLI
            ELSE T261.A261_2DDD_CONT
          END DDDCEL,
          CASE WHEN TO_NUMBER(REPLACE(regexp_replace(NVL(T261.A261_2DDD_CONT,''), '[^0-9]', ''),' ','')) IS NULL OR TO_NUMBER(REPLACE(regexp_replace(NVL(T261.A261_2DDD_CONT,''), '[^0-9]', ''),' ','')) = 0 
            THEN T018.A018_TEL2_CLI
            ELSE T261.A261_CEL_CONT
          END FONECEL,
          T018.A018_SITE,
          T039.A039_PORTE,
          CASE WHEN T039.A039_PORTE = 'EI' THEN 'Microempreendedor Individual'
               WHEN T039.A039_PORTE = 'ME' THEN 'Micro Empresa'
               WHEN T039.A039_PORTE = 'PQ' THEN 'Pequena Empresa'
               ELSE ''
          END Perfil,
          T048.A048_DSC_SETOR,
          T011C.A011_NM_CID,
          T021C.A021_SGL_EST
  FROM T014_CONTATO_CLIENTE T014
  JOIN T018_CLI_CADAST T018 ON T018.A012_CD_CLI = T014.A012_CD_CLI
  JOIN T011_CIDADE T011 ON T011.A011_CD_CID = T018.A011_CD_CID
   AND T011.A035_CD_PAIS = T018.A035_CD_PAIS
   AND T011.A021_CD_EST = T018.A021_CD_EST
  JOIN T021_ESTADO T021 ON T021.A021_CD_EST = T011.A021_CD_EST
   AND T021.A035_CD_PAIS = T011.A035_CD_PAIS
  JOIN T039_PESSOA_JUR T039 ON T039.A012_CD_CLI = T018.A012_CD_CLI
  JOIN T262_DAD_AD_CONT T262 ON T262.A261_CD_CONT = T014.A261_CD_CONT
  JOIN T261_CONTATO T261 ON T261.A261_CD_CONT =T262.A261_CD_CONT
  JOIN T048_SETOR T048 ON T048.A048_CD_SETOR = T039.A048_CD_SETOR
  JOIN T011_CIDADE T011C ON T011C.A011_CD_CID = T262.A011_CD_CID
   AND T011C.A035_CD_PAIS = T262.A035_CD_PAIS
   AND T011C.A021_CD_EST = T262.A021_CD_EST
  JOIN T021_ESTADO T021C ON T021C.A021_CD_EST = T011C.A021_CD_EST
   AND T021C.A035_CD_PAIS = T011C.A035_CD_PAIS
 WHERE T014.A261_CD_CONT = " + A261_CD_CONT + @"
   AND T262.A262_EMAIL IS NOT NULL
   AND T018.A018_TP_CLI <> 1
   AND NVL(T014.A014_IND_DESAT,0) = 0
   AND NVL(T018.A018_IND_DES_CLI,0) = 0
ORDER BY T014.A014_DT_INC DESC";
                                #endregion

                                using (var rdContato = Classes.DataBase.ExecuteReader(CommandType.Text, getDados))
                                {
                                    if (rdContato.Read())
                                    {
                                        string parsedContent = rdContato["SJSON"].ToString();
                                        parsedContent = parsedContent.Replace("Codigo", "Código");
                                        parsedContent = parsedContent.Replace("C?digo", "Código");
                                        rdContato.Close();

                                        if (!String.IsNullOrEmpty(parsedContent))
                                        {
                                            try
                                            {
                                                ServicePointManager.ServerCertificateValidationCallback += ValidateServerCertificate;

                                                var http = (HttpWebRequest)WebRequest.Create(new Uri("https://www.rdstation.com.br/api/1.3/conversions"));
                                                http.ProtocolVersion = HttpVersion.Version10;
                                                http.ContentType = "application/json";
                                                http.Method = "POST";
                                                http.Timeout = 300000;
                                                http.Credentials = CredentialCache.DefaultCredentials;

                                                //ASCIIEncoding encoding = new ASCIIEncoding();
                                                UTF8Encoding encoding = new UTF8Encoding();
                                                Byte[] bytes = encoding.GetBytes(parsedContent);

                                                Stream newStream = http.GetRequestStream();
                                                newStream.Write(bytes, 0, bytes.Length);
                                                newStream.Close();

                                                var response = http.GetResponse();

                                                var stream = response.GetResponseStream();
                                                var sr = new StreamReader(stream);
                                                var content = sr.ReadToEnd();

                                                http.Abort();
                                                response.Close();
                                                stream.Close();
                                                sr.Close();
                                            }
                                            catch (Exception exEnvioConversao)
                                            {
                                                GravaLOG(0, Convert.ToInt32(A261_CD_CONT), "sendLead", ("A261_CD_CONT informado.: " + A261_CD_CONT + " Erro ao enviar Lead.: " + exEnvioConversao.Message));
                                                //SendEmail("A261_CD_CONT informado.: " + A261_CD_CONT + " Erro ao enviar Lead.: "+exEnvioConversao.Message, "sendlead", "Integracao RD-Station via POST - ERRO");
                                            }
                                        }
                                        else
                                        {
                                            GravaLOG(0, Convert.ToInt32(A261_CD_CONT), "sendLead", ("A261_CD_CONT informado.: " + A261_CD_CONT + " Não retornou JSON de Contato"));
                                            //SendEmail("A261_CD_CONT informado.: " + A261_CD_CONT + " Não retornou JSON de Contato", "sendlead", "Integracao RD-Station via POST - ERRO");
                                        }
                                    }
                                    else
                                    {
                                        GravaLOG(0, Convert.ToInt32(A261_CD_CONT), "sendLead", ("A261_CD_CONT informado.: " + A261_CD_CONT + " Não retornou dados de Contato"));
                                        //SendEmail("A261_CD_CONT informado.: " + A261_CD_CONT + " Não retornou dados de Contato", "sendlead", "Integracao RD-Station via POST - ERRO");
                                    }
                                }
                            }
                            catch (Exception exDadosdeContato)
                            {
                                rdrCLI.Close();
                                GravaLOG(0, Convert.ToInt32(A261_CD_CONT), "sendLead", ("A261_CD_CONT informado.: " + A261_CD_CONT + " Erro dados de Contato.: " + exDadosdeContato.Message));
                                //SendEmail("A261_CD_CONT informado.: " + A261_CD_CONT + " Erro dados de Contato.: "+exDadosdeContato.Message, "sendlead", "Integracao RD-Station via POST - ERRO");
                            }
                            rdrCLI.Close();
                        }
                        else
                        {
                            GravaLOG(0, Convert.ToInt32(A261_CD_CONT), "sendLead", "A261_CD_CONT informado.: " + A261_CD_CONT + " não resultou registros no SIA");
                            //SendEmail("A261_CD_CONT informado.: " + A261_CD_CONT + " não resultou registros no SIA", "sendlead", "Integracao RD-Station via POST - ERRO");
                        }
                    }
                }
                else
                {
                    GravaLOG(0, Convert.ToInt32(A261_CD_CONT), "sendLead", "A261_CD_CONT não informado.: " + A261_CD_CONT);
                    //SendEmail("A261_CD_CONT não informado.: " + A261_CD_CONT, "sendlead", "Integracao RD-Station via POST - ERRO");
                }
                return "OK";
            }
            catch (Exception ex)
            {
                string msgErro = "Contato.: " + A261_CD_CONT + " - Exception.: " + ex.Message;
                try
                {
                    SendEmail(msgErro, "sendlead", "Integracao RD-Station via POST - ERRO");
                    //string _comando = "insert into sebrae.t1500_email (a1500_email,a1500_de,a1500_para,a1500_assunto,a1500_msg,a1500_enviado,a1500_dt_inc,a1500_dt_alt) values ";
                    //_comando += " (nvl((select max(a1500_email)+1 from sebrae.t1500_email),0) ,'web@sc.sebrae.com.br','luan@esfera.com.br','Integracao RD-Station via POST - ERRO','" + ex.Message.Replace("'", "''") + "',0,sysdate,sysdate)";
                    //OracleDataReader dr2 = Classes.DataBase.ExecuteReader(CommandType.Text, _comando);
                    //dr2.Close();
                }
                catch (Exception ex2)
                {
                    msgErro += " sendlead " + ex2.Message;
                    string _comando = "insert into sebrae.t1500_email (a1500_email,a1500_de,a1500_para,a1500_assunto,a1500_msg,a1500_enviado,a1500_dt_inc,a1500_dt_alt) values ";
                    _comando += " (nvl((select max(a1500_email)+1 from sebrae.t1500_email),0) ,'web@sc.sebrae.com.br','luan@esfera.com.br','Integracao RD-Station via POST - ERRO','" + msgErro.Replace("'", "''") + "',0,sysdate,sysdate)";
                    OracleDataReader dr2 = Classes.DataBase.ExecuteReader(CommandType.Text, _comando);
                    dr2.Close();
                }
                return "OK";
            }
        }

        [AcceptVerbs("GET")]
        [Route("api/sendconversion/{A022_CD_EV}/{A012_CD_CLI}/{A261_CD_CONT}")]
        public string SendConversion(string A022_CD_EV, string A012_CD_CLI, string A261_CD_CONT)
        {
            try
            {
                if ((!String.IsNullOrEmpty(A022_CD_EV) && !String.IsNullOrWhiteSpace(A022_CD_EV)) &&
                    (!String.IsNullOrEmpty(A012_CD_CLI) && !String.IsNullOrWhiteSpace(A012_CD_CLI)) &&
                    (!String.IsNullOrEmpty(A261_CD_CONT) && !String.IsNullOrWhiteSpace(A261_CD_CONT)))
                {
                    string A014_NUM_CONT = "";

                    #region Querie para Obter Dados de Conversion para RD
                    string getDados = @"SELECT '{""token_rdstation"":""7aea823368c80461ecd7857485c8d802"",""identificador"" : ""titulo_'||T022.A008_CD_TIT_EV||'"",
""email"" : ""'||REPLACE(T262.A262_EMAIL,'''','')||'""}' SJSON, T197.A014_NUM_CONT, 'titulo_'||T022.A008_CD_TIT_EV Titulo, REPLACE(T262.A262_EMAIL,'''','') Email
  FROM T197_PARTIC_EV_FECH T197
  JOIN T022_EVENTOS T022 ON T022.A022_CD_EV = T197.A022_CD_EV
  JOIN T018_CLI_CADAST T018 ON T018.A012_CD_CLI = T197.A012_CD_CLI
  JOIN T014_CONTATO_CLIENTE T014 ON T014.A012_CD_CLI = T018.A012_CD_CLI
   AND T014.A014_NUM_CONT = T197.A014_NUM_CONT
  JOIN T262_DAD_AD_CONT T262 ON T262.A261_CD_CONT = T014.A261_CD_CONT
 WHERE T197.A022_CD_EV = " + A022_CD_EV + @"
   AND T197.A012_CD_CLI = " + A012_CD_CLI + @"
   AND T014.A261_CD_CONT = " + A261_CD_CONT + @"
   AND T018.A018_TP_CLI <> 1
   AND T262.A262_EMAIL IS NOT NULL";
                    #endregion

                    #region Envia Conversão
                    using (var rdContato = Classes.DataBase.ExecuteReader(CommandType.Text, getDados))
                    {
                        try
                        {
                            if (rdContato.Read())
                            {
                                string parsedContent = rdContato["SJSON"].ToString();
                                A014_NUM_CONT = rdContato["A014_NUM_CONT"].ToString();

                                if (!String.IsNullOrEmpty(parsedContent))
                                {
                                    ServicePointManager.ServerCertificateValidationCallback += ValidateServerCertificate;

                                    var http = (HttpWebRequest)WebRequest.Create(new Uri("https://www.rdstation.com.br/api/1.3/conversions"));
                                    http.ProtocolVersion = HttpVersion.Version10;
                                    http.ContentType = "application/json";
                                    http.Method = "POST";
                                    http.Timeout = 300000;
                                    http.Credentials = CredentialCache.DefaultCredentials;

                                    //ASCIIEncoding encoding = new ASCIIEncoding();
                                    UTF8Encoding encoding = new UTF8Encoding();
                                    Byte[] bytes = encoding.GetBytes(parsedContent);

                                    Stream newStream = http.GetRequestStream();
                                    newStream.Write(bytes, 0, bytes.Length);
                                    newStream.Close();

                                    var response = http.GetResponse();

                                    var stream = response.GetResponseStream();
                                    var sr = new StreamReader(stream);
                                    var content = sr.ReadToEnd();

                                    http.Abort();
                                    response.Close();
                                    stream.Close();
                                    sr.Close();

                                    try
                                    {
                                        string msg = "Envio de Email de Convers&atilde;o - T&iacute;tulo de Evento<br>";
                                        msg += "Evento.: " + A022_CD_EV + "<br>";
                                        msg += "Título.: " + rdContato["Titulo"].ToString() + "<br>";
                                        msg += "Cód Cliente.: " + A012_CD_CLI + "<br>";
                                        msg += "Cód Contato.: " + A261_CD_CONT + "<br>";
                                        msg += "Email.: " + rdContato["Email"].ToString() + "<br>";

                                        #region Email para Alexandre
                                        try
                                        {
                                            string login = "AKIAJTUN5CXPLBDKTQDQ";
                                            string senha = "AgAEpwszssBciD0Snt78Pi4LBoKKiRM38auLfTQ2oVr0";
                                            string smtp = "email-smtp.us-east-1.amazonaws.com";
                                            int port = 25;
                                            bool ssl = true;

                                            var mensagem = new MailMessage();
                                            mensagem.From = new MailAddress("web@sc.sebrae.com.br");
                                            //mensagem.To.Add(new MailAddress("luan@esfera.com.br"));
                                            mensagem.To.Add(new MailAddress("alexandre@sc.sebrae.com.br"));
                                            mensagem.Subject = "Integra&ccedil;&atilde;o RD - Convers&atilde;o - T&iacute;tulo Evento";
                                            mensagem.Body = msg;
                                            mensagem.IsBodyHtml = true;

                                            var mailClient = new SmtpClient(smtp);
                                            //mailClient.Timeout = Convert.ToInt32(TimeSpan.FromMinutes(5).TotalMilliseconds);
                                            //mailClient.UseDefaultCredentials = false;
                                            mailClient.Credentials = new NetworkCredential(login, senha);

                                            if (port > 0)
                                                mailClient.Port = port;

                                            mailClient.EnableSsl = ssl;

                                            mailClient.Send(mensagem);
                                        }
                                        catch
                                        {
                                            string _comando = "insert into sebrae.t1500_email (a1500_email,a1500_de,a1500_para,a1500_assunto,a1500_msg,a1500_enviado,a1500_dt_inc,a1500_dt_alt) values ";
                                            _comando += " (nvl((select max(a1500_email)+1 from sebrae.t1500_email),0) ,'web@sc.sebrae.com.br','alexandre@sc.sebrae.com.br','Integra&ccedil;&atilde;o RD - Convers&atilde;o - T&iacute;tulo Evento','" + msg.Replace("'", "`") + "',0,sysdate,sysdate)";
                                            OracleDataReader dr2 = Classes.DataBase.ExecuteReader(CommandType.Text, _comando);
                                            dr2.Close();

                                            //_comando = "insert into sebrae.t1500_email (a1500_email,a1500_de,a1500_para,a1500_assunto,a1500_msg,a1500_enviado,a1500_dt_inc,a1500_dt_alt) values ";
                                            //_comando += " (nvl((select max(a1500_email)+1 from sebrae.t1500_email),0) ,'web@sc.sebrae.com.br','luan@esfera.com.br','Integração RD - Conversão - Titulo Evento','" + msg.Replace("'", "`") + "',0,sysdate,sysdate)";
                                            //dr2 = Classes.DataBase.ExecuteReader(CommandType.Text, _comando);
                                            //dr2.Close();
                                        }
                                        #endregion
                                        #region Comando Insert
                                        string insere = @"INSERT INTO CONVERSOES_RDSTATION SELECT " + A022_CD_EV + @"
," + A012_CD_CLI + @"," + A261_CD_CONT + @"," + A014_NUM_CONT + @",'" + parsedContent + @"',systimestamp FROM DUAL";
                                        #endregion

                                        var rds = Classes.DataBase.ExecuteReader(CommandType.Text, insere);
                                        rds.Close();
                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                        }
                        finally
                        {
                            rdContato.Close();
                        }
                    }
                    #endregion
                }
                else
                {
                    string msg = "Tentativa de Envio de Conversão sem Evento, Cliente ou Contato";

                    string _comando = "insert into sebrae.t1500_email (a1500_email,a1500_de,a1500_para,a1500_assunto,a1500_msg,a1500_enviado,a1500_dt_inc,a1500_dt_alt) values ";
                    _comando += " (nvl((select max(a1500_email)+1 from sebrae.t1500_email),0) ,'web@sc.sebrae.com.br','luan@esfera.com.br','ERRO Integração RD - SendConversion','" + msg.Replace("'", "`") + "',0,sysdate,sysdate)";
                    OracleDataReader dr2 = Classes.DataBase.ExecuteReader(CommandType.Text, _comando);
                    dr2.Close();

                    Console.WriteLine(msg);

                    return "NOK";
                }
            }
            catch (Exception ex)
            {
                string msgErro = ex.Message;
                try
                {
                    SendEmail(ex.Message, "sendconversion", "Integracao RD-Station via POST - ERRO");
                    //string _comando = "insert into sebrae.t1500_email (a1500_email,a1500_de,a1500_para,a1500_assunto,a1500_msg,a1500_enviado,a1500_dt_inc,a1500_dt_alt) values ";
                    //_comando += " (nvl((select max(a1500_email)+1 from sebrae.t1500_email),0) ,'web@sc.sebrae.com.br','luan@esfera.com.br','Integracao RD-Station via POST - ERRO','" + ex.Message.Replace("'", "''") + "',0,sysdate,sysdate)";
                    //OracleDataReader dr2 = Classes.DataBase.ExecuteReader(CommandType.Text, _comando);
                    //dr2.Close();
                }
                catch (Exception ex2)
                {
                    msgErro += " sendconversion " + ex2.Message;
                    string _comando = "insert into sebrae.t1500_email (a1500_email,a1500_de,a1500_para,a1500_assunto,a1500_msg,a1500_enviado,a1500_dt_inc,a1500_dt_alt) values ";
                    _comando += " (nvl((select max(a1500_email)+1 from sebrae.t1500_email),0) ,'web@sc.sebrae.com.br','luan@esfera.com.br','Integracao RD-Station via POST - ERRO','" + msgErro.Replace("'", "''") + "',0,sysdate,sysdate)";
                    OracleDataReader dr2 = Classes.DataBase.ExecuteReader(CommandType.Text, _comando);
                    dr2.Close();
                }
            }

            return "OK";
        }

        [NonAction]
        public static bool ValidateServerCertificate(
        object sender,
        X509Certificate certificate,
        X509Chain chain,
        SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;

            Console.WriteLine("Certificate error: {0}", sslPolicyErrors);

            // Do not allow this client to communicate with unauthenticated servers. 
            return false;
        }

        [NonAction]
        public void SendEmail(string message, string route, string subject)
        {
            try
            {
                string login = "AKIAJTUN5CXPLBDKTQDQ";
                string senha = "AgAEpwszssBciD0Snt78Pi4LBoKKiRM38auLfTQ2oVr0";
                string smtp = "email-smtp.us-east-1.amazonaws.com";
                int port = 25;
                bool ssl = true;

                var mensagem = new MailMessage();
                mensagem.From = new MailAddress("web@sc.sebrae.com.br");
                mensagem.To.Add(new MailAddress("luan@esfera.com.br"));

                mensagem.Subject = subject;
                if (!String.IsNullOrEmpty(route))
                    mensagem.Body = "<p>Erro na Rota.: " + route + "</p><p>" + message + "</p>";
                else
                    mensagem.Body = "<p>" + message + "</p>";
                mensagem.IsBodyHtml = true;

                var mailClient = new SmtpClient(smtp);
                //mailClient.Timeout = Convert.ToInt32(TimeSpan.FromMinutes(5).TotalMilliseconds);
                //mailClient.UseDefaultCredentials = false;
                mailClient.Credentials = new NetworkCredential(login, senha);

                if (port > 0)
                    mailClient.Port = port;

                mailClient.EnableSsl = ssl;

                mailClient.Send(mensagem);
            }
            catch (Exception emExc)
            {
                string msgErro = route + " <br> " + message + " -> Exception SendEmail SMTP.: <br> " + emExc.Message;

                if (!String.IsNullOrEmpty(route))
                    msgErro = "Erro na Rota.: " + route + " " + msgErro;

                try
                {
                    string _comando = "insert into sebrae.t1500_email (a1500_email,a1500_de,a1500_para,a1500_assunto,a1500_msg,a1500_enviado,a1500_dt_inc,a1500_dt_alt) values ";
                    _comando += " (nvl((select max(a1500_email)+1 from sebrae.t1500_email),0) ,'web@sc.sebrae.com.br','luan@esfera.com.br','Integracao RD-Station via POST - ERRO','" + msgErro.Replace("'", "''") + "',0,sysdate,sysdate)";
                    OracleDataReader dr2 = Classes.DataBase.ExecuteReader(CommandType.Text, _comando);
                    dr2.Close();

                    _comando += " (nvl((select max(a1500_email)+1 from sebrae.t1500_email),0) ,'web@sc.sebrae.com.br','alexandre@sc.sebrae.com.br','"+subject+"','" + message.Replace("'", "''") + "',0,sysdate,sysdate)";
                    dr2 = Classes.DataBase.ExecuteReader(CommandType.Text, _comando);
                    dr2.Close();
                }
                catch (Exception sendEmail)
                {
                    msgErro += " - Exception Envio de Email Oracle.: " + sendEmail.Message;
                    string _comando = "insert into sebrae.t1500_email (a1500_email,a1500_de,a1500_para,a1500_assunto,a1500_msg,a1500_enviado,a1500_dt_inc,a1500_dt_alt) values ";
                    _comando += " (nvl((select max(a1500_email)+1 from sebrae.t1500_email),0) ,'web@sc.sebrae.com.br','luan@esfera.com.br','Integracao RD-Station via POST - ERRO','" + msgErro.Replace("'", "''") + "',0,sysdate,sysdate)";
                    OracleDataReader dr2 = Classes.DataBase.ExecuteReader(CommandType.Text, _comando);
                    dr2.Close();
                }
            }
        }

        [NonAction]
        public void GravaLOG(int A012_CD_CLI, int A261_CD_CONT, string route, string message)
        {
            string sSql = "";
            try
            {
                sSql = "INSERT INTO T000_LOG_ERRO_RDSTATION SELECT ";
                if (A261_CD_CONT > 0)
                    sSql += A261_CD_CONT + ",";
                else
                    sSql += "0,";
                if (A012_CD_CLI > 0)
                    sSql += A012_CD_CLI + ",";
                else
                    sSql += "0,";

                sSql += "'" + route + "','" + message + "', systimestamp FROM DUAL";

                var drIns = Classes.DataBase.ExecuteReader(CommandType.Text, sSql);
                drIns.Close();
            }
            catch (Exception exErroInsLog)
            {
                string msgErro = "ERRO ao tentar Gravar o Log.: " + exErroInsLog.Message + " -- " + sSql;
                msgErro += " " + (!String.IsNullOrEmpty(route) ? route : "N/A");

                SendEmail(msgErro, route, "Erro ao Gravar LOG");
            }
        }

        [Route("api/testaSmtpEmail")]
        [AcceptVerbs("GET")]
        public string TestaEmail()
        {
            string to = "luan@esfera.com.br";
            string from = "web@sc.sebrae.com.br";
            string subject = "Using the new SMTP client.";
            string body = @"Teste SMTP Sem TimeOut";

            string login = "AKIAJTUN5CXPLBDKTQDQ";
            string senha = "AgAEpwszssBciD0Snt78Pi4LBoKKiRM38auLfTQ2oVr0";
            string smtp = "email-smtp.us-east-1.amazonaws.com";
            int port = 25;
            bool ssl = true;

            MailMessage message = new MailMessage(from, to, subject, body);
            SmtpClient client = new SmtpClient(smtp);
            Console.WriteLine("Changing time out from {0} to 100.", client.Timeout);
            //client.Timeout = 100;
            client.Credentials = new NetworkCredential(login, senha);
            client.Timeout = Convert.ToInt32(TimeSpan.FromMinutes(5).TotalMilliseconds);
            if (port > 0)
                client.Port = port;

            client.EnableSsl = ssl;

            string respostas = "";

            try
            {
                client.Send(message);
                respostas += "{Success:true, Message='Email enviado: Teste SMTP Sem TimeOut com SSL'},";
            }
            catch (Exception ex)
            {
                respostas += "{Success:false, Message='Erro ao tentar enviar email: Teste SMTP Sem TimeOut com SSL --> "+ex.Message+"'},";
            }

            try
            {
                client.Timeout = 300000; //5 minutos
                client.Credentials = new NetworkCredential(login, senha);
                client.Timeout = Convert.ToInt32(TimeSpan.FromMinutes(5).TotalMilliseconds);
                if (port > 0)
                    client.Port = port;

                client.EnableSsl = ssl;

                client.Send(message);
                respostas += "{Success:true, Message='Email enviado: Teste SMTP Com TimeOut com SSL'},";
            }
            catch (Exception ex)
            {
                respostas += "{Success:false, Message='Erro ao tentar enviar email: Teste SMTP Com TimeOut com SSL --> " + ex.Message + "'},";
            }

            try
            {
                //client.Timeout = 300000; //5 minutos
                client.Credentials = new NetworkCredential(login, senha);
                client.Timeout = Convert.ToInt32(TimeSpan.FromMinutes(5).TotalMilliseconds);
                if (port > 0)
                    client.Port = port;

                client.EnableSsl = false;

                client.Send(message);
                respostas += "{Success:true, Message='Email enviado: Teste SMTP Sem TimeOut Sem SSL'},";
            }
            catch (Exception ex)
            {
                respostas += "{Success:false, Message='Erro ao tentar enviar email: Teste SMTP Sem TimeOut Sem SSL --> " + ex.Message + "'},";
            }

            try
            {
                client.Timeout = 300000; //5 minutos
                client.Credentials = new NetworkCredential(login, senha);
                client.Timeout = Convert.ToInt32(TimeSpan.FromMinutes(5).TotalMilliseconds);
                if (port > 0)
                    client.Port = port;

                client.EnableSsl = false;

                client.Send(message);
                respostas += "{Success:true, Message='Email enviado: Teste SMTP Com TimeOut Sem SSL'},";
            }
            catch (Exception ex)
            {
                respostas += "{Success:false, Message='Erro ao tentar enviar email: Teste SMTP Com TimeOut Sem SSL --> " + ex.Message + "'}";
            }

            respostas = "[" + respostas + "]";

            return respostas;
        }
    }
}
