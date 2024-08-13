namespace Bmi.Api.Converters
{
  public class CmToMeterConverter : IConverter
  {
    public float Convert(float cmValue)
    {
      return cmValue / 100;
    }
  }
}
