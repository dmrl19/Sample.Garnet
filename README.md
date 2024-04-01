# Sample project to use Garnet as cache
This is a sample project to use Garnet as your cache storage

## Prerequisites
* [Garnet](https://microsoft.github.io/garnet/docs)


## Garnet

> Garnet is a new remote cache-store from Microsoft Research, that is designed to be extremely fast, extensible, and low latency. Garnet is thread-scalable within a single node. It also supports sharded cluster execution, with replication, checkpointing, failover, and transactions. It can operate over main memory as well as tiered storage (such as SSD and Azure Storage)

## Setup your services Locally using docker compose

To build with docker-compose on an specific file use the following command `docker-compose -f [DockerComposeYamlFile] build`

```sh
docker-compose -f docker-compose.yaml build
```

## Run continers with docker-compose
To run containers with docker-compose on an specific file use the following command: `docker-compose -f [DockerComposeYamlFile] up -d`

```sh
docker-compose -f docker-compose.yaml up -d
```

## Stop containers with docker-compose
To stop containers with docker-compose on an specific file use the following command: `docker-compose -f [DockerComposeYamlFile] down` or `docker-compose -f [DockerComposeYamlFile] down --rmi local`

This command will only stop the containers specified on the yaml file

```sh
docker-compose -f single.docker-compose.yml down
```
This command will stop and remove the local images specified on the yaml file.

```sh
docker-compose -f single.docker-compose.yml down --rmi local
```