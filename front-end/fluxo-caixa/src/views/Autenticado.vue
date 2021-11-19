<template>
  <div class="autenticado">
    <div class="menu-lateral">
      <menu-lateral></menu-lateral>
    </div>
    <div class="conteudo-principal"></div>
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
  grid-template-columns: 340px 1fr;
  grid-template-rows: 110px 1fr;
  grid-template-areas:
    "header header"
    "menu conteudo";

  .__header {
    grid-area: header;
  }
  .__menu {
    grid-area: menu;
    height: 100vh;
    background: var(--main-color);
    max-width: 275px;
    padding: 5px;
    display: flex;
    flex-direction: column;
  }
  .__conteudo {
    grid-area: conteudo;
    padding: 25px 40px;
    background: #fff;
    max-width: 70vw;
    width: 100%;
    margin: 20px;
    box-shadow: 0 5px 5px rgb(0 0 0 / 60%);
    border-radius: 16px;
  }
}
</style>
