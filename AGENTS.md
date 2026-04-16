# Agent Contract/Workflow

## BLOCKING REQUIREMENT — Mandatory pre-work before ANY code generation or modification

You MUST complete ALL of the following steps BEFORE writing, editing, or generating any code. No exceptions.

1. Read `copilot-instructions.md` (root).
2. Read ALL of the following instruction files using the readFile tool:
   - `.github/instructions/backend-dotnet.instructions.md`
   - `.github/instructions/frontend-data.instructions.md`
   - `.github/instructions/frontend-ui.instructions.md`
   - `.github/instructions/repo-structure.instructions.md`
3. If instructions conflict or are incomplete, STOP and ask.
4. Never assume architecture or tools not described here.
5. If the task involves backend database setup, local backend startup, or MySQL configuration, verify whether `backend/.env` exists before making code changes. If it is required and missing, create it from `backend/.env.example` or add it with the required keys unless the user must supply unknown secret values first.

Skipping step 1 or 2 is a violation of this contract. Do not proceed with code generation until all files above have been read in the current conversation turn.

After generating or modifying code:
1. Run the relevant interactive dashboard flow in the browser before considering the task validated.
2. Treat build, lint, and static checks as insufficient on their own for user-facing changes.
3. If the interactive browser flow cannot be run, explicitly report that limitation and why.