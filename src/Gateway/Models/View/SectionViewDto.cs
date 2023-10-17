namespace Gateway.Models.View
{
    public class SectionViewDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public string[] Topics { get; set; } = null!;
    }
}
