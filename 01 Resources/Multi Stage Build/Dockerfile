FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source

COPY *.csproj ./
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /source/out .
EXPOSE 80
ENTRYPOINT ["dotnet", "BookStore_AzureAppService.dll"]