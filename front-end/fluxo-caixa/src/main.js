import { createApp, } from 'vue'
import VueApexCharts from "vue3-apexcharts";
import App from './App.vue'
import router from './router'
import store from './store'
import "animate.css";

createApp(App)
    .use(store)
    .use(router)
    .use(VueApexCharts)
    .mount('#app')
