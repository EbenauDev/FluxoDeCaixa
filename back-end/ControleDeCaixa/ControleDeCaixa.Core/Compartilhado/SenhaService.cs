using System.Security.Cryptography;
using System.Text;

namespace ControleDeCaixa.Core.Compartilhado
{
    public static class SenhaService
    {
        public static string CriptografarSenhaSha256(string senha)
        {
            using (var servico = SHA256.Create())
            {
                byte[] senhaEmBytes = UTF8Encoding.UTF8.GetBytes(senha);
                byte[] hashValue = servico.ComputeHash(senhaEmBytes);
                return hashValue.ToString();
            }
        }
    }
}
