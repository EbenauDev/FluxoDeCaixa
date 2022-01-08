<template>
  <div class="formulario w-100">
    <form name="recuperarSenha" class="form w-100">
      <div class="tooltip">
        <p>Metas</p>
      </div>

      <div class="form-group">
        <label class="form-label" for="descricaoMeta">Qual é a sua meta?</label>
        <textarea
          class="form-control"
          name="descricaoMeta"
          id="descricaoMeta"
          rows="5"
          v-model="meta.descricao"
        ></textarea>
      </div>
      <div class="form-group">
        <label class="form-label" for="valorDesejado"
          >Qual é o valor desejado?</label
        >
        <input
          class="form-control"
          type="text"
          name="valorDesejado"
          id="valorDesejado"
          v-model="meta.valorDesejado"
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
          @click="handlerMeta && handlerMeta()"
        >
          Nova
        </button>
      </div>
    </form>
  </div>
</template>

<script>
export default {
  name: "FormularioMeta",
  data() {
    return {
      meta: {},
    };
  },
  methods: {
    handlerMeta() {
      console.log("to do...");
    },
  },
};
</script>

<style>
</style>