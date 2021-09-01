using ControleDeCaixa.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeCaixa.WebAPI.Repositorio
{
    public class CaixaRepositorio : ICaixaRepositorio
    {
        public async Task<IEnumerable<FluxoCaixa>> RecuperarFluxoDeCaixaAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(3));
            var fluxoCaixas = new List<FluxoCaixa>();
            fluxoCaixas.Add(new FluxoCaixa(anoReferencia: "2021",
                                           caixaDoMes: new List<CaixaMes>() {
                                                   new CaixaMes(id: 1,
                                                                mes: "Agosto",
                                                                mesReferencia: "2021-08",
                                                                custos: new List<Custo>(){
                                                                    new Custo(1, "Mensalidade Universidade", 316.15),
                                                                    new Custo(2, "Fatura cartão de Crédito", 415.00),
                                                                },
                                                                receitas: new List<Receita>(){
                                                                    new Receita(1, "Salário", 2000)
                                                                })
                                           }));

            return fluxoCaixas;

        }
    }
}
