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
    /*
     *  public Form1()
        {
            InitializeComponent();
            keyBoard4.OnKeyBoardKeyClicked += KeyBoard4_OnKeyBoardKeyClicked;
        }

        private void KeyBoard4_OnKeyBoardKeyClicked(object sender, KeyEventArgs e)
        {
            MessageBox.Show(e.KeyCode.ToString());
        }
     * 
     * */
    public partial class UCkeyBoard : UserControl
    {
        public event EventHandler<KeyEventArgs> OnKeyBoardKeyClicked = delegate { };
        public enum KeyBoardType { Keyboard, LampBoard }


        public string KeyOrder { get; private set; }

        private KeyBoardType _keyboardType = KeyBoardType.Keyboard;
        Button[] btnKeys = new Button[26];

        //======================================================================================ctors
        public UCkeyBoard(string aKeyOrder)
        {
            KeyOrder = aKeyOrder;
        }

        public UCkeyBoard()
        {
            //this.KeyOrder = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            KeyOrder = "QWERTZUIOASDFGHJKPYXCVBNML";

            InitializeComponent();

            for (int i = 0; i < btnKeys.Length; i++)
            {
                btnKeys[i] = new Button();
                btnKeys[i].Tag = KeyOrder[i];
                btnKeys[i].Text = KeyOrder[i].ToString();
                btnKeys[i].Click += KeyBoard_Click;

                this.Controls.Add(btnKeys[i]);
            }
            KeyBoard_Resize(null, null);
        }

        //===========================================================================================
        private void KeyBoard_Click(object sender, EventArgs e)
        {
            //Console.WriteLine( ((Button)sender).Tag.ToString() );

            Keys k = (Keys)(char)((Button)sender).Tag;
            OnKeyBoardKeyClicked(this, new KeyEventArgs(k));
        }

        //===========================================================================================
        private void KeyBoard_Resize(object sender, EventArgs e)
        {
            //breedte/hoogte ratio van 400 op 120 aanhouden 
            //in stapjes van 40 per breedte(kgv) om de knoppen (min value =120)
            //en hoogte aanvaardbare waardes te kunnen geven

            this.Width -= (this.Width % 40);
            this.Height = (int)(this.Width * 0.3);


            //knoppen zijn 30 bij breedte 400 bij h van 120
            int BREEDTE_HOOGTE_KNOPPEN = this.Height / 4;

            int HOR_LIJN_1 = (int)(this.Height * 0.25); //30 bij hoogte 120
            int HOR_LIJN_2 = (int)(this.Height * 0.50);   //60 bij hoogte 120
            int HOR_LIJN_3 = (int)(this.Height * 0.75); //90 bij hoogte 120

            int TTBREEDTEKNOP_PLUS_SPATIE = (int)(this.Width * 0.11); //44 bij 400breed
            int AFTREKSPATIE_HOR = (int)(this.Width * 0.055); //20 bij 400breed

            int tmpX, tmpY = 0;
            for (int i = 0; i < btnKeys.Length; i++)
            {
                if (i < 9)
                {
                    tmpY = HOR_LIJN_1 - (BREEDTE_HOOGTE_KNOPPEN / 2);
                    tmpX = ((((i % 26) + 1) * TTBREEDTEKNOP_PLUS_SPATIE) - AFTREKSPATIE_HOR) - (BREEDTE_HOOGTE_KNOPPEN / 2);
                }
                else if (i < 17)
                {
                    tmpY = HOR_LIJN_2 - (BREEDTE_HOOGTE_KNOPPEN / 2);
                    tmpX = ((((i % 26) - 8) * TTBREEDTEKNOP_PLUS_SPATIE)) - (BREEDTE_HOOGTE_KNOPPEN / 2);
                }
                else
                {
                    tmpY = HOR_LIJN_3 - (BREEDTE_HOOGTE_KNOPPEN / 2);
                    tmpX = ((((i % 26) - 16) * TTBREEDTEKNOP_PLUS_SPATIE) - AFTREKSPATIE_HOR) - (BREEDTE_HOOGTE_KNOPPEN / 2);
                }
                btnKeys[i].Location = new Point(tmpX, tmpY);
                btnKeys[i].Size = new Size(BREEDTE_HOOGTE_KNOPPEN, BREEDTE_HOOGTE_KNOPPEN);
                btnKeys[i].Font = new Font("Arial", Height / 10);
            }

        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // UCkeyBoard
            // 
            this.Name = "UCkeyBoard";
            this.Size = new System.Drawing.Size(300, 150);
            this.Load += new System.EventHandler(this.UCkeyBoard_Load);
            this.ResumeLayout(false);

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void UCkeyBoard_Load(object sender, EventArgs e)
        {

        }
    }
}
