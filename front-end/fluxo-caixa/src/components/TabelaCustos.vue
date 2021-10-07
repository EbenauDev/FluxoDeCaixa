<template>
  <div>
    <div class="container">
      <header class="container-header">
        <div>
          <h2>Tabela Custos</h2>
          <p>Todos os seus custos estão listados na tabela abaixo</p>
        </div>
        <div>
          <button
            class="btn btn-primary upper f-bold"
            type="button"
            @click="novoCusto"
          >
            Custo
          </button>
        </div>
      </header>

      <div class="tabela">
        <div class="tabela__cabecalho background-primary">
          <div>#</div>
          <div>Descrição</div>
          <div>Valor</div>
        </div>
        <div class="tabela__corpo">
          <div>1</div>
          <div>Placa de vídeo</div>
          <div>R$ 4.999,00</div>
        </div>
      </div>
    </div>

    <modal v-if="adicionarNovoCusto">
      <div class="w-100 p-20">
        <header>
          <h2 class="main-color-dark">Adicionar um novo custo</h2>
          <p>Utilize o formulário abaixo para adicionar um novo custo</p>
        </header>
        <form class="form" name="novoCusto">
          <div class="form-group">
            <label class="form-label" for="descricao">Descrição</label>
            <input class="form-control" id="descricao" type="text" />
          </div>
          <div class="form-group">
            <label class="form-label" for="valor">Valor</label>
            <input class="form-control" id="valor" type="text" />
          </div>
          <div class="form-footer">
            <button class="btn btn-cancel" type="button" @click="closeModal">
              Cancelar
            </button>
            <button
              class="btn btn-primary f-bold upper"
              type="button"
              @click="salvarNovoCusto"
            >
              Salvar
            </button>
          </div>
        </form>
      </div>
    </modal>
  </div>
</template>

<script>
import Modal from "./Modal.vue";
export default {
  name: "TabelaCustos",
  components: { Modal },
  data() {
    return {
      adicionarNovoCusto: false,
    };
  },
  methods: {
    closeModal() {
      this.adicionarNovoCusto = false;
    },
    novoCusto() {
      this.adicionarNovoCusto = true;
    },
    salvarNovoCusto() {
      this.adicionarNovoCusto = false;
    },
  },
};
</script>

<style lang="scss" scoped>
</style>

