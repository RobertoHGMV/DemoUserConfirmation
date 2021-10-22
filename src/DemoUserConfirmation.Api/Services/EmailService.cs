using System;
using System.Net;
using System.Net.Mail;

namespace DemoUserConfirmation.Api.Services
{
    public class EmailService
    {
        public string EnviaMensagemEmail(string Destinatario, string callbackUrl)
        {
            var mailMessage = new MailMessage("Remetente", Destinatario);
            mailMessage.Subject = "Confirme a sua conta";
            mailMessage.Body =  $"Confirme a sua conta clicando <a href=\"{callbackUrl}\">AQUI</a>";

            var client = new SmtpClient("smtp.gmail.com", 587);
            var cred = new NetworkCredential("SEU_EMAIL@gmail.com", "SUA_SENHA");
            client.EnableSsl = true;
            client.Credentials = cred;

            client.UseDefaultCredentials = false;

            client.Send(mailMessage);

            return "Mensagem enviada para  " + Destinatario + " às " + DateTime.Now.ToString() + ".";
        }
    }
}