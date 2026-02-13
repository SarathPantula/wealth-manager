# OpenAPI & Swagger

- **OpenAPI** = the machine-readable spec (JSON) that describes the API.
- **Swagger** = Swagger UI, the interactive web page to browse and try the API.

| What        | URL (when API is running)        |
|------------|-----------------------------------|
| OpenAPI spec | `/openapi/v1.json` (e.g. http://localhost:8080/openapi/v1.json) |
| Swagger UI   | `/swagger` (e.g. http://localhost:8080/swagger)                 |

**Export spec to file:** Run `./scripts/generate-openapi.sh` (optionally pass base URL). Requires the API to be running.
