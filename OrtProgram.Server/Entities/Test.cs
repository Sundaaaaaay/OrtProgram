namespace OrtProgram.Server.Entities;

public class Test
{
    public int Id { get; set; }
    public string TestType { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    
    public ICollection<Question> Questions {get; set;} = new List<Question>();
}