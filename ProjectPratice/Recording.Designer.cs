namespace ProjectPratice
{
    partial class Recording
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Recording));
            this.btnRecord = new System.Windows.Forms.Button();
            this.btnStopAndSave = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // btnRecord
            // 
            this.btnRecord.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnRecord.FlatAppearance.BorderSize = 0;
            this.btnRecord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRecord.Image = ((System.Drawing.Image)(resources.GetObject("btnRecord.Image")));
            this.btnRecord.Location = new System.Drawing.Point(34, 62);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(84, 93);
            this.btnRecord.TabIndex = 0;
            this.toolTip1.SetToolTip(this.btnRecord, "Record");
            this.btnRecord.UseVisualStyleBackColor = false;
            this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);
            // 
            // btnStopAndSave
            // 
            this.btnStopAndSave.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnStopAndSave.FlatAppearance.BorderSize = 0;
            this.btnStopAndSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStopAndSave.Image = ((System.Drawing.Image)(resources.GetObject("btnStopAndSave.Image")));
            this.btnStopAndSave.Location = new System.Drawing.Point(172, 62);
            this.btnStopAndSave.Name = "btnStopAndSave";
            this.btnStopAndSave.Size = new System.Drawing.Size(84, 93);
            this.btnStopAndSave.TabIndex = 0;
            this.toolTip1.SetToolTip(this.btnStopAndSave, "Stop/Save");
            this.btnStopAndSave.UseVisualStyleBackColor = false;
            this.btnStopAndSave.Click += new System.EventHandler(this.btnStopAndSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(119, 185);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 22);
            this.label1.TabIndex = 1;
            this.label1.Text = "Time";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Recording
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DodgerBlue;
            this.ClientSize = new System.Drawing.Size(289, 266);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnStopAndSave);
            this.Controls.Add(this.btnRecord);
            this.Name = "Recording";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Recording";
            this.Load += new System.EventHandler(this.Recording_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnRecord;
        private System.Windows.Forms.Button btnStopAndSave;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
    }
}