<template>
  <div class="container-nao-autenticado background-primary">
    <div class="__logo">
      <h1>Fluxo de Caixa</h1>
      <p>Controle suas finanças de um jeito mais simples</p>
    </div>
    <div class="__login">
      <div class="tooltip">
        <label>
          Login
          <input type="radio" id="login" value="Login" hidden />
        </label>
        <label>
          Nova Conta
          <input type="radio" id="login" value="Login" hidden />
        </label>
      </div>

      <form
        v-if="fazerNovaConta == false"
        class="form form-autenticacao"
        name="loginUsuario"
      >
        <div class="form-group">
          <label class="form-label" for="Nickname">Nickname</label>
          <input class="form-control" id="Nickname" type="text" />
        </div>
        <div class="form-group">
          <label class="form-label" for="Senha">Senha</label>
          <input class="form-control" id="Senha" type="password" />
        </div>
        <div class="form-footer">
          <button class="btn btn-cancel" type="button" @click="closeModal">
            Cancelar
          </button>
          <button class="btn btn-primary f-bold upper" type="button">
            Login
          </button>
        </div>
      </form>
      <form
        v-if="fazerNovaConta"
        class="form form-autenticacao"
        name="novoUsuario"
      >
        <div class="form-group">
          <label class="form-label" for="descricao">Descrição</label>
          <input class="form-control" id="descricao" type="text" />
        </div>
        <div class="form-group">
          <label class="form-label" for="valor">Valor</label>
          <input class="form-control" id="valor" type="text" />
        </div>
        <div class="form-footer">
          <button class="btn btn-cancel" type="button" @click="closeModal">
            Cancelar
          </button>
          <button
            class="btn btn-primary f-bold upper"
            type="button"
            @click="salvarNovoCusto"
          >
            Salvar
          </button>
        </div>
      </form>
    </div>
  </div>
</template>

<script>
export default {
  name: "NaoAutenticado",
  data() {
    return {
      usuario: {
        nickname: "",
        senha: null,
      },
      fazerNovaConta: false,
      novoUsuario: {
        nickname: "",
        avatar: "",
        senha: "",
        confirmarSenha: "",
        email: "",
      },
    };
  },
};
</script>

<style lang="scss" scoped>
.container-nao-autenticado {
  width: 100%;
  height: 100%;
  display: grid;
  grid-template-columns: 340px 485px;
  grid-gap: 5rem;
  place-content: center;
  place-items: center;

  .__login {
    width: 100%;
    height: 100%;
    background: #fff;
    display: flex;
    border-radius: 4px;
    box-shadow: 2px 3px 17px 3px #1e1e1ea6;
    padding: 10px;
    display: flex;
    flex-direction: column;
  }
  .form-autenticacao {
  }
}
</style>