import { createStore } from 'vuex'
import movimentacoes from "./modules/movimentacoes.store";
import pessoa from "./modules/pessoa.store";

const store = createStore();
store.registerModule("movimentacoes", movimentacoes);
store.registerModule("pessoa", pessoa);


export default store;