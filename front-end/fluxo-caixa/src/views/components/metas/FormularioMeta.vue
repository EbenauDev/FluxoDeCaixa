<template>
  <div class="formulario w-100">
    <form name="recuperarSenha" class="form w-100">
      <div class="tooltip">
        <p>{{ configuracao.ehNovaMeta ? "Nova Meta" : "Atualizar meta" }}</p>
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
          Salvar
        </button>
      </div>
    </form>
  </div>
</template>

<script>
export default {
  name: "FormularioMeta",
  props: {
    onClose: {
      type: Function,
      required: true,
    },
    configuracao: {
      type: Object,
    },
  },
  created() {
    console.log(this.configuracao);
    if (this.configuracao.ehNovaMeta == false) {
      this.meta = this.configuracao.metaParaAtualizar;
    }
  },
  data() {
    return {
      meta: {},
    };
  },
  methods: {
    _atualizarMeta() {
      this.$store
        .dispatch("metas/atualizarMeta", {
          pessoaId: this.pessoa.id,
          meta: this.meta,
        })
        .then(() => {
          this.$toast.open({
            message: `A meta foi atualizada com sucesso`,
            type: "success",
          });
        })
        .catch(() => {
          this.$toast.open({
            message: `Houve ao tentar atualizar a sua meta`,
            type: "error",
          });
        });
    },
    _adicionarMeta() {
      this.$store
        .dispatch("metas/novaMeta", {
          pessoaId: this.pessoa.id,
          meta: this.meta,
        })
        .then(() => {
          this.$toast.open({
            message: `Nova meta foi adicionada ao sistema com sucesso`,
            type: "success",
          });
        })
        .catch(() => {
          this.$toast.open({
            message: `Houve um problema ao tentar adicionar a nova menta no sistema`,
            type: "error",
          });
        });
    },
    handlerMeta() {
      if (this.configuracao.ehNovaMeta) {
        this._adicionarMeta();
        return;
      }
      this._atualizarMeta();
    },
  },
};
</script>

<style>
</style>