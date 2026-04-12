using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using static System.Net.Mime.MediaTypeNames;

namespace ZenBoxUI.Blazor.Common
{
  public abstract class InputBaseComponent<T> : ComponentBase
  {
    [Parameter] public string Id { get; set; } = Guid.NewGuid().ToString();

    [Parameter] public string InputId { get; set; } = Guid.NewGuid().ToString();

    [Parameter] public string? NullText { get; set; }

    [Parameter] public string? InputCssClass { get; set; }

    [Parameter] public string? CssClass { get; set; }

    [Parameter] public bool ClearButton { get; set; }

    [Parameter] public ZbClearButtonValueBehavior ClearBehavior { get; set; } = ZbClearButtonValueBehavior.Default;

    [Parameter] public bool? ValidationEnabled { get; set; } = true;

    [Parameter] public bool Disabled { get; set; }

    [Parameter] public EventCallback OnInput { get; set; }

    [Parameter] public EventCallback OnChange { get; set; }

    [Parameter] public EventCallback OnFocus { get; set; }

    [Parameter] public EventCallback OnBlur { get; set; }

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object> Attributes { get; set; } = [];
  }
}
