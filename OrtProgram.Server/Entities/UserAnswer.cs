namespace OrtProgram.Server.Entities;

public class UserAnswer
{
    public int QuestionId { get; set; }
    public string SelectedAnswer { get; set; } = string.Empty;
    public DateTime TestingTime { get; set; }
}