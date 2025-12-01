# Core — Description of `Assets/Scripts/Core/`

This document describes the purpose and usage of core scripts and definitions.

Files and purpose

- `GameConfig.cs` — Central game configuration holder (read-only runtime settings, references to major ScriptableObjects). Use it as a single-source for tuning runtime values when available.

- `RunSettings.cs` — Settings for a single run (difficulty modifiers, timers, XP multipliers). Passed into run-start flow (`SceneBootstrap` / `GameLoopController`).

- `SceneBootstrap.cs` — Scene entry-point. Responsible for selecting the first scene, creating singleton managers (if needed), and triggering the auto-setup when the project is first opened.

- `SceneNames.cs` — String constants for scene names used across the codebase. Prefer using these constants instead of raw strings to avoid typos.

- `PlayerStats.cs` — Holds runtime player stats (HP, movement speed, ammo, etc.). Usually attached to the player or injected into `PlayerController`.

- `SaveService.cs` — Simple JSON-based save and load system. Persist player progress and settings. Key points:
  - Saves JSON under `Application.persistentDataPath`.
  - Serializes relevant `PlayerStats` and unlocked cosmetics/economy info.

- `RNGService.cs` — Random number generator utilities, including weighted RNG and repeatable seeds for reproducible runs (useful for debugging).

- `CurrencyDefinition.cs`, `EconomyConfig.cs`, `IAPProduct.cs` — ScriptableObject definitions used by the Economy and IAP systems. They contain currency names, identifiers, and IAP product mappings.

- `EnemyDefinition.cs`, `WeaponDefinition.cs`, `WaveDefinition.cs` — ScriptableObjects which define enemy types, weapons, and wave configurations. Use the editor to tune values.

- `CosmeticItem.cs` — Defines cosmetic items (IDs, display names, visuals). Cosmetics are lightweight data holders used by `CosmeticService`.

Usage notes

- ScriptableObjects are the primary method for content tuning (weapons, waves, enemies). When you need to add a new enemy or weapon, add a new `*.asset` in `Assets/Resources/ScriptableObjects/` and reference it in the relevant spawner.
- Prefer reading values from `GameConfig`/definition SOs at start and caching them in controllers for runtime performance.
- `SceneNames` avoids hard-coded scene name strings; use `SceneNames.Bootstrap` / `SceneNames.Arena` where available.

Troubleshooting

- If assets are missing after cloning, run the auto-setup (see `Assets/Scripts/Editor/ProjectAutoSetup.cs`) or reimport the project to generate the required `.asset` files and scenes.
