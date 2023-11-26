#region

using Application.Common;
using Application.EntityManagement.Comments.Dtos;
using MediatR;

#endregion

namespace Application.EntityManagement.Comments.Commands;

public record CreateCommentCommand(CommentDto CommentDto) : IRequest<CommandResult>;