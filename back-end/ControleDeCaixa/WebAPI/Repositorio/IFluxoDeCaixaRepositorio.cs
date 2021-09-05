using ControleDeCaixa.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeCaixa.WebAPI.Repositorio
{
    public interface IFluxoDeCaixaRepositorio
    {
        Task<IEnumerable<FluxoDeCaixaAnual>> RecuperarFluxoDeCaixa(string ano);
    }
}
