FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy solution and project files first
COPY ["Kanban.Api/Kanban.Api.csproj", "Kanban.Api/"]
COPY ["Kanban.Application/Kanban.Application.csproj", "Kanban.Application/"]
COPY ["Kanban.Domain/Kanban.Domain.csproj", "Kanban.Domain/"]
COPY ["Kanban.Infrastructure/Kanban.Infrastructure.csproj", "Kanban.Infrastructure/"]

# Restore dependencies
RUN dotnet restore "Kanban.Api/Kanban.Api.csproj"

# Copy the rest of the source code
COPY . .

# Build the application
RUN dotnet build "Kanban.Api/Kanban.Api.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "Kanban.Api/Kanban.Api.csproj" -c Release -o /app/publish

# Final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Expose port 80
EXPOSE 80

ENTRYPOINT ["dotnet", "Kanban.Api.dll"]