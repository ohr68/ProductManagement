using FluentValidation;

namespace Products.Application.Products.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .WithMessage("É obrigatório informar o nome do produto.")
            .MaximumLength(100)
            .WithMessage("O tamanho máximo permitido para o nome do produto é de 100 caracteres.");
        
        RuleFor(p => p.Description)
            .NotEmpty()
            .WithMessage("É obrigatório informar a descrição do produto.")
            .MaximumLength(300)
            .WithMessage("O tamanho máximo permitido para a descrição do produto é de 300 caracteres.");

        RuleFor(p => p.Price)
            .NotEmpty()
            .WithMessage("É obrigatório informar o preço do produto.");
    }
}