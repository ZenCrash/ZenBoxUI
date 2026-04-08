using Microsoft.Extensions.DependencyInjection;

namespace ZenBoxUI.Blazor
{
  public static class ZenBoxUiServicesCollectionExtension
  {
    public static IServiceCollection AddZenBoxUiBlazor(this IServiceCollection services)
    {
      services.AddScoped<IZBoxToastService, ZBoxToastService>();

      return services;
    }
  }
}
