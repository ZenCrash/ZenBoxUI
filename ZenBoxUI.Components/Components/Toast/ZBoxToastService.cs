namespace ZenBoxUI.Blazor
{
  public interface IZBoxToastService
  {
    event Action<ToastOptions>? OnToastAdded;
    event Action<string>? OnToastRemoved;

    void ShowToast(ToastOptions options);
  }

  public class ZBoxToastService : IZBoxToastService
  {
    public event Action<ToastOptions>? OnToastAdded;
    public event Action<string>? OnToastRemoved;

    public void ShowToast(ToastOptions options)
    {
      OnToastAdded?.Invoke(options);
    }
  }
}
