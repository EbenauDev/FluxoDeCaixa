<template>
  <div>
    <div>
      <tabela-receitas></tabela-receitas>
      <tabela-custos></tabela-custos>
    </div>
  </div>
</template>

<script>
import TabelaCustos from "../components/TabelaCustos.vue";
import TabelaReceitas from "../components/TabelaReceitas.vue";
export default {
  name: "PaginaInicial",
  components: { TabelaCustos, TabelaReceitas },
};
</script>

<style lang="sass" scoped>
</style>
