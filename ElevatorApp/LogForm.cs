using System;
using System.Windows.Forms;
using ElevatorApp.Data;

namespace ElevatorApp
{
    public partial class LogForm : Form
    {
        private readonly LogRepository _logRepository;

        public LogForm(LogRepository logRepository)
        {
            InitializeComponent();
            _logRepository = logRepository;
            LoadLogs();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadLogs();
        }

        private void LoadLogs()
        {
            try
            {
                gridLogs.DataSource = _logRepository.GetAllLogs();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Could not load log entries: " + ex.Message, "Log Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
