namespace Application.EntityManagement.Answers.Dtos;

public record AnswerDto(
    int ExternalId,
    string Title,
    string Description
);