using System;
using System.Collections.Generic;

namespace ElevatorApp
{
    // picks whichever elevator can reach the floor soonest without pulling
    // a busy one off its current job
    public class ElevatorDispatcher
    {
        private readonly List<Elevator> _elevators;

        public ElevatorDispatcher(IEnumerable<Elevator> elevators)
        {
            _elevators = new List<Elevator>(elevators);
        }

        public void DispatchExternalCall(int floor)
        {
            Elevator best = null;
            int bestScore = int.MaxValue;

            foreach (var elevator in _elevators)
            {
                int score = Math.Abs(elevator.CurrentFloor - floor);
                if (elevator.IsBusy)
                    score += 10;

                if (score < bestScore)
                {
                    bestScore = score;
                    best = elevator;
                }
            }

            best?.RequestFloor(floor);
        }
    }
}
