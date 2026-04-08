using Microsoft.AspNetCore.Components;

namespace ZenBoxUI.Blazor.Common
{
  public abstract class InputBaseModel
  {
    public string? Id { get; set; } = Guid.NewGuid().ToString();

    public string? InputId { get; set; } = Guid.NewGuid().ToString();

    public string? NullText { get; set; }

    public string? InputCssClass { get; set; }

    public bool ClearButton { get; set; }

    public string? CssClass { get; set; }

    public bool? ValidationEnabled { get; set; } = true;

    public bool Enabled { get; set; } = true;

    public bool ReadOnly { get; set; }

    public EventCallback OnInput { get; set; }

    public EventCallback OnChange { get; set; }

    public EventCallback OnFocus { get; set; }

    public EventCallback OnBlur { get; set; }

    public EventCallback OnKeyDown { get; set; }

    public EventCallback OnKeyUp { get; set; }


    public Dictionary<string, object> Attributes { get; set; } = [];
  }
}
