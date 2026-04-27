using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;

namespace ZenBoxUI.Blazor.Common
{
  internal class InputBuilder<T>(InputBaseComponent<T> component, InputType inputType)
  {
    // =====================================================
    // <Div> => <input>     [<button>]      [<button>]
    // <Div> => <InputType> [<ClearButton>] [<FeatureSpesificButtons>]
    // =====================================================
    public void BuildRenderTree(RenderTreeBuilder builder)
    {
      builder.OpenElement(0, "div");
      builder.AddAttribute(1, "id", component.Id);
      builder.AddAttribute(2, "class", BuildWrapperClass());
      BuildInput(builder, component);
      BuildClearButton(builder, component);
      InputSpecificBuilder.InputOptionsBuilder(builder, component, inputType);
      builder.CloseElement();
    }

    // =====================================================
    // INPUT RENDERING - <Input>
    // =====================================================
    private void BuildInput(RenderTreeBuilder builder, InputBaseComponent<T> component)
    {
      builder.OpenElement(20, "input");
      builder.AddAttribute(21, "id", component.InputId);
      builder.AddAttribute(22, "class", BuildInputClass());
      builder.AddAttribute(23, "value", component.Value?.ToString() ?? string.Empty);
      builder.AddAttribute(24, "type", Enum.GetName(inputType)!.ToLower());
      builder.AddAttribute(25, "placeholder", component.NullText);
      //Disable input
      if (component.Disabled)
        builder.AddAttribute(30, "disabled", component.Disabled);
      //Events
      builder.AddAttribute(31, "oninput", EventCallback.Factory.Create<ChangeEventArgs>(component, component.HandleInput));
      builder.AddAttribute(32, "onchange", EventCallback.Factory.Create<ChangeEventArgs>(component, component.HandleChange));
      builder.AddAttribute(33, "onfocus", EventCallback.Factory.Create<FocusEventArgs>(component, component.HandleFocus));
      builder.AddAttribute(34, "onblur", EventCallback.Factory.Create<FocusEventArgs>(component, component.HandleBlur));
      builder.CloseElement();
    }

    // =====================================================
    // <ClearButton> - Optional
    // =====================================================
    private static void BuildClearButton(RenderTreeBuilder builder, InputBaseComponent<T> component)
    {
      if (!component.ClearButton || component.Disabled || component.Value == null)
        return;

      builder.OpenElement(40, "button");
      builder.AddAttribute(41, "type", "button");
      builder.AddAttribute(42, "class", "zb-clear-btn");
      builder.AddAttribute(43, "onclick", EventCallback.Factory.Create<FocusEventArgs>(component, component.HandleClearButton));
      builder.OpenElement(44, "i");
      builder.AddAttribute(45, "class", "zbi zbi-x");
      builder.CloseElement();
      builder.CloseElement();
    }

    // =====================================================
    // STYLE HELPERS
    // =====================================================
    private string BuildWrapperClass()
    {
      var classes = new List<string> { "zb-input" };

      if (component is { ClearButton: true, Disabled: false })
        classes.Add("zb-has-clear");
      if (component is ZbTextInput textInput && textInput.PasswordToggleButton && !component.Disabled)
        classes.Add("zb-has-password-toggle");
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
      if (component.ValueExpression is null)
        return false;
      var field = FieldIdentifier.Create(component.ValueExpression);
      return component.EditContext.GetValidationMessages(field).Any();
    }
  }
}
