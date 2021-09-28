using FluentValidation;

namespace ControleDeCaixa.WebAPI.InputModel
{
    public class OperacaoCaixaInputModel
    {
        public int CaixaId { get; set; }
        public string Operacao { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
    }

    public class OperacaoCaixaValidator : AbstractValidator<OperacaoCaixaInputModel>
    {
        public OperacaoCaixaValidator()
        {
            RuleFor(op => op.CaixaId).NotEmpty().NotEqual(0).WithMessage("Caixa Id não deve ser nulo!");
            RuleFor(op => op.Operacao).NotEmpty().WithMessage("A operação de caixa não pode ser vazia!");
        }
    }
}
