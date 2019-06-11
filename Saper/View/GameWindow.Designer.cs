namespace Saper.View
{
    partial class GameWindow
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
            this.buttonReset = new System.Windows.Forms.Button();
            this.labelTime = new System.Windows.Forms.Label();
            this.textBoxTime = new System.Windows.Forms.TextBox();
            this.panelGameBoard = new System.Windows.Forms.Panel();
            this.timerGameTime = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(155, 11);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(75, 21);
            this.buttonReset.TabIndex = 5;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.ButtonReset_Click);
            // 
            // labelTime
            // 
            this.labelTime.AutoSize = true;
            this.labelTime.Location = new System.Drawing.Point(27, 14);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(33, 13);
            this.labelTime.TabIndex = 4;
            this.labelTime.Text = "Time:";
            // 
            // textBoxTime
            // 
            this.textBoxTime.Enabled = false;
            this.textBoxTime.Location = new System.Drawing.Point(63, 11);
            this.textBoxTime.Name = "textBoxTime";
            this.textBoxTime.ReadOnly = true;
            this.textBoxTime.Size = new System.Drawing.Size(68, 20);
            this.textBoxTime.TabIndex = 3;
            // 
            // panelGameBoard
            // 
            this.panelGameBoard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelGameBoard.Location = new System.Drawing.Point(10, 47);
            this.panelGameBoard.Name = "panelGameBoard";
            this.panelGameBoard.Size = new System.Drawing.Size(252, 252);
            this.panelGameBoard.TabIndex = 6;
            this.panelGameBoard.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PanelGameBoard_MouseClick);
            // 
            // timerGameTime
            // 
            this.timerGameTime.Interval = 1000;
            // 
            // GameWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(274, 311);
            this.Controls.Add(this.panelGameBoard);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.labelTime);
            this.Controls.Add(this.textBoxTime);
            this.Name = "GameWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GameForm";
            this.Shown += new System.EventHandler(this.GameWindow_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.TextBox textBoxTime;
        private System.Windows.Forms.Panel panelGameBoard;
        private System.Windows.Forms.Timer timerGameTime;
    }
}