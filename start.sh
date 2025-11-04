#!/bin/bash

# Blog Engine Quick Start Script
# This script helps you get the Blog Engine API up and running quickly

set -e

echo "🚀 Blog Engine Quick Start"
echo "=========================="
echo ""

# Check if .NET is installed
if ! command -v dotnet &> /dev/null; then
    echo "❌ .NET SDK not found!"
    echo "Please install .NET 9.0 SDK from: https://dotnet.microsoft.com/download/dotnet/9.0"
    exit 1
fi

# Check .NET version
DOTNET_VERSION=$(dotnet --version)
echo "✅ Found .NET SDK version: $DOTNET_VERSION"
echo ""

# Navigate to project directory
cd "$(dirname "$0")/DemoDotnetCore"

# Restore packages
echo "📦 Restoring NuGet packages..."
dotnet restore
echo ""

# Build the solution
echo "🔨 Building solution..."
dotnet build --no-restore
echo ""

# Check if database exists
if [ ! -f "BlogEngine.DataModels/blogengine.db" ]; then
    echo "🗄️  Creating database..."
    cd BlogEngine.DataModels
    
    # Install EF tools if not already installed
    if ! dotnet ef --version &> /dev/null; then
        echo "Installing Entity Framework Core tools..."
        dotnet tool install --global dotnet-ef --version 9.0.6
    fi
    
    # Apply migrations
    dotnet ef database update
    echo "✅ Database created successfully"
    cd ..
else
    echo "✅ Database already exists"
fi
echo ""

# Run the application
echo "🎉 Starting Blog Engine API..."
echo ""
echo "The API will be available at:"
echo "  - HTTP:  http://localhost:5000"
echo "  - HTTPS: https://localhost:5001"
echo "  - Swagger: http://localhost:5000/swagger"
echo ""
echo "Press Ctrl+C to stop the server"
echo ""

cd BlogEngine
dotnet run
