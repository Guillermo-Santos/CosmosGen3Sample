#!/usr/bin/env bash
set -euo pipefail

echo "Running git submodule update --init --recursive"
git submodule update --init --recursive

if [ -d "Cosmos" ]; then
  echo "Entering Cosmos directory and running postCreateCommand.sh if present"
  cd Cosmos
  if [ -x .devcontainer/postCreateCommand.sh ]; then
    .devcontainer/postCreateCommand.sh
  elif [ -f .devcontainer/postCreateCommand.sh ]; then
    bash .devcontainer/postCreateCommand.sh
  else
    echo "No postCreateCommand.sh found in Cosmos; skipping."
  fi
else
  echo "Cosmos directory not found; skipping running its postCreateCommand.sh"
fi
