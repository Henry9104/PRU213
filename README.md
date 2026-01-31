

## ROOT PROJECT STRUCTURE

```
JungleDash/
├── Assets/
│   ├── Scripts/
│   │   ├── Core/
│   │   │   ├── GameManager.cs
│   │   │   ├── LevelManager.cs
│   │   │   └── AudioManager.cs
│   │   │
│   │   ├── Player/
│   │   │   ├── PlayerController.cs
│   │   │   ├── PlayerMovement.cs
│   │   │   ├── PlayerHealth.cs
│   │   │   └── PlayerAnimation.cs
│   │   │
│   │   ├── Enemy/
│   │   │   ├── EnemyController.cs
│   │   │   └── EnemyPatrol.cs
│   │   │
│   │   ├── Environment/
│   │   │   ├── Checkpoint.cs
│   │   │   ├── Hazard.cs
│   │   │   └── MovingPlatform.cs
│   │   │
│   │   ├── UI/
│   │   │   ├── MainMenuUI.cs
│   │   │   ├── PauseMenuUI.cs
│   │   │   ├── GameOverUI.cs
│   │   │   └── HUDController.cs
│   │   │
│   │   └── Systems/
│   │       ├── ScoreSystem.cs
│   │       ├── Collectible.cs
│   │       └── SaveSystem.cs
│   │
│   ├── Scenes/
│   │   ├── MainMenu.unity
│   │   ├── Level1_Jungle.unity
│   │   ├── Level2_Jungle.unity
│   │   ├── Level3_Jungle.unity
│   │   ├── GameOver.unity
│   │   └── Credits.unity
│   │
│   ├── Prefabs/
│   │   ├── Player/
│   │   │   └── Player.prefab
│   │   ├── Enemies/
│   │   │   ├── Enemy.prefab
│   │   │   └── Boss.prefab
│   │   ├── Environment/
│   │   │   ├── Checkpoint.prefab
│   │   │   ├── Spike.prefab
│   │   │   └── MovingPlatform.prefab
│   │   ├── UI/
│   │   │   ├── HUD.prefab
│   │   │   └── PauseMenu.prefab
│   │   └── Collectibles/
│   │       └── Coin.prefab
│   │
│   ├── Art/
│   │   ├── Characters/
│   │   │   ├── Player/
│   │   │   └── Enemies/
│   │   ├── Tilesets/
│   │   │   └── JungleTiles/
│   │   ├── Backgrounds/
│   │   │   └── Jungle/
│   │   └── UI/
│   │       ├── Buttons/
│   │       └── Icons/
│   │
│   ├── Animations/
│   │   ├── Player/
│   │   └── Enemies/
│   │
│   ├── Audio/
│   │   ├── Music/
│   │   └── SFX/
│   │
│   ├── Materials/
│   ├── Tilemaps/
│   ├── Fonts/
│   ├── Resources/   (optional)
│   └── Plugins/
│
├── Packages/
├── ProjectSettings/
├── Library/          (auto-generated)
├── Logs/             (auto-generated)
└── UserSettings/     (auto-generated)
```

---

## NOTES

* The **Assets** folder contains all game-related resources.
* Each level is stored as a separate scene.
* Scripts are organized by responsibility to support teamwork and maintenance.
* Checkpoint and respawn systems are located in `Scripts/Environment` and `Scripts/Core`.

---
