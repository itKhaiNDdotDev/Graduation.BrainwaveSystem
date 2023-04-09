#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Graduation.BrainwaveSystem.APIs/Graduation.BrainwaveSystem.APIs.csproj", "Graduation.BrainwaveSystem.APIs/"]
RUN dotnet restore "Graduation.BrainwaveSystem.APIs/Graduation.BrainwaveSystem.APIs.csproj"
COPY . .
WORKDIR "/src/Graduation.BrainwaveSystem.APIs"
RUN dotnet build "Graduation.BrainwaveSystem.APIs.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Graduation.BrainwaveSystem.APIs.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Graduation.BrainwaveSystem.APIs.dll"]