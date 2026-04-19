using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Linq.Expressions;
using ZenBoxUI.Blazor.Common;

namespace ZenBoxUI.Blazor.Components.InputDataEditor.InputNumber
{
  public class ZbNumberInput : InputBaseComponent<int?>
  {
    [Parameter]
    public int? Value
    {
      get => base.Value;
      set => base.Value = value;
    }

    [Parameter]
    public EventCallback<int?> ValueChanged
    {
      get => base.ValueChanged;
      set => base.ValueChanged = value;
    }
    [Parameter]
    public Expression<Func<int?>>? ValueExpression
    {
      get => base.ValueExpression;
      set => base.ValueExpression = value;
    }
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
      new InputBuilder<int?>(this, InputType.Number).BuildRenderTree(builder);
    }
  }
}
