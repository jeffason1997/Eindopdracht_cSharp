using System;

namespace Eindopdracht_cSharp
{
    partial class HomeGUI
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
            this.StartGameLabel = new System.Windows.Forms.Label();
            this.IPLabel = new System.Windows.Forms.Label();
            this.JoinButton = new System.Windows.Forms.Label();
            this.IPfield = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(37, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(561, 95);
            this.label1.TabIndex = 0;
            this.label1.Text = "Connect Four";
            // 
            // StartGameLabel
            // 
            this.StartGameLabel.AutoSize = true;
            this.StartGameLabel.Location = new System.Drawing.Point(279, 287);
            this.StartGameLabel.Name = "StartGameLabel";
            this.StartGameLabel.Size = new System.Drawing.Size(80, 17);
            this.StartGameLabel.TabIndex = 1;
            this.StartGameLabel.Text = "Start Game";
            this.StartGameLabel.Click += new System.EventHandler(this.StartGameLabel_Click);
            this.StartGameLabel.MouseEnter += new System.EventHandler(this.StartGameLabel_MouseEnter);
            this.StartGameLabel.MouseLeave += new System.EventHandler(this.StartGameLabel_MouseLeave);
            // 
            // IPLabel
            // 
            this.IPLabel.AutoSize = true;
            this.IPLabel.Location = new System.Drawing.Point(289, 561);
            this.IPLabel.Name = "IPLabel";
            this.IPLabel.Size = new System.Drawing.Size(46, 17);
            this.IPLabel.TabIndex = 2;
            this.IPLabel.Text = "label2";
            // 
            // JoinButton
            // 
            this.JoinButton.AutoSize = true;
            this.JoinButton.Location = new System.Drawing.Point(279, 315);
            this.JoinButton.Name = "JoinButton";
            this.JoinButton.Size = new System.Drawing.Size(76, 17);
            this.JoinButton.TabIndex = 3;
            this.JoinButton.Text = "Join Game";
            this.JoinButton.Click += new System.EventHandler(this.JoinButton_Click);
            this.JoinButton.MouseEnter += new System.EventHandler(this.JoinButton_MouseEnter);
            this.JoinButton.MouseLeave += new System.EventHandler(this.JoinButton_MouseLeave);
            // 
            // IPfield
            // 
            this.IPfield.Location = new System.Drawing.Point(269, 335);
            this.IPfield.Name = "IPfield";
            this.IPfield.Size = new System.Drawing.Size(100, 22);
            this.IPfield.TabIndex = 4;
            // 
            // HomeGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(650, 587);
            this.Controls.Add(this.IPfield);
            this.Controls.Add(this.JoinButton);
            this.Controls.Add(this.IPLabel);
            this.Controls.Add(this.StartGameLabel);
            this.Controls.Add(this.label1);
            this.Name = "HomeGUI";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

       
        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label StartGameLabel;
        private System.Windows.Forms.Label IPLabel;
        private System.Windows.Forms.Label JoinButton;
        private System.Windows.Forms.TextBox IPfield;
    }
}

