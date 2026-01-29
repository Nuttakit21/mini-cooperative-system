using Microsoft.AspNetCore.Components;
using MiniCoop.Web.Services;

namespace MiniCoop.Web.Pages
{
    public partial class Index
    {
        [Inject] ApiService Api { get; set; } = default!;
        int test = 0;

        protected async Task push()
        {
            try
            {
                test++;
            }
            catch (Exception ex) { }
        }
    }
}