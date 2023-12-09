﻿using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.Answers.Commands;
using Application.EntityManagement.UserRoles.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.UserRoles.Handlers;

public class DeleteUserRoleByExternalIdCommandHandler(IRepository<UserRole> repository, ILogger logger)
    : IRequestHandler<DeleteUserRoleByExternalIdCommand, CommandResult>
{
    public virtual async Task<CommandResult> Handle(DeleteUserRoleByExternalIdCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByExternalIdAsync(request.ExternalId, cancellationToken);

        if (entity is null)
        {
            return CommandResult.Failure(MessageConstants.NotFound);
        }

        var deletedEntity = await repository.DeleteOneAsync(entity, cancellationToken);

        if (deletedEntity is not null)
        {
            return CommandResult.Success(MessageConstants.SuccessfullyDeleted);
        }

        logger.LogError(MessageConstants.EntityDeletionFailed, DateTime.UtcNow, typeof(UserRole), typeof(DeleteAnswerByExternalIdCommand));

        return CommandResult.Failure(MessageConstants.InternalServerError);
    }
}