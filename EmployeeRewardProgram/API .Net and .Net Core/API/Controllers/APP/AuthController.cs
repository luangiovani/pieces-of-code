using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.DTO.APP;
using Domain.Services.Gestao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ERwPHelpers;

namespace ERwPControllers.APP
{
    /// <autor>
    /// Luan Fernandes - Ewave | 04/2019
    /// </autor>
    /// <tarefa>
    /// Development of the Employee Reward Program application
    /// </tarefa>
    /// <atividades>
    /// Desenvolvimento de Controllers para expôr como Services de API
    /// </atividades>
    /// <summary>
    /// Controller para Autenticação no sistema
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        /// <summary>
        /// Objeto Local de Serviço de Operações com informações de Colaboradores
        /// </summary>
        private readonly ColaboradorService _service;

        /// <summary>
        /// Objeto local de Configurações do Sistema
        /// </summary>
        private readonly AppSettings _appSettings;

        /// <summary>
        /// Service de autenticação
        /// </summary>
        private readonly AuthService _authService;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="service">Injeção do serviço de colaboradores</param>
        /// <param name="appSettings">Injeção das configurações do sistema</param>
        /// <param name="logger">Injeção do serviço de Log</param>
        public AuthController(IOptions<AppSettings> appSettings, 
            ColaboradorService service,
            AuthService authService)
        {
            _appSettings = appSettings.Value;
            _service = service;
            _authService = authService;
        }

        /// <summary>
        /// Serviço de Autenticação
        /// </summary>
        /// <param name="loginDTO">DTO com as informações para autenticação</param>
        /// <returns>Response Status 200 caso sucesso, StatusCode do Erro com "error" descrição do erro.</returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public async System.Threading.Tasks.Task<IActionResult> AuthenticateAsync([FromBody]LoginDTO loginDTO)
        {
            try
            {
                if (loginDTO == null)
                    return BadRequest(new { success = false, obj = loginDTO, message = "Usuário ou Senha incorretos" });
                else if (string.IsNullOrEmpty(loginDTO.login) || string.IsNullOrEmpty(loginDTO.senha))
                    return BadRequest(new { success = false, obj = loginDTO, message = "Informe Login e Senha" });

                /// O CS do Login, retorna somente os números do CS
                UsuarioDTO user = _service.ObterUsuarioOpcoesMenu(loginDTO.cs, loginDTO.aplicacao_id);

                if (user != null)
                {
                    var userResp = await _authService.LoginAsync(loginDTO.login, loginDTO.senha);

                    if (userResp.Success)
                    {
                        var tokenHandler = new JwtSecurityTokenHandler();
                        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                        var signingKey = new SymmetricSecurityKey(key);

                        var tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Issuer = "Issuer",
                            Audience = "Audience",
                            SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
                            Subject = new ClaimsIdentity(new Claim[]
                            {
                        new Claim("userId", user.id.ToString()),
                        new Claim("userCS", user.cs),
                        new Claim("perfil", user.perfil.ToString()),
                        new Claim("aplicacaoId", loginDTO.aplicacao_id)
                            }),
                            Expires = DateTime.UtcNow.AddMinutes(120),
                            NotBefore = DateTime.Now

                        };
                        var token = tokenHandler.CreateToken(tokenDescriptor);
                        var tokenString = tokenHandler.WriteToken(token);

                        user.token = tokenString;

                        /// Retorno OK da Operação com Nome do usuário, ID e Token
                        return Ok(new
                        {
                            success = true,
                            message = "Usuário autenticado com sucesso!",
                            obj = user,
                            token = tokenString
                        });
                    }
                    else
                    {
                        /// Retorno OK da Operação com Nome do usuário, ID e Token
                        return Ok(new
                        {
                            success = false,
                            message = userResp.Message
                        });
                    }

                }
                else
                {
                    /// Retorno OK da Operação com Nome do usuário, ID e Token
                    return Ok(new
                    {
                        success = false,
                        message = "Usuário não autenticado"
                    });
                }
            }
            catch
            {
                /// Retorno OK da Operação com Nome do usuário, ID e Token
                return Ok(new
                {
                    success = false,
                    message = "Ocorreu um erro interno no servidor."
                });
            }
        }
    }
}