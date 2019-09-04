#!/bin/bash

# Usage info
show_help() {
cat << EOF
Usage: ${0##*/} [-ilth]
Runs the service using the local.env configuration
    -i          Runs (i)nteractive which maintains terminal session and shows logs.
    -l          Runs the container based off the 'intelligenceoidcapi' (l)ocal image.
    -t          Defines the (t)ag for the image to pull from DTR. Default value is 'latest-orange'.
    -h          Displays help and exits.
EOF
}

set -e

RED='\e[41;97m'
GREEN='\033[0;32m'
NC='\033[0m'
OPTIND=1
local_image=0
tag='latest'
# Set the docker run args that were being set previously in one set of args instead of two.
run_detached_arg='-id'

watering_image_name=atsnipes1/waterback
watering_container_name=backend

# Resetting OPTIND is necessary if getopts was used previously in the script.
# It is a good idea to make OPTIND local if you process options in a function.

while getopts ilt:h opt; do
    case $opt in
        i)  run_detached_arg='-i' # Remove running detached if the user chooses
            ;;
        l)  local_image=1
            ;;
        t)  tag=$OPTARG
            ;;
        h)
            show_help
            exit 0
            ;;
        *)
            show_help >&2
            exit 1
            ;;
    esac
done
shift "$((OPTIND-1))" # Shift off the options and optional --.

# Switch to the dir where script resides
cd $(dirname "${BASH_SOURCE[0]}")
if [ ! $? -eq 0 ]; then
    echo "CD failed to change to pathway = '$(dirname "${BASH_SOURCE[0]}")', exiting local.sh."
    exit 1
fi

# Run the dependencies
#./dependencies.sh

# Pull Watering image from DTR
if [ $local_image -eq 0 ];then
    watering_image_name=atsnipes1/waterback:$tag

    set +e
    # Try to pull from the dtr
    docker pull $watering_image_name

    # If it failed, try to login and pull again
    if [ $? -ne 0 ]; then
        echo "Please login to pull the watering image from the dtr."
        if [[ $OSTYPE != msys* ]] ; then
            docker login
        else
            winpty docker login
        fi
        set -e
        docker pull $watering_image_name
    else
        set -e
    fi
fi

# Remove any containers that exist w/ the current Intelligence container name
if [ "$(docker ps -a -q -f name="$watering_container_name")" ]; then
    docker rm -vf $watering_container_name > /dev/null 2>&1
fi

echo "Running the watering container."
docker run $run_detached_arg --name $watering_container_name -p 50288:80 -p 44394:443 $watering_image_name
# docker run $run_detached_arg --name $watering_container_name -p 5201:80 -p 44394:443 --env-file local.env -v "/$(pwd)/src/DSI.IntelligenceOIDC.Api/run/local:/app/secrets" --network DV $watering_image_name
# docker run -dt -v "C:\Users\atsni\vsdbg\vs2017u5:/remote_debugger:rw" -v "E:\Code\sideproj\newproj\backend_new\backend\backend:/app" -v "C:\Users\atsni\AppData\Roaming\Microsoft\UserSecrets:/root/.microsoft/usersecrets:ro" -v "C:\Users\atsni\AppData\Roaming\ASP.NET\Https:/root/.aspnet/https:ro" -v "C:\Users\atsni\.nuget\packages\:/root/.nuget/fallbackpackages2" -v "C:\Program Files\dotnet\sdk\NuGetFallbackFolder:/root/.nuget/fallbackpackages" -e "DOTNET_USE_POLLING_FILE_WATCHER=1" -e "ASPNETCORE_ENVIRONMENT=Development" -e "NUGET_PACKAGES=/root/.nuget/fallbackpackages2" -e "NUGET_FALLBACK_PACKAGES=/root/.nuget/fallbackpackages;/root/.nuget/fallbackpackages2" -p 50288:80 -p 44394:443 --entrypoint tail backend:dev -f /dev/null 