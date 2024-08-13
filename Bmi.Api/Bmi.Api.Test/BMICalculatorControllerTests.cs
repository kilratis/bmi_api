using Bmi.Api.Controllers;
using Bmi.Api.Converters;
using Bmi.Api.Models.BMICalculatorAPI;
using Microsoft.AspNetCore.Mvc;
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
      var okResult = result.Result as OkObjectResult;
      Assert.NotNull(okResult);
      Assert.IsInstanceOf<BmiResponse>(okResult.Value);

      var response = okResult.Value as BmiResponse;
      Assert.AreEqual(23.15f, response.Bmi, 0.01f);
      Assert.AreEqual("Normal weight", response.Category);
    }

    [Test]
    public void CalculateBmi_UnderweightCategory()
    {
      // Arrange
      var request = new BmiRequest
      {
        Height = 180,  // centimeters
        Weight = 50    // kilograms
      };
      var expectedHeightInMeters = 1.8f;
      _cmToMeterConverterMock.Setup(x => x.Convert(request.Height)).Returns(expectedHeightInMeters);

      // Act
      var result = _controller.CalculateBmi(request);

      // Assert
      Assert.NotNull(result);
      var okResult = result.Result as OkObjectResult;
      Assert.NotNull(okResult);
      Assert.IsInstanceOf<BmiResponse>(okResult.Value);

      var response = okResult.Value as BmiResponse;
      Assert.AreEqual(15.43f, response.Bmi, 0.01f);
      Assert.AreEqual("Underweight", response.Category);
    }

    [Test]
    public void CalculateBmi_OverweightCategory()
    {
      // Arrange
      var request = new BmiRequest
      {
        Height = 160,  // centimeters
        Weight = 80    // kilograms
      };
      var expectedHeightInMeters = 1.6f;
      _cmToMeterConverterMock.Setup(x => x.Convert(request.Height)).Returns(expectedHeightInMeters);

      // Act
      var result = _controller.CalculateBmi(request);

      // Assert
      Assert.NotNull(result);
      var okResult = result.Result as OkObjectResult;
      Assert.NotNull(okResult);
      Assert.IsInstanceOf<BmiResponse>(okResult.Value);

      var response = okResult.Value as BmiResponse;
      Assert.AreEqual(31.25f, response.Bmi, 0.01f);
      Assert.AreEqual("Obesity", response.Category);
    }
  }
}
