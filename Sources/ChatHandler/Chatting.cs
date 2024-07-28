using MusicPlayerApp.Debugs;
using MusicPlayerApp.Sources;
using MusicPlayerApp.Sources.CharacterHandle;
using MusicPlayerApp.Sources.ImageHandle;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Metin2AutoFishCSharp.Sources.ChatHandler
{
    internal class Chatting
    {
        private ImageObjects imagesObject;
        private GameAlphabetDetecter chatDetecter;
        private GameObjectCoordinates coor;
        private ChatSentencer chatSentencer;
        private ScreenShotWinAPI screenShot;
        private GameInputHandler inputGame;
        private TimerGame timerGenerateAnswer;
        private TimerGame timerTradeAnswerWait;

        public ChatFileHandler chatFileHandle;
        public WhispersHandle whispers;

        private string storeAnotherPlayerOldChat = string.Empty;
        //private int countUnkonwAnser = 0;   

        private bool isWaitAnsweringActive =false;
        private bool isTradeAnsweringWait = false;

        private int chatImageCounter = 1;

        public Chatting(ImageObjects imagesObject)
        {
            this.imagesObject = imagesObject;
            chatDetecter = new GameAlphabetDetecter(imagesObject);
            coor = new GameObjectCoordinates(imagesObject);
            chatSentencer = new ChatSentencer();
            screenShot = new ScreenShotWinAPI();
            inputGame = new GameInputHandler();
            chatFileHandle = new ChatFileHandler();
            timerGenerateAnswer = new TimerGame();
            whispers = new WhispersHandle(imagesObject);
            timerTradeAnswerWait = new TimerGame();
        }
        
        public void ChatStart()
        {
            
               
                    if(ChatIsActive())
                    {
                        DebugPfCnsl.println("Chat Is Active No Detection right now");
                        return;
                    }
                    if(CharInfo.CharNameString == string.Empty)
                    {
                        DebugPfCnsl.println("kendi ismini tespit etmediğin için chat return etti");
                        return;
                    }
                    bool isUnkownMessageDetected = false;
                    string[] parseSentences = null;

                    for (int oneLineChat = 0; oneLineChat < 4; oneLineChat++)
                    {
                        if (ThreadGlobals.CheckGameIsStopped() || !ThreadGlobals.isSettingButtonSeemed) return;

                        //In order to avoid using BitBlt locking longer, provide using it for other screenshot opretions
                        Rectangle chatWhiteDetection = imagesObject.FindBorderAreaForWantedColor(ColorGame.CHAT_WHITE_COLOR,
                            new Rectangle(coor.RectOneLineFullChatArea().X,
                           coor.RectOneLineFullChatArea().Y - (GameObjectCoordinates.DISTANCE_BETW_CHAT_HEIGHT * oneLineChat),
                           15, coor.RectOneLineFullChatArea().Height));

                        if(chatWhiteDetection == Rectangle.Empty)
                        {
                            continue;
                        }
                        Rectangle chatDetectArea = new Rectangle(coor.RectOneLineFullChatArea().X,
                           coor.RectOneLineFullChatArea().Y - (GameObjectCoordinates.DISTANCE_BETW_CHAT_HEIGHT * oneLineChat),
                           coor.RectOneLineFullChatArea().Width, coor.RectOneLineFullChatArea().Height);

                        string chatResult = chatDetecter.DetectGameText(ColorGame.CHAT_WHITE_COLOR,
                            chatDetectArea);

                        if (chatResult != null && chatResult != string.Empty)
                        {
                             parseSentences = chatSentencer.SplitSentenceByFirstSpace(chatResult);

                    //DebugPfCnsl.println("parsesentences[0] = " + parseSentences[0]);
                    // DebugPfCnsl.println("Charinfo.Charnamestring = " + CharInfo.CharNameString);
                    //DebugPfCnsl.println("parsesentences[0] char length = " + parseSentences[0].ToCharArray().Length);
                    // DebugPfCnsl.println("CharNameString char length = " + CharInfo.CharNameString.ToCharArray().Length);
                    //DebugPfCnsl.println("parseSentences Length = " + parseSentences.Length);
                    
                            if(parseSentences.Length <= 1 || (parseSentences[0].ToCharArray().Length <= 0 || 
                        parseSentences[0].Trim() == string.Empty || parseSentences[1].ToCharArray().Length <= 0 ||
                        parseSentences[1].Trim() == string.Empty))
                            {
                                DebugPfCnsl.println("tespit edilen cümle boşluktan ibaret...");
                                break;
                                //return;
                            }
                            
                               
                                if (parseSentences[0].Equals(CharInfo.CharNameString))
                                {
                                    DebugPfCnsl.println("kendi ismini tespit ettin");
                                    break;
                                   // return;
                                }

                                if (parseSentences[1].Equals(storeAnotherPlayerOldChat))
                                {
                                    DebugPfCnsl.println("tespit edilen cümle bir önceki ile aynı");
                                    break;
                                   // return;
                                }
                                else
                                {
                                    storeAnotherPlayerOldChat = parseSentences[1];
                                }

                        
                            if (!timerGenerateAnswer.CheckCountDownMinute(7, 17))
                          {
                            if (!isWaitAnsweringActive)
                            {

                                string generateAnswer = chatFileHandle.ChatGetAnswerFromFile(parseSentences[1]);

                                if (generateAnswer != null && generateAnswer != string.Empty)
                                {
                                 
                                    chatFileHandle.RecordChatToFile(ChatFileHandler.DetectedWord, parseSentences, generateAnswer);

                                TelegramBot.SendMessageTelegram(parseSentences[0] + " adlı kullanıcının chattaki mesajı = " +
                                            parseSentences[1] + " Tespit ettiğin kelime = " + ChatFileHandler.DetectedWord + " Gönderdiğin cevap = " +
                                           generateAnswer);

                                FileHandler.SaveImageAsPng(screenShot.CaptureSpecifiedScreen(coor.RectMetin2GameScreen()),
                                    "chatImage" + chatImageCounter++ + ".png", PathWayStruct.PATH_SCREENSHOTS);

                                ChatInTheGame(generateAnswer);
                                   
                                    return;
                                }
                                else
                                {
                                    isUnkownMessageDetected = true;
                                    TimerGame.SleepRandom(1500, 2000);
                                    oneLineChat = 0;
                                    /*DebugPfCnsl.println("Generated an answer for unkonw sentences and result = ");

                                    string[] resultString = GenerateAnswerForUnknowns();
                                    DebugPfCnsl.PrintArray(resultString);

                                    chatFileHandle.RecordChatToFile("Tespit edilen kelime bulunamadi ", parseSentences, resultString);

             
                                        ChatInTheGame(resultString);*/

                                }
                                timerGenerateAnswer.SetStartedMinuteTime();
                            }
                            else
                            {
                                chatFileHandle.RecordChatToFile("şu an tespit yapılmıyor", parseSentences, 
                                    "Şu an karşı oyuncuya cevap verilmiyor");
                            }

                            
                        }
                        else
                        {
                            //  DebugPfCnsl.println("time is elapsed");
                            isWaitAnsweringActive = false;
                        }

                        //}
                        //}
                    }
                        

                    }
                   
                    if(isUnkownMessageDetected)
            {
                if(!isWaitAnsweringActive)
                DebugPfCnsl.println("Generated an answer for unkonw sentences and result = ");

                string[] resultString = GenerateAnswerForUnknowns();
                DebugPfCnsl.PrintArray(resultString);

                chatFileHandle.RecordChatToFile("Tespit edilen kelime bulunamadi ", parseSentences, resultString);

                string telegramMessage = String.Join(" ", resultString);
                TelegramBot.SendMessageTelegram(parseSentences[0] + " adlı kullanıcının chattaki mesajı = " +
                                  parseSentences[1] + " Tespit ettiğin herhangi kelime bulunamadı.Gönderdiğin cevap = " +
                                 telegramMessage);

                FileHandler.SaveImageAsPng(screenShot.CaptureSpecifiedScreen(coor.RectMetin2GameScreen()),
                                   "chatImage" + chatImageCounter++ + ".png", PathWayStruct.PATH_SCREENSHOTS);

                ChatInTheGame(resultString);
            }
                
           
        }
        public bool ChatActivateOrNot(bool state)
        {
            TimerGame timerGame = new TimerGame(); 

            while(ChatIsActive() != state)
            {

                if(timerGame.CheckDelayTimeInSecond(5))
                {
                    if (ThreadGlobals.CheckGameIsStopped() || !ThreadGlobals.isSettingButtonSeemed) return false;

                    inputGame.KeyPress(KeyboardInput.ScanCodeShort.ENTER);

                    TimerGame.SleepRandom(300, 500);
                    
                }
                else
                {
                    DebugPfCnsl.println("Chat couldn't be maken user prefer !!");
                    return false;
                }
                
            }
            return true;
        }
       
        public void ChatInTheGame(params string[] sendStrings)
        {
            if(sendStrings == null || sendStrings.Length == 0) {
                DebugPfCnsl.println("ChatInTheGame sendStrings param is null");
                return; }

            while (ThreadGlobals.isActiveFishBoard)
            {
                if (ThreadGlobals.CheckGameIsStopped()) return;
            }

            for (int i = 0; i < sendStrings.Length; i++)
            {
                ThreadGlobals.isChatting = true;
                TimerGame.SleepRandom(1500, 2500);

                if (ChatActivateOrNot(true))
                {
                    inputGame.KeySendText(sendStrings[i]);
                }
                ChatActivateOrNot(false);

                ThreadGlobals.isChatting = false;
                TimerGame.SleepRandom(1500, 2500);
            }
          
        }
        public bool ChatIsActive()
        {
            int[] targetWhiteChatIsActive = imagesObject.RecordWantedColorIntArray(ColorGame.CHAT_WHITE_COLOR,
               screenShot.ImageArraySpecifiedArea(coor.RectChatIsActive()));

            if(imagesObject.compareTwoArrayQuickly(imagesObject.arrayWhiteChatIsActive,
                targetWhiteChatIsActive))return true;
            return false;
        }
       /* public bool DetectPlayerOnMiniMap()
        {
            if(ThreadGlobals.CheckGameIsStopped() || !ThreadGlobals.isSettingButtonSeemed) return false;
            for(int i = 0; i <2; i++)
            {
                if(i == 0)
                {
                    if(imagesObject.FindBorderAreaForWantedColor(ColorGame.MINIMAP_PLAYER_DARKYELLOW,
                        coor.RectMiniMapArea()) != Rectangle.Empty ||
                       imagesObject.FindBorderAreaForWantedColor(ColorGame.MINIMAP_PLAYER_OPENYELLOW,
                        coor.RectMiniMapArea()) != Rectangle.Empty)
                    {
                        ThreadGlobals.isEnemyDetected = false;
                        return true;
                    }
                }
                else
                {
                    if (imagesObject.FindBorderAreaForWantedColor(ColorGame.MINIMAP_PLAYER_DARKPINK,
                        coor.RectMiniMapArea()) != Rectangle.Empty ||
                        imagesObject.FindBorderAreaForWantedColor(ColorGame.MINIMAP_PLAYER_OPENPINK,
                        coor.RectMiniMapArea()) != Rectangle.Empty)
                    {
                        ThreadGlobals.isEnemyDetected = true;
                        return true;
                    }
                }
            }
            return false;
        }*/
       
        private string [] GenerateAnswerForUnknowns()
        {
            if(!ThreadGlobals.isFishingStopped)
            {
                isWaitAnsweringActive = true;
                Random rand = new Random();
                int result = rand.Next(7);

                if (result == 1)
                {
                    return new string[] { "şu an balık tutuyorum odaklanmam gerek" };
                }
                else if (result == 2)
                {
                    return new string[] { "reis k.bakma balık tutuyorum", "bu yüzden daha fazla mesajlaşmak istemiyorum" };
                }
                else if (result == 3)
                {
                    return new string[] { "şşt sessizlik", "izin verirsen balık tutayim" };
                }
                else if (result == 4)
                {
                    return new string[] { "balık tutarken mesajlajmayı sevmem", "şimdilik son mesajım" };
                }
                else if (result == 5)
                {
                    return new string[] { "sessiz kalma hakkımı kullanıyorum", "sana iyi oyunlar" };
                }
                else if (result == 6)
                {
                    return new string[] { "ben burda yokum varsay", "çok çalışmam gerek sana bay bay" };
                }
                else
                {
                    return new string[] { "balık tutuyorum şimdilik daha cevap yazmıyacağım" };
                }
            }
            else if(!ThreadGlobals.isLevelFarmStopped)
            {
                isWaitAnsweringActive = true;
                Random rand = new Random();
                int result = rand.Next(4);

                if (result == 1)
                {
                    return new string[] { "şu an kasılıyorum sana iyi oyunlar" };
                }
                else if (result == 2)
                {
                    return new string[] { "reis k.bakma kasılıyorum", "sana iyi oyunlar" };
                }
                else if(result == 3)
                {
                    return new string[] { "ben fazla yazışmayı sevmem benden bu kadar" };
                }
                else
                {
                    return new string[] { "oyuna yeni başladım kasılıyorum",
                    "kendimce takılacağım sana iyi oyunlar"};
                }
            }


            return new string[] { " ben fazla yazışmayı sevmem benden bu kadar" };




        }
        public void ChatForTrade()
        {
            if(!timerTradeAnswerWait.CheckCountDownSecond(30,60))
            {
                if(!isTradeAnsweringWait)
                {
                    ChatInTheGame(GenerateAnswerForTradeOpen());
                    timerTradeAnswerWait.SetStartedSecondTime();
                }
            }
            else
            {
                isTradeAnsweringWait = false;
                ChatForTrade();
            }
           
        }
        public string [] GenerateAnswerForTradeOpen()
        {
            if (!ThreadGlobals.isFishingStopped)
            {
                while (ThreadGlobals.isPrepareFishingStarted)
                {
                    if (ThreadGlobals.CheckGameIsStopped() || ThreadGlobals.isCharKilled) return null;
                }
                Random rand = new Random();
                int result = rand.Next(5);
                isTradeAnsweringWait = true;

                if (result == 1)
                {
                    return new string[] { "ne yapmaya çalışıyorsun" };
                }
                else if (result == 2)
                {
                    return new string[] { "balık tutuyorum niye tic atıyorsun" };
                }
                else if (result == 3)
                {
                    return new string[] { "hayırdır sorun mu var" };
                }
                else if (result == 4)
                {
                    return new string[] { "balıkları şimdi satmıyorum tic atma" };
                }
                else
                {
                    return new string[] { "tic atma balık tutuyorum görmüyor musun" };
                }
            }
            else if (!ThreadGlobals.isLevelFarmStopped)
            {
              
                Random rand = new Random();
                int result = rand.Next(5);
                isTradeAnsweringWait = true;

                if (result == 1)
                {
                    return new string[] { "ne yapmaya çalışıyorsun" };
                }
                else if (result == 2)
                {
                    return new string[] { "kasılıyorum reis tic atma" };
                }
                else if (result == 3)
                {
                    return new string[] { "hayırdır sorun mu var" };
                }
                else if (result == 4)
                {
                    return new string[] { "item gösterme yada soru sorma sana iyi oyunlar" };
                }
                else
                {
                    return new string[] { "ben oyundan anlamıyorum boşa birşey gösterme" };
                }
            }
            return new string[] { "ben oyundan anlamıyorum boşa birşey gösterme" };

        }
    }
}
