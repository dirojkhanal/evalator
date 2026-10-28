using System;
using System.Drawing;
using System.Windows.Forms;

namespace ElevatorApp
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            btnCallFloor1 = new Button();
            btnCallFloor2 = new Button();
            lblFloor1Status = new Label();
            lblFloor2Status = new Label();
            grpElevatorA = new GroupBox();
            btnA_Floor1 = new Button();
            btnA_Floor2 = new Button();
            lblA_CurrentFloor = new Label();
            grpElevatorB = new GroupBox();
            btnB_Floor1 = new Button();
            btnB_Floor2 = new Button();
            lblB_CurrentFloor = new Label();
            btnShowLog = new Button();

            btnCallFloor1.Text = "Call - Floor 1";
            btnCallFloor1.Location = new Point(20, 20);
            btnCallFloor1.Size = new Size(120, 35);
            btnCallFloor1.Click += btnCallFloor1_Click;

            btnCallFloor2.Text = "Call - Floor 2";
            btnCallFloor2.Location = new Point(20, 65);
            btnCallFloor2.Size = new Size(120, 35);
            btnCallFloor2.Click += btnCallFloor2_Click;

            lblFloor1Status.Location = new Point(160, 28);
            lblFloor1Status.Size = new Size(180, 20);
            lblFloor1Status.Text = "Floor 1: empty";

            lblFloor2Status.Location = new Point(160, 73);
            lblFloor2Status.Size = new Size(180, 20);
            lblFloor2Status.Text = "Floor 2: empty";

            grpElevatorA.Text = "Elevator A - Control Panel";
            grpElevatorA.Location = new Point(20, 120);
            grpElevatorA.Size = new Size(220, 150);

            btnA_Floor1.Text = "Go to Floor 1";
            btnA_Floor1.Location = new Point(15, 30);
            btnA_Floor1.Size = new Size(180, 30);
            btnA_Floor1.Click += btnA_Floor1_Click;

            btnA_Floor2.Text = "Go to Floor 2";
            btnA_Floor2.Location = new Point(15, 65);
            btnA_Floor2.Size = new Size(180, 30);
            btnA_Floor2.Click += btnA_Floor2_Click;

            lblA_CurrentFloor.Location = new Point(15, 105);
            lblA_CurrentFloor.Size = new Size(180, 25);
            lblA_CurrentFloor.Font = new Font(Font, FontStyle.Bold);
            lblA_CurrentFloor.Text = "Floor 1";

            grpElevatorA.Controls.Add(btnA_Floor1);
            grpElevatorA.Controls.Add(btnA_Floor2);
            grpElevatorA.Controls.Add(lblA_CurrentFloor);

            grpElevatorB.Text = "Elevator B - Control Panel";
            grpElevatorB.Location = new Point(260, 120);
            grpElevatorB.Size = new Size(220, 150);

            btnB_Floor1.Text = "Go to Floor 1";
            btnB_Floor1.Location = new Point(15, 30);
            btnB_Floor1.Size = new Size(180, 30);
            btnB_Floor1.Click += btnB_Floor1_Click;

            btnB_Floor2.Text = "Go to Floor 2";
            btnB_Floor2.Location = new Point(15, 65);
            btnB_Floor2.Size = new Size(180, 30);
            btnB_Floor2.Click += btnB_Floor2_Click;

            lblB_CurrentFloor.Location = new Point(15, 105);
            lblB_CurrentFloor.Size = new Size(180, 25);
            lblB_CurrentFloor.Font = new Font(Font, FontStyle.Bold);
            lblB_CurrentFloor.Text = "Floor 1";

            grpElevatorB.Controls.Add(btnB_Floor1);
            grpElevatorB.Controls.Add(btnB_Floor2);
            grpElevatorB.Controls.Add(lblB_CurrentFloor);

            btnShowLog.Text = "View Log";
            btnShowLog.Location = new Point(20, 290);
            btnShowLog.Size = new Size(460, 35);
            btnShowLog.Click += btnShowLog_Click;

            Text = "Elevator Control System";
            ClientSize = new Size(520, 350);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            Controls.Add(btnCallFloor1);
            Controls.Add(btnCallFloor2);
            Controls.Add(lblFloor1Status);
            Controls.Add(lblFloor2Status);
            Controls.Add(grpElevatorA);
            Controls.Add(grpElevatorB);
            Controls.Add(btnShowLog);
        }

        private Button btnCallFloor1;
        private Button btnCallFloor2;
        private Label lblFloor1Status;
        private Label lblFloor2Status;
        private GroupBox grpElevatorA;
        private Button btnA_Floor1;
        private Button btnA_Floor2;
        private Label lblA_CurrentFloor;
        private GroupBox grpElevatorB;
        private Button btnB_Floor1;
        private Button btnB_Floor2;
        private Label lblB_CurrentFloor;
        private Button btnShowLog;
    }
}
