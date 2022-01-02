using ControleDeCaixa.WebAPI.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeCaixa.WebAPI.Repositorio
{

    public interface ISenhasRepositorio
    {
        Task<Resultado<object, Falha>> GravarTokenNovaSenha(object token);
    }
    public class SenhasRepositorio
    {

    }
}
