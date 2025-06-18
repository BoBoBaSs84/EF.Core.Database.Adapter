FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["Directory.Build.props", "./"]
COPY ["Directory.Packages.props", "./"]
COPY ["src/BB84.Home.Application/BB84.Home.Application.csproj", "src/BB84.Home.Application/"]
COPY ["src/BB84.Home.Domain/BB84.Home.Domain.csproj", "src/BB84.Home.Domain/"]
COPY ["src/BB84.Home.Infrastructure/BB84.Home.Infrastructure.csproj", "src/BB84.Home.Infrastructure/"]
COPY ["src/BB84.Home.Presentation/BB84.Home.Presentation.csproj", "src/BB84.Home.Presentation/"]
COPY ["src/BB84.Home.API/BB84.Home.API.csproj", "src/BB84.Home.API/"]
RUN dotnet restore "src/BB84.Home.API/BB84.Home.API.csproj"
COPY . .
WORKDIR "/src/src/BB84.Home.API"
RUN dotnet build "BB84.Home.API.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "BB84.Home.API.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BB84.Home.API.dll"]
