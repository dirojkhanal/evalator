# Assignment 1 Roadmap — "Control an Elevator" (C# Project)

**Unit:** CIS016-2 / CIS0116-2 | **Weight:** 30% | **Deadline:** 03/11/2026, 10:00 | **Today:** 28/10/2026 → **6 days left**

> ⚠️ **Generative AI use is explicitly "not permitted" on this assignment**, and you must personally demonstrate and explain your source code in Week 7 (missing/failing the demo = automatic fail). This roadmap is a planning aid only — write the code yourself so you can defend it.

---

## 1. What you're building

A WinForms (or WPF) desktop app simulating a 2-floor elevator, with:
- GUI buttons to call/select floors + a status display
- Control logic that moves the "elevator" and updates the GUI
- A logging system backed by a real database (MS Access or equivalent)
- Animated movement via delegates + timers
- Optional: BackgroundWorker concurrency, and a second elevator using State + Observer patterns

## 2. Marks breakdown (100 total)

| Task | What | Marks |
|---|---|---|
| 1 | GUI: request buttons (per floor), control panel (floor buttons, current-floor display, two floor status displays, log button) | 20 |
| 2 | Control logic: button presses move elevator + sync all displays | 10 |
| 3 | Logging & data: DB storage of status+timestamp, display logs in GUI, relative paths, no duplicated DB code, exception handling | 30 |
| 4 | Animate movement with delegates + timers | 10 |
| 5.1 | BackgroundWorker separates GUI from DB ops | 5 (bonus) |
| 5.2 | Two-elevator system using State + Observer patterns, nearest-elevator dispatch | 15 (bonus) |
| 6 | Testing report + self-assessment matrix | 10 |

Task 3 is the single biggest chunk (30 marks) — prioritize getting the DB layer solid and clean before chasing the Task 5 bonus marks (20 combined).

## 3. Architecture plan

- **UI layer:** WinForms form(s) — keep event handlers thin, delegate real work to a `Controller`/`Elevator` class.
- **Domain layer:** `Elevator` class holding current floor, state, and a `Move(int targetFloor)` method. Raises events the GUI subscribes to (keeps GUI and logic decoupled — this also sets you up for Task 5.2's Observer pattern).
- **Data layer:** one `LogRepository` (or similar) class wrapping all Access/ADO.NET calls — `Insert(LogEntry)`, `GetAll()`. This is where "avoid duplication in DB-related event handlers" gets satisfied: every event handler calls this one class, never writes raw SQL inline.
  - Use **relative paths** to the `.accdb` file (e.g. `Path.Combine(Application.StartupPath, "Data", "elevator.mdb")`), not a hardcoded `C:\...` path.
  - Use the **disconnected model**: `DataAdapter` + `DataSet`/`DataTable`, and commit with `DataAdapter.Update()` — not `ExecuteNonQuery()`. This is an explicit rubric line item (5 marks).
  - Wrap DB calls in `try/catch` with meaningful handling (not empty catch blocks) — "robustness" marks depend on this.
- **Animation:** a `System.Windows.Forms.Timer` ticking to move a visual marker floor-by-floor; trigger via a delegate/event raised from the `Elevator` class so Task 2's logic and Task 4's animation stay separate concerns.
- **Task 5.1 (bonus):** move DB reads/writes into a `BackgroundWorker.DoWork` handler; update GUI only in `RunWorkerCompleted`/`ProgressChanged` (never touch controls from the worker thread directly).
- **Task 5.2 (bonus):** 
  - **State pattern:** encapsulate elevator states (Idle, Moving, DoorOpen, etc.) as classes implementing a common interface, so `Elevator` delegates behavior to its current state object.
  - **Observer pattern:** a dispatcher that both elevators' panels notify when a request comes in; dispatcher picks whichever elevator has the shortest estimated time without interrupting its current task.

## 4. 6-day timeline (28/10 → 03/11)

| Day | Focus | Target marks locked in |
|---|---|---|
| **Day 1 (Tue 28/10)** | Scaffold project, design GUI layout (Task 1), wire up basic button click handlers with no logic yet | Task 1 shape done |
| **Day 2 (Wed 29/10)** | Implement control logic (Task 2): moving between 2 floors, syncing all displays | Tasks 1–2 done (30 marks) |
| **Day 3 (Thu 30/10)** | Set up Access DB + `LogRepository`, disconnected model, relative path, insert on every move | Task 3 core (20/30) |
| **Day 4 (Fri 31/10)** | Finish Task 3: display log in GUI via log button, exception handling pass, de-duplicate DB code; start Task 4 timer/delegate animation | Task 3 complete (30) + Task 4 started |
| **Day 5 (Sat 01/11)** | Finish Task 4; if time allows, Task 5.1 BackgroundWorker; otherwise skip straight to testing | Task 4 done (10), stretch: 5.1 |
| **Day 6 (Sun 02/11)** | Write the testing report + fill in the self-assessment matrix; full regression pass; package per naming convention | Task 6 done (10) |
| **Submission morning (Mon 03/11, before 10am)** | Final zip, check naming convention `studentID_AssignmentOne.zip`, upload to BREO, double-check it opens cleanly on another machine | — |

Task 5.2 (the 15-mark multi-elevator bonus) only fits if Days 1–4 go faster than planned — it's a substantial rewrite of the control logic. Treat it as a stretch goal, not a day-1 commitment, given the timeline.

## 5. Submission checklist

- [ ] Complete C# project folder (all source + solution files)
- [ ] Database file included, referenced via **relative path**
- [ ] Testing report (Word/PDF) covering validation + evaluation
- [ ] Self-assessment matrix filled in (Yes/No + reference to testing report + your own mark per row)
- [ ] Zipped as `studentID_AssignmentOne.zip`
- [ ] Uploaded to BREO → Assignment 1 folder → "Assignment One C#"
- [ ] Confirmed Week 7 demo slot and can walk through every line you're presenting

## 6. Self-check before you submit

- Does every DB-touching event handler call through one shared repository method (no copy-pasted SQL)? → duplication marks
- Does the DB path work if the project folder is moved to a different drive? → portability marks
- Can you trigger an exception deliberately (e.g. DB file missing) and does the app show a message instead of crashing? → robustness marks
- Can you explain, out loud, what each class does without looking at notes? → demo readiness
