using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ZenBoxUI.Blazor.Toast
{
  internal static class ToastStyleBuilder
  {
    private static string[] PrimaryStyleColors => ["#0D6EFD", "#E7EFF9"];
    private static string[] SecondaryStyleColors => ["#6C757D", "#F1F3F5"];
    private static string[] SuccessStyleColors => ["#107C10", "#E7F2E6"];
    private static string[] DangerStyleColors => ["#C50F1F", "#FCEAE8"];
    private static string[] WarningStyleColors => ["#FFC107", "#FFF8E1"];
    private static string[] InfoStyleColors => ["#0DCAF0", "#E7F8FD"];
    private static string[] LightStyleColors => ["#F8F9FA", "#FFFFFF"];
    private static string[] DarkStyleColors => ["#212529", "#E9ECEF"];

    internal static string GetToastContainerStyles(ToastAlignment alignment)
    {
      //var styles = new List<string>();
      //styles.Add(GetToastPositionAndAlignmentCss(alignment));
      //return string.Join(" ", styles);

      return GetToastPositionAndAlignmentCss(alignment);
    }
    internal static string GetToastStyles(string? maxWidth, ToastRenderStyle renderStyle, string width)
    {
      var styles = new List<string>();
      styles.Add($"--zbox-width: {width};");
      styles.Add($"--zbox-max-width: {maxWidth ?? width};");
      styles.Add(GetRenderStyleCss(renderStyle));
      return string.Join(" ", styles);
    }

    private static string GetToastPositionAndAlignmentCss(ToastAlignment alignment) => alignment switch
    {
      ToastAlignment.TopLeft => "zbox-top zbox-left ",
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
      ToastRenderStyle.Primary => $"--zbox-primary: {PrimaryStyleColors[0]};" + $"--zbox-secondary: {PrimaryStyleColors[1]};" + $"--zbox-title: {GetContrastColor(PrimaryStyleColors[0])};" + $"--zbox-text: {GetContrastColor(PrimaryStyleColors[1])};",
      ToastRenderStyle.Secondary => $"--zbox-primary: {SecondaryStyleColors[0]};" + $"--zbox-secondary: {SecondaryStyleColors[1]};" + $"--zbox-title: {GetContrastColor(SecondaryStyleColors[0])};" + $"--zbox-text: {GetContrastColor(SecondaryStyleColors[1])};",
      ToastRenderStyle.Success => $"--zbox-primary: {SuccessStyleColors[0]};" + $"--zbox-secondary: {SuccessStyleColors[1]};" + $"--zbox-title: {GetContrastColor(SuccessStyleColors[0])};" + $"--zbox-text: {GetContrastColor(SuccessStyleColors[1])};",
      ToastRenderStyle.Danger => $"--zbox-primary: {DangerStyleColors[0]};" + $"--zbox-secondary: {DangerStyleColors[1]};" + $"--zbox-title: {GetContrastColor(DangerStyleColors[0])};" + $"--zbox-text: {GetContrastColor(DangerStyleColors[1])};",
      ToastRenderStyle.Warning => $"--zbox-primary: {WarningStyleColors[0]};" + $"--zbox-secondary: {WarningStyleColors[1]};" + $"--zbox-title: {GetContrastColor(WarningStyleColors[0])};" + $"--zbox-text: {GetContrastColor(WarningStyleColors[1])};",
      ToastRenderStyle.Info => $"--zbox-primary: {InfoStyleColors[0]};" + $"--zbox-secondary: {InfoStyleColors[1]};" + $"--zbox-title: {GetContrastColor(InfoStyleColors[0])};" + $"--zbox-text: {GetContrastColor(InfoStyleColors[1])};",
      ToastRenderStyle.Light => $"--zbox-primary: {LightStyleColors[0]};" + $"--zbox-secondary: {LightStyleColors[1]};" + $"--zbox-title: {GetContrastColor(LightStyleColors[0])};" + $"--zbox-text: {GetContrastColor(LightStyleColors[1])};",
      ToastRenderStyle.Dark => $"--zbox-primary: {DarkStyleColors[0]};" + $"--zbox-secondary: {DarkStyleColors[1]};" + $"--zbox-title: {GetContrastColor(DarkStyleColors[0])};" + $"--zbox-text: {GetContrastColor(DarkStyleColors[1])};",
      _ => $"--zbox-primary: {PrimaryStyleColors[0]};" + $"--zbox-secondary: {PrimaryStyleColors[1]};" + $"--zbox-title: {GetContrastColor(PrimaryStyleColors[0])};" + $"--zbox-text: {GetContrastColor(PrimaryStyleColors[1])};",
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
  }
}
