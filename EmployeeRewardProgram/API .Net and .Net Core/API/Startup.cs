using Database.Queries.APP;
using Database.Queries.Gestao;
using Database.Queries.Loja;
using Database.Queries.Venda;
using Domain.Helpers;
using Domain.Repositories.APP;
using Domain.Repositories.Gestao;
using Domain.Repositories.Loja;
using Domain.Repositories.Venda;
using Domain.Services;
using Domain.Services.APP;
using Domain.Services.Gestao;
using Domain.Services.Loja;
using Domain.Services.Venda;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ERwPHelpers;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERwP
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<IISServerOptions>(options =>
            {
                options.AutomaticAuthentication = false;
            });

            services.Configure<IISOptions>(options =>
            {
                options.ForwardClientCertificate = false;
            });

            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2); ;

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            //configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var signingKey = new SymmetricSecurityKey(key);

            _ = services.AddAuthentication(x =>
              {
                  x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                  x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
              })
            .AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var userService = context.HttpContext.RequestServices.GetRequiredService<ColaboradorService>();
                        string userId = context.Principal.Claims.Where(ctx => ctx.Type == "userId").FirstOrDefault().ToString();
                        userId = userId.Replace("userId: ", "").Trim();

                        string userCS = context.Principal.Claims.Where(ctx => ctx.Type == "userCS").FirstOrDefault().ToString();
                        userCS = userCS.Replace("userCS: ", "").Trim();

                        string aplicacaoId = context.Principal.Claims.Where(ctx => ctx.Type == "aplicacaoId").FirstOrDefault().ToString();
                        aplicacaoId = aplicacaoId.Replace("aplicacaoId: ", "").Trim();

                        if (!string.IsNullOrEmpty(userId))
                        {
                            var user = userService.ObterUsuarioOpcoesMenu(userCS, aplicacaoId);
                            if (user == null)
                            {
                                context.Fail("Não Autorizado");
                            }

                            string path = context.HttpContext.Request.Path;
                            if (!string.IsNullOrEmpty(path) && path.Split('/').Length >= 3)
                            {
                                string controllerName = path.Split('/')[1];
                                string actionName = path.Split('/')[2];
                                if (controllerName != "auth")
                                {
                                    var perfilMenuService = context.HttpContext.RequestServices.GetRequiredService<PerfilMenuDeNavegacaoService>();

                                    if (!perfilMenuService.ValidaAcesso(user.perfilId, controllerName, actionName))
                                    {
                                        context.Fail("Não Autorizado");
                                    }
                                }
                            }
                            else
                                context.Fail("Não foi Possível localizar esta rota");
                        }
                        else
                        {
                            context.Fail("Não Autorizado");
                        }

                        return Task.CompletedTask;
                    }
                };
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = signingKey,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(0)
                };
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //// configure DI for application services
            //services.AddTransient<IBaseService<Usuario>, UsuarioService>();
            //services.AddTransient<IBaseService<Aplicacao>, BaseService<Aplicacao>>();

            #region Injeção de Dependência

            #region APP

            #region Aplicacao
            services.AddSingleton<AplicacaoService>();
            services.AddSingleton<AplicacaoRepository>();
            services.AddSingleton<AplicacaoQueries>();
            #endregion

            #region Log
            services.AddSingleton<LogService>();
            services.AddSingleton<LogOperacoesRepository>();
            services.AddSingleton<LogOperacoesQueries>();
            services.AddSingleton<LogTransacoesRepository>();
            services.AddSingleton<LogTransacoesQueries>();
            #endregion

            #region Menu
            services.AddSingleton<MenuDeNavegacaoService>();
            services.AddSingleton<MenuDeNavegacaoRepository>();
            services.AddSingleton<MenuDeNavegacaoQueries>();
            #endregion

            #region Perfil Menu
            services.AddSingleton<PerfilMenuDeNavegacaoService>();
            services.AddSingleton<PerfilMenuDeNavegacaoRepository>();
            services.AddSingleton<PerfilMenuDeNavegacaoQueries>();
            #endregion

            #region Perfil
            services.AddSingleton<PerfilService>();
            services.AddSingleton<PerfilRepository>();
            services.AddSingleton<PerfilQueries>();
            #endregion

            #region Email

            services.AddSingleton<SendEmailService>();

            #endregion

            #region Auth

            services.AddSingleton<AuthService>();

            #endregion
            #endregion

            #region Gestao

            #region Avaliacao
            services.AddSingleton<AvaliacaoService>();
            services.AddSingleton<AvaliacaoRepository>();
            services.AddSingleton<AvaliacaoQueries>();
            #endregion

            #region Cargo
            services.AddSingleton<CargoService>();
            services.AddSingleton<CargoRepository>();
            services.AddSingleton<CargoQueries>();
            #endregion

            #region Colaborador
            services.AddTransient<ColaboradorService>();
            services.AddTransient<ColaboradorRepository>();
            services.AddTransient<ColaboradorQueries>();
            #endregion

            #region Configuração Verba / Expiração
            services.AddSingleton<ConfiguracaoExpiracaoService>();
            services.AddSingleton<ConfiguracaoExpiracaoRepository>();
            services.AddSingleton<ConfiguracaoExpiracaoPontosQueries>();

            services.AddSingleton<ConfiguracaoVerbaService>();
            services.AddSingleton<ConfiguracaoVerbaRepository>();
            services.AddSingleton<ConfiguracaoDistribuicaoVerbasQueries>();
            #endregion

            #region Expiração de Pontos do Colaborador

            services.AddSingleton<ExpiracaoPontosColaboradorService>();
            services.AddSingleton<ExpiracaoPontosColaboradorRepository>();
            services.AddSingleton<ExpiracaoPontosColaboradorQueries>();

            #endregion

            #region Loja
            services.AddSingleton<LojaService>();
            services.AddSingleton<LojaRepository>();
            services.AddSingleton<LojaQueries>();
            #endregion

            #region MeioDeCompra
            services.AddSingleton<MeioDeCompraService>();
            services.AddSingleton<MeioDeCompraRepository>();
            services.AddSingleton<MeioDeCompraQueries>();
            #endregion

            #region Recomendacao
            services.AddSingleton<RecomendacaoService>();
            services.AddSingleton<RecomendacaoRepository>();
            services.AddSingleton<RecomendacaoQueries>();
            #endregion

            #region SituacaoAvaliacao
            services.AddSingleton<SituacaoAvaliacaoService>();
            services.AddSingleton<SituacaoAvaliacaoRepository>();
            services.AddSingleton<SituacaoAvaliacaoQueries>();
            #endregion

            #region SituacaoRecomendacao
            services.AddSingleton<SituacaoRecomendacaoService>();
            services.AddSingleton<SituacaoRecomendacaoRepository>();
            services.AddSingleton<SituacaoRecomendacaoQueries>();
            #endregion

            #region TaxaConversao
            services.AddSingleton<TaxaConversaoService>();
            services.AddSingleton<TaxaConversaoRepository>();
            services.AddSingleton<TaxaConversaoQueries>();
            #endregion

            #region TipoRecomendacao
            services.AddSingleton<TipoRecomendacaoService>();
            services.AddSingleton<TipoRecomendacaoRepository>();
            services.AddSingleton<TipoRecomendacaoQueries>();
            #endregion
            
            #region Verba
            services.AddSingleton<VerbaService>();
            services.AddSingleton<VerbaRepository>();
            services.AddSingleton<VerbaQueries>();
            #endregion

            #endregion

            #region Loja

            #region OpcaoEntrega
            services.AddSingleton<OpcaoEntregaService>();
            services.AddSingleton<OpcaoEntregaRepository>();
            services.AddSingleton<OpcaoEntregaQueries>();
            #endregion

            #region Opcoes
            services.AddSingleton<OpcoesService>();
            services.AddSingleton<OpcoesRepository>();
            services.AddSingleton<OpcoesQueries>();
            #endregion

            #region Produto
            services.AddSingleton<ProdutoService>();
            services.AddSingleton<ProdutoRepository>();
            services.AddSingleton<ProdutoQueries>();
            #endregion

            #region ProdutoOpcoes
            services.AddSingleton<ProdutoOpcoesService>();
            services.AddSingleton<ProdutoOpcoesRepository>();
            services.AddSingleton<ProdutoOpcoesQueries>();
            #endregion

            #region ProdutoOpcoesValores
            services.AddSingleton<ProdutoOpcoesValoresService>();
            services.AddSingleton<ProdutoOpcoesValoresRepository>();
            services.AddSingleton<ProdutoOpcoesValoresQueries>();
            #endregion

            #region TipoOpcao
            services.AddSingleton<TipoOpcaoService>();
            services.AddSingleton<TipoOpcaoRepository>();
            services.AddSingleton<TipoOpcaoQueries>();
            #endregion

            #region ValoresOpcoes
            services.AddSingleton<ValoresOpcoesService>();
            services.AddSingleton<ValoresOpcoesRepository>();
            services.AddSingleton<ValoresOpcoesQueries>();
            #endregion

            #endregion

            #region Venda

            #region Compra
            services.AddSingleton<CompraService>();
            services.AddSingleton<CompraRepository>();
            services.AddSingleton<CompraQueries>();
            #endregion

            #region ItemCompra
            services.AddSingleton<ItemCompraService>();
            services.AddSingleton<ItemCompraRepository>();
            services.AddSingleton<ItemCompraQueries>();
            #endregion

            #region ItemCompraOpcaoValor
            services.AddSingleton<ItemCompraOpcaoValorService>();
            services.AddSingleton<ItemCompraOpcaoValorRepository>();
            services.AddSingleton<ItemCompraOpcaoValorQueries>();
            #endregion

            #region OpcaoEntregaCompra
            services.AddSingleton<OpcaoEntregaCompraService>();
            services.AddSingleton<OpcaoEntregaCompraRepository>();
            services.AddSingleton<OpcaoEntregaCompraQueries>();
            #endregion

            #region SituacaoCompra
            services.AddSingleton<SituacaoCompraService>();
            services.AddSingleton<SituacaoCompraRepository>();
            services.AddSingleton<SituacaoCompraQueries>();
            #endregion

            #region SituacaoTroca
            services.AddSingleton<SituacaoTrocaService>();
            services.AddSingleton<SituacaoTrocaRepository>();
            services.AddSingleton<SituacaoTrocaQueries>();
            #endregion

            #region Troca
            services.AddSingleton<TrocaService>();
            services.AddSingleton<TrocaRepository>();
            services.AddSingleton<TrocaQueries>();
            #endregion

            #endregion

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // global cors policy
            app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
