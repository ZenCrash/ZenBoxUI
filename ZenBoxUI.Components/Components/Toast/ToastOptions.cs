using Microsoft.AspNetCore.Components;
using System.ComponentModel;

namespace ZenBoxUI.Blazor
{
  public class ToastOptions
  {
    /// <summary>
    /// Unique identifier for the toast.
    /// Default Text: Guid.NewGuid().ToString()
    /// </summary>
    public string Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Speceifies the duration for which the toast should be displayed before automatically disappearing.
    /// </summary>
    [DefaultValue(null)]
    public int? DisplayTime { get; set; }

    /// <summary>
    /// Specifies the title of the toast.
    /// </summary>
    [DefaultValue(null)]
    public string? Title { get; set; }

    /// <summary>
    /// specifies the text content of the toast.
    /// </summary>
    [DefaultValue(null)]
    public string? Text { get; set; }

    /// <summary>
    /// specifies CSS classes to apply to the toast.
    /// </summary>
    [DefaultValue(null)]
    public string? CssClass { get; set; }

    /// <summary>
    /// specifies CSS Icon class to apply next to the title in the toast.
    /// </summary>
    [DefaultValue(null)]
    public string? IconCssClass { get; set; }

    /// <summary>
    /// Specifies the maximum lines of text in the body of the toast.
    /// </summary>
    [DefaultValue(null)]
    public int MaxLineHeight { get; set; } = 3;

    /// <summary>
    /// width of the toast.
    /// </summary>
    [DefaultValue("300px")]
    public string Width { get; set; } = "300px";

    /// <summary>
    /// Ahows a close button on the toast, allowing users to manually dismiss it.
    /// </summary>
    [DefaultValue(true)]
    public bool ShowCloseButton { get; set; } = true;

    /// <summary>
    /// Sets the visual style to use when rendering the toast notification.
    /// </summary>
    [DefaultValue(ToastRenderTheme.Basic)]
    public ToastRenderTheme RenderTheme { get; set; } = ToastRenderTheme.Basic;

    /// <summary>
    /// Sets the visual style to use when rendering the toast notification.
    /// </summary>
    [DefaultValue(ToastRenderStyle.Info)]
    public ToastRenderStyle RenderStyle { get; set; } = ToastRenderStyle.Info;

    /// <summary>
    /// Adds a custom content to the toast body.
    /// </summary>
    [DefaultValue(null)]
    public RenderFragment? CustomContent { get; set; } = null;
  }
}
