<template>
  <div class="header-personalizado">
    <div class="__profile">
      <div class="__avatar">
        <img src="@/assets/avatar.png" alt="" />
      </div>
      <div class="__nickname">
        <p>Jhon doe</p>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "Header",
};
</script>

<style lang="scss">
.header-personalizado {
  max-width: 100%;
  width: 100%;
  display: flex;
  max-height: 105px;
  background: var(--main-color);
  border-bottom: 5px solid var(--main-color-dark);
  .__profile {
    display: flex;
    align-items: center;
    margin-left: 4rem;
    width: 100%;
    .__avatar {
      max-width: 45px;
      max-height: 45px;
      width: 100%;
      height: 100%;
      border-radius: 50%;
      border: 2px solid var(--main-color-dark);
      overflow: hidden;
      margin-right: 10px;
      img {
        width: 100%;
        height: 100%;
        object-fit: cover;
      }
    }
    .__nickname {
      font-size: 21px;
      font-weight: 600;
    }
  }
}
</style>