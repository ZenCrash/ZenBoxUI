using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using static System.Net.Mime.MediaTypeNames;

namespace ZenBoxUI.Blazor.Common
{
  public abstract class InputBaseComponent : ComponentBase
  {

    [Parameter]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Parameter]
    public string InputId { get; set; } = Guid.NewGuid().ToString();

    [Parameter]
    public string? NullText { get; set; }

    [Parameter]
    public string? InputCssClass { get; set; }

    [Parameter]
    public bool ClearButton { get; set; }

    [Parameter]
    public string? CssClass { get; set; }

    [Parameter]
    public bool? ValidationEnabled { get; set; } = true;

    [Parameter]
    public bool Enabled { get; set; } = true;

    [Parameter]
    public bool ReadOnly { get; set; }

    [Parameter]
    public EventCallback OnInput { get; set; }

    [Parameter]
    public EventCallback OnChange { get; set; }

    [Parameter]
    public EventCallback OnFocus { get; set; }

    [Parameter]
    public EventCallback OnBlur { get; set; }

    [Parameter]
    public EventCallback OnKeyDown { get; set; }

    [Parameter]
    public EventCallback OnKeyUp { get; set; }


    public async Task HandleInput(ChangeEventArgs e)
    {
      //var text = e.Value?.ToString();
      //await OnInput.InvokeAsync(text);

      if (OnInput.HasDelegate)
        await OnInput.InvokeAsync();
    }

    public async Task HandleChange(ChangeEventArgs e)
    {
      //var text = e.Value?.ToString();
      //await OnChange.InvokeAsync(text);

      if (OnChange.HasDelegate)
        await OnChange.InvokeAsync();
    }

    public async Task HandleFocus(FocusEventArgs e)
    {
      if (OnFocus.HasDelegate)
        await OnFocus.InvokeAsync();
    }

    public async Task HandleBlur(FocusEventArgs e)
    {
      if (OnBlur.HasDelegate)
        await OnBlur.InvokeAsync();
    }

    public async Task HandleKeyDown(KeyboardEventArgs e)
    {
      if (OnKeyDown.HasDelegate)
        await OnKeyDown.InvokeAsync();
    }

    public async Task HandleKeyUp(KeyboardEventArgs e)
    {
      if (OnKeyUp.HasDelegate)
        await OnKeyUp.InvokeAsync();
    }


    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object> Attributes { get; set; } = [];
  }
}
