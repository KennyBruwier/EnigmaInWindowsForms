using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing;

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
            SuspendLayout();
            CreateDualAlfaButtons(new Point(0, 20), "B");
            CreateDualAlfaButtons(new Point(120, 20), "III", true);
            CreateDualAlfaButtons(new Point(240, 20), "II", true);
            CreateDualAlfaButtons(new Point(360, 20), "I", true);
            CreateDualAlfaButtons(new Point(480, 20), "ETW");
            CreateDualAlfaButtons(new Point(600, 20), "Plugboard");
            CreateOldKeyboard(new Point(725, 400), "Keyboard");
            CreateResetBtn(new Point(835, 555));
            CreateOldKeyboard(new Point(725, 200), "Lampboard", false);
            ResumeLayout(false);
            InitializeComponent();
            BackColor = SystemColors.ControlDark;
            Width = 1020;
            Height = 650;
            //Pen pen = new Pen(Color.FromArgb(255, 0, 0, 0));
            //e.Graphics.DrawLine(pen, 20, 10, 300, 100);
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
        private void CreateOldKeyboard(Point point, string name, bool bEnable = true)
        {
            Button button;
            Label label;
            TextBox textbox;
            label = new Label
            {
                Name = "lbl" + ToUpperFirstLetter(TrimSpaces(name)),
                Location = new Point(point.X, point.Y),
                Size = new Size(30 * 9, 30),
                Text = name,
                Font = myFontL,
                Padding = new Padding(3),
                TextAlign = ContentAlignment.TopCenter
            }; 
            Controls.Add(label);
            textbox = new TextBox
            {
                Name = "tb" + ToUpperFirstLetter(TrimSpaces(name)),
                Location = new Point(point.X, point.Y + (bEnable?30:130)),
                Size = new Size(30 * 9, 30),
                Text = bEnable ? "<INPUT>" : "<OUTPUT>",
                Font = myFont,
                Padding = new Padding(3),
                ReadOnly = true,
                TextAlign = HorizontalAlignment.Center
            };
            if (bEnable) tbInput = textbox;
            else tbOutput = textbox;
            Controls.Add(textbox);

            int[] rijLengte = { 9, 8, 9 };
            int XtoAdd = 0;
            int iChar = 0;
            for (int y = 0; y < 3; y++)
            {
                for (int i = 0; i < rijLengte[y]; i++)
                {
                    string currentChar = ((KeyBoardKeys)iChar++).ToString();
                    int X = point.X + (y==1?15:0)+(XtoAdd++ * 30);
                    int Y = point.Y + (bEnable ? 60 : 30) + (y * 30);
                    button = new Button
                    {
                        Name = "btn" + ToUpperFirstLetter(TrimSpaces(name)) + currentChar,
                        Location = new Point(X, Y),
                        Size = new Size(30, 30),
                        TabIndex = i,
                        Text = currentChar,
                        Font = myFont,
                        Enabled = bEnable,
                        UseVisualStyleBackColor = true,
                    };
                    if (bEnable) button.Click += new EventHandler(KeyPressed);
                    Controls.Add(button);
                }
                XtoAdd = 0;
            }
        }
        private void CreateDualAlfaButtons(Point point, string name, bool bRotor = false)
        {
            string trimmedName = ToUpperFirstLetter(TrimSpaces(name));
            if (bRotor)
            {
                Label lblOption = new Label
                {
                    Location = new Point(point.X + 40, point.Y),
                    Padding = new Padding(3),
                    Width = 40,
                    Height = 20,
                    Name = "lblOption" + trimmedName.Substring(0, (trimmedName.Length > 5) ? 5 : trimmedName.Length),
                    Text = "0",
                    Font = myFont,
                    TextAlign = ContentAlignment.MiddleCenter
                };
                Controls.Add(lblOption);

                for (int i = 0; i < 2; i++)
                {
                    Button button = new Button
                    {
                        Location = new Point(point.X + ((i == 0) ? 0 : 80), point.Y),
                        Margin = new Padding(0),
                        Name = (i == 0) ? $"Left{trimmedName}Option" : $"Right{trimmedName}Option",
                        Size = new Size(40, 20),
                        TabIndex = 0,
                        Text = (i == 0) ? "<<" : ">>",
                        Font = myFontS,
                        UseVisualStyleBackColor = true
                    };
                    Controls.Add(button);
                }
                Label lblRotation = new Label
                {
                    Location = new Point(point.X + 20, point.Y + (26 * 20) + 40),
                    Padding = new Padding(2),
                    Width = 80,
                    Height = 25,
                    Name = "lblRot" + trimmedName.Substring(0, (trimmedName.Length > 5) ? 5 : trimmedName.Length),
                    Text = "A",
                    TextAlign = ContentAlignment.MiddleCenter,
                    BackColor = SystemColors.ButtonHighlight,
                    Font = myFontL
                };
                Controls.Add(lblRotation);
            }

            Label lblName = new Label
            {
                Location = new Point(point.X + 40, point.Y + 20),
                Padding = new Padding(3),
                Width = 40,
                Height = 20,
                Name = "lbl" + trimmedName.Substring(0, (trimmedName.Length > 5) ? 5 : trimmedName.Length),
                Text = trimmedName.Substring(0, (trimmedName.Length > 6) ? 6 : trimmedName.Length),
                Font = myFont,
                TextAlign = ContentAlignment.MiddleCenter
            };
            Controls.Add(lblName);
            CreateAlfabetButtons(point, trimmedName);
            CreateAlfabetButtons(new Point(point.X + 100, point.Y), trimmedName, false);
        }
        private void CreateAlfabetButtons(Point point, string name, bool bLeft = true)
        {
            string trimmedName = ToUpperFirstLetter(TrimSpaces(name));
            char cA = 'A';
            Button button = new Button
            {
                Location = new Point(point.X + ((bLeft) ? 0 : -20), point.Y + 20),
                Margin = new Padding(0),
                Name = bLeft ? $"Left{trimmedName}Option" : $"Right{trimmedName}Option",
                Size = new Size(40, 20),
                TabIndex = 0,
                Text = bLeft ? "<<" : ">>",
                Font = myFontS,
                UseVisualStyleBackColor = true
            }; 
            Controls.Add(button);

            for (int i = 0; i < 26; i++)
            {
                button = new Button
                {
                    Location = new Point(point.X, point.Y + 40 + (i * 20)),
                    Name = trimmedName + Convert.ToChar(cA + i).ToString(),
                    Size = new Size(20, 20),
                    TabIndex = i,
                    Text = Convert.ToChar(cA + i).ToString(),
                    Font = myFontS,
                    UseVisualStyleBackColor = true
                };
                Controls.Add(button);
            }
        }
        private string ToUpperFirstLetter(string source)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;
            char[] letters = source.ToCharArray();
            letters[0] = char.ToUpper(letters[0]);
            return new string(letters);
        }
        private string TrimSpaces(string source)
        {
            return String.Concat(source.Where(c => !Char.IsWhiteSpace(c)));
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
            }
            return controlZero;
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
