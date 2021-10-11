<template>
  <div>
    <div>
      <tabela-receitas></tabela-receitas>
      <tabela-custos></tabela-custos>
      <fluxo-de-caixa></fluxo-de-caixa>
    </div>
  </div>
</template>

<script>
import TabelaCustos from "../components/TabelaCustos.vue";
import TabelaReceitas from "../components/TabelaReceitas.vue";
import FluxoDeCaixa from "../components/FluxoDeCaixa.vue";
export default {
  name: "PaginaInicial",
  components: { TabelaCustos, TabelaReceitas, FluxoDeCaixa },
  created() {
    console.log(this.$store);
    this.$store.dispatch("fluxoDeCaixa/recuperarCaixaMaisRecente");
  },
};
</script>

<style lang="sass" scoped>
</style>
