using System.Net;
using Application.Common;
using Application.EntityManagement.Orders.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.EntityManagement.Orders.Handlers;

public class GetAllOrdersQueryHandler(IRepository<Order> repository)
    : IRequestHandler<GetAllOrdersQuery, QueryReferenceResponse<IEnumerable<Order>>>
{
    public virtual async Task<QueryReferenceResponse<IEnumerable<Order>>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAllAsync(null, cancellationToken);

        return new QueryReferenceResponse<IEnumerable<Order>>
        (
            entities.Paginate(request.Pagination),
            true,
            Messages.SuccessfullyRetrieved,
            HttpStatusCode.OK
        );
    }
}