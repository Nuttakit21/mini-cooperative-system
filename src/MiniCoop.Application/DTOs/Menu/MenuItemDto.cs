namespace MiniCoop.Application.DTOs.Menu
{
    public class MenuItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Path { get; set; }
    }
}
