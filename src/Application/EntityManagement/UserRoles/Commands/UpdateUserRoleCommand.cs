﻿#region

using Application.Common;
using Application.EntityManagement.UserRoles.Dtos;
using MediatR;

#endregion

namespace Application.EntityManagement.UserRoles.Commands;

public record UpdateUserRoleCommand(int ExternalId, UserRoleDto UserRoleDto) : IRequest<CommandResult>;