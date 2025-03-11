using FluentValidation;

namespace Products.Application.Models;

public class PagedRequestInputModelValidator : AbstractValidator<PagedRequestInputModel>
{
    public PagedRequestInputModelValidator()
    {
        RuleFor(p => p.PageIndex)
            .NotEmpty()
            .WithMessage("É obrigatório informar a página.")
            .GreaterThanOrEqualTo(1)
            .WithMessage("Uma página válida deve ser informada.");

        RuleFor(p => p.PageSize)
            .NotEmpty()
            .WithMessage("É obrigatório informar a quantidade de registros por página.")
            .GreaterThanOrEqualTo(1)
            .WithMessage("Um valor válido deve ser informado para a quantidade de registros por página.");
    }
}