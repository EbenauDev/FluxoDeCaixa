<template>
  <form name="novoAnoDeContas" class="form">
    <div class="form-group">
      <label class="form-label color-primary" for="mesSelecionado"
        >Seleciona abaixo para qual mês você deseja criar as
        movimentações</label
      >
      <select
        class="form-control"
        name="mesSelecionado"
        id="mesSelecionado"
        v-model="mesSelecionado"
        @change="definirMesDeReferencia"
      >
        <option value="0">Janeiro</option>
        <option value="1">Fevereiro</option>
        <option value="2">Março</option>
        <option value="3">Abril</option>
        <option value="4">Maio</option>
        <option value="5">Junho</option>
        <option value="6">Julho</option>
        <option value="7">Agosto</option>
        <option value="8">Setembro</option>
        <option value="9">Outubro</option>
        <option value="10">Novembro</option>
        <option value="11">Dezembro</option>
      </select>
    </div>
    <div class="form-group">
      <label class="form-label color-primary" for="descricao">Descrição</label>
      <textarea
        v-model="mesDeMovimentacao.descricao"
        class="form-control"
        name="descricao"
        id="descricao"
        rows="6"
      ></textarea>
    </div>

    <div class="form-footer">
      <button class="btn" type="button" @click="onCancel && onCancel()">
        Cancelar
      </button>
      <button
        class="btn btn-primary"
        type="button"
        @click="novoMesDeMovimentacoes"
      >
        Criar
      </button>
    </div>
  </form>
</template>

<script>
import httpRequest from "@/services/$httpRequest";
import { mapState } from "vuex";
export default {
  name: "NovoAnoDeMovimentacoes",
  props: {
    onCancel: {
      type: Function,
    },
  },
  created() {
    this.mesDeMovimentacao.idAnoMovimentacoes = this.filtroHeaderAtivo.anoId;
  },
  computed: {
    ...mapState({
      pessoa: (state) => state.pessoa.pessoa,
      filtroHeaderAtivo: (state) => state.movimentacoes.filtroAtivo,
    }),
    anosDeControle() {
      new Date().getFullYear();
      var _anos = [new Date().getFullYear()];
      var _counter = 0;
      while (_counter < 4) {
        _counter++;
        _anos.push(new Date().getFullYear() + _counter);
      }
      return _anos;
    },
  },
  data() {
    return {
      mesSelecionado: 0,
      mesDeMovimentacao: {
        idAnoMovimentacoes: 0,
        descricao: "",
        mesDeReferencia: "",
      },
    };
  },
  methods: {
    definirMesDeReferencia() {
      this.mesDeMovimentacao.mesDeReferencia = `${new Date(
        new Date().setMonth(this.mesSelecionado)
      )
        .toISOString()
        .substring(0, 10)}T00:00:00`;
    },
    async recuperarAnosDeMovimentacoes() {
      await this.$store.dispatch(
        "movimentacoes/recuperarAnosDeMovimentacoes",
        this.pessoa.id
      );
    },
    async novoMesDeMovimentacoes() {
      try {
        const _url = `Movimentacoes/Pessoa/${this.pessoa.id}/MesDeMovimentacoes`;
        await httpRequest.post(_url, this.mesDeMovimentacao);
        await this.recuperarAnosDeMovimentacoes();
        this.$toast.open({
          message: "Mês de movimentações inserido com sucesso",
          type: "success",
        });
        this.onCancel && this.onCancel();
      } catch (error) {
        this.$toast.open({
          message: error.mensagem,
          type: "error",
        });
      }
    },
  },
};
</script>

<style>
</style>