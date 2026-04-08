using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using ZenBoxUI.Blazor.Common;

namespace ZenBoxUI.Blazor
{
  public class ZbTextInput : InputBaseComponent
  {
    [Parameter]
    public string? Text { get; set; }

    [Parameter]
    public EventCallback<string> TextChanged { get; set; }

    [Parameter]
    public int? InputDelay { get; set; }

    [Parameter]
    public bool Password { get; set; }

    //======================================//
    // Component Builder                    //
    //======================================//

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
      new InputTextBuilder(this).BuildRenderTree(builder);
    }
  }
}
