# BLOCKING REQUIREMENT

Before generating or modifying ANY code, you MUST read ALL instruction files listed in AGENTS.md. This is non-negotiable and must happen in every conversation turn that involves code changes.

If a repo instruction requires a local setup artifact for the task, such as `backend/.env` for backend database work, treat that artifact as part of the task. Verify it exists and create it when appropriate instead of leaving the requirement as documentation only.

---

# Project Context/Repo-wide

This project is a fullstack application with:
- Frontend: React (Next.js App Router, TypeScript)
- Backend: ASP.NET Core (.NET 8) Web API
- The frontend and backend are separate applications.

## Architectural Principles
- The frontend NEVER accesses the database directly.
- All data access goes through the backend via HTTP.
- Authentication, authorization and data ownership are enforced server-side.
- The frontend is responsible only for UI and UX.

## Validation Workflow
- After generating or modifying code, run the relevant interactive dashboard flow in the browser before considering the task validated.
- Do not treat build, lint, or type-check results as a substitute for interactive browser validation of user-facing behavior.
- If interactive browser validation cannot be performed, state that explicitly in the response together with the blocking reason.

If any task would violate these principles, STOP and ask.