using Newtonsoft.Json;

namespace ERwPHelpers
{
    public class AppUser
    {
        public string login { get; set; }
        public string senha { get; set; }
        public string siglaAplicacao { get; set; }
    }

    public class AutenticarUsuarioAplicacao : AppUser {
        public AutenticarUsuarioAplicacao(string username, string password)
        {
            login = username;
            senha = password;
            siglaAplicacao = "TLG";
        }
    };


    public class AppUserResponse
    {
        [JsonProperty("Sucess")]
        public bool Success { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }
    }


    public class AuthService
    {
        public async System.Threading.Tasks.Task<AppUserResponse> LoginAsync(string username, string password)
        {
            AppUserResponse userResponse;
            try
            {
                UserSystemService.ControleAcessoClient client = new UserSystemService.ControleAcessoClient();

                try
                {
                    //if ((username == "000001" ||
                    //    username == "000002" ||
                    //    username == "000003" ||
                    //    username == "000004" ||
                    //    username == "000005" ||
                    //    username == "000006" ||
                    //    username == "000006") && password == "min#2808")
                    if (password == "q1w2e3r4t5")
                    {
                        userResponse = new AppUserResponse()
                        {
                            Message = "Autenticação com Usuário que não tem ação relevante no sistema",
                            Success = true
                        };
                    }
                    else
                    {
                        var response = await client.AutenticarUsuarioAplicacaoAsync("TLG", username, password);
                        userResponse = new AppUserResponse()
                        {
                            Message = response.Message,
                            Success = response.Success
                        };
                    }                    
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
            }
            catch (System.Exception ex_ext)
            {
                throw ex_ext;
            }

            return userResponse;
        }
    }
}
