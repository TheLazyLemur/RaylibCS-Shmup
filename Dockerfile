FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["TestGame/TestGame.csproj", "TestGame/"]
RUN dotnet restore "TestGame/TestGame.csproj"
COPY . .
WORKDIR "/src/TestGame"
RUN dotnet build "TestGame.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestGame.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestGame.dll"]
