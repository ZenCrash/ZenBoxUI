using Microsoft.AspNetCore.Components;

namespace ZenBoxUI.Blazor.Common
{
  public class ZbComponentBase : ComponentBase
  {
    [Parameter] public string Id { get; set; } = Guid.NewGuid().ToString();
  }
}
