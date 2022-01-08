<template>
  <div>
    <div class="card">
      <div class="card__header">
        <div>
          <p class="m-0 card__title">Metas</p>
        </div>
        <div class="card-header__actions">
          <button class="btn btn-outline" @click="abrirModalHandler()">
            Novo objetivo
          </button>
        </div>
      </div>
      <div class="card__body">
        <h4 class="color-primary text-center">
          Funcionalidade não está disponível nessa versão
        </h4>
      </div>
    </div>
    <div v-if="abrirModalFormulario">
      <modal>
        <formulario-meta></formulario-meta>
      </modal>
    </div>
  </div>
</template>

<script>
import Modal from "@/components/Modal";
import FormularioMeta from "./FormularioMeta.vue";
import { mapState } from "vuex";
export default {
  name: "ListagemDeMetas",
  components: {
    Modal,
    FormularioMeta,
  },
  computed: {
    ...mapState({
      metas: (state) => state.metas.metas,
    }),
  },
  data() {
    return {
      abrirModalFormulario: false,
    };
  },
  methods: {
    abrirModalHandler() {
      this.abrirModalFormulario = !this.abrirModalFormulario;
    },
  },
};
</script>

<style>
</style>