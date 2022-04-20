# Timetable
[![Test](https://github.com/VulderApp/Timetable/actions/workflows/test.yml/badge.svg)](https://github.com/VulderApp/Timetable/actions/workflows/test.yml)

Microservice for getting school branches and timetables 

## Run development server
```bash
$ docker-compose -f docker-compose.dev.yml up -d
$ dotnet restore
$ dotnet watch --project ./src/Vulder.Timetable.Api
```

## Build a release
```bash
$ dotnet restore
$ dotnet build
$ dotnet publish ./src/Vulder.Timetable.Api -c Release
```

## Build a docker image
```bash
$ docker build -t vulderapp/timetable:release .
```
## Run a docker image
```bash
$ docker run -p 80:80 -e BASE_API_URL=api_url -e REDIS_CONNECTION_STRING=connection_string vulderapp/timetable:release
```