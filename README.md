# RETURN CENTER

Vertical-slice bootstrap for the reverse-logistics automation game.

## Target
- Unity 6.3 LTS
- Windows PC / Steam
- C#

## First run
1. Open this folder as a Unity project.
2. Let scripts compile.
3. `Assets/ReturnCenter/Scenes/VS_Warehouse.unity` is generated automatically.
4. Open it via **RETURN CENTER > Open Vertical Slice Scene** if Unity did not open it automatically.
5. Enter Play Mode.

## Bootstrap controls
- WASD: move
- Mouse: look
- E: inspect the package
- 1: Resell
- 2: Repair
- 3: Recycle
- Esc: unlock cursor

The first playable proof is intentionally tiny: one return package, one inspection decision, and three disposition routes.

## Verification
Run **RETURN CENTER > Run Core Smoke Tests** in the Unity Editor.

Command-line form once a Unity executable is available:

```text
Unity -batchmode -projectPath <project> -executeMethod ReturnCenter.Editor.ReturnCenterSmokeTests.RunFromCommandLine -quit -logFile -
```

## Status
Source scaffold created outside a Unity runtime environment. Unity compilation, Play Mode behavior, Windows build, and regression verification are **not yet verified**.
