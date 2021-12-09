const state = {
    loadingAutenticacao: false,
    pessoa: {}

}

const actions = {
    atualizarPessoaState({ commit }, pessoa) {
        commit("atualizarPessoa", pessoa);
    },
    limparPessoaState({ commit }) {
        commit("limparPessoa");
    }
}

const mutations = {
    atualizarLoading(state, loading) {
        state.loadingAutenticacao = loading;
    },
    atualizarPessoa(state, pessoa) {
        state.pessoa = pessoa;
    },
    limparPessoa(state) {
        state.pessoa = {};
    }
}

export default {
    namespaced: true,
    state,
    actions,
    mutations
}