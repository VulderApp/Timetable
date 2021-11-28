using System;
using Flurl.Http.Testing;
using Vulder.Timetable.Infrastructure.Api;
using Vulder.Timetable.Infrastructure.Api.Models;
using Xunit;

namespace Vulder.Timetable.UnitTests.Infrastructure.Api;

public class SchoolApiTest
{
    [Fact]
    public async void TestGetSchoolModel_CheckHashCode()
    {
        Environment.SetEnvironmentVariable("BASE_API_URL", "http://vulder.xyz");
        var schoolModel = new GetSchoolResponse
        {
            Id = Guid.NewGuid(),
            Name = "ZSP 2 w Warszawie",
            SchoolUrl = "http://example.com",
            TimetableUrl = "http://example.com/timetable",
        };
        
        using var httpTest = new HttpTest();
        httpTest.ForCallsTo("http://vulder.xyz/*")
            .WithVerb("GET")
            .WithoutQueryParam("schoolId")
            .RespondWithJson(schoolModel);

        var result = await SchoolApi.GetSchoolModel(Guid.Empty);
        
        Assert.Equal(schoolModel.GetHashCode(), result.GetHashCode());
    }
}