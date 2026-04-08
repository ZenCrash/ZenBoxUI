using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;

namespace ZenBoxUI.Blazor.Common
{
  public class InputTextBuilder(ZbTextInput component)
  {
    // =====================================================
    // PUBLIC ENTRY POINT
    // =====================================================
    public void BuildRenderTree(RenderTreeBuilder builder)
    {
      builder.OpenElement(0, "div");
      builder.AddAttribute(1, "class", component.CssClass);

      BuildInput(builder, component);
      BuildClearButton(builder, component);

      builder.CloseElement();
    }

    // =====================================================
    // INPUT RENDERING
    // =====================================================
    private void BuildInput(RenderTreeBuilder builder, ZbTextInput component)
    {
      builder.OpenElement(10, "input");

      builder.AddMultipleAttributes(11, component.Attributes);

      builder.AddAttribute(12, "class", component.InputCssClass);
      builder.AddAttribute(13, "value", component.Text ?? string.Empty);
      builder.AddAttribute(14, "type", !component.Password ? "text" : "password");
      builder.AddAttribute(15, "placeholder", component.NullText);

      if (!component.Enabled)
        builder.AddAttribute(16, "disabled", !component.Enabled);

      // EVENTS
      builder.AddAttribute(20, "oninput",
          EventCallback.Factory.Create<ChangeEventArgs>(component, component.HandleInput));

      builder.AddAttribute(21, "onchange",
          EventCallback.Factory.Create<ChangeEventArgs>(component, component.HandleChange));

      builder.AddAttribute(22, "onfocus",
          EventCallback.Factory.Create<FocusEventArgs>(component, component.HandleFocus));

      builder.AddAttribute(23, "onblur",
          EventCallback.Factory.Create<FocusEventArgs>(component, component.HandleBlur));

      builder.AddAttribute(24, "onkeydown",
          EventCallback.Factory.Create<KeyboardEventArgs>(component, component.HandleKeyDown));

      builder.AddAttribute(25, "onkeyup",
          EventCallback.Factory.Create<KeyboardEventArgs>(component, component.HandleKeyUp));

      builder.CloseElement();
    }

    // =====================================================
    // CLEAR BUTTON
    // =====================================================
    private void BuildClearButton(RenderTreeBuilder builder, ZbTextInput component)
    {
      if (!component.ClearButton || !component.Enabled)
        return;

      builder.OpenElement(30, "button");

      builder.AddAttribute(31, "type", "button");
      builder.AddAttribute(32, "class", "zb-clear-btn");

      builder.AddAttribute(33, "onclick",
          EventCallback.Factory.Create(component, async () =>
          {
            component.Text = string.Empty;

            await component.OnInput.InvokeAsync();
            await component.OnChange.InvokeAsync();
          }));

      builder.AddContent(34, "✖");

      builder.CloseElement();
    }

    // =====================================================
    // STYLE HELPERS
    // =====================================================
    private string BuildWrapperClass()
    {
      var classes = new List<string> { "zb-input" };

      classes.Add(component.Enabled ? "" : "zb-disabled");

      if (!string.IsNullOrWhiteSpace(component.CssClass))
        classes.Add(component.CssClass);

      return string.Join(" ", classes);
    }

    private string BuildInputClass()
    {
      var classes = new List<string> { "zb-input-element" };

      if (!string.IsNullOrWhiteSpace(component.InputCssClass))
        classes.Add(component.InputCssClass);

      return string.Join(" ", classes);
    }

    // =====================================================
    // ATTRIBUTES
    // =====================================================
    private Dictionary<string, object> BuildAttributes()
    {
      var attrs = new Dictionary<string, object>(component.Attributes)
      {
        ["id"] = component.InputId
      };

      return attrs;
    }
  }
}
