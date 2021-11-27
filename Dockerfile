FROM mcr.microsoft.com/dotnet/sdk:6.0.100-alpine3.14-amd64 AS build

WORKDIR /src

COPY . .

RUN dotnet restore
RUN dotnet publish src/Vulder.Timetable.Api -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:6.0.0-alpine3.14-amd64

WORKDIR /app

COPY --from=build /app .

EXPOSE 80
EXPOSE 443

ENTRYPOINT [ "dotnet", "Vulder.Timetable.Api.dll" ]