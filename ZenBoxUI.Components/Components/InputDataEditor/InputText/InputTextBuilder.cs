using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;

namespace ZenBoxUI.Blazor.Common
{
  public class InputTextBuilder<T>(ZbTextInput component)
  {
    // =====================================================
    // PUBLIC ENTRY POINT - <Div>
    // =====================================================
    public void BuildRenderTree(RenderTreeBuilder builder)
    {
      builder.OpenElement(0, "div");
      builder.AddAttribute(1, "id", component.Id);
      builder.AddAttribute(2, "class", BuildWrapperClass());

      BuildInput(builder, component);
      BuildClearButton(builder, component);

      builder.CloseElement();
    }

    // =====================================================
    // INPUT RENDERING - <Input>
    // =====================================================
    private void BuildInput(RenderTreeBuilder builder, ZbTextInput component)
    {
      builder.OpenElement(10, "input");
      builder.AddMultipleAttributes(11, component.Attributes);

      builder.AddAttribute(12, "id", component.InputId);
      builder.AddAttribute(13, "class", BuildInputClass());
      builder.AddAttribute(14, "value", component.Text?.ToString() ?? string.Empty);
      builder.AddAttribute(15, "type", !component.Password ? "text" : "password");
      builder.AddAttribute(16, "placeholder", component.NullText);
      if (component.Disabled)
        builder.AddAttribute(17, "disabled", component.Disabled);

      // EVENTS
      builder.AddAttribute(20, "oninput",
          EventCallback.Factory.Create<ChangeEventArgs>(component, component.HandleInput));
      builder.AddAttribute(21, "onchange",
          EventCallback.Factory.Create<ChangeEventArgs>(component, component.HandleChange));
      builder.AddAttribute(22, "onfocus",
          EventCallback.Factory.Create<FocusEventArgs>(component, component.HandleFocus));
      builder.AddAttribute(23, "onblur",
          EventCallback.Factory.Create<FocusEventArgs>(component, component.HandleBlur));

      builder.CloseElement();
    }

    // =====================================================
    // CLEAR BUTTON
    // =====================================================
    private void BuildClearButton(RenderTreeBuilder builder, ZbTextInput component)
    {
      if (!component.ClearButton || component.Disabled)
        return;

      builder.OpenElement(30, "button");

      builder.AddAttribute(31, "type", "button");
      builder.AddAttribute(32, "class",
        $"zb-clear-btn {(string.IsNullOrEmpty(component.Text) ? "zb-clear-btn-hidden" : "")}");
      builder.AddAttribute(33, "onclick",
        EventCallback.Factory.Create(component, async () =>
        {
          if (component.Disabled)
            return;

          string? newValue =
            component.ClearBehavior == ZbClearButtonValueBehavior.Null
              ? null
              : string.Empty;

          await component.TextChanged.InvokeAsync(newValue);

          if (component.EditContext is not null)
            component.EditContext.NotifyFieldChanged(component._fieldIdentifier);

          if (component.OnInput.HasDelegate)
            await component.OnInput.InvokeAsync();

          if (component.OnChange.HasDelegate)
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

      if (component.ClearButton && !component.Disabled)
        classes.Add("zb-has-clear");

      if (component.Disabled)
        classes.Add("zb-disabled");

      if (!string.IsNullOrWhiteSpace(component.CssClass))
        classes.Add(component.CssClass);

      return string.Join(" ", classes);
    }

    private string BuildInputClass()
    {
      var classes = new List<string> { "zb-input-element" };

      if (!string.IsNullOrWhiteSpace(component.InputCssClass))
        classes.Add(component.InputCssClass);

      if (IsInvalid())
        classes.Add("zb-input-invalid");

      return string.Join(" ", classes);
    }

    private bool IsInvalid()
    {
      if (component.EditContext is null)
        return false;

      if (component.TextExpression is null)
        return false;

      var field = FieldIdentifier.Create(component.TextExpression);

      return component.EditContext.GetValidationMessages(field).Any();
    }
  }
}
