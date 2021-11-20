#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["SaaS.Api/SaaS.Api.csproj", "SaaS.Api/"]
COPY ["SaaS.Application/SaaS.Application.csproj", "SaaS.Application/"]
COPY ["SaaS.Domain/SaaS.Domain.csproj", "SaaS.Domain/"]
COPY ["SaaS.Infrastructure/SaaS.Infrastructure.csproj", "SaaS.Infrastructure/"]
RUN dotnet restore "SaaS.Api/SaaS.Api.csproj"
COPY . .
WORKDIR "/src/SaaS.Api"
RUN dotnet build "SaaS.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SaaS.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SaaS.Api.dll"]
