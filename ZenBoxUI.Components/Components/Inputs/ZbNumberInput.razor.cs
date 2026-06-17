using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Globalization;
using ZenBoxUI.Blazor.Common;
using static System.Net.Mime.MediaTypeNames;

namespace ZenBoxUI.Blazor;

public partial class ZbNumberInput<TValue> : ZbInputBase<TValue>
{
  [Parameter] public decimal? MinValue { get; set; }
  [Parameter] public decimal? MaxValue { get; set; }
  [Parameter] public decimal? Increment { get; set; }
  private string? _displayValue { get; set; }

  protected override void OnParametersSet()
  {
    base.OnParametersSet();
    _displayValue = (Value != null) ? Value.ToString() : string.Empty;
  }

  /// <summary>
  /// Disable the increment and decrement buttons.
  /// </summary>
  [Parameter]
  public bool DisableIncrementButtons { get; set; }

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
  // Events
  // =========================

  async Task SetValue(string? value)
  {
    TValue newValue = default!;
    var targetType = Nullable.GetUnderlyingType(typeof(TValue)) ?? typeof(TValue);

    if (string.IsNullOrWhiteSpace(value))
    {
      decimal fallback = MinValue ?? 0m;
      newValue = ConvertFromDecimal(fallback);
    }
    else
    {
      if (!decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var d))
      {
        newValue = default!;
      }
      else
      {
        d = Clamp(d);

        object? parsed = targetType switch
        {
          // Signed ints
          _ when targetType == typeof(int) => 
            d > int.MaxValue ? int.MaxValue :
               d < int.MinValue ? int.MinValue :
               (int)d,

          _ when targetType == typeof(long)
            => d > long.MaxValue ? long.MaxValue :
               d < long.MinValue ? long.MinValue :
               (long)d,

          _ when targetType == typeof(short)
            => d > short.MaxValue ? short.MaxValue :
               d < short.MinValue ? short.MinValue :
               (short)d,

          _ when targetType == typeof(sbyte)
            => d > sbyte.MaxValue ? sbyte.MaxValue :
               d < sbyte.MinValue ? sbyte.MinValue :
               (sbyte)d,

          // Unsigned
          _ when targetType == typeof(byte)
            => d > byte.MaxValue ? byte.MaxValue :
               d < byte.MinValue ? byte.MinValue :
               (byte)d,

          _ when targetType == typeof(ushort)
            => d > ushort.MaxValue ? ushort.MaxValue :
               d < ushort.MinValue ? ushort.MinValue :
               (ushort)d,

          _ when targetType == typeof(uint)
            => d > uint.MaxValue ? uint.MaxValue :
               d < uint.MinValue ? uint.MinValue :
               (uint)d,

          _ when targetType == typeof(ulong)
            => d < 0 ? 0 :
               d > ulong.MaxValue ? ulong.MaxValue :
               (ulong)d,

          // Floating
          _ when targetType == typeof(float)
          => (float)d > float.MaxValue ? float.MaxValue : 
             (float)d < float.MinValue ? float.MinValue : 
             (float)d,

          _ when targetType == typeof(double)
            => (double)d > double.MaxValue ? double.MaxValue :
               (double)d < double.MinValue ? double.MinValue :
               (double)d,

          _ when targetType == typeof(decimal)
            => d > decimal.MaxValue ? decimal.MaxValue :
               d < decimal.MinValue ? decimal.MinValue :
               d,

          _ when targetType == typeof(Half)
            => d > (decimal)Half.MaxValue ? Half.MaxValue :
               d < (decimal)Half.MinValue ? Half.MinValue :
               (Half)d,

          _ when targetType == typeof(System.Numerics.BigInteger)
            => new System.Numerics.BigInteger(d),

          _ => Convert.ChangeType(d, targetType, CultureInfo.InvariantCulture)
        };

        newValue = (TValue)parsed!;
      }
    }

    await SetValueAsync(newValue);

    if (OnInput.HasDelegate)
      await OnInput.InvokeAsync();
  }

  private decimal Clamp(decimal value)
  {
    if (MinValue.HasValue && value < MinValue.Value)
      value = MinValue.Value;

    if (MaxValue.HasValue && value > MaxValue.Value)
      value = MaxValue.Value;

    return value;
  }

  // =========================
  // Events
  // =========================

  private async Task HandleInput(ChangeEventArgs e)
  {
    //DisplayValue = value;
    //await InternalValueChanged(value);
  }

  private async Task HandleChange(ChangeEventArgs e)
  {
    //throw new NotImplementedException("HandleChange is not implemented yet.");
  }

  private async Task HandleFocus(FocusEventArgs e)
  {
    //throw new NotImplementedException("HandleFocus is not implemented yet.");
  }

  private async Task HandleDeselect(FocusEventArgs e)
  {
    //throw new NotImplementedException("HandleDeselect is not implemented yet.");
  }

  private async Task IncrementValue()
  {
    if (Value is null || Disabled || ReadOnly)
      return;

    var current = Convert.ToDecimal(Value ?? default(TValue));
    var newValue = current + (Increment ?? 1m);
    if (newValue > MaxValue)
      newValue = MaxValue.Value;

    var resultValue = ConvertFromDecimal(newValue);
    await SetValueAsync(resultValue);

    if (OnChange.HasDelegate)
      await OnChange.InvokeAsync();
  }

  private async Task DecrementValue()
  {
    if (Value is null || Disabled || ReadOnly)
      return;

    var current = Convert.ToDecimal(Value ?? default(TValue));
    var newValue = current - (Increment ?? 1m);
    if (newValue < MinValue)
      newValue = MinValue.Value;

    var resultValue = ConvertFromDecimal(newValue);
    await SetValueAsync(resultValue);

    if (OnChange.HasDelegate)
      await OnChange.InvokeAsync();
  }

  private TValue ConvertFromDecimal(decimal value)
  {
    var targetType = Nullable.GetUnderlyingType(typeof(TValue)) ?? typeof(TValue);

    object result = targetType switch
    {
      _ when targetType == typeof(int) => (int)value,
      _ when targetType == typeof(long) => (long)value,
      _ when targetType == typeof(short) => (short)value,
      _ when targetType == typeof(sbyte) => (sbyte)value,

      _ when targetType == typeof(byte) => (byte)value,
      _ when targetType == typeof(ushort) => (ushort)value,
      _ when targetType == typeof(uint) => (uint)value,
      _ when targetType == typeof(ulong) => value < 0 ? 0ul : (ulong)value,

      _ when targetType == typeof(float) => (float)value,
      _ when targetType == typeof(double) => (double)value,
      _ when targetType == typeof(decimal) => value,
      _ when targetType == typeof(Half) => (Half)value,

      _ when targetType == typeof(System.Numerics.BigInteger)
        => new System.Numerics.BigInteger(value),

      _ => Convert.ChangeType(value, targetType, CultureInfo.InvariantCulture)
    };

    return (TValue)result;
  }

  private async Task ClearInput()
  {
    _displayValue = string.Empty;
    _displayClearButton = false;
    await SetValueAsync(default!);
    if (OnChange.HasDelegate)
      await OnChange.InvokeAsync();
  }
}