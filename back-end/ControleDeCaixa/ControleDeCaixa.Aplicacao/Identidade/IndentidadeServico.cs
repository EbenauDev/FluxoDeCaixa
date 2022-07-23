using ControleDeCaixa.Core.Compartilhado;
using ControleDeCaixa.Infra.Identidade;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeCaixa.Aplicacao.Identidade
{
    public interface IIndentidadeServico
    {
        Task<Resultado<Credenciais, Falha>> CriarCredenciaisAsync(string usuario, string senha);
    }

    public sealed class IndentidadeServico : IIndentidadeServico
    {
        private readonly IIdentidadeRepositorio _identidadeRepositorio;

        public IndentidadeServico(IIdentidadeRepositorio identidadeRepositorio)
        {
            _identidadeRepositorio = identidadeRepositorio;
        }

        public async Task<Resultado<Credenciais, Falha>> CriarCredenciaisAsync(string usuario, string senha)
        {
            if (await _identidadeRepositorio.UsuarioEstahDisponivel(usuario) is var resultadoUsuario && resultadoUsuario.EhSucesso is false)
                return resultadoUsuario.Falha;
            return new Credenciais(usuario, SenhaService.CriptografarSenhaSha256(senha));
        }

    }
}
