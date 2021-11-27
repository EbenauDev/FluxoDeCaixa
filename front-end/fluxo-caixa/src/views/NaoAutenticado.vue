<template>
  <div class="tela-principal">
    <div class="titulo">
      <h1>Controle suas finanças de um jeito mais simples</h1>
      <p>Crie sua conta agora mesmo e tenha acesso a recursos incríveis</p>
    </div>
    <div class="formulario">
      <form name="login" class="form" v-if="!criarConta">
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
          <button class="btn btn-primary" type="button">Login</button>
          <div>
            <span @click="criarConta = !criarConta"
              >Você não possui uma conta? Crie uma</span
            >
          </div>
        </div>
      </form>
      <form name="novaConta" class="form" v-if="criarConta">
        <div class="tooltip">
          <p class="m-0">Nova Conta</p>
        </div>
        <div v-if="etapasCadastro == 'primeiraEtapa'">
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
              v-model="novaConta.nome"
            />
          </div>
          <div class="form-group">
            <label class="form-label" for="dataNascimento"
              >Data nascimento</label
            >
            <input
              class="form-control"
              type="text"
              name="DataNascimento"
              id="dataNascimento"
              v-model="novaConta.dataNascimento"
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
              @change="usernameEstahDisponivelAsync"
              v-model="novaConta.username"
            />
          </div>
          <div class="form-group">
            <label class="form-label" for="avatar">Avatar</label>
            <input
              class="form-control"
              type="url"
              name="Avatar"
              id="avatar"
              v-model="novaConta.avatar"
            />
          </div>
          <div class="form-group">
            <label class="form-label" for="senha">Senha</label>
            <input
              class="form-control"
              type="password"
              name="Senha"
              id="senha"
              v-model="novaConta.senha"
            />
          </div>
          <div class="form-group">
            <label class="form-label" for="confirmarSenha"
              >Confirmar senha</label
            >
            <input
              class="form-control"
              type="password"
              name="ConfirmarSenha"
              id="confirmarSenha"
              v-model="novaConta.confirmarSenha"
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
          <button
            class="btn btn-primary"
            type="button"
            @click="cadastrarNovaContaAsync"
          >
            Criar
          </button>
          <div>
            <span @click="criarConta = !criarConta">Cancelar</span>
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
import httpRequest from "@/services/$httpRequest";
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
      criarConta: false,
      novaConta: {
        nome: "",
        dataNascimento: "",
        email: "",
        senha: "",
        avatar: "",
        username: "",
      },
    };
  },
  methods: {
    mostrarModalRecuperarSenha() {
      this.recuperarSenha = !this.recuperarSenha;
    },
    usernameEstahDisponivelAsync() {
      setTimeout(async () => {
        try {
          console.log(this);
          const _usernameEstahDisponvel = await httpRequest.get(
            `http://localhost:5000/api/pessoa?username=${this.novaConta.username}`
          );
          console.log(`_usernameEstahDisponvel: ${_usernameEstahDisponvel}`);
        } catch (error) {
          this.$toast.open({
            message: `Houve um problema ao tentar verificar se o username ${this.novaConta.username} está disponível.`,
            type: "error",
          });
        }
      }, 200);
    },
    async cadastrarNovaContaAsync() {
      try {
        await httpRequest.post(
          "http://localhost:5000/api/pessoa/NovoCadastro",
          JSON.stringify(this.novaConta)
        );
      } catch (error) {
        this.$toast.open({
          message:
            "Houve um problema ao tentar salvar o seu cadastro. Por favor, tente novamente mais tarde.",
          type: "error",
        });
      }
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
}
</style>
