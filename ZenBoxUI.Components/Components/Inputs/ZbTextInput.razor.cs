using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Globalization;
using ZenBoxUI.Blazor.Common;

namespace ZenBoxUI.Blazor;

public partial class ZbTextInput : ZbInputBase<string?>
{

  /// <summary>
  /// Should field be a password field.
  /// </summary>
  [Parameter] public bool IsPassword { get; set; }

  /// <summary>
  /// enable a button to toggle password visibility.
  /// </summary>
  [Parameter] public bool PasswordToggleButton { get; set; }

  // =========================
  // CSS
  // =========================

  private string WrapperClass =>
    string.Join(" ",
      new[]
      {
        "zb-input",
        HasError() ? "zb-input-invalid" : null,
        Disabled ? "zb-disabled" : null,
        _displayClearButton ? "zb-has-clear" : null,
        CssClass
      }.Where(x => !string.IsNullOrWhiteSpace(x)));

  private string InputClass => $"zb-input-control {InputCssClass}";

  // =========================
  // INPUT EVENTS
  // =========================

  private async Task HandleFocus(FocusEventArgs e)
  {
    if (OnFocus.HasDelegate)
      await OnFocus.InvokeAsync();
  }

  private async Task HandleDeselect(FocusEventArgs e)
  {
    if (OnDeselect.HasDelegate)
      await OnDeselect.InvokeAsync();
  }

  private async Task HandleInput(ChangeEventArgs e)
  {
    var value = e.Value?.ToString();
    if (ClearButton && (InputBindMode != ZbInputBindMode.OnChange || (InputBindMode == ZbInputBindMode.OnChange && !OnInput.HasDelegate)))
      _value = value;

    _displayClearButton = ClearButton && value != null && !Disabled;

    if (InputBindMode == ZbInputBindMode.OnInput)
      await SetValueAsync(value);
    else if (InputBindMode == ZbInputBindMode.InputDelay)
      await DebounceAsync(async () => await SetValueAsync(value));

    if (OnInput.HasDelegate)
    {
      await OnInput.InvokeAsync(value);
    }

  }

  private async Task HandleChange(ChangeEventArgs e)
  {
    var value = e.Value?.ToString();
    if (InputBindMode == ZbInputBindMode.OnChange)
    {
      _value = value;
      await SetValueAsync(value);
    }

    if (OnChange.HasDelegate)
      await OnChange.InvokeAsync(value);
  }

  // =========================
  // ACTIONS
  // =========================

  private async Task ClearInputBtn()
  {
    _value = null;
    _displayClearButton = false;
    await SetValueAsync(null);
  }

  private void TogglePasswordBtn()
  {
    IsPassword = !IsPassword;
  }

}