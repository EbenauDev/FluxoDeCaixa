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
    async recuperarAnosDeMovimentacoes({ commit }, pessoaId) {
        try {
            var resultado = await httpRequest.get(`Movimentacoes/Pessoa/${pessoaId}/ListarAnosDeMovimentacoes`);
            commit("atualizarAnoDeMovimentacoes", resultado);
        } catch (error) {
            console.error(error);
        }
    },
    async recuperarMesesDeMovimentacoes({ commit }, { pessoaId, anoId }) {
        try {
            var resultado = await httpRequest.get(`Movimentacoes/Pessoa/${pessoaId}/Ano/${anoId}/MesDeMovimentacoes`);
            commit("atualizarMesesDeMovimentacoes", resultado);
        } catch (error) {
            console.error(error);
        }
    },
    async recuperarMovimentacoesDoMes({ commit }, { pessoaId, anoId, mesId }) {
        try {
            var resultado = await httpRequest.get(`Movimentacoes/Pessoa/${pessoaId}/Ano/${anoId}/Mes/${mesId}/Movimentacoes`);
            commit("atualizarMovimentacoesDoMes", resultado);
        } catch (error) {
            console.error(error);
        }
    },
    async removerOperacaoDoMes({ commit }, { mesId, operacaoMesId }) {
        try {
            await httpRequest.delete(`Movimentacoes/Mes/${mesId}/Movimentacao/${operacaoMesId}`);
            commit("removerOperacaoDoMes", operacaoMesId);
        } catch (error) {
            console.error(error);
        }
    },
    async adicionarOperacaoDoMes(_, { pessoaId, operacao }) {
        try {
            await httpRequest.post(`Movimentacoes/Pessoa/${pessoaId}/OperacaoMes`, operacao);
        } catch (error) {
            console.error(error);
        }
    },
    async atualizarOperacaoDoMes(_, { pessoaId, operacao }) {
        try {
            await httpRequest.put(`Movimentacoes/Pessoa/${pessoaId}/OperacaoMes/${operacao.id}`, operacao);
        } catch (error) {
            console.log(error);
        }
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