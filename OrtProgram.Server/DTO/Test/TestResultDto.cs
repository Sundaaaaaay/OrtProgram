namespace OrtProgram.Server.DTO.Test;

public class TestResultDto
{
    public int TotalQuestions { get; set; }
    public int CorrectAnswers { get; set; }
    public List<QuestionResultDtо> Details { get; set; } = new();
}