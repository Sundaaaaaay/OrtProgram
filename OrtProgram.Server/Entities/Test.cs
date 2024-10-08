namespace OrtProgram.Server.Entities;

public class Test
{
    public int Id { get; set; }
    public string TestType { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    //TODO: add function that increaces amount of questions after adding question to test or add function that counts amount of questions
    public int QuestionsAmount { get; set; } = 0;
    
    public ICollection<Question> Questions {get; set;} = new List<Question>();
}