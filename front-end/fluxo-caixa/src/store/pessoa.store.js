const state = {
    loadingAutenticacao: false,
    pessoa: {}

}

const actions = {
    async login({ commit }) {
        commit('atualizarLoading', true);
        setTimeout(() => {
            commit('atualizarLoading', false);
        }, 800)
    }
}

const mutations = {
    atualizarLoading(state, loading) {
        state.loadingAutenticacao = loading;
    }
}

export default {
    namespaced: true,
    state,
    actions,
    mutations
}