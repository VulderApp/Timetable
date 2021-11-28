using System;
using System.Net;
using Flurl.Http.Testing;
using Vulder.Timetable.Infrastructure.Api.Models;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using Xunit;

namespace Vulder.Timetable.IntegrationTests.Controllers.Branch;

public class GetBranchesControllerTest
{
    [Fact]
    public async void GET_200_StatusCode()
    {
        var schoolModel = new GetSchoolResponse
        {
            Id = Guid.NewGuid(),
            Name = "ZSP 2 w Warszawie",
            SchoolUrl = "http://example.com",
            TimetableUrl = "http://example.com/timetable",
        };

        var server = WireMockServer.Start();
        server.Given(Request.Create()
                .WithPath("/school/GetSchool")
                .UsingGet()
                .WithParam("schoolId", schoolModel.Id.ToString()))
            .RespondWith(Response.Create()
                .WithStatusCode(200)
                .WithBodyAsJson(schoolModel)
            );
        
        Environment.SetEnvironmentVariable("BASE_API_URL", server.Urls[0]);

        await using var application = new WebServerFactory();
        using var client = application.CreateClient();
        using var getBranchesResponse = await client.GetAsync($"branch/GetBranches?schoolId={schoolModel.Id}");
        
        Assert.Equal(HttpStatusCode.OK, getBranchesResponse.StatusCode);
    }
}