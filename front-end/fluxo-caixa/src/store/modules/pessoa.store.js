import httpRequest from "@/services/$httpRequest";
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
    },
    async recuperarPessoaPorId({ commit }, pessoaId) {
        return new Promise((resolve, reject) => {
            httpRequest.get(`Pessoa/${pessoaId}`)
                .then((resultado) => {
                    commit("atualizarPessoa", resultado);
                    resolve();
                })
                .catch(erro => {
                    reject(erro);
                })
        });
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