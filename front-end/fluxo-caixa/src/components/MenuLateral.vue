<template>
  <div class="menu-lateral">
    <div class="__avatar">
      <img
        v-if="pessoa && pessoa.avatar"
        :src="pessoa.avatar"
        :alt="pessoa.username"
      />
    </div>
    <div class="__opcoes">
      <button
        class="opcao"
        @click="toRoute('Autenticado.ConfiguracoesDaConta')"
        :class="{
          'opcao--ativo': routeActive == 'Autenticado.ConfiguracoesDaConta',
        }"
      >
        <i class="fas fa-user-cog"></i>
      </button>
      <button
        class="opcao"
        @click="toRoute('Autenticado.VisaoGeral')"
        :class="{ 'opcao--ativo': routeActive == 'Autenticado.VisaoGeral' }"
      >
        <i class="fas fa-database"></i>
      </button>
    </div>
    <div class="__rodape">
      <div>
        <button class="opcao" @click="singOut()">
          <i class="fas fa-sign-out-alt"></i>
        </button>
      </div>
    </div>
  </div>
</template>

<script>
import { mapState } from "vuex";
import sessionService from "@/services/$sessionService";
export default {
  name: "MenuLateral",
  computed: {
    ...mapState({
      pessoa: (state) => state.pessoa.pessoa,
    }),
  },
  data() {
    return {
      routeActive: null,
    };
  },
  methods: {
    toRoute(routeName) {
      this.$router.push({ name: routeName });
      this.routeActive = routeName;
    },
    singOut() {
      sessionService.deleteSessionToken();

      this.$router.push({ name: "NaoAutenticado" });
      this.$toast.open({
        message: `Você foi deslogado com sucesso. até outra hora ${this.pessoa.username}`,
        type: "success",
      });
      this.$store.dispatch("pessoa/limparPessoaState");
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
    max-width: 70px;
    max-height: 70px;
    min-height: 70px;
    min-width: 70px;
    transition: all 0.2s linear;
    width: 100%;
    height: 100vh;
    background: #ddd;
    border-radius: 50%;
    margin-bottom: 4rem;
    margin-top: 2.5rem;
    img {
      width: 100%;
      height: 100%;
      border-radius: 50%;
      cursor: pointer;
      border: 3px solid #fff;
    }
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