using System;
using System.ComponentModel;
using System.Windows.Forms;
using ElevatorApp.Data;
using ElevatorApp.Models;

namespace ElevatorApp
{
    public partial class MainForm : Form
    {
        private readonly LogRepository _logRepository;
        private readonly Elevator _elevatorA;
        private readonly Elevator _elevatorB;
        private readonly ElevatorDispatcher _dispatcher;

        public MainForm()
        {
            InitializeComponent();

            _logRepository = new LogRepository();

            _elevatorA = new Elevator("Elevator A", 1);
            _elevatorB = new Elevator("Elevator B", 1);

            _elevatorA.FloorChanged += Elevator_FloorChanged;
            _elevatorB.FloorChanged += Elevator_FloorChanged;
            _elevatorA.MoveCompleted += Elevator_MoveCompleted;
            _elevatorB.MoveCompleted += Elevator_MoveCompleted;

            _dispatcher = new ElevatorDispatcher(new[] { _elevatorA, _elevatorB });

            RefreshDisplays();
        }

        private void btnCallFloor1_Click(object sender, EventArgs e) => _dispatcher.DispatchExternalCall(1);
        private void btnCallFloor2_Click(object sender, EventArgs e) => _dispatcher.DispatchExternalCall(2);

        private void btnA_Floor1_Click(object sender, EventArgs e) => _elevatorA.RequestFloor(1);
        private void btnA_Floor2_Click(object sender, EventArgs e) => _elevatorA.RequestFloor(2);
        private void btnB_Floor1_Click(object sender, EventArgs e) => _elevatorB.RequestFloor(1);
        private void btnB_Floor2_Click(object sender, EventArgs e) => _elevatorB.RequestFloor(2);

        private void btnShowLog_Click(object sender, EventArgs e)
        {
            using (var logForm = new LogForm(_logRepository))
            {
                logForm.ShowDialog(this);
            }
        }

        private void Elevator_FloorChanged(Elevator elevator, int floor)
        {
            RefreshDisplays();
        }

        private void Elevator_MoveCompleted(Elevator elevator, int floor)
        {
            RefreshDisplays();

            var entry = new LogEntry
            {
                ElevatorName = elevator.Name,
                Floor = floor,
                Timestamp = DateTime.Now
            };

            // write the log off the UI thread so the GUI never waits on the DB
            var worker = new BackgroundWorker();
            worker.DoWork += (s, args) => _logRepository.InsertLog((LogEntry)args.Argument);
            worker.RunWorkerAsync(entry);
        }

        private void RefreshDisplays()
        {
            lblA_CurrentFloor.Text = "Floor " + _elevatorA.CurrentFloor;
            lblB_CurrentFloor.Text = "Floor " + _elevatorB.CurrentFloor;

            lblFloor1Status.Text = BuildFloorStatus(1);
            lblFloor2Status.Text = BuildFloorStatus(2);
        }

        private string BuildFloorStatus(int floor)
        {
            bool aHere = _elevatorA.CurrentFloor == floor;
            bool bHere = _elevatorB.CurrentFloor == floor;

            if (aHere && bHere) return "Floor " + floor + ": A, B";
            if (aHere) return "Floor " + floor + ": A";
            if (bHere) return "Floor " + floor + ": B";
            return "Floor " + floor + ": empty";
        }
    }
}
