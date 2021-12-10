const state = {
    anoDeMovimentacao: [],
    mesDeMovimentacao: {},
    metas: {}
}

const actions = {
    async adicionarMesDeMovimentacao({ commit }, payload) {
        setTimeout(() => {
            commit("atualizarAnoDeMovimentacoes", payload);
        }, 1000)
    },
    async recuperarCaixaMaisRecente({ commit }) {
        commit('atualizarFluxoDeCaixa', 120);
    }
}

const mutations = {
    atualizarAnoDeMovimentacoes(state, anoDeMovimentacao) {
        state.anoDeMovimentacao = anoDeMovimentacao;
    },
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