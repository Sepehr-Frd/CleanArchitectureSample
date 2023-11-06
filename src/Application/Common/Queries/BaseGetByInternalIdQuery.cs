using Domain.Common;
using MediatR;
using System.Linq.Expressions;

namespace Application.Common.Queries;

public abstract record BaseGetByInternalIdQuery<TEntity>
(
    Guid InternalId,
    Expression<Func<TEntity, object?>>[] RelationsToInclude
) : IRequest<QueryReferenceResponse<TEntity>>
    where TEntity : BaseEntity;