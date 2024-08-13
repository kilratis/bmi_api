using Bmi.Api.Converters;

namespace Bmi.Api
{
  public static class ServiceRegistration
  {
    public static void AddServices(IServiceCollection services)
    {
      // Register services here
      services.AddTransient<IConverter, CmToMeterConverter>();
    }
  }
}
