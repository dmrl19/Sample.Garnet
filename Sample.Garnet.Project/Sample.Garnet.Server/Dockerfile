# docker build . -f Sample.Garnet.Server/Dockerfile --tag dmm91/garnet-server-sample
# docker run --name garnet-server-sample -p 6379:6379 dmm91/garnet-server-sample

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
COPY Sample.Garnet.Server Sample.Garnet.Server
WORKDIR /Sample.Garnet.Server
RUN dotnet restore
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish --self-contained false -f net8.0

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 6379
EXPOSE 6380

ENTRYPOINT ["/app/Sample.Garnet.Server", "-i", "128m", "--port", "6379"]