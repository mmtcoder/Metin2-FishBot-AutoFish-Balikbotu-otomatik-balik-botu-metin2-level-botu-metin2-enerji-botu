using Metin2AutoFishCSharp.Sources.CharacterHandle;
using Metin2AutoFishCSharp.Sources.ChatHandler;
using Metin2AutoFishCSharp.Sources.GameHandler;
using Metin2AutoFishCSharp.Sources.LevelAndFarms;
using MusicPlayerApp.Debugs;
using MusicPlayerApp.Sources.CharacterHandle;
using MusicPlayerApp.Sources.GameHandler;
using MusicPlayerApp.Sources.ImageHandle;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

namespace MusicPlayerApp.Sources
{
    internal class ThreadsHandler
    {
        public const string MainThreadName = "Main Thread";
        public const string T1Name = "Thread 1";
        public const string T2Name = "Thread 2";
        public const string T3Name = "Thread 3";

        private ScreenShotWinAPI screenShot;
       
        private DebugDrawingHandle debugScreen;

        private ImageObjects imageObject ;
        private FishingHandle fishing ;
        private CheckGameStatus gameStatus ;
        private GameObjectCoordinates coordinates ;
        private CharSpecialThings charThings;
        private Chatting chatting;
        private GameInputHandler inputGame;
        private GameAlphabetDetecter gameWords;
        private CharInfo charInfo;
        private CharPickUpItems charPickUp;
        private ChatSentencer chatSentencer;
        private PlayersHandler playersHandle;
        private LevelHandle levelHandle;
        private StatusHandler statusHandler;
        private CharMovement charMovement;
        private EnerjyCristalHandle enerjyCrisHandle;

        private TimerGame timeGeneral;
        private TimerGame timerLevelFarmBot;
        private TimerGame timeEnergyBot;

       // private static readonly object lockObject = new object();

         static Rectangle ScreenBounds = Screen.PrimaryScreen.Bounds;
      
       

       
        public ThreadsHandler(ImageObjects imageObject)
        {
            this.imageObject = imageObject;
            fishing = new FishingHandle(imageObject);
            gameStatus = new CheckGameStatus(imageObject);
            coordinates = new GameObjectCoordinates(imageObject);
            screenShot = new ScreenShotWinAPI();
            debugScreen = new DebugDrawingHandle();
            charThings = new CharSpecialThings(imageObject);
            timeGeneral = new TimerGame();
            chatting = new Chatting(imageObject);
            inputGame = new GameInputHandler();
            gameWords = new GameAlphabetDetecter(imageObject);
            charInfo = new CharInfo(imageObject);
            chatSentencer = new ChatSentencer();
            playersHandle = new PlayersHandler(imageObject);
            levelHandle = new LevelHandle(imageObject);
            statusHandler = new StatusHandler(imageObject,gameWords,levelHandle);
            charPickUp = new CharPickUpItems(imageObject, gameWords);
            timerLevelFarmBot = new TimerGame();
            timeEnergyBot = new TimerGame();
            charMovement = new CharMovement(imageObject, gameWords);
            enerjyCrisHandle = new EnerjyCristalHandle(imageObject,gameWords);
        }
        public void Start()
        {
          
            //@@@@@@@@    THREAD 1    @@@@@@@@@@@@

            // DebugPfCnsl.PrintArray(imageObject.FindAllImagesOnScreen(imageObject.array200WhiteNumber,
            //     coordinates.Rect200WhiteSample(), coordinates.RectFirstSlotPlace()));

            Thread t1 = new Thread(() =>
            {
               


                 HandleFormElement(MainForm.buttonFishingStartCopy, "", false);
                 ThreadGlobals.IsThreadOneActive = true;
                 MakeBackCounting();
                 HandleFormElement(MainForm.buttonFishingStartCopy, "", true);

                 DebugPfCnsl.println("min Work time = " + TimerGame.MIN_WORK_TIME
                         + " max work time = " + TimerGame.MAX_WORK_TIME);
                 DebugPfCnsl.println("min break time = " + TimerGame.MIN_BREAK_TIME
                        + " max break time = " + TimerGame.MAX_BREAK_TIME);
                 DebugPfCnsl.println("Game Stop Time = " + TimerGame.GAME_STOP_TIME);

                if(ThreadGlobals.isSettingButtonSeemed) {

                   /* if (!ThreadGlobals.isFishingStopped)
                    { charInfo.ProvideCharNameCanSee(); }
                    else if (!ThreadGlobals.isLevelFarmStopped)
                    {
                        charInfo.ProvideCharNameCanSee();
                        //levelHandle.GetAutoHunter().StartStopOtomatikAv();
                        
                    }*/
                    if(!ThreadGlobals.CheckGameIsStopped())
                    {
                        charInfo.ProvideCharNameCanSee();
                    }
                  
                }

                 while (ThreadGlobals.IsThreadOneActive)
                 {
                     if (ThreadGlobals.isTimerBreakEnabled)
                     {
                         if (!timeGeneral.CheckDelayTimeInMinute(TimerGame.GAME_STOP_TIME))
                         {
                             charThings.SettingButtonClick(SettingButtonPrefers.EXIT_BUTTON);
                             DebugPfCnsl.println("Game is STOPPED by Timer Global");
                             HandleFormElement(MainForm.labelCopyStartStatus, "Game is STOPPED by Timer Global");
                             Stop();
                             return;
                         }
                     }
                     if(!ThreadGlobals.isFishingStopped)
                    {
                        fishing.StartFishing();
                    }
                     else if(!ThreadGlobals.isLevelFarmStopped)
                    {
                        levelHandle.StartLevelAndFarming();
                    }
                     else if(!ThreadGlobals.isEnergyCristalStopped)
                    {
                        // charMovement.ReadMiniMapCoordinates();
                        enerjyCrisHandle.StartEnergyCristal();
                        
                    }
                     
                 }
            });
            t1.Name = T1Name;
            t1.Start();

            //@@@@@@@@@@   THREAD 2   @@@@@@@@@@@@@
            Thread t2 = new Thread(() =>
            {
                ThreadGlobals.IsThreadTwoActive = true;
                
                TimerGame.SleepRandom(1500, 2000);
                while (ThreadGlobals.IsThreadTwoActive)
                {
                   
                    gameStatus.StartCheckingGame();
                    //ThreadGlobalValues.DebugThreadGloablValues();
                   
                  
                } 
            });
            t2.Name = T2Name;
            t2.Start();

            //@@@@@@@@@@   THREAD 3   @@@@@@@@@@@@@
            Thread t3 = new Thread(() =>
            {
                
                ThreadGlobals.IsThreadThreeActive = true;
                Thread.Sleep(3100);

                
                while(ThreadGlobals.IsThreadThreeActive)
                {
                    // HandleFormElement(MainForm.pictureBox,"",false,screenShot.CaptureSpecifiedScreen(
                    //     coordinates.RectMiniMapArea()));
                    // DebugPfCnsl.println("detected player result = " + playersHandle.DetectAnotherPlayersMiniMap());
                    //if(chatting.DetectPlayerOnMiniMap())
                    //{

                    //DebugPfCnsl.println("Detected result = " + playersHandle.DetectAnotherPlayer());
                    if (!ThreadGlobals.isFishingStopped)
                    {
                        if (ThreadGlobals.isSettingButtonSeemed)
                        {
                            // levelHandle.ControlHpAndManaBar();
                            //DebugPfCnsl.println("status is detected = " + statusHandler.CheckStatusImproveTitle());

                            if (charThings.CheckTradePanelActive())
                            {
                                TimerGame.SleepRandom(2000, 4000);
                                charThings.CloseTradePanel();
                                chatting.ChatForTrade();
                            }

                            if (!ThreadGlobals.isPrepareFishingStarted)
                            {
                                // DebugPfCnsl.println("trade is active = " + charThings.CheckTradePanelActive());
                                // DebugPfCnsl.println("Whisper is detected = " + chatting.whispers.CheckHasAnyWhisper());
                                if(ThreadGlobals.isWhisperAnswerActive)
                                {
                                    if (chatting.whispers.CheckHasAnyWhisper())
                                    {
                                        chatting.whispers.StartWhisperHandling();
                                    }
                                }
                               
                                if(ThreadGlobals.isChattingAnswerActive)
                                {
                                    if (playersHandle.DetectAnotherPlayersMiniMap() || playersHandle.DetectAnotherPlayer())
                                    {
                                        chatting.ChatStart();
                                    }
                                }
                               

                            }
                        }
                    }
                    else if (!ThreadGlobals.isLevelFarmStopped)
                    {
                        
                        
                            if (ThreadGlobals.isSettingButtonSeemed)
                            {
                                if (!timerLevelFarmBot.CheckDelayTimeInSecond(3))
                                {
                                    if (charThings.CheckTradePanelActive())
                                    {
                                        DebugPfCnsl.println("birisi ticaret attı");
                                        TimerGame.SleepRandom(2000, 4000);
                                        charThings.CloseTradePanel();
                                       // chatting.ChatForTrade();
                                    }
                                  //  if (chatting.whispers.CheckHasAnyWhisper())
                                   // {
                                   //     chatting.whispers.StartWhisperHandling();
                                   // }
                                   // if (playersHandle.DetectAnotherPlayersMiniMap() || playersHandle.DetectAnotherPlayer())
                                   // {
                                   //     chatting.ChatStart();
                                   // }

                                    if (statusHandler.CheckStatusImproveTitle())
                                    {
                                        statusHandler.StartStatusHandle();
                                    }
                                    if(ThreadGlobals.isETPPickUpActive)
                                     {
                                    timerLevelFarmBot.SetStartedSecondTime();
                                     }
                                    
                                }

                                charPickUp.PickUpWantedItem("ejderha taşı");
                            }
                        }else if(!ThreadGlobals.isEnemyDetected)
                    {
                        if (!timeEnergyBot.CheckDelayTimeInSecond(3))
                        {
                            if (charThings.CheckTradePanelActive())
                            {
                                DebugPfCnsl.println("birisi ticaret attı");
                                TimerGame.SleepRandom(700, 1400);
                                charThings.CloseTradePanel();
                                //chatting.ChatForTrade();
                            }
                            timeEnergyBot.SetStartedSecondTime();
                        }
                        }

                        

                    
                   
                    
                    //}
                }
            });
            t3.Name = T3Name;
            t3.Start();
        }

        public void Stop()
        {
            DebugPfCnsl.println(" Stop function is called");
            //ThreadGlobals.isFishingStopped = true;
           
            //HandleFormElement(MainForm.labelCopyStartStatus, "Fish Bot is stopped");
            ThreadGlobals.SetDefaultGloabalValues();

            

            new Thread(() =>
            {
                HandleFormElement(MainForm.buttonFishingStartCopy, "", false);
                while (ThreadGlobals.IsThreadOneActive || ThreadGlobals.IsThreadTwoActive ||
                ThreadGlobals.IsThreadThreeActive) { }
                HandleFormElement(MainForm.buttonFishingStartCopy, "", true);

                
            }).Start();
        }

        public void HandleFormElement(Control visComponent, string text = "", bool state = false, Bitmap bitmapClipped = null)
        {
            if (visComponent != null)
            {
                if(visComponent.InvokeRequired) 
                {
                    if (visComponent is Label)
                    {
                        Action action = () => { visComponent.Text = text; };
                        visComponent.Invoke(action);
                    }
                    else if (visComponent is Button)
                    {
                        Action action = () => { visComponent.Enabled = state; };
                        visComponent.Invoke(action);
                    }
                    else if (visComponent is PictureBox box)
                    {
                        if (bitmapClipped != null)
                        {
                            Action action = () => { box.Image = bitmapClipped; };
                            visComponent.Invoke(action);
                        }
                    }
                    else if (visComponent is TextBox textBox)
                    {
                        Action action = () => { textBox.Text = text; };
                        visComponent.Invoke(action);
                    }
                }
                else
                {
                    if (visComponent is Label)
                    {
                        visComponent.Text = text;
                    }
                    else if (visComponent is Button)
                    {
                        visComponent.Enabled = state;
                    }
                    else if (visComponent is PictureBox box)
                    {
                        if (bitmapClipped != null)
                        {
                            box.Image = bitmapClipped;
                        }
                    }
                    else if(visComponent is TextBox textBox)
                    {
                        textBox.Text = text;
                    }
                }
             
            }
        }

        private void MakeBackCounting()
        {
            if (!ThreadGlobals.isFishingStopped)
            {
                HandleFormElement(MainForm.labelCopyStartStatus, "Fish Bot is starting 3 seconds ");
                Thread.Sleep(1000);

                HandleFormElement(MainForm.labelCopyStartStatus, "Fish Bot is starting 2 seconds ");
                Thread.Sleep(1000);

                HandleFormElement(MainForm.labelCopyStartStatus, "Fish Bot is starting 1 seconds ");
                Thread.Sleep(1000);

                HandleFormElement(MainForm.labelCopyStartStatus, "Fish Bot is started ");
            }
            else if(!ThreadGlobals.isLevelFarmStopped)
            {
                HandleFormElement(MainForm.labelCopyLevelFarmStatus, "Level Farm Bot is starting 3 seconds ");
                Thread.Sleep(1000);

                HandleFormElement(MainForm.labelCopyLevelFarmStatus, "Level Farm Bot is starting 2 seconds ");
                Thread.Sleep(1000);

                HandleFormElement(MainForm.labelCopyLevelFarmStatus, "Level Farm Bot is starting 1 seconds ");
                Thread.Sleep(1000);

                HandleFormElement(MainForm.labelCopyLevelFarmStatus, "Level Farm Bot is started ");
            }
            else if(!ThreadGlobals.isEnergyCristalStopped)
            {
                HandleFormElement(MainForm.labelCopyEnergyCristalStatus, "Level Farm Bot is starting 3 seconds ");
                Thread.Sleep(1000);

                HandleFormElement(MainForm.labelCopyEnergyCristalStatus, "Level Farm Bot is starting 2 seconds ");
                Thread.Sleep(1000);

                HandleFormElement(MainForm.labelCopyEnergyCristalStatus, "Level Farm Bot is starting 1 seconds ");
                Thread.Sleep(1000);
            }
            
        }

        
    }
}
