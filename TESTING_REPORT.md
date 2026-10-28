# Testing Report — Elevator Control System

> Note: this code has not been built or run yet (no C# toolchain was available while writing it). Run every test below yourself in Visual Studio after cloning, and fill in the "Actual result" column with what you actually saw before you submit.

## Test plan

| # | Test case | Steps | Expected result | Actual result |
|---|---|---|---|---|
| T1 | App starts | Run the app (F5) | Main window opens, both elevators show "Floor 1", both floor statuses show "Floor 1: A, B" | |
| T2 | Internal request moves one elevator | Click "Go to Floor 2" in Elevator A's panel | After ~0.7–1.4s, Elevator A's label shows "Floor 2"; Elevator B unchanged; floor status labels update | |
| T3 | Outside call picks nearest free elevator | With both elevators on Floor 1, click "Call - Floor 2" | Whichever elevator responds moves to Floor 2; the other stays put | |
| T4 | Busy elevator queues a new request | While Elevator A is mid-move to Floor 2, click "Go to Floor 1" on A's panel again | Elevator A finishes its trip to Floor 2 first, then moves to Floor 1 afterwards — it doesn't reverse mid-trip | |
| T5 | Logging on every move | Perform a few moves, then click "View Log" | A row appears for every completed move, newest first, with elevator name, floor, and timestamp | |
| T6 | Log survives restart | Close and reopen the app, click "View Log" | Previous entries are still there (same `Data/elevator.db` file) | |
| T7 | Relative path works after moving the project | Copy the whole `bin\Debug\net8.0-windows` output folder to a different drive/folder and run the exe from there | App still runs and creates/reads `Data/elevator.db` next to the exe, no crash | |
| T8 | DB error is handled, not a crash | Temporarily rename `Data/elevator.db` to a read-only/locked file (or remove write permission), then try moving an elevator | App shows a message box error instead of crashing | |

## Self-assessment matrix

| Task | Sub-task | Possible marks | Self-assessment (Yes/No) | Reference | Mark awarded |
|---|---|---|---|---|---|
| Task 1 | Complete GUI for Task 1 | 10 | Yes | T1 | |
| Task 1 | Event handler skeleton for all GUI buttons | 10 | Yes | T2, T3 | |
| Task 2 | All event handlers function as specified | 10 | Yes | T2, T3, T4 | |
| Task 3 | Database designed and successfully connected | 5 | Yes | T1, T5 | |
| Task 3 | Log information retrieved from DB and displayed in GUI | 5 | Yes | T5 | |
| Task 3 | Log information stored in DB | 5 | Yes | T5, T6 | |
| Task 3 | Disconnected model used (DataAdapter.Update()) | 5 | Yes | Data/LogRepository.cs | |
| Task 3 | Relative paths; logical errors eliminated; exceptions handled | 5 | Yes | T7, T8 | |
| Task 3 | No duplication in DB-related event handlers | 5 | Yes | DOCUMENTATION.md Task 3 | |
| Task 4 | Events from Task 2 animated with delegate + timer | 10 | Yes | T2, T3 | |
| Task 5.1 | GUI/DB separation via BackgroundWorker | 5 | Yes | T5 | |
| Task 5.2 | Two elevators with State + Observer patterns | 15 | Yes | T3, T4 | |
| Task 6 | Testing report includes all expected content | 10 | Yes | this document | |
| **Total** | | **100** | | | |

Fill the "Mark awarded" column in yourself after marking/feedback — that's not something to pre-fill.
