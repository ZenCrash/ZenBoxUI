using ZenBoxUI.Blazor;

namespace ZenBoxUI.BlazorTestUI.Components.Pages.Components.InputText.Usecases
{
  public class InputTextEventsToasts(IZBoxToastService ToastService)
  {
    public void TriggerOnTextChange()
    {
      ToastService.ShowToast(new ToastOptions
      {
        Title = "Text - OnChange" + " " + Guid.NewGuid(),
        Text = "I got trigger On Change",
        RenderTheme = ToastRenderTheme.Basic,
        RenderStyle = ToastRenderStyle.Warning,
        DisplayTime = 2000
      });
    }
    public void TriggerOnInput()
    {
      ToastService.ShowToast(new ToastOptions
      {
        Title = "Text - OnInput" + " " + Guid.NewGuid(),
        Text = "I got trigger On Input",
        RenderTheme = ToastRenderTheme.Basic,
        RenderStyle = ToastRenderStyle.Info,
        DisplayTime = 2000
      });
    }
    public void TriggerOnChange()
    {
      ToastService.ShowToast(new ToastOptions
      {
        Title = "Text - OnChange" + " " + Guid.NewGuid(),
        Text = "I got trigger On Change",
        RenderTheme = ToastRenderTheme.Basic,
        RenderStyle = ToastRenderStyle.Warning,
        DisplayTime = 2000
      });
    }


    public void TriggerOnFocus()
    {
      ToastService.ShowToast(new ToastOptions
      {
        Title = "Text - OnFocus" + " " + Guid.NewGuid(),
        Text = "I got trigger On Focus",
        RenderTheme = ToastRenderTheme.Basic,
        RenderStyle = ToastRenderStyle.Success,
        DisplayTime = 2000
      });
    }

    public void TriggerOnBlur()
    {
      ToastService.ShowToast(new ToastOptions
      {
        Title = "Text - OnBlur" + " " + Guid.NewGuid(),
        Text = "I got trigger On Blur (Deselect)",
        RenderTheme = ToastRenderTheme.Basic,
        RenderStyle = ToastRenderStyle.Danger,
        DisplayTime = 2000
      });
    }
  }
}
