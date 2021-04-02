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
        private TextBox tbInput;
        private TextBox tbOutput;
        private List <Label> lblRotors = new List<Label>();
        
        public Form1()
        {
            SuspendLayout();
            CreateDualAlfaButtons(new Point(0, 30), "B");
            CreateDualAlfaButtons(new Point(120, 30), "III", true);
            CreateDualAlfaButtons(new Point(240, 30), "II", true);
            CreateDualAlfaButtons(new Point(360, 30), "I", true);
            CreateDualAlfaButtons(new Point(480, 30), "ETW");
            CreateDualAlfaButtons(new Point(600, 30), "Plugboard");
            CreateOldKeyboard(new Point(725, 300), "Key board");
            CreateOldKeyboard(new Point(725, 100), "Lamp board", false);
            ResumeLayout(false);
            InitializeComponent();
            BackColor = SystemColors.ControlDark;
            Width = 1020;
            Height = 650;
        }
        public void CreateOldKeyboard(Point point, string name, bool bEnable = true)
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
        public void CreateDualAlfaButtons(Point point, string name, bool bRotor = false)
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
                    Location = new Point(point.X + 40, point.Y + (26 * 20) + 30),
                    Padding = new Padding(3),
                    Width = 40,
                    Height = 20,
                    Name = "lblRot" + trimmedName.Substring(0, (trimmedName.Length > 5) ? 5 : trimmedName.Length),
                    Text = "A",
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = myFont
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
        public void CreateAlfabetButtons(Point point, string name, bool bLeft = true, bool bRotor = false)
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
        public string ToUpperFirstLetter(string source)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;
            char[] letters = source.ToCharArray();
            letters[0] = char.ToUpper(letters[0]);
            return new string(letters);
        }
        public string TrimSpaces(string source)
        {
            return String.Concat(source.Where(c => !Char.IsWhiteSpace(c)));
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        void KeyPressed(object sender, EventArgs e)
        {
            Button button = sender as Button;
            char Encrypted = enigma.Gebruiken(Convert.ToChar(button.Text));
            if (tbInput.Text == "<INPUT>") tbInput.Text = "";
            tbInput.Text += button.Text;
            if (tbOutput.Text == "<OUTPUT>") tbOutput.Text = "";
            else SwitchEnabled("btnLampboard"+tbOutput.Text.Substring(tbOutput.Text.Length - 1));
            tbOutput.Text += Encrypted;
            SwitchEnabled("btnLampboard" + Encrypted);

        }
        private void SwitchEnabled(string btnName)
        {
            foreach (Button button in Controls.OfType<Button>())
            {
                if (button.Name == btnName)
                {
                    button.Enabled = button.Enabled? false: true;
                }    
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
