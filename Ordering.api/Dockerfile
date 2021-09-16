#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Ordering.api/Ordering.api.csproj", "Ordering.api/"]
COPY ["Ordering.Application/Ordering.Application.csproj", "Ordering.Application/"]
COPY ["Ordering.Domain/Ordering.Domain.csproj", "Ordering.Domain/"]
COPY ["Ordering.Infraestructure/Ordering.Infraestructure.csproj", "Ordering.Infraestructure/"]
RUN dotnet restore "Ordering.api/Ordering.api.csproj"
COPY . .
WORKDIR "/src/Ordering.api"
RUN dotnet build "Ordering.api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ordering.api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ordering.api.dll"]
