using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Linq.Expressions;

namespace ZenBoxUI.Blazor.Common
{
  /// <summary>
  /// Base class for all ZenBox input components. Provides common binding, validation,
  /// and EditForm integration logic for derived input controls.
  /// </summary>
  /// <typeparam name="TValue">The type of the input value.</typeparam>
  public abstract class ZbInputBase<TValue> : ComponentBase
  {
    /// <summary>
    /// Id for the wrapper element.
    /// </summary>
    [Parameter] public string Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Id for the input element.
    /// </summary>
    [Parameter] public string InputId { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Placeholder text displayed when the input is empty.
    /// </summary>
    [Parameter] public string? NullText { get; set; }

    /// <summary>
    /// CSS class for the wrapper element.
    /// </summary>
    [Parameter] public string? CssClass { get; set; }

    /// <summary>
    /// Gets or sets the CSS class or classes to apply to the input element.
    /// </summary>
    [Parameter] public string? InputCssClass { get; set; }

    /// <summary>
    /// Disables the input component, preventing user interaction.
    /// </summary>
    [Parameter] public bool Disabled { get; set; }

    /// <summary>
    /// Enables a clear button that allows users to quickly clear the input field.
    /// </summary>
    [Parameter] public bool ClearButton { get; set; }

    /// <summary>
    /// Delay in milliseconds when InputBindMode is set to InputDelay. <see cref="InputBindMode"/>:
    /// </summary>
    [Parameter] public int InputDelay { get; set; } = 300;

    /// <summary>
    /// Binding mode that determines when the input value is updated.
    /// </summary>
    [Parameter] public ZbInputBindMode InputBindMode { get; set; } = ZbInputBindMode.OnChange;

    /// <summary>
    /// Event callback that is invoked when the input value changes. The timing of this event depends on the value of <see cref="InputBindMode"/>.
    /// </summary>
    [Parameter] public EventCallback<TValue?> OnInput { get; set; }

    /// <summary>
    /// Gets or sets the callback that is invoked when the value changes.
    /// </summary>
    [Parameter] public EventCallback<TValue?> OnChange { get; set; }

    /// <summary>
    /// Gets or sets the callback that is invoked when the component receives focus.
    /// </summary>
    [Parameter] public EventCallback<TValue?> OnFocus { get; set; }

    /// <summary>
    /// Gets or sets the callback that is invoked when the component is deselected.
    /// </summary>
    [Parameter] public EventCallback<TValue?> OnDeselect { get; set; }

    /* Value */

    /// <summary>
    /// Gets or sets the current value.
    /// </summary>
    [Parameter] public TValue? Value { get; set; }

    /// <summary>
    /// Gets or sets the callback that is invoked when the value changes.
    /// </summary>
    [Parameter] public EventCallback<TValue?> ValueChanged { get; set; }

    /// <summary>
    /// Gets or sets an expression that identifies the bound value for the component.
    /// </summary>
    [Parameter] public Expression<Func<TValue?>>? ValueExpression { get; set; }

    /* Edit Context */

    /// <summary>
    /// Gets or sets the <see cref="EditContext"/> for the current editing scope.
    /// This is an inherited cascading parameter. No need to set this explicitly when using the component within an <see cref="EditForm"/>.
    /// </summary>
    /// <remarks>This property is typically supplied as a cascading parameter in Blazor forms to provide
    /// validation and editing context for form components. Setting this property enables components to participate in
    /// form validation and state tracking.</remarks>
    [CascadingParameter] public EditContext? EditContext { get; set; }

    /* Internal States */

    protected TValue? _value;

    protected bool _displayClearButton;

    protected FieldIdentifier FieldIdentifier;

    /* Other */

    protected override void OnParametersSet()
    {
      _value = Value;
      _displayClearButton = ClearButton && Value is not null && !Disabled;

      if (ValueExpression is not null)
        FieldIdentifier = FieldIdentifier.Create(ValueExpression);
    }

    internal async Task SetValueAsync(TValue value)
    {
      await ValueChanged.InvokeAsync(value);
      EditContext?.NotifyFieldChanged(FieldIdentifier);
    }

    internal bool HasError()
    {
      return EditContext?.GetValidationMessages(FieldIdentifier).Any() == true;
    }

    // =========================
    // DEBOUNCE
    // =========================

    private CancellationTokenSource? _cts = new CancellationTokenSource();

    protected async Task DebounceAsync(Func<Task> action)
    {
      await _cts?.CancelAsync()!;
      _cts = new CancellationTokenSource();
      var token = _cts.Token;

      try
      {
        await Task.Delay(InputDelay, token);

        if (!token.IsCancellationRequested)
          await action();
      }
      catch (TaskCanceledException)
      {
        // expected
      }
    }
  }
}
