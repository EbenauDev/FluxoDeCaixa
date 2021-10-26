import { createRouter, createWebHistory } from 'vue-router';

const routes = [
  {
    path: '/',
    name: 'NaoAutenticado',
    component: () => import(/* webpackChunkName: "NaoAutenticado" */'../views/NaoAutenticado.vue'),
  },
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
});

export default router
