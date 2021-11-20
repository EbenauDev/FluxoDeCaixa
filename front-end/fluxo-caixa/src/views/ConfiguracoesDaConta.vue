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
          <label class="form-label" for="username">Nome</label>
          <input
            class="form-control"
            type="text"
            name="Username"
            id="username"
          />
        </div>
        <div class="form-group">
          <label class="form-label" for="username">Data nascimento</label>
          <input
            class="form-control"
            type="text"
            name="Username"
            id="username"
          />
        </div>
      </div>
      <div>
        <div>
          <p class="f-16 color-primary f-bold m-0">Dados de login</p>
        </div>
        <div class="form-group">
          <label class="form-label" for="username">Avatar</label>
          <input
            class="form-control"
            type="url"
            name="Username"
            id="username"
          />
        </div>
        <div class="form-group">
          <label class="form-label" for="username">Senha</label>
          <input
            class="form-control"
            type="password"
            name="Username"
            id="username"
          />
        </div>
        <div class="form-group">
          <label class="form-label" for="username">Confirmar senha</label>
          <input
            class="form-control"
            type="password"
            name="Username"
            id="username"
          />
        </div>
      </div>
      <div class="form-footer">
        <button class="btn btn-primary" type="button">Criar</button>
        <div>
          <span>Cancelar</span>
        </div>
      </div>
    </form>
  </div>
</template>

<script>
export default {
  name: "EditarConta",
  data() {
    return {
      dadosDaConta: {},
    };
  },
  methods: {
    atualizarConta() {},
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