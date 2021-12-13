<template>
  <div>
    <div class="card card-ano-movimentacoes">
      <div class="card__header">
        <div>
          <p>
            Ano ativo
            <strong class="color-primary">{{ anoSelecionado.ano }}</strong>
          </p>
        </div>
        <div>
          <div>
            <select
              name="anoDeMovimentacoes"
              id="anoDeMovimentacoes"
              class="form-control"
              v-model="anoSelecionado"
              @change="recuperarMesesDeMovimentacoes"
            >
              <option
                :value="ano"
                v-for="ano in anoDeMovimentacoes"
                :key="ano.id"
              >
                Filtrar pelo ano {{ ano.ano }}
              </option>
            </select>
            <div class="d-inline-block">
              <button
                class="btn btn-outline btn-small"
                @click="devoAdicionarNovoAno = !devoAdicionarNovoAno"
              >
                Novo ano
              </button>
            </div>
          </div>
        </div>
        <div>
          <p>
            Mês ativo
            <strong class="color-primary">{{ mesSelecionado.mes }}</strong>
          </p>
        </div>
        <div>
          <div>
            <select
              name="mesesDeMovimentacoes"
              id="mesesDeMovimentacoes"
              class="form-control"
              v-model="mesSelecionado"
              @change="recuperarMovimentacoesDoMes"
            >
              <option
                :value="mes"
                v-for="mes in mesesDeMovimentacoes"
                :key="mes.id"
              >
                Mês {{ mes.mes }}
              </option>
            </select>
            <div class="d-inline-block">
              <button
                class="btn btn-outline btn-small"
                @click="devoAdicionarNovoMes = !devoAdicionarNovoMes"
                :disabled="!anoDeMovimentacoes.length || !anoSelecionado.id"
              >
                Novo mês
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div v-if="devoAdicionarNovoAno">
      <modal>
        <novo-ano-de-movimentacoes
          :on-cancel="fecharModalNovoAno"
        ></novo-ano-de-movimentacoes>
      </modal>
    </div>
    <div v-if="devoAdicionarNovoMes">
      <modal>
        <novo-mes-de-movimentacoes
          :on-cancel="fecharModalNovoMes"
        ></novo-mes-de-movimentacoes>
      </modal>
    </div>
  </div>
</template>

<script>
import NovoAnoDeMovimentacoes from "./NovoAnoDeMovimentacoes.vue";
import NovoMesDeMovimentacoes from "./NovoMesDeMovimentacoes.vue";
import Modal from "../../components/Modal.vue";
import { mapState } from "vuex";
export default {
  name: "CardMovimentacoesAnuais",
  components: {
    Modal,
    NovoAnoDeMovimentacoes,
    NovoMesDeMovimentacoes,
  },
  mounted() {
    this.recuperarAnosDeMovimentacoes();
  },
  computed: {
    ...mapState({
      pessoa: (state) => state.pessoa.pessoa,
      anoDeMovimentacoes: (state) => state.movimentacoes.anosDeMovimentacoes,
      mesesDeMovimentacoes: (state) => state.movimentacoes.mesesDeMovimentacoes,
      filtroHeaderAtivo: (state) => state.movimentacoes.filtroAtivo,
    }),
  },
  data() {
    return {
      devoAdicionarNovoAno: false,
      devoAdicionarNovoMes: false,
      anoSelecionado: {},
      mesSelecionado: {},
    };
  },
  methods: {
    fecharModalNovoAno() {
      this.devoAdicionarNovoAno = !this.devoAdicionarNovoAno;
    },
    fecharModalNovoMes() {
      this.devoAdicionarNovoMes = !this.devoAdicionarNovoMes;
      this.recuperarMesesDeMovimentacoes();
    },
    async recuperarAnosDeMovimentacoes() {
      await this.$store.dispatch(
        "movimentacoes/recuperarAnosDeMovimentacoes",
        this.pessoa.id
      );
    },
    async recuperarMesesDeMovimentacoes() {
      const payload = {
        pessoaId: this.pessoa.id,
        anoId: this.anoSelecionado.id,
        mesId: 0,
      };
      await this.$store.dispatch(
        "movimentacoes/recuperarMesesDeMovimentacoes",
        payload
      );
      this.$store.commit("movimentacoes/atualizarFiltroAtivo", payload);
    },
    async recuperarMovimentacoesDoMes() {
      const payload = {
        pessoaId: this.pessoa.id,
        anoId: this.anoSelecionado.id,
        mesId: this.mesSelecionado.id,
      };
      await this.$store.dispatch(
        "movimentacoes/recuperarMovimentacoesDoMes",
        payload
      );
      this.$store.commit("movimentacoes/atualizarFiltroAtivo", payload);
    },
  },
};
</script>

<style lang="scss" scoped>
.card-ano-movimentacoes {
  max-width: 970px;
  .card__header {
    display: grid;
    grid-template-columns: 140px 0.6fr 156px 0.6fr;
  }
  select {
    min-width: 175px;
    margin-right: 15px;
    max-width: 195px;
    width: 100%;
  }
}
</style>