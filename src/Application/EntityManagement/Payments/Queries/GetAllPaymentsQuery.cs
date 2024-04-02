using System.Linq.Expressions;
using Application.Common;
using Domain.Common;
using Domain.Entities;
using MediatR;

namespace Application.EntityManagement.Payments.Queries;

public record GetAllPaymentsQuery(
        Pagination Pagination,
        Expression<Func<Payment, bool>>? Filter = null)
    : IRequest<QueryResponse<IEnumerable<Payment>>>;