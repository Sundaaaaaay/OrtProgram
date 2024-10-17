namespace OrtProgram.Server.DTO.Test;

public class UserAnswersSubmittionDto
{
    public int TestId { get; set; }
    public List<UserAnswerDto> Answers { get; set; } = new List<UserAnswerDto>();
}