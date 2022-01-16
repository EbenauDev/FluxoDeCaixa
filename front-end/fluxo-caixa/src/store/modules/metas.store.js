import httpRequest from "@/services/$httpRequest";
const state = {
    metas: [],
}

const actions = {
    recuperarMetas({ commit }, pessoaId) {
        return new Promise((resolve, reject) => {
            httpRequest.get(`Metas/Resumo?pessoaId=${pessoaId}`)
                .then(_metas => {
                    commit("atualizarMetas", _metas);
                    resolve()
                })
                .catch(erro => {
                    reject(erro);
                });
        });
    },
    removerMeta({ dispatch }, { pessoaId, metaId }) {
        return new Promise((resolve, reject) => {
            httpRequest.delete(`Metas/Remover?pessoaId=${pessoaId}&metaId=${metaId}`)
                .then(() => {
                    dispatch("recuperarMetas", pessoaId);
                    resolve()
                })
                .catch(erro => {
                    reject(erro);
                });
        });
    }
}

const mutations = {
    atualizarMetas(state, metas) {
        state.metas = metas;
    }
}

export default {
    namespaced: true,
    state,
    actions,
    mutations
}