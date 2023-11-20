﻿using Application.Abstractions;
using Application.Common;
using Application.EntityManagement.Roles.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Roles.Handlers;

public class UpdateRoleCommandHandler(
        IRepository<Role> repository,
        IMappingService mappingService,
        ILogger logger)
    : IRequestHandler<UpdateRoleCommand, CommandResult>
{
    public virtual async Task<CommandResult> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByExternalIdAsync(request.ExternalId, cancellationToken);

        if (entity is null)
        {
            return CommandResult.Success(Messages.NotFound);
        }

        var newEntity = mappingService.Map(request.RoleDto, entity);

        if (newEntity is null)
        {
            logger.LogError(message: Messages.MappingFailed, DateTime.UtcNow, typeof(Role), typeof(UpdateRoleCommandHandler));

            return CommandResult.Failure(Messages.InternalServerError);
        }

        var updatedEntity = await repository.UpdateAsync(newEntity, cancellationToken);

        if (updatedEntity is not null)
        {
            return CommandResult.Success(Messages.SuccessfullyUpdated);
        }

        logger.LogError(message: Messages.EntityUpdateFailed, DateTime.UtcNow, typeof(Role), typeof(UpdateRoleCommandHandler));

        return CommandResult.Failure(Messages.InternalServerError);
    }
}