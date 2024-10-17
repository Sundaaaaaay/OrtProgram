namespace OrtProgram.Server.DTO.Test;

public class QuestionResultDtо
{
    public int QuestionId { get; set; }
    public bool IsCorrect { get; set; }
    public string CorrectAnswer { get; set; } = string.Empty;
}