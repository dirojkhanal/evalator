using System.Drawing;
using System.Windows.Forms;

namespace ElevatorApp
{
    partial class LogForm
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
            gridLogs = new DataGridView();
            btnRefresh = new Button();

            gridLogs.Location = new Point(15, 15);
            gridLogs.Size = new Size(460, 300);
            gridLogs.ReadOnly = true;
            gridLogs.AllowUserToAddRows = false;
            gridLogs.AllowUserToDeleteRows = false;
            gridLogs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            btnRefresh.Text = "Refresh";
            btnRefresh.Location = new Point(15, 325);
            btnRefresh.Size = new Size(100, 30);
            btnRefresh.Click += btnRefresh_Click;

            Text = "Elevator Operation Log";
            ClientSize = new Size(490, 370);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            Controls.Add(gridLogs);
            Controls.Add(btnRefresh);
        }

        private DataGridView gridLogs;
        private Button btnRefresh;
    }
}
