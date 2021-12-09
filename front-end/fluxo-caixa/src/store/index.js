import { createStore } from 'vuex'
import fluxoDeCaixa from "./modules/fluxoDeCaixa.store";
import pessoa from "./modules/pessoa.store";

const store = createStore();
store.registerModule("fluxoDeCaixa", fluxoDeCaixa);
store.registerModule("pessoa", pessoa);


export default store;