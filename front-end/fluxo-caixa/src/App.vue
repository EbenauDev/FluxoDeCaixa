<template>
  <router-view />
</template>

<style lang="scss">
@import url("https://fonts.googleapis.com/css2?family=Noto+Sans+Display:wght@400;600;700&display=swap");
@import url("https://fonts.googleapis.com/css2?family=Poppins:wght@400;600&display=swap");
@import url("https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css");

:root {
  --main-color: #0074b7;
  --main-color-dark: #003b73;
  --main-color-light: #60a3d9;
}

html,
body {
  margin: 0;
  padding: 0;
  box-sizing: border-box;
}
body {
  height: 100vh;
  width: 100vw;
  display: grid;
  color: #fff;
  background: rgb(2, 0, 36);
  background: linear-gradient(
    302deg,
    rgba(2, 0, 36, 1) 0%,
    rgba(1, 29, 101, 1) 59%,
    rgba(4, 22, 67, 1) 100%
  );
  font-family: sans-serif;
}
</style>
