using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace EnigmaInWindowsForms
{
    abstract class Board : UserControl
    {
        protected readonly Font myFont = new Font("Book Antiqua", 7.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
        protected readonly Font myFontL = new Font("Book Antiqua", 10.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
        protected readonly Font myFontS = new Font("Book Antiqua", 5.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
        public event EventHandler<KeyEventArgs> OnKeyBoardKeyClicked = delegate { };
        protected TextBox tbDisplay;
        public enum BoardTypes
        {
            Spiegel,
            RotorIII,
            RotorII,
            RotorI,
            EntryWheel,
            Plugboard,
            Keyboard,
            Lampboard
        }
        public virtual BoardTypes Type { get; set; }
        protected string ToUpperFirstLetter(string source)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;
            char[] letters = source.ToCharArray();
            letters[0] = char.ToUpper(letters[0]);
            return new string(letters);
        }
        protected string TrimSpaces(string source)
        {
            return String.Concat(source.Where(c => !Char.IsWhiteSpace(c)));
        }
        protected void KeyPressed(object sender, EventArgs e)
        {
            //Button button = sender as Button;
            //char Encrypted = enigma.Gebruiken(Convert.ToChar(button.Text));
            //TbInOutput_Switch();
            //BtnSwitchEnabled("btnLampboard" + Encrypted);
            //tbInput.Text += button.Text;
            //tbOutput.Text += Encrypted;
            //LblRotationRefresh();
            Keys k = (Keys)(char)((Button)sender).Tag;
            OnKeyBoardKeyClicked(this, new KeyEventArgs(k));
        }
        protected void CreateOldKeyboard(Point point, string name, bool bEnable = true)
        {
            this.Size = new Size(286, 174);
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
                TextAlign = ContentAlignment.TopCenter,
                BackColor = Color.LightGray
            };
            Controls.Add(label);

            textbox = new TextBox
            {
                Name = "tbDisplay_"+ ToUpperFirstLetter(TrimSpaces(name)),
                Location = new Point(point.X, point.Y + (bEnable ? 30 : 130)),
                Size = new Size(30 * 9, 30),
                Text = bEnable ? "<INPUT>" : "<OUTPUT>",
                Font = myFont,
                Padding = new Padding(3),
                ReadOnly = true,
                TextAlign = HorizontalAlignment.Center
            };
            tbDisplay = textbox;
            Controls.Add(tbDisplay);

            int[] rijLengte = { 9, 8, 9 };
            int XtoAdd = 0;
            int iChar = 0;
            for (int y = 0; y < 3; y++)
            {
                for (int i = 0; i < rijLengte[y]; i++)
                {
                    string currentChar = ((KeyBoardKeys)iChar++).ToString();
                    int X = point.X + (y == 1 ? 15 : 0) + (XtoAdd++ * 30);
                    int Y = point.Y + (bEnable ? 60 : 30) + (y * 30);
                    button = new Button
                    {
                        Name = "btn" + ToUpperFirstLetter(TrimSpaces(name)) + currentChar,
                        Tag = Convert.ToChar(currentChar),
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
        public virtual void InitializeComponent()
        {
        }
        protected Control SearchControl(string btnName)
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

                        }
                        break;
                }
            }
            return controlZero;
        }
        protected private void BtnSwitchEnabled(string btnName)
        {
            foreach (Button button in this.Controls.OfType<Button>())
            {
                if (button.Name == btnName)
                {
                    button.Enabled = !button.Enabled;
                }
            }
        }
        private void FindDisplay()
        {
            if (tbDisplay == null)
                tbDisplay = SearchControl("tbDisplay_Keyboard") as TextBox;
            if (tbDisplay == null)
                tbDisplay = SearchControl("tbDisplay_Lampboard") as TextBox;
        }
        public void InputChar(char input, bool bOutput = false)
        {
            if (tbDisplay == null) FindDisplay();
            if (tbDisplay != null)
            {
                tbDisplay_RemoveDefault();
                tbDisplay.Text += input;
            }
        }
        protected void tbDisplay_RemoveDefault(bool bInput = true, bool bReset = false)
        {
            if (bReset)
                tbDisplay.Text = bInput?"<INPUT>":"<OUTPUT>";
            else
                if ((tbDisplay.Text == "<INPUT>") || (tbDisplay.Text == "<OUTPUT>")) tbDisplay.Text = "";
        }
    }
    partial class Rotorboard : Board
    {
        public override BoardTypes Type { get => base.Type; set => base.Type = value; }
        public Rotorboard(BoardTypes type)
        {
            Type = type;
            InitializeComponent();
        }
        public Rotorboard()
        {

        }
        public override void InitializeComponent()
        {
            this.SuspendLayout();
            switch (Type)
            {
                case BoardTypes.Plugboard:
                    CreateDualAlfaButtons(new Point(10, 10), "Plugboard", false);
                    break;
                case BoardTypes.EntryWheel:
                    CreateDualAlfaButtons(new Point(10, 10), "ETW", false);
                    break;
                case BoardTypes.RotorI:
                    CreateDualAlfaButtons(new Point(10, 10), "I", true);
                    break;
                case BoardTypes.RotorII:
                    CreateDualAlfaButtons(new Point(10, 10), "II", true);
                    break;
                case BoardTypes.RotorIII:
                    CreateDualAlfaButtons(new Point(10, 10), "III", true);
                    break;
                case BoardTypes.Spiegel:
                    CreateDualAlfaButtons(new Point(10, 10), "B", false);
                    break;
                default:
                    break;
            }
            //this.Load += new System.EventHandler(this.Rotorboard_Load);
            this.ResumeLayout();
        }
        protected void CreateDualAlfaButtons(Point point, string name, bool bRotor = false)
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
                    Name = "lblOption" + ((trimmedName.Length > 5) ? trimmedName.Substring(0, 5) : trimmedName),
                    Text = "0",
                    Font = myFont,
                    TextAlign = ContentAlignment.MiddleCenter
                };
                this.Controls.Add(lblOption);

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
                    this.Controls.Add(button);
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
                this.Controls.Add(lblRotation);
            }

            Label lblName = new Label
            {
                Location = new Point(point.X + 40, point.Y + 20),
                Padding = new Padding(3),
                Width = 40,
                Height = 20,
                Name = "lbl" + trimmedName.Substring(0, (trimmedName.Length > 5) ? 5 : trimmedName.Length),
                Text = (trimmedName.Length > 6) ? trimmedName.Substring(0, 6) : trimmedName,
                Font = myFont,
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.Controls.Add(lblName);
            CreateAlfabetButtons(point, trimmedName);
            CreateAlfabetButtons(new Point(point.X + 100, point.Y), trimmedName, false);
        }
        protected void CreateAlfabetButtons(Point point, string name, bool bLeft = true)
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
            this.Controls.Add(button);

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
                this.Controls.Add(button);
            }
        }
    }
    partial class EnigmaKeyboard : Board
    {
        public override BoardTypes Type { get => base.Type; set => base.Type = value; }
        public EnigmaKeyboard(BoardTypes type)
        {
            Type = type;
            InitializeComponent();
        }
        public EnigmaKeyboard()
        {

        }
        public override void InitializeComponent()
        {
            this.SuspendLayout();
            this.Visible = false;
            CreateOldKeyboard(new Point(10, 10), "Keyboard");
            //FindDisplays();
            this.Visible = true;
            this.ResumeLayout(false);
        }
    }
    partial class EnigmaLampboard : Board
    {
        public EnigmaLampboard()
        {

        }
        public override void InitializeComponent()
        {
            this.SuspendLayout();
            this.Visible = false;
            CreateOldKeyboard(new Point(10, 10), "Lampboard",false);
            this.Visible = true;
            this.ResumeLayout(false);
        }
        public void OutputChar(char myChar)
        {
            if (tbDisplay.Text != "<OUTPUT>")
                if (tbDisplay.Text.Length > 0)
                {
                    BtnSwitchEnabled("btnLampboard" + tbDisplay.Text.Substring(tbDisplay.Text.Length - 1));
                }
            InputChar(myChar);
            BtnSwitchEnabled("btnLampboard" + myChar);
        }
    }
    public class Enigma
    {
        private int[] selectedRotors = new int[3];
        public Rotor[] Rotors { get; set; }
        public Stecker StekkerDoos { get; set; }
        public Spiegel Spiegel { get; set; }
        public Enigma()
        {
            Opstarten();
        }
        private void Opstarten()
        {
            Rotors = new Rotor[5];
            for (int i = 0; i < 5; i++)
            {
                if (i < 3)
                    Rotors[i] = new Rotor(i+1);
                else
                    Rotors[i] = new Rotor();
                Rotors[i].Permutation = new EnigmaPermutation((EnigmaPermutation.WWII_Permutations)i);
            }
            StekkerDoos = new Stecker();
            Spiegel = new Spiegel();
        }
        public char Gebruiken(char cIn)
        {
                char cOut = StekkerDoos.RotorGebruiken(cIn);
                FindSelectedRotors();
                RotorsKeyStrike();
                for (int i = 0; i < selectedRotors.GetLength(0); i++)
                {
                    cOut = Rotors[selectedRotors[i]].RotorGebruiken(cOut);
                }
                cOut = Spiegel.SpiegelGebruiken(cOut);
                for (int i = 0; i < selectedRotors.GetLength(0); i++)
                {
                    cOut = Rotors[selectedRotors[selectedRotors.GetLength(0) - 1 - i]].RotorGebruiken(cOut, true);
                }
                return StekkerDoos.RotorGebruiken(cOut);
        }
        private void FindSelectedRotors()
        {
            for (int i = 0; i < Rotors.GetLength(0); i++)
            {
                switch (Rotors[i].myCode)
                {
                    case 1:
                        selectedRotors[0] = i;
                        break;
                    case 2:
                        selectedRotors[1] = i;
                        break;
                    case 3:
                        selectedRotors[2] = i;
                        break;
                }
            }
        }
        private void RotorsKeyStrike()
        {
            if ((Rotors[selectedRotors[0]].myRotation + 1) == 25)
            {
                if ((Rotors[selectedRotors[1]].myRotation + 1) == 25)
                {
                    if ((Rotors[selectedRotors[2]].myRotation + 1) == 25)
                    {
                        Rotors[selectedRotors[0]].myRotation = 0;
                        Rotors[selectedRotors[1]].myRotation = 0;
                        Rotors[selectedRotors[2]].myRotation = 0;
                    }
                    else
                    {
                        Rotors[selectedRotors[2]].myRotation++;
                        Rotors[selectedRotors[1]].myRotation = 0;
                    }
                }
                else
                {
                    Rotors[selectedRotors[1]].myRotation++;
                    Rotors[selectedRotors[0]].myRotation = 0;
                }
            }
            else Rotors[selectedRotors[0]].myRotation++;
        }
        public void ResetAllRotors()
        {
            Rotors[selectedRotors[0]].myRotation = 0;
            Rotors[selectedRotors[1]].myRotation = 0;
            Rotors[selectedRotors[2]].myRotation = 0;
        }
    }
    public class EnigmaPermutation
    {
        public enum WWII_Permutations
        {
            RotorI,
            RotorII,
            RotorIII,
            RotorIV,
            RotorV,
            RotorVI,
            RotorVII,
            RotorVIII,
            BetaRotor,
            GammaRotor,
            ReflectorB,
            ReflectorC
        }
        public string Naam { get; private set; }
        private const string encryptionKeysLeft = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public string EncryptionKeysLeft { get { return encryptionKeysLeft; } }
        public string EncryptionKeysRight { get; set; }
        public WWII_Permutations PermutationType { get; set; }
        public EnigmaPermutation(WWII_Permutations type)
        {
            PermutationType = type;
            LoadPermutation();
        }
        public EnigmaPermutation()
        {
            EncryptionKeysRight = EncryptionKeysLeft;
        }
        private void LoadPermutation()
        {
            switch (PermutationType)
            {
                case WWII_Permutations.RotorI:     EncryptionKeysRight = "EKMFLGDQVZNTOWYHXUSPAIBRCJ"; break;
                case WWII_Permutations.RotorII:    EncryptionKeysRight = "AJDKSIRUXBLHWTMCQGZNPYFVOE"; break;
                case WWII_Permutations.RotorIII:   EncryptionKeysRight = "BDFHJLCPRTXVZNYEIWGAKMUSQO"; break;
                case WWII_Permutations.RotorIV:    EncryptionKeysRight = "ESOVPZJAYQUIRHXLNFTGKDCMWB"; break;
                case WWII_Permutations.RotorV:     EncryptionKeysRight = "VZBRGITYUPSDNHLXAWMJQOFECK"; break;
                case WWII_Permutations.RotorVI:    EncryptionKeysRight = "JPGVOUMFYQBENHZRDKASXLICTW"; break;
                case WWII_Permutations.RotorVII:   EncryptionKeysRight = "NZJHGRCXMYSWBOUFAIVLPEKQDT"; break;
                case WWII_Permutations.RotorVIII:  EncryptionKeysRight = "FKQHTLXOCBJSPDZRAMEWNIUYGV"; break;
                case WWII_Permutations.BetaRotor:  EncryptionKeysRight = "LEYJVCNIXWPBQMDRTAKZGFUHOS"; break;
                case WWII_Permutations.GammaRotor: EncryptionKeysRight = "FSOKANUERHMBTIYCWLQPZXVGJD"; break;
                case WWII_Permutations.ReflectorB: EncryptionKeysRight = "YRUHQSLDPXNGOKMIEBFZCWVJAT"; break;
                case WWII_Permutations.ReflectorC: EncryptionKeysRight = "FVPJIAOYEDRZXWGCTKUQSBNMHL"; break;
            }
            Naam = nameof(PermutationType);
        }
    }
    public class Rotor
    {
        /* Change the rotors 
         * 
         * [ R1 ] [ R2 ] [ R3 ]
         * [ 01 ] [ 01 ] [ 01 ]
         * 
         * Would you like to use the setup wizard or the quick notation?
         * 
         * [1] Setup wizard.
         * [2] Quick notation.
         * [3] Nothing.
         * 
         *  [1] Setup wizard
         * 
         *  Pick 3 different rotor pieces.
         * 
         *  [1] Rotor 1.
         *  [2] Rotor 2.
         *  [3] Rotor 3.
         *  [4] Rotor 4.
         *  [5] Rotor 5.
         * 
         *  <strike numberkeys>
         * 
         *  Well done. The rotors are set in place.
         *  Here is a quick recap:
         *  - The 1st rotor is 3, and is set to number 1.
         *  - The 2nd rotor is 4, and is set to number 1.
         *  - The 3rd rotor is 5, and is set to number 1.
         * 
         *  [2] Quick notation.
         *  
         *  Welcome to the Quick Setup.
         *  Write the setup like this: [R1].[R2].[R3].[N1].[N2].[N3]
         *  Example: 5.2.3.13.4.17
         *  Input:
         */
        public string Naam { get; set; }
        public EnigmaPermutation Permutation { get; set; } 
        public int myCode { get; private set; } = -1;
        public int myRotation { get; set; } = 0;
        public Rotor()
        {

        }
        public Rotor(int code)
        {
            myCode = code;
        }
        public char RotorGebruiken (char cIn, bool reverse = false)
        {
            string encryptionKeysRight;
            string encryptionKeysLeft;
            if (reverse)
            {
                encryptionKeysRight = Permutation.EncryptionKeysLeft;
                encryptionKeysLeft = Permutation.EncryptionKeysRight;
            }
            else
            {
                encryptionKeysRight = Permutation.EncryptionKeysRight;
                encryptionKeysLeft = Permutation.EncryptionKeysLeft;
            }
            char nIn = MoveChar(cIn, myRotation);
            for (int i = 0; i < encryptionKeysLeft.Length; i++)
            {
                if (nIn == encryptionKeysLeft[i])
                    return MoveChar(encryptionKeysRight[i],(0-myRotation));
            }
            return ' ';
        }
        private char MoveChar(char cIn, int iPos)
        {
            if ((cIn + iPos) > 'Z')
                return Convert.ToChar('A' + (cIn + (iPos - 1) - 'Z'));
            else
                if ((cIn + iPos < 'A'))
                    return Convert.ToChar('Z' - ('A' - (cIn + iPos+1)));
                return Convert.ToChar(cIn + iPos);
        }
        public void Omwisselen(char cIn, char cOut)
        {
            string encryptionKeysRight = "";
            cIn = Char.ToUpper(cIn);
            cOut = Char.ToUpper(cOut);
            for (int i = 0; i < Permutation.EncryptionKeysRight.Length; i++)
            {
                if (cIn == Permutation.EncryptionKeysLeft[i])
                    encryptionKeysRight += cOut;
                else if (cOut == Permutation.EncryptionKeysLeft[i])
                    encryptionKeysRight += cIn;
                else
                    encryptionKeysRight += encryptionKeysRight[i];
            }
            Permutation.EncryptionKeysRight = encryptionKeysRight;
        }
        public void Reset()
        {
            Permutation.EncryptionKeysRight = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        }
    }
    public class Stecker : Rotor
    {
        public Stecker()
        {
            Permutation = new EnigmaPermutation();
        }
    }
    public class Reflector : Rotor
    {
        public Reflector()
        {
            Permutation = new EnigmaPermutation(EnigmaPermutation.WWII_Permutations.ReflectorB);
        }
        public Reflector(EnigmaPermutation.WWII_Permutations type)
        {
            Permutation = new EnigmaPermutation(type);
        }
    }
    public class Spiegel 
    {
        private Reflector[] Reflectors = new Reflector[2];
        public Spiegel()
        {
            Reflectors[0] = new Reflector(EnigmaPermutation.WWII_Permutations.ReflectorB);
            Reflectors[1] = new Reflector(EnigmaPermutation.WWII_Permutations.ReflectorC);
        }
        public char SpiegelGebruiken(char cIn)
        {
            char reflect = ' ';
            for (int i = 0; i < Reflectors.GetLength(0); i++)
            {
                if ((reflect = Reflectors[i].RotorGebruiken(cIn)) != ' ') break;
            }
            return reflect;
        }
    }
}
