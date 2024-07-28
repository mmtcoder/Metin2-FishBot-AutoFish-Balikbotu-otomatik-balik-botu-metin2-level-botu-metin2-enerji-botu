using MusicPlayerApp.Debugs;
using MusicPlayerApp.Sources;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

using System.Windows.Forms;

namespace Metin2AutoFishCSharp.Sources.ChatHandler
{
   
    internal class ChatFileHandler : ChatSentencer
    {
        private static readonly string ChatQuestAnswerPath = FileHandler.FindFolderNameFromBase("ChatQuestionAnswer");
        // "D:\\CSharpMetin2AutoFishing\\MusicPlayerApp\\MusicPlayerApp\\ChatResources\\ChatQuestionAnswer\\ChatQuestAnswer.txt";
        //private static readonly string ChatQuestPathNotWorking = System.Environment.GetEnvironmentVariable("ChatQuestionAnswer");

        private static readonly string DetectWordMark = "&";
        private static readonly string AnswerWordMark = "^";

        public static string DetectedWord = "";
        public ChatFileHandler() 
        {
            
        }

        public void ChatFileWriter(string[] detectWantedWord , string[] answerDetectedWord)
        {
            List<string> storeBeforeContext = new List<string>();  
            StreamReader reader = new StreamReader(ChatQuestAnswerPath + "\\ChatQuestAnswer.txt");
            
            try
            {
                do
                {                
                    //DebugPfCnsl.println(chatFileString);
                    storeBeforeContext.Add(reader.ReadLine());
                }
                while (reader.Peek() != -1);
            }
            catch
            {
                DebugPfCnsl.println("File is empty");
            }
            finally
            {
                reader.Close();
            }

            StreamWriter writer = new StreamWriter(ChatQuestAnswerPath + "\\ChatQuestAnswer.txt");

            for(int i = 0; i < storeBeforeContext.Count; i++)
            {
                if (storeBeforeContext[i] != null && storeBeforeContext[i] != string.Empty)
                {               
                    writer.WriteLine(storeBeforeContext[i]);
                }
               
            }
            writer.WriteLine(DetectWordMark );

            for(int i = 0; i < detectWantedWord.Length; i++)
            {
                          
               writer.WriteLine(detectWantedWord[i]);             
                       
            }
            writer.WriteLine(AnswerWordMark);

            for (int i = 0; i < answerDetectedWord.Length; i++)
            {
                
               writer.WriteLine(answerDetectedWord[i]);
                
            }

            writer.Close();
           
        }

        public void RecordChatToFile(string detectedWord,string[] anotherPlayerSentence, params string[] answeredSentence)
        {
            if(anotherPlayerSentence == null || answeredSentence == null)
            {
                DebugPfCnsl.println("RecordChatToFile method couldn't do anything ");
                return;
            }
            List<string> storeBeforeContext = new List<string>();
            StreamReader reader = new StreamReader(ChatQuestAnswerPath + "\\RecordChats.txt");

            try
            {
                do
                {
                    //DebugPfCnsl.println(chatFileString);
                    storeBeforeContext.Add(reader.ReadLine());
                }
                while (reader.Peek() != -1);
            }
            catch
            {
                DebugPfCnsl.println("File is empty");
            }
            finally
            {
                reader.Close();
            }

            StreamWriter writer = new StreamWriter(ChatQuestAnswerPath + "\\RecordChats.txt");

            for(int k = 0; k < storeBeforeContext.Count; k++)
            {
                writer.WriteLine(storeBeforeContext[k]);
            }

            writer.WriteLine("Diğer Oyuncudan Tespit Edilenler = ");

            for(int i = 0;i < anotherPlayerSentence.Length;i++)
            {
                if(i == 0)
                {
                    writer.WriteLine("İsmi = " + anotherPlayerSentence[0] );
                }
                else
                {
                    writer.WriteLine("Cümlesi = " + anotherPlayerSentence[1] + "     " + DateTime.Now.ToString());
                }
            }

            writer.WriteLine("Tespit edilen kelime = " + detectedWord);
            writer.WriteLine("Verilen Cevap = ");

            for(int i = 0; i < answeredSentence.Length; i++)
            {
                writer.WriteLine(answeredSentence[i] + "      "  + DateTime.Now.ToString());
            }
            
            writer.Close();
        }

        public void RecordWhisperToFile(string detectedWord, string[] anPlayerSentence, params string[] answeredSentence)
        {
            List<string> storeBeforeContext = new List<string>();
            StreamReader reader = new StreamReader(ChatQuestAnswerPath + "\\RecordWhisperChat.txt");

            try
            {
                do
                {
                    //DebugPfCnsl.println(chatFileString);
                    storeBeforeContext.Add(reader.ReadLine());
                }
                while (reader.Peek() != -1);
            }
            catch
            {
                DebugPfCnsl.println("File is empty");
            }
            finally
            {
                reader.Close();
            }

            StreamWriter writer = new StreamWriter(ChatQuestAnswerPath + "\\RecordWhisperChat.txt");

            for (int k = 0; k < storeBeforeContext.Count; k++)
            {
                writer.WriteLine(storeBeforeContext[k]);
            }

            writer.WriteLine("Fısıltı İle Tespit Edilen = ");

            for (int i = 0; i < anPlayerSentence.Length; i++)
            {
                if (i == 0)
                {
                    writer.WriteLine("İsmi = " + anPlayerSentence[0]);
                }
                else
                {
                    writer.WriteLine("Cümlesi = " + anPlayerSentence[1] + "     " + DateTime.Now.ToString());
                }
            }

            writer.WriteLine("Fısıltı Tespit edilen kelime = " + detectedWord);
            writer.WriteLine("Verilen Cevap = ");

            for (int i = 0; i < answeredSentence.Length; i++)
            {
                writer.WriteLine(answeredSentence[i] + "      " + DateTime.Now.ToString());
            }

            writer.Close();
        }

        public static void ReadAllFileForTextBox(RichTextBox textBox)
        {
            StreamReader reader = new StreamReader(ChatQuestAnswerPath + "\\ChatQuestAnswer.txt");
            List<string> listReadLines = new List<string>();
            string storeForTextBox = "";
            string storeLine = "";

            try
            {
                do
                {
                    storeLine = reader.ReadLine();
                    if(storeLine.Equals(DetectWordMark))
                    {
                        textBox.AppendText(Environment.NewLine);//listReadLines.Add(Environment.NewLine);
                        textBox.ForeColor = Color.Red;
                        textBox.AppendText("Tespit Edilmek İstenen Kelimer = ");//listReadLines.Add("Tespit Edilmek İstenen Kelimer = ");
                        textBox.ForeColor = Color.Black;
                        textBox.AppendText(Environment.NewLine);
                        //storeForTextBox += "Tespit Edilmek İstenen Kelimer = " + "\n";
                    }
                    else if(storeLine.Equals(AnswerWordMark))
                    {
                        textBox.AppendText(Environment.NewLine);
                        textBox.ForeColor = Color.Blue;
                        textBox.AppendText("Tespit edilen kelimelere göre cevaplar = ");
                        textBox.ForeColor = Color.Black;
                        textBox.AppendText(Environment.NewLine);
                        //storeForTextBox += "Tespit edilen kelimelere göre cevaplar  = " + "\n";
                    }
                    else
                    {
                        textBox.AppendText((storeLine + ","));
                       // storeForTextBox += storeLine + " ";
                    }
                }
                while (reader.Peek() != -1);
            }
            catch
            {
                DebugPfCnsl.println("File is empty");
                textBox.AppendText("File is empty");
            }
            finally
            {
                reader.Close();
            }

           // return listReadLines.ToArray();
        }

        public string ChatGetAnswerFromFile(string gameChatWords)
        {
            StreamReader reader = new StreamReader(ChatQuestAnswerPath + "\\ChatQuestAnswer.txt");
            List<string> listAnswers = new List<string>();
            string storeALine = "";
            try
            {
                do
                {
                    storeALine = reader.ReadLine();
                    if(storeALine.Equals(DetectWordMark))
                    {
                        string detectWantedWord = "";
                        //Detect another players chats
                        while (reader.Peek() != -1 && !detectWantedWord.Equals(AnswerWordMark))
                        {
                            detectWantedWord = reader.ReadLine();
                            if (Contains(gameChatWords,detectWantedWord))
                            {

                                DebugPfCnsl.println("Detect wanted word result = "+
                                    detectWantedWord);
                                DetectedWord = detectWantedWord;
                                //Detect answerWordMark string for reading answer
                                while (reader.Peek() != -1)
                                {
                                    storeALine = reader.ReadLine();
                                    if (storeALine.Equals(AnswerWordMark))
                                    {
                                        string answerWord = "";
                                        //Read asnwers for asking player sentence
                                        while (reader.Peek() != -1)
                                        {
                                            answerWord = reader.ReadLine();
                                            if (!answerWord.Equals(DetectWordMark))
                                            {
                                               // DebugPfCnsl.println("added list string = "+ 
                                                  //  answerWord);
                                                listAnswers.Add(answerWord);
                                            }
                                            else
                                            {
                                                DebugPfCnsl.println("else detectWordMark working");
                                                if (listAnswers.Count > 0)
                                                {
                                                    string result = listAnswers.ElementAt(TimerGame.MakeRandomValue(0, listAnswers.Count));
                                                    DebugPfCnsl.println("answered to player = " + result);
                                                    return result;
                                                }
                                                else
                                                {
                                                    return string.Empty;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                       
                    }
                }
                while (reader.Peek() != -1);
            }
            catch
            {
                DebugPfCnsl.println("File is empty");
            }
            finally
            {
                reader.Close();
            }

            //DebugPfCnsl.PrintArray(listAnswers.ToArray());

            if(listAnswers.Count > 0 )
            {
                string result = listAnswers.ElementAt(TimerGame.MakeRandomValue(0, listAnswers.Count));
                DebugPfCnsl.println("answered to player 22 = " + result);
                return result;
            }
            else
            {
                return string.Empty;
            }

            
        }
    }
}
