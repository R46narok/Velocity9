FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Services/Skeletal/V9.Services.Skeletal.Serverless/V9.Services.Skeletal.Serverless.csproj", "src/Services/Skeletal/V9.Services.Skeletal.Serverless/"]
COPY ["src/Services/Skeletal/V9.Services.Skeletal/V9.Services.Skeletal.csproj", "src/Services/Skeletal/V9.Services.Skeletal/"]
COPY ["src/Common/V9.Application/V9.Application.csproj", "src/Common/V9.Application/"]
COPY ["src/Common/V9.Domain/V9.Domain.csproj", "src/Common/V9.Domain/"]
COPY ["src/Common/V9.Infrastructure/V9.Infrastructure.csproj", "src/Common/V9.Infrastructure/"]
RUN dotnet restore "src/Services/Skeletal/V9.Services.Skeletal.Serverless/V9.Services.Skeletal.Serverless.csproj"
COPY . .
WORKDIR "/src/src/Services/Skeletal/V9.Services.Skeletal.Serverless"
RUN dotnet build "V9.Services.Skeletal.Serverless.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "V9.Services.Skeletal.Serverless.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "V9.Services.Skeletal.Serverless.dll"]
