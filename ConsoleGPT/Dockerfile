# Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ConsoleGPT/ConsoleGPT.csproj", "ConsoleGPT/"]
RUN dotnet restore "ConsoleGPT/ConsoleGPT.csproj"
COPY . .
WORKDIR "/src/ConsoleGPT"
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Run
FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ConsoleGPT.dll"]
