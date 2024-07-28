using MusicPlayerApp.Debugs;
using MusicPlayerApp.Sources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metin2AutoFishCSharp.Sources.LevelAndFarms
{
    internal class SkillsHandler
    {
        private TimerGame[] timersGameSkillTime;
        private bool[] stateKeyPresses;
        private TimerGame timerWaitSkillUsing;
        private GameInputHandler inputHandler;

       // private bool firstPress = false;
        public SkillsHandler() 
        {
            timersGameSkillTime = new TimerGame[8];
            timersGameSkillTime[0] = new TimerGame();
            timersGameSkillTime[1] = new TimerGame();
            timersGameSkillTime[2] = new TimerGame();
            timersGameSkillTime[3] = new TimerGame();
            timersGameSkillTime[4] = new TimerGame();
            timersGameSkillTime[5] = new TimerGame();
            timersGameSkillTime[6] = new TimerGame();
            timersGameSkillTime[7] = new TimerGame();

            stateKeyPresses = new bool[8];
            timerWaitSkillUsing = new TimerGame();
            inputHandler = new GameInputHandler();
        }

        public void StartSkillUsing()
        {
            if(ThreadGlobals.isLevelFarmStopped || !ThreadGlobals.isSettingButtonSeemed)
            {
                DebugPfCnsl.println("StartSkillUsing is returned");
            }
            if (AutoHunter.IS_AUTO_HUNTER_STARTED)
            {
               
                    //for key_1 
                    if (ThreadGlobals.SKILL_TIME_FOR_KEYS[0] >= 0)
                    {
                        PressWantedSkill(ref stateKeyPresses[0], timersGameSkillTime[0], ThreadGlobals.SKILL_TIME_FOR_KEYS[0]
                            ,KeyboardInput.ScanCodeShort.KEY_1);
                       
                       
                    }
                    //for key_2 
                    if (ThreadGlobals.SKILL_TIME_FOR_KEYS[1] >= 0)
                    {
                        PressWantedSkill(ref stateKeyPresses[1], timersGameSkillTime[1], ThreadGlobals.SKILL_TIME_FOR_KEYS[1]
                            ,KeyboardInput.ScanCodeShort.KEY_2);
                       
                    }
                    //for key_3
                    if (ThreadGlobals.SKILL_TIME_FOR_KEYS[2] >= 0)
                    {
                        PressWantedSkill(ref stateKeyPresses[2], timersGameSkillTime[2], ThreadGlobals.SKILL_TIME_FOR_KEYS[2]
                            ,KeyboardInput.ScanCodeShort.KEY_3);
                        
                    }
                    //for key_4
                    if (ThreadGlobals.SKILL_TIME_FOR_KEYS[3] >= 0)
                    {
                        PressWantedSkill(ref stateKeyPresses[3], timersGameSkillTime[3], ThreadGlobals.SKILL_TIME_FOR_KEYS[3]
                            ,KeyboardInput.ScanCodeShort.KEY_4);
                        
                    }
                    //for key_F1
                    if (ThreadGlobals.SKILL_TIME_FOR_KEYS[4] >= 0)
                    {
                        PressWantedSkill(ref stateKeyPresses[4], timersGameSkillTime[4], ThreadGlobals.SKILL_TIME_FOR_KEYS[4]
                            ,KeyboardInput.ScanCodeShort.F1);
                       
                    }
                    //for key_F2
                    if (ThreadGlobals.SKILL_TIME_FOR_KEYS[5] >= 0)
                    {
                        PressWantedSkill(ref stateKeyPresses[5], timersGameSkillTime[5], ThreadGlobals.SKILL_TIME_FOR_KEYS[5]
                            ,KeyboardInput.ScanCodeShort.F2);
                       
                    }
                    //for key_F3
                    if (ThreadGlobals.SKILL_TIME_FOR_KEYS[6] >= 0)
                    {
                        PressWantedSkill(ref stateKeyPresses[6], timersGameSkillTime[6], ThreadGlobals.SKILL_TIME_FOR_KEYS[6]
                            ,KeyboardInput.ScanCodeShort.F3);
                       
                    }
                    //for key_F4
                    if (ThreadGlobals.SKILL_TIME_FOR_KEYS[7] >= 0)
                    {
                        PressWantedSkill(ref stateKeyPresses[7], timersGameSkillTime[7], ThreadGlobals.SKILL_TIME_FOR_KEYS[7]
                            ,KeyboardInput.ScanCodeShort.F4);
                       
                    }
                    
                   // firstPress = true;
                
                
            }
        }

        private void PressWantedSkill(ref bool state,TimerGame timer,int delayTime,KeyboardInput.ScanCodeShort code)
        {
            if (timer.CheckDelayTimeInSecond(delayTime))
            {
                if (!state)
                {
                    inputHandler.KeyPress(KeyboardInput.ScanCodeShort.TAB);
                    inputHandler.KeyPress(code);
                    state = true;
                    DebugPfCnsl.println("tuş " +code.ToString() + " basıldı");
                    TimerGame.SleepRandom(3000, 3200);
                }
            }
            else
            {
                state = false;
                timer.SetStartedSecondTime();
            }
        }
    }
}
