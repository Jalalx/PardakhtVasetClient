using System;
using System.Windows.Forms;

namespace PaymentLinkApp
{
    public partial class PaymentLinkForm : Form
    {
        public PaymentLinkForm()
        {
            InitializeComponent();
        }

        private void PaymentLinkForm_Load(object sender, EventArgs e)
        {
            drpExpireAfterDays.SelectedIndex = 0;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            Close();
        }

        public string Description
        {
            get
            {
                return txtDesc.Text.Trim();
            }
        }

        public decimal Amount
        {
            get
            {
                return Convert.ToDecimal(txtAmount.Value);
            }
        }

        public DateTime InvoiceDate
        {
            get
            {
                return txtInvoiceDate.Value;
            }
        }

        public string InvoiceNumber
        {
            get
            {
                return txtInvoiceNo.Text.Trim();
            }
        }

        public ushort ExpiresAfterDays
        {
            get
            {
                return Convert.ToUInt16(drpExpireAfterDays.SelectedValue);
            }
        }
    }
}
