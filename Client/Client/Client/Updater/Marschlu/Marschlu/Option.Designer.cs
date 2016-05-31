namespace Marschlu
{
    partial class Option
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Option));
            this.button1 = new System.Windows.Forms.Button();
            this.R1920 = new System.Windows.Forms.RadioButton();
            this.R1600 = new System.Windows.Forms.RadioButton();
            this.R1280 = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(132, 36);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(122, 40);
            this.button1.TabIndex = 0;
            this.button1.Text = "RePatch";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // R1920
            // 
            this.R1920.AutoSize = true;
            this.R1920.Location = new System.Drawing.Point(12, 25);
            this.R1920.Name = "R1920";
            this.R1920.Size = new System.Drawing.Size(78, 17);
            this.R1920.TabIndex = 1;
            this.R1920.TabStop = true;
            this.R1920.Text = "1920x1080";
            this.R1920.UseVisualStyleBackColor = true;
            this.R1920.CheckedChanged += new System.EventHandler(this.R1920_CheckedChanged);
            // 
            // R1600
            // 
            this.R1600.AutoSize = true;
            this.R1600.Location = new System.Drawing.Point(12, 48);
            this.R1600.Name = "R1600";
            this.R1600.Size = new System.Drawing.Size(72, 17);
            this.R1600.TabIndex = 2;
            this.R1600.TabStop = true;
            this.R1600.Text = "1600x900";
            this.R1600.UseVisualStyleBackColor = true;
            this.R1600.CheckedChanged += new System.EventHandler(this.R1600_CheckedChanged);
            // 
            // R1280
            // 
            this.R1280.AutoSize = true;
            this.R1280.Location = new System.Drawing.Point(12, 71);
            this.R1280.Name = "R1280";
            this.R1280.Size = new System.Drawing.Size(72, 17);
            this.R1280.TabIndex = 3;
            this.R1280.TabStop = true;
            this.R1280.Text = "1280x720";
            this.R1280.UseVisualStyleBackColor = true;
            this.R1280.CheckedChanged += new System.EventHandler(this.R1280_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Resolution";
            // 
            // Option
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(264, 96);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.R1280);
            this.Controls.Add(this.R1600);
            this.Controls.Add(this.R1920);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Option";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Options";
            this.Load += new System.EventHandler(this.Option_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton R1920;
        private System.Windows.Forms.RadioButton R1600;
        private System.Windows.Forms.RadioButton R1280;
        private System.Windows.Forms.Label label1;
    }
}