using OrtProgram.Server.DTO.Test;
using OrtProgram.Server.Entities;

namespace OrtProgram.Server.Mappers;

public static class UserAnswerDtoMapper
{
    public static UserAnswer ToUserAnswerModel(this UserAnswerDto userAnswerDto)
    {
        return new UserAnswer
        {
            QuestionId = userAnswerDto.QuestionId,
            SelectedAnswer = userAnswerDto.SelectedAnswer,
        };
    }

    public static UserAnswerDto ToResponseTestResultDto(this UserAnswer userAnswer)
    {
        return new UserAnswerDto
        {
            QuestionId = userAnswer.QuestionId,
            SelectedAnswer = userAnswer.SelectedAnswer,
        };
    }
}