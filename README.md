# Elevator Control System

A WinForms C# app simulating a 2-floor, 2-elevator system with logging to a SQLite database.

## Requirements

- Visual Studio 2022 (Community edition is fine)
- During install, make sure the **".NET desktop development"** workload is checked
- Internet connection on first build (so NuGet can download the SQLite package)

## How to open and run

1. Clone or download this repository.
2. Open `ElevatorApp.sln` in Visual Studio.
3. Let it restore NuGet packages (happens automatically on build; if it doesn't, right-click the solution in Solution Explorer → **Restore NuGet Packages**).
4. Press **F5** (or the green ▶ Start button) to build and run.
5. On first run, the app creates `ElevatorApp\bin\Debug\net8.0-windows\Data\elevator.db` automatically — no manual DB setup needed.

If VS asks which project to set as startup, right-click `ElevatorApp` in Solution Explorer → **Set as Startup Project**.

## Using the app

- **Call - Floor 1 / Floor 2** (top left): simulates someone outside pressing the call button on that floor. The dispatcher sends whichever elevator can get there soonest.
- **Elevator A / Elevator B panels**: each elevator's own internal control panel — "Go to Floor 1/2" requests only move that specific elevator.
- **Floor status labels**: show which elevator (A, B, both, or neither) is currently sitting at each floor.
- **View Log**: opens a window listing every recorded move (elevator name, floor, timestamp), read from the database.

## Project layout

```
ElevatorApp.sln
ElevatorApp/
  ElevatorApp.csproj
  Program.cs                 entry point
  MainForm.cs / .Designer.cs main window + GUI wiring
  LogForm.cs / .Designer.cs  log viewer window
  Elevator.cs                core elevator logic (state, movement, events)
  ElevatorDispatcher.cs       picks which elevator answers an outside call
  States/
    IElevatorState.cs
    IdleState.cs
    MovingState.cs
  Models/
    LogEntry.cs
  Data/
    LogRepository.cs          all database access lives here
```

See `DOCUMENTATION.md` for how each assignment task maps to the code, and `TESTING_REPORT.md` for the test plan and self-assessment matrix.
