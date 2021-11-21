<template>
  <div>
    <div class="card">
      <p class="cursor-pointer">
        Mes corrente: <strong class="color-primary">Novembro</strong> | Ano:
        <strong class="color-primary">2021</strong>
      </p>
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
    <div v-if="false">
      <modal></modal>
    </div>
  </div>
</template>


<script>
import Modal from "../components/Modal.vue";
export default {
  name: "VisaoGeral",
  components: {
    Modal,
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