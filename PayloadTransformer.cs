using System;
using System.Collections.Generic;
using System.Text;

namespace COMP_3951_BlockForge_TechPro
{
    public class PayloadTransformer
    {
        private int Shift { get; }

        private char CaesarCipher(char c, int offset)
        {
            char insert = c;
            if (c >= 'a' && c <= 'z')
            {
                int difference = c - 'a';
                int shifted = (difference + offset) % 26;
                if (shifted < 0)
                {
                    shifted += 26;
                }
                insert = (char)('a' + shifted);
            }
            else if (c >= 'A' && c <= 'Z')
            {
                int difference = c - 'A';
                int shifted = (difference + offset) % 26;
                if (shifted < 0)
                {
                    shifted += 26;
                }
                insert = (char)('A' + shifted);
            }

            return insert;
        }

        public PayloadTransformer(int shift)
        {
            this.Shift = shift % 26;
            if (this.Shift < 0)
            {
                this.Shift += 26;
            }
        }

        public string Scramble(string input)
        {
            StringBuilder output = new StringBuilder();

            foreach (char c in input)
            {
               output.Append(CaesarCipher(c, Shift));
            }

            return output.ToString();
        }

        public string Unscramble(string input)
        {
            StringBuilder output = new StringBuilder();

            foreach (char c in input)
            {
                output.Append(CaesarCipher(c, -Shift));
            }

            return output.ToString();
        }
    }
}
