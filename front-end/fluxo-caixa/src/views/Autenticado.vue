<template>
  <div class="autenticado">
    <div class="menu-lateral">
      <menu-lateral></menu-lateral>
    </div>
    <div class="conteudo-principal">
      <router-view />
    </div>
  </div>
</template>

<script>
import MenuLateral from "../components/MenuLateral.vue";

export default {
  name: "PaginaInicial",
  components: {
    MenuLateral,
  },
  created() {
    this.$store.dispatch("fluxoDeCaixa/recuperarCaixaMaisRecente");
  },
};
</script>

<style lang="scss" scoped>
.autenticado {
  display: grid;
  grid-template-columns: 200px 1fr;
}
</style>
