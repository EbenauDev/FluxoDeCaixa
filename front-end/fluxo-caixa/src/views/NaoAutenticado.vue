<template>
  <div class="tela-principal">
    <div class="titulo">
      <h1>Controle suas finanças de um jeito mais simples</h1>
      <p>Crie sua conta agora mesmo e tenha acesso a recursos incríveis</p>
    </div>
    <div class="formulario autenticao">
      <form v-if="!criarConta" name="login" class="form">
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
            v-model="credenciais.username"
          />
        </div>
        <div class="form-group">
          <label class="form-label" for="senha">Senha</label>
          <input
            class="form-control"
            type="password"
            name="Senha"
            id="senha"
            v-model="credenciais.senha"
          />
          <div class="text-right c-black">
            <span
              class="cursor-pointer f-13 color-primary f-bold"
              @click="mostrarModalRecuperarSenha"
              >Esqueceu a senha?</span
            >
          </div>
        </div>
        <div class="form-footer">
          <button class="btn btn-primary" type="button" @click="realizarLogin">
            Login
          </button>
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
              v-model="novaConta.nome"
              required
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
              v-format-date="{ format: 'DD/MM/YYYY' }"
              v-model="novaConta.dataNascimento"
              required
            />
          </div>
          <div class="form-group">
            <label class="form-label" for="email">E-mail</label>
            <input
              class="form-control"
              type="email"
              name="Email"
              id="email"
              v-model="novaConta.email"
              required
            />
          </div>
        </div>
        <div>
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
              required
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
              required
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
              required
            />
          </div>
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
import sessionService from "@/services/$sessionService";
import dateFormater from "@/util/formatar-datas";
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
      credenciais: {
        username: "",
        senha: "",
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
          await httpRequest.get(`pessoa?username=${this.novaConta.username}`);
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
        this.novaConta.dataNascimento = dateFormater(
          "DD/MM/YYYY",
          this.novaConta.dataNascimento
        );
        var pessoa = await httpRequest.post(
          "pessoa/NovoCadastro",
          this.novaConta
        );
        httpRequest.setHeaders("Authorization", `Bearer ${pessoa.token}`);
        this.$store.dispatch("pessoa/atualizarPessoaState", pessoa);
        this.$router.push("Autenticado");
        this.$toast.open({
          message: `conta criada com sucesso. Seja bem vindo ${pessoa.username}`,
          type: "success",
        });
      } catch (error) {
        this.$toast.open({
          message:
            "Houve um problema ao tentar salvar o seu cadastro. Por favor, tente novamente mais tarde.",
          type: "error",
        });
      }
    },
    async realizarLogin() {
      try {
        var pessoa = await httpRequest.post(
          "Login/Autenticar",
          this.credenciais
        );
        httpRequest.setHeaders("Authorization", `Bearer ${pessoa.token}`);
        sessionService.setSessionToken(pessoa.token);
        this.$store.dispatch("pessoa/atualizarPessoaState", pessoa);
        this.$router.push("Autenticado");
        this.$toast.open({
          message: `Seja bem vindo ${pessoa.username}`,
          type: "success",
        });
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
  grid-template-columns: 1.4fr 1fr;
  align-content: center;
  height: 100vh;
  max-width: 1024px;
  margin: 0 auto;
  .autenticao {
    background-color: #fff;
    display: flex;
    align-items: center;
    padding: 25px 30px;
    border-radius: 16px;
    width: 100%;
    max-width: 360px;
  }
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
