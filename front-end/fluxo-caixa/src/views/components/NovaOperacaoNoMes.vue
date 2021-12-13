<template>
  <div class="formulario w-100">
    <form name="novaOperacaoNoMes" class="form">
      <div class="tooltip">
        <p class="m-0" v-if="tipoOperacao == 'receita'">Novo receita</p>
        <p class="m-0" v-if="tipoOperacao == 'custo'">Novo custo</p>
      </div>
      <div>
        <div class="form-group">
          <label class="form-label" for="senha">Descrição</label>
          <textarea
            class="form-control"
            v-model="operacaoDoMes.descricao"
          ></textarea>
        </div>
        <div class="form-group">
          <label class="form-label" for="valor">Valor</label>
          <input
            class="form-control"
            type="number"
            name="valor"
            id="valor"
            v-model="operacaoDoMes.valor"
          />
        </div>
      </div>
      <div class="form-footer">
        <button class="btn" type="button" @click="onCancel && onCancel()">
          Cancelar
        </button>
        <button
          class="btn btn-primary"
          type="button"
          @click="handlerOperacao()"
        >
          {{ modo == "inserir" ? "Nova operacao" : "Atualizar operação" }}
        </button>
      </div>
    </form>
  </div>
</template>

<script>
import { mapState } from "vuex";
export default {
  name: "NovaOperacaoNoMes",
  computed: {
    ...mapState({
      pessoa: (state) => state.pessoa.pessoa,
      filtroHeaderAtivo: (state) => state.movimentacoes.filtroAtivo,
    }),
  },
  props: {
    modo: {
      type: String,
      required: true,
      default: () => "inserir",
    },
    tipoOperacao: {
      type: String,
      required: true,
    },
    operacao: {
      type: Object,
      required: false,
    },
    onCancel: {
      type: Function,
      required: true,
    },
  },
  created() {
    this.operacaoDoMes = Object.assign({}, this.operacao);
  },
  data() {
    return {
      operacaoDoMes: {},
    };
  },
  methods: {
    async _recuperarMovimentacoesDoMes() {
      const { pessoaId, anoId, mesId } = this.filtroHeaderAtivo;

      await this.$store.dispatch("movimentacoes/recuperarMovimentacoesDoMes", {
        pessoaId,
        anoId,
        mesId,
      });
    },
    async _adicionarOperacao() {
      try {
        await this.$store.dispatch("movimentacoes/adicionarOperacaoDoMes", {
          pessoaId: this.pessoa.id,
          operacao: this.operacaoDoMes,
        });
        await this._recuperarMovimentacoesDoMes();
        this.$toast.open({
          message: `${this.tipoOperacao}s atualizado(a) com sucesso`,
          type: "success",
        });
        this.onCancel && this.onCancel();
      } catch (error) {
        this.$toast.open({
          message:
            error.mensagem ||
            `Houve um problema ao inserir as ${this.tipoOperacao}s`,
          type: "error",
        });
      }
    },
    async _atualizarOperacao() {
      try {
        await this.$store.dispatch("movimentacoes/atualizarOperacaoDoMes", {
          pessoaId: this.pessoa.id,
          operacao: this.operacaoDoMes,
        });
        await this._recuperarMovimentacoesDoMes();
        this.$toast.open({
          message: `${this.tipoOperacao}s atualizado(a) com sucesso`,
          type: "success",
        });
        this.onCancel && this.onCancel();
      } catch (error) {
        this.$toast.open({
          message:
            error.mensagem ||
            `Houve um problema ao atualizar as ${this.tipoOperacao}s`,
          type: "error",
        });
      }
    },
    handlerOperacao() {
      if (this.modo == "atualizar") {
        return this._atualizarOperacao();
      }
      return this._adicionarOperacao();
    },
  },
};
</script>

<style>
</style>