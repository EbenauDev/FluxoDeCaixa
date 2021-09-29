using ControleDeCaixa.WebAPI.InputModel;
using System.Threading.Tasks;
using ControleDeCaixa.WebAPI.Entites;
using ControleDeCaixa.WebAPI.Generics;
using ControleDeCaixa.WebAPI.Repositorio;

namespace ControleDeCaixa.WebAPI.Handler
{
    public interface IOperacaoCaixaHandler
    {
        Task<Resultado<bool, Falha>> IncluirOperacaoCaixaAsync(OperacaoCaixaInputModel operacaoCaixaInput);
    }
    public class OperacaoCaixaHandler : IOperacaoCaixaHandler
    {
        private readonly IFluxoDeCaixaRepositorio _fluxoDeCaixaRepositorio;
        public OperacaoCaixaHandler(IFluxoDeCaixaRepositorio fluxoDeCaixaRepositorio)
        {
            _fluxoDeCaixaRepositorio = fluxoDeCaixaRepositorio;
        }

        public async Task<Resultado<bool, Falha>> IncluirOperacaoCaixaAsync(OperacaoCaixaInputModel operacaoCaixaInput)
        {
            var operacaoCaixaValidator = new OperacaoCaixaValidator();
            if (operacaoCaixaValidator.Validate(operacaoCaixaInput) is var resultadoValidator && resultadoValidator.IsValid == false)
                return Resultado<bool, Falha>.NovaFalha(Falha.Nova(400, "input model está inválida"));
            var operacaoCaixa = OperacaoCaixa.Nova(
                 operacaoCaixaInput.CaixaId,
                 operacaoCaixaInput.Operacao,
                 operacaoCaixaInput.Descricao,
                 operacaoCaixaInput.Valor);

            if (await _fluxoDeCaixaRepositorio.IncluirOperacaoCaixaAsync(operacaoCaixa) is var resultado && resultado.EhFalha)
                return Resultado<bool, Falha>.NovaFalha(resultado.Falha);
            return Resultado<bool, Falha>.NovoSucesso(true);
        }
    }
}
