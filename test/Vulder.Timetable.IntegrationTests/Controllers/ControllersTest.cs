using System;
using System.Net;
using Vulder.Timetable.Infrastructure.Api.Models;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using WireMock.Settings;
using Xunit;

namespace Vulder.Timetable.IntegrationTests.Controllers;

public class ControllersTest : IDisposable
{
    private const string ShortUrl = "/plany/o1.html";
    private readonly GetSchoolResponse _schoolTestModel;
    private readonly WireMockServer _server;

    public ControllersTest()
    {
        _server = WireMockServer.Start(new WireMockServerSettings
        {
            Urls = new[] { "http://+:5001" },
            StartAdminInterface = true,
            ReadStaticMappings = true
        });

        _schoolTestModel = new GetSchoolResponse
        {
            Id = Guid.NewGuid(),
            Name = "ZSP 2 w Warszawie",
            SchoolUrl = "http://example.com",
            TimetableUrl = "https://vulderapp.github.io/fake-school/"
        };

        _server.Given(Request.Create()
                .WithPath("/school/GetSchool")
                .UsingGet()
                .WithParam("schoolId", _schoolTestModel.Id.ToString()))
            .RespondWith(Response.Create()
                .WithStatusCode(200)
                .WithBodyAsJson(_schoolTestModel)
            );

        Environment.SetEnvironmentVariable("BASE_API_URL", _server.Urls[0]);
    }

    public void Dispose()
    {
        _server.Dispose();
    }

    [Fact]
    public async void Branches_GET_200_StatusCode()
    {
        await using var application = new WebServerFactory();
        using var client = application.CreateClient();
        using var getBranchesResponse = await client.GetAsync($"branches?schoolId={_schoolTestModel.Id}");

        Assert.Equal(HttpStatusCode.OK, getBranchesResponse.StatusCode);
    }

    [Fact]
    public async void Timetable_GET_200_StatusCode()
    {
        await using var application = new WebServerFactory();
        using var client = application.CreateClient();
        using var getBranchesResponse =
            await client.GetAsync(
                $"timetable?schoolId={_schoolTestModel.Id}&shortPath={ShortUrl}");

        Assert.Equal(HttpStatusCode.OK, getBranchesResponse.StatusCode);
    }
}