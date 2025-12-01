# Editor — Description of `Assets/Scripts/Editor/`

This folder contains editor-only utilities. The main file is `ProjectAutoSetup.cs` which prepares the project on first import.

`ProjectAutoSetup.cs`

- Responsibilities:
  - Create required `ScriptableObject` assets in `Assets/Resources/ScriptableObjects/` if they do not exist.
  - Create minimal prefabs (primitive-based) used by the prototype.
  - Create required scenes (`Bootstrap`, `MainMenu`, `Arena`, `VictoryScreen`, `DeathScreen`, `PauseScreen`) and set `Bootstrap` as scene #0 in Build Settings.
  - Create an `Input Actions` asset at `Assets/Input/PlayerControls.inputactions` if missing.

- When to re-run:
  - After cloning the repository if assets are missing.
  - If you change the ScriptableObject schema (new fields) and need to regenerate templates.

How to run manually

- Open `ProjectAutoSetup.cs` and find the static setup entry method. You can call it from the editor or reimport the project to trigger it.

Safety notes

- The auto-setup attempts to preserve existing user assets. It only creates defaults when assets are missing.
- Be mindful before deleting generated assets—if you delete an SO that the code expects, the auto-setup will recreate a default, but you may lose tuned values.
