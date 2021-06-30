using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IntegradorAtendimentosRDStation.Models;
using Microsoft.Web.Services2.Messaging;
using Newtonsoft.Json;

namespace IntegradorAtendimentosRDStation.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            LoadData(null);
            ViewBag.Title = "Home Page";
            return View(new IntegradorModel() { SEQUENCIAL = 0});
        } 

        public ActionResult LeadsParticipacoes(FiltroLeadsParticipacoesEventosModel filtro)
        {
            var model = new FiltroLeadsParticipacoesEventosModel();
            if (filtro != null)
            {
                model = filtro;
            }
            model.Load(filtro);

            var listaTitulos = new List<TitulosModel>();

            using (var drTIT = Classes.DataBase.ExecuteReader(CommandType.Text, "SELECT T008.A008_CD_TIT_EV, T008.A008_CD_TIT_EV||' - ' || T008.A008_TIT_EV A008_TIT_EV FROM T008_TIT_EVENTO T008 WHERE NVL(A008_IND_DESAT,0) = 0"))
            {
                while (drTIT.Read())
	            {
	                listaTitulos.Add(new TitulosModel(){
                        A008_CD_TIT_EV = Convert.ToInt32(drTIT["A008_CD_TIT_EV"].ToString()),
                        A008_TIT_EV = drTIT["A008_TIT_EV"].ToString()
                    });
	            }
                drTIT.Close();
            }

             ViewBag.Titulos = new SelectList(listaTitulos.OrderBy(s => s.A008_TIT_EV).ToList(),
               "A008_CD_TIT_EV",
               "A008_TIT_EV",
               (filtro != null ? filtro.codTituloEvento : ""));

            return View(model);
        }

        [HttpPost]
        public ActionResult GravarAPI(IntegradorModel model)
        {
            if (ModelState.IsValid)
            {
                #region CODOBJ - CODSOL
                string CODSOL = "";
                string CODOBJ = "";

                string qProjAcao = @"SELECT upper(titsol) Projeto,
				 T774.CodSOL,
				 T774.CodObj,
				 T772.COD_PROJETO_SGE,
         T772.TITOBJ,
         NVL(T772.CODACAO_SEQ,0) CODACAO_SEQ
				 FROM  
				 T774_SOL_OBJ_AE T774,
				 T773_SOLUCAO T773,
				 T772_OBJETIVO T772
				 Where 
				 T774.codsol = T773.codsol and
				 T774.codobj = T772.codobj and
				 T773.zm_Ano = T774.ano and
				 T772.ctt_ano = T774.ano and
                 T772.ctt_ano >= (SELECT A077_ANO_REF FROM T077_MES_REF)
                 AND T772.COD_PROJETO_SGE ='" + model.COD_PROJETO_SGE + @"'
                 AND T772.CODACAO_SEQ ='" + model.COD_ACAO_SEQ + @"'
            ORDER BY upper(titsol)";

                using (var rdrAcoes = Classes.DataBase.ExecuteReader(CommandType.Text, qProjAcao))
                {
                    while (rdrAcoes.Read())
                    {
                        CODOBJ = rdrAcoes["CODOBJ"].ToString();
                        CODSOL = rdrAcoes["CODSOL"].ToString();
                    }
                    rdrAcoes.Close();
                }
                model.CODOBJ = CODOBJ;
                model.CODSOL = CODSOL;
                #endregion

                model.USUARIO_INC = (model.SEQUENCIAL > 0 ? "" : usuarioLogadoNome);
                model.USUARIO_ALT = usuarioLogadoNome;
                try
                {
                    model.Gravar();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao processar sua solicitação.: "+ex.Message);

                    LoadData(model);
                    return View("Index", model);
                }
                
                return RedirectToAction("Index");    
            }
            else
            {
                LoadData(model);
                return View("Index", model);
            }
        }

        public void LoadData(IntegradorModel integrador)
        {
            #region Integradores
            IList<IntegradorModel> all = new List<IntegradorModel>();
            string sSqlIntegradores = @"SELECT DISTINCT I.SEQUENCIAL, I.API, I.CATEGORIAATENDIMENTO, I.TIPOATENDIMENTO, I.A001_OBS, I.CODSOL, I.CODOBJ, I.COD_PROJETO_SGE, I.COD_ACAO_SEQ, I.DT_INC, I.USUARIO_INC, I.DT_ALT, I.USUARIO_ALT, I.ATIVO, T773.TITSOL PROJETO, T772.TITOBJ ACAO, T773.ZM_ANO, COUNT(DISTINCT LI.A001_NUM_SEQUENCIAL) CONVERSOES
  FROM INTEGRADOR_RDSTATION I
  JOIN T773_SOLUCAO T773 ON T773.COD_PROJETO_SGE = I.COD_PROJETO_SGE
   AND T773.CODSOL = I.CODSOL
  JOIN T772_OBJETIVO T772 ON T772.COD_PROJETO_SGE = T773.COD_PROJETO_SGE
   AND T772.CODACAO_SEQ = I.COD_ACAO_SEQ
   AND T772.CTT_ANO = T773.ZM_ANO
LEFT JOIN LOG_INTEGRADOR_RDSTATION LI ON LI.SEQUENCIAL_INTEGRADOR = I.SEQUENCIAL   
 WHERE T773.ZM_ANO >= " + DateTime.Now.Year + @"  
GROUP BY I.SEQUENCIAL, I.API,  I.CATEGORIAATENDIMENTO, I.TIPOATENDIMENTO, I.A001_OBS, I.CODSOL, I.CODOBJ, I.COD_PROJETO_SGE, I.COD_ACAO_SEQ, I.DT_INC, I.USUARIO_INC, I.DT_ALT, I.USUARIO_ALT, I.ATIVO, T773.TITSOL, T772.TITOBJ, T773.ZM_ANO
ORDER BY T773.ZM_ANO DESC";
            using (var rdr = Classes.DataBase.ExecuteReader(CommandType.Text, sSqlIntegradores))
            {
                while (rdr.Read())
                {
                    all.Add(new IntegradorModel()
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
                        CONVERSOES = Convert.ToInt32(rdr["CONVERSOES"].ToString())
                    });
                }
                rdr.Close();
            }

            ViewBag.Integradores = all.Count() > 0 ? all.OrderBy(m => m.API).ToList() : all.ToList();
            #endregion

            #region Projetos
            IList<ProjetosModel> lstProjetos = new List<ProjetosModel>();
            
            string querieProjetos = @"SELECT upper(titsol) Projeto,
				 T774.CodSOL,
				 T772.COD_PROJETO_SGE
				 FROM  
				 T774_SOL_OBJ_AE T774,
				 T773_SOLUCAO T773,
				 T772_OBJETIVO T772
				 Where 
				 T774.codsol = T773.codsol and
				 T774.codobj = T772.codobj and
				 T773.zm_Ano = T774.ano and
				 T772.ctt_ano = T774.ano and
         T772.ctt_ano >= (SELECT A077_ANO_REF FROM T077_MES_REF)
GROUP BY upper(titsol),
				 T774.CodSOL,
				 T772.COD_PROJETO_SGE         
ORDER BY upper(titsol)";

            using (var rdrProjetos = Classes.DataBase.ExecuteReader(CommandType.Text, querieProjetos))
            {
                while (rdrProjetos.Read())
                {
                    lstProjetos.Add(new ProjetosModel()
                    {
                        COD_PROJETO_SGE = rdrProjetos["COD_PROJETO_SGE"].ToString(),
                        CODSOL = rdrProjetos["CODSOL"].ToString(),
                        Nome = rdrProjetos["Projeto"].ToString()
                    });
                }
                rdrProjetos.Close();
            }
            #endregion

            #region Ações
            IList<AcoesModel> lstAcoes = new List<AcoesModel>();

            string projetoFilter = integrador != null ? integrador.COD_PROJETO_SGE : lstProjetos.FirstOrDefault().COD_PROJETO_SGE;

            string querieAcoes = @"SELECT upper(titsol) Projeto,
				 T774.CodSOL,
				 T774.CodObj,
				 T772.COD_PROJETO_SGE,
         T772.TITOBJ,
         NVL(T772.CODACAO_SEQ,0) COD_ACAO_SEQ
				 FROM  
				 T774_SOL_OBJ_AE T774,
				 T773_SOLUCAO T773,
				 T772_OBJETIVO T772
				 Where 
				 T774.codsol = T773.codsol and
				 T774.codobj = T772.codobj and
				 T773.zm_Ano = T774.ano and
				 T772.ctt_ano = T774.ano and
                 T772.ctt_ano >= (SELECT A077_ANO_REF FROM T077_MES_REF) AND T772.COD_PROJETO_SGE ='" + projetoFilter + @"'
ORDER BY upper(titsol)";

            using (var rdrAcoes = Classes.DataBase.ExecuteReader(CommandType.Text, querieAcoes))
            {
                while (rdrAcoes.Read())
                {
                    lstAcoes.Add(new AcoesModel()
                    {
                        COD_PROJETO_SGE = rdrAcoes["COD_PROJETO_SGE"].ToString(),
                        CODOBJ = rdrAcoes["CODOBJ"].ToString(),
                        Nome = rdrAcoes["TITOBJ"].ToString(),
                        Cod_ACAO_SEQ = Convert.ToInt32(rdrAcoes["COD_ACAO_SEQ"].ToString())
                    });
                }
                rdrAcoes.Close();
            }
            #endregion

            #region Categorias
            IList<CategoriaAtendimentoModel> listaCategorias = new List<CategoriaAtendimentoModel>();

            string sSqlC = "SELECT * FROM T784_CATEGORIA";
            using (var rdrCateg = Classes.DataBase.ExecuteReader(CommandType.Text, sSqlC))
            {
                while (rdrCateg.Read())
                {
                    listaCategorias.Add(new CategoriaAtendimentoModel()
                    {
                        Codigo = Convert.ToInt32(rdrCateg["A784_CD_CATEGORIA"].ToString()),
                        Nome = rdrCateg["A784_DSC_CATEGORIA"].ToString()
                    });
                }
                rdrCateg.Close();
            }
            #endregion

            #region Tipos de Atendimentos
            IList<TipoAtendimentoModel> listaTipoAtendimento = new List<TipoAtendimentoModel>();

            string sSqlTA = "SELECT * FROM T054_TIPO_ATEND";
            using (var rdrTA = Classes.DataBase.ExecuteReader(CommandType.Text, sSqlTA))
            {
                while (rdrTA.Read())
                {
                    listaTipoAtendimento.Add(new TipoAtendimentoModel()
                    {
                        Codigo = Convert.ToInt32(rdrTA["A054_CD_TP_AT"].ToString()),
                        Nome = rdrTA["A054_DSC_TP_AT"].ToString()
                    });
                }
                rdrTA.Close();
            }
            #endregion

            ViewBag.Projetos = new SelectList(lstProjetos.OrderBy(s => s.Nome).ToList(),
               "COD_PROJETO_SGE",
               "Nome",
               integrador != null ? integrador.COD_PROJETO_SGE : "");

            ViewBag.Acoes = new SelectList(lstAcoes.OrderBy(s => s.Nome).ToList(),
               "COD_ACAO_SEQ",
               "Nome",
               integrador != null ? integrador.COD_ACAO_SEQ.ToString() : "");

            ViewBag.Status = new SelectList(new SelectListItem[]
            {
                new SelectListItem { Text = "SIM", Value =  "1" },
                new SelectListItem { Text = "NÃO", Value =  "0" }
            }, "Value", "Text", integrador != null ? integrador.ATIVO.ToString() : "1");

            ViewBag.Categorias = new SelectList(listaCategorias.OrderBy(s => s.Nome).ToList(),
               "Codigo",
               "Nome",
               integrador != null ? integrador.CATEGORIAATENDIMENTO.ToString() : "");

            ViewBag.TipoAtendimento = new SelectList(listaTipoAtendimento.OrderBy(s => s.Nome).ToList(),
               "Codigo",
               "Nome",
               integrador != null ? integrador.TIPOATENDIMENTO.ToString() : "");
        }

        [HttpGet]
        public JsonResult GetAcoesPorProjeto(string COD_PROJETO_SGE)
        {
            #region Ações
            IList<AcoesModel> lstAcoes = new List<AcoesModel>();

            string querieAcoes = @"SELECT upper(titsol) Projeto,
				 T774.CodSOL,
				 T774.CodObj,
				 T772.COD_PROJETO_SGE,
         T772.TITOBJ,
         NVL(T772.CODACAO_SEQ,0) CODACAO_SEQ
				 FROM  
				 T774_SOL_OBJ_AE T774,
				 T773_SOLUCAO T773,
				 T772_OBJETIVO T772
				 Where 
				 T774.codsol = T773.codsol and
				 T774.codobj = T772.codobj and
				 T773.zm_Ano = T774.ano and
				 T772.ctt_ano = T774.ano and
                 T772.ctt_ano >= (SELECT A077_ANO_REF FROM T077_MES_REF)
                 AND T772.COD_PROJETO_SGE ='" + COD_PROJETO_SGE + @"'
            ORDER BY upper(titsol)";

            using (var rdrAcoes = Classes.DataBase.ExecuteReader(CommandType.Text, querieAcoes))
            {
                while (rdrAcoes.Read())
                {
                    lstAcoes.Add(new AcoesModel()
                    {
                        COD_PROJETO_SGE = rdrAcoes["COD_PROJETO_SGE"].ToString(),
                        CODOBJ = rdrAcoes["CODOBJ"].ToString(),
                        Nome = rdrAcoes["TITOBJ"].ToString(),
                        Cod_ACAO_SEQ = Convert.ToInt32(rdrAcoes["CODACAO_SEQ"].ToString())
                    });
                }
                rdrAcoes.Close();
            }
            #endregion

            var retorno = this.Json(new { Result = lstAcoes }, JsonRequestBehavior.AllowGet);
            return retorno;
        }

        [HttpGet]
        public JsonResult ObterIntegrador(int id)
        {
            string sSqlIntegradores = @"SELECT DISTINCT I.SEQUENCIAL, I.API, I.A001_OBS, I.CODSOL, I.CODOBJ, I.COD_PROJETO_SGE, I.COD_ACAO_SEQ, I.DT_INC, I.USUARIO_INC, I.DT_ALT, I.USUARIO_ALT, I.ATIVO, T773.TITSOL PROJETO, T772.TITOBJ ACAO, T773.ZM_ANO, COUNT(DISTINCT LI.A001_NUM_SEQUENCIAL) CONVERSOES, NVL(I.TIPOATENDIMENTO,0) TIPOATENDIMENTO, NVL(I.CATEGORIAATENDIMENTO,0) CATEGORIAATENDIMENTO
  FROM INTEGRADOR_RDSTATION I
  JOIN T773_SOLUCAO T773 ON T773.COD_PROJETO_SGE = I.COD_PROJETO_SGE
   AND T773.CODSOL = I.CODSOL
  JOIN T772_OBJETIVO T772 ON T772.COD_PROJETO_SGE = T773.COD_PROJETO_SGE
   AND T772.CODACAO_SEQ = I.COD_ACAO_SEQ
   AND T772.CTT_ANO = T773.ZM_ANO
LEFT JOIN LOG_INTEGRADOR_RDSTATION LI ON LI.SEQUENCIAL_INTEGRADOR = I.SEQUENCIAL   
 WHERE T773.ZM_ANO >= " + DateTime.Now.Year + @"  
   AND I.SEQUENCIAL = " + id + @"  
GROUP BY I.SEQUENCIAL, I.API, I.A001_OBS, I.CODSOL, I.CODOBJ, I.COD_PROJETO_SGE, I.COD_ACAO_SEQ, I.DT_INC, I.USUARIO_INC, I.DT_ALT, I.USUARIO_ALT, I.ATIVO, T773.TITSOL, T772.TITOBJ, T773.ZM_ANO, I.TIPOATENDIMENTO, I.CATEGORIAATENDIMENTO
ORDER BY T773.ZM_ANO DESC";

            IntegradorModel integrador = null;
            using (var rdr = Classes.DataBase.ExecuteReader(CommandType.Text, sSqlIntegradores))
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
                }
                rdr.Close();
            }

            var retorno = this.Json(new { Result = integrador }, JsonRequestBehavior.AllowGet);
            return retorno;
        }

        public JsonResult TESTESE()
        {
            var sc = new SoapClient("https://bpm.sebrae-sc.com.br/softexpert/webserviceproxy/se/ws/wf_ws.php");
        }


    }
}
