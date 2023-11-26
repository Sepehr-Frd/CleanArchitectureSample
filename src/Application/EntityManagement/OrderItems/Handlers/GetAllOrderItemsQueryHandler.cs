#region

using Application.Common;
using Application.EntityManagement.OrderItems.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using System.Net;

#endregion

namespace Application.EntityManagement.OrderItems.Handlers;

public sealed class GetAllOrderItemsQueryHandler : IRequestHandler<GetAllOrderItemsQuery, QueryReferenceResponse<IEnumerable<OrderItem>>>
{
    private readonly IRepository<OrderItem> _repository;

    public GetAllOrderItemsQueryHandler(IRepository<OrderItem> repository)
    {
        _repository = repository;
    }

    public async Task<QueryReferenceResponse<IEnumerable<OrderItem>>> Handle(GetAllOrderItemsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(null, request.Pagination, cancellationToken);

        return new QueryReferenceResponse<IEnumerable<OrderItem>>(
            entities,
            true,
            Messages.SuccessfullyRetrieved,
            HttpStatusCode.OK);
    }
}