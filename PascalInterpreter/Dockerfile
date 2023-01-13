FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["PascalInterpreter/PascalInterpreter.csproj", "PascalInterpreter/"]
RUN dotnet restore "PascalInterpreter/PascalInterpreter.csproj"
COPY . .
WORKDIR "/src/PascalInterpreter"
RUN dotnet build "PascalInterpreter.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PascalInterpreter.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PascalInterpreter.dll"]
