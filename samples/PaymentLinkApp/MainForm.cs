using System;
using System.Windows.Forms;

namespace PaymentLinkApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new OptionsForm().ShowDialog(this);
        }

        private void refreshDataToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
