import { createApp, } from 'vue'
import VueApexCharts from "vue3-apexcharts";
import App from './App.vue'
import router from './router'
import store from './store'
import "animate.css";

//VueToast
import VueToast from 'vue-toast-notification';
import 'vue-toast-notification/dist/theme-sugar.css';

createApp(App)
    .use(store)
    .use(router)
    .use(VueApexCharts)
    .use(VueToast)
    .mount('#app')
