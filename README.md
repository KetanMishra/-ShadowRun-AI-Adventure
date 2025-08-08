<<<<<<< HEAD


# Shadow Run: The Lost Relic

**A highly polished third-person Unity adventure game, designed to showcase advanced programming, AI, and game architecture skills for recruiters and studios.**

---

## Portfolio Summary
Shadow Run: The Lost Relic is a complete, professional-quality Unity project that demonstrates:
- Clean, modular C# code with clear separation of concerns (Player, AI, UI, GameManager)
- Advanced AI using NavMesh, state machines, and adaptive difficulty
- Root Motion character controller and smooth, responsive gameplay
- Robust game loop with win/lose conditions, collectibles, and UI feedback
- Industry-standard Unity folder structure and documentation

This project is ready for review by recruiters and can be used as a portfolio centerpiece for gameplay, AI, and Unity development roles.

---

## Game Description
Explore a lush island, avoid or fight AI-driven enemies, and retrieve the lost relic before time runs out. Collect coins for bonus points and use health packs to survive. The game is designed to be small in scope but highly polished, perfect for showcasing your skills to recruiters.


## Features & Technical Highlights
- Third-person Root Motion character controller (walk, run, jump, attack)
- Stamina and health systems with UI feedback
- Melee combat with precise hit detection
- Collectibles: coins, health packs, relic
- AI NPCs with:
	- NavMesh pathfinding
	- State machine (Patrol, Chase, Attack, Search)
	- Raycast vision cone and proximity detection
	- Adaptive difficulty (AI gets faster/more alert as player collects coins)
	- Enemy health and defeat logic
- Robust game loop:
	- Main and side objectives
	- Win/lose conditions (health, timer, relic)
	- Score and timer UI
- Clean, modular, and well-commented C# scripts
- Professional Unity folder structure for easy navigation and review

## Folder Structure
```
Assets/
	Scripts/
		Player/
		AI/
		UI/
		GameManager/
	Scenes/
	Prefabs/
	Models/
	Animations/
	Materials/
	Textures/
	UI/
	Audio/
docs/
```
- All C# scripts are organized in subfolders for clarity.
- Scenes, prefabs, models, animations, materials, textures, UI, and audio are separated for easy management.
- docs/ contains screenshots or GIFs for your portfolio and README.

## Installation & Running
1. Open the project in Unity 2022.3 LTS or newer.
2. Open `Assets/Scenes/MainScene.unity` and press Play.
3. Build for Windows or WebGL via File > Build Settings.

## Controls
- Move: WASD / Left Stick
- Camera: Mouse / Right Stick
- Jump: Space / A
- Sprint: Left Shift / Bumper
- Attack: Left Mouse / X
- Interact: E

## AI System
Enemy NPCs use NavMesh for pathfinding and have vision cones for perception. They adapt to player progress and switch between patrol, chase, attack, and search states.

## Screenshots & Gameplay
Below are screenshots and a gameplay GIF from Shadow Run: The Lost Relic.

| Main Menu         | Exploration         | Combat             |
|-------------------|--------------------|--------------------|
| ![Main Menu](docs/main_menu.png) | ![Exploration](docs/exploration.png) | ![Combat](docs/combat.png) |

| Collecting        | Win Screen         | Lose Screen        |
|-------------------|--------------------|--------------------|
| ![Collecting](docs/collecting.png) | ![Win Screen](docs/win_screen.png) | ![Lose Screen](docs/lose_screen.png) |

**Gameplay GIF:**
![Gameplay](docs/gameplay.gif)

## License
MIT License (see MIT_LICENSE.txt)

---


---

**For more details, see the comments in each script and the docs/ folder for visual references.**
=======
# -ShadowRun-AI-Adventure
A Unity-based interactive 3D maze game featuring collectibles, a countdown timer, dynamic lighting, and win/lose screens. Built with C# for gameplay mechanics, optimized for smooth performance, and designed to showcase core Unity development skills.
>>>>>>> daa5c8a435d564da48d44567d61d7711936383cd
