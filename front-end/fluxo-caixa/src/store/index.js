import { createStore } from 'vuex'
import fluxoDeCaixa from "./fluxoDeCaixa.store";
import pessoa from "./pessoa.store";

const store = createStore();
store.registerModule("fluxoDeCaixa", fluxoDeCaixa);
store.registerModule("pessoa", pessoa);


export default store;