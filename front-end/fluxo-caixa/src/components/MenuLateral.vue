<template>
  <div class="menu-lateral">
    <div class="__avatar"></div>
    <div class="__opcoes">
      <button
        class="opcao"
        @click="toRoute('configuracoes-da-conta')"
        :class="{ 'opcao--ativo': routeActive == 'configuracoes-da-conta' }"
      >
        <i class="fas fa-user-cog"></i>
      </button>
      <button
        class="opcao"
        @click="toRoute('visao-geral')"
        :class="{ 'opcao--ativo': routeActive == 'visao-geral' }"
      >
        <i class="fas fa-database"></i>
      </button>
    </div>
    <div class="__rodape">
      <div>
        <button class="opcao">
          <i class="fas fa-sign-out-alt"></i>
        </button>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "MenuLateral",
  data() {
    return {
      routeActive: null,
    };
  },
  methods: {
    toRoute(route) {
      console.log(this);
      this.$router.push({
        path: route,
      });
      this.routeActive = route;
    },
  },
};
</script>

<style lang="scss" scoped>
.menu-lateral {
  max-width: 110px;
  min-width: 100px;
  width: 100%;
  max-height: 768px;
  height: 100vh;
  background: #08133d;
  border-radius: 40px;
  display: flex;
  flex-direction: column;
  align-items: center;
  padding: 10px;
  margin: 35px 40px;
  position: relative;
  .__avatar {
    max-width: 75px;
    max-height: 75px;
    min-height: 70px;
    min-width: 70px;
    transition: all 0.2s linear;
    width: 100%;
    height: 100vh;
    background: #ddd;
    border-radius: 50%;
    margin-bottom: 4rem;
    margin-top: 2.5rem;
  }
  .__opcoes {
    min-height: 420px;
  }
  .__rodape {
    position: absolute;
    bottom: 15px;
  }

  .opcao {
    max-width: 60px;
    width: 100vw;
    display: flex;
    align-items: center;
    justify-content: center;
    min-height: 40px;
    font-size: 1.4rem;
    margin: 15px 0;
    border-radius: 10px;
    border: none;
    cursor: pointer;
    background: transparent;
    color: #fff;
  }
  .opcao--ativo {
    color: var(--main-color-dark);
    background: #fff;
    box-shadow: inset 1px 3px 7px 0px rgb(0 0 0 / 60%);
  }
}
</style>