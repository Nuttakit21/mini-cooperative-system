namespace MiniCoop.Application.DTOs.Menu
{
    public class MenuDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Icon { get; set; }
        public List<MenuGroupDto> Groups { get; set; } = new();
    }
}
