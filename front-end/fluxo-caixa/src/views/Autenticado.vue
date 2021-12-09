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
import { mapState } from "vuex";
export default {
  name: "PaginaInicial",
  components: {
    MenuLateral,
  },
  created() {
    console.log(this.pessoa);
    this.$store.dispatch("fluxoDeCaixa/recuperarCaixaMaisRecente");
  },
  computed: {
    ...mapState({
      pessoa: (state) => state.pessoa.pessoa,
    }),
  },
};
</script>

<style lang="scss" scoped>
.autenticado {
  display: grid;
  grid-template-columns: 200px 1fr;
}
</style>
