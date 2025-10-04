# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution file and restore dependencies
COPY ["MediaCompressor.sln", "./"]

# Copy all project files
COPY ["MediaCompressor.API/MediaCompressor.API.csproj", "MediaCompressor.API/"]
COPY ["MediaCompressor.Application/MediaCompressor.Application.csproj", "MediaCompressor.Application/"]
COPY ["MediaCompressor.Core/MediaCompressor.Core.csproj", "MediaCompressor.Core/"]

# Restore
RUN dotnet restore "MediaCompressor.API/MediaCompressor.API.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/src/MediaCompressor.API"
RUN dotnet publish -c Release -r linux-x64 --self-contained false -o /app/publish

# 🧩 Install SkiaSharp native dependencies
RUN apt-get update && apt-get install -y \
    libfontconfig1 \
    libfreetype6 \
    libpng16-16 \
    libx11-6 \
    libxext6 \
    libxi6 \
    libxrender1 \
    libxrandr2 \
    libxfixes3 \
    && rm -rf /var/lib/apt/lists/*

# Stage 2: Run
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "MediaCompressor.API.dll"]

