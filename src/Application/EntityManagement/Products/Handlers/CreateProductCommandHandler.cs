using Application.Abstractions;
using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.Products.Commands;
using Application.EntityManagement.Products.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Products.Handlers;

public class CreateProductCommandHandler(
        IRepository<Product> repository,
        IMappingService mappingService,
        ILogger logger)
    : IRequestHandler<CreateProductCommand, CommandResult>
{
    public async Task<CommandResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = mappingService.Map<CreateProductDto, Product>(request.CreateProductDto);

        if (entity is null)
        {
            logger.LogError(message: MessageConstants.MappingFailed, DateTime.UtcNow, typeof(Product), typeof(CreateProductCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        var createdEntity = await repository.CreateAsync(entity, cancellationToken);

        if (createdEntity is not null)
        {
            return CommandResult.Success(MessageConstants.SuccessfullyCreated);
        }

        logger.LogError(message: MessageConstants.EntityCreationFailed, DateTime.UtcNow, typeof(Product), typeof(CreateProductCommandHandler));

        return CommandResult.Failure(MessageConstants.InternalServerError);
    }
}