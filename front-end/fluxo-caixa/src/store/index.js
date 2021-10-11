import { createStore } from 'vuex'
import fluxoDeCaixa from "./fluxoDeCaixa.store";

const store = createStore();
store.registerModule("fluxoDeCaixa", fluxoDeCaixa);


export default store;