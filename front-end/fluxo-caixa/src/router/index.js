import { createRouter, createWebHistory } from 'vue-router';

const routes = [
  {
    path: '/',
    name: 'NaoAutenticado',
    component: () => import(/* webpackChunkName: "NaoAutenticado" */'../views/NaoAutenticado.vue'),
  },
  {
    path: '/autenticado',
    name: 'Autenticado',
    component: () => import(/* webpackChunkName: "Autenticado" */'../views/Autenticado.vue'),
    children: [
      {
        path: "configuracoes-da-conta",
        name: "Autenticado.ConfiguracoesDaConta",
        component: () => import(/* webpackChunkName: "ConfiguracoesDaConta" */'../views/ConfiguracoesDaConta.vue'),
      },
      {
        path: "visao-geral",
        name: "Autenticado.VisaoGeral",
        component: () => import(/* webpackChunkName: "ConfiguracoesDaConta" */'../views/VisaoGeral.vue')
      },
    ]
  },
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
});

export default router
