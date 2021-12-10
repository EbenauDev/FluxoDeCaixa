<template>
  <form name="novoAnoDeContas" class="form">
    <div class="form-group">
      <label class="form-label color-primary" for="username"
        >Ano de controle</label
      >
      <select name="" id="" class="form-control" v-model="movimentacoes.ano">
        <option :value="item" v-for="item in anosDeControle" :key="item">
          Ano {{ item }}
        </option>
      </select>
    </div>
    <div class="form-group">
      <label class="form-label color-primary" for="descricao">Descrição</label>
      <textarea
        v-model="movimentacoes.descricao"
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
        @click="novoAnoDeMovimentacoes"
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
  computed: {
    ...mapState({
      pessoa: (state) => state.pessoa.pessoa,
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
      movimentacoes: {
        ano: 0,
        descricao: "",
      },
    };
  },
  methods: {
    async novoAnoDeMovimentacoes() {
      try {
        const _url = `http://localhost:5001/Movimentacoes/${this.pessoa.id}/NovoAnoDeMovimentacoes`;
        var resultado = await httpRequest.post(_url, this.movimentacoes);
        console.log(resultado);
      } catch (error) {
        console.error(error);
        this.$toast.open({
          message:
            "Houve um problema ao tentar registrar o ano de movimentações. Por favor, tente novamente mais tarde.",
          type: "error",
        });
      }
    },
  },
};
</script>

<style>
</style>