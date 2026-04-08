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

    [Parameter] public bool Password { get; set; }

    //======================================//
    // Component Builder                    //
    //======================================//

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
      new InputTextBuilder(this).BuildRenderTree(builder);
    }
  }
}
