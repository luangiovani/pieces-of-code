using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class SendEmailService
    {
        private string login { get; set; }
        private string senha { get; set; }
        private string smtp { get; set; }
        private int port { get; set; }
        private bool ssl { get; set; }

        public SendEmailService(IConfiguration config)
        {

            login = config.GetSection("EmailSettings").GetSection("EmailLogin").Value;
            senha = config.GetSection("EmailSettings").GetSection("EmailSenha").Value;
            smtp = config.GetSection("EmailSettings").GetSection("EmailSmtp").Value;
            var sport = config.GetSection("EmailSettings").GetSection("EmailPort").Value;
            var sssl = config.GetSection("EmailSettings").GetSection("EmailSSL").Value;

            port = !string.IsNullOrEmpty(sport)
                ? Convert.ToInt32(sport)
                : 0;
            ssl = !String.IsNullOrEmpty(sssl)
                ? Convert.ToBoolean(sssl)
                : false;
        }

        public Task SendAsync(string message, string subject, string to)
        {
            try
            {
                #region Email com Imagem em anexo
                if (String.IsNullOrEmpty(to) || String.IsNullOrWhiteSpace(to))
                    to = "";

                var oMail = new MailMessage();
                /// Imagem para o Header
                Attachment oAttachment = new Attachment("./Content/Images/img_head_email.jpg");
                oAttachment.ContentDisposition.Inline = true;
                oAttachment.ContentId = "imgheadlogo";

                oMail.From = new MailAddress(login,"ERwP");
                //mensagem.To.Add(new MailAddress(to));
                oMail.To.Add(new MailAddress("luangiovani@gmail.com"));
                oMail.ReplyToList.Add(new MailAddress("luan.fernandes@ewave.com.br"));
                oMail.Subject = subject;
                oMail.Body = message;

                oMail.IsBodyHtml = true;
                oMail.Attachments.Add(oAttachment);

                var mailClient = new SmtpClient(smtp);
                //mailClient.UseDefaultCredentials = false;
                mailClient.Credentials = new NetworkCredential(login, senha);

                if (port > 0)
                    mailClient.Port = port;

                mailClient.EnableSsl = ssl;

                mailClient.Send(oMail);
                #endregion
            }
            catch (Exception emExc)
            {
                string a = emExc.Message;
                throw emExc;
            }


            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
    }
}
