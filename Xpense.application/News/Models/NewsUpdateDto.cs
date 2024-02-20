public class NewsUpdateDto
{
    public Guid Id { get; set; } // Consider if you need this for the Update operation
    public string Title { get; set; }
    public string Content { get; set; }
    public int Priority { get; set; }
}
