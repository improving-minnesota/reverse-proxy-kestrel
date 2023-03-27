#! /usr/bin/env bash
cd "$(dirname "0")"
export ASPNETCORE_ENVIRONMENT="Development"
./bin/Debug/net6.0/linux-x64/publish/app