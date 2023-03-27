FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

EXPOSE 5000

WORKDIR /app
# Copy everything 
COPY . ./
# Restore deps as distinct layers
RUN dotnet restore "app.csproj"
# Build
RUN dotnet publish "app.csproj" -c Release -o /app/publish

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
RUN #apt-get update && apt-get install -y libgdiplus
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["/app/app"]