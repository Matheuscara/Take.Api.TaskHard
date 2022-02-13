FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine3.11 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine3.11 AS build
WORKDIR /src
COPY ["Take.Api.TaskHard.Facades/Take.Api.TaskHard.Facades.csproj", "Take.Api.TaskHard.Facades/"]
COPY ["Take.Api.TaskHard.Models/Take.Api.TaskHard.Models.csproj", "Take.Api.TaskHard.Models/"]
COPY ["Take.Api.TaskHard.Services/Take.Api.TaskHard.Services.csproj", "Take.Api.TaskHard.Services/"]
COPY ["Take.Api.TaskHard/Take.Api.TaskHard.csproj", "Take.Api.TaskHard/"]

RUN dotnet restore "Take.Api.TaskHard/Take.Api.TaskHard.csproj"
COPY . .
WORKDIR "/src/Take.Api.TaskHard"
RUN dotnet build "Take.Api.TaskHard.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Take.Api.TaskHard.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Take.Api.TaskHard.dll"]