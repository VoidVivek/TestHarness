namespace PartsBreakdown.TestHarness
{
    partial class Form1
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
            this.btnTmsEnabled = new System.Windows.Forms.Button();
            this.btnNonTmsEnabled = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnTmsEnabled
            // 
            this.btnTmsEnabled.Location = new System.Drawing.Point(12, 12);
            this.btnTmsEnabled.Name = "btnTmsEnabled";
            this.btnTmsEnabled.Size = new System.Drawing.Size(103, 23);
            this.btnTmsEnabled.TabIndex = 0;
            this.btnTmsEnabled.Text = "TMS Enabled";
            this.btnTmsEnabled.UseVisualStyleBackColor = true;
            this.btnTmsEnabled.Click += new System.EventHandler(this.btnTmsEnabled_Click);
            // 
            // btnNonTmsEnabled
            // 
            this.btnNonTmsEnabled.Location = new System.Drawing.Point(121, 12);
            this.btnNonTmsEnabled.Name = "btnNonTmsEnabled";
            this.btnNonTmsEnabled.Size = new System.Drawing.Size(103, 23);
            this.btnNonTmsEnabled.TabIndex = 1;
            this.btnNonTmsEnabled.Text = "Non TMS Enabled";
            this.btnNonTmsEnabled.UseVisualStyleBackColor = true;
            this.btnNonTmsEnabled.Click += new System.EventHandler(this.btnNonTmsEnabled_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 305);
            this.Controls.Add(this.btnNonTmsEnabled);
            this.Controls.Add(this.btnTmsEnabled);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Parts Breakdown test harness";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnTmsEnabled;
        private System.Windows.Forms.Button btnNonTmsEnabled;
    }
}

