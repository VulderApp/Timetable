using Flurl;
using Flurl.Http;
using Vulder.Timetable.Core;
using Vulder.Timetable.Infrastructure.Api.Models;

namespace Vulder.Timetable.Infrastructure.Api;

public static class SchoolApi
{
    public static async Task<GetSchoolResponse> GetSchoolModel(Guid schoolId)
        => await Constants.BaseApiUrl
            .AppendPathSegment("/school/GetSchool")
            .SetQueryParam("schoolId", schoolId)
            .WithClient(new FlurlClient(new HttpClient(new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (_, _, _, _) => true,
            })))
            .GetAsync()
            .ReceiveJson<GetSchoolResponse>();
}