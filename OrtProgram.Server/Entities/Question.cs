namespace OrtProgram.Server.Entities;

public class Question
{
    public int Id { get; set; }
    public string QuestionText { get; set; } = string.Empty;
    public string firstAnswer { get; set; } = string.Empty;
    public string secondAnswer { get; set; } = string.Empty;
    public string thirdAnswer { get; set; } = string.Empty;
    public string rightAnswer { get; set; } = string.Empty;
    
    public int TestId { get; set; }
    public Test Test { get; set; }
}