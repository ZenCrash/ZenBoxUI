namespace ZenBoxUI.Blazor
{
  public interface IZBoxToastOptions
  {
    public ToastOptions Primary { get; }
    public ToastOptions Secondary { get; }
    public ToastOptions Success { get; }
    public ToastOptions Danger { get; }
    public ToastOptions Warning { get; }
    public ToastOptions Info { get; }
  }

  public class ZBoxToastOptions : IZBoxToastOptions
  {
    public ToastOptions Primary => new()
    {
      RenderStyle = ToastRenderStyle.Primary,
      DisplayTime = TimeSpan.FromSeconds(5),
      ShowCloseButton = true,
    };

    public ToastOptions Secondary => new()
    {
      RenderStyle = ToastRenderStyle.Secondary,
      DisplayTime = TimeSpan.FromSeconds(5),
      ShowCloseButton = true,
    };

    public ToastOptions Success => new()
    {
      RenderStyle = ToastRenderStyle.Success,
      DisplayTime = TimeSpan.FromSeconds(5),
      ShowCloseButton = true,
    };

    public ToastOptions Danger => new()
    {
      RenderStyle = ToastRenderStyle.Danger,
      DisplayTime = TimeSpan.Zero,
      ShowCloseButton = true,
    };

    public ToastOptions Warning => new()
    {
      RenderStyle = ToastRenderStyle.Warning,
      DisplayTime = TimeSpan.Zero,
      ShowCloseButton = true,
    };

    public ToastOptions Info => new()
    {
      RenderStyle = ToastRenderStyle.Info,
      DisplayTime = TimeSpan.FromSeconds(5),
      ShowCloseButton = true,
    };
  }
}
