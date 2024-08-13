namespace Bmi.Api.Models.BMICalculatorAPI
{
  public class BmiRequest
  {
    /// <summary>
    /// In centimeters
    /// </summary>
    public float Height { get; set; }

    /// <summary>
    /// In kilograms
    /// </summary>
    public float Weight { get; set; }
  }
}
