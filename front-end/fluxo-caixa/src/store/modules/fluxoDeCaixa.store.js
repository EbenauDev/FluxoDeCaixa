const state = {
    fluxoDeCaixa: []
}

const actions = {
    async recuperarCaixaMaisRecente({ commit }) {
        commit('atualizarFluxoDeCaixa', 120);
    }
}

const mutations = {
    atualizarFluxoDeCaixa(state, payload) {
        state.fluxoDeCaixa = payload;
    }
}

export default {
    namespaced: true,
    state,
    actions,
    mutations
}