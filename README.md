# C# Docker Workshop - WebApi Container



## Learning Objectives
- Create and publish a simple WebApi
- Create and configure a Dockerfile for .Net  
- Build a Docker image
- Create and run a Docker container

## WebApi


Examine the WebApi.  Feel free to add further endpoints/controller methods. 

Lets go through the steps to **Containerize** this application.  

- From within the exercise.wwwapi directory (where the exercise.wwwapi.csproj resides) run the 
command:  

```
dotnet publish -c Release 
```

- This should create a Release folder inside the exercise.main\bin\Release\net7.0 with a Release build of the application.  
- Now from within the exercise.main directory (where the exercise.main.csproj resides) create a Dockerfile:
```
touch Docker
```
- Now open the Dockerfile and add the following contents to it:  
```
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /exercise.wwwapi

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /exercise.wwwapi
COPY --from=build-env /exercise.wwwapi/out .
ENTRYPOINT ["dotnet", "exercise.wwwapi.dll"]
```
- Now create a Docker Compose file
```
touch docker-compose.yml
```
- Now open the docker-compose.yml and add the following contents to it:
```
version: '3.9'

services:
  csharpapp:
    container_name: csharpapi
    image: uerbzr/csharpapi:1.0
    build: .
    ports:
      - "8080:8080"
    environment:
      ConnectionStrings__DefaultConnection: "Host=db;Database=postgres;Username=postgres;Password=postgres"
    depends_on:
      - db
  
  db:
    container_name: db
    image: postgres:12
    ports:
      - "5432:5432"
    environment:
      POSTGRES_PASSWORD: postgres
      POSTGRES_USER: postgres
      POSTGRES_DB: postgres
    volumes:
      - pgdata:/var/lib/postgresql/data

volumes:
  pgdata: {}
```
- All being well you should be able to run the following command: 
```
docker build -t my-api-image -f Dockerfile .  
```
- This should build and create your docker image.  You should be able to see your image in Docker Desktop now.  

## Create a container
Now you have an image you need to create a container by running:
```
docker create --name my-api-container my-api-image 
```
## Listing all containers 
```
docker ps -a
```
## Starting a container
```
docker start my-api-container
```
## Attaching to a container
```
docker attach --sig-proxy=false my-api-container
```
### Stopping a container
```
docker stop my-api-container
```

## Deleting a container
```
docker rm my-api-container
```


