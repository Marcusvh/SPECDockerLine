# --- Base runtime image ---
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5109
ENV ASPNETCORE_URLS=http://+:80

RUN apt-get update \
    && apt-get install -y netcat-openbsd \
    && rm -rf /var/lib/apt/lists/*


# --- Build stage ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src

# Install EF tools (SDK image supports dotnet tool)
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

# Copy and restore
COPY ["ExpenseTracker.csproj", "."]
RUN dotnet restore "ExpenseTracker.csproj"

# Copy the rest of the source
COPY . .

# Build
RUN dotnet build "ExpenseTracker.csproj" -c $configuration -o /app/build


# --- Publish stage ---
FROM build AS publish
ARG configuration=Release
RUN dotnet publish "ExpenseTracker.csproj" -c $configuration -o /app/publish /p:UseAppHost=false


# --- Final runtime image ---
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ExpenseTracker.dll"]
