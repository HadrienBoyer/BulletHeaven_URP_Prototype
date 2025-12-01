# Gameplay — Description of `Assets/Scripts/Gameplay/`

This page explains the gameplay-related scripts and the main run flow.

Main controllers & responsibilities

- `SceneBootstrap.cs` (Core) — prepares singletons and selects the first scene.

- `GameLoopController.cs` — Central run manager. Controls wave progression, run timer, and transitions to end screens (`VictoryScreen`, `DeathScreen`). Start/stop run logic lives here.

- `PlayerController.cs` — Player input processing, movement and interaction with `AimController`, `AutoShooter`, and `Health`.

- `AimController.cs` — Converts input (mouse/analog stick) into aiming direction used by the shooter systems.

- `AutoShooter.cs` — Handles automated firing for player or AI-controlled shooters. Read `WeaponDefinition` for rates, spread and damage values.

- `Projectile.cs` — Projectile behaviour: movement, collision detection, applying damage, and lifetime.

- `EnemyController.cs` — Enemy AI and logic. Uses `EnemyDefinition` for stats and optionally spawns projectiles.

- `Spawner.cs` — Spawner logic used by waves to spawn enemies. Reads `WaveDefinition` and uses `RNGService` to pick enemy types.

- `Health.cs` — Component used by player/enemies to track HP and emit death events.

- `AoEController.cs` — Area-of-effect logic for player AoE (grows with XP). Deals damage or applies effects to targets within range.

Secondary controllers & screens

- `MainMenuController.cs`, `PauseController.cs`, `EndScreenController.cs` — UI flow controllers for menu, pause, and end-of-run screens.

- `CutSceneController.cs` — Controls timeline-driven cutscenes.

- `CameraRig.cs` — Camera follow/animations, shake and transitions.

- `ExperienceOrb.cs` — XP pick-ups that the player collects to grow AoE / progress unlocks.

Flow summary

1. `SceneBootstrap` loads `Bootstrap` scene and sets up singletons.
2. `GameLoopController` starts a run using `RunSettings`. Waves are spawned by `Spawner` according to `WaveDefinition`.
3. Player uses `PlayerController` + `AutoShooter` to fight enemies.
4. When player HP hits 0, `GameLoopController` transitions to `DeathScreen`.

Tips for contributors

- To add a new weapon, create a new `WeaponDefinition` ScriptableObject and reference it in the player/enemy shooter.
- To tune waves, edit `WaveDefinition` assets; spawners read them at run start.
- Use `RNGService` for weighted randomness instead of `UnityEngine.Random` for consistent behavior across systems.
