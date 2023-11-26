#region

using Application.Common;
using Domain.Common;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

#endregion

namespace Application.EntityManagement.Users.Queries;

public sealed record GetAllUsersQuery(
        Pagination Pagination,
        Expression<Func<User, bool>>? Filter = null)
    : IRequest<QueryReferenceResponse<IEnumerable<User>>>;