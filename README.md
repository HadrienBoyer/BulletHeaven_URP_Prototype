# Bullet Heaven 3D (Unity 6 URP) — Prototype Jouable

**Ouvre ce dossier comme un projet Unity 6 (URP)**. Au premier import, un script d’auto-setup créera :

- Les **.asset** (ScriptableObjects) nécessaires
- Les **prefabs** (uniquement des primitives Unity)
- Les **scènes** : `Bootstrap`, `MainMenu`, `CutScene`, `Arena`, `VictoryScreen`, `DeathScreen`, `PauseScreen`
- Les entrées du **Build Settings**
- Un **Input Actions** (`Assets/Input/PlayerControls.inputactions`) pour clavier/souris & manette (Xbox)

## Lancer

1. Ouvre le projet dans **Unity 6 URP** (ou plus récent).
2. Patiente pendant l’import des packages (URP, Input System, TMP, Timeline).
3. L’auto-setup va générer les assets/scènes au besoin, puis définir `Bootstrap` en scène #0.
4. Clique **Play** (ou lance le build).

## Contrôles (Input System)

- **Déplacement** : `ZQSD` / `WASD` / **Left Stick**
- **Viser** : Souris vers la direction / **Right Stick**
- **Tirer** : `Click gauche` / **Right Trigger**
- **Dash** : `Space` / **South Button (A)**
- **Pause** : `Esc` / **Start**

## Boucle de jeu

- Run de **3 minutes**. Vise / évite. Le but est de **ne pas tomber à 0 HP**.
- Tir **auto** côté joueur et ennemis. **AoE** du joueur grandit avec l’XP.
- **Waves** configurées via ScriptableObjects, RNG pondérée, spawns progressifs.

## GaaS & IAP (structure prête)

- **Economy** (soft/hard currency), **Cosmetics**, **IAPCatalog** (SO) + services stub.
- Intégration **Unity IAP / UGS** aisée via `IAPService` (interface) et `EconomyService`.

## Juice & DOTween

- Caméras secouées, feedbacks, particules.
- Si **DOTween** est installé, ajoutez le define `TWEEN_DOTWEEN` dans *Player Settings → Scripting Define Symbols* pour activer les tweens (sinon fallback interne).

## Dossiers clés

- `Assets/Scripts/Core`: Config, RNG, sauvegarde JSON, SceneNames, RunSettings, etc.
- `Assets/Scripts/Gameplay`: Player/Enemy/Projectile/AoE/Spawner/Loop.
- `Assets/Scripts/Services`: Economy, Cosmetics, IAP (stubs), Analytics (stub).
- `Assets/Scripts/Editor`: **Auto-setup** (génère assets, prefabs, scènes, Build Settings).
- `Assets/Resources/ScriptableObjects`: **.asset** créés automatiquement.
- `Assets/Input/PlayerControls.inputactions`: maps & bindings.

## Notes

- Le projet utilise **URP**, **Input System**, **TextMeshPro**, **Timeline**. Aucun autre plugin requis.
- Tout est **primitives 3D Unity** (Cube/Sphere/Capsule/Plane) + Materials simples.
- Code **C# 2025** propre : PascalCase, commentaires ciblés, noms explicites, SOLID-lite.

Bon run — et que les dés du RNG soient (généralement) de votre côté.
