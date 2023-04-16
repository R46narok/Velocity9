FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/Skeletal/V9.Services.Skeletal.Api/V9.Services.Skeletal.Api.csproj", "src/Services/Skeletal/V9.Services.Skeletal.Api/"]
COPY ["src/Common/V9.Application/V9.Application.csproj", "src/Common/V9.Application/"]
COPY ["src/Common/V9.Domain/V9.Domain.csproj", "src/Common/V9.Domain/"]
COPY ["src/Common/V9.Infrastructure/V9.Infrastructure.csproj", "src/Common/V9.Infrastructure/"]
COPY ["src/Services/Skeletal/V9.Services.Skeletal/V9.Services.Skeletal.csproj", "src/Services/Skeletal/V9.Services.Skeletal/"]
RUN dotnet restore "src/Services/Skeletal/V9.Services.Skeletal.Api/V9.Services.Skeletal.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Skeletal/V9.Services.Skeletal.Api"
RUN dotnet build "V9.Services.Skeletal.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "V9.Services.Skeletal.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "V9.Services.Skeletal.Api.dll"]
