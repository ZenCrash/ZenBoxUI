using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using System.Globalization;
using System.Linq.Expressions;

namespace ZenBoxUI.Blazor.Common
{
  public abstract class InputBaseComponent<T> : ZbComponentBase
  {


    [Parameter] public string InputId { get; set; } = Guid.NewGuid().ToString();

    [Parameter] public string? NullText { get; set; }

    [Parameter] public string? InputCssClass { get; set; }

    [Parameter] public string? CssClass { get; set; }

    [Parameter] public bool ClearButton { get; set; }

    [Parameter] public bool? ValidationEnabled { get; set; } = true;

    [Parameter] public bool Disabled { get; set; }

    [Parameter] public int? InputDelay { get; set; } = 300;

    [Parameter] public ZbInputBindMode InputBindMode { get; set; } = ZbInputBindMode.OnChange;

    [Parameter] public EventCallback OnInput { get; set; }

    [Parameter] public EventCallback OnChange { get; set; }

    [Parameter] public EventCallback OnFocus { get; set; }

    [Parameter] public EventCallback OnBlur { get; set; }
    [CascadingParameter] public EditContext? EditContext { get; set; }

    internal Expression<Func<T?>>? ValueExpression { get; set; }

    internal T? Value { get; set; }
    public EventCallback<T?> ValueChanged { get; set; }
    protected FieldIdentifier FieldIdentifier { get; set; }
    protected CancellationTokenSource? DebounceCts;
    protected T? PendingValue;

    internal void HandleClearButton()
    {
      ValueChanged.InvokeAsync(default(T));

      if (EditContext is not null)
        EditContext.NotifyFieldChanged(FieldIdentifier);

      if (OnInput.HasDelegate)
        OnInput.InvokeAsync();

      if (OnChange.HasDelegate)
        OnChange.InvokeAsync();
    }

    internal async Task HandleFocus(FocusEventArgs e)
    {
      if (OnFocus.HasDelegate)
        await OnFocus.InvokeAsync();
    }

    internal async Task HandleBlur(FocusEventArgs e)
    {
      if (OnBlur.HasDelegate)
        await OnBlur.InvokeAsync();
    }

    internal async Task HandleInput(ChangeEventArgs e)
    {
      if (InputBindMode == ZbInputBindMode.OnInput)
      {
        Value = BindConverter.TryConvertTo<T>(e.Value, CultureInfo.CurrentCulture, out var result)
          ? result
          : default!;
        await ValueChanged.InvokeAsync(Value);

        if (EditContext is not null)
          EditContext.NotifyFieldChanged(FieldIdentifier);
      }
      else if (InputBindMode == ZbInputBindMode.InputDelay)
      {
        PendingValue = BindConverter.TryConvertTo<T>(e.Value, CultureInfo.CurrentCulture, out var result)
          ? result
          : default!;

        DebounceCts?.Cancel();
        DebounceCts = new CancellationTokenSource();
        var token = DebounceCts.Token;

        var delay = InputDelay ?? 0;

        try
        {
          await Task.Delay(delay, token);

          if (token.IsCancellationRequested)
            return;

          Value = PendingValue;

          await ValueChanged.InvokeAsync(Value);

          if (EditContext is not null)
            EditContext.NotifyFieldChanged(FieldIdentifier);
        }
        catch (TaskCanceledException)
        {

        }
      }

      if (OnInput.HasDelegate)
        await OnInput.InvokeAsync();
    }

    internal async Task HandleChange(ChangeEventArgs e)
    {
      if (InputBindMode == ZbInputBindMode.OnChange)
      {
        Value = BindConverter.TryConvertTo<T>(e.Value, CultureInfo.CurrentCulture, out var result)
          ? result
          : default!;

        await ValueChanged.InvokeAsync(Value);

        if (EditContext is not null)
          EditContext.NotifyFieldChanged(FieldIdentifier);
      }

      if (OnChange.HasDelegate)
        await OnChange.InvokeAsync();
    }

    protected override void OnParametersSet()
    {
      if (ValueExpression is not null)
      {
        FieldIdentifier = FieldIdentifier.Create(ValueExpression);
      }
    }
  }
}
