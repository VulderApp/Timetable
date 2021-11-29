using System;
using System.Net;
using Vulder.Timetable.Infrastructure.Api.Models;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using Xunit;

namespace Vulder.Timetable.IntegrationTests.Controllers.Timetable;

public class GetTimetableControllerTest
{
    private const string ClassName = "6A";
    private const string ShortUrl = "%2Fplany%2Fs2.html";
    
    [Fact]
    public async void GET_200_StatusCode()
    {
        var schoolModel = new GetSchoolResponse
        {
            Id = Guid.NewGuid(),
            Name = "ZSP 2 w Warszawie",
            SchoolUrl = "http://example.com",
            TimetableUrl = "https://vulderapp.github.io/fake-school/"
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
        using var getBranchesResponse = await client.GetAsync($"timetable/GetTimetable?schoolId={schoolModel.Id}&className={ClassName}&shortPath={ShortUrl}");

        Assert.Equal(HttpStatusCode.OK, getBranchesResponse.StatusCode);
    }
}