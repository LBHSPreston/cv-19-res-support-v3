using System.Collections.Generic;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V3.UseCase;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V3.UseCase
{
    [TestFixture]
    public class GetCallbacksUseCaseTests
    {
        private Mock<IHelpRequestGateway> _mockGateway;
        private GetCallbacksUseCase _classUnderTest;

        [SetUp]
        public void SetUp()
        {
            _mockGateway = new Mock<IHelpRequestGateway>();
            _classUnderTest = new GetCallbacksUseCase(_mockGateway.Object);
        }

        [Test]
        public void ReturnsPopulatedHelpRequestListIfParamsProvided()
        {
            var reqParams = new CallbackQuery() { HelpNeeded = "shielding" };
            var expectedResponse = new List<HelpRequestWithResident>() { new HelpRequestWithResident() { Id = 3 } };
            _mockGateway.Setup(x => x.GetCallbacks(reqParams)).Returns(expectedResponse);
            var response = _classUnderTest.Execute(reqParams);
            _mockGateway.Verify(x => x.GetCallbacks(reqParams), Times.Once);
            response.Should().NotBeNull();
            response.Should().BeEquivalentTo(expectedResponse);
        }
    }
}
