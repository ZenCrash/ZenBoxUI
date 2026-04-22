using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Linq.Expressions;
using System.Numerics;
using ZenBoxUI.Blazor.Common;

namespace ZenBoxUI.Blazor
{
  public class ZbNumberInput<T> : InputBaseComponent<T?>
  {
    [Parameter]
    public T? Value
    {
      get => base.Value;
      set => base.Value = value;
    }

    [Parameter]
    public EventCallback<T?> ValueChanged
    {
      get => base.ValueChanged;
      set => base.ValueChanged = value;
    }

    [Parameter]
    public Expression<Func<T?>>? ValueExpression
    {
      get => base.ValueExpression;
      set => base.ValueExpression = value;
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
      new InputBuilder<T?>(this, InputType.Number)
        .BuildRenderTree(builder);
    }
  }
}

