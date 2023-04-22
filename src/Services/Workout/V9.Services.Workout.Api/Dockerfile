FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/Workout/V9.Services.Workout.Api/V9.Services.Workout.Api.csproj", "src/Services/Workout/V9.Services.Workout.Api/"]
COPY ["src/Common/V9.Application/V9.Application.csproj", "src/Common/V9.Application/"]
COPY ["src/Common/V9.Domain/V9.Domain.csproj", "src/Common/V9.Domain/"]
COPY ["src/Common/V9.Infrastructure/V9.Infrastructure.csproj", "src/Common/V9.Infrastructure/"]
COPY ["src/Services/Workout/V9.Services.Workout/V9.Services.Workout.csproj", "src/Services/Workout/V9.Services.Workout/"]
COPY ["src/Services/Workout/V9.Services.Workout.DeepLearning/V9.Services.Workout.DeepLearning.csproj", "src/Services/Workout/V9.Services.Workout.DeepLearning/"]
RUN dotnet restore "src/Services/Workout/V9.Services.Workout.Api/V9.Services.Workout.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Workout/V9.Services.Workout.Api"
RUN dotnet build "V9.Services.Workout.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "V9.Services.Workout.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "V9.Services.Workout.Api.dll"]
