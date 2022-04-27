using ControleDeCaixa.Compartilhado.Generics;
using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeCaixa.Infraestrutura.Servicos
{

    public interface IMailService
    {
        Task<Resultado<bool, Falha>> EnviarEmailAsync(string emailDestinatario, string nomeDestinatario, string templateEmail);
    }

    public sealed class GmailService : IMailService
    {

        public async Task<Resultado<bool, Falha>> EnviarEmailAsync(string emailDestinatario, string nomeDestinatario, string templateEmail)
        {
            try
            {
                var credenciais = new { Remetente = "", Senha = "" };
                MailMessage mailMessage = new MailMessage(credenciais.Remetente, emailDestinatario);
                mailMessage.Subject = "Teste envio de e-mail";
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = templateEmail;
                mailMessage.SubjectEncoding = Encoding.GetEncoding("UTF-8");
                mailMessage.BodyEncoding = Encoding.GetEncoding("UTF-8");

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(credenciais.Remetente, credenciais.Senha);
                smtpClient.EnableSsl = true;

                await smtpClient.SendMailAsync(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                return Falha.NovaComException("Falha ao enviar o e-mail", ex);
            }
        }

    }
}
