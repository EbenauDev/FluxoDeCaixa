<template>
  <div class="formulario configuracoes-da-conta">
    <form name="configuracoesDaConta" class="form" v-if="true">
      <div class="tooltip">
        <p class="m-0">Configurações de Segurança</p>
      </div>
      <div>
        <div>
          <p class="f-16 color-primary f-bold m-0">Manutenção de Senhas</p>
        </div>
        <div class="form-group">
          <label class="form-label" for="senha">Nova senha</label>
          <input
            class="form-control"
            type="password"
            name="Senha"
            id="senha"
            v-model="credenciaisDeAcesso.senha"
          />
        </div>
        <div class="form-group">
          <label class="form-label" for="confirmarSenha">Confirmar senha</label>
          <input
            class="form-control"
            type="password"
            name="ConfirmarSenha"
            id="confirmarSenha"
            v-model="credenciaisDeAcesso.confirmarSenha"
          />
          <p
            class="f-11 c-red"
            v-if="
              credenciaisDeAcesso.confirmarSenha != credenciaisDeAcesso.senha
            "
          >
            As senhas não confererem
          </p>
        </div>
      </div>
      <div class="form-footer">
        <button class="btn btn-primary" type="button" @click="atualizarSenha()">
          Atualizar
        </button>
      </div>
    </form>
  </div>
</template>

<script>
import httpRequest from "@/services/$httpRequest";
import { mapState } from "vuex";
export default {
  name: "AlterarSenha",
  computed: {
    ...mapState({
      pessoa: (state) => state.pessoa.pessoa,
    }),
  },
  data() {
    return {
      credenciaisDeAcesso: {},
    };
  },
  methods: {
    async atualizarSenha() {
      try {
        await httpRequest.put(
          `Pessoa/${this.pessoa.id}/AlterarSenha`,
          this.credenciaisDeAcesso
        );
        this.$toast.open({
          message: "Senha alterada com sucesso",
          type: "success",
        });
        this.$router.push({ name: "Autenticado.VisaoGeral" });
      } catch (error) {
        console.error(error);
        this.$toast.open({
          message:
            "Houve um problema ao tentar alterar a sua senha. Tente novamente mais tarde",
          type: "error",
        });
      }
    },
  },
};
</script>

<style lang="scss" scoped>
.configuracoes-da-conta {
  max-width: 420px;
  margin-top: 2.3rem;
  margin-left: 1.5rem;
}
</style>