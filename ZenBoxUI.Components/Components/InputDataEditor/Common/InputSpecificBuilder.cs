using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;

namespace ZenBoxUI.Blazor.Common
{
  internal class InputSpecificBuilder
  {
    public static void InputOptionsBuilder<T>(RenderTreeBuilder builder, InputBaseComponent<T> component, InputType inputType)
    {
      switch (inputType)
      {
        case InputType.Text:
        case InputType.Password:
          InputTextOptionsBuilder(builder, component as ZbTextInput, inputType);
          break;
        case InputType.Number:
          InputNumberOptionsBuilder(builder, component, inputType);
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof(inputType));
      }
    }

    public static void InputTextOptionsBuilder(RenderTreeBuilder builder, ZbTextInput? component, InputType inputType)
    {
      if (!component!.PasswordToggleButton || component.Disabled)
        return;

      builder.OpenElement(50, "button");
      builder.AddAttribute(51, "type", "button");
      builder.AddAttribute(52, "class", "zb-password-toggle-btn");
      builder.AddAttribute(53, "onclick",
        EventCallback.Factory.Create<FocusEventArgs>(component, component.HandlePasswordToggleButton));
      builder.OpenElement(54, "i");
      builder.AddAttribute(55, "class",
        component.Password ? "zbi zbi-eye-fill" : "zbi zbi-eye-slash-fill"); builder.CloseElement();
      builder.CloseElement();
    }

    public static void InputNumberOptionsBuilder<T>(RenderTreeBuilder builder, InputBaseComponent<T> component, InputType inputType)
    {

    }
  }
}
