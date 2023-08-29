#!/usr/bin/bash

export DOCKER_REGISTRY=phongbv
export WEBAPP_STORAGE_HOME=/f/Source/phongbv/AspnetMicroservices/src/site
docker-compose -f docker-compose.yml -f docker-compose.override.yml up --build