using Bmi.Api.Controllers;
using Bmi.Api.Converters;
using Bmi.Api.Models.BMICalculatorAPI;
using Moq;

namespace Bmi.Api.Tests
{
  [TestFixture]
  public class BMICalculatorControllerTests
  {
    private BMICalculatorController _controller;
    private Mock<IConverter> _cmToMeterConverterMock;

    [SetUp]
    public void Setup()
    {
      _cmToMeterConverterMock = new Mock<IConverter>();

      _controller = new BMICalculatorController(_cmToMeterConverterMock.Object);
    }

    [Test]
    public void CalculateBmi_ReturnsCorrectBmiAndCategory()
    {
      // Arrange
      var request = new BmiRequest
      {
        Height = 180,  // centimeters
        Weight = 75    // kilograms
      };
      var expectedHeightInMeters = 1.8f;
      _cmToMeterConverterMock.Setup(x => x.Convert(request.Height)).Returns(expectedHeightInMeters);

      // Act
      var result = _controller.CalculateBmi(request);

      // Assert
      Assert.NotNull(result);
      Assert.IsInstanceOf<BmiResponse>(result.Value);

      var response = result.Value;
      Assert.AreEqual(23.15f, response.Bmi, 0.01f); // Allowing a precision of 2 decimal places
      Assert.AreEqual("Normal weight", response.Category);
    }
  }
}
