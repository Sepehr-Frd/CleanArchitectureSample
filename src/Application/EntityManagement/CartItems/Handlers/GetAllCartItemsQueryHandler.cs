#region

using Application.Common;
using Application.EntityManagement.CartItems.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using System.Net;

#endregion

namespace Application.EntityManagement.CartItems.Handlers;

public class GetAllCartItemsQueryHandler : IRequestHandler<GetAllCartItemsQuery, QueryReferenceResponse<IEnumerable<CartItem>>>
{
    private readonly IRepository<CartItem> _repository;

    public GetAllCartItemsQueryHandler(IRepository<CartItem> repository)
    {
        _repository = repository;
    }

    public virtual async Task<QueryReferenceResponse<IEnumerable<CartItem>>> Handle(GetAllCartItemsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(null, request.Pagination, cancellationToken);

        return new QueryReferenceResponse<IEnumerable<CartItem>>
            (
            entities,
            true,
            Messages.SuccessfullyRetrieved,
            HttpStatusCode.OK
            );
    }
}