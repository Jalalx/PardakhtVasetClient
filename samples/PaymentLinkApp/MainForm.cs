using PaymentLinkApp.Properties;
using Septa.PardakhtVaset.Client;
using System;
using System.Windows.Forms;

namespace PaymentLinkApp
{
    public partial class MainForm : Form
    {
        private PardakhtVasetClient _client = null;

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
            int total;
            var resultset = _client.PaymentLinkRepository.GetAll(0, 20, null, out total);
            paymentLinksGridView.DataSource = resultset;
        }

        private void newLinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var paymentLinkForm = new PaymentLinkForm();
                var result = paymentLinkForm.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    var link = _client.Create(paymentLinkForm.Amount, null, paymentLinkForm.InvoiceNumber,
                        paymentLinkForm.InvoiceDate, paymentLinkForm.ExpiresAfterDays, paymentLinkForm.Description);
                    refreshDataToolStripMenuItem_Click(this, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var options = new PardakhtVasetClientOptions();
            options.ConnectionString = Settings.Default.ConnectionString;
            options.ApiKey = Settings.Default.ApiKey;
            options.Password = Settings.Default.Password;

            options.TablePrefix = "";

            _client = new PardakhtVasetClient(options);
            _client.Init(Settings.Default.ClusterId);

            _client.PaymentLinkNotificationService.PaymentLinkChanged += PaymentLinkNotificationService_PaymentLinkChanged;
            _client.PaymentLinkNotificationService.Start(TimeSpan.FromSeconds(10));

            try
            {
                refreshDataToolStripMenuItem_Click(sender, e);
            }
            catch { }
        }

        private void PaymentLinkNotificationService_PaymentLinkChanged(object sender, PaymentLinkChangedEventArgs e)
        {
            if (e.Status == PardakhtVasetServices.RequestStatus.Paid)
            {
                MessageBox.Show(string.Format("Payment link with token '{0}' and follow id '{1}' is paid at '{2}'", e.Token, e.FollowId, e.ResultDate),
                    "Paid", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            e.Handled = true;
        }

        private void cancelSelectedPaymentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (paymentLinksGridView.SelectedRows.Count == 1)
            {
                var token = paymentLinksGridView.SelectedRows[0].Cells[11].Value as string;
                var result = _client.Cancel(token);
                if (result.Success)
                {
                    MessageBox.Show("Payment cancelled successfully.", "Payment cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(result.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select only one payment to cancel.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
