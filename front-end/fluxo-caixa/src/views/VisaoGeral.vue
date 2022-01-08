<template>
  <div>
    <div>
      <card-movimentacoes-anuais></card-movimentacoes-anuais>
    </div>
    <div class="visao-geral" v-if="movimentacoesMesEstahAtivo">
      <div>
        <div class="card">
          <div class="card__header">
            <div>
              <p class="m-0 card__title">Receitas</p>
            </div>
            <div class="card-header__actions">
              <button
                class="btn btn-outline"
                type="button"
                @click="inserirOperacaoDoMes('receita')"
              >
                Nova receita
              </button>
            </div>
          </div>
          <div class="card__body">
            <div class="tabela-operacoes-mes">
              <div class="__cabecalho">
                <div>#</div>
                <div>Descrição</div>
                <div>Valor</div>
                <div></div>
              </div>
              <div
                v-if="movimentacoesDoMes && movimentacoesDoMes.receitas.length"
              >
                <div
                  class="__corpo"
                  v-for="receita in movimentacoesDoMes.receitas"
                  :key="receita.id"
                >
                  <div>1</div>
                  <div>{{ receita.descricao }}</div>
                  <div>{{ Number(receita.valor).toReal() }}</div>
                  <div class="table-actions">
                    <button
                      class="btn btn-small m-r-5"
                      type="button"
                      @click="removerOperacaoDoMes(receita)"
                    >
                      <i class="far fa-trash-alt"></i>
                    </button>
                    <button
                      class="btn btn-small btn-primary"
                      @click="editarOperacaoDoMes('receita', receita)"
                      type="button"
                    >
                      <i class="fas fa-pen"></i>
                    </button>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>

        <div class="card">
          <div class="card__header">
            <div>
              <p class="m-0 card__title">Custos</p>
            </div>
            <div class="card-header__actions">
              <button
                class="btn btn-outline"
                type="button"
                @click="inserirOperacaoDoMes('custo')"
              >
                Novo custo
              </button>
            </div>
          </div>
          <div class="card__body">
            <div class="tabela-operacoes-mes">
              <div class="__cabecalho">
                <div>#</div>
                <div>Descrição</div>
                <div>Valor</div>
                <div></div>
              </div>
              <div
                v-if="movimentacoesDoMes && movimentacoesDoMes.despesas.length"
              >
                <div
                  class="__corpo"
                  v-for="despesa in movimentacoesDoMes.despesas"
                  :key="despesa.id"
                >
                  <div>1</div>
                  <div>{{ despesa.descricao }}</div>
                  <div>{{ Number(despesa.valor).toReal() }}</div>
                  <div class="table-actions">
                    <button
                      class="btn btn-small m-r-5"
                      type="button"
                      @click="removerOperacaoDoMes(despesa)"
                    >
                      <i class="far fa-trash-alt"></i>
                    </button>
                    <button
                      class="btn btn-small btn-primary"
                      type="button"
                      @click="editarOperacaoDoMes('custo', despesa)"
                    >
                      <i class="fas fa-pen"></i>
                    </button>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div>
        <div class="card">
          <div class="card__header">
            <div>
              --
              <p class="m-0 card__title">
                Histórico:
                {{
                  formatarMesDeReferencia(movimentacoesDoMes.mesDeReferencia)
                }}
              </p>
            </div>
            <div class="card-header__actions">
              <button class="btn btn-outline" @click="verHistoricoDeOperacoes">
                Historico
              </button>
            </div>
          </div>
          <div class="card__body historico">
            <div class="card historico__card">
              <p class="f-14">Total de Receitas</p>
              <p class="f-bold main-color-dark f-16">
                {{
                  Number(movimentacoesDoMes.saldoDoMes.totalReceitas).toReal()
                }}
              </p>
            </div>
            <div class="card historico__card">
              <p class="f-14">Total de Custos</p>
              <p class="f-bold main-color-dark f-16">
                {{
                  Number(movimentacoesDoMes.saldoDoMes.totalDespesas).toReal()
                }}
              </p>
            </div>
            <div class="card historico__card">
              <p class="f-14">Saldo do Mês</p>
              <p class="f-bold main-color-dark f-16">
                {{ Number(movimentacoesDoMes.saldoDoMes.saldoTotal).toReal() }}
              </p>
            </div>
          </div>
        </div>
        <listagem-de-metas></listagem-de-metas>
      </div>
    </div>
    <div v-else class="card empty-state">
      <div class="__icone">
        <i class="fas fa-coins main-color-dark"></i>
      </div>
      <div class="__mensagem">
        <h1 class="main-color-dark">Nenhum movimentação encontrada</h1>
        <p>
          Utilize os filtros no card acima para visualizar as movimentações aqui
          ou se preferir, crie um novo controle de finanças
        </p>
      </div>
    </div>
    <div>
      <modal v-if="exibirModalOperacao">
        <nova-operacao-no-mes
          :modo="configuracaoModalOperacaoMes.modo"
          :tipo-operacao="configuracaoModalOperacaoMes.tipoOperacao"
          :operacao="configuracaoModalOperacaoMes.operacao"
          :on-cancel="configuracaoModalOperacaoMes.onCancel"
        ></nova-operacao-no-mes>
      </modal>
    </div>
  </div>
</template>


<script>
import moment from "moment";
import CardMovimentacoesAnuais from "./components/CardMovimentacoesAnuais.vue";
import NovaOperacaoNoMes from "./components/NovaOperacaoNoMes.vue";
import ListagemDeMetas from "./components/metas/ListagemDeMetas.vue";
import Modal from "../components/Modal.vue";
import { mapState } from "vuex";
export default {
  name: "VisaoGeral",
  components: {
    CardMovimentacoesAnuais,
    NovaOperacaoNoMes,
    ListagemDeMetas,
    Modal,
  },
  computed: {
    ...mapState({
      pessoa: (state) => state.pessoa.pessoa,
      movimentacoesDoMes: (state) => state.movimentacoes.movimentacoesDoMes,
    }),
    movimentacoesMesEstahAtivo() {
      return this.$store.getters["movimentacoes/possuiMesDeMovimentacoes"];
    },
  },
  data() {
    return {
      exibirModalOperacao: false,
      operacaoMes: {},
      configuracaoModalOperacaoMes: {
        modo: "insercao",
        tipoOperacao: "receita",
        operacao: {},
      },
    };
  },
  methods: {
    async removerOperacaoDoMes(operacaoDoMes) {
      await this.$store.dispatch("movimentacoes/removerOperacaoDoMes", {
        mesId: this.movimentacoesDoMes.id,
        operacaoMesId: operacaoDoMes.id,
      });
    },
    formatarMesDeReferencia(mesDeReferencia) {
      moment.locale("pt-br");
      return moment(mesDeReferencia).format("MMMM [de] YYYY");
    },
    editarOperacaoDoMes(tipoOperacao, operacaoDoMes) {
      this.configuracaoModalOperacaoMes = {
        modo: "atualizar",
        tipoOperacao,
        operacao: { ...operacaoDoMes, pessoaId: this.pessoa.id },
        onCancel: () => (this.exibirModalOperacao = !this.exibirModalOperacao),
      };
      this.exibirModalOperacao = !this.exibirModalOperacao;
    },
    inserirOperacaoDoMes(tipoOperacao) {
      this.configuracaoModalOperacaoMes = {
        modo: "inserir",
        tipoOperacao,
        operacao: {
          id: 0,
          mesId: this.movimentacoesDoMes.id,
          tipoOperacao: tipoOperacao == "receita" ? "Entrada" : "Saida",
        },
        onCancel: () => (this.exibirModalOperacao = !this.exibirModalOperacao),
      };
      this.exibirModalOperacao = !this.exibirModalOperacao;
    },
    fecharModalDeOperacoes() {
      this.exibirModalOperacaoMes = !this.exibirModalOperacaoMes;
    },
    verHistoricoDeOperacoes() {
      this.$router.push({
        name: "Autenticado.Historico",
        params: {
          pessoaId: this.pessoa.id,
          anoId: this.movimentacoesDoMes.idAnoMovimentacoes,
        },
      });
    },
  },
};
</script>

<style lang="scss" scoped>
.visao-geral {
  display: grid;
  grid-template-columns: 1.2fr 0.8fr;
}

.empty-state {
  max-width: 970px;
  min-height: 320px;
  text-align: center;
  display: flex;
  justify-items: center;
  justify-content: center;
  flex-direction: column;
  .__icone {
    i {
      font-size: 4.5em;
    }
  }
}

.historico {
  display: flex;
  justify-content: center;
  .historico__card {
    box-shadow: 0 2px 3px rgba($color: #000000, $alpha: 0.5);
    text-align: center;
    min-width: 125px;
    width: 100%;
    max-width: 135px;
  }
}
</style>