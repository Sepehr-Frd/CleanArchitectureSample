using Application.Abstractions;
using Application.Common;
using Application.EntityManagement.Comments.Dtos;
using Application.EntityManagement.Comments.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Application.EntityManagement.Comments.Handlers;

public class GetAllCommentsByProductExternalIdQueryHandler
    : IRequestHandler<GetAllCommentsByProductExternalIdQuery, QueryReferenceResponse<IEnumerable<CommentDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMappingService _mappingService;
    private readonly ILogger _logger;

    public GetAllCommentsByProductExternalIdQueryHandler(IUnitOfWork unitOfWork, IMappingService mappingService, ILogger logger)
    {
        _unitOfWork = unitOfWork;
        _mappingService = mappingService;
        _logger = logger;
    }

    public async Task<QueryReferenceResponse<IEnumerable<CommentDto>>> Handle(GetAllCommentsByProductExternalIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork
            .ProductRepository
            .GetByExternalIdAsync(request.ProductExternalId, cancellationToken,
                entity => entity.Comments);

        if (product is null)
        {
            return new QueryReferenceResponse<IEnumerable<CommentDto>>(
                null,
                false,
                Messages.NotFound,
                HttpStatusCode.NotFound);
        }

        if (product.Comments is null)
        {
            _logger.LogError(Messages.EntityRelationshipsRetrievalFailed, DateTime.UtcNow, typeof(Product), typeof(GetAllCommentsByProductExternalIdQueryHandler));

            return new QueryReferenceResponse<IEnumerable<CommentDto>>(
                null,
                false,
                Messages.InternalServerError,
                HttpStatusCode.InternalServerError);
        }

        var commentDtos = _mappingService.Map<ICollection<Comment>, ICollection<CommentDto>>(product.Comments);

        if (commentDtos is not null)
        {
            return new QueryReferenceResponse<IEnumerable<CommentDto>>(
                commentDtos.Paginate(request.Pagination),
                true,
                Messages.SuccessfullyRetrieved,
                HttpStatusCode.OK);
        }

        _logger.LogError(Messages.MappingFailed, DateTime.UtcNow, typeof(ICollection<Comment>), typeof(GetAllCommentsByProductExternalIdQueryHandler));

        return new QueryReferenceResponse<IEnumerable<CommentDto>>(
            null,
            false,
            Messages.InternalServerError,
            HttpStatusCode.InternalServerError);
    }
}