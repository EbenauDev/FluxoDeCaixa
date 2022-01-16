<template>
  <div>
    <div class="card">
      <div class="card__header">
        <div>
          <p class="m-0 card__title">Metas</p>
        </div>
        <div class="card-header__actions">
          <button class="btn btn-outline" @click="novoObjetivo()">
            Novo objetivo
          </button>
        </div>
      </div>
      <div class="card__body">
        <div class="card-meta" v-for="meta in metas" :key="meta.id">
          <div class="__cabecalho">
            <div>
              <h4 class="main-color-dark">{{ meta.descricao }}</h4>
            </div>
            <div class="__acoes">
              <button
                class="btn btn-small m-r-5"
                type="button"
                @click="removerMeta(meta)"
              >
                <i class="far fa-trash-alt"></i>
              </button>
              <button
                class="btn btn-small btn-primary"
                @click="editarMeta(meta)"
                type="button"
              >
                <i class="fas fa-pen"></i>
              </button>
            </div>
          </div>
          <div class="__corpo">
            <div class="m-b-5">
              <p class="f-13">Valor desejado</p>
              <p class="f-bold">{{ Number(meta.valorDesejado).toReal() }}</p>
            </div>
            <div>
              <p class="f-13">Valor que falta</p>
              <p class="f-bold">{{ Number(meta.valorRestante).toReal() }}</p>
            </div>
          </div>
          <div class="__progresso">
            <div class="__status" :style="{ width: meta.progressoMeta + '%' }">
              <span> {{ meta.progressoMeta }}% </span>
              <span>
                <i class="far fa-flag"></i>
              </span>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div v-if="abrirModalFormulario">
      <modal>
        <formulario-meta
          :on-close="fecharModal"
          :configuracao="configuracao"
        ></formulario-meta>
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
      pessoa: (state) => state.pessoa.pessoa,
    }),
  },

  data() {
    return {
      abrirModalFormulario: false,
      configuracao: {
        pessoaId: 0,
        ehNovaMeta: false,
        metaParaAtualizar: {},
      },
    };
  },
  mounted() {
    this.recuperarMetas();
  },
  methods: {
    async recuperarMetas() {
      try {
        await this.$store.dispatch("metas/recuperarMetas", this.pessoa.id);
      } catch (error) {
        this.$toast.open({
          message: `Houve um problema recuperar as suas metas`,
          type: "error",
        });
      }
    },
    novoObjetivo() {
      this.abrirModalFormulario = true;
      this.configuracao = {
        pessoaId: this.pessoa.id,
        ehNovaMeta: true,
        metaParaAtualizar: {},
      };
    },
    editarMeta(meta) {
      this.configuracao = {
        pessoaId: this.pessoa.id,
        ehNovaMeta: false,
        metaParaAtualizar: meta,
      };
      this.abrirModalFormulario = true;
    },
    removerMeta({ id }) {
      this.$store
        .dispatch("metas/removerMeta", {
          pessoaId: this.pessoa.id,
          metaId: id,
        })
        .then(() => {
          this.$toast.open({
            message: `Meta foi removida com sucesso`,
            type: "success",
          });
        })
        .catch(() => {
          this.$toast.open({
            message: `Houve um problema ao remover a meta do sistema. Tente novamente mais tarde`,
            type: "error",
          });
        });
    },
    fecharModal() {
      this.abrirModalFormulario = false;
    },
  },
};
</script>

<style lang="scss" scoped>
.card-meta {
  padding: 20px 18px;
  border-radius: 10px;
  box-shadow: 0 3px 6px #e3e3e3;
  margin-bottom: 15px;
  .__cabecalho {
    display: grid;
    grid-template-columns: 1fr 120px;
    margin-bottom: 10px;
    .__acoes {
      display: flex;
      justify-content: center;
      button {
        height: 30px;
      }
    }
  }
  .__corpo {
    margin-bottom: 15px;
  }
  .__progresso {
    width: 100%;
    display: flex;
    background: #f5f5f5;
    max-height: 30px;
    border-radius: 20px;
    .__status {
      display: grid;
      grid-template-columns: 1fr 30px;
      min-height: 20px;
      align-items: center;
      padding: 5px 16px;
      border-radius: 20px;
      background: #12bf12;
      color: #fff;
      font-size: 13px;
    }
  }

  p,
  h3,
  h4 {
    margin: 4px 0px;
    padding: 0;
  }
}
</style>