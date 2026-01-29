namespace MiniCoop.Web.Shared.Layout
{
    public partial class MainLayout 
    {
        bool isCollapsed = false;

        void ToggleSidebar()
        {
            isCollapsed = !isCollapsed;
        }
    }
}