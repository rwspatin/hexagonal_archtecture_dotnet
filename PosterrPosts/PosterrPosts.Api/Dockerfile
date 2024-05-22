FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PosterrPosts.Api/PosterrPosts.Api.csproj", "PosterrPosts.Api/"]
RUN dotnet restore "PosterrPosts.Api/PosterrPosts.Api.csproj"
COPY . .
WORKDIR "/src/PosterrPosts.Api"
RUN dotnet build "PosterrPosts.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PosterrPosts.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PosterrPosts.Api.dll"]