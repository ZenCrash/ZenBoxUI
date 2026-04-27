using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Linq.Expressions;
using ZenBoxUI.Blazor.Common;

namespace ZenBoxUI.Blazor
{
  public class ZbTextInput : InputBaseComponent<string?>
  {

    /// <summary>
    /// Gets or sets the text to display.
    /// </summary>
    [Parameter]
    public string? Text
    {
      get => base.Value;
      set => base.Value = value;
    }

    /// <summary>
    /// Gets or sets the callback that is invoked when the text value changes.
    /// </summary>
    /// <remarks>The callback receives the new text value as a parameter. Use this event to respond to user
    /// input or programmatic changes to the text.</remarks>
    [Parameter]
    public EventCallback<string?> TextChanged
    {
      get => base.ValueChanged;
      set => base.ValueChanged = value;
    }

    /// <summary>
    /// Gets or sets the expression used to bind the text value for this component.
    /// </summary>
    /// <remarks>This property enables two-way binding scenarios by allowing the component to read and update
    /// the bound value. The expression is typically used for validation and change tracking in data binding
    /// frameworks.</remarks>
    [Parameter]
    public Expression<Func<string?>>? TextExpression
    {
      get => base.ValueExpression;
      set => base.ValueExpression = value;
    }

    /// <summary>
    /// Should field be a password field.
    /// </summary>
    [Parameter] public bool Password { get; set; }

    [Parameter] public bool PasswordToggleButton { get; set; }

    internal void HandlePasswordToggleButton()
    {
      Password = !Password;
    }

    /// <summary>
    /// Builds the render tree for the component.
    /// </summary>
    /// <param name="builder">The render tree builder used to construct the component's UI.</param>
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
      var inputType = !Password ? InputType.Text : InputType.Password;
      new InputBuilder<string?>(this, inputType).BuildRenderTree(builder);
    }
  }
}
