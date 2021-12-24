using ControleDeCaixa.WebAPI.Generics;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeCaixa.WebAPI.Aplicacao
{

    public interface ISendGridSendEmailService
    {
        Task<Resultado<bool, Falha>> EnviarEmailAsync(string emailDestinatario, string nomeDestinatario, string templateEmail);
    }

    public sealed class SendGridSendEmailService : ISendGridSendEmailService
    {
        private readonly string sendGridAPIKey;
        public SendGridSendEmailService(IConfiguration configuration)
        {
            sendGridAPIKey = configuration["ChaveSendGrid"];
        }

        public async Task<Resultado<bool, Falha>> EnviarEmailAsync(string emailDestinatario, string nomeDestinatario, string templateEmail)
        {
            var cliente = new SendGridClient(sendGridAPIKey);
            var remetente = new EmailAddress("ebenau06@gmail.com", "João Tiago");
            var destinatario = new EmailAddress(emailDestinatario, nomeDestinatario);
            var assuntoEmail = "Teste envio de e-mail";
            var mensagemEmail = MailHelper.CreateSingleEmail(
                remetente,
                destinatario,
                assuntoEmail,
                plainTextContent: string.Empty,
                templateEmail);

            var resposta = await cliente.SendEmailAsync(mensagemEmail);
            if (resposta.IsSuccessStatusCode == false)
                return Falha.Nova($"Falha ao enviar e-mail para  {nomeDestinatario}");
            return true;
        }

    }
}
