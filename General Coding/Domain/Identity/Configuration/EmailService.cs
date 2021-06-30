using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Framework.Domain.Identity
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            string login = ConfigurationManager.AppSettings["EmailLogin"];
            string senha = ConfigurationManager.AppSettings["EmailSenha"];
            string smtp = ConfigurationManager.AppSettings["EmailSmtp"];
            int port = !string.IsNullOrEmpty(ConfigurationManager.AppSettings["EmailPort"])
                ? Convert.ToInt32(ConfigurationManager.AppSettings["EmailPort"])
                : 0;
            bool ssl = !String.IsNullOrEmpty(ConfigurationManager.AppSettings["EmailSSL"])
                ? Convert.ToBoolean(ConfigurationManager.AppSettings["EmailSSL"])
                : false;

            var mensagem = new MailMessage();
            mensagem.From = new MailAddress(login);
            mensagem.To.Add(new MailAddress(message.Destination));
            mensagem.Subject = message.Subject;
            mensagem.Body = message.Body;
            mensagem.IsBodyHtml = true;

            var mailClient = new SmtpClient(smtp);
            //mailClient.UseDefaultCredentials = false;
            mailClient.Credentials = new NetworkCredential(login, senha);

            if (port > 0)
                mailClient.Port = port;

            mailClient.EnableSsl = ssl;

            mailClient.Send(mensagem);

            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
    }
}