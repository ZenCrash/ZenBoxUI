using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;

namespace ZenBoxUI.Blazor.Common
{
  public class InputTextBuilder(ZbTextInput component)
  {
    // =====================================================
    // <Div> => [<TextLabel>] <TextInput> [<ClearButton>] [<PasswordToggleButton>]
    // =====================================================
    public void BuildRenderTree(RenderTreeBuilder builder)
    {
      builder.OpenElement(0, "div");
      builder.AddAttribute(1, "id", component.Id);
      builder.AddAttribute(2, "class", BuildWrapperClass());

      //BuildLabel(builder, component);
      BuildInput(builder, component);
      BuildClearButton(builder, component);

      builder.CloseElement();
    }

    // =====================================================
    // <Label> - OPTIONAL
    // =====================================================

    private void BuildLabel(RenderTreeBuilder builder, ZbTextInput component)
    {
      if (!string.IsNullOrWhiteSpace(component.Label))

      builder.OpenElement(10, "label");
      builder.AddAttribute(11, "for", component.InputId);
      builder.AddAttribute(12, "class", "zb-input-label");
      builder.AddContent(13, component.Label);
      builder.CloseElement();
    }

    // =====================================================
    // INPUT RENDERING - <Input>
    // =====================================================
    private async Task BuildInput(RenderTreeBuilder builder, ZbTextInput component)
    {
      builder.OpenElement(20, "input");
      builder.AddAttribute(21, "id", component.InputId);
      builder.AddAttribute(22, "class", BuildInputClass());
      builder.AddAttribute(23, "value", component.Text?.ToString() ?? string.Empty);
      builder.AddAttribute(24, "type", !component.Password ? "text" : "password");
      builder.AddAttribute(25, "placeholder", component.NullText);

      //Disable input
      if (component.Disabled)
        builder.AddAttribute(30, "disabled", component.Disabled);

      // EVENTS
      builder.AddAttribute(31, "oninput", EventCallback.Factory.Create<ChangeEventArgs>(component, component.HandleInput));
      builder.AddAttribute(32, "onchange", EventCallback.Factory.Create<ChangeEventArgs>(component, component.HandleChange));
      builder.AddAttribute(33, "onfocus", EventCallback.Factory.Create<FocusEventArgs>(component, component.HandleFocus));
      builder.AddAttribute(34, "onblur", EventCallback.Factory.Create<FocusEventArgs>(component, component.HandleBlur));
      builder.CloseElement();
    }

    // =====================================================
    // <ClearButton> - Optional
    // =====================================================
    private void BuildClearButton(RenderTreeBuilder builder, ZbTextInput component)
    {
      if (!component.ClearButton || component.Disabled)
        return;

      builder.OpenElement(40, "button");
      builder.AddAttribute(41, "type", "button");
      builder.AddAttribute(42, "class", $"zb-clear-btn {(string.IsNullOrEmpty(component.Text) ? "zb-clear-btn-hidden" : "")}");
      builder.AddAttribute(43, "onclick", EventCallback.Factory.Create<FocusEventArgs>(component, component.HandleClearButton));
      builder.AddContent(44, "✖");
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
