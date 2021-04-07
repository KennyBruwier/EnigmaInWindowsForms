using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace EnigmaInWindowsForms
{
    enum KeyBoardKeys
    {
        Q, W, E, R, T, Z, U, I, O,
        A, S, D, F, G, H, J, K,
        P, Y, X, C, V, B, N, M, L
    }
    public partial class Form1 : Form
    {
        private readonly Font myFont = new Font("Book Antiqua", 7.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
        private readonly Font myFontL = new Font("Book Antiqua", 10.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
        private readonly Font myFontS = new Font("Book Antiqua", 5.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
        private Enigma enigma = new Enigma();
        private TextBox tbInput, tbOutput;
        private List <Label> lblRotors = new List<Label>();
        
        public Form1()
        {
            Width = 843;
            Height = 823;
            SuspendLayout();
            InitializeComponent();

            foreach (Control control in this.Controls)
            {
                switch (control)
                {
                    case Board board: board.InitializeComponent(); break;
                }
            }

            enigmaKeyboard.OnKeyBoardKeyClicked += enigmaKeyboard_OnKeyBoardKeyClicked;
            //BackColor = Color.FromArgb(64, 64, 64); 

            //Pen pen = new Pen(Color.FromArgb(255, 0, 0, 0));
            //e.Graphics.DrawLine(pen, 20, 10, 300, 100);
             ResumeLayout(false);
       }

        private void enigmaKeyboard_OnKeyBoardKeyClicked(object sender, KeyEventArgs e)
        {
            char cEvent = Convert.ToChar(e.KeyCode);
            //MessageBox.Show(e.KeyCode.ToString());

            //(SearchControl("tbKeyboard") as TextBox).Text = Convert.ToChar(enigma.Rotors[0].myRotation + 'A').ToString();
            enigmaLampboard1.OutputChar(enigma.Gebruiken(cEvent));
            enigmaKeyboard.InputChar(cEvent);
        }

        private void Rotorboard_OnKeyBoardKeyClicked(object sender, KeyEventArgs e)
        {
            throw new NotImplementedException();
        }
        private void KeyBoard4_OnKeyBoardKeyClicked(object sender, KeyEventArgs e)
        {
            MessageBox.Show(e.KeyCode.ToString());
        }
        private void CreateResetBtn(Point location, int iSize = 50)
        {
            PictureBox pbReset = new PictureBox
            {
                BackColor = Color.Transparent,
                BackgroundImage = Properties.Resources.reset,
                BackgroundImageLayout = ImageLayout.Stretch,
                Cursor = Cursors.Hand,
                Location = location,
                Name = "pbReset",
                Size = new Size(iSize, iSize),
                SizeMode = PictureBoxSizeMode.StretchImage,
                TabIndex = 0,
                TabStop = false,
            };
            pbReset.Click += new EventHandler(ResetEnigma);
            Controls.Add(pbReset);
        }
        private void ResetEnigma(object sender, EventArgs e)
        {
            enigma.ResetAllRotors();
            LblRotationRefresh();
            TbInOutput_Switch(true);
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void KeyPressed(object sender, EventArgs e)
        {
            Button button = sender as Button;
            char Encrypted = enigma.Gebruiken(Convert.ToChar(button.Text));
            TbInOutput_Switch();
            BtnSwitchEnabled("btnLampboard" + Encrypted);
            tbInput.Text += button.Text;
            tbOutput.Text += Encrypted;
            LblRotationRefresh();
        }
        private void TbInOutput_Switch(bool bReset = false)
        {
            if (bReset)
            {
                if (tbOutput.Text != "<OUTPUT") 
                    if (tbOutput.Text.Length > 0) 
                        BtnSwitchEnabled("btnLampboard" + tbOutput.Text.Substring(tbOutput.Text.Length - 1));
                tbOutput.Text = "<OUTPUT>";
                tbInput.Text = "<INPUT>";
            }
            else
            {
                if (tbOutput.Text == "<OUTPUT>") tbOutput.Text = "";
                else
                {
                    if (tbOutput.Text.Length > 0) 
                        BtnSwitchEnabled("btnLampboard" + tbOutput.Text.Substring(tbOutput.Text.Length - 1));
                }
                if (tbInput.Text == "<INPUT>") tbInput.Text = "";
            }

        }
        private void LblRotationRefresh()
        {
            (SearchControl("lblRotI") as Label).Text = Convert.ToChar(enigma.Rotors[0].myRotation + 'A').ToString();
            (SearchControl("lblRotII") as Label).Text = Convert.ToChar(enigma.Rotors[1].myRotation + 'A').ToString();
            (SearchControl("lblRotIII") as Label).Text = Convert.ToChar(enigma.Rotors[2].myRotation + 'A').ToString();
        }
        private void BtnSwitchEnabled(string btnName)
        {
            foreach (Button button in Controls.OfType<Button>())
            {
                if (button.Name == btnName)
                {
                    button.Enabled = !button.Enabled;
                }    
            }
        }
        private Control SearchControl(string btnName)
        {
            Control controlZero = null;
            foreach (Control control in Controls)
            {
                if (control.Name == btnName)
                    return control;
                switch (control)
                {
                    case UserControl userControl:
                        {
                            foreach (Control control1 in userControl.Controls)
                            {
                                if (control.Name == btnName)
                                    return control;
                            }
                            
                        }break;
                }
            }
            return controlZero;
        }
        private void uCkeyBoard1_Load(object sender, EventArgs e)
        {

        }
        private void enigmaKeyboard1_Load(object sender, EventArgs e)
        {

        }
        private void enigmaKeyboard1_Load_2(object sender, EventArgs e)
        {

        }
        private void entrywheel_Load(object sender, EventArgs e)
        {

        }
        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {

        }
        private void enigmaLampboard1_Load(object sender, EventArgs e)
        {
            
        }
        public void DrawLinePointF(PaintEventArgs e)
        {

            // Create pen.
            Pen blackPen = new Pen(Color.Black, 3);

            // Create points that define line.
            PointF point1 = new PointF(100.0F, 100.0F);
            PointF point2 = new PointF(500.0F, 100.0F);

            // Draw line to screen.
            e.Graphics.DrawLine(blackPen, point1, point2);
        }
    }
}
