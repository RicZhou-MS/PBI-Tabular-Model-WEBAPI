## run below command to build docker image
## (he build command arguments: Name the image pbitabluarwebapi. Look for the Dockerfile in the current folder (the period at the end).)
# docker build -t pbitabluarwebapi .

## run below command to run docker image
## (The run command arguments: Run the app, mapping your machine’s port 6000 to the container’s published port 8080 using -p)
## (container's published port is specified in appsettings.json(8080 in this case) or ASPNETCORE_HTTP_PORTS in container Env)
# docker run -it --rm -p 6000:8080 --name pbi-tabular-web-api pbitabluarwebapi

## "login to Azure Container Registry"
# docker login -u <user> -p <password> acr0612.azurecr.cn

## "create a new tag for a Docker image with repository name"
# docker tag pbi-tabular-web-api:0.1 acr0612.azurecr.cn/pbi-tabular-web-api:0.1

## "push a Docker image to a container registry"
# docker push acr0612.azurecr.cn/pbi-tabular-web-api:0.1


# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
#COPY *.sln .
COPY *.csproj .
RUN dotnet restore

# copy everything else and build app
COPY *.* .
WORKDIR /source
RUN dotnet publish -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "PBI-Tabular-Model-Web-API.dll"]