using System.Security.Cryptography;
using System.Text;

namespace ControleDeCaixa.WebAPI.Aplicacao
{

    public interface ICriptografiaMD5
    {
        string ConverterParaMD5(string input);
    }

    public class CriptografiaMD5 : ICriptografiaMD5
    {
        public string ConverterParaMD5(string input)
        {
            MD5 md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}
