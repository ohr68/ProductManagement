using FluentValidation;
using Mapster;
using MediatR;
using ProductManagement.Common.Exceptions;
using ProductManagement.Common.Messaging.Interfaces;
using Products.Domain.Entities;
using Products.Domain.Interfaces;
using Products.Domain.Messaging.Products;
using Serilog;

namespace Products.Application.Products.CreateProduct;

public class CreateProductCommandHandler(
    IRepository<Product> productRepository,
    IQueueService queueService,
    IValidator<CreateProductCommand> validator) : IRequestHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var product = request.Adapt<Product>();

        var success = await productRepository.CreateAsync(product, cancellationToken);

        if (!success)
            throw new BadRequestException("Houve uma falha ao cadastrar o produto.");

        Log.Information("Enviando produto cadastrado para fila.");
        await queueService.Publish(product.Adapt<ProductCreated>(), cancellationToken);
        Log.Information("Produto enviado para fila com sucesso.");

        return product.Adapt<CreateProductResult>();
    }
}