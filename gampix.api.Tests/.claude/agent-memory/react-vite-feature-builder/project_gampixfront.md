---
name: gampixfront frontend project
description: Tech stack, structure, and conventions for the gampixfront React+Vite frontend of the Gampix sports betting app
type: project
---

Frontend project lives at `C:\Users\LuccaG\source\repos\Gampix_tst\gampixfront`.

**Stack:** React 19, Vite, @tanstack/react-query, axios, react-router-dom (added 2026-03-30), CSS Modules.

**Dark theme palette** (from BetForm.module.css — use as reference for new pages):
- Page background: `#0f0f0f`
- Card/section background: `#1a1a1a`
- Borders: `#2e2e2e` / `#3a3a3a`
- Accent / primary button: `#646cff` (hover: `#535bf2`)
- Body text: `#d4d4d4`
- Muted labels: `#a0a0a0`
- White headings: `#ffffff`

**Key files:**
- `src/api/betsApi.js` — axios functions; `API_BASE_URL` = `VITE_API_URL + "/api"`. Stats endpoint uses `VITE_API_URL + "/stats"` (no `/api` prefix).
- `src/main.jsx` — wraps app in `QueryClientProvider`
- `src/App.jsx` — `BrowserRouter` + `Routes`; routes: `/` → BetForm, `/stats` → StatsPage
- `src/components/BetForm.jsx` + `BetForm.module.css`
- `src/pages/StatsPage.jsx` + `StatsPage.module.css`

**Why:** Stats endpoint is at `/stats`, not `/api/stats` — the ASP.NET API exposes it outside the `/api` prefix.
