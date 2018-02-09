using Septa.PardakhtVaset.Client;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PaymentLinkApp
{
    public partial class OptionsForm : Form
    {
        public OptionsForm()
        {
            InitializeComponent();
        }

        private void btnCheckApiKey_Click(object sender, EventArgs e)
        {
            try
            {
                var client = new PardakhtVasetClient(new PardakhtVasetClientOptions { ConnectionString = txtConnectionString.Text.Trim(), TablePrefix = "" });
                var result = client.Test(txtApiKey.Text);

                if (result)
                {
                    MessageBox.Show("Api key is valid!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Api key is not valid! Please enter a valid API Key.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTestConnectionString_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = new SqlConnection(txtConnectionString.Text))
                {
                    connection.Open();
                }

                MessageBox.Show("Connection successfull.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
