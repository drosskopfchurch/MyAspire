public class Answer
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int QuestionId { get; set; } = 0;
    public double Time { get; set; } = 0.0;
    public Guid PlayerId { get; set; }
    public int Value { get; set; } = 0;
}