using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeCaixa.WebAPI.Entidades
{
    public class OperacaoMes
    {
        public OperacaoMes(int id, double valor, int mesId, int descricao, EMovimentacaoMes tipoOperacao)
        {
            Id = id;
            Valor = valor;
            MesId = mesId;
            Descricao = descricao;
            TipoOperacao = tipoOperacao;
        }

        public OperacaoMes(double valor, int mesId, int descricao, EMovimentacaoMes tipoOperacao)
        {
            Valor = valor;
            MesId = mesId;
            Descricao = descricao;
            TipoOperacao = tipoOperacao;
        }

        public int Id { get; set; }
        public double Valor { get; set; }
        public int MesId { get; set; }
        public int Descricao { get; set; }
        public EMovimentacaoMes TipoOperacao { get; set; }

        public OperacaoMes DefinirId(int id)
        {
            Id = id;
            return this;
        }
    }

    public enum EMovimentacaoMes
    {
        Custos = 'C',
        Receitas = 'R'
    }
}
