# Use the official ASP.NET Core Runtime as a base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8181

# Use the official .NET SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["todo-mvc-application/todo-mvc-application.csproj", "todo-mvc-application/"]
RUN dotnet restore "todo-mvc-application/todo-mvc-application.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "todo-mvc-application/todo-mvc-application.csproj" -c Release -o /app/build

# Publish  the application
FROM build AS publish
RUN dotnet publish "todo-mvc-application/todo-mvc-application.csproj" -c Release -o /app/publish

# Use the base image to run the application
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet", "todo-mvc-application.dll" ]