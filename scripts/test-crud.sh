#!/usr/bin/env bash
# Tests all GoldAssets CRUD operations. Requires the API to be running.
# Usage: ./scripts/test-crud.sh [base_url]
# Example: ./scripts/test-crud.sh http://localhost:8080

set -e
BASE="${1:-http://localhost:8080}"

echo "Testing GoldAssets API at $BASE"
echo ""

# 1. CREATE
echo "=== 1. CREATE (POST /api/GoldAssets) ==="
CREATE=$(curl -s -X POST "$BASE/api/GoldAssets" -H "Content-Type: application/json" -d '{"value":100000,"karat":24}')
echo "$CREATE"
ID=$(echo "$CREATE" | sed -n 's/.*"id":"\([^"]*\)".*/\1/p')
if [ -z "$ID" ]; then echo "FAIL: No id in response"; exit 1; fi
echo "Created id: $ID"
echo ""

# 2. READ ALL
echo "=== 2. READ ALL (GET /api/GoldAssets) ==="
curl -s "$BASE/api/GoldAssets" | head -c 400
echo ""
echo ""

# 3. READ ONE
echo "=== 3. READ ONE (GET /api/GoldAssets/$ID) ==="
CODE=$(curl -s -o /tmp/get_one.json -w "%{http_code}" "$BASE/api/GoldAssets/$ID")
cat /tmp/get_one.json | head -c 300
echo ""
echo "Status: $CODE"
echo ""

# 4. UPDATE
echo "=== 4. UPDATE (PUT /api/GoldAssets/$ID) ==="
curl -s -X PUT "$BASE/api/GoldAssets/$ID" -H "Content-Type: application/json" -d '{"value":150000,"karat":22}' | head -c 300
echo ""
echo ""

# 5. DELETE
echo "=== 5. DELETE (DELETE /api/GoldAssets/$ID) ==="
DEL_CODE=$(curl -s -o /dev/null -w "%{http_code}" -X DELETE "$BASE/api/GoldAssets/$ID")
echo "Status: $DEL_CODE (expected 204)"
echo ""

# 6. GET after delete (expect 404)
echo "=== 6. GET after DELETE (expect 404) ==="
GET_CODE=$(curl -s -o /dev/null -w "%{http_code}" "$BASE/api/GoldAssets/$ID")
echo "Status: $GET_CODE (expected 404)"
echo ""

if [ "$DEL_CODE" = "204" ] && [ "$GET_CODE" = "404" ]; then
  echo "All CRUD tests passed."
else
  echo "One or more tests failed."
  exit 1
fi
