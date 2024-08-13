using Bmi.Api.Converters;
using Bmi.Api.Models.BMICalculatorAPI;
using Microsoft.AspNetCore.Mvc;

namespace Bmi.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class BMICalculatorController : Controller
  {
    private IConverter _cmToMeterConverter;

    public BMICalculatorController(IConverter cmToMeterConverter)
    {
      _cmToMeterConverter = cmToMeterConverter;
    }

    [HttpPost]
    public ActionResult<BmiResponse> CalculateBmi([FromBody] BmiRequest request)
    {
      if (request.Height <= 0 || request.Weight <= 0)
      {
        return BadRequest("Height and weight must be positive values.");
      }
      try
      {
        float heightInMeters = _cmToMeterConverter.Convert(request.Height);
        float bmi = request.Weight / (heightInMeters * heightInMeters);
        string category;

        if (bmi < 18.5)
        {
          category = "Underweight";
        }
        else if (bmi >= 18.5 && bmi < 24.9)
        {
          category = "Normal weight";
        }
        else if (bmi >= 25 && bmi < 29.9)
        {
          category = "Overweight";
        }
        else
        {
          category = "Obesity";
        }

        var response = new BmiResponse
        {
          Bmi = bmi,
          Category = category
        };

        return Ok(response);
      }
      catch (Exception ex)
      {
        return StatusCode(500, $"Internal server error: {ex.Message}");
      }
    }
  }
}
