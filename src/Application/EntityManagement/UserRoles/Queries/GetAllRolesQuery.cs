﻿using Application.Common;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace Application.EntityManagement.UserRoles.Queries;

public record GetAllUserRolesQuery(
        Pagination Pagination,
        Expression<Func<UserRole, object?>>[]? RelationsToInclude = null,
        Expression<Func<UserRole, bool>>? Filter = null)
    : IRequest<QueryReferenceResponse<IEnumerable<UserRole>>>;