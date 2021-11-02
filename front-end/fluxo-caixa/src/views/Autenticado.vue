<template>
  <div class="autenticado">
    <header-personalizado class="__header"></header-personalizado>
    <div class="__menu">
      <div>
        <ul>
          <li>Manutenção</li>
          <li>Histórico</li>
        </ul>
      </div>
      <div>
        <span>Sair</span>
      </div>
    </div>
    <section class="__conteudo">
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
    </section>
  </div>
</template>

<script>
import HeaderPersonalizado from "../components/Header.vue";
import TabelaCustos from "../components/TabelaCustos.vue";
import TabelaReceitas from "../components/TabelaReceitas.vue";
import FluxoDeCaixa from "../components/FluxoDeCaixa.vue";
export default {
  name: "PaginaInicial",
  components: {
    HeaderPersonalizado,
    TabelaCustos,
    TabelaReceitas,
    FluxoDeCaixa,
  },
  created() {
    console.log(this.$store);
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
