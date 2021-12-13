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
        try {
            var resultado = await httpRequest.get(`Pessoa/${pessoaId}`);
            commit("atualizarPessoa", resultado);
        } catch (error) {
            this.$toast.open({
                message:
                    "Houve um problema ao tentar carregar o seu cadastro",
                type: "error",
            });
        }
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