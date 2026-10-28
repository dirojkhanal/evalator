# Documentation — mapping code to the assignment tasks

## Task 1 — GUI (20 marks)

`MainForm.Designer.cs` builds:

- Two outside "Call" buttons (one per floor)
- Two floor status labels (who's currently at each floor)
- Two control panels (one per elevator), each with its own floor buttons and a current-floor display
- One "View Log" button opening `LogForm`

## Task 2 — Control logic (10 marks)

`Elevator.RequestFloor(floor)` is the single entry point for any button press — outside call, or either panel's own button. It delegates to whatever `IElevatorState` the elevator is currently in, and `MainForm` refreshes every label whenever an elevator fires `FloorChanged` or `MoveCompleted`.

## Task 3 — Logging & data (30 marks)

All database code lives in `Data/LogRepository.cs`, used from one place (`MainForm.Elevator_MoveCompleted`) — no repeated SQL in event handlers.

- **Database**: SQLite (`Data/elevator.db`), created automatically with `CREATE TABLE IF NOT EXISTS`. SQLite is used here as the "equivalent" to MS Access the brief allows — it needs no separate install and works identically on any machine running the app.
- **Relative path**: built from `AppDomain.CurrentDomain.BaseDirectory`, never a hardcoded drive path.
- **Disconnected model**: `InsertLog` fills a `DataTable` from a `SQLiteDataAdapter`, adds the new row locally, then commits with `adapter.Update(table)` — not `ExecuteNonQuery`.
- **Exception handling**: both `InsertLog` and `GetAllLogs` wrap their DB work in `try/catch` and rethrow a clear `ApplicationException`; `LogForm` catches that and shows a message box instead of crashing.

## Task 4 — Animation (10 marks)

`Elevator` owns a `System.Windows.Forms.Timer`. `BeginMove` starts it; each `Timer_Tick` moves the elevator one floor closer and raises the `FloorChanged` **delegate-based event** so the GUI updates mid-trip, not just at arrival. `MoveCompleted` (also a delegate) fires once the target floor is reached.

## Task 5.1 — Concurrency (5 bonus marks)

`MainForm.Elevator_MoveCompleted` hands the DB write off to a `BackgroundWorker`, so a slow disk/DB operation never blocks the UI thread.

## Task 5.2 — Two elevators, State + Observer patterns (15 bonus marks)

- **State pattern**: `IElevatorState` / `IdleState` / `MovingState`. `Elevator` doesn't contain `if (isMoving)` checks — it just asks its current state to handle the request, so a busy elevator naturally queues new requests instead of abandoning its trip.
- **Observer pattern**: `Elevator` is the subject — it exposes `FloorChanged` and `MoveCompleted` events. `MainForm` is an observer (updates the GUI). `ElevatorDispatcher` picks the best elevator for an outside call by reading each elevator's live `CurrentFloor`/`IsBusy` state, so a request from outside is never forced onto a busy elevator at the expense of its current passenger.
- Each elevator keeps its own panel for internal requests, so a request made from inside Elevator A can only ever move Elevator A.

## Task 6 — Testing (10 marks)

See `TESTING_REPORT.md`.
