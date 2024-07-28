using MusicPlayerApp.Debugs;
using MusicPlayerApp.Sources.ImageHandle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metin2AutoFishCSharp.Sources.ChatHandler
{
    internal class ChatSentencer
    {

        

        public ChatSentencer()
        {
            
        }

        public bool Contains(string sentence,string findWord)
        {
            string[] parsedSentence = sentence.Split(new char[] { ' ', }, StringSplitOptions.RemoveEmptyEntries);
           // DebugPfCnsl.PrintArray(parsedSentence);

            string[] parseFindWord = findWord.Split(new char[] { ' ', }, StringSplitOptions.RemoveEmptyEntries);

            int lenghtFindWordParsed = 0;

            if(parseFindWord.Length > parsedSentence.Length )return false;

            for(int indexFind =0; indexFind < parseFindWord.Length; indexFind++)
            {
                for(int indexSentence =0;  indexSentence < parsedSentence.Length; indexSentence++)
                {
                    if(parsedSentence[indexSentence].Length < parseFindWord[indexFind].Length)
                    {
                        continue;
                    }
                    if (ComputeLevenshteinDistance(parsedSentence[indexSentence], parseFindWord[indexFind]))
                    {
                        lenghtFindWordParsed++;
                        //Maybe sentence contains same word than one time so break loop
                        break;
                    }
                }
            }

            if(lenghtFindWordParsed == parseFindWord.Length)
            {
                return true;
            }
           
           

            return false;
        }
        private bool ComputeLevenshteinDistance(string sentence,string findThis)
        {
            int[,] matrix = new int[sentence.Length + 1, findThis.Length + 1];

            for (int i = 0; i <= sentence.Length; i++)
                matrix[i, 0] = i;

            for (int j = 0; j <= findThis.Length; j++)
                matrix[0, j] = j;

            for (int i = 1; i <= sentence.Length; i++)
            {
                for (int j = 1; j <= findThis.Length; j++)
                {
                    int cost = (sentence[i - 1] == findThis[j - 1]) ? 0 : 1;

                    matrix[i, j] = Math.Min(
                        Math.Min(matrix[i - 1, j] + 1, matrix[i, j - 1] + 1),
                        matrix[i - 1, j - 1] + cost);
                }
            }
            int distance = matrix[sentence.Length, findThis.Length];

           // DebugPfCnsl.println("sentence = " + sentence + "  findThis = " + findThis);
            //DebugPfCnsl.println("distance = " + distance + "  findThis lenght = " + findThis.Length);

            if(findThis.Length < 3)
            {
                if(distance == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if(findThis.Length <= 6)
            {
                if(distance <= 1)
                {
                    return true ;
                }
                else
                {
                    return false;
                }
            }
           else if (findThis.Length > 6)
            {
                if (distance <= 2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            

            return false;
        
    }
        private bool CompareStrings(string sentence, string findWord)
        {
            char[] charSentence = sentence.ToArray();
            char[] charFindWord = findWord.ToArray();

            if(charFindWord.Length == 1)
            {

            }else
            {
               // if()
            }
            return false;
        }
        public string[] SplitSentenceByFirstSpace(string sentence)
        {
            int firstSpaceIndex = sentence.IndexOf(' ');

            if (firstSpaceIndex == -1)
            {
                // Eğer boşluk bulunamazsa, cümle tek bir kelimeden oluşuyor demektir
                return new string[] { sentence };
            }

            string firstPart = sentence.Substring(0, firstSpaceIndex);
            string secondPart = sentence.Substring(firstSpaceIndex + 1);

            return new string[] { firstPart, secondPart };
        }
    }
}
