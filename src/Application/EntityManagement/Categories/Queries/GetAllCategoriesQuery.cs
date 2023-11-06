using Application.Common;
using Application.EntityManagement.Categories.Dtos;
using Domain.Entities;
using MediatR;

namespace Application.EntityManagement.Categories.Queries;

public record GetAllCategoriesQuery : IRequest<QueryReferenceResponse<IEnumerable<CategoryDto>>>;