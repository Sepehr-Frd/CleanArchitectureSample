#region

using Application.Common;
using Application.EntityManagement.Payments.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using System.Net;

#endregion

namespace Application.EntityManagement.Payments.Handlers;

public sealed class GetAllPaymentsQueryHandler(IRepository<Payment> repository)
    : IRequestHandler<GetAllPaymentsQuery, QueryReferenceResponse<IEnumerable<Payment>>>
{
    public async Task<QueryReferenceResponse<IEnumerable<Payment>>> Handle(GetAllPaymentsQuery request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAllAsync(null, request.Pagination, cancellationToken);

        return new QueryReferenceResponse<IEnumerable<Payment>>(
            entities,
            true,
            Messages.SuccessfullyRetrieved,
            HttpStatusCode.OK);
    }
}