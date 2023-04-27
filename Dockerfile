FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
WORKDIR /app
COPY ./MovieAppCase.Core/*.csproj ./MovieAppCase.Core/
COPY ./MovieAppCase.Api/*.csproj ./MovieAppCase.Api/
COPY ./MovieAppCase.Repository/*.csproj ./MovieAppCase.Repository/
COPY ./MovieAppCase.Service/*.csproj ./MovieAppCase.Service/
COPY ./MovieCrawler/*.csproj ./MovieCrawler/
COPY ./MovieAppCase.Consumer/*.csproj ./MovieAppCase.Consumer/
COPY ./MovieAppCase.Test/*.csproj ./MovieAppCase.Test/
COPY *.sln .
RUN dotnet restore
COPY . .
RUN dotnet publish ./MovieAppCase.Api/*.csproj -o /publish/
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /publish .
ENV ASPNETCORE_URLS="http://*:5000"
ENTRYPOINT ["dotnet","MovieAppCase.Api.dll"]
