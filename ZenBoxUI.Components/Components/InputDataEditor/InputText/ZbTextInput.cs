using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using ZenBoxUI.Blazor.Common;

namespace ZenBoxUI.Blazor
{
  public class ZbTextInput : InputBaseComponent<string?>
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
    [Parameter] public EventCallback<string?> TextChanged { get; set; }

    /// <summary>
    /// Gets or sets the expression used to bind the text value for this component.
    /// </summary>
    /// <remarks>This property enables two-way binding scenarios by allowing the component to read and update
    /// the bound value. The expression is typically used for validation and change tracking in data binding
    /// frameworks.</remarks>
    [Parameter] public Expression<Func<string?>>? TextExpression { get; set; }

    //[Parameter] public int? InputDelay { get; set; }

    /// <summary>
    /// Should field be a password field.
    /// </summary>
    
    [Parameter] public bool Password { get; set; }

    protected override void OnParametersSet()
    {
      if (TextExpression is not null)
      {
        FieldIdentifier = FieldIdentifier.Create(TextExpression);
      }
    }

    public async Task HandleInput(ChangeEventArgs e)
    {
      if (InputBindMode == ZbInputBindMode.OnInput)
      {
        Text = (string?)e.Value;
        await TextChanged.InvokeAsync(Text);

        if (EditContext is not null)
          EditContext.NotifyFieldChanged(FieldIdentifier);
      }

      if (OnInput.HasDelegate)
        await OnInput.InvokeAsync();
    }

    public async Task HandleChange(ChangeEventArgs e)
    {
      if (InputBindMode == ZbInputBindMode.OnChange)
      {
        Text = (string?)e.Value;
        await TextChanged.InvokeAsync(Text);

        if (EditContext is not null)
          EditContext.NotifyFieldChanged(FieldIdentifier);
      }

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


    public void HandleClearButton()
    {
      var newValue = (ClearBehavior == ZbClearButtonValueBehavior.Default) ? string.Empty : null;
      TextChanged.InvokeAsync(newValue);

      if (EditContext is not null)
        EditContext.NotifyFieldChanged(FieldIdentifier);

      if (OnInput.HasDelegate)
        OnInput.InvokeAsync();

      if (OnChange.HasDelegate)
        OnChange.InvokeAsync();
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
