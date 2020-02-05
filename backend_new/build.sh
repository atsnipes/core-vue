#!/bin/bash

# halt on errors
set -e

cd $( dirname "${BASH_SOURCE[0]}")

SOLUTIONCONFIG=Release
WATERINGAPIDIR=./backend
PROJECTFILE=$WATERINGAPIDIR/backend.csproj

# Remove bin, obj, and publish folder
dotnet clean -c $SOLUTIONCONFIG
rm -rf docker/publish

# Restore Nuget packages
dotnet restore

# Build the release configuration
dotnet build -c $SOLUTIONCONFIG

# Run all unit tests in tests directory
#for x in ./tests/**/*Tests.csproj; do dotnet test -c $SOLUTIONCONFIG --no-build "$x"; done

# Publish the API project to the publish directory
dotnet publish $PROJECTFILE -c $SOLUTIONCONFIG -o ../../docker/publish

# Build the docker image from the resulting publish output
docker build --file ./backend/Dockerfile --no-cache --tag atsnipes1/waterback .

# Push up into the dtr
docker push atsnipes1/waterback
