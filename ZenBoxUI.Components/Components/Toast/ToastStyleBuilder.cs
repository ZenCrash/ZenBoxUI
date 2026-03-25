using Microsoft.AspNetCore.Components;

namespace ZenBoxUI.Blazor
{
  internal static class ToastStyleBuilder
  {
    private static string[] SuccessStyleColors => ["#107C10", "#E7F2E6"];
    private static string[] DangerStyleColors => ["#C50F1F", "#FCEAE8"];
    private static string[] WarningStyleColors => ["#FFC107", "#FFF8E1"];
    private static string[] InfoStyleColors => ["#0D6EFD", "#E7EFF9"];
    private static string[] LightStyleColors => ["#CCCCCC", "#FFFFFF"];
    private static string[] DarkStyleColors => ["#212529", "#E9ECEF"];

    internal static string GetToastContainerStyles(ToastAlignment alignment)
    {
      //var styles = new List<string>();
      //styles.Add(GetToastPositionAndAlignmentCss(alignment));
      //return string.Join(" ", styles);

      return GetPositionAndAlignmentCss(alignment);
    }
    internal static string GetToastStyles(string? maxWidth, ToastRenderStyle renderStyle, string width, int lineHeight)
    {
      var styles = new List<string>();
      styles.Add($"--zbox-width: {width};");
      styles.Add($"--zbox-max-width: {maxWidth ?? width};");
      styles.Add(GetRenderStyleCss(renderStyle));
      styles.Add($"--toast-line-height: {lineHeight};");
      return string.Join(" ", styles);
    }

    private static string GetPositionAndAlignmentCss(ToastAlignment alignment) => alignment switch
    {
      ToastAlignment.TopLeft => "zbox-top zbox-left",
      ToastAlignment.TopCenter => "zbox-top zbox-center",
      ToastAlignment.TopRight => "zbox-top zbox-right",
      ToastAlignment.MiddleLeft => "zbox-middle zbox-left",
      ToastAlignment.MiddleCenter => "zbox-middle zbox-center",
      ToastAlignment.MiddleRight => "zbox-middle zbox-right",
      ToastAlignment.BottomLeft => "zbox-bottom zbox-left",
      ToastAlignment.BottomCenter => "zbox-bottom zbox-center",
      ToastAlignment.BottomRight => "zbox-bottom zbox-right",
      _ => "zbox-bottom zbox-right"
    };



    internal static string GetRenderStyleCss(ToastRenderStyle renderStyle) => renderStyle switch
    {
      ToastRenderStyle.Success => $"--zbox-primary: {SuccessStyleColors[0]};" + $"--zbox-secondary: {SuccessStyleColors[1]};" + $"--zbox-title: {GetContrastColor(SuccessStyleColors[0])};" + $"--zbox-text: {GetContrastColor(SuccessStyleColors[1])};" + $"--zbox-toast-icon-filter: 100%;",
      ToastRenderStyle.Danger => $"--zbox-primary: {DangerStyleColors[0]};" + $"--zbox-secondary: {DangerStyleColors[1]};" + $"--zbox-title: {GetContrastColor(DangerStyleColors[0])};" + $"--zbox-text: {GetContrastColor(DangerStyleColors[1])};" + $"--zbox-toast-icon-filter: 100%;",
      ToastRenderStyle.Warning => $"--zbox-primary: {WarningStyleColors[0]};" + $"--zbox-secondary: {WarningStyleColors[1]};" + $"--zbox-title: {GetContrastColor(WarningStyleColors[0])};" + $"--zbox-text: {GetContrastColor(WarningStyleColors[1])};" + $"--zbox-toast-icon-filter: 0%;",
      ToastRenderStyle.Info => $"--zbox-primary: {InfoStyleColors[0]};" + $"--zbox-secondary: {InfoStyleColors[1]};" + $"--zbox-title: {GetContrastColor(InfoStyleColors[0])};" + $"--zbox-text: {GetContrastColor(InfoStyleColors[1])};" + $"--zbox-toast-icon-filter: 100%;",
      ToastRenderStyle.Light => $"--zbox-primary: {LightStyleColors[0]};" + $"--zbox-secondary: {LightStyleColors[1]};" + $"--zbox-title: {GetContrastColor(LightStyleColors[0])};" + $"--zbox-text: {GetContrastColor(LightStyleColors[1])};" + $"--zbox-toast-icon-filter: 0%;",
      ToastRenderStyle.Dark => $"--zbox-primary: {DarkStyleColors[0]};" + $"--zbox-secondary: {DarkStyleColors[1]};" + $"--zbox-title: {GetContrastColor(DarkStyleColors[0])};" + $"--zbox-text: {GetContrastColor(DarkStyleColors[1])};" + $"--zbox-toast-icon-filter: 100%;",
      _ => $"--zbox-primary: {InfoStyleColors[0]};" + $"--zbox-secondary: {InfoStyleColors[1]};" + $"--zbox-title: {GetContrastColor(InfoStyleColors[0])};" + $"--zbox-text: {GetContrastColor(InfoStyleColors[1])};" + $"--zbox-toast-icon-filter: 0%;"
    };

    private static string GetContrastColor(string hexColor)
    {
      if (string.IsNullOrWhiteSpace(hexColor))
        throw new ArgumentException("Invalid color value.");
      hexColor = hexColor.TrimStart('#');
      if (hexColor.Length != 6)
        throw new ArgumentException("Hex color must be 6 characters long.");

      int r = Convert.ToInt32(hexColor.Substring(0, 2), 16);
      int g = Convert.ToInt32(hexColor.Substring(2, 2), 16);
      int b = Convert.ToInt32(hexColor.Substring(4, 2), 16);

      // Calculate brightness
      double brightness = (r * 0.299) + (g * 0.587) + (b * 0.114);
      // Threshold 128
      return brightness < 128 ? "#FFFFFF" : "#000000";
    }

    internal static string GetIconPath(ToastRenderStyle renderStyle) => renderStyle switch
    {
    ToastRenderStyle.Success => "_content/ZenBoxUI.Blazor/svg/check-circle-fill.svg",
    ToastRenderStyle.Danger => "_content/ZenBoxUI.Blazor/svg/x-circle-fill.svg",
    ToastRenderStyle.Warning => "_content/ZenBoxUI.Blazor/svg/exclamation-triangle-fill.svg",
    ToastRenderStyle.Info => "_content/ZenBoxUI.Blazor/svg/info-circle-fill.svg",
    ToastRenderStyle.Light => "_content/ZenBoxUI.Blazor/svg/info-circle-fill.svg",
    ToastRenderStyle.Dark => "_content/ZenBoxUI.Blazor/svg/info-circle-fill.svg",
    _ => "_content/ZenBoxUI.Blazor/svg/info-circle-fill.svg"
  };
}
}
