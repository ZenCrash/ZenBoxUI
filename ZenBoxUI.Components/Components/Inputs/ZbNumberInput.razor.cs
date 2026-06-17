using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Primitives;
using Microsoft.Extensions.Primitives;
using Microsoft.JSInterop;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Globalization;
using System.Linq;
using System.Net.Mime;
using System.Text.RegularExpressions;
using System.Threading.Channels;
using System.Threading.Tasks;
using ZenBoxUI.Blazor.Common;

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
      var zero = targetType switch
      {
        _ when targetType == typeof(int) => 0,
        _ when targetType == typeof(long) => 0L,
        _ when targetType == typeof(short) => (short)0,
        _ when targetType == typeof(byte) => (byte)0,
        _ when targetType == typeof(uint) => 0u,
        _ when targetType == typeof(ulong) => 0ul,
        _ when targetType == typeof(float) => 0f,
        _ when targetType == typeof(double) => 0d,
        _ when targetType == typeof(decimal) => 0m,
        _ when targetType == typeof(sbyte) => (sbyte)0,
        _ when targetType == typeof(ushort) => (ushort)0,
        _ when targetType == typeof(Half) => (Half)0,
        _ when targetType == typeof(System.Numerics.BigInteger) => new System.Numerics.BigInteger(0),
        _ => Activator.CreateInstance(targetType)!
      };

      newValue = (TValue)zero;
    }
    else
    {
      object? parsed = null;

      //Signed Integers
      if (targetType == typeof(int))
      {
        if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var d))
        {
          if (d > int.MaxValue) parsed = int.MaxValue;
          else if (d < int.MinValue) parsed = int.MinValue;
          else parsed = (int)d;
        }
      }
      else if (targetType == typeof(long))
      {
        if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var d))
        {
          if (d > long.MaxValue) parsed = long.MaxValue;
          else if (d < long.MinValue) parsed = long.MinValue;
          else parsed = (long)d;
        }
      }
      else if (targetType == typeof(short))
      {
        if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var d))
        {
          if (d > short.MaxValue) parsed = short.MaxValue;
          else if (d < short.MinValue) parsed = short.MinValue;
          else parsed = (short)d;
        }
      }
      else if (targetType == typeof(sbyte))
      {
        if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var d))
        {
          if (d > sbyte.MaxValue) parsed = sbyte.MaxValue;
          else if (d < sbyte.MinValue) parsed = sbyte.MinValue;
          else parsed = (sbyte)d;
        }
      }

      //Unsigned Integers
      else if (targetType == typeof(byte))
      {
        if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var d))
        {
          if (d > byte.MaxValue) parsed = byte.MaxValue;
          else if (d < byte.MinValue) parsed = byte.MinValue; // always 0
          else parsed = (byte)d;
        }
      }
      else if (targetType == typeof(ushort))
      {
        if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var d))
        {
          if (d > ushort.MaxValue) parsed = ushort.MaxValue;
          else if (d < ushort.MinValue) parsed = ushort.MinValue;
          else parsed = (ushort)d;
        }
      }
      else if (targetType == typeof(uint))
      {
        if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var d))
        {
          if (d > uint.MaxValue) parsed = uint.MaxValue;
          else if (d < uint.MinValue) parsed = uint.MinValue;
          else parsed = (uint)d;
        }
      }
      else if (targetType == typeof(ulong))
      {
        if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var d))
        {
          if (d < 0) parsed = 0;
          else if (d > ulong.MaxValue) parsed = ulong.MaxValue;
          else parsed = (ulong)d;
        }
      }

      //Floating Point
      else if (targetType == typeof(float))
      {
        if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var d))
        {
          if (d > float.MaxValue) parsed = float.MaxValue;
          else if (d < float.MinValue) parsed = float.MinValue;
          else parsed = (float)d;
        }
      }
      else if (targetType == typeof(double))
      {
        if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var d))
        {
          parsed = d;
        }
      }
      else if (targetType == typeof(decimal))
      {
        if (decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var m))
        {
          parsed = m;
        }
        else if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var d))
        {
          if (d > (double)decimal.MaxValue) parsed = decimal.MaxValue;
          else if (d < (double)decimal.MinValue) parsed = decimal.MinValue;
          else parsed = (decimal)d;
        }
      }
      else if (targetType == typeof(Half))
      {
        if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var d))
        {
          if (d > (double)Half.MaxValue) parsed = Half.MaxValue;
          else if (d < (double)Half.MinValue) parsed = Half.MinValue;
          else parsed = (Half)d;
        }
      }

      //Big Integer
      else if (targetType == typeof(System.Numerics.BigInteger))
      {
        if (System.Numerics.BigInteger.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var big))
        {
          parsed = big;
        }
        else if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var d))
        {
          parsed = new System.Numerics.BigInteger(d);
        }
      }

      else
      {
        parsed = default(TValue);
      }

      if (parsed != null)
      {
        newValue = (TValue)Convert.ChangeType(parsed, targetType, CultureInfo.InvariantCulture);
      }
    }

    await SetValueAsync(newValue);

    if (OnInput.HasDelegate)
      await OnInput.InvokeAsync();
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
    //throw new NotImplementedException("IncrementValue is not implemented yet.");
  }

  private async Task DecrementValue()
  {
    //throw new NotImplementedException("DecrementValue is not implemented yet.");
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