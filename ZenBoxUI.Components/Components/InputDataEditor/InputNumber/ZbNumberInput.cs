using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Linq.Expressions;
using ZenBoxUI.Blazor.Common;

namespace ZenBoxUI.Blazor.Components.InputDataEditor.InputNumber
{
  public class ZbNumberInput
  {
    [Parameter]
    public int? Value { get; set; }

    [Parameter]
    public EventCallback<int?> ValueChanged { get; set; }

    [Parameter]
    public Expression<Func<int?>>? ValueExpression { get; set; }

    protected void BuildRenderTree(RenderTreeBuilder builder)
    {
      //new InputNumberBuilder(this).BuildRenderTree(builder);
    }
  }
}
