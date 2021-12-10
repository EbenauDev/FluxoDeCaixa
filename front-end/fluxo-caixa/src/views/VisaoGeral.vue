<template>
  <div>
    <div class="card card">
      <div class="card__header">
        <div>
          <p class="m-0 card__title">Ano</p>
        </div>
        <div class="card-header__actions">
          <button
            class="btn btn-outline"
            @click="devoAdicionarNovoAno = !devoAdicionarNovoAno"
          >
            Novo ano
          </button>
        </div>
      </div>
    </div>
    <div class="visao-geral">
      <div>
        <div class="card">
          <div class="card__header">
            <div>
              <p class="m-0 card__title">Receitas</p>
            </div>
            <div class="card-header__actions">
              <button class="btn btn-outline">Nova receita</button>
            </div>
          </div>
        </div>
        <div class="card">
          <div class="card__header">
            <div>
              <p class="m-0 card__title">Custos</p>
            </div>
            <div class="card-header__actions">
              <button class="btn btn-outline">Novo custo</button>
            </div>
          </div>
        </div>
      </div>
      <div>
        <div class="card">
          <div class="card__header">
            <div>
              <p class="m-0 card__title">Novembro de 2021</p>
            </div>
            <div class="card-header__actions">
              <button class="btn btn-outline">Historico</button>
            </div>
          </div>
        </div>
        <div class="card">
          <div class="card__header">
            <div>
              <p class="m-0 card__title">Progresso 2021</p>
            </div>
            <div class="card-header__actions">
              <button class="btn btn-outline">Novo objetivo</button>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div v-if="devoAdicionarNovoAno">
      <modal>
        <novo-ano-de-movimentacoes></novo-ano-de-movimentacoes>
      </modal>
    </div>
  </div>
</template>


<script>
import Modal from "../components/Modal.vue";
import NovoAnoDeMovimentacoes from "./components/NovoAnoDeMovimentacoes.vue";
export default {
  name: "VisaoGeral",
  components: {
    Modal,
    NovoAnoDeMovimentacoes,
  },
  data() {
    return {
      devoAdicionarNovoAno: false,
    };
  },
};
</script>

<style lang="scss" scoped>
.card {
  padding: 10px 15px;
  border-radius: 10px;
  background-color: #fff;
  color: #333;
  margin: 2rem 1rem;
  .card__header {
    display: grid;
    grid-template-columns: 1fr 160px;
    align-items: center;
    .card__title {
      font-size: 18px;
      font-weight: bold;
      color: var(--main-color-dark);
      position: relative;
      &::before {
        content: "";
        width: 35px;
        height: 6px;
        background-color: var(--main-color-dark);
        border-radius: 6px;
        position: absolute;
        top: -10px;
      }
    }
    .card-header__actions {
      justify-self: flex-end;
    }
  }
}
.visao-geral {
  display: grid;
  grid-template-columns: 1.2fr 0.8fr;
}
</style>