using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using cv19ResSupportV3.V3.Boundary.Response;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Infrastructure;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V3.E2ETests
{
    [TestFixture]
    public class CreateHelpRequestCall : IntegrationTests<Startup>
    {
        [SetUp]
        public void RunBeforeAnyTests()
        {
            DatabaseContext.Database.RollbackTransaction();
            E2ETestHelpers.ClearTable(DatabaseContext);
        }
        [Test]
        public async Task CreateHelpRequestCallReturnsTheCorrectInformation()
        {
            var helpRequestEntity = new Fixture().Build<HelpRequestEntity>()
                .With(x => x.Id, 1).Create();
            DatabaseContext.HelpRequestEntities.Add(helpRequestEntity);
            DatabaseContext.SaveChanges();
            var requestObject = new Fixture().Build<HelpRequestCall>().Create();
            var data = JsonConvert.SerializeObject(requestObject);
            HttpContent postContent = new StringContent(data, Encoding.UTF8, "application/json");
            var uri = new Uri($"api/v3/help-requests/1/calls", UriKind.Relative);
            var response = Client.PostAsync(uri, postContent);
            postContent.Dispose();
            var statusCode = response.Result.StatusCode;
            statusCode.Should().Be(201);
            var content = response.Result.Content;
            Console.WriteLine("123456");
            Console.WriteLine(content);
            var stringContent = await content.ReadAsStringAsync().ConfigureAwait(true);
            var convertedResponse = JsonConvert.DeserializeObject<HelpRequestCallCreateResponse>(stringContent);
            var createdEntity = DatabaseContext.HelpRequestCallEntities.Find(requestObject.Id);
            createdEntity.HelpRequestId.Should().Be(1);
            createdEntity.CallType.Should().Be(requestObject.CallType);
            createdEntity.CallOutcome.Should().Be(requestObject.CallOutcome);
            createdEntity.CallDateTime. Should().BeCloseTo(requestObject.CallDateTime, 2000);
        }
    }
}
