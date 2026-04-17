using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using System.Linq.Expressions;
using static System.Net.Mime.MediaTypeNames;

namespace ZenBoxUI.Blazor.Common
{
  public abstract class InputBaseComponent<T> : ComponentBase
  {
    [Parameter] public string Id { get; set; } = Guid.NewGuid().ToString();

    [Parameter] public string InputId { get; set; } = Guid.NewGuid().ToString();

    [Parameter] public string? NullText { get; set; }

    [Parameter] public string? InputCssClass { get; set; }

    [Parameter] public string? CssClass { get; set; }

    [Parameter] public bool ClearButton { get; set; }

    [Parameter] public bool? ValidationEnabled { get; set; } = true;

    [Parameter] public bool Disabled { get; set; }

    [Parameter] public string? Label { get; set; }

    [Parameter] public int? InputDelay { get; set; } = 300;

    [Parameter] public ZbInputBindMode InputBindMode { get; set; } = ZbInputBindMode.OnChange;

    [Parameter] public ZbClearButtonValueBehavior ClearBehavior { get; set; } = ZbClearButtonValueBehavior.Default;

    [Parameter] public EventCallback OnInput { get; set; }

    [Parameter] public EventCallback OnChange { get; set; }

    [Parameter] public EventCallback OnFocus { get; set; }

    [Parameter] public EventCallback OnBlur { get; set; }

    [Parameter] public Expression<Func<T?>>? ValueExpression { get; set; }

    [CascadingParameter] public EditContext? EditContext { get; set; }

    internal FieldIdentifier FieldIdentifier;

    protected CancellationTokenSource? DebounceCts;
    protected string? PendingValue;

  }
}
