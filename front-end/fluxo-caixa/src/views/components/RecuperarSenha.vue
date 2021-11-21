<template>
  <div class="formulario w-100">
    <form name="recuperarSenha" class="form w-100">
      <div class="tooltip">
        <p>Recuperar senha</p>
      </div>
      <div class="form-group">
        <label class="form-label" for="email">E-mail</label>
        <input class="form-control" type="text" name="Email" id="email" />
      </div>
      <div class="form-group">
        <label class="form-label" for="username">Username</label>
        <input class="form-control" type="text" name="Username" id="username" />
      </div>
      <div class="form-footer--modal">
        <button class="btn" type="button" @click="onClose && onClose()">
          Cancelar
        </button>
        <button class="btn btn-primary" type="button">Recuperar</button>
      </div>
    </form>
  </div>
</template>

<script>
export default {
  name: "RecuperarSenha",
  props: {
    onClose: {
      type: Function,
      required: true,
    },
  },
};
</script>

<style lang="scss" scoped>
.formulario {
  background-color: #fff;
  display: flex;
  align-items: center;
  padding: 20px;
  border-radius: 16px;
  min-height: 300px;

  label.form-label {
    color: #222;
  }

  div.form-footer--modal {
    margin-top: 2.5rem;
    display: grid;
    grid-template-columns: 1fr 1fr;
    grid-gap: 2rem;
  }
}
</style>