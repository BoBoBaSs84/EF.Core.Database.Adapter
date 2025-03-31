FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 443

ENV ASPNETCORE_URLS=http://+:443

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["src/Application/BB84.Home.Application.csproj", "src/Application/"]
COPY ["src/Domain/BB84.Home.Domain.csproj", "src/Domain/"]
COPY ["src/Infrastructure/BB84.Home.Infrastructure.csproj", "src/Infrastructure/"]
COPY ["src/Presentation/BB84.Home.Presentation.csproj", "src/Presentation/"]
COPY ["src/WebAPI/BB84.Home.API.csproj", "src/WebAPI/"]
COPY ["Directory.Build.props", "./"]
COPY ["Directory.Packages.props", "./"]
RUN dotnet restore "src/WebAPI/BB84.Home.API.csproj"
COPY . .
WORKDIR "/src/src/WebAPI"
RUN dotnet build "BB84.Home.API.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "BB84.Home.API.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BB84.Home.API.dll"]
