const state = {
    loadingAutenticacao: false,
    pessoa: {
        "id": 1,
        "nome": "Joa Tiago",
        "dataNascimento": "2000-06-24T00:00:00.000",
        "avatar": "http://pm1.narvii.com/7378/5623dfb9533b07c62ece9048a7ba002e8a016263r1-640-640v2_00.jpg",
        "username": "joao.ofical",
        "email": "ebenau06@gmail.com"
    }

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