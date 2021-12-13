<template>
  <div class="card card-historico">
    <div class="card__header">
      <div>
        <p class="m-0 card__title">Histórico</p>
      </div>
      <div class="card-header__actions">
        <button
          class="btn btn-outline no-print"
          type="button"
          @click="imprimirHistorico()"
        >
          Imprimir
        </button>
      </div>
    </div>
    <div class="card__body">
      <h2>Abaixo as suas movimentações feitas no ano de {{ historico.ano }}</h2>
      <div
        v-for="movimentacao in historico.movimentacoesMes"
        :key="movimentacao.id"
      >
        <h3>Movimentações feitas no mês de {{ movimentacao.mes }}</h3>
        <div>
          <div class="tabela-operacoes-mes">
            <div class="__cabecalho">
              <div>#</div>
              <div>Descrição</div>
              <div>Valor</div>
              <div>Tipo</div>
            </div>
            <div>
              <div
                class="__corpo"
                v-for="(movimentacaoNoMes, index) in movimentacao.movimentacoes"
                :key="movimentacaoNoMes.id"
              >
                <div>{{ index + 1 }}</div>
                <div>{{ movimentacaoNoMes.descricao }}</div>
                <div>{{ Number(movimentacaoNoMes.valor).toReal() }}</div>
                <div>
                  {{
                    movimentacaoNoMes.tipoOperacao == "Entrada"
                      ? "Receita"
                      : "Custos"
                  }}
                </div>
              </div>
            </div>
          </div>
          <div class="saldo-mes">
            <p>
              Total receitas:
              <strong>
                {{
                  Number(movimentacao.saldoMovimentacoes.totalReceitas).toReal()
                }}</strong
              >
            </p>
            <p class="m-l-25 m-r-25">
              Total despesas:
              <strong>
                {{
                  Number(movimentacao.saldoMovimentacoes.totalDespesas).toReal()
                }}
              </strong>
            </p>
            <p>
              Saldo total:
              <strong>
                {{
                  Number(movimentacao.saldoMovimentacoes.saldoTotal).toReal()
                }}</strong
              >
            </p>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import httpRequest from "@/services/$httpRequest";
export default {
  name: "Historico",
  created() {
    this.carregarHistorico();
  },
  data() {
    return {
      historico: {},
    };
  },
  methods: {
    imprimirHistorico() {
      window && window.print();
    },
    async carregarHistorico() {
      try {
        const { pessoaId, anoId } = this.$route.params;
        var resultado = await httpRequest.get(
          `Movimentacoes/Pessoa/${pessoaId}/Ano/${anoId}/Historico`
        );
        this.historico = resultado;
        this.$toast.open({
          message: "Histórico carregado com sucesso",
          type: "success",
        });
      } catch (error) {
        this.$toast.open({
          message:
            "Houve um problema ao tentar salvar o seu cadastro. Por favor, tente novamente mais tarde.",
          type: "error",
        });
      }
    },
    //
  },
};
</script>

<style lang="scss" scoped>
.card-historico {
  max-width: 768px;
  .saldo-mes {
    display: flex;
    flex-direction: row;
    justify-content: flex-end;
    margin-top: 20px;
    margin-bottom: 25px;
  }
}

@media print {
  .card-historico {
    visibility: visible;
    position: absolute;
    left: 0;
    top: 0;
    right: 0;
    margin: 20px;
  }
}
</style>