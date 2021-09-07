using System.Collections.Generic;

namespace ControleDeCaixa.WebAPI.Entities
{
    public class CaixaDoMes
    {
        public CaixaDoMes(int id, string mesReferencia, IEnumerable<Receita> receitas, IEnumerable<Custo> custos, IEnumerable<CaixaHisto> historico)
        {
            Id = id;
            MesReferencia = mesReferencia;
            Receitas = receitas;
            Custos = custos;
            Historico = historico;
        }

        public int Id { get; set; }
        public string MesReferencia { get; set; }
        public IEnumerable<Receita> Receitas { get; set; }
        public IEnumerable<Custo> Custos { get; set; }
        public IEnumerable<CaixaHisto> Historico { get; set; }
    }

    public class Receita
    {
        public Receita(int id, string descricao, double valor)
        {
            Id = id;
            Descricao = descricao;
            Valor = valor;
        }

        public int Id { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
    }

    public class Custo
    {
        public Custo(int id, string descricao, double valor)
        {
            Id = id;
            Descricao = descricao;
            Valor = valor;
        }

        public int Id { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
    }

    public class CaixaHisto
    {
        public CaixaHisto(int id, double totalReceitas, double totalCustos, double caixaInicial, double caixaFinal, double fluxoDeCaixa)
        {
            Id = id;
            TotalReceitas = totalReceitas;
            TotalCustos = totalCustos;
            CaixaInicial = caixaInicial;
            CaixaFinal = caixaFinal;
            FluxoDeCaixa = fluxoDeCaixa;
        }

        public int Id { get; set; }
        public double TotalReceitas { get; set; }
        public double TotalCustos { get; set; }
        public double CaixaInicial { get; set; }
        public double CaixaFinal { get; set; }
        public double FluxoDeCaixa { get; set; }
    }
}
