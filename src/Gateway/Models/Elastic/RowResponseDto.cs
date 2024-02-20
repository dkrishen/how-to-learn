namespace Gateway.Models.Elastic;

public class RowResponseDto
{
    public Guid SectionId { get; set; }
    public string SectionTitle { get; set; }
    public string TopicTitle { get; set; }
    public string TopicDescription { get; set; }
    public Guid TopicId { get; set; }
}
