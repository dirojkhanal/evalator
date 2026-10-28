using ElevatorApp;

namespace ElevatorApp.States
{
    public class IdleState : IElevatorState
    {
        public string Name => "Idle";

        public void HandleRequest(Elevator elevator, int floor)
        {
            if (floor == elevator.CurrentFloor)
                return;

            elevator.BeginMove(floor);
        }
    }
}
