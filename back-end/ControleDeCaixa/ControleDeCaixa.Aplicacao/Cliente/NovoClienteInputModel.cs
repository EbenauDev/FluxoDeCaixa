using FluentValidation;
using System;

namespace ControleDeCaixa.Aplicacao.Cliente
{
    public class NovoClienteInputModel
    {
        public string Nome { get; set; }
        public DateTime Nascimento { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }

    }

    public class NovoClienteInputModelValidator : AbstractValidator<NovoClienteInputModel>
    {
        public NovoClienteInputModelValidator()
        {
            RuleFor(model => model.Nome).NotEmpty().WithMessage("O Cliente deve possuir um nome");
            RuleFor(model => model.Nascimento).NotNull().WithMessage("A data de nascimento/fundação deve ser informada");
            RuleFor(model => model.Usuario).NotNull().WithMessage("O usuário deve ser informado");
            RuleFor(model => model.Usuario).MinimumLength(3).WithMessage("Número de caracteres para o usuário deve ser maior que três");
            RuleFor(model => model.Senha).MinimumLength(6).WithMessage("A senha deve ter no mínimo 6 caracteres");
        }
    }
}
