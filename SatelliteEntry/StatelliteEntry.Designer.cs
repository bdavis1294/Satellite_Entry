namespace SatelliteEntry
{
    partial class SatelliteEntry
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BtnRun = new System.Windows.Forms.Button();
            this.BtnAdd = new System.Windows.Forms.Button();
            this.TxtBoxID = new System.Windows.Forms.TextBox();
            this.TxtBoxIdList = new System.Windows.Forms.RichTextBox();
            this.LblID = new System.Windows.Forms.Label();
            this.LblValidateMessage = new System.Windows.Forms.Label();
            this.LblEnteredIds = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BtnRun
            // 
            this.BtnRun.Enabled = false;
            this.BtnRun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnRun.Location = new System.Drawing.Point(442, 405);
            this.BtnRun.Name = "BtnRun";
            this.BtnRun.Size = new System.Drawing.Size(75, 23);
            this.BtnRun.TabIndex = 3;
            this.BtnRun.Text = "Run";
            this.BtnRun.UseVisualStyleBackColor = true;
            this.BtnRun.Click += new System.EventHandler(this.BtnRun_Click);
            // 
            // BtnAdd
            // 
            this.BtnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnAdd.Location = new System.Drawing.Point(236, 48);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(76, 23);
            this.BtnAdd.TabIndex = 1;
            this.BtnAdd.Text = "Add";
            this.BtnAdd.UseVisualStyleBackColor = true;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // TxtBoxID
            // 
            this.TxtBoxID.Location = new System.Drawing.Point(102, 48);
            this.TxtBoxID.MaxLength = 9;
            this.TxtBoxID.Name = "TxtBoxID";
            this.TxtBoxID.Size = new System.Drawing.Size(105, 23);
            this.TxtBoxID.TabIndex = 0;
            this.TxtBoxID.TextChanged += new System.EventHandler(this.TxtBoxID_TextChanged);
            this.TxtBoxID.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TxtBoxID_KeyUp);
            this.TxtBoxID.Validating += new System.ComponentModel.CancelEventHandler(this.TxtBoxID_Validating);
            // 
            // TxtBoxIdList
            // 
            this.TxtBoxIdList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtBoxIdList.Location = new System.Drawing.Point(102, 116);
            this.TxtBoxIdList.Name = "TxtBoxIdList";
            this.TxtBoxIdList.ReadOnly = true;
            this.TxtBoxIdList.Size = new System.Drawing.Size(415, 248);
            this.TxtBoxIdList.TabIndex = 2;
            this.TxtBoxIdList.Text = "";
            this.TxtBoxIdList.TextChanged += new System.EventHandler(this.TxtBoxIdList_TextChanged);
            // 
            // LblID
            // 
            this.LblID.AutoSize = true;
            this.LblID.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblID.Location = new System.Drawing.Point(21, 52);
            this.LblID.Name = "LblID";
            this.LblID.Size = new System.Drawing.Size(65, 15);
            this.LblID.TabIndex = 4;
            this.LblID.Text = "NORAD ID:";
            // 
            // LblValidateMessage
            // 
            this.LblValidateMessage.AutoSize = true;
            this.LblValidateMessage.Location = new System.Drawing.Point(102, 74);
            this.LblValidateMessage.Name = "LblValidateMessage";
            this.LblValidateMessage.Size = new System.Drawing.Size(0, 15);
            this.LblValidateMessage.TabIndex = 5;
            this.LblValidateMessage.Visible = false;
            // 
            // LblEnteredIds
            // 
            this.LblEnteredIds.AutoSize = true;
            this.LblEnteredIds.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblEnteredIds.Location = new System.Drawing.Point(21, 116);
            this.LblEnteredIds.Name = "LblEnteredIds";
            this.LblEnteredIds.Size = new System.Drawing.Size(69, 15);
            this.LblEnteredIds.TabIndex = 4;
            this.LblEnteredIds.Text = "Entered IDs:";
            // 
            // SatelliteEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(597, 450);
            this.Controls.Add(this.LblEnteredIds);
            this.Controls.Add(this.LblValidateMessage);
            this.Controls.Add(this.LblID);
            this.Controls.Add(this.TxtBoxIdList);
            this.Controls.Add(this.TxtBoxID);
            this.Controls.Add(this.BtnAdd);
            this.Controls.Add(this.BtnRun);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Name = "SatelliteEntry";
            this.Text = "SatelliteEntry";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnRun;
        private System.Windows.Forms.Button BtnAdd;
        private System.Windows.Forms.TextBox TxtBoxID;
        private System.Windows.Forms.RichTextBox TxtBoxIdList;
        private System.Windows.Forms.Label LblID;
        private System.Windows.Forms.Label LblValidateMessage;
        private System.Windows.Forms.Label LblEnteredIds;
    }
}

