using MusicPlayerApp.Debugs;
using MusicPlayerApp.Sources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metin2AutoFishCSharp.Sources.Inputs
{
    internal class KeyboardTextInput : KeyboardInput
    {
        public void KeySendText(string sentence)
        {
            ScanCodeShort[] scanCodes = ConvertTextToKeyboardValues(sentence);

            foreach(var code in scanCodes)
            {
                KeyPress(code);
            }
        }

        private ScanCodeShort[] ConvertTextToKeyboardValues(string text)
        {
            string lowerText = text.ToLower();
            ScanCodeShort[] scanCodes = new ScanCodeShort[text.Length];

            int counter = 0;
            foreach (char c in lowerText)
            {
                scanCodes[counter++] = ConvertCharToScanCode(c);
            }
            return scanCodes;
        }

        private ScanCodeShort ConvertCharToScanCode(char c) 
        {
            switch (c)
            {
                case '0':
                    return ScanCodeShort.KEY_0;
                case '1':
                    return ScanCodeShort.KEY_1;
                case '2':
                    return ScanCodeShort.KEY_2;
                case '3':
                    return ScanCodeShort.KEY_3;
                case '4':
                    return ScanCodeShort.KEY_4;
                case '5':
                    return ScanCodeShort.KEY_5;
                case '6':
                    return ScanCodeShort.KEY_6;
                case '7':
                    return ScanCodeShort.KEY_7;
                case '8':
                    return ScanCodeShort.KEY_8;
                case '9':
                    return ScanCodeShort.KEY_9;              
                case 'a':
                    return ScanCodeShort.KEY_A;

                case 'b':
                    return ScanCodeShort.KEY_B;

                case 'c':
                    return ScanCodeShort.KEY_C;

                case 'ç':
                    return ScanCodeShort.KEY_TURKISH_C;

                case 'd':
                    return ScanCodeShort.KEY_D;

                case 'e':
                    return ScanCodeShort.KEY_E;

                case 'f':
                    return ScanCodeShort.KEY_F;

                case 'g':
                    return ScanCodeShort.KEY_G;

                case 'ğ':
                    return ScanCodeShort.KEY_TURKISH_G;

                case 'h':
                    return ScanCodeShort.KEY_H;

                case 'ı':
                    return ScanCodeShort.KEY_TURKISH_I;

                case 'i':
                    return ScanCodeShort.KEY_i;

                case 'j':
                    return ScanCodeShort.KEY_J;

                case 'k':
                    return ScanCodeShort.KEY_K;

                case 'l':
                    return ScanCodeShort.KEY_L;

                case 'm':
                    return ScanCodeShort.KEY_M;

                case 'n':
                    return ScanCodeShort.KEY_N;

                case 'o':
                    return ScanCodeShort.KEY_O;

                case 'ö':
                    return ScanCodeShort.KEY_TURKISH_O;

                case 'q':
                    return ScanCodeShort.KEY_Q;

                case 'p':
                    return ScanCodeShort.KEY_P;

                case 'r':
                    return ScanCodeShort.KEY_R;

                case 's':
                    return ScanCodeShort.KEY_S;

                case 'ş':
                    return ScanCodeShort.KEY_Turkish_S;

                case 't':
                    return ScanCodeShort.KEY_T;

                case 'u':
                    return ScanCodeShort.KEY_U;

                case 'ü':
                    return ScanCodeShort.KEY_TURKISH_U;

                case 'v':
                    return ScanCodeShort.KEY_V;

                case 'w':
                    return ScanCodeShort.KEY_W;

                case 'x':
                    return ScanCodeShort.KEY_X;

                case 'y':
                    return ScanCodeShort.KEY_Y;

                case 'z':
                    return ScanCodeShort.KEY_Z;

                default:
                   // DebugPfCnsl.println(c + " char value is not defined");
                    return ScanCodeShort.SPACE;
            }
        }
    }
}
