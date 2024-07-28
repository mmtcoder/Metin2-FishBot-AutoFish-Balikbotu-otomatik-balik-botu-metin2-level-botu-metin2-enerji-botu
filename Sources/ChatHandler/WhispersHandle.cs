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
using System.Web;

namespace Metin2AutoFishCSharp.Sources.ChatHandler
{
    internal class WhispersHandle
    {
        private ImageObjects imagesObject;
        private TimerGame timerWhisperDetection;
        private TimerGame timerGenerateAnswer;
        private ScreenShotWinAPI screenShot;
        private GameObjectCoordinates coor;
        private GameInputHandler inputGame;
        private GameAlphabetDetecter chatDetecter;
        private ChatSentencer chatSentencer;
        private ChatFileHandler chatFileHandle;

        public Rectangle rectDetectedWhisperPanel = Rectangle.Empty;
        private readonly int MAX_CHAT_LINE = 7;

        private int whisperImageCounter = 0;
        private string storeAnotherPlayerOldChat = string.Empty;

        //current whisper Panel detection rect = {X=185,Y=73,Width=15,Height=13}
        //whisper Chattin area {X=53,Y=93,Width=256,Height=105}
        //whisper chat clicking point = {X=181,Y=206,Width=3,Height=2}
        //whisper chat close cross = {X=321,Y=79,Width=4,Height=5}

        bool isWhisperFirstDetected = false;
        private bool isWaitAnsweringActive = false;

        public WhispersHandle(ImageObjects imagesObject)
        {
            this.imagesObject = imagesObject;
            timerWhisperDetection = new TimerGame();
            screenShot = new ScreenShotWinAPI();
            coor = new GameObjectCoordinates(imagesObject);
            inputGame = new GameInputHandler();
            chatDetecter = new GameAlphabetDetecter(imagesObject);
            chatSentencer = new ChatSentencer();
            chatFileHandle = new ChatFileHandler();
            timerGenerateAnswer = new TimerGame();  
        }

        public void StartWhisperHandling()
        {
            OpenWhisperPanel();
            FocusWhisperText();
            SendMessageFromWhisper(DetectAndGenerateAnswer());
            CloseWhisperPanel();
        }
        public void OpenWhisperPanel()
        {
            DebugPfCnsl.println("OpenWhisperPanel is running");
            TimerGame timerOpenWhisper = new TimerGame();
            rectDetectedWhisperPanel = DetectWhisperPanel();
            while (rectDetectedWhisperPanel == Rectangle.Empty)
            {
                if (timerOpenWhisper.CheckDelayTimeInSecond(5))
                {
                    ClickWhisperBox();
                    TimerGame.SleepRandom(200, 300);
                    rectDetectedWhisperPanel = DetectWhisperPanel();
                }
                else
                {
                    DebugPfCnsl.println("Whisper Box couldn't open ");
                    return;
                }
            }

        }

        public void CloseWhisperPanel()
        {
            DebugPfCnsl.println("CloseWhisperPanel running");
            TimerGame timeClosePanel = new TimerGame();

            FileHandler.SaveImageAsPng(screenShot.CaptureSpecifiedScreen(coor.RectMetin2GameScreen()),
               "whisper" + whisperImageCounter++ + ".png", PathWayStruct.PATH_SCREENSHOTS);

            if (rectDetectedWhisperPanel != Rectangle.Empty)
            {
                while(DetectWhisperPanel() != Rectangle.Empty)
                {
                    if(timeClosePanel.CheckDelayTimeInSecond(5))
                    {
                        inputGame.MouseMoveAndPressLeft(rectDetectedWhisperPanel.X + 136,
                      rectDetectedWhisperPanel.Y + 6);
                        TimerGame.SleepRandom(300, 500);
                    }else
                    {
                        DebugPfCnsl.println("Whisper Panel couldn't close");
                        return ;
                    }
                  
                }
            }
            ThreadGlobals.isWhisperDetected = false;
        }

        public string[] DetectAndGenerateAnswer()
        {
            if (rectDetectedWhisperPanel != Rectangle.Empty)
            {
                bool isAnyDetectionMaken = false;
                string[] parseSentences = null;

                Rectangle rectCurrentChatDetection = new Rectangle(rectDetectedWhisperPanel.X - 132,
                    rectDetectedWhisperPanel.Y + 20, 256, 105);

                for (int line = 6; line >= 0; line--)
                {
                    Rectangle rectDetectOneLine = new Rectangle(rectCurrentChatDetection.X, rectCurrentChatDetection.Y + (15 * line),
                            rectCurrentChatDetection.Width, 15);

                    string chatResult = chatDetecter.DetectGameText(ColorGame.CHAT_WHITE_COLOR,
                           rectDetectOneLine);

                    if (chatResult != null && chatResult != string.Empty)
                    {
                         parseSentences = chatSentencer.SplitSentenceByFirstSpace(chatResult);

                      
                        if (parseSentences[0].ToCharArray().Length <= 0)
                        {
                            DebugPfCnsl.println("tespit edilen cümle boşluktan ibaret ...");

                            if (isAnyDetectionMaken)
                            {
                                DebugPfCnsl.println("Generated an answer for unkonw sentences and result = ");

                                string[] resultString = GenerateAnswerForUnknowns();
                                DebugPfCnsl.PrintArray(resultString);

                                chatFileHandle.RecordWhisperToFile("Fısıltı Tespit edilen kelime bulunamadi ", parseSentences, resultString);
                                return resultString;
                            }

                            return null;
                        }
                        if (parseSentences.Length > 1)
                        {

                            if (parseSentences[0].Equals(CharInfo.CharNameString))
                            {
                                DebugPfCnsl.println("kendi ismini tespit ettin");

                                break;
                                //return null;
                            }

                            if (parseSentences[1].Equals(storeAnotherPlayerOldChat))
                            {
                                DebugPfCnsl.println("tespit edilen cümle bir önceki ile aynı");

                                break;
                                // return null;
                            }
                            else
                            {
                                storeAnotherPlayerOldChat = parseSentences[1];
                            }
                            if (!timerGenerateAnswer.CheckCountDownSecond(30, 60))
                            {
                                if (!isWaitAnsweringActive)
                                {
                                    string generateAnswer = chatFileHandle.ChatGetAnswerFromFile(parseSentences[1]);
                                    

                                    if (generateAnswer != null && generateAnswer != string.Empty)
                                    {
                                       
                                        chatFileHandle.RecordWhisperToFile(ChatFileHandler.DetectedWord, parseSentences, generateAnswer);

                                        TelegramBot.SendMessageTelegram(parseSentences[0] + " adlı kullanıcı sana fısıltıdan bu mesajı gönderdi = " +
                                           parseSentences[1] + " Tespit ettiğin kelime = " + ChatFileHandler.DetectedWord + " Gönderdiğin cevap = " +
                                          generateAnswer);

                                        string[] arrayString = new string[1];
                                        arrayString[0] = generateAnswer;

                                        return arrayString;
                                    }
                                    else
                                    {
                                        isAnyDetectionMaken = true;
                                        /*DebugPfCnsl.println("Generated an answer for unkonw sentences and result = ");

                                        string[] resultString = GenerateAnswerForUnknowns();
                                        DebugPfCnsl.PrintArray(resultString);

                                        chatFileHandle.RecordWhisperToFile("Fısıltı Tespit edilen kelime bulunamadi ", parseSentences, resultString);


                                        return resultString;*/

                                    }
                                    timerGenerateAnswer.SetStartedSecondTime();
                                }
                                else
                                {
                                    chatFileHandle.RecordWhisperToFile(ChatFileHandler.DetectedWord, parseSentences, "Şu an fısıltıya " +
                                        "cevap verilmiyor");
                                }
                            }
                            else
                            {
                                isWaitAnsweringActive = false;
                                DebugPfCnsl.println("isWaitAnsweringActive time is elapsed so DetectAndGenerateAnswer is called again");
                                DetectAndGenerateAnswer();
                                return null;
                            }

                                  

                        }
                    }
                }
                if (isAnyDetectionMaken)
                {
                    if(parseSentences != null)
                    {
                        DebugPfCnsl.println("Generated an answer for unkonw sentences and result = ");

                        string[] resultString = GenerateAnswerForUnknowns();
                        DebugPfCnsl.PrintArray(resultString);

                        chatFileHandle.RecordWhisperToFile("Fısıltı Tespit edilen kelime bulunamadi ", parseSentences, resultString);

                        string telegramMessage = String.Join(" ",resultString);
                        TelegramBot.SendMessageTelegram(parseSentences[0] + " adlı kullanıcı sana fısıltıdan bu mesajı gönderdi = " +
                                          parseSentences[1] + " Tespit ettiğin herhangi kelime bulunamadı.Gönderdiğin cevap = " +
                                         telegramMessage);

                        return resultString;
                    }
                    else
                    {
                        return null;
                    }
                    
                }
                else
                {
                    return null;
                }

            }
                    else
                    {
                        DebugPfCnsl.println("DetectAndGenerateAnswer ' rectDetectedWhisperPanel ' field is empty");
                        DetectWhisperPanel();
                    }
                    return null;
                }

                private void FocusWhisperText()
                {
                    if (rectDetectedWhisperPanel != Rectangle.Empty)
                    {
                        inputGame.MouseMoveAndPressLeft(rectDetectedWhisperPanel.X,
                            rectDetectedWhisperPanel.Y + 133);
                    }
                }

                private void SendMessageFromWhisper(params string[] messages)
                {
                    if (messages == null)
                    {
                        DebugPfCnsl.println("SendMessageFromWhisper function returned ");
                        return;
                    }
                    for (int i = 0; i < messages.Length; i++)
                    {
                        inputGame.KeySendText(messages[i]);
                        inputGame.KeyPress(KeyboardInput.ScanCodeShort.ENTER);
                        TimerGame.SleepRandom(1500, 2500);
                    }
                }

                public bool CheckHasAnyWhisper()
                {
                    if ((ThreadGlobals.CheckGameIsStopped() || !ThreadGlobals.isSettingButtonSeemed))
                    {
                        DebugPfCnsl.println("CheckHAsANyWhisper function is returned false");
                        ThreadGlobals.isWhisperDetected = false;
                        return false;
                    }
                    int[] targetWhisperIcon = screenShot.ImageArraySpecifiedArea(
                        coor.RectWhisperOneArea());

                    if (!isWhisperFirstDetected)
                    {
                        if (imagesObject.CompareTwoArrayAdvanced(imagesObject.arrayWhisperIcon,
                       targetWhisperIcon, ImageSensibilityLevel.SENSIBILTY_HIGH))
                        {
                            ThreadGlobals.isWhisperDetected = true;
                            isWhisperFirstDetected = true;
                            timerWhisperDetection.SetStartedMilliSecTime();
                            return true;
                        }
                    }
                    else
                    {
                        if (timerWhisperDetection.CheckDelayTimeInMilliSec(2000))
                        {
                             //DebugPfCnsl.println("2 saniyenin içinde ");
                            return true;
                        }
                        else
                        {
                             // DebugPfCnsl.println("2 saniye geçti ");
                            isWhisperFirstDetected = false;
                            timerWhisperDetection.SetStartedMilliSecTime();
                            CheckHasAnyWhisper();
                        }
                    }
                    ThreadGlobals.isWhisperDetected = false;
                    return false;
                }

                private void ClickWhisperBox()
                {
                    inputGame.MouseMoveAndPressLeft(coor.RectWhisperOneArea().X,
                        coor.RectWhisperOneArea().Y);
                }
                private Rectangle DetectWhisperPanel()
                {
                    Rectangle[] result = imagesObject.FindAllImagesOnScreen(imagesObject.arrayWhisperPanelDetect,
                        coor.RectWhisperPanelDetecSample(), coor.RectMetin2GameScreen());

                    if (result != null)
                    {
                        if (result.Length > 0)
                        {
                            return result[0];
                        }
                    }
                    return Rectangle.Empty;
                }

        public string[] GenerateAnswerForUnknowns()
        {
            isWaitAnsweringActive = true;
            Random rand = new Random();
            int result = rand.Next(5);

            if (result == 1)
            {
                return new string[] { "balık tuttuğum zaman mesajlajmıyorum" };
            }
            else if (result == 2)
            {
                return new string[] { "şu an meşgulüm", "cevap yazmak istemiyorum" };

            }
            else if (result == 3)
            {
                return new string[] { "reis salda balık tutayım" };

            }
            else if (result == 4)
            {
                return new string[] {"tam hızımı almışken beni yavaşlatma ",
                    "sana zahmet mesaj atma" };
            }
            else if (result == 5)
            {
                return new string[] { "izin verirsen balık tutayım",
                "sana iyi oyunlar"};
            }
            else
            {
                return new string[] { "izin verirsen balık tutayım",
                "sana iyi oyunlar"};
            }
        }
    } 
}
