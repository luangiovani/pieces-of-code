using System;
using Facebook;
using System.Net;
using Newtonsoft.Json;

namespace Framework.Domain.Utils
{
    public class Facebook
    {
        //http://www.rodolfofadino.com.br/2013/12/api-do-facebook-com-c-facebook-sdk-for-net/
        //https://developers.facebook.com/docs/graph-api/reference/
        //https://github.com/facebook-csharp-sdk/facebook-csharp-sdk/issues/92
        //http://stackoverflow.com/questions/8724393/downloading-facebook-ads-statistics-in-background-no-web-browser/8729576#8729576

        private static readonly string app_id = "1077360585692138";
        private static readonly string secret = "d5d97452a63aeedbf2dc9e883a4c50c1";
        private static readonly string permissions = "manage_pages";
        private static string access_token = "";
        private static FacebookClient _fb = new FacebookClient();

        //PASSOS
        /// <summary>
        /// get/v2.6/me
        /// user_id: 147511965663405
        /// get/v2.6/me/accounts
        /// get/v2.6/147511965663405/accounts
        /// page_id: 141963892499260
        /// get/v2.6/141963892499260/posts
        /// </summary>

        public static void Authenticate(string url)
        {
            var urlLogin = _fb.GetLoginUrl(new
            {
                client_id = app_id,
                redirect_uri = url,
                response_type = "code",
                display = "page",
                scope = permissions
            });

            HttpWebResponse response;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlLogin);
            request.Method = "GET";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.121 Safari/535.2";
            response = (HttpWebResponse)request.GetResponse();

            //WebClient w = new WebClient();
            //w.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.121 Safari/535.2");
            //var a = w.DownloadString(response.ResponseUri.OriginalString);

            //string a = response.GetResponseStream().Read()

            //request = (HttpWebRequest)WebRequest.Create(response.ResponseUri.OriginalString);
            //request.Method = "GET";
            //request.AllowAutoRedirect = true;
            //request.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.121 Safari/535.2";
            //using (response = (HttpWebResponse)request.GetResponse())
            //{
            //    if ((int)response.StatusCode >= 300 && (int)response.StatusCode <= 399)
            //    {
            //        string uriString = response.Headers["Location"];
            //    }
            //}

            //Authorize(url, response.ResponseUri);
        }

        public static void Authorize(string url)
        {
            //FacebookOAuthResult oauthResult;
            //if (_fb.TryParseOAuthCallbackUrl(new Uri(authorize_url), out oauthResult))
            //{
            //    if (oauthResult.IsSuccess)
            //    {
            //        FBToken result = (FBToken)_fb.Get("/oauth/access_token", new
            //        {
            //            client_id = app_id,
            //            client_secret = secret,
            //            redirect_uri = url,
            //            code = oauthResult.Code
            //        });
            //        access_token = result.access_token;
            //    }
            //    else
            //    {

            //    }
            //}
        }

        public static void AuthorizeApp()
        {
            //TOKEN APP não possui acesso aos dados da página
            var result = _fb.Get("/oauth/access_token", new
            {
                client_id = app_id,
                client_secret = secret,
                grant_type = "client_credentials"
            });

            var jsonObj = JsonConvert.DeserializeObject<FBTokenApp>(result.ToString());
            access_token = jsonObj.access_token;
        }

        public static void Me()
        {
            var request = _fb.Get("me");
        }

        public void Amigos()
        {
            _fb.AccessToken = "";
            var request = _fb.Get("me/friends");
        }

        public void Timeline()
        {
            _fb.AccessToken = "";
            var request = _fb.Get("me/feed");
        }

        public void PublicarTimeline()
        {
            _fb.AccessToken = "";

            var parms = new
            {
                picture = "",
                link = "",
                name = "",
                caption = "",
                description = "",
                message = ""
            };

            try
            {
                var postId = _fb.Post("me/feed", parms);
            }
            catch
            {

            }
        }
    }

    struct FBToken
    {
        public string access_token { get; set; }
        public string expires { get; set; }
    }

    struct FBTokenApp
    {
        public string access_token { get; set; }
    }
}