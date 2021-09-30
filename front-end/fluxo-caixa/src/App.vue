<template>
  <router-view />
</template>

<style lang="scss">
@import url("https://fonts.googleapis.com/css2?family=Noto+Sans+Display:wght@400;600;700&display=swap");
@import "./scss/globals.scss";
:root {
  --cor-principal1: #d6e4f5;
  --cor-principal2: #83afe2;
  --cor-principal3: #3079cf;
  --cor-principal4: #1d497c;
  --cor-principal5: #0a1829;
  --cor-secundaria1: #f8d3d5;
  --cor-secundaria2: #eb7a81;
  --cor-secundaria3: #de212e;
  --cor-secundaria4: #85141b;
  --cor-secundaria5: #2c0709;
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
