namespace PaymentLinkApp
{
    partial class OptionsForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnCheckApiKey = new System.Windows.Forms.Button();
            this.txtApiKey = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnTestConnectionString = new System.Windows.Forms.Button();
            this.txtConnectionString = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 14);
            this.label1.TabIndex = 5;
            this.label1.Text = "API Key:";
            // 
            // btnCheckApiKey
            // 
            this.btnCheckApiKey.Location = new System.Drawing.Point(359, 71);
            this.btnCheckApiKey.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCheckApiKey.Name = "btnCheckApiKey";
            this.btnCheckApiKey.Size = new System.Drawing.Size(139, 30);
            this.btnCheckApiKey.TabIndex = 4;
            this.btnCheckApiKey.Text = "برسی API Key";
            this.btnCheckApiKey.UseVisualStyleBackColor = true;
            // 
            // txtApiKey
            // 
            this.txtApiKey.Location = new System.Drawing.Point(19, 34);
            this.txtApiKey.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtApiKey.Name = "txtApiKey";
            this.txtApiKey.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtApiKey.Size = new System.Drawing.Size(479, 22);
            this.txtApiKey.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 14);
            this.label2.TabIndex = 8;
            this.label2.Text = "ارتباط با بانک اطلاعاتی:";
            // 
            // btnTestConnectionString
            // 
            this.btnTestConnectionString.Location = new System.Drawing.Point(358, 163);
            this.btnTestConnectionString.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnTestConnectionString.Name = "btnTestConnectionString";
            this.btnTestConnectionString.Size = new System.Drawing.Size(139, 30);
            this.btnTestConnectionString.TabIndex = 7;
            this.btnTestConnectionString.Text = "برسی ارتباط";
            this.btnTestConnectionString.UseVisualStyleBackColor = true;
            // 
            // txtConnectionString
            // 
            this.txtConnectionString.Location = new System.Drawing.Point(19, 126);
            this.txtConnectionString.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtConnectionString.Name = "txtConnectionString";
            this.txtConnectionString.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtConnectionString.Size = new System.Drawing.Size(478, 22);
            this.txtConnectionString.TabIndex = 6;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(411, 234);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(86, 27);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "ذخیره";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 274);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnTestConnectionString);
            this.Controls.Add(this.txtConnectionString);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCheckApiKey);
            this.Controls.Add(this.txtApiKey);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionsForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "تنظیمات";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCheckApiKey;
        private System.Windows.Forms.TextBox txtApiKey;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnTestConnectionString;
        private System.Windows.Forms.TextBox txtConnectionString;
        private System.Windows.Forms.Button btnSave;
    }
}