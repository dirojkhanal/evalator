using System.Collections.Generic;
using System.Windows.Forms;
using ElevatorApp.States;

namespace ElevatorApp
{
    public delegate void FloorChangedHandler(Elevator elevator, int floor);
    public delegate void MoveCompletedHandler(Elevator elevator, int floor);

    public class Elevator
    {
        private readonly Timer _timer;
        private readonly Queue<int> _pendingRequests = new Queue<int>();
        private int _targetFloor;

        public string Name { get; }
        public int CurrentFloor { get; private set; }
        public IElevatorState State { get; private set; }
        public bool IsBusy => State is MovingState;

        public event FloorChangedHandler FloorChanged;
        public event MoveCompletedHandler MoveCompleted;

        public Elevator(string name, int startFloor)
        {
            Name = name;
            CurrentFloor = startFloor;
            State = new IdleState();

            _timer = new Timer();
            _timer.Interval = 700;
            _timer.Tick += Timer_Tick;
        }

        public void RequestFloor(int floor)
        {
            State.HandleRequest(this, floor);
        }

        public void QueueRequest(int floor)
        {
            if (floor != _targetFloor && !_pendingRequests.Contains(floor))
                _pendingRequests.Enqueue(floor);
        }

        public void BeginMove(int floor)
        {
            _targetFloor = floor;
            State = new MovingState();
            _timer.Start();
        }

        private void Timer_Tick(object sender, System.EventArgs e)
        {
            if (CurrentFloor == _targetFloor)
            {
                _timer.Stop();
                State = new IdleState();
                MoveCompleted?.Invoke(this, CurrentFloor);

                if (_pendingRequests.Count > 0)
                    RequestFloor(_pendingRequests.Dequeue());

                return;
            }

            CurrentFloor += CurrentFloor < _targetFloor ? 1 : -1;
            FloorChanged?.Invoke(this, CurrentFloor);
        }
    }
}
