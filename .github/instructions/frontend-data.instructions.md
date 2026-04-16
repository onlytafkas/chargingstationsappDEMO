---
description: Data fetching rules for the React frontend.
applyTo: frontend/**
---

# Data Fetching

## Execution model
- Data fetching MUST happen server-side.
- React Client Components MUST NOT fetch data directly.

## Backend communication
- All data access MUST go through the ASP.NET Core backend via HTTP.
- The frontend MUST NOT access the database or ORM directly.

## Helpers
- All HTTP calls MUST be implemented in dedicated helper functions.
- Components MUST NOT call `fetch` directly.

## Server actions
- Files that begin with `"use server"` MUST export only async functions.
- Do NOT export constants, objects, or non-async values from a `"use server"` module.
- Shared types, initial state objects, and helper constants for server actions MUST live in a separate sibling module and be imported into both the server action and the client component.

If client-side fetching seems required, STOP and ask.