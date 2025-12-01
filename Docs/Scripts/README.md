# Scripts — Overview

This page documents the `Assets/Scripts/` folder structure and links to per-folder documentation.

Layout

- `Assets/Scripts/Core/` — configuration, ScriptableObject definitions, save/load, RNG, bootstrap and scene name constants.
- `Assets/Scripts/Gameplay/` — player, enemy, projectile, spawners, controllers and UI flow (main menu, pause, end screens).
- `Assets/Scripts/Services/` — Economy, IAP, Analytics, Cosmetic services (mostly service interfaces and simple implementations).
- `Assets/Scripts/Editor/` — project auto-setup and editor utilities.

Docs

- `Docs/Scripts/Core.md` — Core components and ScriptableObjects.
- `Docs/Scripts/Gameplay.md` — Gameplay controllers and flow.
- `Docs/Scripts/Services.md` — Services descriptions and integration points.
- `Docs/Scripts/Editor.md` — Editor-only helpers and auto-setup behavior.

How to add a new script

1. Place feature code in the appropriate folder (`Core`, `Gameplay`, or `Services`).
2. Follow PascalCase for types and methods, explicit method names, and try to keep classes small and focused.
3. If you add new ScriptableObjects, add them to `Assets/Resources/ScriptableObjects/` (auto-setup will generate them on import if needed).
4. Add a short doc or usage note to the relevant `Docs/Scripts/*` page when adding non-trivial systems.
