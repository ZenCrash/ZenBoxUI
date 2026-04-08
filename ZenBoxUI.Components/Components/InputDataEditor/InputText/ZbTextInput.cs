using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using ZenBoxUI.Blazor.Common;

namespace ZenBoxUI.Blazor
{
  public class ZbTextInput : InputBaseComponent
  {
    /// <summary>
    /// Gets or sets the text to display.
    /// </summary>
    [Parameter] public string? Text { get; set; }

    /// <summary>
    /// Gets or sets the callback that is invoked when the text value changes.
    /// </summary>
    /// <remarks>The callback receives the new text value as a parameter. Use this event to respond to user
    /// input or programmatic changes to the text.</remarks>
    [Parameter] public EventCallback<string> TextChanged { get; set; }

    /// <summary>
    /// Gets or sets the expression used to bind the text value for this component.
    /// </summary>
    /// <remarks>This property enables two-way binding scenarios by allowing the component to read and update
    /// the bound value. The expression is typically used for validation and change tracking in data binding
    /// frameworks.</remarks>
    [Parameter] public Expression<Func<string>>? TextExpression { get; set; }


    //[Parameter] public int? InputDelay { get; set; }

    /// <summary>
    /// Should field be a password field.
    /// </summary>
    
    [Parameter] public bool Password { get; set; }

    public async Task HandleInput(ChangeEventArgs e)
    {
      Text = e.Value?.ToString();
      await TextChanged.InvokeAsync(Text);

      if (OnInput.HasDelegate)
        await OnInput.InvokeAsync();
    }

    public async Task HandleChange(ChangeEventArgs e)
    {
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

    //======================================//
    // Component Builder                    //
    //======================================//

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
      new InputTextBuilder(this).BuildRenderTree(builder);
    }
  }
}
