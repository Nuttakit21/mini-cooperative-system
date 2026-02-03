namespace MiniCoop.Application.DTOs
{
    public class ApplicationDto
    {
        public int APPLICATION_ID { get; set; }
        public string APPLICATION_CODE { get; set; } = null!;
        public string APPLICATION_NAME { get; set; } = null!;
        public string? ICON { get; set; }
    }
}
