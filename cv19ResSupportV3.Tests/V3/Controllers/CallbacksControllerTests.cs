using System;
using System.Collections.Generic;
using System.Diagnostics;
using AutoFixture;
using cv19ResSupportV3.V3.Boundary.Requests;
using cv19ResSupportV3.V3.Boundary.Response;
using cv19ResSupportV3.V3.Controllers;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Factories;
using cv19ResSupportV3.V3.Infrastructure;
using cv19ResSupportV3.V3.UseCase;
using cv19ResSupportV3.V3.UseCase.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Moq;

namespace cv19ResRupportV3.Tests.V3.Controllers
{

    [TestFixture]
    public class CallbacksControllerTests
    {
        private CallbacksController _classUnderTest;
        private Mock<IGetCallbacksUseCase> _getCallbacksUseCase;

        [SetUp]
        public void SetUp()
        {
            _getCallbacksUseCase = new Mock<IGetCallbacksUseCase>();
            _classUnderTest = new CallbacksController(_getCallbacksUseCase.Object);
        }

        [Test]
        public void ReturnsResponseWithStatus()
        {
            var requests = new Fixture().CreateMany<HelpRequestEntity>();
            var reqParams = new CallbackRequestParams();
            _getCallbacksUseCase.Setup(x => x.Execute(reqParams))
                .Returns(requests.ToResponse());
            var response = _classUnderTest.GetCallbacks(reqParams) as OkObjectResult;
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(200);
        }
    }
}