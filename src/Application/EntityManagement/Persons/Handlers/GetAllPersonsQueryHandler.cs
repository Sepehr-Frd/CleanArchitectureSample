using Application.Common;
using Application.EntityManagement.Persons.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using System.Net;

namespace Application.EntityManagement.Persons.Handlers;

public class GetAllPersonsQueryHandler(IRepository<Person> repository)
    : IRequestHandler<GetAllPersonsQuery, QueryReferenceResponse<IEnumerable<Person>>>
{
    public virtual async Task<QueryReferenceResponse<IEnumerable<Person>>> Handle(GetAllPersonsQuery request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAllAsync(null, cancellationToken, request.RelationsToInclude);

        return new QueryReferenceResponse<IEnumerable<Person>>
            (
            entities.Paginate(request.Pagination),
            true,
            Messages.SuccessfullyRetrieved,
            HttpStatusCode.OK
            );
    }
}