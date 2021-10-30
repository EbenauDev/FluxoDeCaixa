<template>
  <div class="container-nao-autenticado background-primary">
    <div class="__logo">
      <h1>Fluxo de Caixa</h1>
      <p>Controle suas finanças de um jeito mais simples</p>
    </div>
    <div class="__login">
      <div class="tooltip">
        <label :class="{ ativo: opcaoFormulario == 'Login' }">
          Login
          <input type="radio" value="Login" hidden v-model="opcaoFormulario" />
        </label>
        <label :class="{ ativo: opcaoFormulario == 'NovaConta' }">
          Nova Conta
          <input
            type="radio"
            value="NovaConta"
            hidden
            v-model="opcaoFormulario"
          />
        </label>
      </div>

      <form
        v-if="opcaoFormulario == 'Login'"
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
          <span></span>
          <button class="btn btn-primary f-bold upper" type="button">
            Login
          </button>
        </div>
      </form>
      <form
        v-if="opcaoFormulario == 'NovaConta'"
        class="form form-autenticacao"
        name="novoUsuario"
      >
        <div class="form-group">
          <label class="form-label" for="Nickname">Nickname</label>
          <input class="form-control" id="Nickname" type="text" />
        </div>
        <div class="form-group">
          <label class="form-label" for="Senha">Avatar</label>
          <input class="form-control" id="Senha" type="url" />
        </div>
        <div class="form-group">
          <label class="form-label" for="Senha">Senha</label>
          <input class="form-control" id="Senha" type="password" />
        </div>
        <div class="form-group">
          <label class="form-label" for="ConfirmarSenha">Confirmar Senha</label>
          <input class="form-control" id="ConfirmarSenha" type="password" />
        </div>
        <div class="form-footer">
          <span></span>
          <button class="btn btn-primary f-bold upper" type="button">
            Registrar-se
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
      opcaoFormulario: "Login",
      novoUsuario: {
        nickname: "",
        avatar: "",
        senha: "",
        confirmarSenha: "",
        email: "",
      },
      fazerNovaConta: false,
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
    .tooltip {
      max-height: 30px;
      max-width: 170px;
      height: 100%;
      display: flex;
      justify-content: space-between;
      align-items: center;
      background: #f8f8f8;
      border-radius: 20px;
      overflow: hidden;
      font-size: 14px;
      label.ativo {
        background: var(--main-color-dark);
        color: #fff;
      }
      label {
        flex: 1;
        height: 100%;
        display: flex;
        align-items: center;
        justify-content: center;
        cursor: pointer;
      }
    }
  }
  .form-autenticacao {
  }
}
</style>