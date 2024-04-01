# docker build . -f Sample.Garnet.Client.Redis/Dockerfile --tag dmm91/garnet-client-redis-sample
# docker run --name garnet-client-redis-sample dmm91/garnet-client-redis-sample

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
COPY Sample.Garnet.Client.Redis Sample.Garnet.Client.Redis
WORKDIR /Sample.Garnet.Client.Redis
RUN dotnet restore
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish --self-contained false -f net8.0

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["/app/Sample.Garnet.Client.Redis"]