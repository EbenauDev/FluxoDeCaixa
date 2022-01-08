<template>
  <div class="formulario w-100">
    <form name="recuperarSenha" class="form w-100">
      <div class="tooltip">
        <p>Recuperar senha</p>
      </div>

      <div class="form-group">
        <label class="form-label" for="username">Username</label>
        <input
          class="form-control"
          type="text"
          name="Username"
          id="username"
          v-model="recuperarSenha.username"
        />
      </div>
      <div class="form-group">
        <label class="form-label" for="dataNascimento">Data nascimento</label>
        <input
          class="form-control"
          type="text"
          name="DataNascimento"
          id="dataNascimento"
          v-format-date="{ format: 'DD/MM/YYYY' }"
          v-model="recuperarSenha.nascimento"
          required
        />
      </div>
      <div class="form-footer--modal">
        <button class="btn" type="button" @click="onClose && onClose()">
          Cancelar
        </button>
        <button
          class="btn btn-primary"
          type="button"
          @click="solicitarRecuperacaoDeSenha && solicitarRecuperacaoDeSenha()"
        >
          Recuperar
        </button>
      </div>
    </form>
  </div>
</template>

<script>
import httpRequest from "@/services/$httpRequest";
import dateFormater from "@/util/formatar-datas";
export default {
  name: "RecuperarSenha",
  props: {
    onClose: {
      type: Function,
      required: true,
    },
  },
  data() {
    return {
      recuperarSenha: {
        username: null,
        nascimento: null,
      },
    };
  },
  methods: {
    async solicitarRecuperacaoDeSenha() {
      try {
        this.recuperarSenha.dataDeNascimento = dateFormater(
          "DD/MM/YYYY",
          this.recuperarSenha.nascimento
        );
        await httpRequest.post("Pessoa/RecuperarSenha", this.recuperarSenha);
        this.$toast.open({
          message:
            "Enviamos um e-mail para você com o link de recuperação de senha",
          type: "success",
        });
        this.onClose && this.onClose();
      } catch (error) {
        this.$toast.open({
          message:
            "Houve um problema ao solicitar a recuperação de senha. Tente novamente mais tarde",
          type: "error",
        });
      }
    },
  },
};
</script>
