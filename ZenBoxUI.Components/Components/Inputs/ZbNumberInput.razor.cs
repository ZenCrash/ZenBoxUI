using System.Globalization;
using System.Numerics;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using ZenBoxUI.Blazor.Common;

namespace ZenBoxUI.Blazor;

public partial class ZbNumberInput<TValue> : ZbInputBase<TValue> where TValue : INumber<TValue>
{
  [Parameter] public TValue? MinValue { get; set; }
  [Parameter] public TValue? MaxValue { get; set; }
  [Parameter] public TValue? Increment { get; set; } = TValue.One;

  /// <summary>
  /// Disable the increment and decrement buttons.
  /// </summary>
  [Parameter] public bool DisableIncrementButtons { get; set; }
  [Parameter] public bool DisabledIncrementHotKeys { get; set; }

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
    var value = BindConverter.TryConvertTo<TValue>(e.Value, CultureInfo.CurrentCulture, out var result)
      ? result
      : default!;
    _value = value;
    _displayClearButton = ClearButton && value != null && !Disabled;

    switch (InputBindMode)
    {
      case ZbInputBindMode.OnInput:
        await SetValueAsync(_value);
        await OnInput.InvokeAsync(_value);
        break;
      case ZbInputBindMode.InputDelay:
        await DebounceAsync(async () =>
        {
          await SetValueAsync(_value);
          await OnInput.InvokeAsync(_value);
        });
        break;
      case ZbInputBindMode.OnChange:
        if (OnInput.HasDelegate)
          await SetValueAsync(value);
        break;
    }
  }

  private async Task HandleChange(ChangeEventArgs e)
  {
    var value = BindConverter.TryConvertTo<TValue>(e.Value, CultureInfo.CurrentCulture, out var result)
      ? result
      : default!;
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

  public async Task ClearInput()
  {
    _value = default;
    _displayClearButton = false;
    await SetValueAsync(default!);
    if (OnChange.HasDelegate)
      await OnChange.InvokeAsync();
  }

  public async Task IncrementValue()
  {
    var x = _value + Increment;
    await CommitValueAsync(_value);
  }

  public async Task DecrementValue()
  {
    var x = _value - Increment;
    await CommitValueAsync(_value);
  }

  private async Task CommitValueAsync(TValue value)
  {
    switch (InputBindMode)
    {
      case ZbInputBindMode.OnInput:
        await SetValueAsync(value);
        await OnInput.InvokeAsync(value);
        break;

      case ZbInputBindMode.InputDelay:
        await DebounceAsync(async () =>
        {
          await SetValueAsync(value);
          await OnInput.InvokeAsync(value);
        });
        break;
    }
  }

}