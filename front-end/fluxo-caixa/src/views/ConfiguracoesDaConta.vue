<template>
  <div class="formulario configuracoes-da-conta">
    <form name="configuracoesDaConta" class="form" v-if="true">
      <div class="tooltip">
        <p class="m-0">Configurações da Conta</p>
      </div>
      <div>
        <div>
          <p class="f-16 color-primary f-bold m-0">Dados Pessoais</p>
        </div>
        <div class="form-group">
          <label class="form-label" for="nome">Nome</label>
          <input
            class="form-control"
            type="text"
            name="Nome"
            id="nome"
            v-model="dadosDaConta.nome"
            readonly
          />
        </div>
        <div class="form-group">
          <label class="form-label" for="dataNascimento">Data nascimento</label>
          <input
            class="form-control"
            type="text"
            name="DataNascimento"
            id="dataNascimento"
            v-model="dadosDaConta.dataNascimento"
            readonly
          />
        </div>
      </div>
      <div>
        <div>
          <p class="f-16 color-primary f-bold m-0">Dados de login</p>
        </div>
        <div class="form-group">
          <label class="form-label" for="avatar">Avatar</label>
          <input
            class="form-control"
            type="url"
            name="Avatar"
            id="avatar"
            v-model="dadosDaConta.avatar"
          />
        </div>
        <div class="form-group">
          <label class="form-label" for="email">Email</label>
          <input
            class="form-control"
            type="url"
            name="Email"
            id="email"
            v-model="dadosDaConta.email"
          />
        </div>
      </div>
      <div class="form-footer">
        <button class="btn btn-primary" type="button" @click="atualizarConta()">
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
  name: "EditarConta",
  computed: {
    ...mapState({
      pessoa: (state) => state.pessoa.pessoa,
    }),
  },
  created() {
    this.dadosDaConta = Object.assign({}, this.pessoa);
  },
  data() {
    return {
      dadosDaConta: {},
    };
  },
  methods: {
    async atualizarConta() {
      try {
        var pessoa = await httpRequest.put(
          `Pessoa/${this.pessoa.id}/AtualizarCadastro`,
          this.dadosDaConta
        );
        this.$store.dispatch("pessoa/atualizarPessoaState", pessoa);
        this.$toast.open({
          message: "Seu cadastro foi atualizado com sucesso.",
          type: "success",
        });
        this.$router.push({ name: "Autenticado.VisaoGeral" });
      } catch (error) {
        this.$toast.open({
          message:
            "Houve um problema ao tentar atualizar o seu cadastro. Por favor, tente novamente mais tarde.",
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