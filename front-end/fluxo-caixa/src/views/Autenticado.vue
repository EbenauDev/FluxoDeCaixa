<template>
  <div class="autenticado">
    <div class="menu-lateral no-print">
      <menu-lateral></menu-lateral>
    </div>
    <div class="conteudo-principal">
      <router-view />
    </div>
  </div>
</template>

<script>
import MenuLateral from "../components/MenuLateral.vue";
import sessionService from "@/services/$sessionService";
import httpRequest from "@/services/$httpRequest";
import { mapState } from "vuex";
export default {
  name: "PaginaInicial",
  components: {
    MenuLateral,
  },
  created() {
    if (Object.keys(this.pessoa).length <= 0) {
      var tokenPessoa = sessionService.getParseTokenSession();
      if (tokenPessoa && tokenPessoa.id) {
        httpRequest.setHeaders("Authorization", `Bearer ${sessionService.getTokenSession()}`);
        this.recuperarCadastroPessoa(tokenPessoa.id);
      }
    }
  },
  computed: {
    ...mapState({
      pessoa: (state) => state.pessoa.pessoa,
    }),
  },
  methods: {
    async recuperarCadastroPessoa(pessoaId) {
      await this.$store.dispatch("pessoa/recuperarPessoaPorId", pessoaId);
    },
  },
};
</script>

<style lang="scss" scoped>
.autenticado {
  display: grid;
  grid-template-columns: 200px 1fr;
}
</style>
