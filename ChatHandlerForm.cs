using Metin2AutoFishCSharp.Sources.ChatHandler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Metin2AutoFishCSharp
{
    public partial class ChatHandlerForm : Form
    {
        private ChatFileHandler chatFileHandle;

        private string[] detectingWords;
        private string[] answerWords;
        public ChatHandlerForm()
        {
            InitializeComponent();
            LoadTextFromChatFile();
            chatFileHandle = new ChatFileHandler();
        }

        private void textBoxDetecting_Leave(object sender, EventArgs e)
        {
            if(textBoxDetecting.Text != string.Empty && textBoxDetecting.Text != null)
            {
                detectingWords = textBoxDetecting.Text.Split(new char[] { ',', '.' }, StringSplitOptions.RemoveEmptyEntries);

                if( detectingWords != null && detectingWords.Length < 0 )
                {
                    MessageBox.Show("Lütfen geçerli türde verileri girin. Örnek (bot,hile )",
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("Lütfen tespit edilecek kelimeleri boşluk" +
                    "bırakarak yada virgül işareti ile giriniz (Örnek = bot hile  yada bot,hile",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBoxAnswers_Leave(object sender, EventArgs e)
        {
            if (textBoxAnswers.Text != string.Empty && textBoxAnswers.Text != null)
            {
                answerWords = textBoxAnswers.Text.Split(new char[] { ',', '.' }, StringSplitOptions.RemoveEmptyEntries);

                if ( answerWords != null && answerWords.Length < 0)
                {
                    MessageBox.Show("Lütfen geçerli türde verileri girin. Örnek (ben hile değilim,bot değilim)",
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("Lütfen tespit edilen kelimeleri cevap vermek için ya virgül" +
                    "yada nokta işareti ile ayrınız (Örnek = ben bot değilim,ben hile kullanmıyorum",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonLoad_MouseClick(object sender, MouseEventArgs e)
        {
            if(detectingWords != null && detectingWords.Length > 0)
            {
                if(answerWords != null && answerWords.Length > 0)
                {
                    chatFileHandle.ChatFileWriter(detectingWords, answerWords);
                    LoadTextFromChatFile();
                }
                else
                {
                    MessageBox.Show("Lütfen tespit edilen kelimeye göre cevap verilecek cümleleri yada kelimeleri " +
                        "giriniz.",
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Lütfen tespit edilecek kelimeler kısmının doldurunuz.",
               "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadTextFromChatFile()
        {
            richTextBoxReadFile.Clear();
            ChatFileHandler.ReadAllFileForTextBox(richTextBoxReadFile);
            // string[] resultFileRead = ChatFileHandler.ReadAllFileForTextBox(textBoxReadTexts);

            /* for(int i = 0; i < resultFileRead.Length; i++)
             {              
                 textBoxReadTexts.AppendText(resultFileRead[i]);
             }*/

        }

    }
}
