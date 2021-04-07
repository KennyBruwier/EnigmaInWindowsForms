
namespace EnigmaInWindowsForms
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
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.enigmaLampboard1 = new EnigmaInWindowsForms.EnigmaLampboard();
            this.rbPlugboard = new EnigmaInWindowsForms.Rotorboard();
            this.rbETW = new EnigmaInWindowsForms.Rotorboard();
            this.rbRotorI = new EnigmaInWindowsForms.Rotorboard();
            this.rbRotorII = new EnigmaInWindowsForms.Rotorboard();
            this.rbRotorIII = new EnigmaInWindowsForms.Rotorboard();
            this.rbSpiegel = new EnigmaInWindowsForms.Rotorboard();
            this.enigmaKeyboard = new EnigmaInWindowsForms.EnigmaKeyboard();
            this.SuspendLayout();
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // enigmaLampboard1
            // 
            this.enigmaLampboard1.BackColor = System.Drawing.Color.Transparent;
            this.enigmaLampboard1.Location = new System.Drawing.Point(412, 615);
            this.enigmaLampboard1.Name = "enigmaLampboard1";
            this.enigmaLampboard1.Size = new System.Drawing.Size(286, 174);
            this.enigmaLampboard1.TabIndex = 12;
            this.enigmaLampboard1.Type = EnigmaInWindowsForms.Board.BoardTypes.Lampboard;
            this.enigmaLampboard1.Load += new System.EventHandler(this.enigmaLampboard1_Load);
            // 
            // rbPlugboard
            // 
            this.rbPlugboard.Location = new System.Drawing.Point(678, 12);
            this.rbPlugboard.Name = "rbPlugboard";
            this.rbPlugboard.Size = new System.Drawing.Size(138, 590);
            this.rbPlugboard.TabIndex = 11;
            this.rbPlugboard.Type = EnigmaInWindowsForms.Board.BoardTypes.Plugboard;
            // 
            // rbETW
            // 
            this.rbETW.Location = new System.Drawing.Point(545, 12);
            this.rbETW.Name = "rbETW";
            this.rbETW.Size = new System.Drawing.Size(139, 590);
            this.rbETW.TabIndex = 10;
            this.rbETW.Type = EnigmaInWindowsForms.Board.BoardTypes.EntryWheel;
            // 
            // rbRotorI
            // 
            this.rbRotorI.Location = new System.Drawing.Point(412, 12);
            this.rbRotorI.Name = "rbRotorI";
            this.rbRotorI.Size = new System.Drawing.Size(150, 590);
            this.rbRotorI.TabIndex = 9;
            this.rbRotorI.Type = EnigmaInWindowsForms.Board.BoardTypes.RotorI;
            // 
            // rbRotorII
            // 
            this.rbRotorII.Location = new System.Drawing.Point(280, 12);
            this.rbRotorII.Name = "rbRotorII";
            this.rbRotorII.Size = new System.Drawing.Size(144, 590);
            this.rbRotorII.TabIndex = 8;
            this.rbRotorII.Type = EnigmaInWindowsForms.Board.BoardTypes.RotorII;
            // 
            // rbRotorIII
            // 
            this.rbRotorIII.Location = new System.Drawing.Point(144, 12);
            this.rbRotorIII.Name = "rbRotorIII";
            this.rbRotorIII.Size = new System.Drawing.Size(142, 590);
            this.rbRotorIII.TabIndex = 7;
            this.rbRotorIII.Type = EnigmaInWindowsForms.Board.BoardTypes.RotorIII;
            // 
            // rbSpiegel
            // 
            this.rbSpiegel.Location = new System.Drawing.Point(12, 12);
            this.rbSpiegel.Name = "rbSpiegel";
            this.rbSpiegel.Size = new System.Drawing.Size(136, 590);
            this.rbSpiegel.TabIndex = 6;
            this.rbSpiegel.Type = EnigmaInWindowsForms.Board.BoardTypes.Spiegel;
            // 
            // enigmaKeyboard
            // 
            this.enigmaKeyboard.BackColor = System.Drawing.Color.Transparent;
            this.enigmaKeyboard.Location = new System.Drawing.Point(114, 615);
            this.enigmaKeyboard.Name = "enigmaKeyboard";
            this.enigmaKeyboard.Size = new System.Drawing.Size(292, 169);
            this.enigmaKeyboard.TabIndex = 5;
            this.enigmaKeyboard.Type = EnigmaInWindowsForms.Board.BoardTypes.Keyboard;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(827, 806);
            this.Controls.Add(this.enigmaLampboard1);
            this.Controls.Add(this.rbPlugboard);
            this.Controls.Add(this.rbETW);
            this.Controls.Add(this.rbRotorI);
            this.Controls.Add(this.rbRotorII);
            this.Controls.Add(this.rbRotorIII);
            this.Controls.Add(this.rbSpiegel);
            this.Controls.Add(this.enigmaKeyboard);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private EnigmaKeyboard enigmaKeyboard;
        private Rotorboard rbSpiegel;
        private Rotorboard rbRotorIII;
        private Rotorboard rbRotorII;
        private Rotorboard rbRotorI;
        private Rotorboard rbETW;
        private Rotorboard rbPlugboard;
        private EnigmaLampboard enigmaLampboard1;
    }
}

