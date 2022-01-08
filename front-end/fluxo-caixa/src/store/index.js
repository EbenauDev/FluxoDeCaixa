import { createStore } from 'vuex'
import movimentacoes from "./modules/movimentacoes.store";
import pessoa from "./modules/pessoa.store";
import metas from "./modules/metas.store";

const store = createStore();
store.registerModule("movimentacoes", movimentacoes);
store.registerModule("pessoa", pessoa);
store.registerModule("metas", metas);


export default store;