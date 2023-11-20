using Application.Common;
using Application.EntityManagement.Answers.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Answers.Handlers;

public class DeleteAnswerByInternalIdCommandHandler(IRepository<Answer> repository, ILogger logger)
    : IRequestHandler<DeleteAnswerByInternalIdCommand, CommandResult>
{
    public virtual async Task<CommandResult> Handle(DeleteAnswerByInternalIdCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByInternalIdAsync(request.InternalId, cancellationToken);

        if (entity is null)
        {
            return CommandResult.Failure(Messages.NotFound);
        }

        var deletedEntity = await repository.DeleteAsync(entity, cancellationToken);

        if (deletedEntity is not null)
        {
            return CommandResult.Success(Messages.SuccessfullyDeleted);
        }

        logger.LogError(Messages.EntityDeletionFailed, DateTime.UtcNow, typeof(Answer), typeof(DeleteAnswerByInternalIdCommandHandler));

        return CommandResult.Failure(Messages.InternalServerError);
    }
}