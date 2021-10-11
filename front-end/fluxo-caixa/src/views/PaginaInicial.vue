<template>
  <div>
    <div>
      <header>
        <h1>Fluxo de Caixa</h1>
        <h2 class="main-color-dark">
          Utile o campo abaixo para filtrar o mês de pesquisa
        </h2>
      </header>
      <form>
        <div>
          <select class="form-control">
            <option value="10/2021">Mes de referência 10/2021</option>
            <option value="11/2021">Mes de referência 11/2021</option>
            <option value="12/2021">Mes de referência 12/2021</option>
          </select>
        </div>
      </form>

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
