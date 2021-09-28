using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeCaixa.WebAPI.Entites
{
    public class OperacaoCaixa
    {
        public OperacaoCaixa(int caixaId, ETipoOperacaoCaixa operacao, string descricao, double valor)
        {
            CaixaId = caixaId;
            Operacao = operacao;
            Descricao = descricao;
            Valor = valor;
        }

        public static OperacaoCaixa Nova(int caixaId, string operacao, string descricao, double valor)
            => new OperacaoCaixa(
                caixaId,
                operacao: (ETipoOperacaoCaixa)Convert.ToChar(operacao),
                descricao,
                valor
        );

        public int CaixaId { get; set; }
        public ETipoOperacaoCaixa Operacao { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
    }

    public enum ETipoOperacaoCaixa
    {
        Entrada = 'E',
        Saida = 'S'
    }

    
}
