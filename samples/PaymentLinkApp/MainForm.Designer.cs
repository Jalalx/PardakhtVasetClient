namespace PaymentLinkApp
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtApiKey = new System.Windows.Forms.TextBox();
            this.btnCheckApiKey = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.paymentLinksGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.paymentLinksGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // txtApiKey
            // 
            this.txtApiKey.Location = new System.Drawing.Point(88, 13);
            this.txtApiKey.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtApiKey.Name = "txtApiKey";
            this.txtApiKey.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtApiKey.Size = new System.Drawing.Size(351, 23);
            this.txtApiKey.TabIndex = 0;
            // 
            // btnCheckApiKey
            // 
            this.btnCheckApiKey.Location = new System.Drawing.Point(320, 47);
            this.btnCheckApiKey.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCheckApiKey.Name = "btnCheckApiKey";
            this.btnCheckApiKey.Size = new System.Drawing.Size(119, 28);
            this.btnCheckApiKey.TabIndex = 1;
            this.btnCheckApiKey.Text = "برسی API Key";
            this.btnCheckApiKey.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "API Key:";
            // 
            // paymentLinksGridView
            // 
            this.paymentLinksGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.paymentLinksGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.paymentLinksGridView.Location = new System.Drawing.Point(12, 87);
            this.paymentLinksGridView.Name = "paymentLinksGridView";
            this.paymentLinksGridView.Size = new System.Drawing.Size(701, 378);
            this.paymentLinksGridView.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(725, 477);
            this.Controls.Add(this.paymentLinksGridView);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCheckApiKey);
            this.Controls.Add(this.txtApiKey);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "پرداخت واسط";
            ((System.ComponentModel.ISupportInitialize)(this.paymentLinksGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtApiKey;
        private System.Windows.Forms.Button btnCheckApiKey;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView paymentLinksGridView;
    }
}

