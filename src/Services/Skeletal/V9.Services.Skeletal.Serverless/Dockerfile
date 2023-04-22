FROM mcr.microsoft.com/azure-functions/dotnet-isolated:4-dotnet-isolated7.0 AS base
WORKDIR /home/site/wwwroot
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build


WORKDIR /src

COPY ["src/Common/V9.Application/V9.Application.csproj", "src/Common/V9.Application/"]
COPY ["src/Common/V9.Domain/V9.Domain.csproj", "src/Common/V9.Domain/"]
COPY ["src/Common/V9.Infrastructure/V9.Infrastructure.csproj", "src/Common/V9.Infrastructure/"]

COPY ["src/Services/Skeletal/V9.Services.Skeletal/V9.Services.Skeletal.csproj", "src/Services/Skeletal/V9.Services.Skeletal/"]
COPY ["src/Services/Skeletal/V9.Services.Skeletal.Serverless/V9.Services.Skeletal.Serverless.csproj", "src/Services/Skeletal/V9.Services.Skeletal.Serverless/"]

RUN dotnet restore "src/Services/Skeletal/V9.Services.Skeletal.Serverless/V9.Services.Skeletal.Serverless.csproj"
COPY . .
WORKDIR "/src/src/Services/Skeletal/V9.Services.Skeletal.Serverless"
RUN dotnet build "V9.Services.Skeletal.Serverless.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "V9.Services.Skeletal.Serverless.csproj" -c Release -o /app/publish /p:UseAppHost=false


FROM base AS final
WORKDIR /home/site/wwwroot
COPY --from=publish /app/publish .
ENV AzureWebJobsScriptRoot=/home/site/wwwroot \
    AzureFunctionsJobHost__Logging__Console__IsEnabled=true