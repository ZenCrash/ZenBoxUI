using System.Globalization;
using System.Numerics;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using ZenBoxUI.Blazor.Common;

namespace ZenBoxUI.Blazor;

public partial class ZbNumberInput<TValue> : ZbInputBase<TValue>
{
  [Parameter] public TValue? MinValue { get; set; }
  [Parameter] public TValue? MaxValue { get; set; }
  [Parameter] public TValue? Increment { get; set; }

  private string IncrementDisplayValue => Increment?.ToString() ?? "1";

  /// <summary>
  /// Disable the increment and decrement buttons.
  /// </summary>
  [Parameter] public bool DisableIncrementButtons { get; set; }
  [Parameter] public bool DisabledIncrementHotKeys { get; set; }

  private decimal? Parse(string? value)
    => decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var d)
      ? d
      : null;

  private static decimal ToDecimal(object value)
    => Convert.ToDecimal(value);

  private static TValue FromDecimal(decimal value)
  {
    var targetType = typeof(TValue);

    // unwrap Nullable<T>
    var underlyingType = Nullable.GetUnderlyingType(targetType) ?? targetType;

    var converted = Convert.ChangeType(value, underlyingType);

    return (TValue)converted!;
  }

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
    var parsedValue = Parse(e.Value?.ToString());

    _displayClearButton = ClearButton && parsedValue.HasValue && !Disabled;

    _value = parsedValue.HasValue
      ? FromDecimal(parsedValue.Value)
      : default;

    switch (InputBindMode)
    {
      case ZbInputBindMode.OnInput:
        await ApplyValueAsync();
        break;

      case ZbInputBindMode.InputDelay:
        await DebounceAsync(ApplyValueAsync);
        break;

      case ZbInputBindMode.OnChange:
        if (OnInput.HasDelegate)
          await ApplyValueAsync();
        break;
    }

    async Task ApplyValueAsync()
    {
      await SetValueAsync(_value);
      await OnInput.InvokeAsync(_value);
    }
  }

  private async Task HandleChange(ChangeEventArgs e)
  {
    var parsedValue = Parse(e.Value?.ToString());
    _value = parsedValue.HasValue
      ? FromDecimal(parsedValue.Value)
      : default;


    if (InputBindMode == ZbInputBindMode.OnChange)
      await SetValueAsync(_value);

    if (OnChange.HasDelegate)
      await OnChange.InvokeAsync(_value);
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
    var currentValue = Parse(_value?.ToString());
    var increment = Increment is not null ? ToDecimal(Increment) : 1;
    var result = currentValue.Value + increment;
    if (MaxValue is not null)
    {
      var max = ToDecimal(MaxValue);
      if (result > max)
        result = max;
    }
    _value = FromDecimal(result);
    await CommitValueAsync(_value);
  }

  public async Task DecrementValue()
  {
    var currentValue = Parse(_value?.ToString());
    var increment = Increment is not null ? ToDecimal(Increment) : 1;
    var result = currentValue.Value - increment;
    if (MinValue is not null)
    {
      var min = ToDecimal(MinValue);
      if (result < min)
        result = min;
    }
    _value = FromDecimal(result);
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
      case ZbInputBindMode.OnChange:
        await SetValueAsync(value);
        await OnChange.InvokeAsync(value);
        break;
    }
  }

}