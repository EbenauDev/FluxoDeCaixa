using System;
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
                byte[] senhaEmBytes = Encoding.UTF8.GetBytes(senha);
                byte[] hashValue = servico.ComputeHash(senhaEmBytes);
                string hashString = string.Empty;
                foreach (byte x in hashValue)
                {
                    hashString += String.Format("{0:x2}", x);
                }
                return hashString;
            }
        }
    }
}
