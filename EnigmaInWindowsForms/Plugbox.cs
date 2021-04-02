using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enigma
{
    /*
     * What do you want to do with the plugbox?
     * 
     * [1] Unplug all the wires.
     * [2] Plug wires.
     * [3] Nothing.
     * 
     *  [1] Unplug all the wires.
     *  
     * 
     *  [2] Plug wires.
     *  
     *   These are the available letters: ABCDEFGHIJKLMNOPQRSTUVWXYZ
     *   Swap: <A>
     *   With: <G>
     *   
     *   ABCDEFGHIJKLMNOPQRSTUVWXYZ
     *   ||||||||||||||||||||||||||
     *   GBCDEFGHIJKLMNOPQRSTUVWXYZ
     *   
     *   Press escape to go back, or another key to plug another wire.
     *   
     */
    public class Plugbox
    {
        public string encryptionKeysLeft { get; set; }
        public string encryptionKeysRight { get; set; }


        public Plugbox()
        {
            encryptionKeysLeft  = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            encryptionKeysRight = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        }
    }
}
