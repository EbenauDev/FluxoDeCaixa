<template>
  <div class="custom-modal">
    <div class="modal-backdrop">
      <div class="modal-content modal-sm">
        <slot></slot>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "Modal",
  props: {
    onClose: {
      type: Function,
      required: false,
    },
  },
};
</script>

<style lang="scss">
.custom-modal {
  position: absolute;
  left: 0;
  top: 0;
  z-index: 999;
  .modal-backdrop {
    background-color: #000000ba;
    width: 100vw;
    height: 100vh;
    display: flex;
    align-items: center;
    justify-content: center;
    animation: fadeIn;
    animation-duration: 0.5s;

    .modal-content {
      max-width: 720px;
      max-height: 600px;
      width: 100%;
      background: #fff;
      display: flex;
      border-radius: 16px;
      padding: 10px;
      animation: slideInDown;
      animation-duration: 0.5s;
    }
    .modal-sm {
      max-width: 420px;
      max-height: 430px;
      min-height: 140px;
    }
  }
}
</style>