using ControleDeCaixa.Core.Compartilhado;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeCaixa.Dominio.ServicosDeDominio.Identidade
{
    public interface IIndentidadeServico
    {
        Task<Resultado<Credenciais, Falha>> CriarCredenciaisAsync(string usuario, string senha);
    }

    public sealed class IndentidadeServico : IIndentidadeServico
    {
        public async Task<Resultado<Credenciais, Falha>> CriarCredenciaisAsync(string usuario, string senha)
        {
            //TODO:: Validar se username é válido;
            throw new NotImplementedException();
        }

    }
}
