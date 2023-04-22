FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Services/Coach/V9.Services.Coach.Api/V9.Services.Coach.Api.csproj", "src/Services/Coach/V9.Services.Coach.Api/"]
COPY ["src/Common/V9.Application/V9.Application.csproj", "src/Common/V9.Application/"]
COPY ["src/Common/V9.Domain/V9.Domain.csproj", "src/Common/V9.Domain/"]
COPY ["src/Common/V9.Infrastructure/V9.Infrastructure.csproj", "src/Common/V9.Infrastructure/"]
COPY ["src/Services/Coach/V9.Services.Coach/V9.Services.Coach.csproj", "src/Services/Coach/V9.Services.Coach/"]
COPY ["src/Services/Coach/V9.Services.Coach.DeepLearning/V9.Services.Coach.DeepLearning.csproj", "src/Services/Coach/V9.Services.Coach.DeepLearning/"]
RUN dotnet restore "src/Services/Coach/V9.Services.Coach.Api/V9.Services.Coach.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Coach/V9.Services.Coach.Api"
RUN dotnet build "V9.Services.Coach.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "V9.Services.Coach.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "V9.Services.Coach.Api.dll"]
