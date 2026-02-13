#!/usr/bin/env bash
# Fetches the OpenAPI spec from a running Wealth Manager API and saves it to openapi/openapi.json.
# Start the API first (e.g. dotnet run or docker compose up), then run this script.

set -e
BASE_URL="${1:-http://localhost:8080}"
OUTPUT_DIR="$(cd "$(dirname "$0")/.." && pwd)/openapi"
OUTPUT_FILE="$OUTPUT_DIR/openapi.json"
mkdir -p "$OUTPUT_DIR"
echo "Fetching OpenAPI spec from $BASE_URL/openapi/v1.json ..."
curl -s "$BASE_URL/openapi/v1.json" -o "$OUTPUT_FILE"
echo "Saved to $OUTPUT_FILE"
