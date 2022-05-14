using System;
using Vulder.Timetable.Infrastructure.Api;
using Vulder.Timetable.Infrastructure.Api.Models;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using Xunit;

namespace Vulder.Timetable.UnitTests.Infrastructure.Api;

public class SchoolApiTest
{
    [Fact]
    public async void TestGetSchoolModel_CheckIfNotNull()
    {
        var schoolModel = new GetSchoolResponse
        {
            Id = Guid.NewGuid(),
            Name = "ZSP 2 w Warszawie",
            SchoolUrl = "http://example.com",
            TimetableUrl = "http://example.com/timetable"
        };

        var server = WireMockServer.Start();
        server.Given(Request.Create()
                .WithPath("/school")
                .UsingGet()
                .WithParam("schoolId", schoolModel.Id.ToString()))
            .RespondWith(Response.Create()
                .WithStatusCode(200)
                .WithBodyAsJson(schoolModel)
            );

        Environment.SetEnvironmentVariable("BASE_API_URL", server.Urls[0]);

        var result = await SchoolApi.GetSchoolModel(schoolModel.Id);

        Assert.NotNull(result);
    }
}