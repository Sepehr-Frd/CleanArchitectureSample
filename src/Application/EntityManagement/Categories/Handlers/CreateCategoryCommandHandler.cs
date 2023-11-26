#region

using Application.Abstractions;
using Application.Common;
using Application.EntityManagement.Categories.Commands;
using Application.EntityManagement.Categories.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

#endregion

namespace Application.EntityManagement.Categories.Handlers;

public class CreateCategoryCommandHandler(IRepository<Category> categoryRepository, IMappingService mappingService, ILogger logger)
    : IRequestHandler<CreateCategoryCommand, CommandResult>
{
    public async Task<CommandResult> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = mappingService.Map<CategoryDto, Category>(request.CategoryDto);

        if (category is null)
        {
            logger.LogError(Messages.MappingFailed, DateTime.UtcNow, typeof(Category), typeof(CreateCategoryCommandHandler));

            return CommandResult.Failure(Messages.InternalServerError);
        }

        await categoryRepository.CreateAsync(category, cancellationToken);

        return CommandResult.Success(Messages.SuccessfullyCreated);
    }
}