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
      var model = BuildModel();

      builder.OpenElement(0, "div");
      builder.AddAttribute(1, "class", model.CssClass);

      BuildInput(builder, model);
      BuildClearButton(builder, model);

      builder.CloseElement();
    }

    // =====================================================
    // MODEL CREATION
    // =====================================================
    private InputTextModel BuildModel()
    {
      return new InputTextModel
      {
        Value = component.Text,
        InputType = component.Password ? "password" : "text",

        Id = component.Id,
        InputId = component.InputId,

        NullText = component.NullText,

        CssClass = BuildWrapperClass(),
        InputCssClass = BuildInputClass(),

        Enabled = component.Enabled,
        ReadOnly = component.ReadOnly,

        ShowClearButton = component.ClearButton && !string.IsNullOrEmpty(component.Text),

        InputDelay = component.InputDelay,

        Attributes = BuildAttributes()
      };
    }

    // =====================================================
    // INPUT RENDERING
    // =====================================================
    private void BuildInput(RenderTreeBuilder builder, InputTextModel model)
    {
      builder.OpenElement(10, "input");

      builder.AddMultipleAttributes(11, model.Attributes);

      builder.AddAttribute(12, "class", model.InputCssClass);
      builder.AddAttribute(13, "value", model.Value ?? string.Empty);
      builder.AddAttribute(14, "type", model.InputType);
      builder.AddAttribute(15, "placeholder", model.NullText);

      if (!model.Enabled)
        builder.AddAttribute(16, "disabled", !component.Enabled);

      if (model.ReadOnly)
        builder.AddAttribute(17, "readonly", "readonly");

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
    private void BuildClearButton(RenderTreeBuilder builder, InputTextModel model)
    {
      if (!model.ShowClearButton || !model.Enabled)
        return;

      builder.OpenElement(30, "button");

      builder.AddAttribute(31, "type", "button");
      builder.AddAttribute(32, "class", "zb-clear-btn");

      builder.AddAttribute(33, "onclick",
          EventCallback.Factory.Create(component, async () =>
          {
            component.Text = string.Empty;
            await component.TextChanged.InvokeAsync(component.Text);

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

      if (component.ReadOnly)
        classes.Add("zb-readonly");

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
