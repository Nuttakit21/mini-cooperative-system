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
                string result = "";
                var data = await Api.GetAsync<string>("api/test/test");

                if (data == null)
                {
                    result = "❌ เรียกไม่ได้ (อาจโดน 401)";
                    return;
                }

                result = data;
            }
            catch (Exception ex) { }
        }
    }
}