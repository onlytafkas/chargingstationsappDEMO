---
description: Repository structure and workflow rules.
applyTo: "**/*"
---

# Repository Rules

- Frontend and backend are separate folders.
- No backend code is allowed in the frontend.
- No frontend code is allowed in the backend.
- Future backend code MUST follow a layered structure: `Controllers -> Services -> Repositories`.
- Secrets are stored in environment variables only.
- For local backend development, a gitignored `backend/.env` file is the required local source for backend database secrets and connection settings. It must never be committed.
- If a task involves backend database configuration or local backend database usage, the agent MUST verify that `backend/.env` exists and create it when missing unless the user needs to provide unknown secrets first.
- Future backend startup/configuration code MUST load `backend/.env` before resolving local database configuration.
- Generated files and build artifacts are excluded via .gitignore.

Always review generated code before committing.