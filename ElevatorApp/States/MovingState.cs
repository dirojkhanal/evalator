using ElevatorApp;

namespace ElevatorApp.States
{
    public class MovingState : IElevatorState
    {
        public string Name => "Moving";

        public void HandleRequest(Elevator elevator, int floor)
        {
            // already on the way somewhere, don't interrupt the current trip
            elevator.QueueRequest(floor);
        }
    }
}
