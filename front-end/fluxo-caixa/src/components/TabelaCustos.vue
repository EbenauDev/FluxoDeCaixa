<template>
  <div>
    <div class="container">
      <h3>Tabela Custos</h3>
      <p>Todos os seus custos estão listados na tabela abaixo</p>
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

    <modal>
      <div>
        <div>
          <h4>Adicionar um novo custo</h4>
        </div>
        <form class="form" name="novoCusto">
          <div class="form-group">
            <label for="descricao">Descrição</label>
            <input id="descricao" type="text" />
          </div>
          <div>
            <label for="valor">Valor</label>
            <input id="valor" type="text" />
          </div>
          <div>
            <button class="btn-cancel" type="button">Cancelar</button>
            <button class="btn-primary" type="button">Salvar</button>
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
};
</script>

<style lang="scss" scoped>
</style>

