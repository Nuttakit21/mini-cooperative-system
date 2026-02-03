namespace MiniCoop.Application.DTOs.Menu
{
    public class MenuGroupDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Icon { get; set; }
        public List<MenuItemDto> Menus { get; set; } = new();
    }
}
