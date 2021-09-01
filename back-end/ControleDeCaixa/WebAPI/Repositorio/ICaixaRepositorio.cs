using ControleDeCaixa.WebAPI.Generics;
using ControleDeCaixa.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeCaixa.WebAPI.Repositorio
{
    public interface ICaixaRepositorio
    {
        Task<IEnumerable<FluxoCaixa>> RecuperarFluxoDeCaixaAsync();
    }
}
