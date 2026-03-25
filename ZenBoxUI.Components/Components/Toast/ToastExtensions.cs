namespace ZenBoxUI.Blazor
{
  public static class ToastExtensions
  {
    public static ToastOptions WithId(this ToastOptions options, string id)
    {
      options.Id = id;
      return options;
    }

    public static ToastOptions WithDisplayTime(this ToastOptions options, int displayTime)
    {
      options.DisplayTime = displayTime;
      return options;
    }

    public static ToastOptions WithTitle(this ToastOptions options, string title)
    {
      options.Title = title;
      return options;
    }

    public static ToastOptions WithText(this ToastOptions options, string text)
    {
      options.Text = text;
      return options;
    }

    public static ToastOptions WithCssClass(this ToastOptions options, string cssClass)
    {
      options.CssClass = cssClass;
      return options;
    }

    public static ToastOptions WithIconCssClass(this ToastOptions options, string iconCssClass)
    {
      options.IconCssClass = iconCssClass;
      return options;
    }

    public static ToastOptions WithMaxHeight(this ToastOptions options, int maxLineHeight)
    {
      options.MaxLineHeight = maxLineHeight;
      return options;
    }

    public static ToastOptions WithShowCloseButton(this ToastOptions options, bool showCloseButton)
    {
      options.ShowCloseButton = showCloseButton;
      return options;
    }

    public static ToastOptions WithStyle(this ToastOptions options, ToastRenderStyle style)
    {
      options.RenderStyle = style;
      return options;
    }

  }
}
