<template>
  <div class="tela-principal">
    <div class="titulo">
      <h1>Controle suas finanças de um jeito mais simples</h1>
      <p>Crie sua conta agora mesmo e tenha acesso a recursos incríveis</p>
    </div>
    <div class="formulario">
      <form name="login" class="form" v-if="false">
        <div class="tooltip">
          <p class="m-0">Login</p>
        </div>
        <div class="form-group">
          <label class="form-label" for="username">Username</label>
          <input
            class="form-control"
            type="text"
            name="Username"
            id="username"
          />
        </div>
        <div class="form-group">
          <label class="form-label" for="senha">Senha</label>
          <input class="form-control" type="text" name="Senha" id="senha" />
          <div class="text-right c-black">
            <span
              class="cursor-pointer f-13 color-primary f-bold"
              @click="mostrarModalRecuperarSenha"
              >Esqueceu a senha?</span
            >
          </div>
        </div>
        <div class="form-footer">
          <button class="btn btn-primary">Login</button>
          <div>
            <span>Você não possui uma conta? Crie uma</span>
          </div>
        </div>
      </form>
      <form name="novaConta" class="form" v-if="true">
        <div class="tooltip">
          <p class="m-0">Nova Conta</p>
        </div>
        <div v-if="etapasCadastro == 'primeiraEtapa'">
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
        <div v-if="etapasCadastro == 'segundaEtapa'">
          <div>
            <p class="f-16 color-primary f-bold m-0">Dados de login</p>
          </div>
          <div class="form-group">
            <label class="form-label" for="username">Username</label>
            <input
              class="form-control"
              type="text"
              name="Username"
              id="username"
            />
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
        <div class="m-t-5 form-steps">
          <input
            type="radio"
            name="etapasCadastro"
            v-model="etapasCadastro"
            value="primeiraEtapa"
          />
          <input
            type="radio"
            name="etapasCadastro"
            v-model="etapasCadastro"
            value="segundaEtapa"
          />
        </div>
        <div class="form-footer">
          <button class="btn btn-primary">Criar</button>
          <div>
            <span>Cancelar</span>
          </div>
        </div>
      </form>
    </div>
    <modal v-if="recuperarSenha">
      <recuperar-senha :on-close="mostrarModalRecuperarSenha"></recuperar-senha>
    </modal>
  </div>
</template>

<script>
import Modal from "../components/Modal.vue";
import RecuperarSenha from "./components/RecuperarSenha.vue";
export default {
  name: "NaoAutenticado",
  components: {
    Modal,
    RecuperarSenha,
  },
  data() {
    return {
      recuperarSenha: false,
      etapasCadastro: "primeiraEtapa",
    };
  },
  methods: {
    mostrarModalRecuperarSenha() {
      this.recuperarSenha = !this.recuperarSenha;
    },
  },
};
</script>

<style lang="scss">
.tela-principal {
  display: grid;
  grid-template-columns: 1fr 1fr;
  align-content: center;
  height: 100vh;
  max-width: 960px;
  margin: 0 auto;
  .titulo {
    font-family: "poppins";
    h1 {
      font-size: 2.5rem;
      line-height: 55px;
      max-width: 437px;
      margin-bottom: 40px;
    }
    p {
      font-size: 1rem;
      line-height: 20px;
      max-width: 437px;
    }
  }
  .formulario {
    background-color: #fff;
    display: flex;
    align-items: center;
    padding: 20px;
    border-radius: 16px;
    min-height: 300px;
    div.tooltip {
      margin-bottom: 15px;

      p {
        color: var(--main-color-dark);
        position: relative;
        font-weight: 600;
        font-size: 1.6rem;
        &::before {
          content: "";
          width: 40px;
          height: 6px;
          background-color: var(--main-color-dark);
          border-radius: 6px;
          position: absolute;
          top: -10px;
        }
      }
    }

    label.form-label {
      color: #222;
    }
    .form-steps {
      width: 100%;
      display: flex;
      align-items: center;
      justify-content: center;
      margin-top: 15px;
    }
    div.form-footer {
      flex-direction: column;
      align-items: center;
      margin-top: 40px;
      button {
        max-width: 190px;
        width: 100%;
      }
      span {
        color: var(--main-color-dark);
        font-size: 12px;
        font-weight: 600;
        margin-top: 4rem;
        display: block;
        cursor: pointer;
      }
    }
  }
}
</style>
