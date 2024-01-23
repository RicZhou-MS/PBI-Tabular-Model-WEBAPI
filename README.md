# PBI-Tabular-Model-Web-API

PBI Tablular Model Web Api wrapper

# Background Knowledge

(NOTE: don't need to do if you clone this project directly)

- Refer to [this link](https://learn.microsoft.com/en-us/aspnet/core/tutorials/min-web-api?view=aspnetcore-8.0&tabs=visual-studio-code) for vscode and .net core preparation for the web api project
- Run `dotnet new web -o .` to create empty .Net core web api project at local folder
- Use `dotnet new gitignore` to generate .gitinore file at local folder

# Initialize the project for packages

- Run following code at project folder in VSCode to install the AAS nuget packages
  ```
  dotnet add package Microsoft.AnalysisServices.AdomdClient.NetCore.retail.amd64
  dotnet add package Microsoft.AnalysisServices.NetCore.retail.amd64
  dotnet add package Microsoft.Identity.Client
  dotnet add package Swashbuckle.AspNetCore
  ```

# Dockerize

- Make sure the [docker desktop](https://www.docker.com/products/docker-desktop/) is running on your machine, then run following command at current project folder to make the docker image
  ```
  docker build -t pbitabluarwebapi .
  ```

# Access Swagger

- Refer to [This link](https://learn.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-7.0&tabs=visual-studio-code) for enable Swagger for the API, check following links for render swagger interface
  - `https://host:<port>/swagger`
  - `https://host:<port>/swagger/v1/swagger.json`
