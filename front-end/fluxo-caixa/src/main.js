import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import "@/services/stringPrototypes"
import "animate.css";
//VueToast
import VueToast from 'vue-toast-notification';
import 'vue-toast-notification/dist/theme-sugar.css';

const app = createApp(App);


app.directive('formatDate', (el, binding) => {
    const _formaters = {
        "DD/MM/YYYY": (event) => {
            const { target } = event;
            target.maxLength = 10;
            target.minLength = 10;
            const pattern = new RegExp("[0-9]{2}/[0-9]{2}/[0-9]{4}");
            if (target.value.length == 2)
                target.value += "/";
            if (target.value.length == 5)
                target.value += "/";

            if (target.value.length == 10 && (pattern.test(target.value) == false)) {
                target.setCustomValidity("Data inválida.");
                target.reportValidity();
            }
            target.setCustomValidity("");

        }
    }
    const handler = (event) => {
        setTimeout(() => {
            _formaters[binding.value.format](event);
        }, 200);
    }
    el.addEventListener("input", handler)
});

app.use(VueToast);
app.use(store);
app.use(router);
app.mount('#app');

export default app;

