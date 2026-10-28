using ElevatorApp;

namespace ElevatorApp.States
{
    public interface IElevatorState
    {
        string Name { get; }
        void HandleRequest(Elevator elevator, int floor);
    }
}
