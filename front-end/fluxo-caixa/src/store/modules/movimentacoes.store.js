import httpRequest from "@/services/$httpRequest";

const state = {
    anosDeMovimentacoes: [],
    mesesDeMovimentacoes: [],
    movimentacoesDoMes: {
        receitas: [],
        despesas: []
    },
    mesAtivo: {},
    metas: {},
    filtroAtivo: {
        pessoaId: null,
        anoId: null,
        mesId: null,
    }
}


const getters = {
    possuiMesDeMovimentacoes: state => {
        if (state.movimentacoesDoMes && !state.movimentacoesDoMes.id) {
            return false;
        }
        return true;
    },
    filtroAtivo: state => {
        return state.filtroAtivo;
    }
}

const actions = {
    recuperarAnosDeMovimentacoes({ commit }, pessoaId) {
        return new Promise((resolve, reject) => {
            httpRequest.get(`Movimentacoes/Pessoa/${pessoaId}/ListarAnosDeMovimentacoes`)
                .then(resultado => {
                    commit("atualizarAnoDeMovimentacoes", resultado);
                    resolve();
                })
                .catch(erro => {
                    reject(erro);
                })
        });
    },
    recuperarMesesDeMovimentacoes({ commit }, { pessoaId, anoId }) {
        return new Promise((resolve, reject) => {
            httpRequest.get(`Movimentacoes/Pessoa/${pessoaId}/Ano/${anoId}/MesDeMovimentacoes`)
                .then(resultado => {
                    commit("atualizarMesesDeMovimentacoes", resultado);
                    resolve();
                })
                .catch(erro => {
                    reject(erro);
                })
        });
    },
    recuperarMovimentacoesDoMes({ commit }, { pessoaId, anoId, mesId }) {
        return new Promise((resolve, reject) => {
            httpRequest.get(`Movimentacoes/Pessoa/${pessoaId}/Ano/${anoId}/Mes/${mesId}/Movimentacoes`).then(resultado => {
                commit("atualizarMovimentacoesDoMes", resultado);
                resolve();
            }).catch(erro => {
                reject(erro);
            })
        })
    },
    removerOperacaoDoMes({ commit }, { mesId, operacaoMesId }) {
        return new Promise((resolve, reject) => {
            httpRequest.delete(`Movimentacoes/Mes/${mesId}/Movimentacao/${operacaoMesId}`).then(() => {
                commit("removerOperacaoDoMes", operacaoMesId);
                resolve()
            }).catch(erro => {
                reject(erro);
            })
        })
    },
    adicionarOperacaoDoMes(_, { pessoaId, operacao }) {
        return new Promise((resolve, reject) => {
            httpRequest.post(`Movimentacoes/Pessoa/${pessoaId}/OperacaoMes`, operacao)
                .then(resolve)
                .catch(reject);
        })
    },
    atualizarOperacaoDoMes(_, { pessoaId, operacao }) {
        return new Promise((resolve, reject) => {
            httpRequest.put(`Movimentacoes/Pessoa/${pessoaId}/OperacaoMes/${operacao.id}`, operacao)
                .then(resolve)
                .catch(reject);
        })
    },
}
const mutations = {
    atualizarAnoDeMovimentacoes(state, anosDeMovimentacoes) {
        state.anosDeMovimentacoes = anosDeMovimentacoes;
    },
    atualizarMesesDeMovimentacoes(state, mesesDeMovimentacao) {
        state.mesesDeMovimentacoes = mesesDeMovimentacao;
    },
    atualizarMovimentacoesDoMes(state, movimentacoesDoMes) {
        state.movimentacoesDoMes = movimentacoesDoMes;
    },
    removerOperacaoDoMes(state, operacaoMesId) {
        state.movimentacoesDoMes.receitas = state.movimentacoesDoMes.receitas.filter(r => r.id != operacaoMesId);
        state.movimentacoesDoMes.despesas = state.movimentacoesDoMes.despesas.filter(d => d.id != operacaoMesId);
    },
    atualizarFiltroAtivo(state, filtro) {
        state.filtroAtivo = filtro;
    }
}

export default {
    namespaced: true,
    state,
    getters,
    actions,
    mutations
}