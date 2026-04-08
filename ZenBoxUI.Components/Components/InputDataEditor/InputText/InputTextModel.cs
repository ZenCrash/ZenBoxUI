using Microsoft.AspNetCore.Components;

namespace ZenBoxUI.Blazor.Common
{
  public class InputTextModel : InputBaseModel
  {
    public string? Value { get; set; }

    public string InputType { get; set; } = "text";

    public string? InputCssClass { get; set; }

    public bool Enabled { get; set; }

    public bool ReadOnly { get; set; }

    public bool ShowClearButton { get; set; }

    public int? InputDelay { get; set; }
  }
}
