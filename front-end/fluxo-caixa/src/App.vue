<template>
  <router-view />
</template>

<style lang="scss">
@import url("https://fonts.googleapis.com/css2?family=Noto+Sans+Display:wght@400;600;700&display=swap");

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
  font-family: "Noto Sans Display", sans-serif;
}
</style>
