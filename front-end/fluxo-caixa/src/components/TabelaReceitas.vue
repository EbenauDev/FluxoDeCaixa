<template>
  <div class="container">
    <header class="container-header">
      <div>
        <h2>Receitas</h2>
        <p>Todos as suas fontes de receitas listadas na tabela abaixo</p>
      </div>
      <div>
        <button
          class="btn btn-primary upper f-bold"
          type="button"
          @click="novoReceita"
        >
          Receita
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
        <div>Salário</div>
        <div>R$ 2.254,00</div>
      </div>
    </div>
    <modal v-if="adicionarNovaReceita">
      <div class="w-100 p-20">
        <header>
          <h2 class="main-color-dark">Receita</h2>
          <p>Utilize o formulário abaixo para adicionar uma nova receita</p>
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
              @click="salvarNovaReceita"
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
  name: "TabelaReceitas",
  components: { Modal },
  data() {
    return {
      adicionarNovaReceita: false,
    };
  },
  methods: {
    closeModal() {
      this.adicionarNovaReceita = false;
    },
    novoReceita() {
      this.adicionarNovaReceita = true;
    },
    salvarNovaReceita() {
      this.adicionarNovaReceita = false;
    },
  },
};
</script>

<style lang="scss" scoped>
</style>