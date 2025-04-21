FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Gratia.Api/Gratia.Api.csproj", "Gratia.Api/"]
RUN dotnet restore "Gratia.Api/Gratia.Api.csproj"
COPY . .
WORKDIR "/src/Gratia.Api"
RUN dotnet build "Gratia.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Gratia.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gratia.Api.dll"] 